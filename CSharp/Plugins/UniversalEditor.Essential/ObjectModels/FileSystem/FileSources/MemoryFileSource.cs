using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class MemoryFileSource : FileSource
	{
		private byte[] mvarData = null;
		public byte[] Data { get { return mvarData; } set { mvarData = value; } }

		public MemoryFileSource(byte[] data)
		{
			mvarData = data;
		}

		public override byte[] GetData(long offset, long length)
		{
			long realLength = Math.Min(length, mvarData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(mvarData, offset, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return mvarData.Length;
		}
	}
}
