//
//  TIFFFillOrder.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public enum TIFFFillOrder : ushort
	{
		/// <summary>
		/// Pixels are arranged within a byte such that pixels with lower column values are stored in the higher-order bits of the byte. 1-bit uncompressed data example: Pixel 0
		/// of a row is stored in the high-order bit of byte 0, pixel 1 is stored in the next-highest bit, ..., pixel 7 is stored in the low-order bit of byte 0, pixel 8 is
		/// stored in the high-order bit of byte 1, and so on. CCITT 1-bit compressed data example: The high-order bit of the first compression code is stored in the high-order
		/// bit of byte 0, the next-highest bit of the first compression code is stored in the next-highest bit of byte 0, and so on.
		/// </summary>
		Normal = 1,
		/// <summary>
		/// Pixels are arranged within a byte such that pixels with lower column values are stored in the lower-order bits of the byte. We recommend that FillOrder = 2 be used
		/// only in special-purpose applications. It is easy and inexpensive for writers to reverse bit order by using a 256-byte lookup table. FillOrder = 2 should be used only
		/// when BitsPerSample = 1 and the data is either uncompressed or compressed using CCITT 1D or 2D compression, to avoid potentially ambiguous situations.
		/// </summary>
		Reverse = 2
	}
}
