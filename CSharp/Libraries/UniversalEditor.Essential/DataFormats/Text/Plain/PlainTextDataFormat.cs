//
//  PlainTextDataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Text.Plain
{
	public class PlainTextDataFormat : DataFormat
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
			PlainTextObjectModel ptom = (objectModel as PlainTextObjectModel);
			if (ptom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				ptom.Lines.Add(line);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PlainTextObjectModel ptom = (objectModel as PlainTextObjectModel);
			if (ptom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			foreach (string line in ptom.Lines)
			{
				writer.WriteLine(line);
			}
		}
	}
}
