//
//  Encoding.cs - provides a little more than System.Text.Encoding (but not much)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.IO
{
	public abstract class Encoding
	{
		private static Encoding _Default = new DefaultEncoding();
		public static Encoding Default { get { return _Default; } }
		private static Encoding _ASCII = new ASCIIEncoding();
		public static Encoding ASCII { get { return _ASCII; } }

		private static Encoding _UTF7 = new UTF7Encoding();
		public static Encoding UTF7 { get { return _UTF7; } }
		private static Encoding _UTF8 = new UTF8Encoding();
		public static Encoding UTF8 { get { return _UTF8; } }
		private static Encoding _UTF16LittleEndian = new UTF16LittleEndianEncoding();
		public static Encoding UTF16LittleEndian { get { return _UTF16LittleEndian; } }
		private static Encoding _UTF16BigEndian = new UTF16BigEndianEncoding();
		public static Encoding UTF16BigEndian { get { return _UTF16BigEndian; } }
		private static Encoding _UTF32 = new UTF32Encoding();
		public static Encoding UTF32 { get { return _UTF32; } }

		private static Encoding _ShiftJIS = new ShiftJISEncoding();
		public static Encoding ShiftJIS { get { return _ShiftJIS; } }

		private static Encoding _Windows1252 = new CodePageEncoding(1252);
		public static Encoding Windows1252 { get { return _Windows1252; } }

		public byte[] GetBytes(string value)
		{
			return GetBytes(value.ToCharArray());
		}
		public byte[] GetBytes(char[] chars)
		{
			return GetBytes(chars, 0, chars.Length);
		}
		public abstract byte[] GetBytes(char[] chars, int index, int count);

		public string GetString(byte[] bytes)
		{
			return GetString(bytes, 0, bytes.Length);
		}
		public string GetString(byte[] bytes, int index, int count)
		{
			char[] chars = GetChars(bytes, index, count);
			string retval = new string(chars);
			return retval;
		}

		public char[] GetChars(byte[] bytes)
		{
			return GetChars(bytes, 0, bytes.Length);
		}
		public abstract char[] GetChars(byte[] bytes, int index, int count);

		public virtual int GetMaxByteCount(int count)
		{
			return count;
		}
		public virtual int GetMaxCharCount(int count)
		{
			return count;
		}
		public virtual int GetByteCount(char[] chars, int index, int count)
		{
			return 1;
		}
		public int GetByteCount(char value)
		{
			return GetByteCount(new char[] { value }, 0, 1);
		}
		public int GetByteCount(char[] chars)
		{
			return GetByteCount(chars, 0, chars.Length);
		}
		public int GetByteCount(string value)
		{
			char[] chars = value.ToCharArray();
			return GetByteCount(chars, 0, chars.Length);
		}

		public static Encoding GetEncoding(int codepage)
		{
			return new CodePageEncoding(codepage);
		}
	}
	public class DefaultEncoding : Encoding
	{
		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] bytes = new byte[count];

			int min = System.Math.Min(chars.Length, count);
			for (int i = index; i < min + index; i++)
			{
				bytes[i - index] = (byte)(chars[i]);
			}
			return bytes;
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] retval = new char[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = (char)bytes[i];
			}
			return retval;
		}
	}
	public class CodePageEncoding : Encoding
	{
		private System.Text.Encoding _encoding = null;
		public CodePageEncoding(int codepage)
		{
			_encoding = System.Text.Encoding.GetEncoding(codepage);
		}

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class ASCIIEncoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.ASCII;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class UTF16BigEndianEncoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.BigEndianUnicode;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class UTF16LittleEndianEncoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.Unicode;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class UTF32Encoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.UTF32;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class UTF7Encoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.UTF7;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class UTF8Encoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.UTF8;

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			int maxCharCt = _encoding.GetMaxCharCount(bytes.Length);
			char[] chars = new char[maxCharCt];
			int reallen = _encoding.GetDecoder().GetChars(bytes, index, count, chars, 0);
			char[] realchars = new char[reallen];
			System.Array.Copy(chars, 0, realchars, 0, realchars.Length);
			return realchars;
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
	public class ShiftJISEncoding : Encoding
	{
		private System.Text.Encoding _encoding = System.Text.Encoding.GetEncoding("shift_jis");

		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return _encoding.GetBytes(chars, index, count);
		}
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return _encoding.GetChars(bytes, index, count);
		}
		public override int GetMaxByteCount(int count)
		{
			return _encoding.GetMaxByteCount(count);
		}
		public override int GetMaxCharCount(int count)
		{
			return _encoding.GetMaxCharCount(count);
		}
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return _encoding.GetByteCount(chars, index, count);
		}
	}
}
