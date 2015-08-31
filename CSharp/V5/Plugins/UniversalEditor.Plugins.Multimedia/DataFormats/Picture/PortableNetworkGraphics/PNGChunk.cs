using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.PortableNetworkGraphics
{
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

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private byte[] mvarData = new byte[0];
        public byte[] Data { get { return mvarData; } set { mvarData = value; } }

        private int mvarCRC = 0;
        public int CRC { get { return mvarCRC; } set { mvarCRC = value; } }

        public override string ToString()
        {
            return mvarName + " [" + mvarData.Length.ToString() + "]";
        }
    }
}
