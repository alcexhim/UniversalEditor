using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class PhysicalFileSource : FileSource
	{
		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		public override byte[] GetData(long offset, long length)
		{
			byte[] sourceData = System.IO.File.ReadAllBytes(mvarFileName);
			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, offset, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return (new System.IO.FileInfo(mvarFileName)).Length;
		}
	}
}
