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
		private static AccessorReference _ar = null;
		public override AccessorReference MakeReference()
		{
			if (_ar == null)
			{
				_ar = base.MakeReference();
				_ar.Visible = false;
			}
			return _ar;
		}

		private System.IO.Stream mvarBaseStream = null;
		public System.IO.Stream BaseStream { get { return mvarBaseStream; } }

		protected override long GetPosition()
		{
			return mvarBaseStream.Position;
		}
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
			set { mvarBaseStream.SetLength(value); }
		}

		// [DebuggerNonUserCode()]
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

		protected internal override int ReadInternal(byte[] buffer, int start, int count)
		{
			// TODO: will ct ever be != count? should we add ct to Position instead of count??
			int ct = mvarBaseStream.Read(buffer, start, count);
			// Position += count;
			return count;
		}

		protected internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			mvarBaseStream.Write(buffer, start, count);
			// Position += count;
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
