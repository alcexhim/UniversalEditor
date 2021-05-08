using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class ONZ : PuyoCompressionModule
	{
		public ONZ()
		{
			base.Name = "LZ11";
			base.CanCompress = true;
			base.CanDecompress = true;
		}
		public override MemoryStream Decompress(ref Stream data)
		{
			MemoryStream result;
			try
			{
				uint num = (uint)data.Length;
				uint num2 = data.ReadUInt(0L) >> 8;
				uint num3 = 4u;
				uint num4 = 0u;
				if (num2 == 0u)
				{
					num2 = data.ReadUInt(4L);
					num3 += 4u;
				}
				byte[] array = data.ToByteArray();
				byte[] array2 = new byte[(int)((uint)((UIntPtr)num2))];
				while (num3 < num && num4 < num2)
				{
					byte b = array[(int)((uint)((UIntPtr)num3))];
					num3 += 1u;
					for (int i = 7; i >= 0; i--)
					{
						if (((int)b & 1 << i) == 0)
						{
							array2[(int)((uint)((UIntPtr)num4))] = array[(int)((uint)((UIntPtr)num3))];
							num3 += 1u;
							num4 += 1u;
						}
						else
						{
							int num5;
							int num6;
							switch (array[(int)((uint)((UIntPtr)num3))] >> 4)
							{
								case 0:
									{
										num5 = ((int)(array[(int)((uint)((UIntPtr)(num3 + 1u)))] & 15) << 8 | (int)array[(int)((uint)((UIntPtr)(num3 + 2u)))]) + 1;
										num6 = ((int)(array[(int)((uint)((UIntPtr)num3))] & 15) << 4 | array[(int)((uint)((UIntPtr)(num3 + 1u)))] >> 4) + 17;
										num3 += 3u;
										break;
									}
								case 1:
									{
										num5 = ((int)(array[(int)((uint)((UIntPtr)(num3 + 2u)))] & 15) << 8 | (int)array[(int)((uint)((UIntPtr)(num3 + 3u)))]) + 1;
										num6 = ((int)(array[(int)((uint)((UIntPtr)num3))] & 15) << 12 | (int)array[(int)((uint)((UIntPtr)(num3 + 1u)))] << 4 | array[(int)((uint)((UIntPtr)(num3 + 2u)))] >> 4) + 273;
										num3 += 4u;
										break;
									}
								default:
									{
										num5 = ((int)(array[(int)((uint)((UIntPtr)num3))] & 15) << 8 | (int)array[(int)((uint)((UIntPtr)(num3 + 1u)))]) + 1;
										num6 = (array[(int)((uint)((UIntPtr)num3))] >> 4) + 1;
										num3 += 2u;
										break;
									}
							}
							for (int j = 0; j < num6; j++)
							{
								array2[(int)((IntPtr)((long)((ulong)num4 + (ulong)((long)j))))] = array2[(int)((IntPtr)((long)((ulong)num4 - (ulong)((long)num5) + (ulong)((long)j))))];
							}
							num4 += (uint)num6;
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
				uint num3 = 4u;
				LzWindowDictionary lzWindowDictionary = new LzWindowDictionary();
				lzWindowDictionary.SetWindowSize(4096);
				lzWindowDictionary.SetMaxMatchAmount(65808);
				if (data.Length <= 16777215L)
				{
					memoryStream.Write(17u | num << 8);
				}
				else
				{
					memoryStream.Write(17u);
					memoryStream.Write(num);
					num3 += 4u;
				}
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
							if (array2[1] <= 16)
							{
								memoryStream.WriteByte((byte)((array2[1] - 1 & 15) << 4 | (array2[0] - 1 & 4095) >> 8));
								memoryStream.WriteByte((byte)(array2[0] - 1 & 255));
								num3 += 2u;
							}
							else
							{
								if (array2[1] <= 272)
								{
									memoryStream.WriteByte((byte)((array2[1] - 17 & 255) >> 4));
									memoryStream.WriteByte((byte)((array2[1] - 17 & 15) << 4 | (array2[0] - 1 & 4095) >> 8));
									memoryStream.WriteByte((byte)(array2[0] - 1 & 255));
									num3 += 3u;
								}
								else
								{
									memoryStream.WriteByte((byte)(16 | (array2[1] - 273 & 65535) >> 12));
									memoryStream.WriteByte((byte)((array2[1] - 273 & 4095) >> 4));
									memoryStream.WriteByte((byte)((array2[1] - 273 & 15) << 4 | (array2[0] - 1 & 4095) >> 8));
									memoryStream.WriteByte((byte)(array2[0] - 1 & 255));
									num3 += 4u;
								}
							}
							lzWindowDictionary.AddEntryRange(array, (int)num2, array2[1]);
							lzWindowDictionary.SlideWindow(array2[1]);
							num2 += (uint)array2[1];
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
		public override string DecompressFilename(ref Stream data, string filename)
		{
			string result;
			if (Path.GetExtension(filename).ToLower() == ".onz")
			{
				result = Path.GetFileNameWithoutExtension(filename) + (Path.GetExtension(filename).IsAllUpperCase() ? ".ONE" : ".one");
			}
			else
			{
				result = filename;
			}
			return result;
		}
		public override string CompressFilename(ref Stream data, string filename)
		{
			string result;
			if (Path.GetExtension(filename).ToLower() == ".one")
			{
				result = Path.GetFileNameWithoutExtension(filename) + (Path.GetExtension(filename).IsAllUpperCase() ? ".ONZ" : ".onz");
			}
			else
			{
				result = filename;
			}
			return result;
		}
		public override bool Check(ref Stream data, string filename)
		{
			bool result;
			try
			{
				result = (data.ReadString(0L, 1) == "\u0011" && !Compression.Dictionary[CompressionFormat.PRS].Check(ref data, filename));
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
