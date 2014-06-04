using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.NewWorldComputing
{
	public class KBPaletteDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("New World Computing palette", new string[] { "*.pal" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			IO.BinaryReader br = base.Stream.BinaryReader;
			if (br.BaseStream.Length != 768) throw new InvalidDataFormatException("Expected a 768-byte palette");

			for (int i = 0; i < (768 / 3); i++)
			{
				byte r = br.ReadByte();
				byte g = br.ReadByte();
				byte b = br.ReadByte();

				r <<= 2;
				g <<= 2;
				b <<= 2;

				Color color = Color.FromArgb(r, g, b);
				palette.Entries.Add(color);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
