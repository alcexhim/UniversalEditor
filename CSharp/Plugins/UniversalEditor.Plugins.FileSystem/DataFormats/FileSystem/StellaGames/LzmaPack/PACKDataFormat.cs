using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.StellaGames.LzmaPack
{
	public class PACKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Stella Games LzmaPack", new string[] { "*.pack" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			byte lzmaDecoderPropertiesSize = reader.ReadByte();
			byte[] lzmaDecoderProperties = reader.ReadBytes(lzmaDecoderPropertiesSize);
			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadLengthPrefixedString();
				long decompressedSize = reader.ReadInt64();
				long compressedSize = reader.ReadInt64();
				long offset = reader.ReadInt64();
				PACKFileAttributes fileAttributes = (PACKFileAttributes)reader.ReadUInt16();

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressedSize", compressedSize);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			long CompressedSize = (long)file.Properties["CompressedSize"];
			long DecompressedSize = (long)file.Properties["DecompressedSize"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(CompressedSize);
			byte[] decompressedData = compressedData;
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();

			byte lzmaDecoderPropertiesSize = 0;
			byte[] lzmaDecoderProperties = new byte[lzmaDecoderPropertiesSize];
			writer.WriteByte(lzmaDecoderPropertiesSize);
			writer.WriteBytes(lzmaDecoderProperties);

			long offset = 1 + lzmaDecoderPropertiesSize + 4;
			foreach (File file in files)
			{
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
				bw.Write(file.Name);
				bw.Close();
				offset += ms.ToArray().Length;
				offset += 26;
			}

			writer.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				writer.WriteLengthPrefixedString(file.Name);
				byte[] decompressedData = file.GetDataAsByteArray();
				byte[] compressedData = decompressedData;

				writer.WriteInt64((long)decompressedData.Length);
				writer.WriteInt64((long)compressedData.Length);
				writer.WriteInt64(offset);

				PACKFileAttributes fileAttributes = PACKFileAttributes.None;
				writer.WriteUInt16((ushort)fileAttributes);

				offset += compressedData.Length;
			}
		}
	}
}
