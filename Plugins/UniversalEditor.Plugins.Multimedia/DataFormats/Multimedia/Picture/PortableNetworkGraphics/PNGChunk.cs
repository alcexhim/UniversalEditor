//
//  PNGChunk.cs - represents a chunk in a PNG image file
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
using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
	/// <summary>
	/// Represents a chunk in a PNG image file.
	/// </summary>
	public class PNGChunk
	{
		public class PNGChunkCollection
			: System.Collections.ObjectModel.Collection<PNGChunk>
		{
			public Dictionary<string, PNGChunk> chunksByName = new Dictionary<string, PNGChunk>();

			public PNGChunk Add(string name, byte[] data)
			{
				PNGChunk chunk = null;
				if (chunksByName.ContainsKey(name))
				{
					chunk = chunksByName[name];

					byte[] originalData = chunk.Data;
					Array.Resize<byte>(ref originalData, originalData.Length + data.Length);
					Array.Copy(data, 0, chunk.Data, chunk.Data.Length - data.Length, data.Length);
					chunk.Data = originalData;
				}
				else
				{
					chunk = new PNGChunk();
					chunk.Name = name;
					chunk.Data = data;
					base.Add(chunk);
				}
				return chunk;
			}
			public PNGChunk this[string name]
			{
				get
				{
					if (chunksByName.ContainsKey(name))
					{
						return chunksByName[name];
					}
					return null;
				}
			}

			protected override void InsertItem(int index, PNGChunk item)
			{
				base.InsertItem(index, item);
				if (!chunksByName.ContainsKey(item.Name))
				{
					chunksByName.Add(item.Name, item);
				}
			}
			protected override void RemoveItem(int index)
			{
				if (chunksByName.ContainsKey(this[index].Name))
				{
					chunksByName.Remove(this[index].Name);
				}
				base.RemoveItem(index);
			}
		}
		public string Name { get; set; } = String.Empty;
		public byte[] Data { get; set; } = new byte[0];
		public int CRC { get; set; } = 0;

		public override string ToString()
		{
			return Name + " [" + Data.Length.ToString() + "]";
		}
	}
}
