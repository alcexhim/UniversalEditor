//
//  VHDHardDiskFooter.cs - represents the footer in a Microsoft Virtual PC VHD file
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
	/// Represents the footer in a Microsoft Virtual PC VHD file.
	/// </summary>
	public class VHDHardDiskFooter
	{
		private byte[] mvarCookie = new byte[] { (byte)'c', (byte)'o', (byte)'n', (byte)'e', (byte)'c', (byte)'t', (byte)'i', (byte)'x' };
		/// <summary>
		/// (8 bytes)
		/// Cookies are used to uniquely identify the original creator of the hard disk image. The values are case-sensitive.
		/// Microsoft uses the “conectix” string to identify this file as a hard disk image created by Microsoft Virtual Server, Virtual PC, and predecessor products. The cookie is stored as an eight-character ASCII string with the “c” in the first byte, the “o” in the second byte, and so on.
		/// </summary>
		public byte[] Cookie { get { return mvarCookie; } set { mvarCookie = value; } }

		private VHDFeatures mvarFeatures = VHDFeatures.None | VHDFeatures.Reserved;
		/// <summary>
		/// (4 bytes)
		/// This is a bit field used to indicate specific feature support. The following table displays the list of features. Any fields not listed are reserved.
		/// </summary>
		public VHDFeatures Features { get { return mvarFeatures; } set { mvarFeatures = value; } }

		private ushort mvarMajorVersion = 1;
		/// <summary>
		/// The most-significant two bytes are for the major version. This must match the file format specification. For the current specification, this field must be initialized to 0x0001.
		/// The major version will be incremented only when the file format is modified in such a way that it is no longer compatible with older versions of the file format.
		/// </summary>
		public ushort MajorVersion { get { return mvarMajorVersion; } set { mvarMajorVersion = value; } }
		private ushort mvarMinorVersion = 0;
		/// <summary>
		/// The least-significant two bytes are the minor version.  This must match the file format specification. For the current specification, this field must be initialized to 0x0000.
		/// </summary>
		public ushort MinorVersion { get { return mvarMinorVersion; } set { mvarMinorVersion = value; } }

		private long mvarDataOffset = 0xFFFFFFFF;
		/// <summary>
		/// This field holds the absolute byte offset, from the beginning of the file, to the next structure. This field is used for dynamic disks and differencing disks, but not fixed disks. For fixed disks, this field should be set to 0xFFFFFFFF.
		/// </summary>
		public long DataOffset { get { return mvarDataOffset; } set { mvarDataOffset = value; } }

		private int mvarTimestamp = 0;
		/// <summary>
		/// This field stores the creation time of a hard disk image. This is the number of seconds since January 1, 2000 12:00:00 AM in UTC/GMT.
		/// </summary>
		public int Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; } }

		private char[] mvarCreatorApplication = new char[] { 'v', 'p', 'c', '\0' };
		/// <summary>
		/// This field is used to document which application created the hard disk. The field is a left-justified text field. It uses a single-byte character set.
		/// If the hard disk is created by Microsoft Virtual PC, "vpc " is written in this field. If the hard disk image is created by Microsoft Virtual Server, then "vs  " is written in this field.
		/// Other applications should use their own unique identifiers.
		/// </summary>
		public char[] CreatorApplication { get { return mvarCreatorApplication; } set { mvarCreatorApplication = value; } }

		private ushort mvarCreatorVersionMajor = 0x0005;
		/// <summary>
		/// This field holds the major version of the application that created the hard disk image.
		/// Virtual Server 2004 sets this value to 0x0001 and Virtual PC 2004 sets this to 0x0005.
		/// </summary>
		public ushort CreatorVersionMajor { get { return mvarCreatorVersionMajor; } set { mvarCreatorVersionMajor = value; } }
		private ushort mvarCreatorVersionMinor = 0x0000;
		/// <summary>
		/// This field holds the major version of the application that created the hard disk image.
		/// Virtual Server 2004 sets this value to 0x0000 and Virtual PC 2004 sets this to 0x0000.
		/// </summary>
		public ushort CreatorVersionMinor { get { return mvarCreatorVersionMinor; } set { mvarCreatorVersionMinor = value; } }

		private int mvarCreatorHostOS = 0x4D616320;
		public int CreatorHostOS { get { return mvarCreatorHostOS; } set { mvarCreatorHostOS = value; } }

		private long mvarOriginalSize = 0;
		/// <summary>
		/// This field stores the size of the hard disk in bytes, from the perspective of the virtual machine, at creation time. This field is for informational purposes.
		/// </summary>
		public long OriginalSize { get { return mvarOriginalSize; } set { mvarOriginalSize = value; } }
		private long mvarCurrentSize = 0;
		/// <summary>
		/// This field stores the current size of the hard disk, in bytes, from the perspective of the virtual machine.
		/// This value is same as the original size when the hard disk is created. This value can change depending on whether the hard disk is expanded.
		/// </summary>
		public long CurrentSize { get { return mvarCurrentSize; } set { mvarCurrentSize = value; } }

		private VHDHardDiskGeometry mvarDiskGeometry = new VHDHardDiskGeometry();
		/// <summary>
		/// This field stores the cylinder, heads, and sectors per track value for the hard disk.
		/// When a hard disk is configured as an ATA hard disk, the CHS values (that is, Cylinder, Heads, Sectors per track) are used by the ATA controller to determine the size of the disk. When the user creates a hard disk of a certain size, the size of the hard disk image in the virtual machine is smaller than that created by the user. This is because CHS value calculated from the hard disk size is rounded down. The pseudo-code for the algorithm used to determine the CHS values can be found in the appendix of this document.
		/// </summary>
		public VHDHardDiskGeometry DiskGeometry { get { return mvarDiskGeometry; } }

		private VHDHardDiskType mvarDiskType = VHDHardDiskType.None;
		public VHDHardDiskType DiskType { get { return mvarDiskType; } set { mvarDiskType = value; } }

		private int mvarChecksum = 0;
		/// <summary>
		/// This field holds a basic checksum of the hard disk footer. It is just a one’s complement of the sum of all the bytes in the footer without the checksum field.
		/// If the checksum verification fails, the Virtual PC and Virtual Server products will instead use the header. If the checksum in the header also fails, the file should be assumed to be corrupt.
		/// </summary>
		public int Checksum { get { return mvarChecksum; } }

		private byte[] mvarUniqueID = new byte[32];
		/// <summary>
		/// Every hard disk has a unique ID stored in the hard disk. This is used to identify the hard disk. This is a 128-bit universally unique identifier (UUID). This field is used to associate a parent hard disk image with its differencing hard disk image(s).
		/// </summary>
		public byte[] UniqueID { get { return mvarUniqueID; } set { mvarUniqueID = value; } }

		private bool mvarSavedState = false;
		/// <summary>
		/// This field holds a one-byte flag that describes whether the system is in saved state. If the hard disk is in the saved state the value is set to 1.  Operations such as compaction and expansion cannot be performed on a hard disk in a saved state.
		/// </summary>
		public bool SavedState { get { return mvarSavedState; } set { mvarSavedState = value; } }

		private byte[] mvarReserved = new byte[427];
		/// <summary>
		/// This field contains zeroes. It is 427 bytes in size.
		/// </summary>
		public byte[] Reserved { get { return mvarReserved; } set { mvarReserved = value; } }
	}
}
