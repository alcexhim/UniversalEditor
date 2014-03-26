using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.Compression.Deflate.Internal;

namespace UniversalEditor.Compression.Common
{
	public class HuffmanTree
	{
		internal const int EndOfBlockCode = 256;
		internal const int MaxDistTreeElements = 32;
		internal const int MaxLiteralTreeElements = 288;
		internal const int NumberOfCodeLengthTreeElements = 19;
		private byte[] codeLengthArray;
		private short[] left;
		private short[] right;
		private static HuffmanTree staticDistanceTree = new HuffmanTree(HuffmanTree.GetStaticDistanceTreeLength());
		private static HuffmanTree staticLiteralLengthTree = new HuffmanTree(HuffmanTree.GetStaticLiteralTreeLength());
		private short[] table;
		private int tableBits;
		private int tableMask;
		public static HuffmanTree StaticDistanceTree
		{
			get
			{
				return HuffmanTree.staticDistanceTree;
			}
		}
		public static HuffmanTree StaticLiteralLengthTree
		{
			get
			{
				return HuffmanTree.staticLiteralLengthTree;
			}
		}
		public HuffmanTree(byte[] codeLengths)
		{
			this.codeLengthArray = codeLengths;
			if (this.codeLengthArray.Length == 288)
			{
				this.tableBits = 9;
			}
			else
			{
				this.tableBits = 7;
			}
			this.tableMask = (1 << this.tableBits) - 1;
			this.CreateTable();
		}
		private uint[] CalculateHuffmanCode()
		{
			uint[] numArray = new uint[17];
			byte[] codeLengthArray = this.codeLengthArray;
			for (int i = 0; i < codeLengthArray.Length; i++)
			{
				int index = (int)codeLengthArray[i];
				numArray[index] += 1u;
			}
			numArray[0] = 0u;
			uint[] numArray2 = new uint[17];
			uint num2 = 0u;
			for (int j = 1; j <= 16; j++)
			{
				numArray2[j] = num2 + numArray[j - 1] << 1;
			}
			uint[] numArray3 = new uint[288];
			for (int k = 0; k < this.codeLengthArray.Length; k++)
			{
				int length = (int)this.codeLengthArray[k];
				if (length > 0)
				{
					numArray3[k] = DecodeHelper.BitReverse(numArray2[length], length);
					numArray2[length] += 1u;
				}
			}
			return numArray3;
		}
		private void CreateTable()
		{
			uint[] numArray = this.CalculateHuffmanCode();
			this.table = new short[1 << this.tableBits];
			this.left = new short[2 * this.codeLengthArray.Length];
			this.right = new short[2 * this.codeLengthArray.Length];
			short length = (short)this.codeLengthArray.Length;
			for (int i = 0; i < this.codeLengthArray.Length; i++)
			{
				int num3 = (int)this.codeLengthArray[i];
				if (num3 > 0)
				{
					int index = (int)numArray[i];
					if (num3 <= this.tableBits)
					{
						int num4 = 1 << num3;
						if (index >= num4)
						{
							throw new System.IO.InvalidDataException("InvalidHuffmanData");
						}
						int num5 = 1 << this.tableBits - num3;
						for (int j = 0; j < num5; j++)
						{
							this.table[index] = (short)i;
							index += num4;
						}
					}
					else
					{
						int num6 = num3 - this.tableBits;
						int num7 = 1 << this.tableBits;
						int num8 = index & (1 << this.tableBits) - 1;
						short[] table = this.table;
						do
						{
							short num9 = table[num8];
							if (num9 == 0)
							{
								table[num8] = (short)(-length);
								num9 = (short)(-length);
								length += 1;
							}
							if ((index & num7) == 0)
							{
								table = this.left;
							}
							else
							{
								table = this.right;
							}
							num8 = (int)(-(int)num9);
							num7 <<= 1;
							num6--;
						}
						while (num6 != 0);
						table[num8] = (short)i;
					}
				}
			}
		}
		public int GetNextSymbol(InputBuffer input)
		{
			uint num = input.TryLoad16Bits();
			int result;
			if (input.AvailableBits == 0)
			{
				result = -1;
			}
			else
			{
				int index = (int)this.table[(int)((IntPtr)((long)((ulong)num & (ulong)((long)this.tableMask))))];
				if (index < 0)
				{
					uint num2 = 1u << this.tableBits;
					do
					{
						index = -index;
						if ((num & num2) == 0u)
						{
							index = (int)this.left[index];
						}
						else
						{
							index = (int)this.right[index];
						}
						num2 <<= 1;
					}
					while (index < 0);
				}
				if ((int)this.codeLengthArray[index] > input.AvailableBits)
				{
					result = -1;
				}
				else
				{
					input.SkipBits((int)this.codeLengthArray[index]);
					result = index;
				}
			}
			return result;
		}
		private static byte[] GetStaticDistanceTreeLength()
		{
			byte[] buffer = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				buffer[i] = 5;
			}
			return buffer;
		}
		private static byte[] GetStaticLiteralTreeLength()
		{
			byte[] buffer = new byte[288];
			for (int i = 0; i <= 143; i++)
			{
				buffer[i] = 8;
			}
			for (int j = 144; j <= 255; j++)
			{
				buffer[j] = 9;
			}
			for (int k = 256; k <= 279; k++)
			{
				buffer[k] = 7;
			}
			for (int l = 280; l <= 287; l++)
			{
				buffer[l] = 8;
			}
			return buffer;
		}
	}
}
