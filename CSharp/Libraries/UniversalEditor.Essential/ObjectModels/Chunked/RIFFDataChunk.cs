using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Chunked
{
	public class RIFFDataChunk : RIFFChunk
	{
		private byte[] mvarData = new byte[0];
		public byte[] Data
		{
			get { return mvarData; }
			set { mvarData = value; }
		}
		public override int Size
		{
			get
			{
				int result;
				if (this.mvarData == null)
				{
					result = 0;
				}
				else
				{
					result = this.mvarData.Length;
				}
				return result;
			}
		}
		public void Extract(string FileName)
		{
			System.IO.File.WriteAllBytes(FileName, mvarData);
		}

		public override object Clone()
		{
			RIFFDataChunk clone = new RIFFDataChunk();
			clone.Data = mvarData;
			clone.ID = base.ID;
			return clone;
		}

        public override string ToString()
        {
            return base.ID + " [" + mvarData.Length.ToString() + "]";
        }
	}
}
