using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Kronosaur.ResourceTable;

namespace UniversalEditor.DataFormats.Kronosaur.ResourceTable
{
	public class TRDBDataFormat  : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ResourceTableObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("https://github.com/kronosaur/Mammoth/blob/2da0caf7195e20b9abc355fda0438f37acc6057c/TSE/CResourceDb.cpp");
			}
			return _dfr;
		}

		private uint mvarFormatVersion = 12;
		public uint FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private int mvarGameFileEntryID = 0;
		public int GameFileEntryID { get { return mvarGameFileEntryID; } set { mvarGameFileEntryID = value; } }

		private string mvarGameTitle = String.Empty;
		public string GameTitle { get { return mvarGameTitle; } set { mvarGameTitle = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ResourceTableObjectModel rtom = (objectModel as ResourceTableObjectModel);
			if (rtom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "BDRT") throw new InvalidDataFormatException("File does not begin with 'BDRT'");

			mvarFormatVersion = reader.ReadUInt32();
			mvarGameFileEntryID = reader.ReadInt32();
			mvarGameTitle = ReadCString(reader);

			if (mvarFormatVersion >= 12)
			{
				int iCount = reader.ReadInt32();
				for (int i = 0; i < iCount; i++)
				{
		 			string fileName = ReadCString(reader);
					int entryID = reader.ReadInt32();
					ResourceTableEntryFlags dwFlags = (ResourceTableEntryFlags)reader.ReadInt32();

					rtom.Entries.Add(fileName, entryID, dwFlags);
				}
			}
		}

		public static string ReadCString(Reader reader)
		{
			uint length = reader.ReadUInt32();
			string value = reader.ReadFixedLengthString(length);

			// for some reason all CStrings are aligned to 4 bytes
			reader.Align(4);
			return value;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ResourceTableObjectModel rtom = (objectModel as ResourceTableObjectModel);
			if (rtom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("BDRT");
			writer.WriteUInt32(mvarFormatVersion);
			writer.WriteInt32(mvarGameFileEntryID);

		}
	}
}
