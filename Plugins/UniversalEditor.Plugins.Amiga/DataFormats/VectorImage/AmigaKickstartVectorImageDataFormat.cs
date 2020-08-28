//
//  AmigaKickstartPicture.cs
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
using MBS.Framework.Drawing;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems;

namespace UniversalEditor.Plugins.Amiga.DataFormats.VectorImage
{
	public class AmigaKickstartVectorImageDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VectorImageObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public AmigaKickstartVectorImageDataFormat()
		{
			PaletteObjectModel palAmiga = new PaletteObjectModel(new PaletteEntry[]
			{
				new PaletteEntry(Color.FromRGBAByte(0xFF, 0xFF, 0xFF)),
				new PaletteEntry(Color.FromRGBAByte(0x00, 0x00, 0x00)),
				new PaletteEntry(Color.FromRGBAByte(0x77, 0x77, 0xCC)),
				new PaletteEntry(Color.FromRGBAByte(0xBB, 0xBB, 0xBB))
			});
			Palette = palAmiga;
		}

		public PaletteObjectModel Palette { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VectorImageObjectModel vec = (objectModel as VectorImageObjectModel);
			if (vec == null) throw new ObjectModelNotSupportedException();

			// https://retrocomputing.stackexchange.com/questions/13897/why-was-the-kickstart-1-x-insert-floppy-graphic-so-bad/13940
			Reader reader = Accessor.Reader;

			AmigaKickstartOpcode opcode = AmigaKickstartOpcode.None;
			byte colorIndex = 0x00;

			PolygonVectorItem polyline = null;

			while (!reader.EndOfStream)
			{
				byte b1 = reader.ReadByte();
				byte b2 = reader.ReadByte();

				if (b1 == 0xFF)
				{
					if (b2 == 0xFF) break;

					if (opcode == AmigaKickstartOpcode.Polyline || opcode == AmigaKickstartOpcode.Fill)
					{
						// we are done drawing the polyline
						vec.Items.Add(polyline);
						polyline = null;
					}

					// start drawing a polyline with the color index given in the second byte. Treat any subsequent two bytes as x,y coordinates belonging to that polyline
					// except if the first byte is FF (see rules 2 and 3) or FE (see rule 4), which is where you stop drawing the line.
					opcode = AmigaKickstartOpcode.Polyline;
					colorIndex = b2;
					polyline = new PolygonVectorItem();
					polyline.Style.BorderColor = Palette.Entries[colorIndex].Color;
					polyline.Style.FillColor = Colors.Transparent;
				}
				else if (b1 == 0xFE)
				{
					// If the first byte is FE, flood fill an area using the color index given in the second byte, starting from the point whose coordinates are given in
					// the next two bytes.
					opcode = AmigaKickstartOpcode.Fill;
					colorIndex = b2;

					polyline = (polyline.Clone() as PolygonVectorItem);

					byte b3 = reader.ReadByte(), b4 = reader.ReadByte();
					polyline.Points.Insert(0, new PositionVector2(b3, b4));
					polyline.Style.BorderColor = Colors.Transparent;
					polyline.Style.FillColor = Palette.Entries[colorIndex].Color;
				}
				else
				{
					if (opcode == AmigaKickstartOpcode.Polyline || opcode == AmigaKickstartOpcode.Fill)
					{
						polyline.Points.Add(new PositionVector2(b1, b2));
					}
				}
			}
			
			if (polyline != null)
			{
				// clean up just in case we get incomplete data
				vec.Items.Add(polyline);
			}

			double maxX = 0.0, maxY = 0.0;
			for (int i = 0; i < vec.Items.Count; i++)
			{
				PolygonVectorItem vi = (vec.Items[i] as PolygonVectorItem);
				if (vi == null) continue;

				for (int j = 0; j < vi.Points.Count; j++)
				{
					if (vi.Points[j].X > maxX)
						maxX = vi.Points[j].X;
					if (vi.Points[j].Y > maxY)
						maxY = vi.Points[j].Y;
				}
			}

			vec.Width = (int)maxX;
			vec.Height = (int)maxY;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			VectorImageObjectModel vec = (objectModel as VectorImageObjectModel);
			if (vec == null) throw new ObjectModelNotSupportedException();

		}
	}
}
