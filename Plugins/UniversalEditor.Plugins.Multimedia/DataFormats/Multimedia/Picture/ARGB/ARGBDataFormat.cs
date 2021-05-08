//
//  ARGBDataFormat.cs - provides a DataFormat for manipulating an image in BGRA 1A1R1G1B format
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

using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.ARGB
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating an image in BGRA 1A1R1G1B format.
	/// </summary>
	public class ARGBDataFormat : DataFormat
	{
		private DataFormatReference _dfr = null;
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
			if (pic == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			if (signature != "BGRA") throw new InvalidDataFormatException("File does not begin with \"BGRA\"");

			int unknown = br.ReadInt32();
			int imageWidth = br.ReadInt32();
			int imageHeight = br.ReadInt32();
			pic.Width = imageWidth;
			pic.Height = imageHeight;

			for (short x = 0; x < imageWidth; x++)
			{
				for (short y = 0; y < imageHeight; y++)
				{
					byte a = br.ReadByte();
					byte r = br.ReadByte();
					byte g = br.ReadByte();
					byte b = br.ReadByte();

					Color color = Color.FromRGBAByte(r, g, b, a);
					pic.SetPixel(color, x, y);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("BGRA");

			int unknown = 0;
			bw.WriteInt32(unknown);
			bw.WriteInt32(pic.Width);
			bw.WriteInt32(pic.Height);

			for (int x = 0; x < pic.Width; x++)
			{
				for (int y = 0; y < pic.Height; y++)
				{
					Color color = pic.GetPixel(x, y);
					bw.WriteByte((byte)(color.A * 255));
					bw.WriteByte((byte)(color.R * 255));
					bw.WriteByte((byte)(color.G * 255));
					bw.WriteByte((byte)(color.B * 255));
				}
			}
		}
	}
}
