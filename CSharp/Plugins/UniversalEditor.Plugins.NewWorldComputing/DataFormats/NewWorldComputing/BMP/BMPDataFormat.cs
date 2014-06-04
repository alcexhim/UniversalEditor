using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.NewWorldComputing.BMP
{
    public class BMPDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic BMP sprite", new string[] { "*.bmp" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureObjectModel pic = (objectModel as PictureObjectModel);
            if (pic == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            br.BaseStream.Position = 0;

            ushort unknown = br.ReadUInt16();
            ushort width = br.ReadUInt16();
            ushort height = br.ReadUInt16();

            pic.Width = width;
            pic.Height = height;

            for (ushort y = 0; y < height; y++)
            {
                for (ushort x = 0; x < width; x++)
                {
                    byte colorIndex = br.ReadByte();
                    System.Drawing.Color color = HoMM2Palette.ColorTable[colorIndex];
                    if (colorIndex == 1)
                    {
                        color = System.Drawing.Color.White;
                    }
                    else if (colorIndex == 0)
                    {
                        color = System.Drawing.Color.Transparent;
                    }
                    else if (colorIndex == 2)
                    {
                        color = System.Drawing.Color.Black;
                    }
                    else // if (colorIndex == 3)
                    {

                    }

                    pic.SetPixel(color, x, y);
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
