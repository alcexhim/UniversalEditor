using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.PVM
{
	public class PVMDataFormat : DataFormat
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

			Reader reader = base.Accessor.Reader;

			// go to the end of the file and read the central directory offset
			if (reader.Accessor.Length < 4) throw new InvalidDataFormatException("File must be larger than 4 bytes");

			reader.Seek(-4, SeekOrigin.End);
			int centralDirectoryOffset = reader.ReadInt32();

			if (centralDirectoryOffset > reader.Accessor.Length) throw new InvalidDataFormatException("Central directory offset is larger than file itself!");

			reader.Seek(centralDirectoryOffset, SeekOrigin.Begin);

			byte signatureLength = reader.ReadByte();
			if (signatureLength != 5) throw new InvalidDataFormatException("Signature expected to be 5 bytes in length");
			string signature = reader.ReadFixedLengthString(signatureLength);
			if (signature != "[PVM]") throw new InvalidDataFormatException("File does not contain the signature '[PVM]'");

			int fileCount = reader.ReadInt32();

			for (int i = 0; i < fileCount; i++)
			{
				short unknown1 = reader.ReadInt16();
				string fileName = reader.ReadFixedLengthString(13).TrimNull();

				// FIXME: these aren't offset/length, they're outside the file limits!!
				int offset = reader.ReadInt32();
				short unknown2 = reader.ReadInt16();		// 8448
				short[] unknown3 = reader.ReadInt16Array(17);

				int length = reader.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Source = new UniversalEditor.ObjectModels.FileSystem.FileSources.EmbeddedFileSource(reader, offset, length);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
