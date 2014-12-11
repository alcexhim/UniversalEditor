using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.BMP
{
    public class BMPDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic BMP sprite", new string[] { "*.bmp" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureObjectModel pic = (objectModel as PictureObjectModel);
            if (pic == null) return;

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;

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
                    Color color = HoMM2Palette.ColorTable[colorIndex];
                    if (colorIndex == 1)
                    {
                        color = Colors.White;
                    }
                    else if (colorIndex == 0)
                    {
                        color = Colors.Transparent;
                    }
                    else if (colorIndex == 2)
                    {
                        color = Colors.Black;
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

        }
    }
}
