//
//  RARHeaderFlags.cs - indicates header attributes for a RAR archive
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
	/// Indicates header attributes for a RAR archive.
	/// </summary>
	[Flags()]
	public enum RARHeaderFlags
	{
		ArchiveVolume = 0x0001,
		/// <summary>
		/// The archive comment is present. RAR 3.x uses the separate comment block
		/// and does not set this flag.
		/// </summary>
		CommentPresent = 0x0002,
		/// <summary>
		/// Archive lock attribute
		/// </summary>
		Lock = 0x0004,
		/// <summary>
		/// Solid attribute (solid archive)
		/// </summary>
		Solid = 0x0008,
		/// <summary>
		/// New volume naming scheme ('volname.partN.rar')
		/// </summary>
		NewVolumeNames = 0x0010,
		/// <summary>
		/// Authenticity information present. RAR 3.x does not set this flag.
		/// </summary>
		AuthenticityPresent = 0x0020,
		/// <summary>
		/// Recovery record present
		/// </summary>
		RecoveryRecordPresent = 0x0040,
		/// <summary>
		/// Block headers are encrypted
		/// </summary>
		EncryptedBlockHeaders = 0x0080,
		/// <summary>
		/// First volume (set only by RAR 3.0 and later)
		/// </summary>
		FirstVolume = 0x0100
	}
}
