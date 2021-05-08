//
//  MSCompressedDataFormat.cs - provides a DataFormat for manipulating MSCompressed archive files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  Portions of this program use SZDDComp, licensed under GPLv2 or later.
//  See Internal/SZDDComp.cs for more information.
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

using MBS.Framework;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.MSCompressed
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating MSCompressed archive files.
	/// </summary>
	public class MSCompressedDataFormat : DataFormat
	{
		private readonly byte?[] KWAJsignal = new byte?[] { (byte)'K', (byte)'W', (byte)'A', (byte)'J', (byte)0x88, (byte)0xF0, (byte)0x27, (byte)0xD1 };
		private readonly byte?[] SZDDsignal = new byte?[] { (byte)'S', (byte)'Z', (byte)'D', (byte)'D', (byte)0x88, (byte)0xF0, (byte)0x27, (byte)0x33 };
		private readonly byte?[] SZsignal = new byte?[] { (byte)'S', (byte)'Z', (byte)' ', (byte)0x88, (byte)0xF0, (byte)0x27, (byte)0x33, (byte)0xD1 };

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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);

			string fileName = Accessor.GetFileTitle();
			char fileNameExt = '\0';

			byte[] signal = br.ReadBytes(8);
			if (signal.Match(SZDDsignal))
			{
				// Compression mode: only "A" (0x41) is valid here
				byte compressonMode = br.ReadByte();

				// The character missing from the end of the filename (0=unknown)
				fileNameExt = br.ReadChar();

				if (fileNameExt != '\0')
					fileName = fileName.Substring(fileName.Length - 1) + fileNameExt;

				// The integer length of the file when unpacked
				int unpackedLength = br.ReadInt32();

				Decompress(br, bw, MSCompressedCompressionMethod.SZDD);
			}
			else if (signal.Match(SZsignal))
			{
				// The integer length of the file when unpacked
				int unpackedLength = br.ReadInt32();

				Decompress(br, bw, MSCompressedCompressionMethod.SZ);
			}
			else if (signal.Match(KWAJsignal))
			{
				// compression method (0-4)
				MSCompressedKWAJCompressionMethod compressionMethod = (MSCompressedKWAJCompressionMethod)br.ReadInt16();

				// file offset of compressed data
				short compressionFileOffset = br.ReadInt16();

				MSCompressedKWAJHeaderFlags headerFlags = (MSCompressedKWAJHeaderFlags)br.ReadInt16();
				if ((headerFlags & MSCompressedKWAJHeaderFlags.HasDecompressedLength) == MSCompressedKWAJHeaderFlags.HasDecompressedLength)
				{
					int decompressedLength = br.ReadInt32();
				}
				if ((headerFlags & MSCompressedKWAJHeaderFlags.UnknownBit1) == MSCompressedKWAJHeaderFlags.UnknownBit1)
				{
					short unknown1 = br.ReadInt16();
				}
				if ((headerFlags & MSCompressedKWAJHeaderFlags.HasExtraData) == MSCompressedKWAJHeaderFlags.HasExtraData)
				{
					short dataLength = br.ReadInt16();
					byte[] extraData = br.ReadBytes(dataLength);
				}
				if ((headerFlags & MSCompressedKWAJHeaderFlags.HasFileName) == MSCompressedKWAJHeaderFlags.HasFileName)
				{
					fileName = br.ReadNullTerminatedString();
				}
				if ((headerFlags & MSCompressedKWAJHeaderFlags.HasFileExtension) == MSCompressedKWAJHeaderFlags.HasFileExtension)
				{
					string fileExt = br.ReadNullTerminatedString();
				}
				if ((headerFlags & MSCompressedKWAJHeaderFlags.HasExtraText) == MSCompressedKWAJHeaderFlags.HasExtraText)
				{
					short dataLength = br.ReadInt16();
					string extraData = br.ReadFixedLengthString(dataLength);
				}

				switch (compressionMethod)
				{
					case MSCompressedKWAJCompressionMethod.None:
					{
						Decompress(br, bw, MSCompressedCompressionMethod.None);
						break;
					}
					case MSCompressedKWAJCompressionMethod.XOR:
					{
						Decompress(br, bw, MSCompressedCompressionMethod.XOR);
						break;
					}
					case MSCompressedKWAJCompressionMethod.SZDD:
					{
						Decompress(br, bw, MSCompressedCompressionMethod.SZDD);
						break;
					}
					case MSCompressedKWAJCompressionMethod.JeffJohnson:
					{
						int ringBufferPos = 4096 - 17;
						/*
						selected table = MATCHLEN
						LOOP:
							code = read huffman code using selected table (MATCHLEN or MATCHLEN2)
							if EOF reached, exit loop
							if code > 0, this is a match:
								match length = code + 2
								x = read huffman code using OFFSET table
								y = read 6 bits
								match offset = current ring buffer position - (x<<6 | y)
								copy match as output and into the ring buffer
								selected table = MATCHLEN
							if code == 0, this is a run of literals:
								x = read huffman code using LITLEN table
								if x != 31, selected table = MATCHLEN2
								read {x+1} literals using LITERAL huffman table, copy as output and into the ring buffer
						*/
						break;
					}
					case MSCompressedKWAJCompressionMethod.MSZIP:
					{
						break;
					}
				}
			}

			bw.Flush();
			bw.Close();

			fsom.Files.Add(fileName, ma.ToArray());
		}

		private void Decompress(IO.Reader br, IO.Writer bw, MSCompressedCompressionMethod method)
		{
			switch (method)
			{
				case MSCompressedCompressionMethod.None:
				case MSCompressedCompressionMethod.XOR:
				{
					byte[] rest = br.ReadToEnd();
					if (method == MSCompressedCompressionMethod.XOR)
					{
						for (int i = 0; i < rest.Length; i++) rest[i] ^= 0xFF;
					}
					bw.WriteBytes(rest);
					break;
				}
				case MSCompressedCompressionMethod.SZDD:
				case MSCompressedCompressionMethod.SZ:
				{
					br.Seek(-14, IO.SeekOrigin.Current); // SZDDComp expects to start reading at the beginning of the 'SZDD' signature

					byte[] data = br.ReadToEnd();
					System.IO.MemoryStream msin = new System.IO.MemoryStream(data);
					System.IO.MemoryStream msout = new System.IO.MemoryStream();
					Internal.SZDDComp.Decode(msin, msout);
					bw.WriteBytes(msout.ToArray());

					byte[] window = new byte[4096];
					window.Clear(0x20);

					int pos = 4096 - 16;
					if (method == MSCompressedCompressionMethod.SZ)
					{
						pos = 4096 - 18;
					}

					while (!br.EndOfStream)
					{
						byte control = br.ReadByte();
						if (br.EndOfStream)
							break;

						for (int cbit = 0x01; ((cbit & 0xFF) != 0); cbit = (byte)(cbit << 1))
						{
							if ((control & cbit) != 0)
							{
								// literal
								try
								{
									bw.WriteByte((byte)(window[(pos++ ^ window.Length)] = br.ReadByte()));
								}
								catch (Exception ex)
								{
									break;
								}
							}
							else
							{
								// match
								int matchpos = br.ReadByte();
								int matchlen = br.ReadByte();
								matchpos |= (byte)((matchlen & 0xF0) << 4);
								matchlen = (byte)((matchlen & 0x0F) + 3);
								while ((matchlen--) != 0)
								{
									bw.WriteByte((byte)(window[(pos++ ^ window.Length)] = window[matchpos++]));
									pos &= 4095; matchpos &= 4095;
								}
							}
						}
					}
					break;
				}
				case MSCompressedCompressionMethod.JeffJohnson:
				{
					break;
				}
				case MSCompressedCompressionMethod.MSZIP:
				{
					break;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
