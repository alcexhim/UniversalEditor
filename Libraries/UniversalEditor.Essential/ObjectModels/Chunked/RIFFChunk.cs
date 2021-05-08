//
//  RIFFChunk.cs - represents a chunk in a Resource Interchange File Format file
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

namespace UniversalEditor.ObjectModels.Chunked
{
	/// <summary>
	/// Represents a chunk in a Resource Interchange File Format file.
	/// </summary>
	public abstract class RIFFChunk : ICloneable
	{
		public IChunkContainer Parent { get; private set; } = null;

		public class RIFFChunkCollection : System.Collections.ObjectModel.Collection<RIFFChunk>
		{
			private IChunkContainer _parent = null;
			public RIFFChunkCollection(IChunkContainer parent)
			{
				_parent = parent;
			}

			public RIFFChunk this[string Name]
			{
				get
				{
					RIFFChunk result;
					foreach (RIFFChunk chunk in this)
					{
						if (chunk.ID == Name)
						{
							result = chunk;
							return result;
						}
					}
					result = null;
					return result;
				}
			}
			public RIFFChunk Add(byte[] Data)
			{
				return this.Add("", Data);
			}
			public RIFFChunk Add(string Name, byte[] Data)
			{
				RIFFDataChunk chunk = new RIFFDataChunk();
				chunk.ID = Name;
				chunk.Data = Data;
				base.Add(chunk);
				return chunk;
			}

			protected override void InsertItem(int index, RIFFChunk item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, RIFFChunk item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = _parent;
			}
			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
				{
					this[i].Parent = null;
				}
				base.ClearItems();
			}
		}

		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		public abstract object Clone();

		public virtual int Size
		{
			get { return 4; }
		}
	}
}
