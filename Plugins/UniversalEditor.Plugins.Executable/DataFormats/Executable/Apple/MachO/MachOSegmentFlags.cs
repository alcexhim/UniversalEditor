//
//  MachOSegmentFlags.cs - indicates attributes of a segment of an Apple Mach-O executable file
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

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Indicates attributes of a segment of an Apple Mach-O executable file.
	/// </summary>
	[Flags()]
	public enum MachOSegmentFlags
	{
		None = 0x00,
		/// <summary>
		/// Indicates that the file contents for this segment occupy the high part of the virtual memory space; the low part is zero-filled (for stacks in core files).
		/// </summary>
		HighVM = 0x01,
		/// <summary>
		/// Indicates that the segment is the virtual memory that's allocated by a fixed virtual memory library for overlap checking in the link editor.
		/// </summary>
		FVMLib = 0x02,
		/// <summary>
		/// Indicates that the segment has nothing that was relocated in it and nothing relocated to it (that is, it may be safely replaced without relocation).
		/// </summary>
		NoReloc = 0x03
	}
}
