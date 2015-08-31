using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture
{
    public class BitmapDataFormat : DataFormat
    {
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
            int reserved3 = br.ReadInt32(); // 40
            int width = br.ReadInt32();
            int height = br.ReadInt32();

            short reserved4 = br.ReadInt16(); // 1
            short bitsPerPixel = br.ReadInt16(); // 24 for 24-bit bitmap
            int reserved6 = br.ReadInt32();         // 8 for 256-color bitmap
            int reserved7 = br.ReadInt32();
            int reserved8 = br.ReadInt32();
            int reserved9 = br.ReadInt32();
            int reserved10 = br.ReadInt32();
            int reserved11 = br.ReadInt32();

            pic.Width = width;
            pic.Height = height;

            byte bitRead = 0;
            int bitsRead = 0;

            // starts from bottom-left corner, goes BGR
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = 0, g = 0, b = 0;

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
                    else if (bitsPerPixel == 24)
                    {
                        b = br.ReadByte(); // (2,2) B 204
                        g = br.ReadByte(); // (2,2) G 72
                        r = br.ReadByte(); // (2,2) R 63
                    }
                    
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);
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

            int reserved3 = 40; // 40
            bw.Write(reserved3);

            bw.Write(pic.Width);
            bw.Write(pic.Height);

            short reserved4 = 1; // 1
            bw.Write(reserved4);

            short bitsPerPixel = 24; // 24 for 24-bit bitmap
            bw.Write(bitsPerPixel);  // 8 for 256-color bitmap

            int reserved6 = 0;
            bw.Write(reserved6);
            int reserved7 = 0;
            bw.Write(reserved6);
            int reserved8 = 0;
            bw.Write(reserved6);
            int reserved9 = 0;
            bw.Write(reserved6);
            int reserved10 = 0;
            bw.Write(reserved6);
            int reserved11 = 0;
            bw.Write(reserved6);

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
                    else */ if (bitsPerPixel == 24)
                    {
                        bw.Write(b);
                        bw.Write(g);
                        bw.Write(r);
                    }
                }
            }
            
            bw.Flush();
        }
    }
}
