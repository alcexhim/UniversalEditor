//
//  TIFFDataType.cs
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
	public enum TIFFDataType : ushort
	{
		/// <summary>
		/// 8-bit unsigned integer.
		/// </summary>
		Byte = 0x0001,
		/// <summary>
		/// 8-bit byte that contains a 7-bit ASCII code; the last byte must be NUL (binary zero).
		/// </summary>
		Ascii = 0x0002,
		/// <summary>
		/// 16-bit (2-byte) unsigned integer.
		/// </summary>
		Short = 0x0003,
		/// <summary>
		/// 32-bit (4-byte) unsigned integer.
		/// </summary>
		Long = 0x0004,
		/// <summary>
		/// Two <see cref="Long" />s: the first represents the numerator of a fraction; the second, the denominator.
		/// </summary>
		Rational = 0x0005,

		/// <summary>
		/// An 8-bit signed (twos-complement) integer. Added in TIFF 6.0.
		/// </summary>
		SignedByte = 0x0006,
		/// <summary>
		/// An 8-bit byte that may contain anything, depending on the definition of the field. Added in TIFF 6.0.
		/// </summary>
		Undefined = 0x0007,
		/// <summary>
		/// A 16-bit (2-byte) signed (twos-complement) integer. Added in TIFF 6.0.
		/// </summary>
		SignedShort = 0x0008,
		/// <summary>
		/// A 32-bit (4-byte) signed (twos-complement) integer. Added in TIFF 6.0.
		/// </summary>
		SignedLong = 0x0009,
		/// <summary>
		/// Two SLONG’s: the first represents the numerator of a fraction, the second the denominator. Added in TIFF 6.0.
		/// </summary>
		SignedRational = 0x000A,
		/// <summary>
		/// Single precision (4-byte) IEEE format. Added in TIFF 6.0.
		/// </summary>
		Float = 0x000B,
		/// <summary>
		/// Double precision (8-byte) IEEE format. Added in TIFF 6.0.
		/// </summary>
		Double = 0x000C
	}
}
