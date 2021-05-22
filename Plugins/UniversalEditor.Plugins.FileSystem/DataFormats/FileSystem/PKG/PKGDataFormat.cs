//
//  PKGDataFormat.cs - provides a DataFormat for manipulating archives in PKG format
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
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.PKG
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in PKG format.
	/// </summary>
	public class PKGDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null) _dfr = base.MakeReferenceInternal();
			_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameName), "Game _name"));
			return _dfr;
		}

		public string GameName { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			br.Endianness = IO.Endianness.BigEndian;
			byte magic1 = br.ReadByte();
			string magic2 = br.ReadFixedLengthString(3); // PKG
			if (magic1 != 0x7F || magic2 != "PKG") throw new InvalidDataFormatException();

			int magic3 = br.ReadInt32();            // unknown, Possible constant of 0x80000001
			int headerSize = br.ReadInt32();        // Size of header (0x00 - 0xBF)
			int unknown1 = br.ReadInt32();          // unknown
			int endBlockSize = br.ReadInt32();      // size of Unknown block at end of data starting @ 0x100
			int unknown2 = br.ReadInt32();          // unknown
			long fileSize = br.ReadInt64();         // Size of file
			long unknown3 = br.ReadInt64();
			long dataSize = br.ReadInt64();         // Size of data @ 0x100 minus 0x80 byte Unknown block.

			string gameID = br.ReadFixedLengthString(48);
			GameName = gameID.TrimNull();


		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
