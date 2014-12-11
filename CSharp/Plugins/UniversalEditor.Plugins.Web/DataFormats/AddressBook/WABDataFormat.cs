using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.AddressBook;

namespace UniversalEditor.DataFormats.AddressBook
{
	public class WABDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			AddressBookObjectModel ab = (objectModel as AddressBookObjectModel);
			if (ab == null) return;

			Reader br = base.Accessor.Reader;

			Guid guid = br.ReadGuid();
			int unknown1 = br.ReadInt32();		// 8
			int unknown2 = br.ReadInt32();		// 4
			int unknown3 = br.ReadInt32();		// 4000
			int unknown4 = br.ReadInt32();		// 16
			int unknown5 = br.ReadInt32();		// 2212
			
			int objectCount = br.ReadInt32();
			int sectorSize = br.ReadInt32();	// 34000
			int unknown7 = br.ReadInt32();		// 136
			int objectOffset = br.ReadInt32();	// 6212

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

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(AddressBookObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Outlook Express address book", new string[] { "*.wab" });
			}
			return _dfr;
		}
	}
}
