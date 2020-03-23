using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Chunked
{
	public class RIFFGroupChunk : RIFFChunk
	{
		private RIFFChunkCollection mvarChunks = new RIFFChunkCollection();
		public RIFFChunkCollection Chunks { get { return mvarChunks; } }

		public override object Clone()
		{
			RIFFGroupChunk clone = new RIFFGroupChunk();
			clone.ID = base.ID;
			foreach (RIFFChunk chunk in mvarChunks)
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
                foreach (RIFFChunk chunk in mvarChunks)
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
