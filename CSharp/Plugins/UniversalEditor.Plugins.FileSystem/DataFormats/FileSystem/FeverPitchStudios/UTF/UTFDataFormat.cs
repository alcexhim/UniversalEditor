using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FeverPitchStudios.UTF
{
	public class UTFDataFormat : DataFormat
	{
		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);

			}
			return _dfr;
		}

		private uint mvarFormatVersion = 0;
		public uint FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "UTF\0") throw new InvalidDataFormatException("File does not begin with 'UTF', 0x00");

			mvarFormatVersion = reader.ReadUInt32();

			uint tableOffset = reader.ReadUInt32();
			uint tableLength = reader.ReadUInt32();
			uint unknown1 = reader.ReadUInt32();
			uint entryLength = reader.ReadUInt32();			// always 44?
			uint nameTableOffset = reader.ReadUInt32();		// always 56?
			uint totalNameTableLength = reader.ReadUInt32();
			uint nameTableLength = reader.ReadUInt32();
			uint fileDataOffset = reader.ReadUInt32();
			uint unknown2 = reader.ReadUInt32();
			uint unknown3 = reader.ReadUInt32();
			uint unknown4 = reader.ReadUInt32();
			uint unknown5 = reader.ReadUInt32();

			int tableEntryCount = (int)((double)tableLength / entryLength);
			
			base.Accessor.Seek(nameTableOffset, SeekOrigin.Begin);
			string[] nameTableEntries = new string[tableEntryCount];
			for (int i = 0; i < tableEntryCount; i++)
			{
				// TODO: figure out how to read name table entries (not documented in Xentax)
			}

			for (int i = 0; i < tableEntryCount; i++)
			{
				// table offset of next entry in same hierarchy level
				uint nextOffset = reader.ReadUInt32();
				// offset of entry's name in name table
				uint nameOffset = reader.ReadUInt32();

				// 16:folder else:file
				UTFFlags flags = (UTFFlags)reader.ReadUInt32();
				uint unknown6 = reader.ReadUInt32();
				// file:offset in file data, folder:offset of its first entry in table
				uint dataOffset = reader.ReadUInt32();
				uint totalSize = reader.ReadUInt32();
				uint size1 = reader.ReadUInt32();
				uint size2 = reader.ReadUInt32();
				uint unknown7 = reader.ReadUInt32();
				uint unknown8 = reader.ReadUInt32();
				uint unknown9 = reader.ReadUInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
