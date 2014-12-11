using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BGA
{
	public class BGADataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("IZArc BGA archive", new byte?[][] { new byte?[] { null, null, null, null, (byte)'B', (byte)'Z', (byte)'2', (byte)0 }, new byte?[] { null, null, null, null, (byte)'G', (byte)'Z', (byte)'I', (byte)'P' } }, new string[] { "*.bza", "*.gza" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				uint unknown1 = reader.ReadUInt32();
				string compressionType = reader.ReadFixedLengthString(4);

				BGACompressionMethod compressionMethod = BGACompressionMethod.Bzip2;
				if (compressionType == "BZ2\0")
				{
					compressionMethod = BGACompressionMethod.Bzip2;
				}
				else if (compressionType == "GZIP")
				{
					compressionMethod = BGACompressionMethod.Gzip;
				}
				else
				{
					throw new InvalidDataFormatException("Compression type " + compressionType + " not supported!");
				}

				uint compressedSize = reader.ReadUInt32();
				uint decompressedSize = reader.ReadUInt32();
				uint checksum /*?*/ = reader.ReadUInt32();
				ushort unknown2 = reader.ReadUInt16();
				ushort unknown3 = reader.ReadUInt16();
				ushort unknown4 = reader.ReadUInt16();

				ushort fileNameLength = reader.ReadUInt16();
				string fileName = reader.ReadFixedLengthString(fileNameLength);

				long offset = reader.Accessor.Position;
				reader.Accessor.Seek(compressedSize, SeekOrigin.Current);

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressedSize", compressedSize);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.Properties.Add("CompressionMethod", compressionMethod);
				file.Properties.Add("checksum", checksum);
				file.Properties.Add("reader", reader);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			uint compressedSize = (uint)file.Properties["CompressedSize"];
			uint decompressedSize = (uint)file.Properties["DecompressedSize"];
			uint checksum = (uint)file.Properties["checksum"];
			BGACompressionMethod compressionMethod = (BGACompressionMethod)file.Properties["CompressionMethod"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedSize);
			byte[] decompressedData = compressedData;
			switch (compressionMethod)
			{
				case BGACompressionMethod.Bzip2:
				{
					decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Bzip2).Decompress(compressedData);
					break;
				}
				case BGACompressionMethod.Gzip:
				{
					decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Gzip).Decompress(compressedData);
					break;
				}
			}
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				uint unknown1 = 0;
				writer.WriteUInt32(unknown1);

				BGACompressionMethod compressionMethod = BGACompressionMethod.Bzip2;
				string compressionType = String.Empty;
				switch (compressionMethod)
				{
					case BGACompressionMethod.Bzip2:
					{
						compressionType = "BZ2";
						break;
					}
					case BGACompressionMethod.Gzip:
					{
						compressionType = "GZIP";
						break;
					}
					default:
					{
						throw new InvalidDataFormatException("Compression type " + compressionType + " not supported!");
						break;
					}
				}
				writer.WriteFixedLengthString(compressionType, 4);

				byte[] decompressedData = file.GetDataAsByteArray();
				byte[] compressedData = decompressedData;
				switch (compressionMethod)
				{
					case BGACompressionMethod.Bzip2:
					{
						compressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Bzip2).Compress(decompressedData);
						break;
					}
					case BGACompressionMethod.Gzip:
					{
						compressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Gzip).Compress(decompressedData);
						break;
					}
				}

				writer.WriteUInt32((uint)compressedData.Length);
				writer.WriteUInt32((uint)decompressedData.Length);

				uint checksum /*?*/ = 0;
				writer.WriteUInt32(checksum);

				ushort unknown2 = 0;
				writer.WriteUInt16(unknown2);
				ushort unknown3 = 0;
				writer.WriteUInt16(unknown3);
				ushort unknown4 = 0;
				writer.WriteUInt16(unknown4);

				ushort fileNameLength = (ushort)file.Name.Length;
				writer.WriteUInt16(fileNameLength);
				writer.WriteFixedLengthString(file.Name, fileNameLength);
				
				writer.WriteBytes(compressedData);
			}
		}
	}
}
