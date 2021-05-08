using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.PPPmD
{
	public class PPPmDStream : Stream
	{
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public override bool CanWrite
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public override bool CanSeek
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public override bool CanRead
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
		public override void Flush()
		{
			throw new NotImplementedException();
		}
	}
}
