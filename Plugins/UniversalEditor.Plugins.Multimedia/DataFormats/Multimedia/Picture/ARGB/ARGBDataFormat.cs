using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.ARGB
{
    public class ARGBDataFormat : DataFormat
    {
        private DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) return;

            IO.Reader br = base.Accessor.Reader;
            string signature = br.ReadFixedLengthString(4);
            if (signature != "BGRA") throw new InvalidDataFormatException("File does not begin with \"BGRA\"");

            int unknown = br.ReadInt32();
            int imageWidth = br.ReadInt32();
            int imageHeight = br.ReadInt32();
            pic.Width = imageWidth;
            pic.Height = imageHeight;

            for (short x = 0; x < imageWidth; x++)
            {
                for (short y = 0; y < imageHeight; y++)
                {
                    byte a = br.ReadByte();
                    byte r = br.ReadByte();
                    byte g = br.ReadByte();
                    byte b = br.ReadByte();

                    Color color = Color.FromRGBAByte(r, g, b, a);
                    pic.SetPixel(color, x, y);
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("BGRA");

			int unknown = 0;
			bw.WriteInt32(unknown);
			bw.WriteInt32(pic.Width);
			bw.WriteInt32(pic.Height);

			for (int x = 0; x < pic.Width; x++)
			{
				for (int y = 0; y < pic.Height; y++)
				{
					Color color = pic.GetPixel(x, y);
					bw.WriteByte((byte)(color.A * 255));
					bw.WriteByte((byte)(color.R * 255));
					bw.WriteByte((byte)(color.G * 255));
					bw.WriteByte((byte)(color.B * 255));
				}
			}
        }
    }
}
