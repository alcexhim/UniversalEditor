//
//  WMFBitCount.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public enum WMFBitCount
	{
		/// <summary>
		/// The number of bits per pixel is undefined.
		/// </summary>
		/// <remarks>
		/// The image SHOULD be in either JPEG or PNG format.[4] Neither of these formats includes a
		/// color table, so this value specifies that no color table is present in the Colors field of the DIB
		/// Object. See [JFIF] and [RFC2083] for more information concerning JPEG and PNG compression
		/// formats.
		/// </remarks>
		BitCount0 = 0x0000,
		/// <summary>
		/// The image is specified with two colors.
		/// </summary>
		/// <remarks>
		/// Each pixel in the bitmap in the BitmapBuffer field of the DIB Object is represented by a single
		/// bit. If the bit is clear, the pixel is displayed with the color of the first entry in the color table in the
		/// Colors field; if the bit is set, the pixel has the color of the second entry in the table.
		/// </remarks>
		BitCount1 = 0x0001,
		/// <summary>
		/// The image is specified with a maximum of 16 colors.
		/// </summary>
		/// <remarks>
		/// Each pixel in the bitmap in the BitmapBuffer field of the DIB Object is represented by a 4-bit
		/// index into the color table in the Colors field, and each byte contains 2 pixels.
		/// </remarks>
		BitCount2 = 0x0004,
		/// <summary>
		/// The image is specified with a maximum of 256 colors.
		/// </summary>
		/// <remarks>
		/// Each pixel in the bitmap in the BitmapBuffer field of the DIB Object is represented by a 4-bit
		/// index into the color table in the Colors field, and each byte contains 2 pixels.
		/// </remarks>
		BitCount3 = 0x0008,
		/// <summary>
		/// The image is specified with a maximum of 2^16 colors.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Each pixel in the bitmap in the BitmapBuffer field of the DIB Object is represented by a 16-bit
		/// value.
		/// </para>
		/// <para>
		/// If the Compression field of the BitmapInfoHeader Object is BI_RGB, the Colors field of the
		/// DIB Object is NULL. Each WORD (defined in [MS-DTYP] section 2.2.61) in the bitmap represents
		/// a single pixel. The relative intensities of red, green, and blue are represented with 5 bits for each
		/// color component. The value for blue is in the least significant 5 bits, followed by 5 bits each for
		/// green and red. The most significant bit is not used.
		/// </para>
		/// <para>
		/// If the Compression field is set to BI_BITFIELDS, the color table in the Colors field contains three
		/// DWORD (defined in [MS-DTYP] section 2.2.9) color masks that specify the red, green, and blue
		/// components, respectively, of each pixel. Each WORD in the bitmap represents a single pixel. The
		/// color table is used for optimizing colors on palette-based devices, and contains the number of
		/// entries specified by the ColorUsed field of the BitmapInfoHeader Object.
		/// </para>
		/// <para>
		/// When the Compression field is set to BI_BITFIELDS, bits set in each DWORD mask MUST be
		/// contiguous and SHOULD NOT overlap the bits of another mask.
		/// </para>
		/// <para>
		/// BI_RGB and BI_BITFIELDS are defined in Compression Enumeration, section 2.1.1.7.
		/// </para>
		/// </remarks>
		BitCount4 = 0x0010,
		/// <summary>
		/// The bitmap in the BitmapBuffer field of the DIB Object has a maximum of 2^24
		/// colors, and the Colors field is NULL. Each 3-byte triplet in the bitmap represents the relative
		/// intensities of blue, green, and red, respectively, for a pixel.
		/// </summary>
		BitCount5 = 0x0018,
		/// <summary>
		/// The bitmap in the BitmapBuffer field of the DIB Object has a maximum of 2^24
		/// colors.
		/// </summary>
		/// <remarks>
		/// <para>
		/// If the Compression field of the BitmapInfoHeader Object is set to BI_RGB, the Colors field of
		/// the DIB Object is set to NULL. Each DWORD in the bitmap in the BitmapBuffer field represents
		/// the relative intensities of blue, green, and red, respectively, for a pixel. The high byte in each
		/// DWORD is not used.
		/// </para>
		/// <para>
		/// If the Compression field is set to BI_BITFIELDS, the color table in the Colors field contains three
		/// DWORD color masks that specify the red, green, and blue components, respectively, of each
		/// pixel. Each DWORD in the bitmap represents a single pixel. The color table is used for optimizing
		/// colors used on palette-based devices and contains the number of entries specified by the
		/// ColorUsed field of the BitmapInfoHeader Object.
		/// </para>
		/// <para>
		/// When the Compression field is set to BI_BITFIELDS, bits set in each DWORD mask MUST be
		/// contiguous and MUST NOT overlap the bits of another mask. All the bits in the pixel do not need to
		/// be used.
		/// </para>
		/// <para>
		/// BI_RGB and BI_BITFIELDS are specified in the Compression Enumeration.
		/// </para>
		/// </remarks>
		BitCount6 = 0x0020
	}
}
