//
//  DLTDataFormat.cs - provides a DataFormat for manipulating archives in Apogee DLT format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com> - Universal Editor port
//
//	Adapted from source published by:
//       Adam Nielsen <malvineous@shikadi.net>, The_coder, Wormbo (xentax forum)
//       http://www.shikadi.net/moddingwiki/DLT_Format
//
//  Copyright (c) 2019-2020 Mike Becker's Software and contributors
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Diagnostics;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Apogee
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Apogee DLT format.
	/// </summary>
	public class DLTDataFormat : DataFormat
	{
		/// <summary>
		/// Universal Editor <see cref="CompressionModule" /> implementation of Apogee's PGBP compression algorithm.
		/// </summary>
		private class PGBPCompressionModule : CompressionModule
		{
			public override string Name => "Apogee PGBP";

			/// <summary>
			/// Chunk size used during compression.  Each chunk expands to this amount of data.
			/// </summary>
			const int CHUNK_SIZE = 4096;

			/// <summary>
			/// Largest possible chunk of compressed data.  (No compression + worst case dictionary size.)
			/// </summary>
			const int CMP_CHUNK_SIZE = (CHUNK_SIZE + 256);

			protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
			{
				throw new NotImplementedException();
			}

			protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
			{
				// Make sure the input data has a valid "PGBP" signature.
				Reader reader = new Reader(new StreamAccessor(inputStream));
				Writer writer = new Writer(new StreamAccessor(outputStream));

				string signature = reader.ReadFixedLengthString(4);
				if (signature != "PGBP")
				{
					throw new InvalidDataFormatException("ERROR: Input file is not a Stargunner compressed file.");
				}

				// Read the header
				uint outSize = reader.ReadUInt32();
				Console.WriteLine("ue: compression: stargun: writing {0} bytes", outSize);
				byte[] chunkOut = new byte[CHUNK_SIZE];

				while (!reader.EndOfStream)
				{
					int chunkSize = reader.ReadUInt16();
					if (chunkSize > CMP_CHUNK_SIZE)
					{
						throw new InvalidDataFormatException("ERROR: Compressed chunk is too large!");
					}

					byte[] chunk = reader.ReadBytes(chunkSize);
					uint lenOut;
					if (outSize < CHUNK_SIZE) lenOut = outSize;
					else lenOut = CHUNK_SIZE;
					if (explode_chunk(chunk, lenOut, ref chunkOut) != 0)
					{
						throw new InvalidDataFormatException("ERROR: Failed to explode chunk!");
					}

					byte[] outChunk = new byte[lenOut];
					Array.Copy(chunkOut, 0, outChunk, 0, Math.Min(outChunk.Length, chunkOut.Length));
					writer.WriteBytes(outChunk);
					outSize -= lenOut;
				}
			}

			/// <summary>
			/// Decompress a data chunk.
			/// </summary>
			/// <param name="_in">Input data.  First byte is the one immediately following the chunk length.</param>
			/// <param name="expanded_size">The size of the input chunk after decompression.  The output buffer must be able to hold this many bytes.</param>
			/// <param name="_out">Output buffer.</param>
			uint explode_chunk(byte[] _in, uint expanded_size, ref byte[] _out)
			{
				byte[] tableA = new byte[256], tableB = new byte[256];
				uint inpos = 0;
				uint outpos = 0;

				while (outpos < expanded_size)
				{
					// Initialise the dictionary so that no bytes are codewords (or if you
					// prefer, each byte expands to itself only.)
					for (int i = 0; i < 256; i++) tableA[i] = (byte)i;

					//
					// Read in the dictionary
					//
					byte code;
					uint tablepos = 0;
					do
					{
						code = _in[inpos++];

						// If the code has the high bit set, the lower 7 bits plus one is the
						// number of codewords that will be skipped from the dictionary.  (Those
						// codewords were initialised to expand to themselves in the loop above.)
						if (code > 127)
						{
							tablepos += (uint)(code - 127);
							code = 0;
						}
						if (tablepos == 256) break;

						// Read in the indicated number of codewords.
						for (int i = 0; i <= code; i++)
						{
							Debug.Assert(tablepos < 256);
							byte data = _in[inpos++];
							tableA[tablepos] = data;
							if (tablepos != data)
							{
								// If this codeword didn't expand to itself, store the second byte
								// of the expansion pair.
								tableB[tablepos] = _in[inpos++];
							}
							tablepos++;
						}
					} while (tablepos < 256);

					// Read the length of the data encoded with this dictionary
					int len = _in[inpos++];
					len |= _in[inpos++] << 8;

					//
					// Decompress the data
					//

					int expbufpos = 0;
					// This is the maximum number of bytes a single codeword can expand to.
					byte[] expbuf = new byte[32];
					while (true)
					{
						if (expbufpos != 0)
						{
							// There is data in the expansion buffer, use that
							code = expbuf[--expbufpos];
						}
						else
						{
							// There is no data in the expansion buffer, use the input data
							if (--len == -1) break; // no more input data
							code = _in[inpos++];
						}

						if (code == tableA[code])
						{
							// This byte is itself, write this to the output
							_out[outpos++] = code;
						}
						else
						{
							// This byte is actually a codeword, expand it into the expansion buffer
							Debug.Assert(expbufpos < expbuf.Length - 2);

							expbuf[expbufpos++] = tableB[code];
							expbuf[expbufpos++] = tableA[code];
						}
					}
				}
				return (uint)(outpos - expanded_size);
			}
		}

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4); // DAVE
			if (signature != "DAVE") throw new InvalidDataFormatException("DLT file does not begin with 'DAVE'");

			short version = reader.ReadInt16(); // who knows

			short fileCount = reader.ReadInt16();
			for (short i = 0; i < fileCount; i++)
			{
				byte[] filenameBytes = reader.ReadBytes(32);
				filenameBytes = decodeFileName(filenameBytes);

				string fileName = System.Text.Encoding.Default.GetString(filenameBytes);
				fileName = fileName.TrimNull();
				fileName = fileName.Replace('\\', '/');

				int unknown1 = reader.ReadInt32();
				int compressedSize = reader.ReadInt32();

				long offset = reader.Accessor.Position;

				string pgbp = reader.ReadFixedLengthString(4);
				int decompressedSize = reader.ReadInt32();

				reader.Seek(compressedSize - 8, SeekOrigin.Current);

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("offset", offset);
				file.Properties.Add("compressedLength", compressedSize);
				file.Properties.Add("decompressedLength", decompressedSize);
				file.DataRequest += File_DataRequest;
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			int length = (int)file.Properties["compressedLength"];

			Accessor.Reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = Accessor.Reader.ReadBytes(length);

			PGBPCompressionModule pgbp = new PGBPCompressionModule();
			byte[] decompressedData = pgbp.Decompress(compressedData);
			e.Data = decompressedData;
		}


		private byte[] decodeFileName(byte[] input)
		{
			for (int i = 1; i < 32; i++)
				input[i] = (byte)((input[i - 1] + i) ^ input[i]);
			return input;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
