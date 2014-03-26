using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Accessors
{
    public class FileAccessor : Accessor
    {
        public override long Length
        {
            get { return mvarFileStream.Length; }
            set { mvarFileStream.SetLength(value); }
        }

        public override void Seek(long length, IO.SeekOrigin position)
        {
            switch (position)
            {
                case IO.SeekOrigin.Begin:
                {
                    mvarFileStream.Seek(length, System.IO.SeekOrigin.Begin);
                    break;
                }
                case IO.SeekOrigin.Current:
                {
                    mvarFileStream.Seek(length, System.IO.SeekOrigin.Current);
                    break;
                }
                case IO.SeekOrigin.End:
                {
                    mvarFileStream.Seek(length, System.IO.SeekOrigin.End);
                    break;
                }
            }
        }

        internal override int ReadInternal(byte[] buffer, int offset, int count)
        {
            int length = mvarFileStream.Read(buffer, offset, count);
            return length;
        }

        internal override int WriteInternal(byte[] buffer, int offset, int count)
        {
            mvarFileStream.Write(buffer, offset, count);
            return count;
        }

        internal override void FlushInternal()
        {
            mvarFileStream.Flush();
        }

        private System.IO.FileStream mvarFileStream = null;

        private bool mvarAllowWrite = false;
        public bool AllowWrite { get { return mvarAllowWrite; } set { mvarAllowWrite = value; } }

        private bool mvarForceOverwrite = false;
        public bool ForceOverwrite { get { return mvarForceOverwrite; } set { mvarForceOverwrite = value; } }

        private string mvarFileName = String.Empty;

        public FileAccessor(string FileName, bool AllowWrite = false, bool ForceOverwrite = false, bool AutoOpen = true)
        {
            mvarFileName = FileName;
            mvarAllowWrite = AllowWrite;
            mvarForceOverwrite = ForceOverwrite;

            if (AutoOpen)
            {
                Open();
            }
        }

        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

        public void Open(string FileName)
        {
            mvarFileName = FileName;
            Open();
        }
        public override void Open()
        {
            System.IO.FileShare share = System.IO.FileShare.Read;
            System.IO.FileMode mode = System.IO.FileMode.OpenOrCreate;
            System.IO.FileAccess access = System.IO.FileAccess.Read;
            if (mvarAllowWrite)
            {
                access = System.IO.FileAccess.ReadWrite;
                if (mvarForceOverwrite)
                {
                    mode = System.IO.FileMode.Create;
                }
            }
            mvarFileStream = System.IO.File.Open(mvarFileName, mode, access, share);
        }

        public override void Close()
        {
        }
    }
}
