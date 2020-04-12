//
//  MPQBlockTableEntryFlags.cs - indicates attributes for a block table entry in a MoPaQ archive
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

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
	/// <summary>
	/// Indicates attributes for a block table entry in a MoPaQ archive.
	/// </summary>
	[Flags()]
	public enum MPQBlockTableEntryFlags : uint
	{
		/// <summary>
		/// File is compressed using PKWARE Data compression library
		/// </summary>
		Implode = 0x00000100,
		/// <summary>
		/// File is compressed using combination of compression methods
		/// </summary>
		Compressed = 0x00000200,
		/// <summary>
		/// The file is encrypted
		/// </summary>
		Encrypted = 0x00010000,
		/// <summary>
		/// The decryption key for the file is altered according to the position of the file in the archive
		/// </summary>
		DynamicKey = 0x00020000,
		/// <summary>
		/// The file contains incremental patch for an existing file in base MPQ
		/// </summary>
		Patch = 0x00100000,
		/// <summary>
		/// Instead of being divided to 0x1000-bytes blocks, the file is stored as single unit
		/// </summary>
		SingleUnit = 0x01000000,
		/// <summary>
		/// File is a deletion marker, indicating that the file no longer exists. This is used to allow patch archives
		/// to delete files present in lower-priority archives in the search chain. The file usually has length of 0 or
		/// 1 byte and its name is a hash
		/// </summary>
		Deleted = 0x02000000,
		/// <summary>
		/// File has checksums for each sector (explained in the File Data section). Ignored if file is not compressed
		/// or imploded.
		/// </summary>
		SectorCRC = 0x04000000,
		/// <summary>
		/// Set if file exists, reset when the file was deleted
		/// </summary>
		Exists = 0x80000000
	}
}
