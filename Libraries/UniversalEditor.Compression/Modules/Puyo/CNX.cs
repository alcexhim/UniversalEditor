using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class CNX : CompressionModule
	{
		public override string Name
		{
			get { return "CNX"; }
		}

		private string mvarOriginalFileNameExtension = String.Empty;
		public string OriginalFileNameExtension { get { return mvarOriginalFileNameExtension; } set { mvarOriginalFileNameExtension = value; } }

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			uint num = inputStream.ReadUInt(8L).SwapEndian() + 16u;
			uint num2 = inputStream.ReadUInt(12L).SwapEndian();
			uint num3 = 16u;
			byte[] array = inputStream.ReadBytes(0L, num);

			while (num3 < num && outputStream.Position < num2)
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
							outputStream.Write(array[(int)((uint)((UIntPtr)num3))]);
							num3 += 1u;
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
								long pos = outputStream.Position;
								outputStream.Position = pos - num6;
								byte val = (byte)outputStream.ReadByte();
								outputStream.Position = pos;
								outputStream.Write(val);
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
								outputStream.Write(array[(int)((uint)((UIntPtr)num3))]);
								num3 += 1u;
							}
							break;
						}
					}
				}
			}
		}
		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			uint num = (uint)inputStream.Length;
			uint num2 = 2048u;
			byte[] array = inputStream.ToByteArray();
			uint num3 = 0u;
			uint num4 = 16u;
			uint num5 = 16u;
			CompressionDictionaries.LzWindowDictionary lzWindowDictionary = new CompressionDictionaries.LzWindowDictionary();
			lzWindowDictionary.SetBlockSize(2048);
			lzWindowDictionary.SetMinMatchAmount(4);
			lzWindowDictionary.SetMaxMatchAmount(35);
			outputStream.Write("CNX\u0002");
			outputStream.Write(mvarOriginalFileNameExtension, 3);
			outputStream.WriteByte(16);
			outputStream.Write(0u);
			outputStream.Write(num.SwapEndian());
			while (num3 < num)
			{
				while (num5 < num2)
				{
					byte b = 0;
					uint num6 = num4;
					outputStream.WriteByte(b);
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
							outputStream.WriteByte((byte)((array3[1] - 3 & 15) << 4 | (array3[0] - 1 & 4095) >> 8));
							outputStream.WriteByte((byte)(array3[0] - 1 & 255));
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
								outputStream.WriteByte(list[0]);
							}
							else
							{
								b |= (byte)(3 << i * 2);
								outputStream.WriteByte((byte)list.Count);
								for (int j = 0; j < list.Count; j++)
								{
									outputStream.WriteByte(list[j]);
								}
							}
							outputStream.WriteByte(array[(int)((uint)((UIntPtr)num3))]);
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
					outputStream.Seek((long)((ulong)num6), SeekOrigin.Begin);
					outputStream.WriteByte(b);
					outputStream.Seek((long)((ulong)num4), SeekOrigin.Begin);
				}
			}
		}

		public string DecompressFilename(ref Stream data, string filename)
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
		public string CompressFilename(ref Stream data, string filename)
		{
			return Path.GetFileNameWithoutExtension(filename) + (Path.GetExtension(filename).IsAllUpperCase() ? ".CNX" : ".cnx");
		}
	}
}
