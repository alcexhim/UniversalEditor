// Universal Editor input/output module shared code - endianness
// Copyright (C) 2011  Mike Becker
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

namespace UniversalEditor.IO
{
    using System;

	/// <summary>
	/// Represents the order of bytes in a multi-byte value (for example, <see cref="Int16" />, <see cref="Int32" />,
	/// and <see cref="Int64" />).
	/// </summary>
    public enum Endianness
    {
		/// <summary>
		/// The bytes are stored with the least-significant byte at the lowest address, while the following bytes are
		/// stored in increasing order of significance. (0x0A0B0C0D = { 0x0D, 0x0C, 0x0B, 0x0A })
		/// </summary>
		LittleEndian,
		/// <summary>
		/// The bytes are stored with the most-significant byte at the lowest address, while the following bytes are
		/// stored in decreasing order of significance. (0x0A0B0C0D = { 0x0A, 0x0B, 0x0C, 0x0D })
		/// </summary>
        BigEndian,
		/// <summary>
		/// Little-endian except for bytes in 32-bit values which are stored with the 16-bit halves swapped.
		/// </summary>
		PDPEndian
    }
}

