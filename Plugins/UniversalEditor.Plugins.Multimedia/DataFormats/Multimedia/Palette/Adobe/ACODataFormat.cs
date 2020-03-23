using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.Adobe
{
    public class ACODataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
                _dfr.Sources.Add("http://www.nomodes.com/aco.html");
                _dfr.ExportOptions.Add(new CustomOptionNumber(nameof(Version), "&Version:", 1, 1, ushort.MaxValue));
            }
            return _dfr;
        }

        private ushort mvarVersion = 1;
        public ushort Version { get { return mvarVersion; } set { mvarVersion = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PaletteObjectModel palette = (objectModel as PaletteObjectModel);
            IO.Reader br = base.Accessor.Reader;
            br.Endianness = IO.Endianness.BigEndian;

            // The palette begins with a two-word header:
            mvarVersion = br.ReadUInt16();
            ushort colorCount = br.ReadUInt16();

            // The header is followed by nc color specs. In a version 1 ACO file, each color spec
            // occupies five words:
            for (ushort i = 0; i < colorCount; i++)
            {
                ushort colorSpace = br.ReadUInt16();
                ushort w = br.ReadUInt16();
                ushort x = br.ReadUInt16();
                ushort y = br.ReadUInt16();
                ushort z = br.ReadUInt16();

                string colorName = String.Empty;
                if (mvarVersion >= 2)
                {
                    ushort colorNameLength = br.ReadUInt16();
                    colorName = br.ReadNullTerminatedString();
                }

                switch (colorSpace)
                {
                    case 0:
                    {
                        // RGB
                        int R = (w / 256);
                        int G = (x / 256);
                        int B = (y / 256);

                        PaletteEntry entry = new PaletteEntry();
                        entry.Color = Color.FromRGBAInt32(R, G, B);
                        palette.Entries.Add(entry);
                        break;
                    }
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            PaletteObjectModel palette = (objectModel as PaletteObjectModel);
            IO.Writer bw = base.Accessor.Writer;
            bw.Endianness = IO.Endianness.BigEndian;

            // The palette begins with a two-word header:
            bw.WriteUInt16(mvarVersion);
            ushort colorCount = (ushort)palette.Entries.Count;
            bw.WriteUInt16(colorCount);

            // The header is followed by nc color specs. In a version 1 ACO file, each color spec
            // occupies five words:
            foreach (PaletteEntry color in palette.Entries)
            {
                ushort colorSpace = 0; // RGB
                bw.WriteUInt16(colorSpace);

                switch (colorSpace)
                {
                    case 0:
                    {
                        ushort w = (ushort)((color.Color.R * 255) * 256);
                        ushort x = (ushort)((color.Color.G * 255) * 256);
                        ushort y = (ushort)((color.Color.B * 255) * 256);
                        ushort z = 0;

                        bw.WriteUInt16(w);
                        bw.WriteUInt16(x);
                        bw.WriteUInt16(y);
                        bw.WriteUInt16(z);
                        break;
                    }
                }

                if (mvarVersion >= 2)
                {
                    bw.WriteUInt16((ushort)color.Name.Length);
                    bw.WriteNullTerminatedString(color.Name);
                }
            }
        }
    }
}
