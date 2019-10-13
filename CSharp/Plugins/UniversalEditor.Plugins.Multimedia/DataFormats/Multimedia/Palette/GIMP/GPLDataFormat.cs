using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.IO;
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
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PaletteObjectModel palette = (objectModel as PaletteObjectModel);
            if (palette == null) throw new ObjectModelNotSupportedException();

            Reader tr = base.Accessor.Reader;
            bool headerRead = false;
            while (!tr.EndOfStream)
            {
                string line = tr.ReadLine();
				if (line.Contains(":"))
				{
					string[] parms = line.Split(new char[] { ':' }, 2);

					string name = parms[0].Trim();
					string value = String.Empty;
					if (parms.Length > 1)
						value = parms[1].Trim();

					if (name == "Name")
					{
						palette.Name = value;
					}
					continue;
				}
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
                    string[] colors = line.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                    if (colors.Length >= 3)
                    {
                        string colorName = String.Empty;
                        int r = Int32.Parse(colors[0]);
                        int g = Int32.Parse(colors[1]);

						if (colors[2].Contains("\t"))
						{
							string[] w = colors[2].Split(new char[] { '\t' });
							colors[2] = w[0];
							colorName = w[1];
						}
                        int b = Int32.Parse(colors[2]);
                        if (colors.Length >= 4)
                        {
                            colorName = colors[3];
                        }

                        Color color = Color.FromRGBAInt32(r, g, b);
                        palette.Entries.Add(color, colorName);
                    }
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteLine("GIMP Palette");
			writer.WriteLine(String.Format("Name: {0}", palette.Name));
			writer.WriteLine("#");

			foreach (PaletteEntry entry in palette.Entries)
			{
				writer.WriteLine(String.Format("{0} {1} {2}\t{3}", entry.Color.GetRedByte().ToString(), entry.Color.GetGreenByte().ToString(), entry.Color.GetBlueByte().ToString(), entry.Name));
			}
        }
    }
}
