//
//  GraphicsInterchangeExtensionBlock.cs - represents an extension to the GIF format
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

using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GraphicsInterchange
{
	/// <summary>
	/// Represents an extension to the GIF format.
	/// </summary>
	public class GraphicsInterchangeExtensionBlock
	{
		public class GraphicsInterchangeExtensionBlockCollection
			: System.Collections.ObjectModel.Collection<GraphicsInterchangeExtensionBlock>
		{
			public GraphicsInterchangeExtensionBlock Add(byte id, params byte[][] dataBlocks)
			{
				GraphicsInterchangeExtensionBlock block = new GraphicsInterchangeExtensionBlock();
				block.ID = id;
				foreach (byte[] dataBlock in dataBlocks)
				{
					block.DataBlocks.Add(dataBlock);
				}
				base.Add(block);
				return block;
			}

			private Dictionary<byte, GraphicsInterchangeExtensionBlock> blocksById = new Dictionary<byte, GraphicsInterchangeExtensionBlock>();
			protected override void InsertItem(int index, GraphicsInterchangeExtensionBlock item)
			{
				blocksById.Add(item.ID, item);
				base.InsertItem(index, item);
			}
			protected override void RemoveItem(int index)
			{
				if (blocksById.ContainsKey(this[index].ID))
				{
					blocksById.Remove(this[index].ID);
				}
				base.RemoveItem(index);
			}

			public GraphicsInterchangeExtensionBlock this[byte id]
			{
				get
				{
					if (blocksById.ContainsKey(id))
					{
						return blocksById[id];
					}
					return null;
				}
			}
			public bool Contains(byte id)
			{
				return blocksById.ContainsKey(id);
			}
			public bool Remove(byte id)
			{
				if (!blocksById.ContainsKey(id))
				{
					return false;
				}

				this.Remove(blocksById[id]);
				return true;
			}
		}

		/// <summary>
		/// Gets or sets a unique single-byte identifier for this <see cref="GraphicsInterchangeExtensionBlock" />.
		/// </summary>
		/// <value>A unique single-byte identifier for this <see cref="GraphicsInterchangeExtensionBlock" />.</value>
		public byte ID { get; set; } = 0;
		/// <summary>
		/// Gets a list of data blocks for this <see cref="GraphicsInterchangeExtensionBlock" />.
		/// </summary>
		/// <value>A list of data blocks for this <see cref="GraphicsInterchangeExtensionBlock" />.</value>
		public List<byte[]> DataBlocks { get; } = new List<byte[]>();
	}
}
