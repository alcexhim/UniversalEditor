//
//  FATFileAttributes.cs - indicates attributes for a file in a FAT filesystem
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
	/// Indicates attributes for a file in a FAT filesystem.
	/// </summary>
	[Flags()]
	public enum FATFileAttributes
	{
		/// <summary>
		/// Read Only. (Since DOS 2.0) If this bit is set, the operating system will not allow a file to be opened
		/// for modification.
		/// </summary>
		/// <remarks>
		///		<para>
		///			Deliberately setting this bit for files which will not be written to (executables, shared
		///			libraries and data files) may help avoid problems with concurrent file access in multi-tasking,
		///			multi-user or network environments with applications not specifically designed to work in such
		///			environments (i.e. non-SHARE-enabled programs).
		///		</para>
		///		<para>
		///			Files larger than 4 GB following the FAT+ proposal may have the Read-only attribute set to keep
		///			unaware operating systems from modifying the file. If FAT+ large files are opened for write via
		///			FAT+ APIs, enabled implementations may ignore the Read-only attribute of FAT+ files when the
		///			System attribute is not set at the same time, and otherwise treat the System attribute as
		///			alternative Read-only attribute for FAT+ large files.
		///		</para>
		/// </remarks>
		ReadOnly = 0x01,
		/// <summary>
		/// Hidden. Hides files or directories from normal directory views.
		/// </summary>
		/// <remarks>
		///		<para>
		///			Under DR DOS 3.31 and higher, under PalmDOS, Novell DOS, OpenDOS, Concurrent DOS, Multiuser DOS,
		///			REAL/32, password protected files and directories also have the hidden attribute set.
		///			Password-aware operating systems should not hide password-protected files from directory views,
		///			even if this bit may be set. The password protection mechanism does not depend on the hidden
		///			attribute being set up to including DR-DOS 7.03, but if the hidden attribute is set, it should
		///			not be cleared for any password-protected files.
		///		</para>
		///		<para>
		///			Files larger than 4 GB following the FAT+ proposal may have the hidden attribute set to hide them
		///			from normal directory scans on unaware operating systems; in this special case, they may ignore
		///			the Hidden attribute for FAT+ files when the System attribute is not set at the same time, and
		///			otherwise treat the System attribute as alternative Hidden attribute for FAT+ large files.
		///		</para>
		/// </remarks>
		Hidden = 0x02,
		/// <summary>
		/// System. Indicates that the file belongs to the system and must not be physically moved (f.e. during
		/// defragmentation), because there may be references into the file using absolute addressing bypassing the
		/// file system (boot loaders, kernel images, swap files, extended attributes, etc.).
		/// </summary>
		System = 0x04,
		/// <summary>
		/// Volume Label. (Since DOS 2.0) Indicates an optional directory volume label, normally only residing in a
		/// volume's root directory.
		/// </summary>
		/// <remarks>
		///		<para>
		///			Ideally, the volume label should be the first entry in the directory (after reserved entries) in
		///			order to avoid problems with VFAT LFNs. If this volume label is not present, some systems may
		///			fall back to display the partition volume label instead, if a EBPB is present in the boot sector
		///			(not present with some non-bootable block device drivers, and possibly not writeable with boot
		///			sector write protection). Even if this volume label is present, partitioning tools like FDISK may
		///			display the partition volume label instead. The entry occupies a directory entry but has no file
		///			associated with it. Volume labels have a filesize entry of zero.
		///		</para>
		///		<para>
		///			Pending delete files and directories under DELWATCH have the volume attribute set until they are
		///			purged or undeleted.
		///		</para>
		///	</remarks>
		VolumeLabel = 0x08,
		/// <summary>
		/// The file entry is a component of a Long File Name entry.
		/// </summary>
		LongFileName = ReadOnly | Hidden | System | VolumeLabel,
		/// <summary>
		/// Subdirectory. (Since DOS 2.0) Indicates that the cluster-chain associated with this entry gets
		/// interpreted as subdirectory instead of as a file. Subdirectories have a filesize entry of zero.
		/// </summary>
		Subdirectory = 0x10,
		/// <summary>
		/// Archive. (Since DOS 2.0) Typically set by the operating system as soon as the file is created or
		/// modified to mark the file as "dirty", and reset by backup software once the file has been backed up to
		/// indicate "pure" state.
		/// </summary>
		Archive = 0x20,
		/// <summary>
		/// Device (internally set for character device names found in filespecs, never found on disk), must not be
		/// changed by disk tools.
		/// </summary>
		Device = 0x40,
		/// <summary>
		/// Reserved, must not be changed by disk tools.
		/// </summary>
		Reserved = 0x80
	}
}
