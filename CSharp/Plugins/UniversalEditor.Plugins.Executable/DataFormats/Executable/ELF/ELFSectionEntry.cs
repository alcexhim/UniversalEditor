//
//  ELFSectionEntry.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace UniversalEditor.DataFormats.Executable.ELF
{
	public struct ELFSectionEntry
	{
		public string name;
		/// <summary>
		/// This member specifies the name of the section. Its value is an index into
		/// the section header string table section, giving the location of a
		/// null-terminated string.
		/// </summary>
		public uint nameindex;
		/// <summary>
		/// This member categorizes the section’s contents and semantics. Section types
		/// and their descriptions appear below.
		/// </summary>
		public ELFSectionType type;
		/// <summary>
		/// Sections support 1-bit flags that describe miscellaneous attributes.
		/// </summary>
		public ELFSectionFlags flags;
		/// <summary>
		/// If the section will appear in the memory image of a process, this member
		/// gives the address at which the section’s first byte should reside.
		/// Otherwise, the member contains 0.
		/// </summary>
		public ulong addr;
		/// <summary>
		/// This member’s value gives the byte offset from the beginning of the file
		/// to the first byte in the section. One section type, SHT_NOBITS described
		/// below, occupies no space in the file, and its sh_offset member locates
		/// the conceptual placement in the file.
		/// </summary>
		public uint offset;
		/// <summary>
		/// This member gives the section’s size in bytes. Unless the section type is
		/// SHT_NOBITS, the section occupies sh_size bytes in the file. A section of
		/// type SHT_NOBITS may have a non-zero size, but it occupies no space in the
		/// file.
		/// </summary>
		public ulong size;
		/// <summary>
		/// This member holds a section header table index link, whose interpretation
		/// depends on the section type. A table below describes the values.
		/// </summary>
		public uint link;
		/// <summary>
		/// This member holds extra information, whose interpretation depends on the
		/// section type. A table below describes the values.
		/// </summary>
		public uint info;
		/// <summary>
		/// Some sections have address alignment constraints. For example, if a
		/// section holds a doubleword, the system must ensure doubleword alignment
		/// for the entire section. That is, the value of sh_addr must be congruent
		/// to 0, modulo the value of sh_addralign. Currently, only 0 and positive
		/// integral powers of two are allowed. Values 0 and 1 mean the section has
		/// no alignment constraints.
		/// </summary>
		public ulong addralign;
		/// <summary>
		/// Some sections hold a table of fixed-size entries, such as a symbol table.
		/// For such a section, this member gives the size in bytes of each entry.
		/// The member contains 0 if the section does not hold a table of fixed-size
		/// entries.
		/// </summary>
		public ulong entsize;
	}
}
