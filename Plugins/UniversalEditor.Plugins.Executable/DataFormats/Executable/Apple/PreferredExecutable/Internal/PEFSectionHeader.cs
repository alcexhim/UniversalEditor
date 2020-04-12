//
//  PEFSectionHeader.cs - represents a header for a Preferred Executable file
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

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable.Internal
{
	/// <summary>
	/// Represents a header for a Preferred Executable file.
	/// </summary>
	internal struct PEFSectionHeader
	{
		/// <summary>
		/// Holds the offset from the start of the section name table to the location of the section name. The name of the section is
		/// stored as a C-style null-terminated character string. If the section has no name, the nameOffset field contains -1.
		/// </summary>
		public int nameOffset;
		/// <summary>
		/// Indicates the preferred address (as designated by the linker) at which to place the section's instance. If the Code Fragment
		/// Manager can place the instance in the preferred memory location, the load-time and link-time addresses are identical and no
		/// internal relocations need to be performed.
		/// </summary>
		public uint defaultAddress;
		/// <summary>
		/// Indicates the size, in bytes, required by the section's contents at execution time. For a code section, this size is merely
		/// the size of the executable code. For a data section, this size indicates the sum of the size of the initialized data plus
		/// the size of any zero-initialized data. Zero-initialized data appears at the end of a section's contents and its length is
		/// exactly the difference of the totalSize and unpackedSize values. For noninstantiated sections, this field is ignored.
		/// </summary>
		public uint totalSize;
		/// <summary>
		/// The size of the section's contents that is explicitly initialized from the container. For code sections, this field is the
		/// size of the executable code. For an unpacked data section, this field indicates only the size of the initialized data. For
		/// packed data this is the size to which the compressed contents expand. The unpackedSize value also defines the boundary
		/// between the explicitly initialized portion and the zero-initialized portion. For noninstantiated sections, this field is
		/// ignored.
		/// </summary>
		public uint unpackedSize;
		/// <summary>
		/// Indicates the size, in bytes, of a section's contents in the container. For code sections, this field is the size of the
		/// executable code. For an unpacked data section, this field indicates only the size of the initialized data. For a packed data
		/// section (see Table 8-1 (page 8-8)) this field is the size of the pattern description contained in the section.
		/// </summary>
		public uint packedSize;
		/// <summary>
		/// Contains the offset from the beginning of the container to the start of the section's contents. Packed data sections and the
		/// loader section should be 4-byte aligned. Code sections and data sections that are not packed should be at least 16-byte
		/// aligned.
		/// </summary>
		public uint containerOffset;
		/// <summary>
		/// Indicates the type of section as well as any special attributes. Table 8-1 (page 8-8) shows the currently supported section
		/// types. Note that instantiated read-only sections cannot have zero-initialized extensions.
		/// </summary>
		public PEFSectionKind sectionKind;
		/// <summary>
		/// Controls how the section information is shared among processes by the Code Fragment Manager. You can specify any of the
		/// sharing options shown in Table 8-2 (page 8-9).
		/// </summary>
		public PEFSharingOption shareKind;
		/// <summary>
		/// Indicates the desired alignment for instantiated sections in memory as a power of 2. A value of 0 indicates 1-byte
		/// alignment, 1 indicates 2-byte (halfword) alignment, 2 indicates 4-byte (word) alignment, and so on. Note that this field
		/// does not indicate the alignment of raw data relative to a container. The Code Fragment Manager does not support this field
		/// under System 7.
		/// 
		/// In System 7, the Code Fragment Manager gives 16-byte alignment to all writable sections. The alignment of read-only
		/// sections, which are used directly from the container, is dependent on the alignment of the section's contents within the
		/// container and the overall alignment of the container itself. When the container is not file-mapped, the overall container
		/// alignment is 16 bytes. When the container is file-mapped, the entire data fork is mapped and aligned to a 4KB boundary. The
		/// overall alignment of a file-mapped container thus depends on the container's alignment within the data fork. Note that
		/// file-mapping is currently supported only on PowerPC machines, and only when virtual memory is enabled.
		/// </summary>
		public byte alignment;
		/// <summary>
		/// Currently reserved and must be set to 0.
		/// </summary>
		public byte reservedA;
	}
}
