//
//  BitmapCompression.cs - indicates the compression method of a Windows bitmap image
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
	/// Indicates the compression method of a Windows bitmap image.
	/// </summary>
	public enum BitmapCompression : int
	{
		/// <summary>
		/// An uncompressed format.
		/// </summary>
		None = 0,
		/// <summary>
		/// A run-length encoded (RLE) format for bitmaps with 8 bpp. The compression format is a 2-byte format
		/// consisting of a count byte followed by a byte containing a color index.
		/// </summary>
		RLE8 = 1,
		/// <summary>
		/// An RLE format for bitmaps with 4 bpp. The compression format is a 2-byte format consisting of a count
		/// byte followed by two word-length color indexes.
		/// </summary>
		RLE4 = 2,
		/// <summary>
		/// Specifies that the bitmap is not compressed and that the color table consists of three DWORD color masks
		/// that specify the red, green, and blue components, respectively, of each pixel. This is valid when used
		/// with 16- and 32-bpp bitmaps.
		/// </summary>
		Bitfields = 3,
		/// <summary>
		/// Indicates that the image is a JPEG image.
		/// </summary>
		JPEG = 4,
		/// <summary>
		///	Indicates that the image is a PNG image.
		/// </summary>
		PNG = 5
	}
}
