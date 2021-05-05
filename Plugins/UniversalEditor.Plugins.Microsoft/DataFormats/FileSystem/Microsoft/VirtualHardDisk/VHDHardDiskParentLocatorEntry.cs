//
//  VHDHardDiskParentLocatorEntry.cs - describes the parent locator entry for a Microsoft Virtual PC VHD virtual hard disk
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.VirtualHardDisk
{
	/// <summary>
	/// Describes the parent locator entry for a Microsoft Virtual PC VHD virtual hard disk.
	/// </summary>
	public class VHDHardDiskParentLocatorEntry
	{
		private uint mvarPlatformCode = 0;
		/// <summary>
		/// The platform code describes which platform-specific format is used for the file locator. For Windows, a file locator is stored as a path (for example. “c:\disksimages\ParentDisk.vhd”). On a Macintosh system, the file locator is a binary large object (blob) that contains an “alias.” The parent locator table is used to support moving hard disk images across platforms.
		/// None: 0x0
		/// Wi2r (deprecated): 0x57693272
		/// Wi2k (deprecated): 0x5769326B
		/// W2ru: 0x57327275 Unicode pathname (UTF-16) on Windows relative to the differencing disk pathname.
		/// W2ku: 0x57326B75 Absolute Unicode (UTF-16) pathname on Windows.
		/// Mac : 0x4D616320 (Mac OS alias stored as a blob)
		/// MacX: 0x4D616358 A file URL with UTF-8 encoding conforming to RFC 2396.
		/// </summary>
		public uint PlatformCode { get { return mvarPlatformCode; } set { mvarPlatformCode = value; } }

		private uint mvarPlatformDataSpace = 0;
		/// <summary>
		/// This field stores the number of 512-byte sectors needed to store the parent hard disk locator.
		/// </summary>
		public uint PlatformDataSpace { get { return mvarPlatformDataSpace; } set { mvarPlatformDataSpace = value; } }

		private uint mvarPlatformDataLength = 0;
		/// <summary>
		/// This field stores the actual length of the parent hard disk locator in bytes.
		/// </summary>
		public uint PlatformDataLength { get { return mvarPlatformDataLength; } set { mvarPlatformDataLength = value; } }

		private uint mvarReserved = 0;
		/// <summary>
		/// This field must be set to zero.
		/// </summary>
		public uint Reserved { get { return mvarReserved; } set { mvarReserved = value; } }

		private ulong mvarPlatformDataOffset = 0;
		/// <summary>
		/// This field stores the absolute file offset in bytes where the platform specific file locator data is stored.
		/// </summary>
		public ulong PlatformDataOffset { get { return mvarPlatformDataOffset; } set { mvarPlatformDataOffset = value; } }
	}
}
