//
//  BMPDataFormat.cs - provides a DataFormat for manipulating tri-state (black, white, or transparent) images in Heroes of Might and Magic II BMP format
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

using UniversalEditor.ObjectModels.Multimedia.Picture;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.BMP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating tri-state (black, white, or transparent) images in Heroes of Might and Magic II BMP format.
	/// </summary>
	public class BMPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) return;

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;

			ushort unknown = br.ReadUInt16();
			ushort width = br.ReadUInt16();
			ushort height = br.ReadUInt16();

			pic.Width = width;
			pic.Height = height;

			for (ushort y = 0; y < height; y++)
			{
				for (ushort x = 0; x < width; x++)
				{
					byte colorIndex = br.ReadByte();
					Color color = HoMM2Palette.ColorTable[colorIndex];
					if (colorIndex == 1)
					{
						color = Colors.White;
					}
					else if (colorIndex == 0)
					{
						color = Colors.Transparent;
					}
					else if (colorIndex == 2)
					{
						color = Colors.Black;
					}
					else // if (colorIndex == 3)
					{

					}

					pic.SetPixel(color, x, y);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{

		}
	}
}
