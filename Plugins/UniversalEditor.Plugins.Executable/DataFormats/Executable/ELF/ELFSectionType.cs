//
//  ELFSectionType.cs - indicates the type of section in an Executable and Linkable Format (ELF) executable
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

namespace UniversalEditor.DataFormats.Executable.ELF
{
	/// <summary>
	/// Indicates the type of section in an Executable and Linkable Format (ELF) executable.
	/// </summary>
	public enum ELFSectionType : uint
	{
		/// <summary>
		/// This value marks the section header as inactive; it does not have an associated
		/// section. Other members of the section header have undefined values.
		/// </summary>
		Null = 0,
		/// <summary>
		/// The section holds information defined by the program, whose format and meaning
		/// are determined solely by the program.
		/// </summary>
		ProgramSpecific = 1,
		/// <summary>
		/// These sections hold a symbol table. Currently, an object file may have only one
		/// section of each type, but this restriction may be relaxed in the future.
		/// Typically, SHT_SYMTAB provides symbols for link editing, though it may also be
		/// used for dynamic linking. As a complete symbol table, it may contain many
		/// symbols unnecessary for dynamic linking. Consequently, an object file may also
		/// contain a SHT_DYNSYM section, which holds a minimal set of dynamic linking
		/// symbols, to save space.
		/// </summary>
		SymbolTable = 2,
		/// <summary>
		/// The section holds a string table. An object file may have multiple string table
		/// sections.
		/// </summary>
		StringTable = 3,
		/// <summary>
		/// The section holds relocation entries with explicit addends, such as type
		/// Elf32_Rela for the 32-bit class of object files. An object file may have
		/// multiple relocation sections.
		/// </summary>
		RelocationWithExplicitAddends = 4,
		/// <summary>
		/// The section holds a symbol hash table. All objects participating in dynamic
		/// linking must contain a symbol hash table. Currently, an object file may have
		/// only one hash table, but this restriction may be relaxed in the future.
		/// </summary>
		SymbolHashTable = 5,
		/// <summary>
		/// The section holds information for dynamic linking. Currently, an object file
		/// may have only one dynamic section, but this restriction may be relaxed in the
		/// future.
		/// </summary>
		DynamicLinking = 6,
		/// <summary>
		/// The section holds information that marks the file in some way.
		/// </summary>
		Note = 7,
		/// <summary>
		/// A section of this type occupies no space in the file but otherwise resembles
		/// SHT_PROGBITS. Although this section contains no bytes, the sh_offset member
		/// contains the conceptual file offset.
		/// </summary>
		NoBits = 8,
		/// <summary>
		/// The section holds relocation entries without explicit addends, such as type
		/// Elf32_Rel for the 32-bit class of object files. An object file may have multiple
		/// relocation sections.
		/// </summary>
		Relocation = 9,
		/// <summary>
		/// This section type is reserved but has unspecified semantics. Programs that
		/// contain a section of this type do not conform to the ABI.
		/// </summary>
		ShLib = 10,
		/// <summary>
		/// These sections hold a symbol table. Currently, an object file may have only one
		/// section of each type, but this restriction may be relaxed in the future.
		/// Typically, SHT_SYMTAB provides symbols for link editing, though it may also be
		/// used for dynamic linking. As a complete symbol table, it may contain many
		/// symbols unnecessary for dynamic linking. Consequently, an object file may also
		/// contain a SHT_DYNSYM section, which holds a minimal set of dynamic linking
		/// symbols, to save space.
		/// </summary>
		DynamicSymbolTable = 11,
		/// <summary>
		/// Values in this inclusive range are reserved for processor-specific semantics.
		/// </summary>
		ProcessorSpecificLo = 0x70000000,
		/// <summary>
		/// Values in this inclusive range are reserved for processor-specific semantics.
		/// </summary>
		ProcessorSpecificHi = 0x7FFFFFFF,
		/// <summary>
		/// This value specifies the lower bound of the range of indexes reserved for
		/// application programs.
		/// </summary>
		UserSpecificLo = 0x80000000,
		/// <summary>
		/// This value specifies the upper bound of the range of indexes reserved for
		/// application programs. Section types between SHT_LOUSER and SHT_HIUSER may be
		/// used by the application, without conflicting with current or future
		/// system-defined section types.
		/// </summary>
		UserSpecificHi = 0xFFFFFFFF
	}
}
