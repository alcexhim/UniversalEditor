//
//  LNKDataFlags.cs - indicates attributes for an LNK shortcut file
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

namespace UniversalEditor.DataFormats.Shortcut.Microsoft
{
	/// <summary>
	/// Indicates attributes for an LNK shortcut file.
	/// </summary>
	[Flags()]
	public enum LNKDataFlags
	{
		None = 0x00000000,
		/// <summary>
		/// The LNK file contains a link target identifier.
		/// </summary>
		HasTargetIDList = 0x00000001,
		/// <summary>
		/// The LNK file contains location information.
		/// </summary>
		HasLinkInfo = 0x00000002,
		/// <summary>
		/// The LNK file contains a description data string.
		/// </summary>
		HasName = 0x00000004,
		/// <summary>
		/// The LNK file contains a relative path data string.
		/// </summary>
		HasRelativePath = 0x00000008,
		/// <summary>
		/// The LNK file contains a working directory data string.
		/// </summary>
		HasWorkingDir = 0x00000010,
		/// <summary>
		/// The LNK file contains a command line arguments data string.
		/// </summary>
		HasArguments = 0x00000020,
		/// <summary>
		/// The LNK file contains a custom icon location.
		/// </summary>
		HasIconLocation = 0x00000040,
		/// <summary>
		/// The data strings in the LNK file are stored in Unicode (UTF-16 little-endian) instead of ASCII.
		/// </summary>
		IsUnicode = 0x00000080,
		/// <summary>
		/// The location information is ignored.
		/// </summary>
		ForceNoLinkInfo = 0x00000100,
		/// <summary>
		/// The LNK file contains environment variables that should be expanded.
		/// </summary>
		HasExpString = 0x00000200,
		/// <summary>
		/// A 16-bit target application is run in a separate virtual machine.
		/// </summary>
		RunInSeparateProcess = 0x00000400,
		// Reserved = 0x00000800,
		/// <summary>
		/// The LNK file contains a Darwin (Mac OS-X) properties data block.
		/// </summary>
		HasDarwinID = 0x00001000,
		/// <summary>
		/// The target application is run as a different user.
		/// </summary>
		RunAsUser = 0x00002000,
		/// <summary>
		/// The LNK file contains an icon location data block.
		/// </summary>
		HasExpIcon = 0x00004000,
		/// <summary>
		/// The file system location is represented in the shell namespace when the path to an
		/// item is parsed into the link target identifiers.
		/// Contains a known folder location data block ?
		/// </summary>
		NoPidlAlias = 0x00008000,
		// Reserved = 0x00010000,
		// Windows Vista and later?
		/// <summary>
		/// The target application is run with the shim layer. The LNK file contains shim layer
		/// properties data block.
		/// </summary>
		RunWithShimLayer = 0x00020000,
		/// <summary>
		/// The LNK does not contain a distributed link tracking data block. Note that LNK files
		/// in Windows XP and earlier do not use the ForceNoLinkTrack flag.
		/// </summary>
		ForceNoLinkTrack = 0x00040000,
		/// <summary>
		/// The LNK file contains a metadata property store data block.
		/// </summary>
		EnableTargetMetadata = 0x00080000,
		/// <summary>
		/// The environment variables location block should be ignored.
		/// </summary>
		DisableLinkPathTracking = 0x00100000,
		DisableKnownFolderTracking = 0x00200000,
		DisableKnownFolderAlias = 0x00400000,
		AllowLinkToLink = 0x00800000,
		UnaliasOnSave = 0x01000000,
		PreferEnvironmentPath = 0x02000000,
		KeepLocalIDListForUNCTarget = 0x04000000
	}
}
