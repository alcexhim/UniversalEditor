using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Puyo.Internal.Compressors
{
	public class PRS : PuyoCompressionModule
	{
		public PRS()
		{
			base.Name = "PRS";
			base.CanCompress = false;
			base.CanDecompress = true;
		}
		public override MemoryStream Decompress(ref Stream data)
		{
			return this.Decompress(ref data, 0u);
		}
		public MemoryStream Decompress(ref Stream data, uint decompressedSize)
		{
			MemoryStream result;
			try
			{
				uint num = (uint)data.Length;
				uint num2 = 0u;
				uint num3 = 0u;
				byte[] array = data.ReadBytes(0L, num);
				List<byte> list = new List<byte>();
				while (num2 < num && (num3 < decompressedSize || decompressedSize == 0u))
				{
					byte b = array[(int)((uint)((UIntPtr)num2))];
					num2 += 1u;
					for (int i = 0; i < 8; i++)
					{
						if (((int)b & 1 << i) > 0)
						{
							list.Add(array[(int)((uint)((UIntPtr)num2))]);
							num2 += 1u;
							num3 += 1u;
						}
						else
						{
							i++;
							if (i >= 8)
							{
								i = 0;
								b = array[(int)((uint)((UIntPtr)num2))];
								num2 += 1u;
							}
							uint num4;
							uint num5;
							if (((int)b & 1 << i) > 0)
							{
								byte b2 = array[(int)((uint)((UIntPtr)num2))];
								byte b3 = array[(int)((uint)((UIntPtr)(num2 + 1u)))];
								num2 += 2u;
								if (num2 >= num)
								{
									break;
								}
								num4 = (uint)(((int)b3 << 8 | (int)b2) >> 3 | -8192);
								num5 = (uint)(b2 & 7);
								if (num5 == 0u)
								{
									num5 = (uint)(array[(int)((uint)((UIntPtr)num2))] + 1);
									num2 += 1u;
								}
								else
								{
									num5 += 2u;
								}
							}
							else
							{
								num5 = 0u;
								for (int j = 0; j < 2; j++)
								{
									i++;
									if (i >= 8)
									{
										i = 0;
										b = array[(int)((uint)((UIntPtr)num2))];
										num2 += 1u;
									}
									num4 = num5 << 1;
									num5 = (num4 | ((((int)b & 1 << i) > 0) ? 1u : 0u));
								}
								num4 = ((uint)array[(int)((uint)((UIntPtr)num2))] | 4294967040u);
								num5 += 2u;
								num2 += 1u;
							}
							int num6 = 0;
							while ((long)num6 < (long)((ulong)num5))
							{
								list.Add(list[(int)((ulong)(num3 + num4) + (ulong)((long)num6))]);
								num6++;
							}
							num3 += num5;
						}
						if (num2 >= num || (num3 >= decompressedSize && decompressedSize != 0u))
						{
							break;
						}
					}
				}
				result = new MemoryStream(list.ToArray());
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
				result = null;
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
				result = (Path.GetExtension(filename) == ".prs");
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
