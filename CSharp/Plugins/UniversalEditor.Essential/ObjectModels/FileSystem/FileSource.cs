using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public abstract class FileSource
	{

		private FileSourceTransformation.FileSourceTransformationCollection mvarTransformations = new FileSourceTransformation.FileSourceTransformationCollection();
		public FileSourceTransformation.FileSourceTransformationCollection Transformations { get { return mvarTransformations; } }

		public byte[] GetData() { return GetData(0, GetLength()); }

		public abstract byte[] GetData(long offset, long length);
		public abstract long GetLength();
	}
}
