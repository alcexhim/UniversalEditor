using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public override void Seek(long offset, IO.SeekOrigin position)
		{
			System.IO.SeekOrigin origin = System.IO.SeekOrigin.Begin;
			switch (position)
			{
				case IO.SeekOrigin.Begin:
				{
					origin = System.IO.SeekOrigin.Begin;
					break;
				}
				case IO.SeekOrigin.Current:
				{
					origin = System.IO.SeekOrigin.Current;
					break;
				}
				case IO.SeekOrigin.End:
				{
					origin = System.IO.SeekOrigin.End;
					break;
				}
			}
			mvarBaseStream.Seek(offset, origin);
		}

		internal override int ReadInternal(byte[] buffer, int start, int count)
		{
			throw new NotImplementedException();
		}

		internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			throw new NotImplementedException();
		}

		public override void Open()
		{
			throw new NotImplementedException();
		}

		public override void Close()
		{
			throw new NotImplementedException();
		}
	}
}
