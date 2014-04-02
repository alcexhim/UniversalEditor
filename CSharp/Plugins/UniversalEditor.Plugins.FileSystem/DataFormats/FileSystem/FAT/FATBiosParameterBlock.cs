using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	public class FATBiosParameterBlock
	{
		private short mvarBytesPerSector = 512;
		/// <summary>
		/// Bytes per sector. A common value is 512, especially for file systems on
		/// IDE (or compatible) disks. The BIOS Parameter Block starts here. Must be
		/// 2 bytes. Default is 512.
		/// </summary>
		public short BytesPerSector { get { return mvarBytesPerSector; } set { mvarBytesPerSector = value; } }

		private byte mvarSectorsPerCluster = 1;
		/// <summary>
		/// Sectors per cluster. Allowed values are powers of two from 1 to 128.
		/// However, the value must not be such that the number of bytes per cluster
		/// becomes greater than 32 KB. Must be 1 byte.
		/// </summary>
		public byte SectorsPerCluster { get { return mvarSectorsPerCluster; } set { mvarSectorsPerCluster = value; } }

		private short mvarReservedSectorCount = 32;
		/// <summary>
		/// Reserved sector count. The number of sectors before the first FAT in the
		/// file system image. Should be 1 for FAT12/FAT16. Usually 32 for FAT32.
		/// Must be 2 bytes.
		/// </summary>
		public short ReservedSectorCount { get { return mvarReservedSectorCount; } set { mvarReservedSectorCount = value; } }

		private byte mvarFileAllocationTableCount = 2;
		/// <summary>
		/// Number of file allocation tables. Almost always 2. Must be 1 byte.
		/// </summary>
		public byte FileAllocationTableCount { get { return mvarFileAllocationTableCount; } set { mvarFileAllocationTableCount = value; } }

		private short mvarMaximumRootDirectoryEntryCount = 0;
		/// <summary>
		/// Maximum number of root directory entries. Only used on FAT12 and FAT16,
		/// where the root directory is handled specially. Should be 0 for FAT32. This
		/// value should always be such that the root directory ends on a sector
		/// boundary (i.e. such that its size becomes a multiple of the sector size).
		/// 224 is typical for floppy disks. Must be 2 bytes.
		/// </summary>
		public short MaximumRootDirectoryEntryCount { get { return mvarMaximumRootDirectoryEntryCount; } set { mvarMaximumRootDirectoryEntryCount = value; } }

		private FATMediaDescriptor mvarMediaDescriptor = FATMediaDescriptor.Unknown;
		/// <summary>
		/// Media descriptor. Same value of media descriptor should be repeated as
		/// first byte of each copy of FAT. Certain operating systems (MSX-DOS
		/// version 1.0) ignore boot sector parameters altogether and use media
		/// descriptor value from the first byte of FAT to determine file system
		/// parameters. Must be 1 byte.
		/// </summary>
		public FATMediaDescriptor MediaDescriptor { get { return mvarMediaDescriptor; } set { mvarMediaDescriptor = value; } }

		private short mvarSectorsPerFAT16 = 0;
		/// <summary>
		/// Sectors per File Allocation Table for FAT12/FAT16. Must be 2 bytes.
		/// </summary>
		public short SectorsPerFAT16 { get { return mvarSectorsPerFAT16; } set { mvarSectorsPerFAT16 = value; } }

		private short mvarSectorsPerTrack = 0;
		/// <summary>
		/// Physical sectors per track for disks with INT 13h cylinder-head-sector
		/// geometry, e.g., 15 for a 1.20 MB floppy.
		/// </summary>
		/// <remarks>
		///		<para>
		///			Unused for drives, which don't support CHS access any more.
		///		</para>
		///		<para>
		///			A zero entry indicates that this entry is reserved, but not used.
		///		</para>
		///		<para>
		///			A value of 0 may indicate LBA-only access, but may cause a divide-by-zero exception in some boot
		///			loaders, which can be avoided by storing a neutral value of 1 here, if no CHS geometry can be
		///			reasonably emulated.
		///		</para>
		/// </remarks>
		public short SectorsPerTrack { get { return mvarSectorsPerTrack; } set { mvarSectorsPerTrack = value; } }

		private short mvarNumberOfHeads = 0;
		/// <summary>
		/// Number of heads for disks with INT 13h cylinder-head-sector geometry, e.g., 2 for a double sided floppy.
		/// </summary>
		/// <remarks>
		///		<para>
		///			Unused for drives, which don't support CHS access any more.
		///		</para>
		///		<para>
		///			A bug in all versions of MS-DOS/PC DOS up to including 7.10 causes these operating systems to
		///			crash for CHS geometries with 256 heads, therefore almost all BIOSes choose a maximum of 255
		///			heads only.
		///		</para>
		///		<para>
		///			A zero entry indicates that this entry is reserved, but not used.
		///		</para>
		///		<para>
		///			A value of 0 may indicate LBA-only access, but may cause a divide-by-zero exception in some boot
		///			loaders, which can be avoided by storing a neutral value of 1 here, if no CHS geometry can be
		///			reasonably emulated.
		///		</para>
		/// </remarks>
		public short NumberOfHeads { get { return mvarNumberOfHeads; } set { mvarNumberOfHeads = value; } }

		private int mvarHiddenSectorCount = 0;
		/// <summary>
		/// Count of hidden sectors preceding the partition that contains this FAT volume. This field should always
		/// be zero on media that are not partitioned. Must be 4 bytes.
		/// </summary>
		/// <remarks>
		///		<para>
		///			This DOS 3.0 entry is incompatible with a similar entry at offset 0x01C in BPBs since DOS 3.31.
		///		</para>
		///		<para>
		///			At least, it can be trusted if it holds zero, or if the logical sectors entry at offset 0x013 is
		///			zero.
		///		</para>
		///		<para>
		///			If this belongs to an Advanced Active Partition (AAP) selected at boot time, the BPB entry will
		///			be dynamically updated by the enhanced MBR to reflect the "relative sectors" value in the
		///			partition table, stored at offset 0x1B6 in the AAP or NEWLDR MBR, so that it becomes possible to
		///			boot the operating system from EBRs.
		///		</para>
		/// </remarks>
		public int HiddenSectorCount { get { return mvarHiddenSectorCount; } set { mvarHiddenSectorCount = value; } }

		private int mvarTotalSectors = 0;
		/// <summary>
		/// Total sectors (if greater than 65535; otherwise, see offset 0x13). Must be 4 bytes.
		/// </summary>
		/// <remarks>
		///		Officially, it must be used only if the logical sectors entry at offset 0x013 is zero, but some
		///		operating systems (some old versions of DR DOS) use this entry also for smaller disks.
		///	</remarks>
		public int TotalSectors { get { return mvarTotalSectors; } set { mvarTotalSectors = value; } }
	}
}
