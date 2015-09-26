using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class LZ01 : PuyoCompressionModule
	{
		public LZ01()
		{
			base.Name = "LZ01";
			base.CanCompress = true;
			base.CanDecompress = true;
		}
		public override MemoryStream Decompress(ref Stream data)
		{
			MemoryStream result;
			try
			{
				uint num = data.ReadUInt(4L);
				uint num2 = data.ReadUInt(8L);
				byte[] array = data.ToByteArray();
				byte[] array2 = new byte[(int)((uint)((UIntPtr)num2))];
				byte[] array3 = new byte[4096];
				uint num3 = 16u;
				uint num4 = 0u;
				uint num5 = 4078u;
				while (num3 < num && num4 < num2)
				{
					byte b = array[(int)((uint)((UIntPtr)num3))];
					num3 += 1u;
					for (int i = 0; i < 8; i++)
					{
						if (((int)b & 1 << i) > 0)
						{
							array2[(int)((uint)((UIntPtr)num4))] = array[(int)((uint)((UIntPtr)num3))];
							array3[(int)((uint)((UIntPtr)num5))] = array2[(int)((uint)((UIntPtr)num4))];
							num3 += 1u;
							num4 += 1u;
							num5 = (num5 + 1u & 4095u);
						}
						else
						{
							int num6 = (array[(int)((uint)((UIntPtr)(num3 + 1u)))] >> 4 & 15) << 8 | (int)array[(int)((uint)((UIntPtr)num3))];
							int num7 = (int)((array[(int)((uint)((UIntPtr)(num3 + 1u)))] & 15) + 3);
							num3 += 2u;
							for (int j = 0; j < num7; j++)
							{
								array2[(int)((IntPtr)((long)((ulong)num4 + (ulong)((long)j))))] = array3[num6 + j & 4095];
								array3[(int)((uint)((UIntPtr)num5))] = array2[(int)((IntPtr)((long)((ulong)num4 + (ulong)((long)j))))];
								num5 = (num5 + 1u & 4095u);
							}
							num4 += (uint)num7;
						}
						if (num3 >= num || num4 >= num2)
						{
							break;
						}
					}
				}
				result = new MemoryStream(array2);
			}
			catch
			{
				result = null;
			}
			return result;
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
				uint num3 = 16u;
				LzBufferDictionary lzBufferDictionary = new LzBufferDictionary();
				lzBufferDictionary.SetBufferSize(4096);
				lzBufferDictionary.SetBufferStart(4078);
				lzBufferDictionary.SetMaxMatchAmount(18);
				memoryStream.Write("LZ01");
				memoryStream.Write(0u);
				memoryStream.Write(num);
				memoryStream.Seek(4L, SeekOrigin.Current);
				while (num2 < num)
				{
					byte b = 0;
					uint num4 = num3;
					memoryStream.WriteByte(b);
					num3 += 1u;
					for (int i = 0; i < 8; i++)
					{
						int[] array2 = lzBufferDictionary.Search(array, num2, num);
						if (array2[1] > 0)
						{
							memoryStream.WriteByte((byte)(array2[0] & 255));
							memoryStream.WriteByte((byte)((array2[0] & 3840) >> 4 | (array2[1] - 3 & 15)));
							lzBufferDictionary.AddEntryRange(array, (int)num2, array2[1]);
							num2 += (uint)array2[1];
							num3 += 2u;
						}
						else
						{
							b |= (byte)(1 << i);
							memoryStream.WriteByte(array[(int)((uint)((UIntPtr)num2))]);
							lzBufferDictionary.AddEntry(array, (int)num2);
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
				memoryStream.Seek(4L, SeekOrigin.Begin);
				memoryStream.Write((uint)memoryStream.Length);
				memoryStream.Seek(0L, SeekOrigin.End);
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
				result = (data.ReadString(0L, 4) == "LZ01");
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
