//
//  StreamAccessor.cs - provide an Accessor for reading from/writing to a System.IO.Stream
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

﻿using System;
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
		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
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

		protected override void FlushInternal()
		{
			mvarBaseStream.Flush();
		}
	}
}
