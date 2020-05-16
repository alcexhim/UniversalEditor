//
//  ADFDiskImageFlags.cs - indicates attributes for an ADF disk image file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.Plugins.Amiga.DataFormats.FileSystem.ADF
{
	/// <summary>
	/// Indicates attributes for an ADF disk image file.
	/// </summary>
	[Flags()]
	public enum ADFDiskImageFlags
	{
		/// <summary>
		/// Indicates the disk image uses the Fast File System (AmigaDOS 2.04), as opposed to the Original File System (AmigaDOS 1.2).
		/// </summary>
		FFS = 0x01,
		/// <summary>
		/// Indicates the disk image supports international characters mode only.
		/// </summary>
		InternationalOnly = 0x02,
		/// <summary>
		/// Indicates the disk image supports directory cache mode and international characters. This mode speeds up directory listing, but uses more disk space.
		/// </summary>
		DirectoryCacheModeAndInternational = 0x04
	}
}
