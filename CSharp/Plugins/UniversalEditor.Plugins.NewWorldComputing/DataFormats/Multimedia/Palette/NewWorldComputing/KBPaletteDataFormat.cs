using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.NewWorldComputing
{
	public class KBPaletteDataFormat : DataFormat
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

			IO.Reader br = base.Accessor.Reader;
			if (br.Accessor.Length != 768) throw new InvalidDataFormatException("Expected a 768-byte palette");

			for (int i = 0; i < (768 / 3); i++)
			{
				byte r = br.ReadByte();
				byte g = br.ReadByte();
				byte b = br.ReadByte();

				r <<= 2;
				g <<= 2;
				b <<= 2;

				Color color = Color.FromRGBA(r, g, b);
				palette.Entries.Add(color);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			for (int i = 0; i < (768 / 3); i++)
			{
				Color color = palette.Entries[i].Color;
				byte r = (byte)color.Red;
				byte g = (byte)color.Green;
				byte b = (byte)color.Blue;

				r >>= 2;
				g >>= 2;
				b >>= 2;

				writer.WriteBytes(new byte[] { r, g, b });
			}
		}
	}
}
