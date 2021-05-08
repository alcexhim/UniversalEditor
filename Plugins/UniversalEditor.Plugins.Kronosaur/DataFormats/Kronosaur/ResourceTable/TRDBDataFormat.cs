//
//  TRDBDataFormat.cs - provides a DataFormat to manipulate Kronosaur TRDB resource tables
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Kronosaur.ResourceTable;

namespace UniversalEditor.DataFormats.Kronosaur.ResourceTable
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Kronosaur TRDB resource tables.
	/// </summary>
	public class TRDBDataFormat  : DataFormat
	{
		private static DataFormatReference _dfr;
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
