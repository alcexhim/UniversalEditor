//
//  PaintShopPaletteDataFormat.cs - provides a DataFormat for manipulating color palettes in Jasc Paint Shop Pro format
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
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.PaintShop
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating color palettes in Jasc Paint Shop Pro format.
	/// </summary>
	public class PaintShopPaletteDataFormat : DataFormat
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
			if (palette == null) throw new ObjectModelNotSupportedException();

			IO.Reader tr = base.Accessor.Reader;
			string signature = tr.ReadLine();
			if (signature != "JASC-PAL") throw new InvalidDataFormatException("File does not begin with \"JASC-PAL\"");

			string unknown = tr.ReadLine();
			string sColorCount = tr.ReadLine();
			int iColorCount = Int32.Parse(sColorCount);

			while (!tr.EndOfStream)
			{
				string colorLine = tr.ReadLine();
				string[] colorInfo = colorLine.Split(new char[] { ' ' });
				if (colorInfo.Length < 3) continue;

				string sR = colorInfo[0], sG = colorInfo[1], sB = colorInfo[2];
				int iR = Int32.Parse(sR), iG = Int32.Parse(sG), iB = Int32.Parse(sB);

				Color color = Color.FromRGBAInt32(iR, iG, iB);
				palette.Entries.Add(color);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
