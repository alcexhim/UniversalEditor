//
//  RARFileHeaderFlags.cs - indicates header attributes for a file in a RAR archive
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

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	/// <summary>
	/// Indicates header attributes for a file in a RAR archive.
	/// </summary>
	[Flags()]
	public enum RARFileHeaderFlags
	{
		/// <summary>
		/// File is continued from previous volume.
		/// </summary>
		ContinuedFromPrevious = 0x01,
		/// <summary>
		/// File continued in next volume.
		/// </summary>
		ContinuedInNext = 0x02,
		/// <summary>
		/// File is encrypted with password.
		/// </summary>
		Encrypted = 0x04,
		/// <summary>
		/// File comment is present. RAR 3.x uses the separate comment block and does not set this flag.
		/// </summary>
		CommentPresent = 0x08,
		/// <summary>
		/// Information from previous files is used (solid flag) (for RAR 2.0 and later)
		/// </summary>
		Solid = 0x10,
		/*
                bits 7 6 5 (for RAR 2.0 and later)

                     0 0 0    - dictionary size   64 KB
                     0 0 1    - dictionary size  128 KB
                     0 1 0    - dictionary size  256 KB
                     0 1 1    - dictionary size  512 KB
                     1 0 0    - dictionary size 1024 KB
                     1 0 1    - dictionary size 2048 KB
                     1 1 0    - dictionary size 4096 KB
                     1 1 1    - file is directory
        */
		/// <summary>
		/// HIGH_PACK_SIZE and HIGH_UNP_SIZE fields are present. These fields are used to archive only very large
		/// files (larger than 2Gb), for smaller files these fields are absent.
		/// </summary>
		SupportLargeFiles = 0x100,
		/// <summary>
		/// FILE_NAME contains both usual and encoded Unicode name separated by zero. In this case NAME_SIZE field
		/// is equal to the length of usual name plus encoded Unicode name plus 1. If this flag is present, but
		/// FILE_NAME does not contain zero bytes, it means that file name is encoded using UTF-8.
		/// </summary>
		SupportUnicode = 0x200,
		/// <summary>
		/// The header contains additional 8 bytes after the file name, which are required to increase encryption
		/// security (so called 'salt').
		/// </summary>
		EncryptionSaltPresent = 0x400,
		/// <summary>
		/// Version flag. It is an old file version, a version number is appended to file name as ';n'.
		/// </summary>
		SupportVersioning = 0x800,
		/// <summary>
		/// Extended time field present.
		/// </summary>
		ExtendedTimeFieldPresent = 0x1000,
		/// <summary>
		/// This bit always is set, so the complete block size is HEAD_SIZE + PACK_SIZE (and plus HIGH_PACK_SIZE,
		/// if <see cref="SupportLargeFiles" /> is set)
		/// </summary>
		AlwaysSet = 0x8000
	}
}
