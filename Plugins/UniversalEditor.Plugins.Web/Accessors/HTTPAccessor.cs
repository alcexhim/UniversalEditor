//
//  HTTPAccessor.cs - provides an Accessor to publish and retrieve data over an HTTP(S) connection
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Net;

namespace UniversalEditor.Accessors
{
	/// <summary>
	/// Provides an <see cref="Accessor" /> to publish and retrieve data over an HTTP(S) connection.
	/// </summary>
	public class HTTPAccessor : Accessor
	{
		private static AccessorReference _ar = null;
		protected override AccessorReference MakeReferenceInternal()
		{
			if (_ar == null)
			{
				_ar = base.MakeReferenceInternal();
				_ar.Title = "Internet (HTTP)";
				_ar.ImportOptions.Add(new CustomOptionText(nameof(FileName), "File _name: "));
			}
			return _ar;
		}

		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private System.Net.HttpWebRequest http = null;
		private System.IO.Stream mvarStream = null;

		private long mvarLength = 0;
		public override long Length
		{
			get
			{
				return mvarLength;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		private long mvarPosition = 0;
		private byte[] mvarBuffer = new byte[0];

		protected override long GetPosition()
		{
			if (mvarStream == null) throw new InvalidOperationException("Please open before looking");
			return mvarPosition;
		}

		public override void Seek(long length, IO.SeekOrigin position)
		{
			switch (position)
			{
				case IO.SeekOrigin.Begin:
				{
					mvarPosition = length;
					break;
				}
				case IO.SeekOrigin.Current:
				{
					mvarPosition += length;
					break;
				}
				case IO.SeekOrigin.End:
				{
					mvarPosition = mvarLength - length;
					break;
				}
			}
		}

		protected override void OpenInternal()
		{
			if (http != null) throw new InvalidOperationException("Please close before opening");
			http = (HttpWebRequest)WebRequest.Create(mvarFileName);
			WebResponse resp = http.GetResponse();

			string contentLength = resp.Headers[HttpResponseHeader.ContentLength];
			long contentLengthL = 0;
			if (Int64.TryParse(contentLength, out contentLengthL))
			{
				mvarLength = contentLengthL;
			}
			mvarBuffer = new byte[mvarLength];

			mvarStream = resp.GetResponseStream();
		}

		private long mvarActualPosition = 0;

		protected override void CloseInternal()
		{
			if (http == null) throw new InvalidOperationException("Please open before closing");
			http.Abort();
			if (mvarStream != null) mvarStream.Close();
			mvarStream = null;
			http = null;
		}

		protected override int WriteInternal(byte[] buffer, int start, int count)
		{
			if (mvarStream == null) throw new InvalidOperationException("Please open before writing");
			mvarStream.Write(buffer, start, count);
			return count;
		}
		protected override int ReadInternal(byte[] buffer, int start, int count)
		{
			if (mvarPosition < mvarActualPosition)
			{
				// count of bytes available to be read from the buffer, starting at
				// current position
				long bufferAvailableCount = (mvarActualPosition - mvarPosition);

				// count of bytes that will be read from the buffer for this read
				// operation
				long bufferCount = Math.Min(count, bufferAvailableCount);

				// count of bytes that will be read from the stream for this read
				// operation
				long newCount = (count - bufferAvailableCount);

				// total count of bytes to be read from both buffer and stream
				long realCount = bufferCount + newCount;

				if (newCount > 0)
				{
					byte[] newBuffer = new byte[newCount];
					int readFromStream = mvarStream.Read(newBuffer, 0, (int)newCount);

					Array.Copy(newBuffer, 0, mvarBuffer, (int)(start + bufferCount), newCount);
				}
				Array.Copy(mvarBuffer, mvarPosition, buffer, start, realCount);

				mvarActualPosition += newCount;
				mvarPosition += count;
				return (int)realCount;
			}
			else if (mvarPosition == mvarActualPosition)
			{
				if (mvarStream == null) throw new InvalidOperationException("Please open before reading");
				int realCount = mvarStream.Read(buffer, start, count);
				Array.Copy(buffer, 0, mvarBuffer, mvarPosition, realCount);
				mvarActualPosition += realCount;
				mvarPosition += count;
				return realCount;
			}
			return -1;
		}

		protected override Accessor GetRelativeInternal(string filename, string prefix = null)
		{
			HTTPAccessor acc = new HTTPAccessor();
			acc.FileName = String.Concat(this.GetFileName(), "/../", filename);
			return acc;
		}
	}
}
