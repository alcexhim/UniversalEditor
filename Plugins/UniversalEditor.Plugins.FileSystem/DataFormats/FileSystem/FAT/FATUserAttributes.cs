//
//  FATUserAttributes.cs - indicates user attributes for the FAT filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	/// <summary>
	/// Indicates user attributes for the FAT filesystem.
	/// </summary>
	[Flags()]
	public enum FATUserAttributes : byte
	{
		/// <summary>
		/// Modify default open rules (user attribute F1)
		/// </summary>
		ModifyDefaultOpenRules = 0x80,
		/// <summary>
		/// Partial close default (user attribute F2)
		/// </summary>
		PartialCloseDefault = 0x40,
		/// <summary>
		/// Ignore close checksum error (user attribute F3)
		/// </summary>
		IgnoreCloseChecksumError = 0x20,
		/// <summary>
		/// Disable checksums (user attribute F4)
		/// </summary>
		DisableChecksums = 0x10,
		Reserved = 0x08,
		DeleteRequiresPassword = 0x04,
		WriteRequiresPassword = 0x02,
		ReadRequiresPassword = 0x01
	}
}
