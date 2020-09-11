﻿//
//  ChunkedObjectModel.cs - provides an ObjectModel for manipulating chunked binary files (such as RIFF)
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

namespace UniversalEditor.ObjectModels.Chunked
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating chunked binary files (such as RIFF).
	/// </summary>
	public class ChunkedObjectModel : ObjectModel, IChunkContainer
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Software Development", "Resource Interchange File Format (RIFF)" };
			}
			return _omr;
        }

#if UE_CHUNKED_RIFF_INCLUDE_METADATA

        private RIFFMetadataItem.RIFFMetadataItemCollection mvarInformation = new RIFFMetadataItem.RIFFMetadataItemCollection();
        public RIFFMetadataItem.RIFFMetadataItemCollection Information { get { return mvarInformation; } }

#endif

		public ChunkedObjectModel()
		{
			Chunks = new RIFFChunk.RIFFChunkCollection(this);
		}

		public RIFFChunk.RIFFChunkCollection Chunks { get; private set; } = null;

		public override void Clear()
		{
			Chunks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ChunkedObjectModel clone = where as ChunkedObjectModel;
			foreach (RIFFChunk chunk in Chunks)
			{
				clone.Chunks.Add(chunk.Clone() as RIFFChunk);
			}
		}
	}
}
