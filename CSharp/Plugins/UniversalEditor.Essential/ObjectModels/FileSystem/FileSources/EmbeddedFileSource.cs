using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class EmbeddedFileSource : FileSource
	{
		private Reader mvarReader = null;
		public Reader Reader { get { return mvarReader; } set { mvarReader = value; } }

		private long mvarOffset = 0;
		public long Offset { get { return mvarOffset; } set { mvarOffset = value; } }

		private long mvarLength = 0;
		public long Length { get { return mvarLength; } set { mvarLength = value; } }

		public override byte[] GetData(long offset, long length)
		{
			mvarReader.Seek(mvarOffset, SeekOrigin.Begin);
			byte[] sourceData = mvarReader.ReadBytes(mvarLength);

			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, 0, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return mvarLength;
		}

		public EmbeddedFileSource(Reader reader, long offset, long length, FileSourceTransformation[] transformations = null)
		{
			mvarReader = reader;
			mvarOffset = offset;
			mvarLength = length;

			if (transformations != null)
			{
				foreach (FileSourceTransformation transformation in transformations)
				{
					base.Transformations.Add(transformation);
				}
			}
		}
	}
}
