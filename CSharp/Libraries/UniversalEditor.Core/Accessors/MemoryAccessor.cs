using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.Accessors
{
	public class MemoryAccessor : Accessor
	{
		private static AccessorReference _ar = null;
		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
				_ar.Visible = false;
			}
			return _ar;
		}

		private byte[] _data = new byte[0];

		private long ptr = 0;
		protected override long GetPosition()
		{
			return ptr;
		}
		public override long Length
		{
			get { return _data.Length; }
			set
			{
				// resize the array - coded by hand to compile happily under Cosmos devkit
				ResizeArray(ref _data, value);
			}
		}

		private void ResizeArray(ref byte[] _data, long value)
		{
			byte[] newdata = new byte[value];
			System.Array.Copy(_data, 0, newdata, 0, System.Math.Min(_data.Length, newdata.Length));
			_data = newdata;
		}

		private string mvarFileName = null;

		public MemoryAccessor()
		{
		}
		public MemoryAccessor(byte[] data, string filename = null)
		{
			_data = data;
			mvarFileName = filename;
		}

		// [DebuggerNonUserCode()]
		public override void Seek(long length, SeekOrigin position)
		{
			long start = 0;
			switch (position)
			{
				case SeekOrigin.Begin:
				{
					start = length;
					break;
				}
				case SeekOrigin.Current:
				{
					start = ptr + length;
					break;
				}
				case SeekOrigin.End:
				{
					start = _data.LongLength - length;
					break;
				}
			}
			if (start >= 0 && start <= _data.Length)
			{
				ptr = start;
			}
			else
			{
				throw new EndOfStreamException();
			}
		}

		public byte[] ToArray()
		{
			byte[] data = new byte[_data.Length];
			System.Array.Copy(_data, 0, data, 0, data.Length);
			return data;
		}

		protected internal override int ReadInternal(byte[] buffer, int start, int count)
		{
			if (Position >= _data.Length) {
				return 0;
			}
			System.Array.Copy(_data, Position, buffer, start, count);
			Position += count;
			return count;
		}
		protected internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			ResizeArray(ref _data, _data.Length + count);
			System.Array.Copy(buffer, start, _data, _data.Length - count, count);
			return count;
		}

		protected override void OpenInternal()
		{
		}
		protected override void CloseInternal()
		{
		}

		public override string GetFileName()
		{
			if (mvarFileName != null) return mvarFileName;
			return base.GetFileName();
		}
		public override string GetFileTitle()
		{
			if (mvarFileName != null) return System.IO.Path.GetFileName(mvarFileName);
			return base.GetFileTitle();
		}
	}
}
