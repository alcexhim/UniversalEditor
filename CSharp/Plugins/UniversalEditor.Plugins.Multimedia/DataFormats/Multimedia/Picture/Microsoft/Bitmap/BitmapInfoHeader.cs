using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap
{
	public struct BitmapInfoHeader
	{
		private int mvarWidth;
		/// <summary>
		/// The width of the bitmap, in pixels. If biCompression is JPEG or PNG, the biWidth member specifies the
		/// width of the decompressed JPEG or PNG image file, respectively.
		/// </summary>
		public int Width { get { return mvarWidth; } set { mvarWidth = value; } }

        private int mvarHeaderSize;
        public int HeaderSize { get { return mvarHeaderSize; } set { mvarHeaderSize = value; } }

		private int mvarHeight;
		public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

		private short mvarPlanes;
		/// <summary>
		/// The number of planes for the target device. This value must be set to 1.
		/// </summary>
		public short Planes { get { return mvarPlanes; } set { mvarPlanes = value; } }

		private BitmapCompression mvarCompression;
		/// <summary>
		/// The type of compression for a compressed bottom-up bitmap (top-down DIBs cannot be compressed).
		/// </summary>
		public BitmapCompression Compression { get { return mvarCompression; } set { mvarCompression = value; } }

		private int mvarImageSize;
		/// <summary>
		/// The size, in bytes, of the image. This may be set to zero for Uncompressed bitmaps. If biCompression is
		/// JPEG or PNG, biSizeImage indicates the size of the JPEG or PNG image buffer, respectively.
		/// </summary>
		public int ImageSize { get { return mvarImageSize; } set { mvarImageSize = value; } }

		private int mvarPelsPerMeterX;
		/// <summary>
		/// The horizontal resolution, in pixels-per-meter, of the target device for the bitmap. An application
		/// can use this value to select a bitmap from a resource group that best matches the characteristics of
		/// the current device.
		/// </summary>
		public int PelsPerMeterX { get { return mvarPelsPerMeterX; } set { mvarPelsPerMeterX = value; } }

		private int mvarPelsPerMeterY;
		/// <summary>
		/// The vertical resolution, in pixels-per-meter, of the target device for the bitmap.
		/// </summary>
		public int PelsPerMeterY { get { return mvarPelsPerMeterY; } set { mvarPelsPerMeterY = value; } }

		private int mvarUsedColorIndexCount;
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
		public int UsedColorIndexCount { get { return mvarUsedColorIndexCount; } set { mvarUsedColorIndexCount = value; } }

		private int mvarRequiredColorIndexCount;
		/// <summary>
		/// The number of color indexes that are required for displaying the bitmap. If this value is zero, all
		/// colors are required.
		/// </summary>
		public int RequiredColorIndexCount { get { return mvarRequiredColorIndexCount; } set { mvarRequiredColorIndexCount = value; } }

		private BitmapBitsPerPixel mvarPixelDepth;
		public BitmapBitsPerPixel PixelDepth { get { return mvarPixelDepth; } set { mvarPixelDepth = value; } }

		public static BitmapInfoHeader Load(IO.BinaryReader br)
		{
			BitmapInfoHeader header = new BitmapInfoHeader();
			header.HeaderSize = br.ReadInt32();                            // 40       vs. 56
            header.Width = br.ReadInt32();                              
			header.Height = br.ReadInt32();
			header.Planes = br.ReadInt16();                             // 1
			header.PixelDepth = (BitmapBitsPerPixel)br.ReadInt16();
			header.Compression = (BitmapCompression)br.ReadInt32();
			header.ImageSize = br.ReadInt32();
			header.PelsPerMeterX = br.ReadInt32();
			header.PelsPerMeterY = br.ReadInt32();
			header.UsedColorIndexCount = br.ReadInt32();
			header.RequiredColorIndexCount = br.ReadInt32();

            if (header.HeaderSize < 56)
            {
                br.BaseStream.Position += (56 - header.HeaderSize);
            }
			return header;
		}
		public static void Save(IO.BinaryWriter bw, BitmapInfoHeader header)
		{
			bw.Write((int)40); // header size
			bw.Write(header.Width);
			bw.Write(header.Height);
			bw.Write(header.Planes);
			bw.Write((short)header.PixelDepth);
			bw.Write((int)header.Compression);
			bw.Write(header.ImageSize);
			bw.Write(header.PelsPerMeterX);
			bw.Write(header.PelsPerMeterY);
			bw.Write(header.UsedColorIndexCount);
			bw.Write(header.RequiredColorIndexCount);
		}
	}
}
