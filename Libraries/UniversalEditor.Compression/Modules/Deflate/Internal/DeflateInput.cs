using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Modules.Deflate.Internal
{
	internal class DeflateInput
	{
		private byte[] buffer;
		private int count;
		private int startIndex;
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}
		internal int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}
		internal int StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}
		internal void ConsumeBytes(int n)
		{
			this.startIndex += n;
			this.count -= n;
		}
	}
}
