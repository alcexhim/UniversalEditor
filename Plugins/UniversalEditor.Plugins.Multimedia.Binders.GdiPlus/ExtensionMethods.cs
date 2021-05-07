using System;
using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor
{
	public static class ExtensionMethods
	{
		public static System.Drawing.Bitmap ToBitmap(this PictureObjectModel pic)
		{
			// THIS WORKS - DO NOT TOUCH
			// IF YOU WANT PROOF, USE THE SAMPLE PROGRAM IN THE BLOCK COMMENT BELOW:
			/*
			PictureObjectModel pic1 = new PictureObjectModel(4, 4);
			pic1.SetPixel(System.Drawing.Color.Red);
			pic1.SetPixel(System.Drawing.Color.Green);
			pic1.SetPixel(System.Drawing.Color.Blue);
			pic1.SetPixel(System.Drawing.Color.Yellow);

			pic1.SetPixel(System.Drawing.Color.Green);
			pic1.SetPixel(System.Drawing.Color.Blue);
			pic1.SetPixel(System.Drawing.Color.Yellow);
			pic1.SetPixel(System.Drawing.Color.Red);

			pic1.SetPixel(System.Drawing.Color.Blue);
			pic1.SetPixel(System.Drawing.Color.Yellow);
			pic1.SetPixel(System.Drawing.Color.Green);
			pic1.SetPixel(System.Drawing.Color.Red);

			pic1.SetPixel(System.Drawing.Color.Blue);
			pic1.SetPixel(System.Drawing.Color.Green);
			pic1.SetPixel(System.Drawing.Color.Yellow);
			pic1.SetPixel(System.Drawing.Color.Red);
			pic1.ToBitmap().Save(@"C:\Temp\test1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
			*/

			byte[] imageByteArray = pic.ToByteArray();
			// System.IO.File.WriteAllBytes(@"G:\Applications\Concertroid\bin\Debug\Models\NightcorePrincess\rose_foxtail\test.data", imageByteArray);

			//**************  NOTE  *******************
			// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
			// The stride refers to the number of bytes allocated for one scanline of the bitmap.
			// In your loop, you copy the pixels one scanline at a time and take into
			// consideration the amount of padding that occurs due to memory alignment.
			// calculate the stride, in bytes, of the image (32bit aligned width of each image row)
			int pixelDepth = 32;
			int stride = (((pic.Width * pixelDepth) + 7) / 8); // width in bytes
			int padding = 0;

			byte[] newImageByteArray = new byte[imageByteArray.Length + padding];
			Array.Copy(imageByteArray, 0, newImageByteArray, 0, imageByteArray.Length);

			// since the Bitmap constructor requires a pointer to an array of image bytes
			// we have to pin down the memory used by the byte array and use the pointer
			// of this pinned memory to create the Bitmap.
			// This tells the Garbage Collector to leave the memory alone and DO NOT touch it.
			System.Runtime.InteropServices.GCHandle imageByteGCHandle = System.Runtime.InteropServices.GCHandle.Alloc(newImageByteArray, System.Runtime.InteropServices.GCHandleType.Pinned);

			IntPtr imageByteHandle = imageByteGCHandle.AddrOfPinnedObject();

			// Bitmap construction and translation to System.Drawing.Bitmap are handled
			// DataFormat-agnostically by the ObjectModel. This code was adapted from the
			// reference code for the TrueVisionTGADataFormat but the parameters are different...
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(pic.Width, pic.Height, stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, imageByteHandle);

			return bitmap;
		}
		public static System.Drawing.Color ToGdiColor(this Color color)
		{
			return System.Drawing.Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
		}
		public static Color ToUniversalColor(this System.Drawing.Color color)
		{
			return Color.FromRGBAByte(color.R, color.G, color.B, color.A);
		}
	}
}
