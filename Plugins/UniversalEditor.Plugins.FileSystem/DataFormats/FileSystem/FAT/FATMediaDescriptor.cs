//
//  FATMediaDescriptor.cs - indicates the type of physical media on which this FAT filesystem resides
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
	/// Indicates the type of physical media on which this FAT filesystem resides.
	/// </summary>
	public enum FATMediaDescriptor : byte
	{
		/// <summary>
		/// The value of the media descriptor is unknown to this implementation of FAT.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// 3.5" Double Sided, 80 tracks per side, 18 or 36 sectors per track (1.44MB
		/// or 2.88MB). 5.25" Double Sided, 80 tracks per side, 15 sectors per track
		/// (1.2MB). Used also for other media types.
		/// </summary>
		MediaDescriptor0 = 0xF0,
		/// <summary>
		/// Fixed disk (i.e. Hard disk).
		/// </summary>
		FixedDisk = 0xF8,
		/// <summary>
		/// 3.5" Double sided, 80 tracks per side, 9 sectors per track (720K). 5.25" Double sided, 80 tracks per side, 15 sectors per track (1.2MB)
		/// </summary>
		MediaDescriptor2 = 0xF9,
		/// <summary>
		/// 5.25" Single sided, 80 tracks per side, 8 sectors per track (320K)
		/// </summary>
		MediaDescriptor3 = 0xFA,
		/// <summary>
		/// 3.5" Double sided, 80 tracks per side, 8 sectors per track (640K)
		/// </summary>
		MediaDescriptor4 = 0xFB,
		/// <summary>
		/// 5.25" Single sided, 40 tracks per side, 9 sectors per track (180K)
		/// </summary>
		MediaDescriptor5 = 0xFC,
		/// <summary>
		/// 5.25" Double sided, 40 tracks per side, 9 sectors per track (360K). Also used for 8".
		/// </summary>
		MediaDescriptor6 = 0xFD,
		/// <summary>
		/// 5.25" Single sided, 40 tracks per side, 8 sectors per track (160K). Also used for 8".
		/// </summary>
		MediaDescriptor7 = 0xFE,
		/// <summary>
		/// 5.25" Double sided, 40 tracks per side, 8 sectors per track (320K)
		/// </summary>
		MediaDescriptor8 = 0xFF
	}
}
