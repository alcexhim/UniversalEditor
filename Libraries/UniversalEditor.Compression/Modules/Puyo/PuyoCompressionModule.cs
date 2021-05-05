using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Puyo
{
	public abstract class PuyoCompressionModule
	{
		public string Name
		{
			get;
			protected set;
		}
		public bool CanCompress
		{
			get;
			protected set;
		}
		public bool CanDecompress
		{
			get;
			protected set;
		}
		public abstract MemoryStream Decompress(ref Stream data);
		public abstract MemoryStream Compress(ref Stream data, string filename);
		public abstract bool Check(ref Stream data, string filename);
		public virtual string DecompressFilename(ref Stream data, string filename)
		{
			return filename;
		}
		public virtual string CompressFilename(ref Stream data, string filename)
		{
			return filename;
		}
		public int[] LZsearch(ref byte[] decompressedData, uint pos, uint decompressedSize)
		{
			int num = 4096;
			int num2 = 18;
			List<int> list = new List<int>();
			int[] result;
			if (pos < 3u || decompressedSize - pos < 3u)
			{
				result = new int[2];
			}
			else
			{
				if (pos >= decompressedSize)
				{
					int[] array = new int[2];
					array[0] = -1;
					result = array;
				}
				else
				{
					int num3 = 1;
					while (num3 < num && (long)num3 < (long)((ulong)pos))
					{
						if (decompressedData[(int)((IntPtr)((long)((ulong)pos - (ulong)((long)num3) - 1uL)))] == decompressedData[(int)((uint)((UIntPtr)pos))])
						{
							list.Add(num3 + 1);
						}
						num3++;
					}
					if (list.Count == 0)
					{
						result = new int[2];
					}
					else
					{
						bool flag = false;
						int num4 = 0;
						while (num4 < num2 && !flag)
						{
							num4++;
							for (int i = 0; i < list.Count; i++)
							{
								if ((ulong)pos + (ulong)((long)num4) >= (ulong)decompressedSize)
								{
									flag = true;
									break;
								}
								if (decompressedData[(int)((IntPtr)((long)((ulong)pos + (ulong)((long)num4))))] != decompressedData[(int)((IntPtr)((long)((ulong)pos - (ulong)((long)list[i]) + (ulong)((long)(num4 % list[i])))))])
								{
									if (list.Count <= 1)
									{
										flag = true;
										break;
									}
									list.RemoveAt(i);
									i--;
								}
							}
						}
						result = new int[]
						{
							num4,
							list[0]
						};
					}
				}
			}
			return result;
		}
	}
}
