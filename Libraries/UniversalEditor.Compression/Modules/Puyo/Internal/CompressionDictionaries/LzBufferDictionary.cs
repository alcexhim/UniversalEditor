using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries
{
	internal class LzBufferDictionary : CompressionDictionary
	{
		private int MinMatchAmount = 3;
		private int MaxMatchAmount = 18;
		private int BufferSize;
		private int BufferStart;
		private int BufferPointer;
		private byte[] BufferData;
		private List<int>[] OffsetList;
		public LzBufferDictionary()
		{
			this.OffsetList = new List<int>[256];
			for (int i = 0; i < this.OffsetList.Length; i++)
			{
				this.OffsetList[i] = new List<int>();
			}
			this.BufferData = new byte[0];
		}
		public int[] Search(byte[] DecompressedData, uint offset, uint length)
		{
			this.RemoveOldEntries(DecompressedData[(int)((uint)((UIntPtr)offset))]);
			int[] result;
			if ((ulong)offset < (ulong)((long)this.MinMatchAmount) || (ulong)(length - offset) < (ulong)((long)this.MinMatchAmount))
			{
				result = new int[2];
			}
			else
			{
				int[] array = new int[2];
				int[] array2 = array;
				for (int i = this.OffsetList[(int)DecompressedData[(int)((uint)((UIntPtr)offset))]].Count - 1; i >= 0; i--)
				{
					int num = this.OffsetList[(int)DecompressedData[(int)((uint)((UIntPtr)offset))]][i];
					int num2 = this.BufferStart + num & this.BufferSize - 1;
					int num3 = 1;
					while (num3 < this.MaxMatchAmount && num3 < this.BufferSize && (long)(num + num3) < (long)((ulong)offset) && (ulong)offset + (ulong)((long)num3) < (ulong)length && DecompressedData[(int)((IntPtr)((long)((ulong)offset + (ulong)((long)num3))))] == this.BufferData[num2 + num3 & this.BufferSize - 1])
					{
						num3++;
					}
					if (num3 >= this.MinMatchAmount && num3 > array2[1])
					{
						array2 = new int[]
						{
							num2,
							num3
						};
						if (num3 == this.MaxMatchAmount)
						{
							break;
						}
					}
				}
				result = array2;
			}
			return result;
		}
		private void RemoveOldEntries(byte index)
		{
			int i = 0;
			while (i < this.OffsetList[(int)index].Count)
			{
				if (this.OffsetList[(int)index][i] >= this.BufferPointer - this.BufferSize)
				{
					break;
				}
				this.OffsetList[(int)index].RemoveAt(0);
			}
		}
		public void SetBufferSize(int size)
		{
			if (this.BufferSize != size)
			{
				this.BufferSize = size;
				this.BufferData = new byte[this.BufferSize];
				for (int i = 0; i < this.BufferSize; i++)
				{
					this.BufferData[i] = 0;
					this.OffsetList[0].Add(i - this.BufferSize);
				}
			}
		}
		public void SetBufferStart(int pos)
		{
			this.BufferStart = pos;
		}
		public void SetMinMatchAmount(int amount)
		{
			this.MinMatchAmount = amount;
		}
		public void SetMaxMatchAmount(int amount)
		{
			this.MaxMatchAmount = amount;
		}
		public void AddEntry(byte[] DecompressedData, int offset)
		{
			this.BufferData[this.BufferStart + this.BufferPointer & this.BufferSize - 1] = DecompressedData[offset];
			this.OffsetList[(int)DecompressedData[offset]].Add(this.BufferPointer);
			this.BufferPointer++;
		}
		public void AddEntryRange(byte[] DecompressedData, int offset, int length)
		{
			for (int i = 0; i < length; i++)
			{
				this.AddEntry(DecompressedData, offset + i);
			}
		}
	}
}
