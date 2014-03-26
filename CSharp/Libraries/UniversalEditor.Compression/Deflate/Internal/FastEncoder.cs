using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.Deflate.Internal
{
	internal class FastEncoder
	{
		internal class Output
		{
			private uint bitBuf;
			private int bitCount;
			private static byte[] distLookup;
			private byte[] outputBuf;
			private int outputPos;
			internal int BytesWritten
			{
				get
				{
					return this.outputPos;
				}
			}
			internal int FreeBytes
			{
				get
				{
					return this.outputBuf.Length - this.outputPos;
				}
			}
			static Output()
			{
				FastEncoder.Output.distLookup = new byte[512];
				FastEncoder.Output.GenerateSlotTables();
			}
			internal void FlushBits()
			{
				while (this.bitCount >= 8)
				{
					int CS_0_0 = this.outputPos++;
					this.outputBuf[CS_0_0] = (byte)this.bitBuf;
					this.bitCount -= 8;
					this.bitBuf >>= 8;
				}
				if (this.bitCount > 0)
				{
					int CS_0_0 = this.outputPos++;
					this.outputBuf[CS_0_0] = (byte)this.bitBuf;
					this.bitCount = 0;
				}
			}
			internal static void GenerateSlotTables()
			{
				int num = 0;
				int index;
				for (index = 0; index < 16; index++)
				{
					for (int i = 0; i < 1 << (int)FastEncoderStatics.ExtraDistanceBits[index]; i++)
					{
						num++;
						FastEncoder.Output.distLookup[num] = (byte)index;
					}
				}
				num >>= 7;
				while (index < 30)
				{
					for (int j = 0; j < 1 << (int)(FastEncoderStatics.ExtraDistanceBits[index] - 7); j++)
					{
						num++;
						FastEncoder.Output.distLookup[256 + num] = (byte)index;
					}
					index++;
				}
			}
			internal int GetSlot(int pos)
			{
				return (int)FastEncoder.Output.distLookup[(pos < 256) ? pos : (256 + (pos >> 7))];
			}
			internal bool SafeToWriteTo()
			{
				return this.outputBuf.Length - this.outputPos > 16;
			}
			internal void UpdateBuffer(byte[] output)
			{
				this.outputBuf = output;
				this.outputPos = 0;
			}
			internal void WriteBits(int n, uint bits)
			{
				this.bitBuf |= bits << this.bitCount;
				this.bitCount += n;
				if (this.bitCount >= 16)
				{
					int CS_0_ = this.outputPos++;
					this.outputBuf[CS_0_] = (byte)this.bitBuf;
					CS_0_ = this.outputPos++;
					this.outputBuf[CS_0_] = (byte)(this.bitBuf >> 8);
					this.bitCount -= 16;
					this.bitBuf >>= 16;
				}
			}
			internal void WriteChar(byte b)
			{
				uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
				this.WriteBits((int)(num & 31u), num >> 5);
			}
			internal void WriteGzipFooter(uint gzipCrc32, uint inputStreamSize)
			{
				int CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(gzipCrc32 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(gzipCrc32 >> 8 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(gzipCrc32 >> 16 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(gzipCrc32 >> 24 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(inputStreamSize & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(inputStreamSize >> 8 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(inputStreamSize >> 16 & 255u);
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = (byte)(inputStreamSize >> 24 & 255u);
			}
			internal void WriteGzipHeader(int compression_level)
			{
				int CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 31;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 139;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 8;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
				if (compression_level == 10)
				{
					CS_0_0 = this.outputPos++;
					this.outputBuf[CS_0_0] = 2;
				}
				else
				{
					CS_0_0 = this.outputPos++;
					this.outputBuf[CS_0_0] = 4;
				}
				CS_0_0 = this.outputPos++;
				this.outputBuf[CS_0_0] = 0;
			}
			internal void WriteMatch(int matchLen, int matchPos)
			{
				uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
				int i = (int)(num & 31u);
				if (i <= 16)
				{
					this.WriteBits(i, num >> 5);
				}
				else
				{
					this.WriteBits(16, num >> 5 & 65535u);
					this.WriteBits(i - 16, num >> 21);
				}
				num = FastEncoderStatics.FastEncoderDistanceCodeInfo[this.GetSlot(matchPos)];
				this.WriteBits((int)(num & 15u), num >> 8);
				int num2 = (int)(num >> 4 & 15u);
				if (num2 != 0)
				{
					this.WriteBits(num2, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num2]));
				}
			}
			internal void WritePreamble()
			{
				Array.Copy(FastEncoderStatics.FastEncoderTreeStructureData, 0, this.outputBuf, this.outputPos, FastEncoderStatics.FastEncoderTreeStructureData.Length);
				this.outputPos += FastEncoderStatics.FastEncoderTreeStructureData.Length;
				this.bitCount = 9;
				this.bitBuf = 34u;
			}
		}
		private Match currentMatch;
		private uint gzipCrc32;
		private bool hasBlockHeader;
		private bool hasGzipHeader;
		private DeflateInput inputBuffer;
		private uint inputStreamSize;
		private FastEncoderWindow inputWindow;
		private bool needsEOB;
		private FastEncoder.Output output;
		private bool usingGzip;
		public FastEncoder(bool doGZip)
		{
			this.usingGzip = doGZip;
			this.inputWindow = new FastEncoderWindow();
			this.inputBuffer = new DeflateInput();
			this.output = new FastEncoder.Output();
			this.currentMatch = new Match();
		}
		public int Finish(byte[] outputBuffer)
		{
			this.output.UpdateBuffer(outputBuffer);
			if (this.needsEOB)
			{
				uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
				int i = (int)(num & 31u);
				this.output.WriteBits(i, num >> 5);
				this.output.FlushBits();
				if (this.usingGzip)
				{
					this.output.WriteGzipFooter(this.gzipCrc32, this.inputStreamSize);
				}
			}
			return this.output.BytesWritten;
		}
		public int GetCompressedOutput(byte[] outputBuffer)
		{
			this.output.UpdateBuffer(outputBuffer);
			if (this.usingGzip && !this.hasGzipHeader)
			{
				this.output.WriteGzipHeader(3);
				this.hasGzipHeader = true;
			}
			if (!this.hasBlockHeader)
			{
				this.hasBlockHeader = true;
				this.output.WritePreamble();
			}
			while (true)
			{
				int count = (this.inputBuffer.Count < this.inputWindow.FreeWindowSpace) ? this.inputBuffer.Count : this.inputWindow.FreeWindowSpace;
				if (count > 0)
				{
					this.inputWindow.CopyBytes(this.inputBuffer.Buffer, this.inputBuffer.StartIndex, count);
					if (this.usingGzip)
					{
						this.gzipCrc32 = DecodeHelper.UpdateCrc32(this.gzipCrc32, this.inputBuffer.Buffer, this.inputBuffer.StartIndex, count);
						uint num2 = this.inputStreamSize + (uint)count;
						if (num2 < this.inputStreamSize)
						{
							break;
						}
						this.inputStreamSize = num2;
					}
					this.inputBuffer.ConsumeBytes(count);
				}
				while (this.inputWindow.BytesAvailable > 0 && this.output.SafeToWriteTo())
				{
					this.inputWindow.GetNextSymbolOrMatch(this.currentMatch);
					if (this.currentMatch.State == MatchState.HasSymbol)
					{
						this.output.WriteChar(this.currentMatch.Symbol);
					}
					else
					{
						if (this.currentMatch.State == MatchState.HasMatch)
						{
							this.output.WriteMatch(this.currentMatch.Length, this.currentMatch.Position);
						}
						else
						{
							this.output.WriteChar(this.currentMatch.Symbol);
							this.output.WriteMatch(this.currentMatch.Length, this.currentMatch.Position);
						}
					}
				}
				if (!this.output.SafeToWriteTo() || this.NeedsInput())
				{
					goto Block_13;
				}
			}
			throw new InvalidDataException("StreamSizeOverflow");
		Block_13:
			this.needsEOB = true;
			return this.output.BytesWritten;
		}
		public bool NeedsInput()
		{
			return this.inputBuffer.Count == 0 && this.inputWindow.BytesAvailable == 0;
		}
		public void SetInput(byte[] input, int startIndex, int count)
		{
			this.inputBuffer.Buffer = input;
			this.inputBuffer.Count = count;
			this.inputBuffer.StartIndex = startIndex;
		}
	}
}
