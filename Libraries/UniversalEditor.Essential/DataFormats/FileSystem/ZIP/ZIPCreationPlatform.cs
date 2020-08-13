//
//  ZIPCreationPlatform.cs - indicates the operating system on which the ZIP file was created
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

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	/// <summary>
	/// Indicates the operating system on which the ZIP file was created.
	/// </summary>
	public enum ZIPCreationPlatform
	{
		/// <summary>
		/// MS-DOS and OS/2 (FAT / VFAT / FAT32 file systems)
		/// </summary>
		MSDOS = 0,
		Amiga = 1,
		OpenVMS = 2,
		Unix = 3,
		VMCMS = 4,
		AtariST = 5,
		/// <summary>
		/// OS/2 H.P.F.S.
		/// </summary>
		HPFS = 6,
		Macintosh = 7,
		/// <summary>
		/// Z-System
		/// </summary>
		ZSystem = 8,
		/// <summary>
		/// CP/M
		/// </summary>
		CPM = 9,
		/// <summary>
		/// Windows NTFS
		/// </summary>
		WindowsNTFS = 10,
		/// <summary>
		/// MVS (OS/390 - Z/OS)
		/// </summary>
		MVS = 11,
		VSE = 12,
		AcornRISC = 13,
		VFAT = 14,
		AlternateMVS = 15,
		BeOS = 16,
		Tandem = 17,
		/// <summary>
		/// OS/400
		/// </summary>
		OS400 = 18,
		/// <summary>
		/// Mac OS X (Darwin)
		/// </summary>
		Darwin = 19
	}
}
