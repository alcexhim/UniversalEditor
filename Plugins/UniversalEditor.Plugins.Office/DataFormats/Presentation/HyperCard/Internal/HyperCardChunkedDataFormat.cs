//
//  HyperCardChunkedDataFormat.cs
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;

namespace UniversalEditor.Plugins.Office.DataFormats.Presentation.HyperCard.Internal
{
	public class HyperCardChunkedDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ChunkedObjectModel chunked = (objectModel as ChunkedObjectModel);

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			while (!reader.EndOfStream)
			{
				uint chunkSize = reader.ReadUInt32();
				string chunkName = reader.ReadFixedLengthString(4);

				RIFFDataChunk chunk = new RIFFDataChunk();
				chunk.ID = chunkName;
				chunk.Data = reader.ReadBytes(chunkSize - 8);
				chunked.Chunks.Add(chunk);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
