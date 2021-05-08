//
//  VHDHardDiskDynamicHeader.cs - represents the header in a dynamic virtual hard disk
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
	/// Description of VHDHardDiskDynamicHeader.
	/// </summary>
	public class VHDHardDiskDynamicHeader
	{
		private byte[] mvarCookie = new byte[] { (byte)'c', (byte)'x', (byte)'s', (byte)'p', (byte)'a', (byte)'r', (byte)'s', (byte)'e' };
		/// <summary>
		/// This field holds the value "cxsparse". This field identifies the header.
		/// </summary>
		public byte[] Cookie { get { return mvarCookie; } set { mvarCookie = value; } }

		private long mvarDataOffset = 0xFFFFFFFF;
		/// <summary>
		/// This field contains the absolute byte offset to the next structure in the hard disk image. It is currently unused by existing formats and should be set to 0xFFFFFFFF.
		/// </summary>
		public long DataOffset { get { return mvarDataOffset; } set { mvarDataOffset = value; } }

		private long mvarTableOffset = 0;
		/// <summary>
		/// This field stores the absolute byte offset of the Block Allocation Table (BAT) in the file.
		/// </summary>
		public long TableOffset { get { return mvarTableOffset; } set { mvarTableOffset = value; } }

		private ushort mvarHeaderVersionMajor = 0x0001;
		/// <summary>
		/// This field stores the version of the dynamic disk header. The most-significant two bytes represent the major version. This must match with the file format specification. For this specification, this field must be initialized to 0x0001.
		/// The major version will be incremented only when the header format is modified in such a way that it is no longer compatible with older versions of the product.
		/// </summary>
		public ushort HeaderVersionMajor { get { return mvarHeaderVersionMajor; } set { mvarHeaderVersionMajor = value; } }

		private ushort mvarHeaderVersionMinor = 0x0000;
		/// <summary>
		/// This field stores the version of the dynamic disk header. The least-significant two bytes represent the minor version. This must match with the file format specification. For this specification, this field must be initialized to 0x0000.
		/// </summary>
		public ushort HeaderVersionMinor { get { return mvarHeaderVersionMinor; } set { mvarHeaderVersionMinor = value; } }

		private uint mvarMaxTableEntries = 0;
		/// <summary>
		/// This field holds the maximum entries present in the BAT. This should be equal to the number of blocks in the disk (that is, the disk size divided by the block size).
		/// </summary>
		public uint MaxTableEntries { get { return mvarMaxTableEntries; } set { mvarMaxTableEntries = value; } }

		private uint mvarBlockSize = 0x00200000;
		/// <summary>
		/// A block is a unit of expansion for dynamic and differencing hard disks. It is stored in bytes. This size does not include the size of the block bitmap. It is only the size of the data section of the block. The sectors per block must always be a power of two. The default value is 0x00200000 (indicating a block size of 2 MB).
		/// </summary>
		public uint BlockSize { get { return mvarBlockSize; } set { mvarBlockSize = value; } }

		private uint mvarChecksum = 0;
		/// <summary>
		/// This field holds a basic checksum of the dynamic header. It is a one’s complement of the sum of all the bytes in the header without the checksum field.
		/// If the checksum verification fails the file should be assumed to be corrupt.
		/// </summary>
		public uint Checksum { get { return mvarChecksum; } }

		private byte[] mvarParentUniqueID = new byte[16];
		/// <summary>
		/// This field is used for differencing hard disks. A differencing hard disk stores a 128-bit UUID of the parent hard disk. For more information, see “Creating Differencing Hard Disk Images” later in this paper.
		/// </summary>
		public byte[] ParentUniqueID { get { return mvarParentUniqueID; } set { mvarParentUniqueID = value; } }

		private uint mvarParentTimestamp = 0;
		/// <summary>
		/// This field stores the modification time stamp of the parent hard disk. This is the number of seconds since January 1, 2000 12:00:00 AM in UTC/GMT.
		/// </summary>
		public uint ParentTimestamp { get { return mvarParentTimestamp; } set { mvarParentTimestamp = value; } }

		private uint mvarReserved1 = 0;
		/// <summary>
		/// This field should be set to zero.
		/// </summary>
		public uint Reserved1 { get { return mvarReserved1; } set { mvarReserved1 = value; } }

		private byte[] mvarParentUnicodeName = new byte[512];
		/// <summary>
		/// This field contains a Unicode string (UTF-16) of the parent hard disk filename.
		/// </summary>
		public byte[] ParentUnicodeName { get { return mvarParentUnicodeName; } set { mvarParentUnicodeName = value; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry1 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry1 { get { return mvarParentLocatorEntry1; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry2 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry2 { get { return mvarParentLocatorEntry2; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry3 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry3 { get { return mvarParentLocatorEntry3; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry4 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry4 { get { return mvarParentLocatorEntry4; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry5 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry5 { get { return mvarParentLocatorEntry5; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry6 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry6 { get { return mvarParentLocatorEntry6; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry7 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry7 { get { return mvarParentLocatorEntry7; } }

		private VHDHardDiskParentLocatorEntry mvarParentLocatorEntry8 = new VHDHardDiskParentLocatorEntry();
		/// <summary>
		/// These entries store an absolute byte offset in the file where the parent locator for a differencing hard disk is stored. This field is used only for differencing disks and should be set to zero for dynamic disks.
		/// </summary>
		public VHDHardDiskParentLocatorEntry ParentLocatorEntry8 { get { return mvarParentLocatorEntry8; } }

		private byte[] mvarReserved2 = new byte[256];
		/// <summary>
		/// This must be initialized to zeroes.
		/// </summary>
		public byte[] Reserved2 { get { return mvarReserved2; } set { mvarReserved2 = value; } }
	}
}
