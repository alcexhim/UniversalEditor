using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.PaintShop
{
    public class PaintShopPaletteDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("JASC Paint Shop color palette", new byte?[][] { new byte?[] { (byte)'J', (byte)'A', (byte)'S', (byte)'C', (byte)'-', (byte)'P', (byte)'A', (byte)'L' } }, new string[] { "*.pal" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PaletteObjectModel palette = (objectModel as PaletteObjectModel);
            if (palette == null) return;

            IO.Reader tr = base.Accessor.Reader;
            string signature = tr.ReadLine();
            if (signature != "JASC-PAL") throw new InvalidDataFormatException("File does not begin with \"JASC-PAL\"");

            string unknown = tr.ReadLine();
            string sColorCount = tr.ReadLine();
            int iColorCount = Int32.Parse(sColorCount);

            while (!tr.EndOfStream)
            {
                string colorLine = tr.ReadLine();
                string[] colorInfo = colorLine.Split(new char[] { ' ' });
                if (colorInfo.Length < 3) continue;

                string sR = colorInfo[0], sG = colorInfo[1], sB = colorInfo[2];
                int iR = Int32.Parse(sR), iG = Int32.Parse(sG), iB = Int32.Parse(sB);

                Color color = Color.FromRGBA(iR, iG, iB);
                palette.Entries.Add(color);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
