//
//  I16DataFormat.cs - provides a DataFormat for manipulating images in I16 format
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

using System;

using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.I16
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in I16 format.
	/// </summary>
	public class I16DataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
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
			ushort u1 = br.ReadUInt16();
			ushort u2 = br.ReadUInt16();
			ushort u3 = br.ReadUInt16();
			ushort u4 = br.ReadUInt16();

			byte u = br.ReadByte();

			int size = (int)((double)br.Accessor.Length / 8);
			pic.Width = size;
			pic.Height = size;

			while (!br.EndOfStream)
			{
				byte r = br.ReadByte();
				byte g = br.ReadByte();
				byte b = br.ReadByte();
				byte a = br.ReadByte();

				Color color = Color.FromRGBAByte(r, g, b, a);
				pic.SetPixel(color);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
