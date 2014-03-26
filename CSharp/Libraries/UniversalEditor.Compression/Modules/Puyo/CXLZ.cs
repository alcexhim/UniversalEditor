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

		protected override void DecompressInternal(Stream inputStream, Stream outputStream, int inputLength, int outputLength)
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
                            long pos = inputStream.Position;
                            inputStream.Position = (inputStream.Position - num5 + j);

							outputStream.WriteByte((byte)inputStream.ReadByte());

                            inputStream.Position = pos;
						}
					}
					if (num3 >= num || outputStream.Position >= num2)
					{
						break;
					}
				}
			}
		}
		protected override void CompressInternal(Stream inputStream, Stream outputStream)
		{
			uint num = (uint)inputStream.Length;
			if (inputStream.Length > 16777215L) throw new Exception("Input file is too large to compress.");

			byte[] array = inputStream.ToByteArray();
			uint num2 = 0u;
			uint num3 = 8u;
			LzWindowDictionary lzWindowDictionary = new LzWindowDictionary();
			lzWindowDictionary.SetWindowSize(4096);
			lzWindowDictionary.SetMaxMatchAmount(18);
			outputStream.Write("CXLZ");
			outputStream.Write(16u | num << 8);
			while (num2 < num)
			{
				byte b = 0;
				uint num4 = num3;
				outputStream.WriteByte(b);
				num3 += 1u;
				for (int i = 7; i >= 0; i--)
				{
					int[] array2 = lzWindowDictionary.Search(array, num2, num);
					if (array2[1] > 0)
					{
						b |= (byte)(1 << i);
						outputStream.WriteByte((byte)((array2[1] - 3 & 15) << 4 | (array2[0] - 1 & 4095) >> 8));
						outputStream.WriteByte((byte)(array2[0] - 1 & 255));
						lzWindowDictionary.AddEntryRange(array, (int)num2, array2[1]);
						lzWindowDictionary.SlideWindow(array2[1]);
						num2 += (uint)array2[1];
						num3 += 2u;
					}
					else
					{
						outputStream.WriteByte(array[(int)((uint)((UIntPtr)num2))]);
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
				outputStream.Seek((long)((ulong)num4), SeekOrigin.Begin);
				outputStream.WriteByte(b);
				outputStream.Seek((long)((ulong)num3), SeekOrigin.Begin);
			}
		}
		public bool Check(Stream data, string filename)
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
