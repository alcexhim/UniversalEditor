//
//  UnrealPaletteDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using System;
using MBS.Framework.Drawing;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.Unreal
{
	public class UnrealPaletteDataFormat : DataFormat
	{
		public UnrealPaletteDataFormat()
		{
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			ushort signature = reader.ReadUInt16();

			while (!reader.EndOfStream)
			{
				byte r = reader.ReadByte();
				byte g = reader.ReadByte();
				byte b = reader.ReadByte();
				byte a = reader.ReadByte();
				palette.Entries.Add(Color.FromRGBAByte(r, g, b, a));
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteUInt16(0x4001);

			for (int i = 0; i < Math.Min(palette.Entries.Count, 256); i++)
			{
				writer.WriteByte(palette.Entries[i].Color.GetRedByte());
				writer.WriteByte(palette.Entries[i].Color.GetGreenByte());
				writer.WriteByte(palette.Entries[i].Color.GetBlueByte());
				writer.WriteByte(palette.Entries[i].Color.GetAlphaByte());
			}

			int remaining = (256 - palette.Entries.Count);
			if (remaining > 0)
			{
				for (int i = 0; i < remaining; i++)
				{
					writer.WriteByte(0);
					writer.WriteByte(0);
					writer.WriteByte(0);
					writer.WriteByte(0);
				}
			}
		}
	}
}
