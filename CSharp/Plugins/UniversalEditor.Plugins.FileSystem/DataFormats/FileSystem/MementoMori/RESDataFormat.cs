using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.MementoMori
{
	public class RESDataFormat : DataFormat
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
			base.Accessor.DefaultEncoding = Encoding.UTF16LittleEndian;

			string signature = br.ReadFixedLengthString(82);
			if (signature != "C\0e\0n\0t\0a\0u\0r\0i\0 \0P\0r\0o\0d\0u\0c\0t\0i\0o\0n\0 \0R\0e\0s\0o\0u\0r\0c\0e\0 \0F\0i\0l\0e\0 \03\0.\01\00\0\n\0\n\0") throw new InvalidDataFormatException("File does not begin with null-terminated Unicode string \"Centauri Production Resource File 3.10\", 0x0A, 0x0A");

			uint fileCount = br.ReadUInt32();
			uint directoryOffset = br.ReadUInt32();

			br.Accessor.Seek(directoryOffset, SeekOrigin.Begin);
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadNullTerminatedString();
				byte[] fileTime = br.ReadBytes(8);
				RESCompressionType compressionType = (RESCompressionType)br.ReadUInt32();
				uint offset = br.ReadUInt32();
				uint decompressedSize = br.ReadUInt32();
				uint compressedSize = br.ReadUInt32();
				uint unknown1 = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("Reader", br);
				file.Properties.Add("CompressionType", compressionType);
				file.Properties.Add("offset", offset);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.Properties.Add("CompressedSize", compressedSize);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("Centauri Production Resource File 3.10\n\n", Encoding.UTF16LittleEndian);

			File[] files = fsom.GetAllFiles();
			bw.WriteUInt32((uint)files.Length);

			uint directoryOffset = 8;
			foreach (File file in files)
			{
				directoryOffset += (uint)file.Size;
			}
			bw.WriteUInt32(directoryOffset);

			uint fileOffset = (uint)bw.Accessor.Position;
			foreach (File file in files)
			{
				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;
				file.Properties["CompressedSize"] = (uint)compressedData.Length;
				bw.WriteBytes(compressedData);
			}

			foreach (File file in files)
			{
				bw.WriteNullTerminatedString(file.Name);
				byte[] fileTime = new byte[8];
				bw.WriteBytes(fileTime);

				RESCompressionType compressionType = RESCompressionType.None;
				bw.WriteUInt32((uint)compressionType);
				bw.WriteUInt32(fileOffset);
				bw.WriteUInt32((uint)file.Size);

				uint compressedSize = (uint)file.Properties["CompressedSize"];
				bw.WriteUInt32(compressedSize);

				uint unknown1 = 0;
				bw.WriteUInt32(unknown1);

				fileOffset += (uint)file.Size;
			}
		}
	}
}
