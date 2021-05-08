//
//  PESectionCharacteristics.cs - describes attributes for a section in a Portable Executable file
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

namespace UniversalEditor.DataFormats.Executable.Microsoft.PortableExecutable
{
	/// <summary>
	/// Describes attributes for a section in a Portable Executable file.
	/// </summary>
	[Flags()]
	public enum PESectionCharacteristics : uint
	{
		None = 0x00000000,
		/// <summary>
		/// Reserved.
		/// </summary>
		TypeNoPad = 0x00000008,
		/// <summary>
		/// Section contains code.
		/// </summary>
		ContainsCode = 0x00000020,
		/// <summary>
		/// Section contains initialized data.
		/// </summary>
		ContainsInitializedData = 0x00000040,
		/// <summary>
		/// Section contains uninitialized data.
		/// </summary>
		ContainsUninitializedData = 0x00000080,
		/// <summary>
		/// Reserved.
		/// </summary>
		LinkOther = 0x00000100,
		/// <summary>
		/// Section contains comments or some other type of information.
		/// </summary>
		LinkInformation = 0x00000200,
		/// <summary>
		/// Section contents will not become part of image.
		/// </summary>
		LinkRemove = 0x00000800,
		/// <summary>
		/// Section contents comdat.
		/// </summary>
		LinkComdat = 0x00001000,
		/// <summary>
		/// Reset speculative exceptions handling bits in the TLB entries for this section.
		/// </summary>
		ResetSpeculativeExceptions = 0x00004000,
		/// <summary>
		/// Section content can be accessed relative to GP
		/// </summary>
		GPRelative = 0x00008000,
		MemoryFarData = 0x00008000,
		MemoryPurgeable = 0x00020000,
		Memory16Bit = 0x00020000,
		MemoryLocked = 0x00040000,
		MemoryPreload = 0x00080000,
		Align1Byte = 0x00100000,
		Align2Bytes = 0x00200000,
		Align4Bytes = 0x00300000,
		Align8Bytes = 0x00400000,
		/// <summary>
		/// Default alignment if no others are specified.
		/// </summary>
		Align16Bytes = 0x00500000,
		Align32Bytes = 0x00600000,
		Align64Bytes = 0x00700000,
		Align128Bytes = 0x00800000,
		Align256Bytes = 0x00900000,
		Align512Bytes = 0x00A00000,
		Align1024Bytes = 0x00B00000,
		Align2048Bytes = 0x00C00000,
		Align4096Bytes = 0x00D00000,
		Align8192Bytes = 0x00E00000,
		AlignMask = 0x00F00000,
		/// <summary>
		/// Section contains extended relocations.
		/// </summary>
		LinkExtendedRelocations = 0x01000000,
		/// <summary>
		/// Section can be discarded.
		/// </summary>
		MemoryDiscardable = 0x02000000,
		/// <summary>
		/// Section is not cachable.
		/// </summary>
		MemoryNotCached = 0x04000000,
		/// <summary>
		/// Section is not pageable.
		/// </summary>
		MemoryNotPaged = 0x08000000,
		/// <summary>
		/// Section is shareable.
		/// </summary>
		MemoryShared = 0x10000000,
		/// <summary>
		/// Section is executable.
		/// </summary>
		MemoryExecutable = 0x20000000,
		/// <summary>
		/// Section is readable.
		/// </summary>
		MemoryReadable = 0x40000000,
		/// <summary>
		/// Section is writeable.
		/// </summary>
		MemoryWritable = 0x80000000
	}
}
