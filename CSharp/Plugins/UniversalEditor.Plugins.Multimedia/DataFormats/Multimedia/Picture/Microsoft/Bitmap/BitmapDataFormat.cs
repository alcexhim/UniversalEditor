using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for loading and saving Windows and OS/2 Bitmap documents.
	/// Supports bitmap formats that are not natively supported by Windows.
	/// </summary>
	public class BitmapDataFormat : DataFormat
	{
		public const int BITMAP_HEADER_SIZE = (2 + 6 * 4 + 2 * 2 + 6 * 4);
		public const int BITMAP_PALETTE_ENTRY_SIZE = 4;

		private BitmapBitsPerPixel mvarPixelDepth = BitmapBitsPerPixel.TrueColor;
		/// <summary>
		/// The number of bits-per-pixel. The biBitCount member of the BITMAPINFOHEADER structure determines the
		/// number of bits that define each pixel and the maximum number of colors in the bitmap.
		/// </summary>
		public BitmapBitsPerPixel PixelDepth { get { return mvarPixelDepth; } set { mvarPixelDepth = value; } }

		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			
			dfr.Filters.Add("Microsoft Windows and OS/2 bitmap", new byte?[][]
			{
				new byte?[] { (byte)'B', (byte)'M' }, // Windows 3.1x, 95, NT, ... etc.; and it is not mandatory unless file size is greater or equal to SIGNATURE
				new byte?[] { (byte)'B', (byte)'A' }, // OS/2 struct Bitmap Array
				new byte?[] { (byte)'C', (byte)'I' }, // OS/2 struct Color Icon
				// new byte?[] { (byte)'C', (byte)'P' }, // OS/2 const Color Pointer
				new byte?[] { (byte)'I', (byte)'C' }, // OS/2 struct Icon
				new byte?[] { (byte)'P', (byte)'T' } // OS/2 Pointer
			}, new string[] { "*.bmp", "*.spa", "*.sph" });

			// TODO: Figure out how to prevent this from colliding with CPK files that start with "CP" ("CPK")
			// dfr.Filters[0].HintComparison = DataFormatHintComparison.FilterOnly;
			// TODO: Sort list of data formats by length of magic byte requirement during the sniffing process? ;)

			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) throw new ObjectModelNotSupportedException();

			IO.BinaryReader br = base.Stream.BinaryReader;
			
			string signature = br.ReadFixedLengthString(2);
			switch (signature)
			{
				case "BM":
				case "BA":
				case "CI":
				case "CP":
				case "IC":
				case "PT":
				{
					break;
				}
				default:
				{
					throw new InvalidDataFormatException();
				}
			}

			int fileSize = br.ReadInt32();
			short reserved1 = br.ReadInt16();
			short reserved2 = br.ReadInt16();
			int offset = br.ReadInt32();

			BitmapInfoHeader header = BitmapInfoHeader.Load(br);
			mvarPixelDepth = header.PixelDepth;

			pic.Width = header.Width;
			pic.Height = header.Height;

			byte bitRead = 0;
			int bitsRead = 0;

			List<Color> palette = new List<Color>();

			// there is a palette
			// To read the palette, we can simply read in a block of bytes
			// since our array elements are guaranteed to be contiguous and in row-major order.
			int paletteSize = offset - header.HeaderSize;
			int numPaletteEntries = paletteSize / BITMAP_PALETTE_ENTRY_SIZE;
			for (int i = 0; i < numPaletteEntries; i++)
			{
				byte b = br.ReadByte();
				byte g = br.ReadByte();
				byte r = br.ReadByte();
				byte a = br.ReadByte();
				palette.Add(Color.FromRGBA(r, g, b, a));
			}

			if (mvarPixelDepth == BitmapBitsPerPixel.TrueColor)
			{
			}
			br.BaseStream.Position = offset;

			// starts from bottom-left corner, goes BGR
			for (int y = header.Height - 1; y >= 0; y--)
			{
				for (int x = 0; x < header.Width; x++)
				{
					byte r = 0, g = 0, b = 0, a = 255;

					switch (mvarPixelDepth)
					{
						case BitmapBitsPerPixel.Monochrome:
						{
							// TODO: Figure out how to read this bitmap
							if (bitsRead == 0) bitRead = br.ReadByte();

							b = (byte)(bitRead << (bitsRead));
							g = (byte)(bitRead << (bitsRead + 1));
							r = (byte)(bitRead << (bitsRead + 2));

							bitsRead += 3;
							if (bitsRead == 8)
							{
								bitsRead = 0;
							}
							break;
						}
						case BitmapBitsPerPixel.Color16:
						{
							break;
						}
						case BitmapBitsPerPixel.Color256:
						{
							byte rgb = br.ReadByte();
							r = (byte)(palette[rgb].Red * 255);
							g = (byte)(palette[rgb].Green * 255);
							b = (byte)(palette[rgb].Blue * 255);
							break;
						}
						case BitmapBitsPerPixel.HighColor:
						{
							// X1R5G5B5
							short value = br.ReadInt16();
							int x1 = value.GetBits(16, 1);
							b = (byte)(8 * value.GetBits(0, 5));
							g = (byte)(8 * value.GetBits(5, 5));
							r = (byte)(8 * value.GetBits(10, 5));
							break;
						}
						case BitmapBitsPerPixel.TrueColor:
						{
							b = br.ReadByte(); // (2,2) B 204
							g = br.ReadByte(); // (2,2) G 72
							r = br.ReadByte(); // (2,2) R 63
							break;
						}
						case BitmapBitsPerPixel.DeepColor:
						{
							if (header.Compression == BitmapCompression.Bitfields)
							{
								// this is really black magic going on here. these aren't bitfields at all..

								a = br.ReadByte(); // (2,2) R 63
								b = br.ReadByte(); // (2,2) B 204
								g = br.ReadByte(); // (2,2) B 204
								r = br.ReadByte(); // (2,2) G 72
								a = 255;
							}
							else
							{
								b = br.ReadByte(); // (2,2) R 63
								g = br.ReadByte(); // (2,2) B 204
								r = br.ReadByte(); // (2,2) B 204
								a = br.ReadByte(); // (2,2) G 72
								a = 255;
							}
							break;
						}
					}
					
					Color color = Color.FromRGBA(r, g, b, a);
					pic.SetPixel(color, x, y);
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			IO.BinaryWriter bw = base.Stream.BinaryWriter;

			string signature = "BM";
			bw.WriteFixedLengthString(signature);

			int fileSize = 54 + (pic.Width * pic.Height * 4);
			bw.Write(fileSize);

			short reserved1 = 0;
			short reserved2 = 0;
			bw.Write(reserved1);
			bw.Write(reserved2);

			int offset = 54;
			bw.Write(offset);

			BitmapInfoHeader header = new BitmapInfoHeader();
			header.Width = pic.Width;
			header.Height = pic.Height;
			header.Planes = 1;
			header.PixelDepth = mvarPixelDepth;
			header.Compression = BitmapCompression.None;
			header.ImageSize = 0;
			header.PelsPerMeterX = 0;
			header.PelsPerMeterY = 0;
			header.UsedColorIndexCount = 0;
			header.RequiredColorIndexCount = 0;
			BitmapInfoHeader.Save(bw, header);

			for (int y = pic.Height - 1; y >= 0; y--)
			{
				for (int x = 0; x < pic.Width; x++)
				{
					Color color = pic.GetPixel(x, y);
					byte r = (byte)(color.Red * 255), g = (byte)(color.Green * 255), b = (byte)(color.Blue * 255);

					/*
					if (bitsPerPixel == 1)
					{
						// TODO: Figure out how to read this bitmap
						if (bitsRead == 0) bitRead = br.ReadByte();

						b = (byte)(bitRead << (bitsRead));
						g = (byte)(bitRead << (bitsRead + 1));
						r = (byte)(bitRead << (bitsRead + 2));

						bitsRead += 3;
						if (bitsRead == 8)
						{
							bitsRead = 0;
						}
					}
					else */ 
					
					switch (mvarPixelDepth)
					{
						case BitmapBitsPerPixel.TrueColor:
						{
							bw.Write(b);
							bw.Write(g);
							bw.Write(r);
							break;
						}
					}
				}
			}
			
			bw.Flush();
		}
	}
}
