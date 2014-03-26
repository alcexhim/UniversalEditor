using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
    public class ProgressEventArgs : CancelEventArgs
    {
        private long mvarTotal = 0;
        public long Total { get { return mvarTotal; } }

        private long mvarCurrent = 0;
        public long Current { get { return mvarCurrent; } }

        private string mvarMessage = String.Empty;
        public string Message { get { return mvarMessage; } }

        public ProgressEventArgs(long current, long total)
        {
            mvarCurrent = current;
            mvarTotal = total;
        }
        public ProgressEventArgs(long current, long total, string message)
        {
            mvarCurrent = current;
            mvarTotal = total;
            mvarMessage = message;
        }
    }
}
