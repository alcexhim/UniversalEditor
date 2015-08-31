using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Bitmap
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for loading and saving Windows and OS/2 Bitmap documents.
	/// Supports bitmap formats that are not natively supported by Windows.
	/// </summary>
    public class BitmapDataFormat : DataFormat
    {
		public const int BITMAP_HEADER_SIZE = (2 + 6 * 4 + 2 * 2 + 6 * 4);
		public const int BITMAP_PALETTE_ENTRY_SIZE = 4;

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

            return dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureObjectModel pic = (objectModel as PictureObjectModel);
            IO.BinaryReader br = base.Stream.BinaryReader;
            
            string signature = br.ReadFixedLengthString(2);
			int fileSize = br.ReadInt32();
            short reserved1 = br.ReadInt16();
            short reserved2 = br.ReadInt16();
            int offset = br.ReadInt32();

			BitmapInfoHeader header = BitmapInfoHeader.Load(br);

			pic.Width = header.Width;
            pic.Height = header.Height;

            byte bitRead = 0;
            int bitsRead = 0;

			List<byte[]> palette = new List<byte[]>();

			// there is a palette
			// To read the palette, we can simply read in a block of bytes
			// since our array elements are guaranteed to be contiguous and in row-major order.
			int paletteSize = offset - BITMAP_HEADER_SIZE;
			int numPaletteEntries = paletteSize / BITMAP_PALETTE_ENTRY_SIZE;
			for (int i = 0; i < numPaletteEntries; i++)
			{
				byte[] paletteData = br.ReadBytes(BITMAP_PALETTE_ENTRY_SIZE);
				palette.Add(paletteData);
			}

            // starts from bottom-left corner, goes BGR
            for (int y = header.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < header.Width; x++)
                {
                    byte r = 0, g = 0, b = 0, a = 255;

					switch (header.PixelDepth)
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
							r = palette[rgb][0];
							g = palette[rgb][1];
							b = palette[rgb][2];
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
							a = br.ReadByte(); // (2,2) R 63
							b = br.ReadByte(); // (2,2) B 204
							g = br.ReadByte(); // (2,2) B 204
							r = br.ReadByte(); // (2,2) G 72
							a = 255;
							break;
						}
					}
                    
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
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
			header.PixelDepth = BitmapBitsPerPixel.TrueColor;
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
                    System.Drawing.Color color = pic.GetPixel(x, y);
                    byte r = (byte)color.R, g = (byte)color.G, b = (byte)color.B;

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
					
					switch (header.PixelDepth)
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
