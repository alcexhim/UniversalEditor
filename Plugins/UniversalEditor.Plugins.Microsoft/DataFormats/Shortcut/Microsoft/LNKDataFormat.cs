//
//  LNKDataFormat.cs - provides a DataFormat to manipulate Microsoft LNK shortcut files.
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
using UniversalEditor.ObjectModels.Shortcut;

namespace UniversalEditor.DataFormats.Shortcut.Microsoft
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft LNK shortcut files.
	/// </summary>
	public class LNKDataFormat : DataFormat
	{
		private static readonly Guid LNK_CLASSID = new Guid("{00021401-0000-0000-00c0-000000000046}");

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ShortcutObjectModel), DataFormatCapabilities.All);
				_dfr.ContentTypes.Add("application/x-ms-shortcut");
				_dfr.Sources.Add("https://03132e779c908f66a75b1162132f53bf2761aa1a.googledrive.com/host/0B3fBvzttpiiSQmluVC1YeDVvZWM/Windows%20Shortcut%20File%20%28LNK%29%20format.pdf");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ShortcutObjectModel shortcut = (objectModel as ShortcutObjectModel);
			if (shortcut == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			#region File header
			uint headerSize = reader.ReadUInt32();
			if (headerSize != 0x0000004C) throw new InvalidDataFormatException("Invalid header size for shortcut file (not 0x0000004C)");

			Guid classID = reader.ReadGuid();
			if (classID.Equals(LNK_CLASSID)) throw new InvalidDataFormatException("Class ID '" + classID.ToString("B") + "' does not match LNK class ID '" + LNK_CLASSID.ToString("B") + "'");

			LNKDataFlags dataFlags = (LNKDataFlags)reader.ReadUInt32();
			LNKFileAttributeFlags fileAttributeFlags = (LNKFileAttributeFlags)reader.ReadUInt32();

			long creationFILETIME = reader.ReadInt64();
			long accessedFILETIME = reader.ReadInt64();
			long modifiedFILETIME = reader.ReadInt64();
			int filesize = reader.ReadInt32();
			int iconindex = reader.ReadInt32();
			LNKWindowState windowState = (LNKWindowState)reader.ReadInt32();
			LNKHotkey hotkey = (LNKHotkey)reader.ReadInt16();
			short reserved1 = reader.ReadInt16();
			int reserved2 = reader.ReadInt32();
			int reserved3 = reader.ReadInt32();
			#endregion
			#region Shell item identifiers list (Link target identifier)
			if ((dataFlags & LNKDataFlags.HasTargetIDList) == LNKDataFlags.HasTargetIDList)
			{
				// The size of the link target identifier shell item identifiers list
				ushort targetIDListSize = reader.ReadUInt16();
				// shell item identifiers (LIBFWSI)
			}
			#endregion
			#region Location information
			if ((dataFlags & LNKDataFlags.HasLinkInfo) == LNKDataFlags.HasLinkInfo)
			{
				uint locationInfoSize = reader.ReadUInt32();
				#region Location information header
				uint locationInfoHeaderSize = reader.ReadUInt32();
				LNKLocationFlags locationFlags = (LNKLocationFlags)reader.ReadUInt32();

				// Offset to the volume information. The offset is relative to the start of the
				// location information.
				uint volumeInformationOffset = reader.ReadUInt32();
				// Offset to the local path. The offset is relative to the start of the location
				// information.
				uint localPathOffset = reader.ReadUInt32();
				// Offset to the network share information. The offset is relative to the start of
				// the location information.
				uint networkShareInformationOffset = reader.ReadUInt32();
				// Offset to the common path. The offset is relative to the start of the location
				// information.
				uint commonPathOffset = reader.ReadUInt32();
				if (locationInfoHeaderSize > 28)
				{
					// Offset to the Unicode local path.
					uint unicodeLocalPathOffset = reader.ReadUInt32();
					if (locationInfoHeaderSize > 32)
					{
						// Offset to the Unicode common path.
						uint unicodeCommonPathOffset = reader.ReadUInt32();
					}
				}
				#endregion

				// The full filename can be determined by:
				//     • combining the local path and the common path
				//     • combining the network share name (in the network share information) with
				//       the common path

				// Note that the network share name is not necessarily terminated by a path
				// separator. Currently it is assumed that the same applies to the local path.
				// Also, the file can contain an empty common path where the local path contains
				// the full path.
			}
			#endregion
			#region Data strings
			#endregion
			#region Extra data blocks
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ShortcutObjectModel shortcut = (objectModel as ShortcutObjectModel);
			if (shortcut == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteUInt32(0x0000004C);

			throw new NotImplementedException();
		}
	}
}
