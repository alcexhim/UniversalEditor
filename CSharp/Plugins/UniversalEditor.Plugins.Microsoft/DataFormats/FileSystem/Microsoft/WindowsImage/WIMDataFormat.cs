using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public class WIMDataFormat : DataFormat
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
			string magic = reader.ReadFixedLengthString(8);
			if (magic != "MSWIM\0\0\0") throw new InvalidDataFormatException("File does not begin with 'MSWIM', 0x00, 0x00, 0x00");

			uint offsetToFirstDataBlock = reader.ReadUInt32();

			uint unknown2 = reader.ReadUInt32();
			uint unknown3 = reader.ReadUInt32();
			uint unknown4 = reader.ReadUInt32();

			Guid guid = reader.ReadGuid();
			ushort unknown5a = reader.ReadUInt16();
			ushort unknown5b = reader.ReadUInt16();
			uint unknown6 = reader.ReadUInt32();
			uint unknown7 = reader.ReadUInt32();
			uint unknown8 = reader.ReadUInt32();
			uint unknown9 = reader.ReadUInt32();
			uint unknown10 = reader.ReadUInt32();
			uint unknown11 = reader.ReadUInt32();
			uint unknown12 = reader.ReadUInt32();
			uint unknown13 = reader.ReadUInt32();
			uint unknown14 = reader.ReadUInt32();

			ulong xmlDataOffset = reader.ReadUInt64();
			ulong xmlDataLength = reader.ReadUInt64();



			// file record
			{
				// 20 byte guid???
				Guid fileGuid = reader.ReadGuid();
				uint fileGuid2 = reader.ReadUInt32();

				ulong unknownB1 = reader.ReadUInt64();
				ulong unknownB2 = reader.ReadUInt64();
				ushort fileNameLength = reader.ReadUInt16();
				string fileName = reader.ReadFixedLengthString(fileNameLength, Encoding.UTF16LittleEndian);
			}

			// file data mapping record
			{
				uint unknownZ1 = reader.ReadUInt32();

				ulong fileLength1 = reader.ReadUInt64();		// maybe compressed/uncompressed length?
				ulong fileOffset = reader.ReadUInt64();
				ulong fileLength = reader.ReadUInt64();

				Guid fileGuid = reader.ReadGuid();
				uint fileGuid2 = reader.ReadUInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("MSWIM", 8);

			throw new NotImplementedException();
		}
	}
}
