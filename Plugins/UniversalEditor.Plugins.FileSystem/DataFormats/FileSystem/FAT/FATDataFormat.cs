//
//  FATDataFormat.cs - provides a DataFormat for manipulating a filesystem in FAT format
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
using System.Collections.Generic;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating a filesystem in FAT format.
	/// </summary>
	public class FATDataFormat : DataFormat
	{
		#region Data Format-specific
		private byte[] mvarJumpInstruction = new byte[] { 0xEB, 0x3C, 0x90 };
		/// <summary>
		/// Jump instruction. This instruction will be executed and will skip past the rest of the
		/// (non-executable) header if the partition is booted from. See Volume Boot Record. If the jump is
		/// two-byte near jmp it is followed by a NOP instruction. Must be 3 bytes.
		/// </summary>
		public byte[] JumpInstruction { get { return mvarJumpInstruction; } set { if (value.Length != 3) { throw new ArgumentOutOfRangeException("value", value, "Jump Instruction must be exactly 3 bytes in length"); } else { mvarJumpInstruction = value; } } }

		private string mvarOEMName = String.Empty;
		/// <summary>
		/// OEM Name (padded with spaces). This value determines in which system disk was formatted. MS-DOS checks this field to determine which other parts of the boot record can be relied on.  Common values are IBM  3.3 (with two spaces between the "IBM" and the "3.3"), MSDOS5.0, MSWIN4.1 and mkdosfs. Must be 8 bytes.
		/// </summary>
		public string OEMName { get { return mvarOEMName; } set { mvarOEMName = value; } }

		private FATBiosParameterBlock mvarBiosParameterBlock = new FATBiosParameterBlock();
		public FATBiosParameterBlock BiosParameterBlock { get { return mvarBiosParameterBlock; } }

		private FATExtendedBiosParameterBlock mvarExtendedBiosParameterBlock = new FATExtendedBiosParameterBlock();
		public FATExtendedBiosParameterBlock ExtendedBiosParameterBlock { get { return mvarExtendedBiosParameterBlock; } }
		#endregion

		#region Overrides
		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(OEMName), "OEM _name", "MSDOS5.0", 8));

				#region Bios Parameter Block
				{
					CustomOptionGroup grp = new CustomOptionGroup(nameof(BiosParameterBlock), "BIOS parameter block");
					grp.Options.Add(new CustomOptionNumber("BytesPerSector", "_Bytes per sector", 512, 0, short.MaxValue));
					grp.Options.Add(new CustomOptionNumber("SectorsPerCluster", "_Sectors per cluster", 1, 1, 128));
					grp.Options.Add(new CustomOptionNumber("ReservedSectorCount", "_Reserved sectors", 32, 0, short.MaxValue));
					grp.Options.Add(new CustomOptionNumber("FileAllocationTableCount", "Number of _file allocation tables", 2, 0, byte.MaxValue));
					grp.Options.Add(new CustomOptionNumber("MaximumRootDirectoryEntryCount", "Maximum number of _root directory entries", 0, 0, short.MaxValue));
					grp.Options.Add(new CustomOptionChoice("MediaDescriptor", "Media _descriptor", true,
						new CustomOptionFieldChoice("Unknown", FATMediaDescriptor.Unknown),
						new CustomOptionFieldChoice("3.5\" double-sided, 80 tracks per side, 18 or 36 sectors per track (1.44MB or 2.88MB)", FATMediaDescriptor.MediaDescriptor0),
						new CustomOptionFieldChoice("5.25\" double-sided, 80 tracks per side, 15 sectors per track (1.2MB)", FATMediaDescriptor.MediaDescriptor0),
						new CustomOptionFieldChoice("Fixed disk (i.e., a hard disk)", FATMediaDescriptor.FixedDisk),
						new CustomOptionFieldChoice("3.5\" double-sided, 80 tracks per side, 9 sectors per track (720K)", FATMediaDescriptor.MediaDescriptor2),
						new CustomOptionFieldChoice("5.25\" double-sided, 80 tracks per side, 15 sectors per track (1.2MB)", FATMediaDescriptor.MediaDescriptor2),
						new CustomOptionFieldChoice("5.25\" single-sided, 80 tracks per side, 8 sectors per track (320K)", FATMediaDescriptor.MediaDescriptor3),
						new CustomOptionFieldChoice("3.5\" double-sided, 80 tracks per side, 8 sectors per track (640K)", FATMediaDescriptor.MediaDescriptor4),
						new CustomOptionFieldChoice("5.25\" single-sided, 40 tracks per side, 9 sectors per track (180K)", FATMediaDescriptor.MediaDescriptor5),
						new CustomOptionFieldChoice("5.25\" or 8\" double-sided, 40 tracks per side, 9 sectors per track (360K)", FATMediaDescriptor.MediaDescriptor6),
						new CustomOptionFieldChoice("5.25\" or 8\" single-sided, 40 tracks per side, 8 sectors per track (160K)", FATMediaDescriptor.MediaDescriptor7),
						new CustomOptionFieldChoice("5.25\" double-sided, 40 tracks per side, 8 sectors per track (320K)", FATMediaDescriptor.MediaDescriptor8)
					));

					grp.Options.Add(new CustomOptionNumber("SectorsPerFAT16", "Sectors per allocation table", 0));
					grp.Options.Add(new CustomOptionNumber("SectorsPerTrack", "Sectors per track", 0));
					grp.Options.Add(new CustomOptionNumber("NumberOfHeads", "Number of heads", 0));
					grp.Options.Add(new CustomOptionNumber("HiddenSectorCount", "Number of _hidden sectors", 0));
					grp.Options.Add(new CustomOptionNumber("TotalSectors", "Number of _total sectors", 0));
					_dfr.ExportOptions.Add(grp);
				}
				#endregion
				#region Extended BIOS parameter block
				{
					CustomOptionGroup grp = new CustomOptionGroup(nameof(ExtendedBiosParameterBlock), "Extended BIOS parameter block");
					grp.Options.Add(new CustomOptionNumber("PhysicalDriveNumber", "Physical drive _number", 0));
					grp.Options.Add(new CustomOptionMultipleChoice("CheckDiskFlags", "CHKDSK _flags", new CustomOptionFieldChoice("Dirty", FATCheckDiskFlags.Dirty), new CustomOptionFieldChoice("Error", FATCheckDiskFlags.Error)));
					grp.Options.Add(new CustomOptionNumber("NumberOfHeads", "Number of heads", 0));
					grp.Options.Add(new CustomOptionNumber("HiddenSectorCount", "Number of _hidden sectors", 0));
					grp.Options.Add(new CustomOptionNumber("TotalSectors", "Number of _total sectors", 0));
					#region Extended Boot Signature
					{
						CustomOptionGroup grp1 = new CustomOptionGroup("ExtendedBootSignature", "Extended Boot Signature");
						grp1.Options.Add(new CustomOptionBoolean("Enabled", "_Include extended boot signature", true));
						grp1.Options.Add(new CustomOptionNumber("VolumeSerialNumber", "Volume _serial number", 0));
						grp1.Options.Add(new CustomOptionText("PartitionVolumeLabel", "Partition _volume label", String.Empty));
						grp1.Options.Add(new CustomOptionText("FileSystemType", "File system _type", String.Empty));
						grp.Options.Add(grp1);
					}
					#endregion
					_dfr.ExportOptions.Add(grp);
				}
				#endregion
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			mvarJumpInstruction = br.ReadBytes(3);

			mvarOEMName = br.ReadFixedLengthString(8);

			#region Bios Parameter Block
			{
				mvarBiosParameterBlock.BytesPerSector = br.ReadInt16();
				mvarBiosParameterBlock.SectorsPerCluster = br.ReadByte();
				mvarBiosParameterBlock.ReservedSectorCount = br.ReadInt16();
				mvarBiosParameterBlock.FileAllocationTableCount = br.ReadByte();
				mvarBiosParameterBlock.MaximumRootDirectoryEntryCount = br.ReadInt16();
				ushort totalLogicalSectors16_1 = br.ReadUInt16();
				mvarBiosParameterBlock.MediaDescriptor = (FATMediaDescriptor)br.ReadByte();
				mvarBiosParameterBlock.SectorsPerFAT16 = br.ReadInt16();

				// The following extensions were documented since DOS 3.0, however, they were already supported by
				// some issues of DOS 2.13. MS-DOS 3.10 still supported the DOS 2.0 format, but could use the DOS
				// 3.0 format as well.
				mvarBiosParameterBlock.SectorsPerTrack = br.ReadInt16();
				mvarBiosParameterBlock.NumberOfHeads = br.ReadInt16();
				mvarBiosParameterBlock.HiddenSectorCount = br.ReadInt16();

				// Total logical sectors including hidden sectors. This DOS 3.2 entry is
				// incompatible with a similar entry at offset 0x020 in BPBs since DOS 3.31.
				// It must not be used if the logical sectors entry at offset 0x013 is zero.
				short totalLogicalSectors16_2 = br.ReadInt16();
				int totalLogicalSectors32 = br.ReadInt32();

				if (totalLogicalSectors16_1 > 0)
				{
					mvarBiosParameterBlock.TotalSectors = totalLogicalSectors16_1;
				}
				else
				{
					mvarBiosParameterBlock.TotalSectors = totalLogicalSectors32;
				}
			}
			#endregion

			long totalFileSize = (mvarBiosParameterBlock.TotalSectors * mvarBiosParameterBlock.BytesPerSector);
			long actualFileSize = br.Accessor.Length;
			if (totalFileSize != actualFileSize)
			{
			}

			/*
			 *	A simple formula translates a volume's given cluster number CN to a logical sector number LSN:
			 *
			 *	Determine (once) SSA=RSC+FN×SF+ceil((32×RDE)/SS), where the reserved sector count RSC is stored at
			 *	offset 0x00E, the FAT number FN at offset 0x010, the sectors per FAT SF at offset 0x016 (FAT12/FAT16)
			 *	or 0x024 (FAT32), the root directory entries RDE at offset 0x011, the sector size SS at offset 0x00B,
			 *	and ceil(x) rounds up to a whole number.
			 *
			 *	Determine LSN=SSA+(CN-2)×SC, where the sectors per cluster SC are stored at offset 0x00D.
			 *
			 *	On unpartitioned media the volume's number of hidden sectors is zero and therefore LSN and LBA
			 *	addresses become the same for as long as a volume's logical sector size is identical to the
			 *	underlying medium's physical sector size. Under these conditions, it is also simple to translate
			 *	between CHS addresses and LSNs as well:
			 *
			 *	LSN=SPT×(HN+(NOS×TN))+SN-1, where the sectors per track SPT are stored at offset 0x018, and the
			 *	number of sides NOS at offset 0x01A. Track number TN, head number HN, and sector number SN
			 *	correspond to Cylinder-head-sector: the formula gives the known CHS to LBA translation.
			 */

			#region Extended BIOS Parameter Block
			{
				mvarExtendedBiosParameterBlock.PhysicalDriveNumber = br.ReadByte();

				// windows NT - CHKDSK flags, bits 7-2 always cleared
				// 0x02:
				// 0x01: volume is "dirty" and was not properly unmounted before shutdown, run CHKDSK on next boot
				byte reserved1 = br.ReadByte();
				mvarExtendedBiosParameterBlock.CheckDiskFlags = (FATCheckDiskFlags)reserved1;

				// Extended boot signature. (Should be 0x29 to indicate that an EBPB with the following 3 entries
				// exists (since OS/2 1.2 and DOS 4.0). Can be 0x28 on some OS/2 1.0-1.1 and PC DOS 3.4 disks
				// indicating an earlier form of the EBPB format with only the serial number following. MS-DOS/PC
				// DOS 4.0 and higher, OS/2 1.2 and higher as well as the Windows NT family recognize both
				// signatures accordingly.)
				byte extendedBootSignature = br.ReadByte();
				mvarExtendedBiosParameterBlock.ExtendedBootSignature.Enabled = false;

				if (extendedBootSignature == 0x29 || extendedBootSignature == 0x28)
				{
					mvarExtendedBiosParameterBlock.ExtendedBootSignature.Enabled = true;
					mvarExtendedBiosParameterBlock.ExtendedBootSignature.HasPartitonLabelAndFSType = false;

					// Typically the serial number "xxxx-xxxx" is created by a 16-bit addition of both DX values
					// returned by INT 21h/AH=2Ah (get system date) and INT 21h/AH=2Ch (get system time) for the
					// high word and another 16-bit addition of both CX values for the low word of the serial
					// number. Alternatively, some DR-DOS disk utilities provide a /# option to generate a
					// human-readable time stamp "mmdd-hhmm" build from BCD-encoded 8-bit values for the month, day,
					// hour and minute instead of a serial number.
					mvarExtendedBiosParameterBlock.ExtendedBootSignature.VolumeSerialNumber = br.ReadInt32();
				}
				if (extendedBootSignature == 0x29)
				{
					mvarExtendedBiosParameterBlock.ExtendedBootSignature.HasPartitonLabelAndFSType = true;

					mvarExtendedBiosParameterBlock.ExtendedBootSignature.PartitionVolumeLabel = br.ReadFixedLengthString(11);
					mvarExtendedBiosParameterBlock.ExtendedBootSignature.FileSystemType = br.ReadFixedLengthString(8);
				}
			}
			#endregion

			// no clue why this is 448 bytes, but it just is...
			byte[] bootstrapProgram = br.ReadBytes(448);

			// The number of blocks that appear before the root directory is given by:
			int numBlocksBeforeRootDir = (mvarBiosParameterBlock.FileAllocationTableCount * mvarBiosParameterBlock.SectorsPerFAT16) + 1;
			long numBytesBeforeRootDir = (long)mvarBiosParameterBlock.BytesPerSector * numBlocksBeforeRootDir;

			int bytesOccupiedByRootDirEntries = (mvarBiosParameterBlock.MaximumRootDirectoryEntryCount * 32);

			br.Accessor.Position = numBytesBeforeRootDir;

			List<FATFileInfo> fileInfos = new List<FATFileInfo>();

			string LFN_FileName = String.Empty;

			#region File name table
			{
				bool fileIsDeleted = false;

				while (!br.EndOfStream)
				{
					byte[] fileNameBytes = br.ReadBytes(8);
					if (fileNameBytes[0] == 0x00)
					{
						// Entry is available and no subsequent entry is in use. Also serves as an
						// end marker when DOS scans a directory table. (Since MS-DOS/PC DOS 1.0,
						// but not in 86-DOS).
					}
					else if (fileNameBytes[0] == 0x05)
					{
						// Initial character is actually 0xE5. (since DOS 3.0)
						//
						// Under DR DOS 6.0 and higher, including PalmDOS, Novell DOS and OpenDOS,
						// 0x05 is also used for pending delete files under DELWATCH. Once they are
						// removed from the deletion tracking queue, the first character of an erased
						// file is replaced by 0xE5.
					}
					else if (fileNameBytes[0] == 0x2E)
					{
						// 'Dot' entry; either "." or ".." (since DOS 2.0)
					}
					else if (fileNameBytes[0] == 0xE5)
					{
						// Entry has been previously erased and is available. File undelete
						// utilities must replace this character with a regular character as part of
						// the undeletion process. See also: 0x05.
						//
						// (The reason, why 0xE5 was chosen for this purpose in 86-DOS is down to the
						// fact, that 8-inch CP/M floppies came pre-formatted with this value filled
						// and so could be used to store files out-of-the box.)
					}

					fileIsDeleted = (fileNameBytes[0] == 0xE5);

					string fileName = System.Text.Encoding.ASCII.GetString(fileNameBytes).Trim();
					if (fileName[0] == '\0') break;

					string fileExt = br.ReadFixedLengthString(3).Trim();

					byte implementationSpecificByte1 = br.ReadByte();

					// CP/M-86 and DOS Plus store user attributes F1'—F4' here. (DOS Plus 1.2 with BDOS 4.1 supports
					// passwords only on CP/M media, not on FAT12 or FAT16 media. While DOS Plus 2.1 supported
					// logical sectored FATs with a partition type 0xF2, FAT16B and FAT32 volumes were not supported
					// by these operation systems. Even if a partition would have been converted to FAT16B it would
					// still not be larger than 32 MB. Therefore, this usage is not conflictive with FAT32.IFS,
					// FAT16+ or FAT32+ as they can never occur on the same type of volume.):
					FATFileAttributes fileAttributes = (FATFileAttributes)implementationSpecificByte1;

					if (fileAttributes == FATFileAttributes.LongFileName)
					{
						br.Accessor.Position -= 12;

						byte LFN_SequenceNumber = br.ReadByte();
						byte[] LFN_UnicodeNamePart1 = br.ReadBytes(10);
						byte LFN_Attributes = br.ReadByte();
						byte LFN_Type = br.ReadByte();
						byte LFN_Checksum = br.ReadByte();
						byte[] LFN_UnicodeNamePart2 = br.ReadBytes(12);
						short LFN_FirstCluster = br.ReadInt16();
						byte[] LFN_UnicodeNamePart3 = br.ReadBytes(4);
						byte[] LFN_UnicodeNameBytes = new byte[26];

						Array.Copy(LFN_UnicodeNamePart1, 0, LFN_UnicodeNameBytes, 0, LFN_UnicodeNamePart1.Length);
						Array.Copy(LFN_UnicodeNamePart2, 0, LFN_UnicodeNameBytes, LFN_UnicodeNamePart1.Length, LFN_UnicodeNamePart2.Length);
						Array.Copy(LFN_UnicodeNamePart3, 0, LFN_UnicodeNameBytes, LFN_UnicodeNamePart1.Length + LFN_UnicodeNamePart2.Length, LFN_UnicodeNamePart3.Length);

						string LFN_UnicodeName = System.Text.Encoding.Unicode.GetString(LFN_UnicodeNameBytes);
						LFN_FileName = LFN_UnicodeName + LFN_FileName;

						if ((LFN_SequenceNumber & 0x40) == 0x40)
						{
							// This is last LFN entry
						}
						else if (LFN_SequenceNumber == 1)
						{
							// This is the first LFN entry, stop reading now.
						}
						continue;
					}

					/*
					// For a deleted file, the original first character of the filename. For the same feature in
					// various other operating systems, see offset 0x0D if enabled in MSX boot sectors at sector
					// offset 0x026. MSX-DOS supported FAT12 volumes only, but third-party extensions for FAT16
					// volumes exist. Therefore, this usage is not conflictive with FAT32.IFS and FAT32+ below. It
					// does not conflict with the usage for user attributes under CP/M-86 and DOS Plus as well,
					// since they are no longer important for deleted files.
					char deletedFileFirstCharImplementation1 = (char)implementationSpecificByte1;

					// Windows NT and later versions uses bits 3 and 4 to encode case information (see below);
					// otherwise 0.

					// DR-DOS 7.0x reserved bits other than 3 and 4 for internal purposes since 1997. The value
					// should be set to 0 by formating tools and must not be changed by disk tools.

					// On FAT32 volumes under OS/2 and eComStation the third-party FAT32.IFS driver utilizes this
					// entry as a mark byte to indicate the presence of extra " EA. SF" files holding extended
					// attributes with parameter /EAS. Version 0.70 to 0.96 used the magic values 0x00 (no EAs),
					// 0xEA (normal EAs) and 0xEC (critical EAs), whereas version 0.97 and higher since 2003-09 use
					// 0x00, 0x40 (normal EAs) and 0x80 (critical EAs) as bitflags for compatibility with Windows NT.

					// Bits other than 3 and 4 are utilized by FAT+, a proposal how to store files larger than 4 GB
					// on FAT32 (and FAT16B) volumes, currently implemented in some versions of EDR-DOS. The value
					// should be set to 0 by formating tools and must not be changed by disk tools. If some of these
					// bits are set, non-enabled implementations should refuse to open the file. To avoid problems
					// with non-aware operating systems, partitions containing files larger than 4 GB could use
					// non-standard partition IDs to hide the partition from these operating systems. Under DR-DOS,
					// partition IDs of secured partition types can be utilized for this purpose. Files larger than
					// 4 GB should have the Hidden, Read-only and System attributes set to hide them from normal
					// directory searches on non-aware operating systems, similar to password protected files under
					// DR-DOS. While FAT+ implementations do not rely on these attributes being set, for FAT+ large
					// files they may ignore these attributes in file searches and when opening large files for
					// modification and instead treat the System attribute as an alternative combined
					// Read-only+Hidden attribute in this scenario.

					// FAT32.IFS is critically conflictive with FAT32+ revision 2.

					byte implementationSpecificByte2 = br.ReadByte();
					// First character of a deleted file under Novell DOS, OpenDOS and DR-DOS 7.02 and higher. A
					// value of 0xE5 (229), as set by DELPURGE, will prohibit undeletion by UNDELETE, a value of
					// 0x00 will allow conventional undeletion asking the user for the missing first filename
					// character. S/DOS 1 and PTS-DOS 6.51 and higher also support this feature if enabled with
					// SAVENAME=ON in CONFIG.SYS. For the same feature in MSX-DOS, see offset 0x0C.
					char deletedFileFirstCharImplementation2 = (char)implementationSpecificByte2;

					// Create time, fine resolution: 10 ms units, values from 0 to 199 (since DOS 7.0 with VFAT).
					byte createTime = implementationSpecificByte2;

					// Double usage for create time ms and file char is not conflictive, since the creation time is
					// no longer important for deleted files.


					// Under DR DOS 3.31 and higher including PalmDOS, Novell DOS and OpenDOS as well
					// as under Concurrent DOS, Multiuser DOS, System Manager, and REAL/32 and
					// possibly also under FlexOS, 4680 OS, 4690 OS any non-zero value indicates the
					// password hash of a protected file, directory or volume label. The hash is
					// calculated from the first eight characters of a password. If the file
					// operation to be carried out requires a password as per the access rights
					// bitmap stored at offset 0x14, the system tries to match the hash against the
					// hash code of the currently set global password (by PASSWORD /G) or, if this
					// fails, tries to extract a semicolon-appended password from the filespec
					// passed to the operating system and checks it against the hash code stored
					// here. A set password will be preserved even if a file is deleted and later
					// undeleted.
					short implementationSpecific3 = br.ReadInt16();

					// Create time (since DOS 7.0 with VFAT). The hour, minute and second are encoded
					// according to the following bitmap:
					// Bits 	Description
					// -------- ------------------
					// 15-11 	Hours (0-23)
					// 10-5 	Minutes (0-59)
					// 4-0 		Seconds/2 (0-29)
					//
					// The seconds is recorded only to a 2 second resolution. Finer resolution for
					// file creation is found at offset 0x0D.
					//
					// If bits 15-11 > 23 or bits 10-5 > 59 or bits 4-0 > 29 here, or when bits 12-0
					// at offset 0x14 hold an access bitmap and this is not a FAT32 volume or a
					// volume using OS/2 Extended Attributes, then this entry actually holds a
					// password hash, otherwise it can be assumed to be a file creation time.



					// FlexOS, 4680 OS and 4690 OS store a record size in the word at entry 0x10.
					// This is mainly used for their special database-like file types random file,
					// direct file, keyed file, and sequential file. If the record size is set to 0
					// (default) or 1, the operating systems assume a record granularity of 1 byte
					// for the file, for which it will not perform record boundary checks in
					// read/write operations.
					//
					// With DELWATCH 2.00 and higher under Novell DOS 7, OpenDOS 7.01 and DR-DOS
					// 7.02 and higher, this entry is used to store the last modified time stamp for
					// pending delete files and directories. Cleared when file is undeleted or
					// purged. See offset 0x0E for a format description.
					//
					// Create date (since DOS 7.0 with VFAT). The year, month and day are encoded
					// according to the following bitmap:
					// Bits 	Description
					// -------- ------------------
					// 15-9 	Year (0 = 1980, 119 = 2099 supported under DOS/Windows, theoretical
					//			up to 127 = 2107)
					// 8-5 		Month (1–12)
					// 4-0 		Day (1–31)
					//
					// The usage for creation date for existing files and last modified time for
					// deleted files is not conflictive because they are never used at the same time.
					// For the same reason, the usage for the record size of existing files and last
					// modified time of deleted files is not conflictive as well. Creation dates and
					// record sizes cannot be used at the same time, however, both are stored only on
					// file creation and never changed later on, thereby limiting the conflict to
					// FlexOS, 4680 OS and 4690 OS systems accessing files created under foreign
					// operating systems as well as potential display or file sorting problems on
					// systems trying to interpret a record size as creation time. To avoid the
					// conflict, the storage of creation dates should be an optional feature of
					// operating systems supporting it.
					short implementationSpecific4 = br.ReadInt16();


					// FlexOS, 4680 OS, 4690 OS, Multiuser DOS, System Manager, REAL/32 and DR DOS
					// 6.0 and higher with multi-user security enabled use this field to store owner
					// IDs. Offset 0x12 holds the user ID, 0x13 the group ID of a file's creator.
					//
					// In multi-user versions, system access requires a logon with account name and
					// password, and the system assigns group and user IDs to running applications
					// according to the previously set up and stored authorization info and
					// inheritance rules. For 4680 OS and 4690 OS, group ID 1 is reserved for the
					// system, group ID 2 for vendor, group ID 3 for the default user group.
					// Background applications started by users have a group ID 2 and user ID 1,
					// whereas operating system background tasks have group IDs 1 or 0 and user IDs
					// 1 or 0. IBM 4680 BASIC and applications started as primary or secondary
					// always get group ID 2 and user ID 1. When applications create files, the
					// system will store their user ID and group ID and the required permissions with
					// the file.
					//
					// With DELWATCH 2.00 and higher under Novell DOS 7, OpenDOS 7.01 and DR-DOS 7.02
					// and higher, this entry is used to store the last modified date stamp for
					// pending delete files and directories. Cleared when file is undeleted or
					// purged. See [implementationSpecific4] for a format description.
					//
					// Last access date (since DOS 7.0 if ACCDATE enabled in CONFIG.SYS for the
					// corresponding drive); see [implementationSpecific4] for a format description.
					//
					// The usage for the owner IDs of existing files and last modified date stamp for
					// deleted files is not conflictive because they are never used at the same time.
					// The usage of the last modified date stamp for deleted files and access date is
					// also not conflictive since access dates are no longer important for deleted
					// files, however, owner IDs and access dates cannot be used at the same time.
					short implementationSpecific5 = br.ReadInt16();

					// Access rights bitmap for world/group/owner read/write/execute/delete
					// protection for password protected files, directories (or volume labels) under
					// DR DOS 3.31 and higher, including PalmDOS, Novell DOS and OpenDOS, and under
					// FlexOS, 4680 OS, 4690 OS, Concurrent DOS, Multiuser DOS, System Manager, and
					// REAL/32.
					//
					// Typical values stored on a single-user system are 0x0000 (PASSWORD /N for all
					// access rights "RWED"), 0x0111 (PASSWORD /D for access rights "RW?-"), 0x0555
					// (PASSWORD /W for access rights "R-?-") and 0x0DDD (PASSWORD /R for files or
					// PASSWORD /P for directories for access rights "--?-"). Bits 1, 5, 9, 12-15
					// will be preserved when changing access rights. If execute bits are set on
					// systems other than FlexOS, 4680 OS or 4690 OS, they will be treated similar
					// to read bits. (Some versions of PASSWORD allow to set passwords on volume
					// labels (PASSWORD /V) as well.)
					//
					// Single-user systems calculate the most restrictive rights of the three sets
					// (DR DOS up to 5.0 used bits 0-3 only) and check if any of the requested file
					// access types requires a permission and if a file password is stored. If not,
					// file access is granted. Otherwise the stored password is checked against an
					// optional global password provided by the operating system and an optional file
					// password provided as part of the filename separated by a semicolon (not under
					// FlexOS, 4680 OS, 4690 OS). If neither of them is provided, the request will
					// fail. If one of them matches, the system will grant access (within the limits
					// of the normal file attributes, that is, a read-only file can still not be
					// opened for write this way), otherwise fail the request.
					//
					// Under FlexOS, 4680 OS and 4690 OS the system assigns group and user IDs to
					// applications when launched. When they request file access, their group and
					// user IDs are compared with the group and user IDs of the file to be opened.
					// If both IDs match, the application will be treated as file owner. If only the
					// group ID matches, the operating system will grant group access to the
					// application, and if the group ID does not match as well, it will grant world
					// access. If an application's group ID and user ID are both 0, the operating
					// system will bypass security checking. Once the permission class has been
					// determined, the operating system will check if any of the access types of the
					// requested file operation requires a permission according to the stored
					// bitflags of the selected class owner, group or world in the file's directory
					// entry. Owner, group and world access rights are independent and do not need to
					// have diminishing access levels. Only, if none of the requested access types
					// require a permission, the operating system will grant access, otherwise it
					// fails.
					//
					// If multiuser file / directory password security is enabled the system will not
					// fail at this stage but perform the password checking mechanism for the
					// selected permission class similar to the procedure described above. With
					// multi-user security loaded many utilities since DR DOS 6.0 will provide an
					// additional /U:name parameter.
					short implementationSpecific6 = br.ReadInt16();
					FATFileAccessRightsFlags fileAccess = (FATFileAccessRightsFlags)implementationSpecific6;

					// Last modified time (since DOS 1.1); see offset 0x0E for a format description.
					//
					// Under Novell DOS, OpenDOS and DR-DOS 7.02 and higher, this entry holds the
					// deletion time of pending delete files or directories under DELWATCH 2.00 or
					// higher. The last modified time stamp is copied to 0x10 for possible later
					// restoration. See offset 0x0E for a format description.
					short implementationSpecific7 = br.ReadInt16();

					// Last modified date; see offset 0x10 for a format description.
					//
					// Under Novell DOS, OpenDOS and DR-DOS 7.02 and higher, this entry holds the
					// deletion date of pending delete files or directories under DELWATCH 2.00 or
					// higher. The last modified date stamp is copied to 0x12 for possible later
					// restoration. See offset 0x10 for a format description.
					short implementationSpecific8 = br.ReadInt16();

					// Start of file in clusters in FAT12 and FAT16. Low two bytes of first cluster
					// in FAT32; with the high two bytes stored at offset 0x14.
					//
					// Entries with the Volume Label flag, subdirectory ".." pointing to FAT12/FAT16
					// root, and empty files with size 0 should have first cluster 0.
					//
					// VFAT LFN entries also have this entry set to 0; on FAT12 and FAT16 volumes
					// this can be used as part of a detection mechanism to distinguish between
					// pending delete files under DELWATCH and VFAT LFNs; see above.
					short implementationSpecific9 = br.ReadInt16();

					// File size in bytes. Entries with the Volume Label or Subdirectory flag set
					// should have a size of 0.
					//
					// VFAT LFN entries never store the value 0x00000000 here. This can be used as
					// part of a detection mechanism to distinguish between pending delete files
					// under DELWATCH and VFAT LFNs; see above.
					//
					// For files larger than 4 GB following the FAT+ proposal, this entry only holds
					// the size of the last chunk of the file (that is bits 31-0). The most
					// significant bits 37-32 are stored in the entry at offset 0x0C.
					short implementationSpecific10 = br.ReadInt16();

					byte[] others = br.ReadBytes(3);
					*/

					byte[] reserved = br.ReadBytes(10);

					short timeCreatedOrLastUpdated = br.ReadInt16();
					short dateCreatedOrLastUpdated = br.ReadInt16();
					short startingClusterNumber = br.ReadInt16();
					uint fileSize = br.ReadUInt32();

					FATFileInfo fi = new FATFileInfo();
					if (fileAttributes != FATFileAttributes.VolumeLabel)
					{
						if (LFN_FileName != String.Empty)
						{
							LFN_FileName = LFN_FileName.TrimNull();

							fi.LongFileName = LFN_FileName;
							fi.ShortFileName = fileName + "." + fileExt;

							LFN_FileName = String.Empty;
						}
						else
						{
							fi.LongFileName = (fileName + "." + fileExt);
							fi.ShortFileName = (fileName + "." + fileExt);
						}
						fi.Offset = startingClusterNumber;
						fi.Length = fileSize;
						if (fileIsDeleted)
						{
							fi.Attributes |= FileAttributes.Deleted;
						}

						if ((fileAttributes & FATFileAttributes.Subdirectory) == FATFileAttributes.Subdirectory)
						{
							long firstSectorOffset = (long)numBytesBeforeRootDir + bytesOccupiedByRootDirEntries;
							int realSectorIndex = fi.Offset - 2; //  2;
							long fileOffset = (firstSectorOffset + (realSectorIndex * mvarBiosParameterBlock.BytesPerSector));

							long pos = br.Accessor.Position;
							br.Accessor.Position = fileOffset;

							// 16896 in test.fat

							br.Accessor.Position = pos;
						}
						else
						{
							fileInfos.Add(fi);
						}
					}
				}
			}
			#endregion
			#region File data
			{
				for (int i = 0; i < fileInfos.Count; i++)
				{
					FATFileInfo fi = fileInfos[i];
					long firstSectorOffset = (long)numBytesBeforeRootDir + bytesOccupiedByRootDirEntries;
					int realSectorIndex = fi.Offset - 2; //  2;
					long fileOffset = (firstSectorOffset + (realSectorIndex * mvarBiosParameterBlock.BytesPerSector));

					File file = new File();
					file.Name = fi.LongFileName;

					file.Attributes = fi.Attributes;

					long pos = br.Accessor.Position;
					if (fileOffset > 0)
					{
						br.Accessor.Position = fileOffset;
						byte[] data = br.ReadBytes(fi.Length);
						file.SetData(data);
					}
					else
					{

					}
					br.Accessor.Position = pos;

					fsom.Files.Add(file);
				}
			}
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			bw.WriteBytes(mvarJumpInstruction);
			bw.WriteFixedLengthString(mvarOEMName, 8);

			#region Bios Parameter Block
			{
				bw.WriteInt16(mvarBiosParameterBlock.BytesPerSector);
				bw.WriteByte(mvarBiosParameterBlock.SectorsPerCluster);
				bw.WriteInt16(mvarBiosParameterBlock.ReservedSectorCount);
				bw.WriteByte(mvarBiosParameterBlock.FileAllocationTableCount);
				bw.WriteInt16(mvarBiosParameterBlock.MaximumRootDirectoryEntryCount);
				if (mvarBiosParameterBlock.TotalSectors <= ushort.MaxValue)
				{
					bw.WriteUInt16((ushort)mvarBiosParameterBlock.TotalSectors);
				}
				else
				{
					bw.WriteUInt16((ushort)0);
				}
				bw.WriteByte((byte)mvarBiosParameterBlock.MediaDescriptor);
				bw.WriteInt16(mvarBiosParameterBlock.SectorsPerFAT16);

				// The following extensions were documented since DOS 3.0, however, they were already supported by
				// some issues of DOS 2.13. MS-DOS 3.10 still supported the DOS 2.0 format, but could use the DOS
				// 3.0 format as well.
				bw.WriteInt16(mvarBiosParameterBlock.SectorsPerTrack);
				bw.WriteInt16(mvarBiosParameterBlock.NumberOfHeads);
				bw.WriteInt32(mvarBiosParameterBlock.HiddenSectorCount);

				// Total logical sectors including hidden sectors. This DOS 3.2 entry is
				// incompatible with a similar entry at offset 0x020 in BPBs since DOS 3.31.
				// It must not be used if the logical sectors entry at offset 0x013 is zero.
				if (mvarBiosParameterBlock.TotalSectors <= ushort.MaxValue)
				{
					bw.WriteUInt16((ushort)mvarBiosParameterBlock.TotalSectors);
				}
				else
				{
					bw.WriteUInt16((ushort)0);
				}
				if (mvarBiosParameterBlock.TotalSectors > ushort.MaxValue)
				{
					bw.WriteInt32(mvarBiosParameterBlock.TotalSectors);
				}
				else
				{
					bw.WriteInt32((int)0);
				}
			}
			#endregion

			long totalFileSize = (mvarBiosParameterBlock.TotalSectors * mvarBiosParameterBlock.BytesPerSector);

			/*
			 *	A simple formula translates a volume's given cluster number CN to a logical sector number LSN:
			 *
			 *	Determine (once) SSA=RSC+FN×SF+ceil((32×RDE)/SS), where the reserved sector count RSC is stored at
			 *	offset 0x00E, the FAT number FN at offset 0x010, the sectors per FAT SF at offset 0x016 (FAT12/FAT16)
			 *	or 0x024 (FAT32), the root directory entries RDE at offset 0x011, the sector size SS at offset 0x00B,
			 *	and ceil(x) rounds up to a whole number.
			 *
			 *	Determine LSN=SSA+(CN-2)×SC, where the sectors per cluster SC are stored at offset 0x00D.
			 *
			 *	On unpartitioned media the volume's number of hidden sectors is zero and therefore LSN and LBA
			 *	addresses become the same for as long as a volume's logical sector size is identical to the
			 *	underlying medium's physical sector size. Under these conditions, it is also simple to translate
			 *	between CHS addresses and LSNs as well:
			 *
			 *	LSN=SPT×(HN+(NOS×TN))+SN-1, where the sectors per track SPT are stored at offset 0x018, and the
			 *	number of sides NOS at offset 0x01A. Track number TN, head number HN, and sector number SN
			 *	correspond to Cylinder-head-sector: the formula gives the known CHS to LBA translation.
			 */

			#region Extended BIOS Parameter Block
			{
				bw.WriteByte(mvarExtendedBiosParameterBlock.PhysicalDriveNumber);

				byte reserved1 = (byte)mvarExtendedBiosParameterBlock.CheckDiskFlags;
				bw.WriteByte(reserved1);

				// Extended boot signature. (Should be 0x29 to indicate that an EBPB with the following 3 entries
				// exists (since OS/2 1.2 and DOS 4.0). Can be 0x28 on some OS/2 1.0-1.1 and PC DOS 3.4 disks
				// indicating an earlier form of the EBPB format with only the serial number following. MS-DOS/PC
				// DOS 4.0 and higher, OS/2 1.2 and higher as well as the Windows NT family recognize both
				// signatures accordingly.)
				byte extendedBootSignature = 0;
				if (mvarExtendedBiosParameterBlock.ExtendedBootSignature.Enabled)
				{
					if (mvarExtendedBiosParameterBlock.ExtendedBootSignature.HasPartitonLabelAndFSType)
					{
						extendedBootSignature = 0x29;
					}
					else
					{
						extendedBootSignature = 0x28;
					}
				}
				bw.WriteByte(extendedBootSignature);

				if (extendedBootSignature == 0x29 || extendedBootSignature == 0x28)
				{
					// Typically the serial number "xxxx-xxxx" is created by a 16-bit addition of both DX values
					// returned by INT 21h/AH=2Ah (get system date) and INT 21h/AH=2Ch (get system time) for the
					// high word and another 16-bit addition of both CX values for the low word of the serial
					// number. Alternatively, some DR-DOS disk utilities provide a /# option to generate a
					// human-readable time stamp "mmdd-hhmm" build from BCD-encoded 8-bit values for the month, day,
					// hour and minute instead of a serial number.
					bw.WriteInt32(mvarExtendedBiosParameterBlock.ExtendedBootSignature.VolumeSerialNumber);
				}
				if (extendedBootSignature == 0x29)
				{
					bw.WriteFixedLengthString(mvarExtendedBiosParameterBlock.ExtendedBootSignature.PartitionVolumeLabel, 11);
					bw.WriteFixedLengthString(mvarExtendedBiosParameterBlock.ExtendedBootSignature.FileSystemType, 8);
				}
			}
			#endregion

			// no clue why this is 448 bytes, but it just is...
			byte[] bootstrapProgram = new byte[448];
			bw.WriteBytes(bootstrapProgram);

			// The number of blocks that appear before the root directory is given by:
			int numBlocksBeforeRootDir = (mvarBiosParameterBlock.FileAllocationTableCount * mvarBiosParameterBlock.SectorsPerFAT16) + 1;
			int numBytesBeforeRootDir = mvarBiosParameterBlock.BytesPerSector * numBlocksBeforeRootDir;

			/*
			br.Accessor.Position = numBytesBeforeRootDir;

			List<string> fileNames = new List<string>();
			List<short> fileOffsets = new List<short>();
			List<uint> fileSizes = new List<uint>();

			string LFN_FileName = String.Empty;

			#region File name table
			{
				while (!br.EndOfStream)
				{
					byte[] fileNameBytes = br.ReadBytes(8);
					if (fileNameBytes[0] == 0x00)
					{
						// Entry is available and no subsequent entry is in use. Also serves as an
						// end marker when DOS scans a directory table. (Since MS-DOS/PC DOS 1.0,
						// but not in 86-DOS).
					}
					else if (fileNameBytes[0] == 0x05)
					{
						// Initial character is actually 0xE5. (since DOS 3.0)
						//
						// Under DR DOS 6.0 and higher, including PalmDOS, Novell DOS and OpenDOS,
						// 0x05 is also used for pending delete files under DELWATCH. Once they are
						// removed from the deletion tracking queue, the first character of an erased
						// file is replaced by 0xE5.
					}
					else if (fileNameBytes[0] == 0x2E)
					{
						// 'Dot' entry; either "." or ".." (since DOS 2.0)
					}
					else if (fileNameBytes[0] == 0xE5)
					{
						// Entry has been previously erased and is available. File undelete
						// utilities must replace this character with a regular character as part of
						// the undeletion process. See also: 0x05.
						//
						// (The reason, why 0xE5 was chosen for this purpose in 86-DOS is down to the
						// fact, that 8-inch CP/M floppies came pre-formatted with this value filled
						// and so could be used to store files out-of-the box.)

					}

					string fileName = System.Text.Encoding.ASCII.GetString(fileNameBytes).Trim();
					if (fileName[0] == '\0') break;

					string fileExt = br.ReadFixedLengthString(3).Trim();

					byte implementationSpecificByte1 = br.ReadByte();

					// CP/M-86 and DOS Plus store user attributes F1'—F4' here. (DOS Plus 1.2 with BDOS 4.1 supports
					// passwords only on CP/M media, not on FAT12 or FAT16 media. While DOS Plus 2.1 supported
					// logical sectored FATs with a partition type 0xF2, FAT16B and FAT32 volumes were not supported
					// by these operation systems. Even if a partition would have been converted to FAT16B it would
					// still not be larger than 32 MB. Therefore, this usage is not conflictive with FAT32.IFS,
					// FAT16+ or FAT32+ as they can never occur on the same type of volume.):
					FATFileAttributes fileAttributes = (FATFileAttributes)implementationSpecificByte1;

					if ((byte)fileAttributes == 15)
					{
						br.Accessor.Position -= 12;

						byte LFN_SequenceNumber = br.ReadByte();
						byte[] LFN_UnicodeNamePart1 = br.ReadBytes(10);
						byte LFN_Attributes = br.ReadByte();
						byte LFN_Type = br.ReadByte();
						byte LFN_Checksum = br.ReadByte();
						byte[] LFN_UnicodeNamePart2 = br.ReadBytes(12);
						short LFN_FirstCluster = br.ReadInt16();
						byte[] LFN_UnicodeNamePart3 = br.ReadBytes(4);
						byte[] LFN_UnicodeNameBytes = new byte[26];

						Array.Copy(LFN_UnicodeNamePart1, 0, LFN_UnicodeNameBytes, 0, LFN_UnicodeNamePart1.Length);
						Array.Copy(LFN_UnicodeNamePart2, 0, LFN_UnicodeNameBytes, LFN_UnicodeNamePart1.Length, LFN_UnicodeNamePart2.Length);
						Array.Copy(LFN_UnicodeNamePart3, 0, LFN_UnicodeNameBytes, LFN_UnicodeNamePart1.Length + LFN_UnicodeNamePart2.Length, LFN_UnicodeNamePart3.Length);

						string LFN_UnicodeName = System.Text.Encoding.Unicode.GetString(LFN_UnicodeNameBytes);
						LFN_FileName = LFN_UnicodeName + LFN_FileName;

						if ((LFN_SequenceNumber & 0x40) == 0x40)
						{
							// This is last LFN entry
						}
						else if (LFN_SequenceNumber == 1)
						{
							// This is the first LFN entry, stop reading now.
						}
						continue;
					}
					if (LFN_FileName != String.Empty)
					{
						LFN_FileName = LFN_FileName.TrimNull();
						fileNames.Add(LFN_FileName);
						LFN_FileName = String.Empty;
					}
					else
					{
						fileNames.Add(fileName + "." + fileExt);
					}

					// - *
					// For a deleted file, the original first character of the filename. For the same feature in
					// various other operating systems, see offset 0x0D if enabled in MSX boot sectors at sector
					// offset 0x026. MSX-DOS supported FAT12 volumes only, but third-party extensions for FAT16
					// volumes exist. Therefore, this usage is not conflictive with FAT32.IFS and FAT32+ below. It
					// does not conflict with the usage for user attributes under CP/M-86 and DOS Plus as well,
					// since they are no longer important for deleted files.
					char deletedFileFirstCharImplementation1 = (char)implementationSpecificByte1;

					// Windows NT and later versions uses bits 3 and 4 to encode case information (see below);
					// otherwise 0.

					// DR-DOS 7.0x reserved bits other than 3 and 4 for internal purposes since 1997. The value
					// should be set to 0 by formating tools and must not be changed by disk tools.

					// On FAT32 volumes under OS/2 and eComStation the third-party FAT32.IFS driver utilizes this
					// entry as a mark byte to indicate the presence of extra " EA. SF" files holding extended
					// attributes with parameter /EAS. Version 0.70 to 0.96 used the magic values 0x00 (no EAs),
					// 0xEA (normal EAs) and 0xEC (critical EAs), whereas version 0.97 and higher since 2003-09 use
					// 0x00, 0x40 (normal EAs) and 0x80 (critical EAs) as bitflags for compatibility with Windows NT.

					// Bits other than 3 and 4 are utilized by FAT+, a proposal how to store files larger than 4 GB
					// on FAT32 (and FAT16B) volumes, currently implemented in some versions of EDR-DOS. The value
					// should be set to 0 by formating tools and must not be changed by disk tools. If some of these
					// bits are set, non-enabled implementations should refuse to open the file. To avoid problems
					// with non-aware operating systems, partitions containing files larger than 4 GB could use
					// non-standard partition IDs to hide the partition from these operating systems. Under DR-DOS,
					// partition IDs of secured partition types can be utilized for this purpose. Files larger than
					// 4 GB should have the Hidden, Read-only and System attributes set to hide them from normal
					// directory searches on non-aware operating systems, similar to password protected files under
					// DR-DOS. While FAT+ implementations do not rely on these attributes being set, for FAT+ large
					// files they may ignore these attributes in file searches and when opening large files for
					// modification and instead treat the System attribute as an alternative combined
					// Read-only+Hidden attribute in this scenario.

					// FAT32.IFS is critically conflictive with FAT32+ revision 2.

					byte implementationSpecificByte2 = br.ReadByte();
					// First character of a deleted file under Novell DOS, OpenDOS and DR-DOS 7.02 and higher. A
					// value of 0xE5 (229), as set by DELPURGE, will prohibit undeletion by UNDELETE, a value of
					// 0x00 will allow conventional undeletion asking the user for the missing first filename
					// character. S/DOS 1 and PTS-DOS 6.51 and higher also support this feature if enabled with
					// SAVENAME=ON in CONFIG.SYS. For the same feature in MSX-DOS, see offset 0x0C.
					char deletedFileFirstCharImplementation2 = (char)implementationSpecificByte2;

					// Create time, fine resolution: 10 ms units, values from 0 to 199 (since DOS 7.0 with VFAT).
					byte createTime = implementationSpecificByte2;

					// Double usage for create time ms and file char is not conflictive, since the creation time is
					// no longer important for deleted files.


					// Under DR DOS 3.31 and higher including PalmDOS, Novell DOS and OpenDOS as well
					// as under Concurrent DOS, Multiuser DOS, System Manager, and REAL/32 and
					// possibly also under FlexOS, 4680 OS, 4690 OS any non-zero value indicates the
					// password hash of a protected file, directory or volume label. The hash is
					// calculated from the first eight characters of a password. If the file
					// operation to be carried out requires a password as per the access rights
					// bitmap stored at offset 0x14, the system tries to match the hash against the
					// hash code of the currently set global password (by PASSWORD /G) or, if this
					// fails, tries to extract a semicolon-appended password from the filespec
					// passed to the operating system and checks it against the hash code stored
					// here. A set password will be preserved even if a file is deleted and later
					// undeleted.
					short implementationSpecific3 = br.ReadInt16();

					// Create time (since DOS 7.0 with VFAT). The hour, minute and second are encoded
					// according to the following bitmap:
					// Bits 	Description
					// -------- ------------------
					// 15-11 	Hours (0-23)
					// 10-5 	Minutes (0-59)
					// 4-0 		Seconds/2 (0-29)
					//
					// The seconds is recorded only to a 2 second resolution. Finer resolution for
					// file creation is found at offset 0x0D.
					//
					// If bits 15-11 > 23 or bits 10-5 > 59 or bits 4-0 > 29 here, or when bits 12-0
					// at offset 0x14 hold an access bitmap and this is not a FAT32 volume or a
					// volume using OS/2 Extended Attributes, then this entry actually holds a
					// password hash, otherwise it can be assumed to be a file creation time.



					// FlexOS, 4680 OS and 4690 OS store a record size in the word at entry 0x10.
					// This is mainly used for their special database-like file types random file,
					// direct file, keyed file, and sequential file. If the record size is set to 0
					// (default) or 1, the operating systems assume a record granularity of 1 byte
					// for the file, for which it will not perform record boundary checks in
					// read/write operations.
					//
					// With DELWATCH 2.00 and higher under Novell DOS 7, OpenDOS 7.01 and DR-DOS
					// 7.02 and higher, this entry is used to store the last modified time stamp for
					// pending delete files and directories. Cleared when file is undeleted or
					// purged. See offset 0x0E for a format description.
					//
					// Create date (since DOS 7.0 with VFAT). The year, month and day are encoded
					// according to the following bitmap:
					// Bits 	Description
					// -------- ------------------
					// 15-9 	Year (0 = 1980, 119 = 2099 supported under DOS/Windows, theoretical
					//			up to 127 = 2107)
					// 8-5 		Month (1–12)
					// 4-0 		Day (1–31)
					//
					// The usage for creation date for existing files and last modified time for
					// deleted files is not conflictive because they are never used at the same time.
					// For the same reason, the usage for the record size of existing files and last
					// modified time of deleted files is not conflictive as well. Creation dates and
					// record sizes cannot be used at the same time, however, both are stored only on
					// file creation and never changed later on, thereby limiting the conflict to
					// FlexOS, 4680 OS and 4690 OS systems accessing files created under foreign
					// operating systems as well as potential display or file sorting problems on
					// systems trying to interpret a record size as creation time. To avoid the
					// conflict, the storage of creation dates should be an optional feature of
					// operating systems supporting it.
					short implementationSpecific4 = br.ReadInt16();


					// FlexOS, 4680 OS, 4690 OS, Multiuser DOS, System Manager, REAL/32 and DR DOS
					// 6.0 and higher with multi-user security enabled use this field to store owner
					// IDs. Offset 0x12 holds the user ID, 0x13 the group ID of a file's creator.
					//
					// In multi-user versions, system access requires a logon with account name and
					// password, and the system assigns group and user IDs to running applications
					// according to the previously set up and stored authorization info and
					// inheritance rules. For 4680 OS and 4690 OS, group ID 1 is reserved for the
					// system, group ID 2 for vendor, group ID 3 for the default user group.
					// Background applications started by users have a group ID 2 and user ID 1,
					// whereas operating system background tasks have group IDs 1 or 0 and user IDs
					// 1 or 0. IBM 4680 BASIC and applications started as primary or secondary
					// always get group ID 2 and user ID 1. When applications create files, the
					// system will store their user ID and group ID and the required permissions with
					// the file.
					//
					// With DELWATCH 2.00 and higher under Novell DOS 7, OpenDOS 7.01 and DR-DOS 7.02
					// and higher, this entry is used to store the last modified date stamp for
					// pending delete files and directories. Cleared when file is undeleted or
					// purged. See [implementationSpecific4] for a format description.
					//
					// Last access date (since DOS 7.0 if ACCDATE enabled in CONFIG.SYS for the
					// corresponding drive); see [implementationSpecific4] for a format description.
					//
					// The usage for the owner IDs of existing files and last modified date stamp for
					// deleted files is not conflictive because they are never used at the same time.
					// The usage of the last modified date stamp for deleted files and access date is
					// also not conflictive since access dates are no longer important for deleted
					// files, however, owner IDs and access dates cannot be used at the same time.
					short implementationSpecific5 = br.ReadInt16();

					// Access rights bitmap for world/group/owner read/write/execute/delete
					// protection for password protected files, directories (or volume labels) under
					// DR DOS 3.31 and higher, including PalmDOS, Novell DOS and OpenDOS, and under
					// FlexOS, 4680 OS, 4690 OS, Concurrent DOS, Multiuser DOS, System Manager, and
					// REAL/32.
					//
					// Typical values stored on a single-user system are 0x0000 (PASSWORD /N for all
					// access rights "RWED"), 0x0111 (PASSWORD /D for access rights "RW?-"), 0x0555
					// (PASSWORD /W for access rights "R-?-") and 0x0DDD (PASSWORD /R for files or
					// PASSWORD /P for directories for access rights "--?-"). Bits 1, 5, 9, 12-15
					// will be preserved when changing access rights. If execute bits are set on
					// systems other than FlexOS, 4680 OS or 4690 OS, they will be treated similar
					// to read bits. (Some versions of PASSWORD allow to set passwords on volume
					// labels (PASSWORD /V) as well.)
					//
					// Single-user systems calculate the most restrictive rights of the three sets
					// (DR DOS up to 5.0 used bits 0-3 only) and check if any of the requested file
					// access types requires a permission and if a file password is stored. If not,
					// file access is granted. Otherwise the stored password is checked against an
					// optional global password provided by the operating system and an optional file
					// password provided as part of the filename separated by a semicolon (not under
					// FlexOS, 4680 OS, 4690 OS). If neither of them is provided, the request will
					// fail. If one of them matches, the system will grant access (within the limits
					// of the normal file attributes, that is, a read-only file can still not be
					// opened for write this way), otherwise fail the request.
					//
					// Under FlexOS, 4680 OS and 4690 OS the system assigns group and user IDs to
					// applications when launched. When they request file access, their group and
					// user IDs are compared with the group and user IDs of the file to be opened.
					// If both IDs match, the application will be treated as file owner. If only the
					// group ID matches, the operating system will grant group access to the
					// application, and if the group ID does not match as well, it will grant world
					// access. If an application's group ID and user ID are both 0, the operating
					// system will bypass security checking. Once the permission class has been
					// determined, the operating system will check if any of the access types of the
					// requested file operation requires a permission according to the stored
					// bitflags of the selected class owner, group or world in the file's directory
					// entry. Owner, group and world access rights are independent and do not need to
					// have diminishing access levels. Only, if none of the requested access types
					// require a permission, the operating system will grant access, otherwise it
					// fails.
					//
					// If multiuser file / directory password security is enabled the system will not
					// fail at this stage but perform the password checking mechanism for the
					// selected permission class similar to the procedure described above. With
					// multi-user security loaded many utilities since DR DOS 6.0 will provide an
					// additional /U:name parameter.
					short implementationSpecific6 = br.ReadInt16();
					FATFileAccessRightsFlags fileAccess = (FATFileAccessRightsFlags)implementationSpecific6;

					// Last modified time (since DOS 1.1); see offset 0x0E for a format description.
					//
					// Under Novell DOS, OpenDOS and DR-DOS 7.02 and higher, this entry holds the
					// deletion time of pending delete files or directories under DELWATCH 2.00 or
					// higher. The last modified time stamp is copied to 0x10 for possible later
					// restoration. See offset 0x0E for a format description.
					short implementationSpecific7 = br.ReadInt16();

					// Last modified date; see offset 0x10 for a format description.
					//
					// Under Novell DOS, OpenDOS and DR-DOS 7.02 and higher, this entry holds the
					// deletion date of pending delete files or directories under DELWATCH 2.00 or
					// higher. The last modified date stamp is copied to 0x12 for possible later
					// restoration. See offset 0x10 for a format description.
					short implementationSpecific8 = br.ReadInt16();

					// Start of file in clusters in FAT12 and FAT16. Low two bytes of first cluster
					// in FAT32; with the high two bytes stored at offset 0x14.
					//
					// Entries with the Volume Label flag, subdirectory ".." pointing to FAT12/FAT16
					// root, and empty files with size 0 should have first cluster 0.
					//
					// VFAT LFN entries also have this entry set to 0; on FAT12 and FAT16 volumes
					// this can be used as part of a detection mechanism to distinguish between
					// pending delete files under DELWATCH and VFAT LFNs; see above.
					short implementationSpecific9 = br.ReadInt16();

					// File size in bytes. Entries with the Volume Label or Subdirectory flag set
					// should have a size of 0.
					//
					// VFAT LFN entries never store the value 0x00000000 here. This can be used as
					// part of a detection mechanism to distinguish between pending delete files
					// under DELWATCH and VFAT LFNs; see above.
					//
					// For files larger than 4 GB following the FAT+ proposal, this entry only holds
					// the size of the last chunk of the file (that is bits 31-0). The most
					// significant bits 37-32 are stored in the entry at offset 0x0C.
					short implementationSpecific10 = br.ReadInt16();

					byte[] others = br.ReadBytes(3);
					* - //

					byte[] reserved = br.ReadBytes(10);
					short timeCreatedOrLastUpdated = br.ReadInt16();
					short dateCreatedOrLastUpdated = br.ReadInt16();
					short startingClusterNumber = br.ReadInt16();
					uint fileSize = br.ReadUInt32();

					fileOffsets.Add(startingClusterNumber);
					fileSizes.Add(fileSize);
				}
			}
			#endregion
			#region File data
			{
				int bytesOccupiedByRootDirEntries = (mvarBiosParameterBlock.MaximumRootDirectoryEntryCount * 32);

				for (int i = 0; i < fileNames.Count; i++)
				{
					long fileOffset = (numBytesBeforeRootDir + bytesOccupiedByRootDirEntries + ((fileOffsets[i] - 2) * mvarBiosParameterBlock.BytesPerSector));

					File file = new File();
					file.Name = fileNames[i];

					long pos = br.Accessor.Position;
					br.Accessor.Position = fileOffset;
					byte[] data = br.ReadBytes(fileSizes[i]);
					br.Accessor.Position = pos;

					file.SetDataAsByteArray(data);

					fsom.Files.Add(file);
				}
			}
			#endregion
		*/
		}
		#endregion
	}

}
