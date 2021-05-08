//
//  MachOSegment.cs - represents a segment in an Apple Mach-O executable file
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

namespace UniversalEditor.DataFormats.Executable.Apple.MachO.Internal
{
	/// <summary>
	/// Represents a segment in an Apple Mach-O executable file.
	/// </summary>
	internal struct MachOSegment
	{
		/// <summary>
		/// Segment's name
		/// </summary>
		public string segname;
		/// <summary>
		/// segment's memory address
		/// </summary>
		public uint vmaddr;
		/// <summary>
		/// segment's memory size
		/// </summary>
		public uint vmsize;
		/// <summary>
		/// segment's file offset
		/// </summary>
		public uint fileoff;
		/// <summary>
		/// amount to map from file
		/// </summary>
		public uint filesize;
		/// <summary>
		/// maximum VM protection
		/// </summary>
		public MachOVMProtection maxprot;
		/// <summary>
		/// initial VM protection
		/// </summary>
		public MachOVMProtection initprot;
		/// <summary>
		/// number of sections
		/// </summary>
		public uint nsects;
		/// <summary>
		/// flags
		/// </summary>
		public MachOSegmentFlags flags;
	}
}
