//
//  WIMArchiveFlags.cs - indicates attributes for a Windows Image (WIM) file
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	/// <summary>
	/// Indicates attributes for a Windows Image (WIM) file.
	/// </summary>
	[Flags()]
	public enum WIMArchiveFlags : uint
	{
		None = 0x00000000,
		Reserved = 0x00000001,
		/// <summary>
		/// Resources within the WIM (both file and metadata) are compressed.
		/// </summary>
		Compressed = 0x00000002,
		/// <summary>
		/// The contents of this WIM should not be changed.
		/// </summary>
		ReadOnly = 0x00000004,
		/// <summary>
		/// Resource data specified by the images within this WIM may be contained in another WIM.
		/// </summary>
		Spanned = 0x00000008,
		/// <summary>
		/// This WIM contains file resources only.  It does not contain any file metadata.
		/// </summary>
		ResourceOnly = 0x00000010,
		/// <summary>
		/// This WIM contains file metadata only.
		/// </summary>
		MetadataOnly = 0x00000020,
		/// <summary>
		/// Limits one writer to the WIM file when opened with the WIM_FLAG_SHARE_WRITE mode. This flag is primarily used in the Windows Deployment Services (WDS) scenario.
		/// </summary>
		WriteInProgress = 0x00000040,
		ReparsePointFixup = 0x00000080,
		CompressReserved = 0x00010000,
		/// <summary>
		/// When <see cref="WIMArchiveFlags.Compressed" /> is set, resources within the wim are compressed using XPRESS compression.
		/// </summary>
		CompressedXpress = 0x00020000,
		/// <summary>
		/// When <see cref="WIMArchiveFlags.Compressed" /> is set, resources within the wim are compressed using LZX compression.
		/// </summary>
		CompressedLZX = 0x00040000
	}
}
