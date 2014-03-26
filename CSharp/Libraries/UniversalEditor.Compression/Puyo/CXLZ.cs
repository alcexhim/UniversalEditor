using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class CXLZ : CompressionModule
	{
        public override string Name
        {
            get { return "CXLZ"; }
        }

		public override void Decompress(Stream inputStream, Stream outputStream, int inputLength, int outputLength)
        {
			uint num = (uint)inputStream.Length;
			uint num2 = inputStream.ReadUInt(4L) >> 8;
			uint num3 = 8u;
			byte[] array = inputStream.ToByteArray();
			while (num3 < num && outputStream.Position < num2)
			{
				byte b = array[(int)((uint)((UIntPtr)num3))];
				num3 += 1u;
				for (int i = 7; i >= 0; i--)
				{
					if (((int)b & 1 << i) == 0)
					{
						outputStream.Write(array[(int)((uint)((UIntPtr)num3))]);
						num3 += 1u;
					}
					else
					{
						int num5 = ((int)(array[(int)((uint)((UIntPtr)num3))] & 15) << 8 | (int)array[(int)((uint)((UIntPtr)(num3 + 1u)))]) + 1;
						int num6 = (array[(int)((uint)((UIntPtr)num3))] >> 4) + 3;
						num3 += 2u;
						for (int j = 0; j < num6; j++)
						{
							array2[(int)((IntPtr)((long)((ulong)num4 + (ulong)((long)j))))] = array2[(int)((IntPtr)((long)((ulong)num4 - (ulong)((long)num5) + (ulong)((long)j))))];
						}
						num4 += (uint)num6;
					}
					if (num3 >= num || outputStream.Position >= num2)
					{
						break;
					}
				}
			}
		}
		public override MemoryStream Compress(ref Stream data, string filename)
		{
			MemoryStream result;
			try
			{
				uint num = (uint)data.Length;
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = data.ToByteArray();
				uint num2 = 0u;
				uint num3 = 8u;
				if (data.Length > 16777215L)
				{
					throw new Exception("Input file is too large to compress.");
				}
				LzWindowDictionary lzWindowDictionary = new LzWindowDictionary();
				lzWindowDictionary.SetWindowSize(4096);
				lzWindowDictionary.SetMaxMatchAmount(18);
				memoryStream.Write("CXLZ");
				memoryStream.Write(16u | num << 8);
				while (num2 < num)
				{
					byte b = 0;
					uint num4 = num3;
					memoryStream.WriteByte(b);
					num3 += 1u;
					for (int i = 7; i >= 0; i--)
					{
						int[] array2 = lzWindowDictionary.Search(array, num2, num);
						if (array2[1] > 0)
						{
							b |= (byte)(1 << i);
							memoryStream.WriteByte((byte)((array2[1] - 3 & 15) << 4 | (array2[0] - 1 & 4095) >> 8));
							memoryStream.WriteByte((byte)(array2[0] - 1 & 255));
							lzWindowDictionary.AddEntryRange(array, (int)num2, array2[1]);
							lzWindowDictionary.SlideWindow(array2[1]);
							num2 += (uint)array2[1];
							num3 += 2u;
						}
						else
						{
							memoryStream.WriteByte(array[(int)((uint)((UIntPtr)num2))]);
							lzWindowDictionary.AddEntry(array, (int)num2);
							lzWindowDictionary.SlideWindow(1);
							num2 += 1u;
							num3 += 1u;
						}
						if (num2 >= num)
						{
							break;
						}
					}
					memoryStream.Seek((long)((ulong)num4), SeekOrigin.Begin);
					memoryStream.WriteByte(b);
					memoryStream.Seek((long)((ulong)num3), SeekOrigin.Begin);
				}
				result = memoryStream;
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public override bool Check(ref Stream data, string filename)
		{
			bool result;
			try
			{
				result = (data.ReadString(0L, 5) == "CXLZ\u0010");
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
