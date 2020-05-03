//
//  V5RARFileFlags.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.WinRAR.V5
{
	[Flags()]
	public enum RARFileBlockFlags
	{
		/// <summary>
		/// Directory file system object (file header only).
		/// </summary>
		DirectoryFileSystemObject = 0x0001,
		/// <summary>
		/// Time field in Unix format is present.
		/// </summary>
		TimeFieldPresent = 0x0002,
		/// <summary>
		/// CRC32 field is present.
		/// </summary>
		CRC32Present = 0x0004,
		/// <summary>
		/// Unpacked size is unknown.
		/// </summary>
		/// <remarks>
		/// If flag 0x0008 (<see cref="UnpackedSizeUnknown" />) is set, unpacked size field is still present, but must be ignored and extraction must be
		/// performed until reaching the end of compression stream. This flag can be set if actual file size is larger than reported by OS or if file size is
		/// unknown such as for all volumes except last when archiving from stdin to multivolume archive.
		/// </remarks>
		UnpackedSizeUnknown = 0x0008
	}
}
