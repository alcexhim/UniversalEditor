//
//  TIFFCompression.cs
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
	public enum TIFFCompression : ushort
	{
		/// <summary>
		/// No compression, but pack data into bytes as tightly as possible, leaving no unused bits (except at the end of a row). The component values are stored as an array of
		/// type BYTE. Each scan line (row) is padded to the next BYTE boundary.
		/// </summary>
		None = 0x01,
		/// <summary>
		/// CCITT Group 3 1-Dimensional Modified Huffman run length encoding.
		/// </summary>
		RunLengthEncoding = 0x02,
		Group3Fax = 0x03,
		Group4Fax = 0x04,
		LZW = 0x05,
		JPEG = 0x06,
		Deflate = 0x08,
		/// <summary>
		/// PackBits compression, a simple byte-oriented run length scheme.
		/// </summary>
		PackBits = 32773
	}
}
