//
//  MemoryAccessor.cs - provide an Accessor for reading from/writing to in-memory data
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.Accessors
{
	public class MemoryAccessor : Accessor
	{
		/// <summary>
		/// Determines the default value for the <see cref="BufferAllocationSize"/> of a newly-created <see cref="MemoryAccessor" />.
		/// </summary>
		/// <value>The default size of the buffer allocation.</value>
		public static int DefaultBufferAllocationSize { get; set; } = 268435456;
		/// <summary>
		/// Determines the amount by which to increase the buffer when a reallocation is needed. Smaller values will cause more allocations, which take time,
		/// but larger values may exceed the host's memory capacity and cause unnecessary paging (which also takes time).
		/// </summary>
		/// <value>The size of the buffer allocation.</value>
		public int BufferAllocationSize { get; set; } = DefaultBufferAllocationSize;

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
			get { return _actualLength; }
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
			byte[] data = new byte[_actualLength];
			System.Array.Copy(_data, 0, data, 0, _actualLength);
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

		private int _actualLength = 0;
		protected internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			if (ptr + count > _data.Length)
			{
				ResizeArray(ref _data, _data.Length + BufferAllocationSize);
			}
			System.Array.Copy(buffer, start, _data, ptr, count);
			ptr += count;
			_actualLength += count;
			return count;
		}

		protected override void FlushInternal()
		{
			base.FlushInternal();
			
			ResizeArray(ref _data, _actualLength);
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
