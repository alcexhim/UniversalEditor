using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

using UniversalEditor.Compression.LZW.Internal;

namespace UniversalEditor.Compression.LZW
{
	public class LZWStream
	{
		private const int MAX_CODES = 4096;
		private const int BYTE_SIZE = 8;
		private const int EXCESS = 4;
		private const int ALPHA = 256;
		private const int MASK1 = 255;
		private const int MASK2 = 15;
		private static int c_leftOver;
		private static bool c_bitsLeftOver;
		private static int[] s;
		private static int size;
		private static int d_leftOver;
		private static bool d_bitsLeftOver;
		private static int d_size = 0;
		private static Element[] h;
		private static void COutput(int pcode, MemoryStream out_Renamed)
		{
			if (LZWStream.c_bitsLeftOver)
			{
				int d = pcode & 255;
				int c = (LZWStream.c_leftOver << 4) + (pcode >> 8);
				out_Renamed.WriteByte((byte)c);
				out_Renamed.WriteByte((byte)d);
				LZWStream.c_bitsLeftOver = false;
			}
			else
			{
				LZWStream.c_leftOver = (pcode & 15);
				int c = pcode >> 4;
				out_Renamed.WriteByte((byte)c);
				LZWStream.c_bitsLeftOver = true;
			}
		}
		public static byte[] Compress(string input)
		{
			return LZWStream.Compress(input, Encoding.Default);
		}
		public static byte[] Compress(string input, Encoding encoding)
		{
			return LZWStream.Compress(encoding.GetBytes(input));
		}
		public static byte[] Compress(byte[] input)
		{
			Hashtable table = new Hashtable();
			for (int i = 0; i < 256; i++)
			{
				table.Add(i, i);
			}
			int codeUsed = 256;
			MemoryStream in_Renamed = new MemoryStream(input);
			MemoryStream out_Renamed = new MemoryStream();
			int c = in_Renamed.ReadByte();
			if (c != -1)
			{
				int pcode = c;
				c = in_Renamed.ReadByte();
				while (in_Renamed.Position < in_Renamed.Length)
				{
					int j = (pcode << 8) + c;
					if (!table.ContainsKey(j))
					{
						LZWStream.COutput(pcode, out_Renamed);
						if (codeUsed < 4096)
						{
							table.Add((pcode << 8) + c, codeUsed++);
						}
						pcode = c;
					}
					else
					{
						int e = (int)table[j];
						pcode = e;
					}
					c = in_Renamed.ReadByte();
				}
				LZWStream.COutput(pcode, out_Renamed);
				if (LZWStream.c_bitsLeftOver)
				{
					out_Renamed.WriteByte((byte)(LZWStream.c_leftOver << 4));
				}
			}
			in_Renamed.Close();
			out_Renamed.Flush();
			out_Renamed.Close();
			return out_Renamed.ToArray();
		}
		public static byte[] Decompress(byte[] input)
		{
			MemoryStream in_Renamed = new MemoryStream(input);
			MemoryStream out_Renamed = new MemoryStream();
			int codeUsed = 256;
			LZWStream.s = new int[4096];
			LZWStream.h = new Element[4096];
			int pcode = LZWStream.DGetCode(in_Renamed);
			if (pcode >= 0)
			{
				LZWStream.s[0] = pcode;
				out_Renamed.WriteByte((byte)LZWStream.s[0]);
				LZWStream.size = 0;
				while (true)
				{
					int ccode = LZWStream.DGetCode(in_Renamed);
					if (ccode < 0)
					{
						break;
					}
					if (ccode < codeUsed)
					{
						LZWStream.DOutput(ccode, out_Renamed);
						if (codeUsed < 4096)
						{
							LZWStream.h[codeUsed++] = new Element(pcode, LZWStream.s[LZWStream.size]);
						}
					}
					else
					{
						LZWStream.h[codeUsed++] = new Element(pcode, LZWStream.s[LZWStream.size]);
						LZWStream.DOutput(ccode, out_Renamed);
					}
					pcode = ccode;
				}
			}
			out_Renamed.Flush();
			out_Renamed.Close();
			in_Renamed.Close();
			return out_Renamed.ToArray();
		}
		private static void DOutput(int code, MemoryStream out_Renamed)
		{
			LZWStream.d_size = -1;
			while (code >= 256)
			{
				LZWStream.s[++LZWStream.d_size] = LZWStream.h[code].suffix;
				code = LZWStream.h[code].prefix;
			}
			LZWStream.s[++LZWStream.d_size] = code;
			for (int i = LZWStream.d_size; i >= 0; i--)
			{
				out_Renamed.WriteByte((byte)LZWStream.s[i]);
			}
		}
		private static int DGetCode(MemoryStream in_Renamed)
		{
			int c = in_Renamed.ReadByte();
			int result;
			if (c == -1)
			{
				result = -1;
			}
			else
			{
				int code;
				if (LZWStream.d_bitsLeftOver)
				{
					code = (LZWStream.d_leftOver << 8) + c;
				}
				else
				{
					int d = in_Renamed.ReadByte();
					code = (c << 4) + (d >> 4);
					LZWStream.d_leftOver = (d & 15);
				}
				LZWStream.d_bitsLeftOver = !LZWStream.d_bitsLeftOver;
				result = code;
			}
			return result;
		}
	}
}
