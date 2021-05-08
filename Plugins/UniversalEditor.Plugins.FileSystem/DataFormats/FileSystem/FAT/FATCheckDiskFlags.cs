//
//  FATCheckDiskFlags.cs - indicates flags written by CHKDSK to a FAT filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	/// <summary>
	/// Indicates flags written by CHKDSK to a FAT filesystem. Bits 7-2 always cleared.
	/// </summary>
	public enum FATCheckDiskFlags
	{
		None = 0x00,
		/// <summary>
		/// Volume is "dirty" and was not properly unmounted before shutdown, run CHKDSK on next boot.
		/// </summary>
		Dirty = 0x01,
		/// <summary>
		/// Disk I/O errors encountered, possible bad sectors, run surface scan on next boot.
		/// </summary>
		Error = 0x02
	}
}
