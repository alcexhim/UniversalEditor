//
//  RIFFPaletteDataFormat.cs
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
using System.Collections.Generic;
using UniversalEditor.DataFormats.Chunked.RIFF;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.NewWorldComputing
{
	public class RIFFPaletteDataFormat : RIFFDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel riff = (objectModels.Pop() as ChunkedObjectModel);
			PaletteObjectModel palette = (objectModels.Pop() as PaletteObjectModel);

			RIFFGroupChunk grp = (riff.Chunks[0] as RIFFGroupChunk);
			if (grp.ID != "PAL ")
				throw new InvalidDataFormatException("RIFF container does not contain a group chunk with ID 'PAL '");

			RIFFDataChunk data = (grp.Chunks["data"] as RIFFDataChunk);
			if (data == null)
				throw new InvalidDataFormatException("'PAL ' group chunk does not contain a data chunk with ID 'data'");

			if (data.Data.Length % 4 != 0)
				throw new InvalidDataFormatException("'data' data chunk length is not evenly divisible by 4");

			for (int i = 4; i < data.Data.Length; i += 4)
			{
				MBS.Framework.Drawing.Color color = MBS.Framework.Drawing.Color.FromRGBAByte(data.Data[i], data.Data[i + 1], data.Data[i + 2]);
				palette.Entries.Add(new PaletteEntry(color));
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PaletteObjectModel palette = (objectModels.Pop() as PaletteObjectModel);
			ChunkedObjectModel riff = new ChunkedObjectModel();

			objectModels.Push(riff);
		}
	}
}
