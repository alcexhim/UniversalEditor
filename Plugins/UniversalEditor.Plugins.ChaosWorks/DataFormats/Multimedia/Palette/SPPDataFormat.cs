//
//  SPPDataFormat.cs - provides a DataFormat for manipulating a color palette in Chaos Works SPP format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using MBS.Framework.Drawing;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.Plugins.ChaosWorks.DataFormats.Multimedia.Palette
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating a color palette in Chaos Works SPP format.
	/// </summary>
	public class SPPDataFormat : DataFormat
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
			if (palette == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			uint version = reader.ReadUInt32();

			while (!reader.EndOfStream)
			{
				byte r = reader.ReadByte();
				byte g = reader.ReadByte();
				byte b = reader.ReadByte();
				byte a = reader.ReadByte();
				a = (byte)(255 - a); // always 0 ?

				palette.Entries.Add(new PaletteEntry(Color.FromRGBAByte(r, g, b, a)));
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteUInt32(1);

			for (int i = 0; i < palette.Entries.Count; i++)
			{
				writer.WriteByte(palette.Entries[i].Color.GetRedByte());
				writer.WriteByte(palette.Entries[i].Color.GetGreenByte());
				writer.WriteByte(palette.Entries[i].Color.GetBlueByte());
				writer.WriteByte(palette.Entries[i].Color.GetAlphaByte());
			}
		}
	}
}
