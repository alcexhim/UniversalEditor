using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class AccessorFileSource : FileSource
	{
		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		public AccessorFileSource(Accessor accessor)
		{
			mvarAccessor = accessor;
		}

		public override byte[] GetData(long offset, long length)
		{
			mvarAccessor.Seek(offset, IO.SeekOrigin.Begin);
			byte[] data = mvarAccessor.Reader.ReadBytes(length);
			return data;
		}

		public override long GetLength()
		{
			return mvarAccessor.Length;
		}
	}
}
