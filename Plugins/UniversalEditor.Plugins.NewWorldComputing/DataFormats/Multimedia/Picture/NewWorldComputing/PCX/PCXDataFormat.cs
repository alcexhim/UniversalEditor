//
//  PCXDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.PCX
{
	public class PCXDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionFile("PaletteFileName", "External _palette", "Palette|*.pal"));
			}
			return _dfr;
		}

		public string PaletteFileName { get; set; } = null;
		public PaletteObjectModel Palette { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			if (!String.IsNullOrEmpty(PaletteFileName) && System.IO.File.Exists(PaletteFileName))
			{
				Palette = Common.Reflection.GetAvailableObjectModel<PaletteObjectModel>(PaletteFileName);
			}

			uint dataLength = reader.ReadUInt32();
			uint width = reader.ReadUInt32();
			uint height = reader.ReadUInt32();
			if (dataLength != (width * height))
			{
				// TODO: sanity check?
			}
				
			pic.Width = (int)width;
			pic.Height = (int)height;

			byte[] indices = reader.ReadBytes(width * height);

			if (Palette == null)
				Palette = new PaletteObjectModel();

			if (!reader.EndOfStream)
			{
				// byte nul = reader.ReadByte();
				// if (nul != 0) return; //eeee?

				for (int i = 0; i < 256; i++)
				{
					byte r = reader.ReadByte();
					byte g = reader.ReadByte();
					byte b = reader.ReadByte();
					Palette.Entries.Add(new PaletteEntry(MBS.Framework.Drawing.Color.FromRGBAByte(r, g, b)));
				}
			}

			int x = 0, y = 0;
			for (int i = 0; i < indices.Length; i++)
			{
				byte index = indices[i];
				pic.SetPixel(Palette.Entries[index].Color, x, y);

				x++;
				if (x >= pic.Width)
				{
					reader.Align(4);

					x = 0;
					y++;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
