using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TroikaGames.Arcanum
{
	public class DATDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
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

			// zlib compression
			
			// seek to end of archive and read TS
			br.Accessor.Seek(-4, SeekOrigin.End);
			
			uint fileTableSize = br.ReadUInt32();

			// seek to TS and read FSP
			uint AS = (uint)(br.Accessor.Length - 4 - fileTableSize);
			br.Accessor.Seek(AS, SeekOrigin.Begin);

			uint fileTableOffset = br.ReadUInt32();

			// seek to FSP and read FA
			br.Accessor.Seek(fileTableOffset, SeekOrigin.Begin);
			uint fileCount = br.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				uint fileNameSize = br.ReadUInt32();
				string fileName = String.Empty;

				try
				{
					fileName = br.ReadFixedLengthString(fileNameSize);
				}
				catch (OverflowException ex)
				{
					throw new InvalidDataFormatException();
				}
				fileName = fileName.TrimNull();

				uint unknown1 = br.ReadUInt32();
				uint unknown2 = br.ReadUInt32();

				uint decompressedSize = br.ReadUInt32();
				uint compressedSize = br.ReadUInt32();
				uint offset = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("reader", br);
				file.Properties.Add("DecompressedLength", decompressedSize);
				file.Properties.Add("CompressedLength", compressedSize);
				file.Properties.Add("offset", offset);
				file.Size = decompressedSize;
				file.DataRequest += file_DataRequest;
			}
		}

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint DecompressedLength = (uint)file.Properties["DecompressedLength"];
			uint CompressedLength = (uint)file.Properties["CompressedLength"];
			uint offset = (uint)file.Properties["offset"];

			br.Accessor.Position = offset;
			byte[] compressedData = br.ReadBytes(CompressedLength);
			byte[] decompressedData = Compression.CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib).Decompress(compressedData);
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			File[] files = fsom.GetAllFiles();

			uint fileTableSize = 8;
			uint fileTableOffset = 4;

			byte[][] compressedDatas = new byte[files.Length][];
			for (int i = 0; i < files.Length; i++)
			{
				byte[] decompressedData = files[i].GetDataAsByteArray();
				byte[] compressedData = Compression.CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib).Compress(decompressedData);
				compressedDatas[i] = compressedData;
				writer.WriteBytes(compressedData);

				fileTableSize += (uint)(24 + files[i].Name.Length);
				fileTableOffset += (uint)compressedDatas[i].Length;
			}

			writer.WriteUInt32(fileTableOffset);
			writer.WriteUInt32((uint)files.Length);

			uint offset = 0;
			for (int i = 0; i < files.Length; i++)
			{
				File file = files[i];
				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteFixedLengthString(file.Name);

				uint unknown1 = 0;
				uint unknown2 = 0;
				writer.WriteUInt32(unknown1);
				writer.WriteUInt32(unknown2);

				byte[] decompressedData = file.GetDataAsByteArray();
				byte[] compressedData = compressedDatas[i];
				
				writer.WriteUInt32((uint)decompressedData.Length);
				writer.WriteUInt32((uint)compressedData.Length);
				writer.WriteUInt32(offset);

				offset += (uint)compressedData.Length;
			}
			writer.WriteUInt32(fileTableSize);
		}
	}
}
