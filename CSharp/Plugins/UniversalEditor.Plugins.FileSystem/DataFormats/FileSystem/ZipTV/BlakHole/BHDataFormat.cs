using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ZipTV.BlakHole
{
	public class BHDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("ZipTV BlakHole archive", new byte?[][] { new byte?[] { (byte)'B', (byte)'H' } }, new string[] { "*.bh" });
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
				string signature = reader.ReadFixedLengthString(2);
				if (signature != "BH")
				{
					reader.Seek(-1, SeekOrigin.Current);
					continue;
				}

															//	uncompressed	deflate		fuse	bzip2-ultra	bzip2-normal
				byte unknown1 = reader.ReadByte();			//	5
				byte unknown2 = reader.ReadByte();			//	7
				ushort unknown3 = reader.ReadUInt16();		// 49
				ushort unknown4 = reader.ReadUInt16();		// 55845
				ushort windowSize = reader.ReadUInt16();		// 4				516			4		516			4
				BHCompressionMethod compressionMethod = (BHCompressionMethod)reader.ReadByte();			// 0				8			3		12			12

				// TODO: figure out how dates are encoded
				uint modifiedDate = reader.ReadUInt32();

				uint compressedSize /* or compressed size */ = reader.ReadUInt32();
				uint decompressedSize /* or decompressed size */ = reader.ReadUInt32();
				
				uint crc = reader.ReadUInt32();
				
				uint unknown8 = reader.ReadUInt32();		// 32
				ushort unknown9 = reader.ReadUInt16();

				uint fileNameLength = reader.ReadUInt32();
				string fileName = reader.ReadFixedLengthString(fileNameLength);

				long offset = base.Accessor.Position;
				reader.Seek(compressedSize, SeekOrigin.Current);

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressionMethod", compressionMethod);
				file.Properties.Add("CompressedSize", compressedSize);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.DataRequest += file_DataRequest;
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			Writer writer = base.Accessor.Writer;
			foreach (File file in files)
			{
				writer.WriteFixedLengthString("BH");

				writer.WriteByte(5); // unknown, version major?
				writer.WriteByte(7); // unknown, version minor?
				writer.WriteUInt16(49); // unknown
				writer.WriteUInt16(55845); // unknown

				ushort windowSize = 4;
				BHCompressionMethod compressionMethod = BHCompressionMethod.Bzip2;
				if (compressionMethod == BHCompressionMethod.Deflate || compressionMethod == BHCompressionMethod.Bzip2)
				{
					windowSize = 516;
				}

				writer.WriteUInt16(windowSize);
				writer.WriteByte((byte)compressionMethod);

				uint modifiedDate = 0;
				writer.WriteUInt32(modifiedDate);

				byte[] decompressedData = file.GetDataAsByteArray();
				byte[] compressedData = null;
				switch (compressionMethod)
				{
					case BHCompressionMethod.Bzip2:
					{
						compressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Bzip2).Compress(decompressedData);
						break;
					}
					case BHCompressionMethod.Deflate:
					{
						compressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Deflate).Compress(decompressedData);
						break;
					}
					case BHCompressionMethod.Fuse:
					{
						throw new NotImplementedException("TODO: implement Fuse compression method for BlakHole");
						break;
					}
					default:
					{
						throw new NotSupportedException("Unknown or unsupported compression method " + ((byte)compressionMethod).ToString());
					}
				}

				writer.WriteUInt32((uint)compressedData.Length);
				writer.WriteUInt32((uint)decompressedData.Length);

				Checksum.Modules.CRC32.CRC32ChecksumModule chksum = new Checksum.Modules.CRC32.CRC32ChecksumModule();
				uint crc = (uint)chksum.Calculate(decompressedData);
				writer.WriteUInt32(crc);

				uint unknown8 = 12;
				writer.WriteUInt32(unknown8);

				ushort unknown9 = 0;
				writer.WriteUInt16(unknown9);

				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteFixedLengthString(file.Name);

				writer.WriteBytes(compressedData);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			BHCompressionMethod compressionMethod = (BHCompressionMethod)file.Properties["CompressionMethod"];
			uint compressedSize = (uint)file.Properties["CompressedSize"];
			uint decompressedSize = (uint)file.Properties["DecompressedSize"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedSize);
			byte[] decompressedData = null;
			switch (compressionMethod)
			{
				case BHCompressionMethod.None:
				{
					decompressedData = compressedData;
					break;
				}
				case BHCompressionMethod.Bzip2:
				{
					decompressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Bzip2).Decompress(compressedData);
					break;
				}
				case BHCompressionMethod.Deflate:
				{
					decompressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Deflate).Decompress(compressedData);
					break;
				}
				case BHCompressionMethod.Fuse:
				{
					throw new NotImplementedException("TODO: implement Fuse compression method for BlakHole");
					break;
				}
				default:
				{
					throw new NotSupportedException("Unknown or unsupported compression method " + ((byte)compressionMethod).ToString());
				}
			}

			e.Data = decompressedData;
		}
	}
}
