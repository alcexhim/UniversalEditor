using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.Accessors
{
	public class StreamAccessor : Accessor
	{
		private System.IO.Stream mvarBaseStream = null;
		public System.IO.Stream BaseStream { get { return mvarBaseStream; } }

		public StreamAccessor()
		{
			mvarBaseStream = new System.IO.MemoryStream();
		}
		public StreamAccessor(System.IO.Stream outputStream)
		{
			mvarBaseStream = outputStream;
		}

		public override long Length
		{
			get { return mvarBaseStream.Length; }
			set { throw new InvalidOperationException(); }
		}

		[DebuggerNonUserCode()]
		public override void Seek(long offset, SeekOrigin position)
		{
			System.IO.SeekOrigin origin = System.IO.SeekOrigin.Begin;
			switch (position)
			{
				case SeekOrigin.Begin:
				{
					origin = System.IO.SeekOrigin.Begin;
					break;
				}
				case SeekOrigin.Current:
				{
					origin = System.IO.SeekOrigin.Current;
					break;
				}
				case SeekOrigin.End:
				{
					origin = System.IO.SeekOrigin.End;
					break;
				}
			}
			mvarBaseStream.Seek(offset, origin);
		}

		internal override int ReadInternal(byte[] buffer, int start, int count)
		{
			int ct = mvarBaseStream.Read(buffer, start, count);
			Position += count;
			return count;
		}

		internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			mvarBaseStream.Write(buffer, start, count);
			Position += count;
			return count;
		}

		protected override void OpenInternal()
		{
		}
		protected override void CloseInternal()
		{
		}
	}
}
