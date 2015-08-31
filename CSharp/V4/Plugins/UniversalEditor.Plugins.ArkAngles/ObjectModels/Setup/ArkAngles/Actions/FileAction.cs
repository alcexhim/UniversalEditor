using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles.Actions
{
	public class FileAction : Action
	{
		private string mvarSourceFileName = String.Empty;
		/// <summary>
		/// The location of the source file to copy.
		/// </summary>
		public string SourceFileName { get { return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		/// <summary>
		/// The location in which to place the copied file. If a directory is specified, the file name from the source file will be used.
		/// </summary>
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		private int mvarFileSize = 0;
		/// <summary>
		/// The size of the file on disk.
		/// </summary>
		public int FileSize { get { return mvarFileSize; } set { mvarFileSize = value; } }

		private string mvarDescription = String.Empty;
		public string Description {  get { return mvarDescription; } set { mvarDescription = value; } }

		public override object Clone()
		{
			FileAction clone = new FileAction();
			clone.Description = (mvarDescription.Clone() as string);
			clone.DestinationFileName = (mvarDestinationFileName.Clone() as string);
			clone.FileSize = mvarFileSize;
			clone.SourceFileName = (mvarSourceFileName.Clone() as string);
			return clone;
		}
	}
}
