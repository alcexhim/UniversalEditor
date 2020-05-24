//
//  BitmapInfoHeader.cs - internal structure representing the BITMAPINFOHEADER for a Windows bitmap image
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

using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap
{
	/// <summary>
	/// Internal structure representing the BITMAPINFOHEADER for a Windows bitmap image.
	/// </summary>
	public struct BitmapInfoHeader
	{
		/// <summary>
		/// The width of the bitmap, in pixels. If biCompression is JPEG or PNG, the biWidth member specifies the
		/// width of the decompressed JPEG or PNG image file, respectively.
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Gets or sets the size of the BITMAPINFOHEADER structure.
		/// </summary>
		/// <value>The size of the BITMAPINFOHEADER structure.</value>
		public int HeaderSize { get; set; }
		/// <summary>
		/// The height of the bitmap, in pixels. If biCompression is JPEG or PNG, the biHeight member specifies the
		/// height of the decompressed JPEG or PNG image file, respectively.
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// The number of planes for the target device. This value must be set to 1.
		/// </summary>
		public short Planes { get; set; }
		/// <summary>
		/// The type of compression for a compressed bottom-up bitmap (top-down DIBs cannot be compressed).
		/// </summary>
		public BitmapCompression Compression { get; set; }
		/// <summary>
		/// The size, in bytes, of the image. This may be set to zero for Uncompressed bitmaps. If biCompression is
		/// JPEG or PNG, biSizeImage indicates the size of the JPEG or PNG image buffer, respectively.
		/// </summary>
		public int ImageSize { get; set; }
		/// <summary>
		/// The horizontal resolution, in pixels-per-meter, of the target device for the bitmap. An application
		/// can use this value to select a bitmap from a resource group that best matches the characteristics of
		/// the current device.
		/// </summary>
		public int PelsPerMeterX { get; set; }
		/// <summary>
		/// The vertical resolution, in pixels-per-meter, of the target device for the bitmap.
		/// </summary>
		public int PelsPerMeterY { get; set; }
		/// <summary>
		///		<para>
		///			The number of color indexes in the color table that are actually used by the bitmap. If this
		///			value is zero, the bitmap uses the maximum number of colors corresponding to the value of the
		///			biBitCount member for the compression mode specified by biCompression.
		///		</para>
		///		<para>
		///			If biClrUsed is nonzero and the biBitCount member is less than 16, the biClrUsed member specifies
		///			the actual number of colors the graphics engine or device driver accesses. If biBitCount is 16 or
		///			greater, the biClrUsed member specifies the size of the color table used to optimize performance
		///			of the system color palettes. If biBitCount equals 16 or 32, the optimal color palette starts
		///			immediately following the three DWORD masks.
		///		</para>
		///		<para>
		///			When the bitmap array immediately follows the BITMAPINFO structure, it is a packed bitmap. Packed
		///			bitmaps are referenced by a single pointer. Packed bitmaps require that the biClrUsed member must
		///			be either zero or the actual size of the color table.
		///		</para>
		/// </summary>
		public int UsedColorIndexCount { get; set; }
		/// <summary>
		/// The number of color indexes that are required for displaying the bitmap. If this value is zero, all
		/// colors are required.
		/// </summary>
		public int RequiredColorIndexCount { get; set; }
		public BitmapBitsPerPixel PixelDepth { get; set; }
		public uint[] Bitfields;

		public static BitmapInfoHeader Load(IO.Reader br)
		{
			long start = br.Accessor.Position;

			BitmapInfoHeader header = new BitmapInfoHeader();
			header.HeaderSize = br.ReadInt32();                            // 40       vs. 56
			header.Width = br.ReadInt32();
			header.Height = br.ReadInt32();
			header.Planes = br.ReadInt16();                             // 1
			header.PixelDepth = (BitmapBitsPerPixel)br.ReadInt16();
			header.Compression = (BitmapCompression)br.ReadInt32();
			header.ImageSize = br.ReadInt32();								// 4400
			header.PelsPerMeterX = br.ReadInt32();                          // 11811
			header.PelsPerMeterY = br.ReadInt32();                          // 11811
			header.UsedColorIndexCount = br.ReadInt32();
			header.RequiredColorIndexCount = br.ReadInt32();

			if (header.Compression == BitmapCompression.Bitfields)
			{
				uint bitfieldR = br.ReadUInt32();
				uint bitfieldG = br.ReadUInt32();
				uint bitfieldB = br.ReadUInt32();
				uint bitfieldA = br.ReadUInt32();
				header.Bitfields = new uint[] { bitfieldR, bitfieldG, bitfieldB, bitfieldA };
			}

			if (header.HeaderSize < 56)
			{
				br.Seek(56 - header.HeaderSize, SeekOrigin.Current);
			}

			if (br.Accessor.Position != (start + header.HeaderSize))
			{
				long offset = (start + header.HeaderSize) - br.Accessor.Position;
				br.Seek(offset, SeekOrigin.Current);
			}
			return header;
		}
		public static void Save(IO.Writer bw, BitmapInfoHeader header)
		{
			bw.WriteInt32((int)40); // header size
			bw.WriteInt32(header.Width);
			bw.WriteInt32(header.Height);
			bw.WriteInt16(header.Planes);
			bw.WriteInt16((short)header.PixelDepth);
			bw.WriteInt32((int)header.Compression);
			bw.WriteInt32(header.ImageSize);
			bw.WriteInt32(header.PelsPerMeterX);
			bw.WriteInt32(header.PelsPerMeterY);
			bw.WriteInt32(header.UsedColorIndexCount);
			bw.WriteInt32(header.RequiredColorIndexCount);
			if (header.Compression == BitmapCompression.Bitfields)
			{
				bw.WriteUInt32Array(header.Bitfields);
			}
		}
	}
}
