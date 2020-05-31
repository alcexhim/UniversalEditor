//
//  StringAccessor.cs - provide an Accessor for reading from/writing to a string
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
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.Accessors
{
	public class StringAccessor : Accessor
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

		private char[] _data = new char[0];

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

		private void ResizeArray(ref char[] _data, long value)
		{
			char[] newdata = new char[value];
			System.Array.Copy(_data, 0, newdata, 0, System.Math.Min(_data.Length, newdata.Length));
			_data = newdata;
		}

		public StringAccessor()
		{
		}
		public StringAccessor(string data)
		{
			_data = data.ToCharArray();
		}
		public StringAccessor(char[] data)
		{
			_data = data;
		}

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
			if (start >= 0 /* && start < _data.Length */)
			{
				ptr = start;
			}
			/*
			else
			{
				throw new EndOfStreamException();
			}
			*/
		}

		public char[] ToArray()
		{
			char[] data = new char[_data.Length];
			System.Array.Copy(_data, 0, data, 0, data.Length);
			return data;
		}
		public override string ToString()
		{
			return new System.String(ToArray());
		}

		protected internal override int ReadInternal(byte[] buffer, int start, int count)
		{
			byte[] bytes = this.DefaultEncoding.GetBytes(_data, (int)ptr, 1);
			count = System.Math.Min(bytes.Length, count);
			System.Array.Copy(bytes, 0, buffer, start, count);
			ptr += count;
			return count;
		}
		protected internal override int WriteInternal(byte[] buffer, int start, int count)
		{
			string value = DefaultEncoding.GetString(buffer);
			int j = _data.Length;
			ResizeArray(ref _data, _data.Length + value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				_data[j + i] = value[i];
			}
			return count;
		}

		protected override void OpenInternal()
		{
		}
		protected override void CloseInternal()
		{
		}

		protected override Accessor GetRelativeInternal(string filename, string prefix = null)
		{
			// FIXME: not implemented
			return null;
		}
	}
}
