using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.Modules.Deflate.Internal
{
	internal class OutputWindow
	{
		private const int WindowMask = 32767;
		private const int WindowSize = 32768;
		private int bytesUsed;
		private int end;
		private byte[] window = new byte[32768];
		public int AvailableBytes
		{
			get
			{
				return this.bytesUsed;
			}
		}
		public int FreeBytes
		{
			get
			{
				return 32768 - this.bytesUsed;
			}
		}
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.bytesUsed), input.AvailableBytes);
			int num2 = 32768 - this.end;
			int num3;
			if (length > num2)
			{
				num3 = input.CopyTo(this.window, this.end, num2);
				if (num3 == num2)
				{
					num3 += input.CopyTo(this.window, 0, length - num2);
				}
			}
			else
			{
				num3 = input.CopyTo(this.window, this.end, length);
			}
			this.end = (this.end + num3 & 32767);
			this.bytesUsed += num3;
			return num3;
		}
		public int CopyTo(byte[] output, int offset, int length)
		{
			int end;
			if (length > this.bytesUsed)
			{
				end = this.end;
				length = this.bytesUsed;
			}
			else
			{
				end = (this.end - this.bytesUsed + length & 32767);
			}
			int num2 = length;
			int num3 = length - end;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				length = end;
			}
			Array.Copy(this.window, end - length, output, offset, length);
			this.bytesUsed -= num2;
			return num2;
		}
		public void Write(byte b)
		{
			int CS_0_0 = this.end++;
			this.window[CS_0_0] = b;
			this.end &= 32767;
			this.bytesUsed++;
		}
		public void WriteLengthDistance(int length, int distance)
		{
			this.bytesUsed += length;
			int sourceIndex = this.end - distance & 32767;
			int num2 = 32768 - length;
			if (sourceIndex > num2 || this.end >= num2)
			{
				while (true)
				{
					length--;
					if (length <= 0)
					{
						break;
					}
					int CS_0_ = this.end++;
					sourceIndex++;
					if (sourceIndex < this.window.Length)
					{
						this.window[CS_0_] = this.window[sourceIndex];
					}
					else
					{
						Console.WriteLine("warning: OutputWindow.cs[79] (WriteLengthDistance): sourceIndex not less than window length");
					}
					this.end &= 32767;
					sourceIndex &= 32767;
				}
			}
			else
			{
				if (length <= distance)
				{
					Array.Copy(this.window, sourceIndex, this.window, this.end, length);
					this.end += length;
				}
				else
				{
					while (true)
					{
						length--;
						if (length <= 0)
						{
							break;
						}
						int CS_0_ = this.end++;
						sourceIndex++;
						this.window[CS_0_] = this.window[sourceIndex];
					}
				}
			}
		}
	}
}
