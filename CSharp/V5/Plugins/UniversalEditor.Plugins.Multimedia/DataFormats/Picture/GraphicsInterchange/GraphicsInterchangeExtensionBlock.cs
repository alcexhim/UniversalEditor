using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.GraphicsInterchange
{
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

		private byte mvarID = 0;
		public byte ID { get { return mvarID; } set { mvarID = value; } }

		private List<byte[]> mvarDataBlocks = new List<byte[]>();
		public List<byte[]> DataBlocks { get { return mvarDataBlocks; } }
	}
}
