//
//  WABDataFormat.cs - provides a DataFormat for manipulating address books in Microsoft Windows binary address book format
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
using UniversalEditor.ObjectModels.AddressBook;

namespace UniversalEditor.DataFormats.AddressBook
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating address books in Microsoft Windows binary address book format.
	/// </summary>
	public class WABDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			AddressBookObjectModel ab = (objectModel as AddressBookObjectModel);
			if (ab == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			Guid guid = br.ReadGuid();
			int unknown1 = br.ReadInt32();      // 8
			int unknown2 = br.ReadInt32();      // 4
			int unknown3 = br.ReadInt32();      // 4000
			int unknown4 = br.ReadInt32();      // 16
			int unknown5 = br.ReadInt32();      // 2212

			int objectCount = br.ReadInt32();
			int sectorSize = br.ReadInt32();    // 34000
			int unknown7 = br.ReadInt32();      // 136
			int objectOffset = br.ReadInt32();  // 6212

			int objectCountAgain = br.ReadInt32();

			// no clue what all this junk is...


			br.Accessor.Position = objectOffset;
			for (int i = 0; i < objectCount; i++)
			{
				string objectName = br.ReadNullTerminatedString(Encoding.UTF16LittleEndian);
				short unknown8 = br.ReadInt16();
				int unknown9 = br.ReadInt32();
				int unknown10 = br.ReadInt32();
				int unknown11 = br.ReadInt32();
				int unknown12 = br.ReadInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(AddressBookObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
	}
}
