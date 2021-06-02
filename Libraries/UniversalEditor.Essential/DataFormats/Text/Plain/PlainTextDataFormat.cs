//
//  PlainTextDataFormat.cs - provides a DataFormat for manipulating unformatted plain text files
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Text.Plain
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating unformatted plain text files.
	/// </summary>
	public class PlainTextDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public ByteOrderMark ByteOrderMark { get; set; } = ByteOrderMark.None;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PlainTextObjectModel ptom = (objectModel as PlainTextObjectModel);
			if (ptom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			// determine if we have BOM
			if (reader.Accessor.Length >= 2)
			{
				byte b1 = reader.ReadByte();
				byte b2 = reader.ReadByte();

				if (b1 == 0xEF && b2 == 0xBB)
				{
					byte b3 = reader.ReadByte();
					if (b3 == 0xBF)
					{
						ByteOrderMark = ByteOrderMark.UTF8;
						reader.Accessor.Seek(-1, SeekOrigin.Current);
					}
					reader.Accessor.Seek(-2, SeekOrigin.Current);
				}
				else if ((b1 == 0xFE && b2 == 0xFF) || (b1 == 0xFF && b2 == 0xFE))
				{
					if (b1 == 0xFE && b2 == 0xFF)
					{
						ByteOrderMark = ByteOrderMark.UTF16LittleEndian;
					}
					else
					{
						ByteOrderMark = ByteOrderMark.UTF16BigEndian;
					}
					reader.Accessor.Seek(-2, SeekOrigin.Current);
				}
				else
				{
					ByteOrderMark = ByteOrderMark.None;
					reader.Accessor.Seek(-2, SeekOrigin.Current);
				}
			}

			ReadText(Accessor, ptom);
		}

		protected virtual void ReadText(Accessor acc, PlainTextObjectModel objectModel)
		{
			while (!acc.Reader.EndOfStream)
			{
				string line = acc.Reader.ReadLine();
				objectModel.Lines.Add(line);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PlainTextObjectModel ptom = (objectModel as PlainTextObjectModel);
			if (ptom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			for (int i = 0; i < ptom.Lines.Count; i++)
			{
				writer.Write(ptom.Lines[i]);
				if (i < ptom.Lines.Count)
					writer.WriteLine();
			}
		}
	}
}
