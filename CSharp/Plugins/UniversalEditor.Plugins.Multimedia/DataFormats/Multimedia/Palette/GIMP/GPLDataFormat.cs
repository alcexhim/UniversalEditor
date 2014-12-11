using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.GIMP
{
    public class GPLDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("GIMP color palette", new byte?[][] { new byte?[] { (byte)'G', (byte)'I', (byte)'M', (byte)'P', (byte)' ', (byte)'P', (byte)'a', (byte)'l', (byte)'e', (byte)'t', (byte)'t', (byte)'e' } }, new string[] { "*.gpl" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PaletteObjectModel palette = (objectModel as PaletteObjectModel);
            if (palette == null) return;

            IO.Reader tr = base.Accessor.Reader;
            bool headerRead = false;
            while (!tr.EndOfStream)
            {
                string line = tr.ReadLine();
                if (line.Contains("#")) line = line.Substring(0, line.IndexOf("#"));
                line = line.Trim();
                if (line == String.Empty) continue;

                if (line == "GIMP Palette")
                {
                    headerRead = true;
                    continue;
                }
                else if (!headerRead)
                {
                    throw new InvalidDataFormatException("File does not begin with \"GIMP Palette\"");
                }
                else
                {
                    string[] colors = line.Split(new char[] { ' ' }, 4, StringSplitOptions.RemoveEmptyEntries);
                    if (colors.Length >= 3)
                    {
                        string colorName = String.Empty;
                        int r = Int32.Parse(colors[0]);
                        int g = Int32.Parse(colors[1]);
                        int b = Int32.Parse(colors[2]);
                        if (colors.Length >= 4)
                        {
                            colorName = colors[3];
                        }

                        Color color = Color.FromRGBA(r, g, b);
                        palette.Entries.Add(color, colorName);
                    }
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
