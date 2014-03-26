using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class CNX : CompressionModule
	{
		public CNX()
		{
			base.Name = "CNX";
			base.CanCompress = false;
			base.CanDecompress = true;
		}
		public override MemoryStream Decompress(ref Stream data)
		{
			uint num = data.ReadUInt(8L).SwapEndian() + 16u;
			uint num2 = data.ReadUInt(12L).SwapEndian();
			uint num3 = 16u;
			uint num4 = 0u;
			byte[] array = data.ReadBytes(0L, num);
			byte[] array2 = new byte[(int)((uint)((UIntPtr)num2))];
			while (num3 < num && num4 < num2)
			{
				byte b = array[(int)((uint)((UIntPtr)num3))];
				num3 += 1u;
				for (int i = 0; i < 4; i++)
				{
					switch (b >> i * 2 & 3)
					{
						case 0:
							{
								byte b2 = array[(int)((uint)((UIntPtr)num3))];
								num3 += (uint)((b2 & 255) + 1);
								i = 3;
								break;
							}
						case 1:
							{
								array2[(int)((uint)((UIntPtr)num4))] = array[(int)((uint)((UIntPtr)num3))];
								num3 += 1u;
								num4 += 1u;
								break;
							}
						case 2:
							{
								uint num5 = (uint)BitConverter.ToUInt16(array, (int)num3).SwapEndian();
								uint num6 = (num5 >> 5) + 1u;
								uint num7 = (num5 & 31u) + 4u;
								num3 += 2u;
								int num8 = 0;
								while ((long)num8 < (long)((ulong)num7))
								{
									array2[(int)((uint)((UIntPtr)num4))] = array2[(int)((uint)((UIntPtr)(num4 - num6)))];
									num4 += 1u;
									num8++;
								}
								break;
							}
						case 3:
							{
								byte b3 = array[(int)((uint)((UIntPtr)num3))];
								num3 += 1u;
								for (int j = 0; j < (int)b3; j++)
								{
									array2[(int)((uint)((UIntPtr)num4))] = array[(int)((uint)((UIntPtr)num3))];
									num3 += 1u;
									num4 += 1u;
								}
								break;
							}
					}
				}
			}
			return new MemoryStream(array2);
		}
		public override MemoryStream Compress(ref Stream data, string filename)
		{
			MemoryStream result;
			try
			{
				uint num = (uint)data.Length;
				uint num2 = 2048u;
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = data.ToByteArray();
				uint num3 = 0u;
				uint num4 = 16u;
				uint num5 = 16u;
				CompressionDictionaries.LzWindowDictionary lzWindowDictionary = new CompressionDictionaries.LzWindowDictionary();
				lzWindowDictionary.SetBlockSize(2048);
				lzWindowDictionary.SetMinMatchAmount(4);
				lzWindowDictionary.SetMaxMatchAmount(35);
				memoryStream.Write("CNX\u0002");
				memoryStream.Write(Path.HasExtension(filename) ? Path.GetExtension(filename).Substring(1) : string.Empty, 3);
				memoryStream.WriteByte(16);
				memoryStream.Write(0u);
				memoryStream.Write(num.SwapEndian());
				while (num3 < num)
				{
					while (num5 < num2)
					{
						byte b = 0;
						uint num6 = num4;
						memoryStream.WriteByte(b);
						num4 += 1u;
						for (int i = 0; i < 4; i++)
						{
							List<byte> list = new List<byte>();
							int[] array2 = new int[2];
							int[] array3;
							do
							{
								array3 = lzWindowDictionary.Search(array, num3, num);
								if (array3[1] == 0)
								{
									list.Add(array[(int)((uint)((UIntPtr)num3))]);
									num3 += 1u;
								}
							}
							while (array3[1] == 0 && list.Count < 255);
							if (array3[1] > 0)
							{
								b |= (byte)(2 << i * 2);
								memoryStream.WriteByte((byte)((array3[1] - 3 & 15) << 4 | (array3[0] - 1 & 4095) >> 8));
								memoryStream.WriteByte((byte)(array3[0] - 1 & 255));
								lzWindowDictionary.AddEntryRange(array, (int)num3, array3[1]);
								lzWindowDictionary.SlideWindow(array3[1]);
								num3 += (uint)array3[1];
								num4 += 2u;
							}
							else
							{
								if (list.Count == 1)
								{
									b |= (byte)(1 << i * 2);
									memoryStream.WriteByte(list[0]);
								}
								else
								{
									b |= (byte)(3 << i * 2);
									memoryStream.WriteByte((byte)list.Count);
									for (int j = 0; j < list.Count; j++)
									{
										memoryStream.WriteByte(list[j]);
									}
								}
								memoryStream.WriteByte(array[(int)((uint)((UIntPtr)num3))]);
								lzWindowDictionary.AddEntry(array, (int)num3);
								lzWindowDictionary.SlideWindow(1);
								num3 += 1u;
								num4 += 1u;
							}
							if (num3 >= num)
							{
								break;
							}
						}
						memoryStream.Seek((long)((ulong)num6), SeekOrigin.Begin);
						memoryStream.WriteByte(b);
						memoryStream.Seek((long)((ulong)num4), SeekOrigin.Begin);
					}
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
			string text = data.ReadString(4L, 3);
			string result;
			if (!(text == string.Empty))
			{
				result = Path.GetFileNameWithoutExtension(filename) + '.' + text;
			}
			else
			{
				result = filename;
			}
			return result;
		}
		public override string CompressFilename(ref Stream data, string filename)
		{
			return Path.GetFileNameWithoutExtension(filename) + (Path.GetExtension(filename).IsAllUpperCase() ? ".CNX" : ".cnx");
		}
		public override bool Check(ref Stream data, string filename)
		{
			bool result;
			try
			{
				result = (data.ReadString(0L, 4) == "CNX\u0002");
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
