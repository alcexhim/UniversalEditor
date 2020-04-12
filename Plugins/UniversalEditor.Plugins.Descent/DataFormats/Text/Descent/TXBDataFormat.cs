//
//  TXBDataFormat.cs - provides a DataFormat for manipulating text in encoded Descent TXB format
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

using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Text.Descent
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating text in encoded Descent TXB format.
	/// </summary>
	public class TXBDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PlainTextObjectModel text = (objectModel as PlainTextObjectModel);
			if (text == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			StringBuilder sb = new StringBuilder();
			byte[] data = reader.ReadToEnd();
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != 0x0D)
				{
					if (data[i] == 0x0A)
					{
						sb.Append((char)0x0D);
						sb.Append((char)0x0A);
					}
					else
					{
						char decchar = (char)data[i];
						char encchar = (char)((((decchar & 0x3F) << 2) + ((decchar & 0xC0) >> 6)) ^ 0xA7);
						sb.Append(encchar);
					}
				}
			}

			text.Text = sb.ToString();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PlainTextObjectModel text = (objectModel as PlainTextObjectModel);
			if (text == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			char[] txt = text.Text.ToCharArray();
			for (int i = 0; i < txt.Length; i++)
			{
				if (txt[i] != 0x0D)
				{
					if (txt[i] == 0x0A)
					{
						writer.WriteChar((char)0x0D);
						writer.WriteChar((char)0x0A);
					}
					else
					{
						char decchar = (char)txt[i];
						char encchar = (char)((((decchar & 0xFC) >> 2) + ((decchar & 0x03) << 6)) ^ 0xE9);
						writer.WriteChar(encchar);
					}
				}
			}
			writer.Flush();
		}
	}
}
