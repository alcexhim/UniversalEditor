using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NScripter.NSA
{
	public class NSADataFormat : DataFormat
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
			reader.Endianness = Endianness.BigEndian;

			ushort fileCount = reader.ReadUInt16();
			ushort unknown1 = reader.ReadUInt16();

			byte unknown2 = reader.ReadByte();

			for (ushort i = 0; i < fileCount; i++)
			{
				byte unknown3 = reader.ReadByte();
				string fileName = reader.ReadNullTerminatedString();

				ushort unknown4 = reader.ReadUInt16();
				ushort unknown5 = reader.ReadUInt16();

				uint unknown6 = reader.ReadUInt32();
				uint unknown7 = reader.ReadUInt32();

				File file = fsom.AddFile(fileName);
				
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
