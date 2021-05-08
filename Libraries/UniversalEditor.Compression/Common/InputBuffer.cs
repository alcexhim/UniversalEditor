using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Common
{
	public class InputBuffer
	{
		private uint bitBuffer;
		private int bitsInBuffer;
		private byte[] buffer;
		private int end;
		private int start;
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer;
			}
		}
		public int AvailableBytes
		{
			get
			{
				return this.end - this.start + this.bitsInBuffer / 8;
			}
		}
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this.bitsInBuffer > 0 && length > 0)
			{
				offset++;
				output[offset] = (byte)this.bitBuffer;
				this.bitBuffer >>= 8;
				this.bitsInBuffer -= 8;
				length--;
				num++;
			}
			int result;
			if (length == 0)
			{
				result = num;
			}
			else
			{
				int num2 = this.end - this.start;
				if (length > num2)
				{
					length = num2;
				}
				Array.Copy(this.buffer, this.start, output, offset, length);
				this.start += length;
				result = num + length;
			}
			return result;
		}
		public bool EnsureBitsAvailable(int count)
		{
			bool result;
			if (this.bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					result = false;
					return result;
				}
				int CS_0_2 = this.start++;
				this.bitBuffer |= (uint)((uint)this.buffer[CS_0_2] << this.bitsInBuffer);
				this.bitsInBuffer += 8;
				if (this.bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						result = false;
						return result;
					}
					CS_0_2 = this.start++;
					this.bitBuffer |= (uint)((uint)this.buffer[CS_0_2] << this.bitsInBuffer);
					this.bitsInBuffer += 8;
				}
			}
			result = true;
			return result;
		}
		private uint GetBitMask(int count)
		{
			return (1u << count) - 1u;
		}
		public int GetBits(int count)
		{
			int result;
			if (!this.EnsureBitsAvailable(count))
			{
				result = -1;
			}
			else
			{
				int num = (int)(this.bitBuffer & this.GetBitMask(count));
				this.bitBuffer >>= count;
				this.bitsInBuffer -= count;
				result = num;
			}
			return result;
		}
		public bool NeedsInput()
		{
			return this.start == this.end;
		}
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this.buffer = buffer;
			this.start = offset;
			this.end = offset + length;
		}
		public void SkipBits(int n)
		{
			this.bitBuffer >>= n;
			this.bitsInBuffer -= n;
		}
		public void SkipToByteBoundary()
		{
			this.bitBuffer >>= this.bitsInBuffer % 8;
			this.bitsInBuffer -= this.bitsInBuffer % 8;
		}
		public uint TryLoad16Bits()
		{
			if (this.bitsInBuffer < 8)
			{
				if (this.start < this.end)
				{
					int CS_0_2 = this.start++;
					this.bitBuffer |= (uint)((uint)this.buffer[CS_0_2] << this.bitsInBuffer);
					this.bitsInBuffer += 8;
				}
				if (this.start < this.end)
				{
					int CS_0_2 = this.start++;
					this.bitBuffer |= (uint)((uint)this.buffer[CS_0_2] << this.bitsInBuffer);
					this.bitsInBuffer += 8;
				}
			}
			else
			{
				if (this.bitsInBuffer < 16 && this.start < this.end)
				{
					int CS_0_2 = this.start++;
					this.bitBuffer |= (uint)((uint)this.buffer[CS_0_2] << this.bitsInBuffer);
					this.bitsInBuffer += 8;
				}
			}
			return this.bitBuffer;
		}
	}
}
