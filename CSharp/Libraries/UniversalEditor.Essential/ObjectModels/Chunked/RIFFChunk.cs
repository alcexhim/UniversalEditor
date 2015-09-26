using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Chunked
{
	public abstract class RIFFChunk : ICloneable
	{
		public class RIFFChunkCollection : System.Collections.ObjectModel.Collection<RIFFChunk>
		{
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
