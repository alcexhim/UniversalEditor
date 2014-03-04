using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
    public delegate void DataRequestEventHandler(object sender, DataRequestEventArgs e);
    public class DataRequestEventArgs : EventArgs
    {
        private byte[] mvarData = null;
        public byte[] Data { get { return mvarData; } set { mvarData = value; } }
    }
}
