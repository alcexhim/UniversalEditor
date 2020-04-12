//
//  MachOSection.cs - describes a section in an Apple Mach-O executable
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
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO.Internal
{
	/// <summary>
	/// Describes a section in an Apple Mach-O executable.
	/// </summary>
	public struct MachOSection
	{
		/// <summary>
		/// section's name
		/// </summary>
		public string sectname;
		/// <summary>
		/// segment the section is in
		/// </summary>
		public string segname;
		/// <summary>
		/// section's memory address
		/// </summary>
		public uint addr;
		/// <summary>
		/// section's size in bytes
		/// </summary>
		public uint size;
		/// <summary>
		/// section's file offset
		/// </summary>
		public uint offset;
		/// <summary>
		/// section's alignment
		/// </summary>
		public uint align;
		/// <summary>
		/// file offset of relocation entries
		/// </summary>
		public uint reloff;
		/// <summary>
		/// number of relocation entries
		/// </summary>
		public uint nreloc;
		/// <summary>
		/// flags
		/// </summary>
		public MachOSectionFlags flags;
		/// <summary>
		/// reserved
		/// </summary>
		public uint reserved1;
		/// <summary>
		/// reserved
		/// </summary>
		public uint reserved2;
	}
}
