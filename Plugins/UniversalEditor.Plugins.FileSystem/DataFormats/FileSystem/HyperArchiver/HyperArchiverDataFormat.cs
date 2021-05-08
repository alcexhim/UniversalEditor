//
//  HyperArchiverDataFormat.cs - provides a DataFormat for manipulating archives in HyperArchiver format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HyperArchiver
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in HyperArchiver format.
	/// </summary>
	public class HyperArchiverDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			if (br.ReadByte() != 0x1a)
				throw new InvalidDataFormatException();

			while (!br.EndOfStream)
			{
				Compression.CompressionMethod CompressionMethod = Compression.CompressionMethod.None;
				switch (br.ReadFixedLengthString(2))
				{
					case "HP":
					{
						CompressionMethod = Compression.CompressionMethod.LZW;
						break;
					}
					case "ST":
					{
						CompressionMethod = Compression.CompressionMethod.None;
						break;
					}
					case "BZ":
					{
						CompressionMethod = Compression.CompressionMethod.Bzip2;
						break;
					}
					case "DE":
					{
						CompressionMethod = Compression.CompressionMethod.Deflate;
						break;
					}
					case "DZ":
					{
						CompressionMethod = Compression.CompressionMethod.Deflate64;
						break;
					}
					case "GZ":
					{
						CompressionMethod = Compression.CompressionMethod.Gzip;
						break;
					}
					case "MA":
					{
						CompressionMethod = Compression.CompressionMethod.LZMA;
						break;
					}
					case "ZS":
					{
						CompressionMethod = Compression.CompressionMethod.LZSS;
						break;
					}
					case "PP":
					{
						CompressionMethod = Compression.CompressionMethod.PPPMd;
						break;
					}
				}

				byte version = br.ReadByte();
				int compressedFileSize = br.ReadInt32();
				uint decompressedFileSize = br.ReadUInt32();
				int dateTimeMSDOS = br.ReadInt32();
				int crc32 = br.ReadInt32();
				byte fileAttribute = br.ReadByte();
				byte fileNameLength = br.ReadByte();
				string fileName = br.ReadFixedLengthString(fileNameLength);

				File f = fsom.AddFile(fileName);
				f.DataRequest += F_DataRequest;
				f.Properties.Add("reader", br);
				f.Properties.Add("offset", base.Accessor.Position);
				f.Properties.Add("method", CompressionMethod);
				f.Properties.Add("compressedLength", compressedFileSize);
				f.Properties.Add("decompressedLength", decompressedFileSize);

				br.Seek(compressedFileSize, SeekOrigin.Current);
			}
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader br = (Reader)f.Properties["reader"];
			Compression.CompressionMethod CompressionMethod = (Compression.CompressionMethod)f.Properties["method"];
			int compressedLength = (int)f.Properties["compressedLength"];
			uint decompressedLength = (uint)f.Properties["decompressedLength"];
			byte[] compressedData = br.ReadBytes(compressedLength);
			byte[] decompressedData = Compression.CompressionModule.FromKnownCompressionMethod(CompressionMethod).Decompress(compressedData); //, decompressedLength);
			e.Data = decompressedData;
		}

		public byte Version { get; set; } = 0;


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteByte(0x1a);

			File[] files = fsom.GetAllFiles();
			for (int i = 0; i < files.Length; i++)
			{
				Compression.CompressionMethod compressionMethod = Compression.CompressionMethod.None; // files[i].CompressionMethod;
				switch (compressionMethod)
				{
					case Compression.CompressionMethod.Deflate:
					{
						bw.WriteFixedLengthString("DE");
						break;
					}
					case Compression.CompressionMethod.Deflate64:
					{
						bw.WriteFixedLengthString("DZ");
						break;
					}
					case Compression.CompressionMethod.Bzip2:
					{
						bw.WriteFixedLengthString("BZ");
						break;
					}
					case Compression.CompressionMethod.LZMA:
					{
						bw.WriteFixedLengthString("MA");
						break;
					}
					case Compression.CompressionMethod.LZW:
					{
						bw.WriteFixedLengthString("HP");
						break;
					}
					case Compression.CompressionMethod.LZSS:
					{
						bw.WriteFixedLengthString("ZS");
						break;
					}
					case Compression.CompressionMethod.PPPMd:
					{
						bw.WriteFixedLengthString("PP");
						break;
					}
					case Compression.CompressionMethod.Gzip:
					{
						bw.WriteFixedLengthString("GZ");
						break;
					}
					default:
					{
						bw.WriteFixedLengthString("ST");
						break;
					}
				}

				bw.WriteByte(Version);
				byte[] decompressedData = files[i].GetData();
				byte[] compressedData = Compression.CompressionModule.FromKnownCompressionMethod(compressionMethod).Compress(decompressedData);
				int compressedFileSize = compressedData.Length;
				uint decompressedFileSize = (uint)decompressedData.Length;
				bw.WriteInt32(compressedFileSize);
				bw.WriteUInt32(decompressedFileSize);
				int dateTimeMSDOS = 0;
				bw.WriteInt32(dateTimeMSDOS);
				int crc32 = 0;
				bw.WriteInt32(crc32);
				byte fileAttribute = 0;
				bw.WriteByte(fileAttribute);
				byte fileNameLength = (byte)files[i].Name.Length;
				bw.WriteByte(fileNameLength);
				bw.WriteFixedLengthString(files[i].Name, fileNameLength);
				bw.WriteBytes(compressedData);
			}
		}
	}
}
