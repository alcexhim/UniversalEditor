using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Puyo.Internal.CompressionDictionaries;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class LZ00 : PuyoCompressionModule
	{
		public LZ00()
		{
			base.Name = "LZ00";
			base.CanCompress = true;
			base.CanDecompress = true;
		}
		public override MemoryStream Decompress(ref Stream data)
		{
			MemoryStream result;
			try
			{
				uint num = data.ReadUInt(4L);
				uint num2 = data.ReadUInt(48L);
				uint xValue = data.ReadUInt(52L);
				byte[] array = data.ToByteArray();
				byte[] array2 = new byte[(int)((uint)((UIntPtr)num2))];
				byte[] array3 = new byte[4096];
				uint num3 = 64u;
				uint num4 = 0u;
				uint num5 = 4078u;
				while (num3 < num && num4 < num2)
				{
					xValue = this.GetNewMagicValue(xValue);
					byte b = this.DecryptByte(array[(int)((uint)((UIntPtr)num3))], xValue);
					num3 += 1u;
					for (int i = 0; i < 8; i++)
					{
						if (((int)b & 1 << i) > 0)
						{
							xValue = this.GetNewMagicValue(xValue);
							array2[(int)((uint)((UIntPtr)num4))] = this.DecryptByte(array[(int)((uint)((UIntPtr)num3))], xValue);
							array3[(int)((uint)((UIntPtr)num5))] = array2[(int)((uint)((UIntPtr)num4))];
							num3 += 1u;
							num4 += 1u;
							num5 = (num5 + 1u & 4095u);
						}
						else
						{
							xValue = this.GetNewMagicValue(xValue);
							byte b2 = this.DecryptByte(array[(int)((uint)((UIntPtr)num3))], xValue);
							xValue = this.GetNewMagicValue(xValue);
							byte b3 = this.DecryptByte(array[(int)((uint)((UIntPtr)(num3 + 1u)))], xValue);
							int num6 = (b3 >> 4 & 15) << 8 | (int)b2;
							int num7 = (int)((b3 & 15) + 3);
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
				uint num3 = 64u;
				uint num4 = (uint)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
				LzBufferDictionary lzBufferDictionary = new LzBufferDictionary();
				lzBufferDictionary.SetBufferSize(4096);
				lzBufferDictionary.SetBufferStart(4078);
				lzBufferDictionary.SetMaxMatchAmount(18);
				memoryStream.Write("LZ00");
				memoryStream.Write(0u);
				memoryStream.Seek(8L, SeekOrigin.Current);
				if (Path.GetExtension(filename).ToLower() == ".mrz")
				{
					filename = Path.GetFileNameWithoutExtension(filename) + ".mrg";
				}
				else
				{
					if (Path.GetExtension(filename).ToLower() == ".tez")
					{
						filename = Path.GetFileNameWithoutExtension(filename) + ".tex";
					}
				}
				memoryStream.Write(filename, 31, 32, Encoding.GetEncoding("Shift_JIS"));
				memoryStream.Write(num);
				memoryStream.Write(num4);
				memoryStream.Seek(8L, SeekOrigin.Current);
				while (num2 < num)
				{
					num4 = this.GetNewMagicValue(num4);
					byte b = 0;
					uint num5 = num3;
					uint xValue = num4;
					memoryStream.WriteByte(b);
					num3 += 1u;
					for (int i = 0; i < 8; i++)
					{
						int[] array2 = lzBufferDictionary.Search(array, num2, num);
						if (array2[1] > 0)
						{
							num4 = this.GetNewMagicValue(num4);
							memoryStream.WriteByte(this.EncryptByte((byte)(array2[0] & 255), num4));
							num4 = this.GetNewMagicValue(num4);
							memoryStream.WriteByte(this.EncryptByte((byte)((array2[0] & 3840) >> 4 | (array2[1] - 3 & 15)), num4));
							lzBufferDictionary.AddEntryRange(array, (int)num2, array2[1]);
							num2 += (uint)array2[1];
							num3 += 2u;
						}
						else
						{
							b |= (byte)(1 << i);
							num4 = this.GetNewMagicValue(num4);
							memoryStream.WriteByte(this.EncryptByte(array[(int)((uint)((UIntPtr)num2))], num4));
							lzBufferDictionary.AddEntry(array, (int)num2);
							num2 += 1u;
							num3 += 1u;
						}
						if (num2 >= num)
						{
							break;
						}
					}
					memoryStream.Seek((long)((ulong)num5), SeekOrigin.Begin);
					memoryStream.WriteByte(this.EncryptByte(b, xValue));
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
		private uint GetNewMagicValue(uint xValue)
		{
			uint num = ((((xValue << 1) + xValue << 5) - xValue << 5) + xValue << 7) - xValue;
			num = (num << 6) - num;
			num = (num << 4) - num;
			return (num << 2) - num + 12345u;
		}
		private byte DecryptByte(byte value, uint xValue)
		{
			uint num = xValue >> 16 & 32767u;
			return (byte)((uint)value ^ (num << 8) - num >> 15);
		}
		private byte EncryptByte(byte value, uint xValue)
		{
			uint num = xValue >> 16 & 32767u;
			return (byte)((uint)value ^ (num << 8) - num >> 15);
		}
		public override string DecompressFilename(ref Stream data, string filename)
		{
			string text = data.ReadString(16L, 32, Encoding.GetEncoding("Shift_JIS"));
			string result;
			if (!(text == string.Empty))
			{
				result = text;
			}
			else
			{
				result = filename;
			}
			return result;
		}
		public override string CompressFilename(ref Stream data, string filename)
		{
			string extension;
			string result;
			if ((extension = Path.GetExtension(filename)) != null)
			{
				if (extension == ".mrg")
				{
					result = Path.GetFileNameWithoutExtension(filename) + ".mrz";
					return result;
				}
				if (extension == ".tex")
				{
					result = Path.GetFileNameWithoutExtension(filename) + ".tez";
					return result;
				}
			}
			result = filename;
			return result;
		}
		public override bool Check(ref Stream data, string filename)
		{
			bool result;
			try
			{
				result = (data.ReadString(0L, 4) == "LZ00");
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
