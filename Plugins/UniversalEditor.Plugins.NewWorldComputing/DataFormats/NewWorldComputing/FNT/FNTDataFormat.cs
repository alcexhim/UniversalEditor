//
//  FNTDataFormat.cs - provides a DataFormat to manipulate fonts used in New World Computing (Heroes of Might and Magic II) games
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

using UniversalEditor.ObjectModels.NewWorldComputing.Font;

namespace UniversalEditor.DataFormats.NewWorldComputing.FNT
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate fonts used in New World Computing (Heroes of Might and Magic II) games.
	/// </summary>
	public class FNTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FontObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FontObjectModel font = (objectModel as FontObjectModel);

			IO.Reader br = base.Accessor.Reader;
			font.GlyphHeight = br.ReadUInt16();
			font.GlyphWidth = br.ReadUInt16();

			string icnfilename = br.ReadFixedLengthString(13);
			icnfilename = icnfilename.TrimNull();
			font.GlyphCollectionFileName = icnfilename;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FontObjectModel font = (objectModel as FontObjectModel);

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt16(font.GlyphHeight);
			bw.WriteUInt16(font.GlyphWidth);
			bw.WriteFixedLengthString(font.GlyphCollectionFileName, 15);
			bw.Flush();
		}
	}
}
