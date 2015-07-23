using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public delegate void DataRequestEventHandler(object sender, DataRequestEventArgs e);
	public class DataRequestEventArgs : EventArgs
	{
		private byte[] mvarData = null;
		public byte[] Data { get { return mvarData; } set { mvarData = value; } }

		private Reader mvarReader = null;
		public Reader Reader { get { return mvarReader; } set { mvarReader = value; } }

		private long mvarOffset = 0;
		public long Offset { get { return mvarOffset; } set { mvarOffset = value; } }

		private long mvarLength = 0;
		public long Length { get { return mvarLength; } set { mvarLength = value; } }
	}
}
