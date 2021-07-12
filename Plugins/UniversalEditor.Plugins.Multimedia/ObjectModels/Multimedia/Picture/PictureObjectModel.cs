//
//  PictureObjectModel.cs - provides an ObjectModel for manipulating image files
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

using System;
using System.Collections.Generic;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Picture
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating image files.
	/// </summary>
	public class PictureObjectModel : ObjectModel
	{
		public class PictureObjectModelCollection
			: System.Collections.ObjectModel.Collection<PictureObjectModel>
		{
		}

		private PositionVector2 lastAddedLocation = new PositionVector2(0, 0);

		public int Stride
		{
			get
			{
				// thanks https://stackoverflow.com/questions/2185944/why-must-stride-in-the-system-drawing-bitmap-constructor-be-a-multiple-of-4
				return GetStride(32);
			}
		}

		public List<Color> ColorMap { get; } = new List<Color>();

		/// <summary>
		/// Generates a color map as a <see cref="List{T}" /> of the <see cref="Color"/>s used in this <see cref="PictureObjectModel" />.
		/// </summary>
		/// <returns>The color map of colors used in this <see cref="PictureObjectModel" />.</returns>
		public List<Color> GenerateColorMap()
		{
			List<Color> colorMap = new List<MBS.Framework.Drawing.Color>();
			for (int x = 0; x < bitmapData.Length; x++)
			{
				for (int y = 0; y < bitmapData[x].Length; y++)
				{
					Color color = bitmapData[x][y];
					if (!colorMap.Contains(color))
						colorMap.Add(color);
				}
			}
			return colorMap;
		}

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
			bitmapData[x][y] = color;

			int realIndex = (int)(index / 4);
		}
		public void ClearPixel(int x, int y)
		{
			int index = (int)((lastAddedLocation.X + (lastAddedLocation.Y * mvarWidth)) * 4);
			bitmapData[x][y] = Color.Empty;

			int realIndex = (int)(index / 4);
		}
		public Color GetPixel(int x, int y)
		{
			if (x >= mvarWidth || y >= mvarHeight)
			{
				throw new InvalidOperationException("Out of image space. Try resizing the image");
			}

			int index = (x + (y * mvarWidth)) * 4;
			int realIndex = (int)(index / 4);
			if (!bitmapData[x][y].IsEmpty)
			{
				Color color = bitmapData[x][y];
				return color;
			}
			return Color.Empty;
		}
		public Color GetPixel(PositionVector2 point)
		{
			return GetPixel((int)point.X, (int)point.Y);
		}

		private Color[][] bitmapData = new Color[0][];

		public PictureObjectModel()
		{
		}
		public PictureObjectModel(int width, int height)
		{
			InitializeBitmapData();

			mvarWidth = width;
			mvarHeight = height;
		}

		private static void InitializeBitmapData(ref Color[][] array, int width, int height)
		{
			array = new Color[width][];
			for (int i = 0; i < width; i++)
			{
				array[i] = new Color[height];
			}
		}
		private void InitializeBitmapData()
		{
			InitializeBitmapData(ref bitmapData, Width, Height);
		}

		private int mvarWidth = 1;
		public int Width
		{
			get { return mvarWidth; }
			set
			{
				mvarWidth = value;
				InitializeBitmapData();
			}
		}
		private int mvarHeight = 1;
		public int Height
		{
			get { return mvarHeight; }
			set
			{
				mvarHeight = value;
				InitializeBitmapData();
			}
		}

		public Dimension2D Size
		{
			get { return new Dimension2D(mvarWidth, mvarHeight); }
			set
			{
				mvarWidth = (int)value.Width;
				mvarHeight = (int)value.Height;

				InitializeBitmapData();
			}
		}


		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = new ObjectModelReference(GetType(), new Guid("{4ae4c9ac-d8ab-4f7d-8d45-5e7fb8c475cc}"));
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
			clone.bitmapData = (bitmapData.Clone() as Color[][]);
		}

		public byte[] ToByteArray(PixelFormat format)
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

					switch (format)
					{
						case PixelFormat.BGRA:
						{
							// BGRA layout
							data[i + 0] = bitmapData[w][h].GetBlueByte();
							data[i + 1] = bitmapData[w][h].GetGreenByte();
							data[i + 2] = bitmapData[w][h].GetRedByte();
							data[i + 3] = bitmapData[w][h].GetAlphaByte();

							i += 4;
							break;
						}
						case PixelFormat.RGBA:
						{
							// RGBA layout
							data[i + 0] = bitmapData[w][h].GetRedByte();
							data[i + 1] = bitmapData[w][h].GetGreenByte();
							data[i + 2] = bitmapData[w][h].GetBlueByte();
							data[i + 3] = bitmapData[w][h].GetAlphaByte();

							i += 4;
							break;
						}
					}
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

		public int GetStride(int bitsPerPixel)
		{
			// this calculation is CORRECT (at least on gdkpixbuf) so ***DON'T TOUCH IT***
			int bytesPerPixel = (int)((double)bitsPerPixel / 8); // (bitsPerPixel + 7) / 8; // wtf???
			int stride = 4 * ((Width * bytesPerPixel + 3) / 4);
			return stride;
		}

		public void Rotate(int degrees)
		{
			int oldheight = Height;
			int oldwidth = Width;

			// fuck this ain't workin
			if (degrees % 360 == 0)
			{
				return;
			}
			switch (degrees)
			{
				case -90:
				{
					Color[][] oldpixels = bitmapData;
					Size = new Dimension2D(Height, Width);

					Color[][] pixels = null;
					InitializeBitmapData(ref pixels, Width, Height);

					// 1 2 3
					// 4 5 6

					// =>

					// 3 6
					// 2 5
					// 1 4

					int x1 = 0, y1 = 0;
					for (int y = 0; y < oldheight; y++)
					{
						for (int x = oldwidth - 1; x >= 0; x--)
						{
							pixels[x1][y1] = oldpixels[x][y];
							y1++;
						}
						y1 = 0;
						x1++;
					}

					bitmapData = pixels;
					break;
				}
				case 90:
				{
					Color[][] oldpixels = bitmapData;
					Size = new Dimension2D(Height, Width);

					Color[][] pixels = null;
					InitializeBitmapData(ref pixels, Width, Height);

					// 1 2 3
					// 4 5 6

					// =>

					// 3 6
					// 2 5
					// 1 4

					int x1 = 0, y1 = 0;
					for (int y = 0; y < oldheight; y++)
					{
						for (int x = oldwidth - 1; x >= 0; x--)
						{
							pixels[x1][y1] = oldpixels[x][y];
							y1++;
						}
						y1 = 0;
						x1++;
					}

					bitmapData = pixels;
					break;
				}
				case 180:
				{

					break;
				}
				case 270:
				{
					break;
				}
			}
		}
	}
}
