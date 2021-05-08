//
//  BinaryDataFormat.cs - provides a DataFormat for manipulating raw binary data
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
using UniversalEditor.ObjectModels.Binary;

namespace UniversalEditor.DataFormats.Binary
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating raw binary data.
	/// </summary>
	/// <remarks>
	/// This is really only here because we need a corresponding <see cref="DataFormat" /> so we can edit raw binary data with the BinaryEditor. Ideally we
	/// could just tell Universal Editor to open a file with the BinaryEditor (not specifying a <see cref="DataFormat" />) and it would do so, just like Eclipse.
	/// </remarks>
	public class BinaryDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(BinaryObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BinaryObjectModel bom = (objectModel as BinaryObjectModel);

			Reader reader = Accessor.Reader;
			byte[] data = reader.ReadToEnd(); // this isn't good

			bom.Data = data;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			BinaryObjectModel bom = (objectModel as BinaryObjectModel);

			Writer writer = Accessor.Writer;
			writer.WriteBytes(bom.Data);
		}
	}
}
