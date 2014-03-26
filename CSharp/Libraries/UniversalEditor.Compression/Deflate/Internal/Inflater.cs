using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.Compression.Common;
using UniversalEditor.Compression.Gzip.Internal;

namespace UniversalEditor.Compression.Deflate.Internal
{
	internal class Inflater
	{
		private int bfinal;
		private int blockLength;
		private byte[] blockLengthBuffer = new byte[4];
		private BlockType blockType;
		private int codeArraySize;
		private int codeLengthCodeCount;
		private HuffmanTree codeLengthTree;
		private byte[] codeLengthTreeCodeLength;
		private byte[] codeList;
		private static readonly byte[] codeOrder = new byte[]
		{
			16, 
			17, 
			18, 
			0, 
			8, 
			7, 
			9, 
			6, 
			10, 
			5, 
			11, 
			4, 
			12, 
			3, 
			13, 
			2, 
			14, 
			1, 
			15
		};
		private uint crc32;
		private static readonly int[] distanceBasePosition = new int[]
		{
			1, 
			2, 
			3, 
			4, 
			5, 
			7, 
			9, 
			13, 
			17, 
			25, 
			33, 
			49, 
			65, 
			97, 
			129, 
			193, 
			257, 
			385, 
			513, 
			769, 
			1025, 
			1537, 
			2049, 
			3073, 
			4097, 
			6145, 
			8193, 
			12289, 
			16385, 
			24577, 
			0, 
			0
		};
		private int distanceCode;
		private int distanceCodeCount;
		private HuffmanTree distanceTree;
		private int extraBits;
		private static readonly byte[] extraLengthBits = new byte[]
		{
			0, 
			0, 
			0, 
			0, 
			0, 
			0, 
			0, 
			0, 
			1, 
			1, 
			1, 
			1, 
			2, 
			2, 
			2, 
			2, 
			3, 
			3, 
			3, 
			3, 
			4, 
			4, 
			4, 
			4, 
			5, 
			5, 
			5, 
			5, 
			0
		};
		private GzipDecoder gZipDecoder;
		private InputBuffer input;
		private int length;
		private static readonly int[] lengthBase = new int[]
		{
			3, 
			4, 
			5, 
			6, 
			7, 
			8, 
			9, 
			10, 
			11, 
			13, 
			15, 
			17, 
			19, 
			23, 
			27, 
			31, 
			35, 
			43, 
			51, 
			59, 
			67, 
			83, 
			99, 
			115, 
			131, 
			163, 
			195, 
			227, 
			258
		};
		private int lengthCode;
		private int literalLengthCodeCount;
		private HuffmanTree literalLengthTree;
		private int loopCounter;
		private OutputWindow output;
		private InflaterState state;
		private static readonly byte[] staticDistanceTreeTable = new byte[]
		{
			0, 
			16, 
			8, 
			24, 
			4, 
			20, 
			12, 
			28, 
			2, 
			18, 
			10, 
			26, 
			6, 
			22, 
			14, 
			30, 
			1, 
			17, 
			9, 
			25, 
			5, 
			21, 
			13, 
			29, 
			3, 
			19, 
			11, 
			27, 
			7, 
			23, 
			15, 
			31
		};
		private uint streamSize;
		private bool using_gzip;
		public int AvailableOutput
		{
			get
			{
				return this.output.AvailableBytes;
			}
		}
		public Inflater(bool doGZip)
		{
			this.using_gzip = doGZip;
			this.output = new OutputWindow();
			this.input = new InputBuffer();
			this.gZipDecoder = new GzipDecoder(this.input);
			this.codeList = new byte[320];
			this.codeLengthTreeCodeLength = new byte[19];
			this.Reset();
		}
		private bool Decode()
		{
			bool flag = false;
			bool flag2 = false;
			bool result;
			if (this.Finished())
			{
				result = true;
			}
			else
			{
				if (this.using_gzip)
				{
					if (this.state == InflaterState.ReadingGZIPHeader)
					{
						if (!this.gZipDecoder.ReadGzipHeader())
						{
							result = false;
							return result;
						}
						this.state = InflaterState.ReadingBFinal;
					}
					else
					{
						if (this.state == InflaterState.StartReadingGZIPFooter || this.state == InflaterState.ReadingGZIPFooter)
						{
							if (!this.gZipDecoder.ReadGzipFooter())
							{
								result = false;
								return result;
							}
							this.state = InflaterState.VerifyingGZIPFooter;
							result = true;
							return result;
						}
					}
				}
				if (this.state == InflaterState.ReadingBFinal)
				{
					if (!this.input.EnsureBitsAvailable(1))
					{
						result = false;
						return result;
					}
					this.bfinal = this.input.GetBits(1);
					this.state = InflaterState.ReadingBType;
				}
				if (this.state == InflaterState.ReadingBType)
				{
					if (!this.input.EnsureBitsAvailable(2))
					{
						this.state = InflaterState.ReadingBType;
						result = false;
						return result;
					}
					this.blockType = (BlockType)this.input.GetBits(2);
					if (this.blockType != BlockType.Dynamic)
					{
						if (this.blockType != BlockType.Static)
						{
							if (this.blockType != BlockType.Uncompressed)
							{
								throw new System.IO.InvalidDataException("UnknownBlockType");
							}
							this.state = InflaterState.UncompressedAligning;
						}
						else
						{
							this.literalLengthTree = HuffmanTree.StaticLiteralLengthTree;
							this.distanceTree = HuffmanTree.StaticDistanceTree;
							this.state = InflaterState.DecodeTop;
						}
					}
					else
					{
						this.state = InflaterState.ReadingNumLitCodes;
					}
				}
				if (this.blockType == BlockType.Dynamic)
				{
					if (this.state < InflaterState.DecodeTop)
					{
						flag2 = this.DecodeDynamicBlockHeader();
					}
					else
					{
						flag2 = this.DecodeBlock(out flag);
					}
				}
				else
				{
					if (this.blockType == BlockType.Static)
					{
						flag2 = this.DecodeBlock(out flag);
					}
					else
					{
						if (this.blockType != BlockType.Uncompressed)
						{
							throw new System.IO.InvalidDataException("UnknownBlockType");
						}
						flag2 = this.DecodeUncompressedBlock(out flag);
					}
				}
				if (flag && this.bfinal != 0)
				{
					if (this.using_gzip)
					{
						this.state = InflaterState.StartReadingGZIPFooter;
						result = flag2;
						return result;
					}
					this.state = InflaterState.Done;
				}
				result = flag2;
			}
			return result;
		}
		private bool DecodeBlock(out bool end_of_block_code_seen)
		{
			end_of_block_code_seen = false;
			int freeBytes = this.output.FreeBytes;
			bool result;
			while (freeBytes > 258)
			{
				switch (this.state)
				{
					case InflaterState.DecodeTop:
						{
							int nextSymbol = this.literalLengthTree.GetNextSymbol(this.input);
							if (nextSymbol < 0)
							{
								result = false;
								return result;
							}
							if (nextSymbol < 256)
							{
								this.output.Write((byte)nextSymbol);
								freeBytes--;
								continue;
							}
							if (nextSymbol == 256)
							{
								end_of_block_code_seen = true;
								this.state = InflaterState.ReadingBFinal;
								result = true;
								return result;
							}
							nextSymbol -= 257;
							if (nextSymbol < 8)
							{
								nextSymbol += 3;
								this.extraBits = 0;
							}
							else
							{
								if (nextSymbol == 28)
								{
									nextSymbol = 258;
									this.extraBits = 0;
								}
								else
								{
									if (nextSymbol >= Inflater.extraLengthBits.Length)
									{
										this.extraBits = 0;
									}
									else
									{
										this.extraBits = (int)Inflater.extraLengthBits[nextSymbol];
									}
								}
							}
							this.length = nextSymbol;
							goto IL_141;
						}
					case InflaterState.HaveInitialLength:
						{
							goto IL_141;
						}
					case InflaterState.HaveFullLength:
						{
							goto IL_1A1;
						}
					case InflaterState.HaveDistCode:
						{
							break;
						}
					default:
						{
							throw new System.IO.InvalidDataException("UnknownState");
						}
				}
			IL_228:
				int num6;
				if (this.distanceCode > 3)
				{
					this.extraBits = this.distanceCode - 2 >> 1;
					int num5 = this.input.GetBits(this.extraBits);
					if (num5 < 0)
					{
						result = false;
						return result;
					}
					num6 = Inflater.distanceBasePosition[this.distanceCode] + num5;
				}
				else
				{
					num6 = this.distanceCode + 1;
				}
				this.output.WriteLengthDistance(this.length, num6);
				freeBytes -= this.length;
				this.state = InflaterState.DecodeTop;
				continue;
			IL_1A1:
				if (this.blockType == BlockType.Dynamic)
				{
					this.distanceCode = this.distanceTree.GetNextSymbol(this.input);
				}
				else
				{
					this.distanceCode = this.input.GetBits(5);
					if (this.distanceCode >= 0)
					{
						this.distanceCode = (int)Inflater.staticDistanceTreeTable[this.distanceCode];
					}
				}
				if (this.distanceCode < 0)
				{
					result = false;
					return result;
				}
				this.state = InflaterState.HaveDistCode;
				goto IL_228;
			IL_141:
				if (this.extraBits > 0)
				{
					this.state = InflaterState.HaveInitialLength;
					int bits = this.input.GetBits(this.extraBits);
					if (bits < 0)
					{
						result = false;
						return result;
					}
					this.length = Inflater.lengthBase[this.length] + bits;
				}
				this.state = InflaterState.HaveFullLength;
				goto IL_1A1;
			}
			result = true;
			return result;
		}
		private bool DecodeDynamicBlockHeader()
		{
			bool result;
			switch (this.state)
			{
				case InflaterState.ReadingNumLitCodes:
					{
						this.literalLengthCodeCount = this.input.GetBits(5);
						if (this.literalLengthCodeCount < 0)
						{
							result = false;
							return result;
						}
						this.literalLengthCodeCount += 257;
						this.state = InflaterState.ReadingNumDistCodes;
						break;
					}
				case InflaterState.ReadingNumDistCodes:
					{
						break;
					}
				case InflaterState.ReadingNumCodeLengthCodes:
					{
						goto IL_CF;
					}
				case InflaterState.ReadingCodeLengthCodes:
					{
						goto IL_118;
					}
				case InflaterState.ReadingTreeCodesBefore:
				case InflaterState.ReadingTreeCodesAfter:
					{
						goto IL_1D6;
					}
				default:
					{
						throw new System.IO.InvalidDataException("UnknownState");
					}
			}
			this.distanceCodeCount = this.input.GetBits(5);
			if (this.distanceCodeCount < 0)
			{
				result = false;
				return result;
			}
			this.distanceCodeCount++;
			this.state = InflaterState.ReadingNumCodeLengthCodes;
		IL_CF:
			this.codeLengthCodeCount = this.input.GetBits(4);
			if (this.codeLengthCodeCount < 0)
			{
				result = false;
				return result;
			}
			this.codeLengthCodeCount += 4;
			this.loopCounter = 0;
			this.state = InflaterState.ReadingCodeLengthCodes;
		IL_118:
			while (this.loopCounter < this.codeLengthCodeCount)
			{
				int bits = this.input.GetBits(3);
				if (bits < 0)
				{
					result = false;
					return result;
				}
				this.codeLengthTreeCodeLength[(int)Inflater.codeOrder[this.loopCounter]] = (byte)bits;
				this.loopCounter++;
			}
			for (int i = this.codeLengthCodeCount; i < Inflater.codeOrder.Length; i++)
			{
				this.codeLengthTreeCodeLength[(int)Inflater.codeOrder[i]] = 0;
			}
			this.codeLengthTree = new HuffmanTree(this.codeLengthTreeCodeLength);
			this.codeArraySize = this.literalLengthCodeCount + this.distanceCodeCount;
			this.loopCounter = 0;
			this.state = InflaterState.ReadingTreeCodesBefore;
		IL_1D6:
			while (this.loopCounter < this.codeArraySize)
			{
				if (this.state == InflaterState.ReadingTreeCodesBefore && (this.lengthCode = this.codeLengthTree.GetNextSymbol(this.input)) < 0)
				{
					result = false;
					return result;
				}
				if (this.lengthCode <= 15)
				{
					int CS_0_3 = this.loopCounter++;
					this.codeList[CS_0_3] = (byte)this.lengthCode;
				}
				else
				{
					if (!this.input.EnsureBitsAvailable(7))
					{
						this.state = InflaterState.ReadingTreeCodesAfter;
						result = false;
						return result;
					}
					if (this.lengthCode == 16)
					{
						if (this.loopCounter == 0)
						{
							throw new System.IO.InvalidDataException();
						}
						byte num4 = this.codeList[this.loopCounter - 1];
						int num5 = this.input.GetBits(2) + 3;
						if (this.loopCounter + num5 > this.codeArraySize)
						{
							throw new System.IO.InvalidDataException();
						}
						for (int j = 0; j < num5; j++)
						{
							int CS_0_3 = this.loopCounter++;
							this.codeList[CS_0_3] = num4;
						}
					}
					else
					{
						if (this.lengthCode == 17)
						{
							int num5 = this.input.GetBits(3) + 3;
							if (this.loopCounter + num5 > this.codeArraySize)
							{
								throw new System.IO.InvalidDataException();
							}
							for (int k = 0; k < num5; k++)
							{
								int CS_0_3 = this.loopCounter++;
								this.codeList[CS_0_3] = 0;
							}
						}
						else
						{
							int num5 = this.input.GetBits(7) + 11;
							if (this.loopCounter + num5 > this.codeArraySize)
							{
								throw new System.IO.InvalidDataException();
							}
							for (int l = 0; l < num5; l++)
							{
								int CS_0_3 = this.loopCounter++;
								this.codeList[CS_0_3] = 0;
							}
						}
					}
				}
				this.state = InflaterState.ReadingTreeCodesBefore;
			}
			byte[] destinationArray = new byte[288];
			byte[] buffer2 = new byte[32];
			Array.Copy(this.codeList, destinationArray, this.literalLengthCodeCount);
			Array.Copy(this.codeList, this.literalLengthCodeCount, buffer2, 0, this.distanceCodeCount);
			if (destinationArray[256] == 0)
			{
				throw new System.IO.InvalidDataException();
			}
			this.literalLengthTree = new HuffmanTree(destinationArray);
			this.distanceTree = new HuffmanTree(buffer2);
			this.state = InflaterState.DecodeTop;
			result = true;
			return result;
		}
		private bool DecodeUncompressedBlock(out bool end_of_block)
		{
			end_of_block = false;
			while (true)
			{
				switch (this.state)
				{
					case InflaterState.UncompressedAligning:
						{
							this.input.SkipToByteBoundary();
							this.state = InflaterState.UncompressedByte1;
							goto IL_09;
						}
					case InflaterState.UncompressedByte1:
					case InflaterState.UncompressedByte2:
					case InflaterState.UncompressedByte3:
					case InflaterState.UncompressedByte4:
						{
							goto IL_09;
						}
					case InflaterState.DecodingUncompressed:
						{
							goto IL_FD;
						}
				}
				goto Block_4;
			IL_09:
				int bits = this.input.GetBits(8);
				if (bits < 0)
				{
					break;
				}
				this.blockLengthBuffer[(int)(this.state - InflaterState.UncompressedByte1)] = (byte)bits;
				if (this.state == InflaterState.UncompressedByte4)
				{
					this.blockLength = (int)this.blockLengthBuffer[0] + (int)this.blockLengthBuffer[1] * 256;
					int num2 = (int)this.blockLengthBuffer[2] + (int)this.blockLengthBuffer[3] * 256;
					if ((ushort)this.blockLength != (ushort)(~(ushort)num2))
					{
						goto Block_3;
					}
				}
				this.state |= (InflaterState)1;
			}
			bool result = false;
			return result;
		Block_3:
			throw new System.IO.InvalidDataException("InvalidBlockLength");
		Block_4:
		throw new System.IO.InvalidDataException("UnknownState");
		IL_FD:
			int num3 = this.output.CopyFrom(this.input, this.blockLength);
			this.blockLength -= num3;
			if (this.blockLength != 0)
			{
				result = (this.output.FreeBytes == 0);
			}
			else
			{
				this.state = InflaterState.ReadingBFinal;
				end_of_block = true;
				result = true;
			}
			return result;
		}
		public bool Finished()
		{
			return this.state == InflaterState.Done || this.state == InflaterState.VerifyingGZIPFooter;
		}
		public int Inflate(byte[] bytes, int offset, int length)
		{
			int num = 0;
			while (true)
			{
				int num2 = this.output.CopyTo(bytes, offset, length);
				if (num2 > 0)
				{
					if (this.using_gzip)
					{
						this.crc32 = DecodeHelper.UpdateCrc32(this.crc32, bytes, offset, num2);
						uint num3 = this.streamSize + (uint)num2;
						if (num3 < this.streamSize)
						{
							break;
						}
						this.streamSize = num3;
					}
					offset += num2;
					num += num2;
					length -= num2;
				}
				if (length == 0 || this.Finished() || !this.Decode())
				{
					goto Block_6;
				}
			}
			throw new System.IO.InvalidDataException("StreamSizeOverflow");
		Block_6:
			if (this.state == InflaterState.VerifyingGZIPFooter && this.output.AvailableBytes == 0)
			{
				if (this.crc32 != this.gZipDecoder.Crc32)
				{
					throw new System.IO.InvalidDataException("InvalidCRC");
				}
				if (this.streamSize != this.gZipDecoder.StreamSize)
				{
					throw new System.IO.InvalidDataException("InvalidStreamSize");
				}
			}
			return num;
		}
		public bool NeedsInput()
		{
			return this.input.NeedsInput();
		}
		public void Reset()
		{
			if (this.using_gzip)
			{
				this.gZipDecoder.Reset();
				this.state = InflaterState.ReadingGZIPHeader;
				this.streamSize = 0u;
				this.crc32 = 0u;
			}
			else
			{
				this.state = InflaterState.ReadingBFinal;
			}
		}
		public void SetInput(byte[] inputBytes, int offset, int length)
		{
			this.input.SetInput(inputBytes, offset, length);
		}
	}
	internal enum InflaterState
	{
		DecodeTop = 10,
		DecodingUncompressed = 20,
		Done = 24,
		HaveDistCode = 13,
		HaveFullLength = 12,
		HaveInitialLength = 11,
		ReadingBFinal = 2,
		ReadingBType,
		ReadingCodeLengthCodes = 7,
		ReadingGZIPFooter = 22,
		ReadingGZIPHeader = 0,
		ReadingNumCodeLengthCodes = 6,
		ReadingNumDistCodes = 5,
		ReadingNumLitCodes = 4,
		ReadingTreeCodesAfter = 9,
		ReadingTreeCodesBefore = 8,
		StartReadingGZIPFooter = 21,
		UncompressedAligning = 15,
		UncompressedByte1,
		UncompressedByte2,
		UncompressedByte3,
		UncompressedByte4,
		VerifyingGZIPFooter = 23
	}
}
