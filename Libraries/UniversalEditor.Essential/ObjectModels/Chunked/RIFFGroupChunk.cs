//
//  RIFFGroupChunk.cs - represents a group chunk (which can contain subchunks) in a Resource Interchange File Format file
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

namespace UniversalEditor.ObjectModels.Chunked
{
	/// <summary>
	/// Represents a group chunk (which can contain subchunks) in a Resource Interchange File Format file.
	/// </summary>
	public class RIFFGroupChunk : RIFFChunk, IChunkContainer
	{
		public RIFFChunkCollection Chunks { get; private set; } = null;
		public RIFFGroupChunk()
		{
			Chunks = new RIFFChunkCollection(this);
		}

		public override object Clone()
		{
			RIFFGroupChunk clone = new RIFFGroupChunk();
			clone.ID = base.ID;
			foreach (RIFFChunk chunk in Chunks)
			{
				clone.Chunks.Add(chunk.Clone() as RIFFChunk);
			}
			return clone;
		}

		public override int Size
		{
			get
			{
				int size = base.Size;
				foreach (RIFFChunk chunk in Chunks)
				{
					size += chunk.Size;
				}
				return size;
			}
		}

		private string mvarTypeID = String.Empty;
		public string TypeID { get { return mvarTypeID; } set { mvarTypeID = value; } }
	}
}
