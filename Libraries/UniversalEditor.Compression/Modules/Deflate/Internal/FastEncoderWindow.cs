using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.Modules.Deflate.Internal
{
	internal class FastEncoderWindow
	{
		private const int FastEncoderHashMask = 2047;
		private const int FastEncoderHashShift = 4;
		private const int FastEncoderHashtableSize = 2048;
		private const int FastEncoderMatch3DistThreshold = 16384;
		private const int FastEncoderWindowMask = 8191;
		private const int FastEncoderWindowSize = 8192;
		private const int GoodLength = 4;
		private const int LazyMatchThreshold = 6;
		internal const int MaxMatch = 258;
		internal const int MinMatch = 3;
		private const int NiceLength = 32;
		private const int SearchDepth = 32;
		private int bufEnd;
		private int bufPos = 8192;
		private ushort[] lookup = new ushort[2048];
		private ushort[] prev = new ushort[8450];
		private byte[] window = new byte[16646];
		public int BytesAvailable
		{
			get
			{
				return this.bufEnd - this.bufPos;
			}
		}
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this.bufEnd;
			}
		}
		public FastEncoderWindow()
		{
			this.bufEnd = this.bufPos;
		}
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this.window, this.bufEnd, count);
			this.bufEnd += count;
		}
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this.bufPos - 8192;
			byte num4 = this.window[this.bufPos];
			while (search > num3)
			{
				if (this.window[search + num] == num4)
				{
					int num5;
					for (num5 = 0; num5 < 258; num5++)
					{
						if (this.window[this.bufPos + num5] != this.window[search + num5])
						{
							break;
						}
					}
					if (num5 > num)
					{
						num = num5;
						num2 = search;
						if (num5 > 32)
						{
							break;
						}
						num4 = this.window[this.bufPos + num5];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this.prev[search & 8191];
			}
			matchPos = this.bufPos - num2 - 1;
			int result;
			if (num == 3 && matchPos >= 16384)
			{
				result = 0;
			}
			else
			{
				result = num;
			}
			return result;
		}
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint hash = this.HashValue(0u, this.window[this.bufPos]);
			hash = this.HashValue(hash, this.window[this.bufPos + 1]);
			int matchPos = 0;
			int num2;
			if (this.bufEnd - this.bufPos <= 3)
			{
				num2 = 0;
			}
			else
			{
				int search = (int)this.InsertString(ref hash);
				if (search != 0)
				{
					num2 = this.FindMatch(search, out matchPos, 32, 32);
					if (this.bufPos + num2 > this.bufEnd)
					{
						num2 = this.bufEnd - this.bufPos;
					}
				}
				else
				{
					num2 = 0;
				}
			}
			if (num2 < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this.window[this.bufPos];
				this.bufPos++;
			}
			else
			{
				this.bufPos++;
				if (num2 <= 6)
				{
					int num3 = 0;
					int num4 = (int)this.InsertString(ref hash);
					int num5;
					if (num4 != 0)
					{
						num5 = this.FindMatch(num4, out num3, (num2 < 4) ? 32 : 8, 32);
						if (this.bufPos + num5 > this.bufEnd)
						{
							num5 = this.bufEnd - this.bufPos;
						}
					}
					else
					{
						num5 = 0;
					}
					if (num5 > num2)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this.window[this.bufPos - 1];
						match.Position = num3;
						match.Length = num5;
						this.bufPos++;
						num2 = num5;
						this.InsertStrings(ref hash, num2);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = matchPos;
						match.Length = num2;
						num2--;
						this.bufPos++;
						this.InsertStrings(ref hash, num2);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = matchPos;
					match.Length = num2;
					this.InsertStrings(ref hash, num2);
				}
			}
			if (this.bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}
		private uint HashValue(uint hash, byte b)
		{
			return hash << 4 ^ (uint)b;
		}
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this.window[this.bufPos + 2]);
			uint num = (uint)this.lookup[(int)((UIntPtr)(hash & 2047))];
			this.lookup[(int)((UIntPtr)(hash & 2047))] = (ushort)this.bufPos;
			this.prev[this.bufPos & 8191] = (ushort)num;
			return num;
		}
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this.bufEnd - this.bufPos <= matchLen)
			{
				this.bufPos += matchLen - 1;
			}
			else
			{
				while (--matchLen > 0)
				{
					this.InsertString(ref hash);
					this.bufPos++;
				}
			}
		}
		public void MoveWindows()
		{
			Array.Copy(this.window, this.bufPos - 8192, this.window, 0, 8192);
			for (int num = 0; num < 2048; num++)
			{
				int num2 = (int)(this.lookup[num] - 8192);
				if (num2 <= 0)
				{
					this.lookup[num] = 0;
				}
				else
				{
					this.lookup[num] = (ushort)num2;
				}
			}
			for (int num = 0; num < 8192; num++)
			{
				long num3 = (long)((ulong)this.prev[num] - 8192uL);
				if (num3 <= 0L)
				{
					this.prev[num] = 0;
				}
				else
				{
					this.prev[num] = (ushort)num3;
				}
			}
			this.bufPos = 8192;
			this.bufEnd = this.bufPos;
		}
		private uint RecalculateHash(int position)
		{
			return (uint)(((int)this.window[position] << 8 ^ (int)this.window[position + 1] << 4 ^ (int)this.window[position + 2]) & 2047);
		}

#if DEBUG
		private void VerifyHashes()
		{
			for (int i = 0; i < 2048; i++)
			{
				ushort j = this.lookup[i];
				while (j != 0 && this.bufPos - (int)j < 8192)
				{
					ushort num3 = this.prev[(int)(j & 8191)];
					if (this.bufPos - (int)num3 >= 8192)
					{
						break;
					}
					j = num3;
				}
			}
		}
#endif

	}
}
