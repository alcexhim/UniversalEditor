using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public delegate void FileSourceTransformationFunction(object sender, System.IO.Stream inputStream, System.IO.Stream outputStream);
	public enum FileSourceTransformationType
	{
		None = 0,
		Input = 1,
		Output = 2,
		InputAndOutput = 3
	}
	public class FileSourceTransformation
	{
		public class FileSourceTransformationCollection
			: System.Collections.ObjectModel.Collection<FileSourceTransformation>
		{

		}

		private FileSourceTransformationType mvarType = FileSourceTransformationType.None;
		public FileSourceTransformationType Type { get { return mvarType; } set { mvarType = value; } }

		private FileSourceTransformationFunction mvarFunction = null;
		public FileSourceTransformationFunction Function { get { return mvarFunction; } set { mvarFunction = value; } }

		public FileSourceTransformation(FileSourceTransformationType type, FileSourceTransformationFunction func)
		{
			mvarType = type;
			mvarFunction = func;
		}
	}
}
