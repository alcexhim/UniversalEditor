using System;
using System.Collections.Generic;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Picture
{
	public class PictureObjectModel : ObjectModel
	{
		public class PictureObjectModelCollection
			: System.Collections.ObjectModel.Collection<PictureObjectModel>
		{
		}

		private PositionVector2 lastAddedLocation = new PositionVector2(0, 0);

		public List<Color> ColorMap { get; } = new List<Color>();

		public void SetPixel(Color color)
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

			SetPixel(color, (int)lastAddedLocation.X, (int)lastAddedLocation.Y);
			lastAddedLocation.X++;
		}
		public void SetPixel(Color color, int x, int y)
		{
			if (x >= mvarWidth || y >= mvarHeight)
			{
				throw new InvalidOperationException("Out of image space. Try resizing the image");
			}

			int index = (x + (y * mvarWidth)) * 4;
			bitmapData[index] = (byte)(color.R * 255);
			bitmapData[index + 1] = (byte)(color.G * 255);
			bitmapData[index + 2] = (byte)(color.B * 255);
			bitmapData[index + 3] = (byte)(color.A * 255);

			int realIndex = (int)(index / 4);
			bitmapDataSet[realIndex] = true;
		}
		public void ClearPixel(int x, int y)
		{
			int index = (int)((lastAddedLocation.X + (lastAddedLocation.Y * mvarWidth)) * 4);
			bitmapData[index] = 0;
			bitmapData[index + 1] = 0;
			bitmapData[index + 2] = 0;
			bitmapData[index + 3] = 0;

			int realIndex = (int)(index / 4);
			bitmapDataSet[realIndex] = false;
		}
		public Color GetPixel(int x, int y)
		{
			if (x >= mvarWidth || y >= mvarHeight)
			{
				throw new InvalidOperationException("Out of image space. Try resizing the image");
			}

			int index = (x + (y * mvarWidth)) * 4;
			int realIndex = (int)(index / 4);
			if (bitmapDataSet[realIndex])
			{
				byte a = bitmapData[index + 0];
				byte r = bitmapData[index + 1];
				byte g = bitmapData[index + 2];
				byte b = bitmapData[index + 3];

				Color color = Color.FromRGBAByte(a, r, g, b);
				return color;
			}
			return Color.Empty;
		}
		public Color GetPixel(PositionVector2 point)
		{
			return GetPixel((int)point.X, (int)point.Y);
		}

		private byte[] bitmapData = new byte[] { 0, 0, 0, 0 };
		private bool[] bitmapDataSet = new bool[] { false };

		public PictureObjectModel()
		{
		}
		public PictureObjectModel(int width, int height)
		{
			bitmapData = new byte[(width * height) * 4];
			bitmapDataSet = new bool[(width * height) * 4];

			mvarWidth = width;
			mvarHeight = height;
		}

		private int mvarWidth = 1;
		public int Width
		{
			get { return mvarWidth; }
			set
			{
				mvarWidth = value;
				bitmapData = new byte[mvarWidth * mvarHeight * 4];
				bitmapDataSet = new bool[mvarWidth * mvarHeight];
			}
		}
		private int mvarHeight = 1;
		public int Height
		{
			get { return mvarHeight; }
			set
			{
				mvarHeight = value;
				bitmapData = new byte[mvarWidth * mvarHeight * 4];
				bitmapDataSet = new bool[mvarWidth * mvarHeight];
			}
		}


		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Picture";
			omr.Path = new string[] { "Multimedia", "Picture", "Single Picture" };
			return omr;
		}

		public override void Clear()
		{
			ColorMap.Clear();
			Width = 1;
			Height = 1;
		}
		public override void CopyTo(ObjectModel destination)
		{
			PictureObjectModel clone = (destination as PictureObjectModel);
			clone.Width = mvarWidth;
			clone.Height = mvarHeight;
			clone.bitmapData = (bitmapData.Clone() as byte[]);
			clone.bitmapDataSet = (bitmapDataSet.Clone() as bool[]);
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
						data[i + 3] = bitmapData[index + 3];
						data[i + 2] = bitmapData[index + 0];
						data[i + 1] = bitmapData[index + 1];
						data[i + 0] = bitmapData[index + 2];
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

					Color color = Color.FromRGBAByte(r, g, b, a);
					pic.SetPixel(color, w, h);

					i += 4;
				}
			}
			return pic;
		}
	}
}
