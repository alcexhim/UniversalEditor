//
//  KCFDataFormat.cs
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
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.KISS
{
	public class KCFDataFormat : DataFormat
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
			if (palette == null)
				throw new ObjectModelNotSupportedException();

			string signature = Accessor.Reader.ReadFixedLengthString(4);
			if (signature != "KiSS")
				throw new InvalidDataFormatException("file does not begin with 'KiSS'");

			byte formatCode = Accessor.Reader.ReadByte();
			byte pixelDepth = Accessor.Reader.ReadByte();
			if (formatCode != 0x10)
			{
				throw new InvalidDataFormatException("KCF palette file does not contain format code 0x10");
			}

			ushort unknown = Accessor.Reader.ReadUInt16(); // 8224
			ushort ncolors = Accessor.Reader.ReadUInt16();
			ushort npalettes = Accessor.Reader.ReadUInt16();

			Accessor.Reader.Seek(20, IO.SeekOrigin.Current); // unknown

			for (ushort i = 0; i < npalettes; i++)
			{
				for (ushort j = 0; j < ncolors; j++)
				{
					if (pixelDepth == 24)
					{
						byte r = Accessor.Reader.ReadByte();
						byte g = Accessor.Reader.ReadByte();
						byte b = Accessor.Reader.ReadByte();
						palette.Entries.Add(new PaletteEntry(Color.FromRGBAByte(r, g, b)));
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null)
				throw new ObjectModelNotSupportedException();

			Accessor.Writer.WriteFixedLengthString("KiSS");

			Accessor.Writer.WriteByte(0x10); // palette

			byte pixelDepth = 24;
			Accessor.Writer.WriteByte(pixelDepth); // only 24bit color support for now

			Accessor.Writer.WriteUInt16(0); // reserved
			Accessor.Writer.WriteUInt16((ushort)palette.Entries.Count);
			Accessor.Writer.WriteUInt16(1); // only 1 palette supported for now

			Accessor.Writer.WriteBytes(new byte[20]); // reserved

			for (ushort i = 0; i < 1; i++)
			{
				for (ushort j = 0; j < palette.Entries.Count; j++)
				{
					if (pixelDepth == 24)
					{
						Color color = palette.Entries[j].Color;
						Accessor.Writer.WriteByte(color.GetRedByte());
						Accessor.Writer.WriteByte(color.GetGreenByte());
						Accessor.Writer.WriteByte(color.GetBlueByte());
					}
				}
			}
		}
	}
}
