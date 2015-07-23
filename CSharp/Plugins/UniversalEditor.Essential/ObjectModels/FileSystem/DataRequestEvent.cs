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
		[Obsolete("Please use FileSources instead of DataRequests")]
		public byte[] Data { get { return mvarData; } set { mvarData = value; } }
	}
}
