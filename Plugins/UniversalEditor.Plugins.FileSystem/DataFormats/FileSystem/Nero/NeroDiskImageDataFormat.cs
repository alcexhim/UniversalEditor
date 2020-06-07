//
//  NeroDiskImageDataFormat.cs - provides a DataFormat for manipulating file systems in Nero disk image format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

// 2013-05-12
using System;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Nero
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating file systems in Nero disk image format.
	/// </summary>
	public class NeroDiskImageDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(ImageName), "Image &name:"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(ImageName2), "Image name (&Joliet):"));
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			string header = br.ReadLengthPrefixedString();
			if (header != "NeroISO0.02.03") throw new InvalidDataFormatException("File does not begin with 0xE, \"NeroISO0.02.03\"");

			int unknown1 = br.ReadInt32();
			int unknown2 = br.ReadInt32();
			short unknown3 = br.ReadInt16();
			Creator = br.ReadLengthPrefixedString();

			byte[] unknown4 = br.ReadBytes(27);

			int unknowna1 = br.ReadInt32();
			int unknowna2 = br.ReadInt32();
			int unknowna3 = br.ReadInt32();
			int unknowna4 = br.ReadInt32();
			int unknowna5 = br.ReadInt32();
			int unknowna6 = br.ReadInt32();

			ImageName = br.ReadLengthPrefixedString();
			ImageName2 = br.ReadLengthPrefixedString();

			int unknown5 = br.ReadInt32();
			int unknown6 = br.ReadInt32();
			int unknown7 = br.ReadInt32();

			byte[] unknown8 = br.ReadBytes(51);
			System.Collections.Generic.List<Internal.DirectoryEntry> entries = new System.Collections.Generic.List<Internal.DirectoryEntry>();
			for (int i = 0; i < unknown5; i++)
			{
				Internal.DirectoryEntry entry = ReadDirectoryEntry(br);
				entries.Add(entry);
			}
		}

		private static Internal.DirectoryEntry ReadDirectoryEntry(IO.Reader br)
		{
			Internal.DirectoryEntry entry = new Internal.DirectoryEntry();
			entry.entries = new System.Collections.Generic.List<UniversalEditor.DataFormats.FileSystem.Nero.Internal.DirectoryEntry>();

			int nameLength = br.ReadInt32();
			entry.name = br.ReadFixedLengthString(nameLength);

			byte unknownb1 = br.ReadByte();
			short subDirectoryCount = br.ReadInt16();
			short unknownb3 = br.ReadInt16();

			for (short i = 0; i < subDirectoryCount; i++)
			{
				Internal.DirectoryEntry entry1 = ReadDirectoryEntry(br);
				entry.entries.Add(entry1);
			}
			return entry;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}

		public string ImageName { get; set; } = String.Empty;
		public string ImageName2 { get; set; } = String.Empty;
		public string Creator { get; set; } = String.Empty;
	}
}
