//
//  KBPaletteDataFormat.cs - provides a DataFormat for manipulating color palettes in Heroes of Might and Magic II KB.PAL format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Palette.NewWorldComputing
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating color palettes in Heroes of Might and Magic II KB.PAL format.
	/// </summary>
	public class KBPaletteDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
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

				Color color = Color.FromRGBAByte(r, g, b);
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
				byte r = (byte)color.R;
				byte g = (byte)color.G;
				byte b = (byte)color.B;

				r >>= 2;
				g >>= 2;
				b >>= 2;

				writer.WriteBytes(new byte[] { r, g, b });
			}
		}
	}
}
