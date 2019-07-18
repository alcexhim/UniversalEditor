using System;
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Picture
{
	public class PictureObjectModel : ObjectModel
	{
        private System.Drawing.Point lastAddedLocation = new System.Drawing.Point(0, 0);
        
		private List<System.Drawing.Color> mvarColorMap = new List<System.Drawing.Color>();
		public List<System.Drawing.Color> ColorMap { get { return mvarColorMap; } }

        public System.Drawing.Bitmap ToBitmap()
        {
            byte[] imageByteArray = ToByteArray();

			//**************  NOTE  *******************
			// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
			// The stride refers to the number of bytes allocated for one scanline of the bitmap.
			// In your loop, you copy the pixels one scanline at a time and take into
			// consideration the amount of padding that occurs due to memory alignment.
			// calculate the stride, in bytes, of the image (32bit aligned width of each image row)
			int pixelDepth = 32;
			int stride = (((int)mvarWidth * pixelDepth + 31) & ~31) >> 3; // width in bytes
			int padding = stride - ((((int)mvarWidth * pixelDepth) + 7) / 8);

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
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(mvarWidth, mvarHeight, stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, imageByteHandle);

			/*
			// get the Pixel format to use with the Bitmap object
			PixelFormat pf = this.GetPixelFormat();


			// load the color map into the Bitmap, if it exists
			if (this.objTargaHeader.ColorMap.Count > 0)
			{
				// get the Bitmap's current palette
				List<System.Drawing.Color> paletteEntries = new List<System.Drawing.Color>();

				// loop trough each color in the loaded file's color map
				for (int i = 0; i < this.objTargaHeader.ColorMap.Count; i++)
				{
					// is the AttributesType 0 or 1 bit
					if (AttributesType == 0 || AttributesType == 1)
					{
						// use 255 for alpha ( 255 = opaque/visible ) so we can see the image
						paletteEntries.Add(System.Drawing.Color.FromArgb(255, this.objTargaHeader.ColorMap[i].R, this.objTargaHeader.ColorMap[i].G, this.objTargaHeader.ColorMap[i].B));
					}
					else
					{
						// use whatever value is there
						paletteEntries.Add(objTargaHeader.ColorMap[i]);
					}
				}
			}
			else
			{
				// no color map

				// check to see if this is a Black and White (Greyscale)
				if (mvarPixelDepth == 8 && (mvarImageType == TargaImageType.UncompressedGrayscale || mvarImageType == TargaImageType.CompressedGrayscale))
				{
					// get the current palette
					System.Drawing.Imaging.ColorPalette pal = bitmap.Palette;

					// create the grayscale palette
					for (int i = 0; i < 256; i++)
					{
						pal.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
					}

					// set the new palette back to the Bitmap object
					bitmap.Palette = pal;
				}
			}
			*/

            return bitmap;
        }   
        public void SetPixel(System.Drawing.Color color)
        {
            if (lastAddedLocation.X >= mvarWidth && lastAddedLocation.Y >= mvarHeight)
            {
                throw new InvalidOperationException("Out of image space. Try resizing the image");
            }

            if (lastAddedLocation.X >= mvarWidth)
            {
                lastAddedLocation.X = 0;
                lastAddedLocation.Y++;
            }

            SetPixel(color, lastAddedLocation.X, lastAddedLocation.Y);
        }
        public void SetPixel(System.Drawing.Color color, int x, int y)
        {
            if (x >= mvarWidth || y >= mvarHeight)
            {
                throw new InvalidOperationException("Out of image space. Try resizing the image");
            }

            int index = (x + (y * mvarWidth)) * 4;
            bitmapData[index] = (byte)color.R;
            bitmapData[index + 1] = (byte)color.G;
            bitmapData[index + 2] = (byte)color.B;
            bitmapData[index + 3] = (byte)color.A;

            int realIndex = (int)(index / 4);
            bitmapDataSet[realIndex] = true;
        }
        public void ClearPixel(int x, int y)
        {
            int index = (lastAddedLocation.X + (lastAddedLocation.Y * mvarWidth)) * 4;
            bitmapData[index] = 0;
            bitmapData[index + 1] = 0;
            bitmapData[index + 2] = 0;
            bitmapData[index + 3] = 0;

            int realIndex = (int)(index / 4);
            bitmapDataSet[realIndex] = false;
        }
        public System.Drawing.Color GetPixel(int x, int y)
        {
            if (x >= mvarWidth || y >= mvarHeight)
            {
                throw new InvalidOperationException("Out of image space. Try resizing the image");
            }

            int index = (x + (y * mvarWidth)) * 4;
            int realIndex = (int)(index / 4);
            if (bitmapDataSet[realIndex])
            {
                byte r = bitmapData[index + 0];
                byte g = bitmapData[index + 1];
                byte b = bitmapData[index + 2];
                byte a = bitmapData[index + 3];

                System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
                return color;
            }
            return System.Drawing.Color.Empty;
        }
        public System.Drawing.Color GetPixel(System.Drawing.Point point)
        {
            return GetPixel(point.X, point.Y);
        }

        private byte[] bitmapData = new byte[] { 0, 0, 0, 0 };
        private bool[] bitmapDataSet = new bool[] { false };

        private int mvarWidth = 1;
        public int Width { get { return mvarWidth; } set { mvarWidth = value; bitmapData = new byte[mvarWidth * mvarHeight * 4]; bitmapDataSet = new bool[mvarWidth * mvarHeight]; } }
        private int mvarHeight = 1;
        public int Height { get { return mvarHeight; } set { mvarHeight = value; bitmapData = new byte[mvarWidth * mvarHeight * 4]; bitmapDataSet = new bool[mvarWidth * mvarHeight]; } }


		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Picture";
            omr.Path = new string[] { "Multimedia", "Picture" };
			return omr;
		}
		private PictureItem.PictureItemCollection mvarItems = new PictureItem.PictureItemCollection();
		public PictureItem.PictureItemCollection Items { get { return this.mvarItems; } }

		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel destination)
		{
			PictureObjectModel clone = (destination as PictureObjectModel);
		}

        public byte[] ToByteArray()
        {
            // memory goes from left to right, top to bottom
            byte[] data = new byte[mvarWidth * mvarHeight * 4];
            int i = 0;
            for (int h = 0; h < mvarHeight; h++)
            {
                for (int w = 0; w < mvarWidth; w++)
                {
					int index = (w + (h * mvarWidth)) * 4;
					int realIndex = (int)(index / 4);
					if (bitmapDataSet[realIndex])
					{
						data[i + 2] = bitmapData[index + 0];
						data[i + 1] = bitmapData[index + 1];
						data[i] = bitmapData[index + 2];
						data[i + 3] = bitmapData[index + 3];
					}
                    i += 4;
                }
            }
            return data;
        }
		public static PictureObjectModel FromByteArray(byte[] data, int width, int height)
		{
			if (width == 0 || height == 0) return null;

			PictureObjectModel pic = new PictureObjectModel();
			pic.Width = width;
			pic.Height = height;

			// memory goes from left to right, top to bottom
			int i = 0;
			for (int h = 0; h < height; h++)
			{
				for (int w = 0; w < width; w++)
				{
					byte b = data[i];
					byte g = data[i + 1];
					byte r = data[i + 2];
					byte a = data[i + 3];

					System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
					pic.SetPixel(color, w, h);

					i += 4;
				}
			}
			return pic;
		}
    }
}
