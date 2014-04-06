using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;
using System.Diagnostics;

namespace UniversalEditor
{
    [DebuggerNonUserCode()]
    public abstract class Accessor
    {
        public Accessor()
        {
            mvarReader = new Reader(this);
            mvarWriter = new Writer(this);
        }

        public abstract long Length { get; set; }

        private long mvarPosition = 0;
        public long Position { get { return mvarPosition; } set { mvarPosition = value;  Seek(mvarPosition, SeekOrigin.Begin); } }
        public long Remaining { get { return Length - Position; } }

        public void Seek(int length, SeekOrigin position)
        {
            Seek((long)length, position);
        }
        public abstract void Seek(long length, SeekOrigin position);

        internal abstract int ReadInternal(byte[] buffer, int start, int count);
        internal abstract int WriteInternal(byte[] buffer, int start, int count);

        internal virtual void FlushInternal()
        {
        }

        private bool mvarIsOpen = false;
        public bool IsOpen { get { return mvarIsOpen; } protected set { mvarIsOpen = value; } }

        public abstract void Open();
        public abstract void Close();

        private Encoding mvarDefaultEncoding = Encoding.Default;
        /// <summary>
        /// The default <see cref="Encoding" /> to use when reading and writing strings.
        /// </summary>
        public Encoding DefaultEncoding { get { return mvarDefaultEncoding; } set { mvarDefaultEncoding = value; } }

        private Reader mvarReader = null;
        public Reader Reader { get { return mvarReader; } }
        private Writer mvarWriter = null;
        public Writer Writer { get { return mvarWriter; } }
    }
}
