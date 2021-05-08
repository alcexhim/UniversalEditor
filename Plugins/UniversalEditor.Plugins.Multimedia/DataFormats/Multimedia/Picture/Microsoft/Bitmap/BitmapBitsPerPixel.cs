//
//  BitmapBitsPerPixel.cs - indicates the bit depth of a Windows bitmap image
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap
{
	/// <summary>
	/// Indicates the bit depth of a Windows bitmap image.
	/// </summary>
	public enum BitmapBitsPerPixel : short
	{
		/// <summary>
		/// The number of bits-per-pixel is specified or is implied by the JPEG or PNG format.
		/// </summary>
		Implied = 0,
		/// <summary>
		/// The bitmap is monochrome, and the bmiColors member of BITMAPINFO contains two entries. Each bit
		/// in the bitmap array represents a pixel. If the bit is clear, the pixel is displayed with the
		/// color of the first entry in the bmiColors table; if the bit is set, the pixel has the color of
		/// the second entry in the table.
		/// </summary>
		Monochrome = 1,
		/// <summary>
		/// The bitmap has a maximum of 16 colors, and the bmiColors member of BITMAPINFO contains up to 16
		/// entries. Each pixel in the bitmap is represented by a 4-bit index into the color table. For
		/// example, if the first byte in the bitmap is 0x1F, the byte represents two pixels. The first pixel
		/// contains the color in the second table entry, and the second pixel contains the color in the
		/// sixteenth table entry.
		/// </summary>
		Color16 = 4,
		/// <summary>
		/// The bitmap has a maximum of 256 colors, and the bmiColors member of BITMAPINFO contains up to 256
		/// entries. In this case, each byte in the array represents a single pixel.
		/// </summary>
		Color256 = 8,
		/// <summary>
		///		<para>
		///			The bitmap has a maximum of 2^16 colors. If the biCompression member of the BITMAPINFOHEADER is
		///			BI_RGB, the bmiColors member of BITMAPINFO is NULL. Each WORD in the bitmap array represents a
		///			single pixel. The relative intensities of red, green, and blue are represented with five bits for
		///			each color component. The value for blue is in the least significant five bits, followed by five
		///			bits each for green and red. The most significant bit is not used. The bmiColors color table is
		///			used for optimizing colors used on palette-based devices, and must contain the number of entries
		///			specified by the biClrUsed member of the BITMAPINFOHEADER.
		///		</para>
		///		<para>
		///			If the biCompression member of the BITMAPINFOHEADER is BI_BITFIELDS, the bmiColors member
		///			contains three DWORD color masks that specify the red, green, and blue components, respectively,
		///			of each pixel. Each WORD in the bitmap array represents a single pixel.
		///		</para>
		///		<para>
		///			When the biCompression member is BI_BITFIELDS, bits set in each DWORD mask must be contiguous
		///			and should not overlap the bits of another mask. All the bits in the pixel do not have to be
		///			used.
		///		</para>
		/// </summary>
		HighColor = 16,
		/// <summary>
		/// The bitmap has a maximum of 2^24 colors, and the bmiColors member of BITMAPINFO is NULL. Each 3-byte
		/// triplet in the bitmap array represents the relative intensities of blue, green, and red, respectively,
		/// for a pixel. The bmiColors color table is used for optimizing colors used on palette-based devices, and
		/// must contain the number of entries specified by the biClrUsed member of the BITMAPINFOHEADER.
		/// </summary>
		TrueColor = 24,
		/// <summary>
		/// The bitmap has a maximum of 2^32 colors. If the biCompression member of the BITMAPINFOHEADER is BI_RGB,
		/// the bmiColors member of BITMAPINFO is NULL. Each DWORD in the bitmap array represents the relative
		/// intensities of blue, green, and red for a pixel. The value for blue is in the least significant 8 bits,
		/// followed by 8 bits each for green and red. The high byte in each DWORD is not used. The bmiColors color
		/// table is used for optimizing colors used on palette-based devices, and must contain the number of
		/// entries specified by the biClrUsed member of the BITMAPINFOHEADER.
		/// </summary>
		DeepColor = 32
	}
}
