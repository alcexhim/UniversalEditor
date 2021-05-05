using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries
{
	internal class LzWindowDictionary : CompressionDictionary
	{
		private int WindowSize = 4096;
		private int WindowStart;
		private int WindowLength;
		private int MinMatchAmount = 3;
		private int MaxMatchAmount = 18;
		private int BlockSize;
		private List<int>[] OffsetList;
		public LzWindowDictionary()
		{
			this.OffsetList = new List<int>[256];
			for (int i = 0; i < this.OffsetList.Length; i++)
			{
				this.OffsetList[i] = new List<int>();
			}
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
					int num2 = 1;
					while (num2 < this.MaxMatchAmount && num2 < this.WindowLength && (long)(num + num2) < (long)((ulong)offset) && (ulong)offset + (ulong)((long)num2) < (ulong)length && DecompressedData[(int)((IntPtr)((long)((ulong)offset + (ulong)((long)num2))))] == DecompressedData[num + num2])
					{
						num2++;
					}
					if (num2 >= this.MinMatchAmount && num2 > array2[1])
					{
						array2 = new int[]
						{
							(int)((ulong)offset - (ulong)((long)num)),
							num2
						};
						if (num2 == this.MaxMatchAmount)
						{
							break;
						}
					}
				}
				result = array2;
			}
			return result;
		}
		public void SlideWindow(int Amount)
		{
			if (this.WindowLength == this.WindowSize)
			{
				this.WindowStart += Amount;
			}
			else
			{
				if (this.WindowLength + Amount <= this.WindowSize)
				{
					this.WindowLength += Amount;
				}
				else
				{
					Amount -= this.WindowSize - this.WindowLength;
					this.WindowLength = this.WindowSize;
					this.WindowStart += Amount;
				}
			}
		}
		public void SlideBlock()
		{
			this.WindowStart += this.BlockSize;
		}
		private void RemoveOldEntries(byte index)
		{
			int i = 0;
			while (i < this.OffsetList[(int)index].Count)
			{
				if (this.OffsetList[(int)index][i] >= this.WindowStart)
				{
					break;
				}
				this.OffsetList[(int)index].RemoveAt(0);
			}
		}
		public void SetWindowSize(int size)
		{
			this.WindowSize = size;
		}
		public void SetMinMatchAmount(int amount)
		{
			this.MinMatchAmount = amount;
		}
		public void SetMaxMatchAmount(int amount)
		{
			this.MaxMatchAmount = amount;
		}
		public void SetBlockSize(int size)
		{
			this.BlockSize = size;
			this.WindowLength = size;
		}
		public void AddEntry(byte[] DecompressedData, int offset)
		{
			this.OffsetList[(int)DecompressedData[offset]].Add(offset);
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
