using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Bitmap
{
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
