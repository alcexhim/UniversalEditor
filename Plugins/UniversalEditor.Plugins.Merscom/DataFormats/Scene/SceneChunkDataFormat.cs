//
//  SceneChunkDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Chunked;

namespace UniversalEditor.Plugins.Merscom.DataFormats.Scene
{
	/// <summary>
	/// ChunkedObjectModel implementation of scene base data format.
	/// </summary>
	public class SceneChunkDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ChunkedObjectModel chunked = (objectModel as ChunkedObjectModel);

			while (!Accessor.Reader.EndOfStream)
			{
				RIFFChunk chunk = ReadChunk(Accessor.Reader);
			}
		}

		private RIFFChunk ReadChunk(IO.Reader reader)
		{
			RIFFChunk chunk = null;
			string chunkID = reader.ReadFixedLengthString(4);
			uint preambleLength = reader.ReadUInt32();
			uint totalChunkSize = reader.ReadUInt32();

			uint unknown1 = reader.ReadUInt32(); // offset to floats
			uint dataLength = reader.ReadUInt32();

			byte[] data = reader.ReadBytes(dataLength);

			chunk = new SceneChunk();
			((SceneChunk)chunk).ID = chunkID;
			((SceneChunk)chunk).Data = data;
			return chunk;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ChunkedObjectModel chunked = (objectModel as ChunkedObjectModel);
		}
	}
}
