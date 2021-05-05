//
//  Endianness.cs - specify big-endian or little-endian
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
		/// Little-endian except for bytes in 32-bit values which are stored with the 16-bit halves swapped (also known as middle-endian).
		/// </summary>
		PDPEndian
    }
}
