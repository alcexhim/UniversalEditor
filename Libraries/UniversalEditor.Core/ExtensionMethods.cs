//
//  ExtensionMethods.cs - various useful (maybe?) extension methods
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

using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor
{
	public static class ExtensionMethods
	{
		public static long GetItemOffset(this List<string> list, int index, int additionalPadding = 0)
		{
			long offset = 0;
			for (int i = 0; i < index; i++)
			{
				if (list[i] == null)
				{
					offset += additionalPadding;
				}
				else
				{
					offset += list[i].Length + additionalPadding;
				}
			}
			return offset;
		}

		public static bool get_EndOfStream(this System.IO.Stream stream)
		{
			return (stream.Position == stream.Length);
		}

		/// <summary>
		/// Gets an int value representing the subset of bits from a single Byte.
		/// </summary>
		/// <param name="b">The Byte used to get the subset of bits from.</param>
		/// <param name="offset">The offset of bits starting from the right.</param>
		/// <param name="count">The number of bits to read.</param>
		/// <returns>
		/// An int value representing the subset of bits.
		/// </returns>
		/// <remarks>
		/// Given -> b = 00110101
		/// A call to GetBits(b, 2, 4)
		/// GetBits looks at the following bits in the byte -> 00{1101}00
		/// Returns 1101 as an int (13)
		/// </remarks>
		public static int GetBits(this byte value, int offset, int count)
		{
			return (value >> offset) & ((1 << count) - 1);
		}
		public static int GetBits(this short value, int offset, int count)
		{
			return (value >> offset) & ((1 << count) - 1);
		}
		public static int GetBits(this int value, int offset, int count)
		{
			return (value >> offset) & ((1 << count) - 1);
		}

		public static byte[] ToBits(this byte value)
		{
			byte[] bits = new byte[8];
			for (int i = 0; i < bits.Length; i++)
			{
				bits[i] = (byte)value.GetBits(i, 1);
			}
			return bits;
		}

		[CLSCompliant(false)]
		public static int GetBits(this ushort value, int offset, int count)
		{
			return (value >> offset) & ((1 << count) - 1);
		}
		[CLSCompliant(false)]
		public static byte[] ToBits(this ushort value)
		{
			byte[] bits = new byte[16];
			for (int i = 0; i < bits.Length; i++)
			{
				bits[i] = (byte)value.GetBits(i, 1);
			}
			return bits;
		}

		public static void AddRange(this System.Collections.Specialized.StringCollection coll, params string[] values)
		{
			coll.AddRange(values);
		}

		public static bool Match(this Array array1, Array array2)
		{
			if (array1.Length != array2.Length)
			{
				return false;
			}

			System.Collections.IEnumerator en1 = array1.GetEnumerator();
			System.Collections.IEnumerator en2 = array2.GetEnumerator();

			en1.MoveNext();
			en2.MoveNext();

			try
			{
				while (en1.Current != null)
				{
					if (en2.Current == null) return false;
					if (!en1.Current.Equals(en2.Current)) return false;

					en1.MoveNext();
					en2.MoveNext();
				}
			}
			catch (InvalidOperationException)
			{
			}
			return true;
		}

		/// <summary>
		/// Returns <see langword="true" /> if <paramref name="value" /> is equal
		/// to any one of the values in <paramref name="anyOf" />.
		/// </summary>
		/// <returns><c>true</c>, if a match was found; <c>false</c> otherwise.</returns>
		/// <param name="value">The value to test.</param>
		/// <param name="anyOf">
		/// An array of items of type <typeparamref name="T" /> to check equality
		/// against <paramref name="value" />.
		/// </param>
		public static bool EqualsAny<T>(this IEquatable<T> value, params T[] anyOf)
		{
			for (int i = 0; i < anyOf.Length; i++)
			{
				T any = anyOf[i];
				if (value.Equals(any))
				{
					return true;
				}
			}
			return false;
		}

		public static bool ContainsAny(this string value, params string[] anyOf)
		{
			bool result;
			for (int i = 0; i < anyOf.Length; i++)
			{
				string any = anyOf[i];
				if (value.Contains(any))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsAny(this string value, params char[] anyOf)
		{
			bool result;
			for (int i = 0; i < anyOf.Length; i++)
			{
				char any = anyOf[i];
				if (value.Contains(any.ToString()))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static int IndexOfAny(this string value, params string[] anyOf)
		{
			int result;
			for (int i = 0; i < anyOf.Length; i++)
			{
				string any = anyOf[i];
				int index = value.IndexOf(any);
				if (index > -1)
				{
					result = index;
					return result;
				}
			}
			result = -1;
			return result;
		}
		public static string Capitalize(this string value)
		{
			string result;
			if (string.IsNullOrEmpty(value))
			{
				result = value;
			}
			else
			{
				if (value.Length == 1)
				{
					result = value.ToUpper();
				}
				else
				{
					result = value.Substring(0, 1).ToUpper() + value.Substring(1);
				}
			}
			return result;
		}

		#region Splitting
		public static T[] Split<T>(this string value, params char[] separator)
		{
			return value.Split<T>(separator, -1, StringSplitOptions.None);
		}
		public static T[] Split<T>(this string value, char[] separator, int count)
		{
			return value.Split<T>(separator, count, StringSplitOptions.None);
		}
		public static T[] Split<T>(this string value, char[] separator, int count, StringSplitOptions options)
		{
			string[] separators = new string[separator.Length];
			for (int i = 0; i < separator.Length; i++)
			{
				separators[i] = separator[i].ToString();
			}
			return value.Split<T>(separators, count, options);
		}
		public static T[] Split<T>(this string value, params string[] separator)
		{
			return value.Split<T>(separator, -1, StringSplitOptions.None);
		}
		public static T[] Split<T>(this string value, string[] separator, int count)
		{
			return value.Split<T>(separator, count, StringSplitOptions.None);
		}
		public static T[] Split<T>(this string value, string[] separator, int count, StringSplitOptions options)
		{
			string[] splitt = null;
			if (count < 0)
			{
				splitt = value.Split(separator, options);
			}
			else
			{
				splitt = value.Split(separator, count, options);
			}
			T[] values = new T[splitt.Length];
			for (int i = 0; i < splitt.Length; i++)
			{
				if (!string.IsNullOrEmpty(splitt[i]))
				{
					values[i] = (T)Convert.ChangeType(splitt[i], typeof(T));
				}
			}
			return values;
		}
		public static string[] Split(this string value, string separator)
		{
			return value.Split(new string[] { separator });
		}
		public static string[] Split(this string value, string[] separator)
		{
			return value.Split(separator, StringSplitOptions.None);
		}
		public static string[] Split(this string value, string[] separator, string ignore)
		{
			return value.Split(separator, ignore, ignore);
		}
		public static string[] Split(this string value, string[] separator, string ignoreBegin, string ignoreEnd)
		{
			return value.Split(separator, ignoreBegin, ignoreEnd, StringSplitOptions.None, -1);
		}
		public static string[] Split(this string value, string[] separator, StringSplitOptions options, int count, string ignore)
		{
			return value.Split(separator, ignore, ignore, options, count);
		}
		public static string[] Split(this string value, char[] separator, string ignore)
		{
			return value.Split(separator, ignore, ignore);
		}
		public static string[] Split(this string value, char[] separator, string ignoreBegin, string ignoreEnd)
		{
			return value.Split(separator, ignoreBegin, ignoreEnd, StringSplitOptions.None, -1);
		}
		public static string[] Split(this string value, char[] separator, string ignore, StringSplitOptions options, int count)
		{
			return value.Split(separator, ignore, ignore, options, count);
		}
		public static string[] Split(this string value, char[] separator, string ignoreBegin, string ignoreEnd, StringSplitOptions options, int count)
		{
			List<string> entries = new List<string>();
			for (int i = 0; i < separator.Length; i++)
			{
				char sep = separator[i];
				entries.Add(sep.ToString());
			}
			return value.Split(entries.ToArray(), ignoreBegin, ignoreEnd, options, count);
		}
		public static string[] Split(this string value, string[] separator, string ignoreBegin, string ignoreEnd, StringSplitOptions options, int count)
		{
			return value.Split(separator, ignoreBegin, ignoreEnd, options, count, true);
		}
		public static string[] Split(this string value, string[] separator, string ignoreBegin, string ignoreEnd, StringSplitOptions options, int count, bool discardIgnoreString)
		{
			List<string> entries = new List<string>();
			bool ignoring = false;
			bool continueOutside = false;
			string next = string.Empty;
			int i = 0;
			while (i < value.Length)
			{
				if (i + ignoreBegin.Length > value.Length)
				{
					goto IL_70;
				}
				if (ignoring || !(value.Substring(i, ignoreBegin.Length) == ignoreBegin))
				{
					goto IL_70;
				}
				ignoring = true;
				if (!discardIgnoreString)
				{
					next += ignoreBegin;
				}
			IL_16F:
				i++;
				continue;
			IL_70:
				if (i + ignoreEnd.Length <= value.Length)
				{
					if (ignoring && value.Substring(i, ignoreEnd.Length) == ignoreEnd)
					{
						ignoring = false;
						if (!discardIgnoreString)
						{
							next += ignoreEnd;
						}
						goto IL_16F;
					}
				}
				if (!ignoring)
				{
					int j = 0;
					while (j < separator.Length)
					{
						if (i + separator[j].Length <= value.Length)
						{
							if (value.Substring(i, separator[j].Length) == separator[j])
							{
								if (count > -1 && (entries.Count >= count - 1))
								{
									next = value.Substring(i - next.Length);
									entries.Add(next);
									i = value.Length - 1;
									break;
								}
								else
								{
									entries.Add(next);
									next = string.Empty;
									i += separator[j].Length - 1;
									continueOutside = true;
								}
							}
						}

						j++;
						continue;
					}
				}
				if (continueOutside)
				{
					continueOutside = false;
					goto IL_16F;
				}
				next += value[i];
				goto IL_16F;
			}
			if (!string.IsNullOrEmpty(next))
			{
				entries.Add(next);
				next = null;
			}
			return entries.ToArray();
		}
		#endregion

		#region Endianness
		[CLSCompliant(false)]
		public static ushort SwapEndian(this ushort x)
		{
			return (ushort)(x >> 8 | (int)x << 8);
		}
		[CLSCompliant(false)]
		public static ushort SwapEndian(this short x)
		{
			return ((ushort)x).SwapEndian();
		}
		[CLSCompliant(false)]
		public static uint SwapEndian(this uint x)
		{
			return x >> 24 | (x << 8 & 16711680u) | (x >> 8 & 65280u) | x << 24;
		}
		[CLSCompliant(false)]
		public static uint SwapEndian(this int x)
		{
			return ((uint)x).SwapEndian();
		}
		[CLSCompliant(false)]
		public static ushort Swap(short x)
		{
			return x.SwapEndian();
		}
		[CLSCompliant(false)]
		public static ushort Swap(ushort x)
		{
			return x.SwapEndian();
		}
		[CLSCompliant(false)]
		public static uint Swap(int x)
		{
			return x.SwapEndian();
		}
		[CLSCompliant(false)]
		public static uint Swap(uint x)
		{
			return x.SwapEndian();
		}
		#endregion
		#region Number
		[CLSCompliant(false)]
		public static uint RoundUp(this uint number, int multiple)
		{
			uint result;
			if ((ulong)number % (ulong)((long)multiple) == 0uL)
			{
				result = number;
			}
			else
			{
				result = (uint)((ulong)number + (ulong)((long)multiple - (long)((ulong)number % (ulong)((long)multiple))));
			}
			return result;
		}
		[CLSCompliant(false)]
		public static ulong RoundUp(this ulong number, int multiple)
		{
			ulong result;
			if ((ulong)number % (ulong)((long)multiple) == 0uL)
			{
				result = number;
			}
			else
			{
				result = (ulong)((ulong)number + (ulong)((long)multiple - (long)((ulong)number % (ulong)((long)multiple))));
			}
			return result;
		}
		public static int RoundUp(this int number, int multiple)
		{
			int result;
			if (number % multiple == 0)
			{
				result = number;
			}
			else
			{
				result = number + (multiple - number % multiple);
			}
			return result;
		}
		public static int Digits(this int number)
		{
			return number.ToString().Length;
		}
		public static int Digits(this long number)
		{
			return number.ToString().Length;
		}

		public static short UpperWord(this short number)
		{
			return (short)(number >> 16);
		}
		public static short LowerWord(this short number)
		{
			return (short)(number & 0xFFFF);
		}
		[CLSCompliant(false)]
		public static ushort UpperWord(this ushort number)
		{
			return (ushort)(number >> 16);
		}
		[CLSCompliant(false)]
		public static ushort LowerWord(this ushort number)
		{
			return (ushort)(number & 0xFFFF);
		}
		public static int UpperWord(this int number)
		{
			return (int)(number >> 16);
		}
		public static int LowerWord(this int number)
		{
			return (int)(number & 0xFFFF);
		}
		[CLSCompliant(false)]
		public static uint UpperWord(this uint number)
		{
			return (uint)(number >> 16);
		}
		[CLSCompliant(false)]
		public static uint LowerWord(this uint number)
		{
			return (uint)(number & 0xFFFF);
		}
		public static long UpperWord(this long number)
		{
			return (long)(number >> 16);
		}
		public static long LowerWord(this long number)
		{
			return (long)(number & 0xFFFF);
		}
		[CLSCompliant(false)]
		public static ulong UpperWord(this ulong number)
		{
			return (ulong)(number >> 16);
		}
		[CLSCompliant(false)]
		public static ulong LowerWord(this ulong number)
		{
			return (ulong)(number & 0xFFFF);
		}
		#endregion
		#region StreamReader Extensions

		public static byte ReadByte(this System.IO.Stream stream, long offset)
		{
			stream.Position = offset;
			return (byte)stream.ReadByte();
		}
		public static byte[] ReadBytes(this System.IO.Stream stream, long offset, int length)
		{
			byte[] array = new byte[length];
			stream.Position = offset;
			stream.Read(array, 0, length);
			return array;
		}
		[CLSCompliant(false)]
		public static byte[] ReadBytes(this System.IO.Stream stream, long offset, uint length)
		{
			return stream.ReadBytes(offset, (int)length);
		}
		public static short ReadShort(this System.IO.Stream stream, long offset)
		{
			byte[] array = new byte[2];
			stream.Position = offset;
			stream.Read(array, 0, 2);
			return BitConverter.ToInt16(array, 0);
		}
		[CLSCompliant(false)]
		public static ushort ReadUShort(this System.IO.Stream stream, long offset)
		{
			byte[] array = new byte[2];
			stream.Position = offset;
			stream.Read(array, 0, 2);
			return BitConverter.ToUInt16(array, 0);
		}
		public static int ReadInt(this System.IO.Stream stream, long offset)
		{
			byte[] array = new byte[4];
			stream.Position = offset;
			stream.Read(array, 0, 4);
			return BitConverter.ToInt32(array, 0);
		}
		[CLSCompliant(false)]
		public static uint ReadUInt(this System.IO.Stream stream, long offset)
		{
			byte[] array = new byte[4];
			stream.Position = offset;
			stream.Read(array, 0, 4);
			return BitConverter.ToUInt32(array, 0);
		}
		public static string ReadString(this System.IO.Stream stream, long offset, int maxLength, bool nullTerminator)
		{
			string text = string.Empty;
			stream.Position = offset;
			for (int i = 0; i < maxLength; i++)
			{
				char c = (char)stream.ReadByte();
				if (c == '\0' && nullTerminator)
				{
					break;
				}
				text += c;
			}
			return text;
		}
		public static string ReadString(this System.IO.Stream stream, long offset, int maxLength)
		{
			return stream.ReadString(offset, maxLength, true);
		}
		public static string ReadString(this System.IO.Stream stream, long offset, int maxLength, Encoding encoding, bool nullTerminator)
		{
			stream.Position = offset;
			byte[] array = new byte[maxLength];
			stream.Read(array, 0, maxLength);
			string text = encoding.GetString(array);
			if (nullTerminator)
			{
				string arg_2D_0 = text;
				char[] trimChars = new char[1];
				text = arg_2D_0.TrimEnd(trimChars);
			}
			return text;
		}
		public static string ReadString(this System.IO.Stream stream, long offset, int maxLength, Encoding encoding)
		{
			return stream.ReadString(offset, maxLength, encoding, true);
		}
		public static byte[] ToByteArray(this System.IO.Stream stream)
		{
			long oldpos = stream.Position;

			byte[] array = new byte[(int)((IntPtr)stream.Length)];
			stream.Position = 0L;
			stream.Read(array, 0, array.Length);

			stream.Position = oldpos;
			return array;
		}
		#endregion
		#region StreamWriter Extensions

		public static void Write(this System.IO.Stream stream, byte value)
		{
			stream.WriteByte(value);
		}
		public static void Write(this System.IO.Stream stream, short value)
		{
			stream.Write(BitConverter.GetBytes(value), 0, 2);
		}
		[CLSCompliant(false)]
		public static void Write(this System.IO.Stream stream, ushort value)
		{
			stream.Write(BitConverter.GetBytes(value), 0, 2);
		}
		public static void Write(this System.IO.Stream stream, int value)
		{
			stream.Write(BitConverter.GetBytes(value), 0, 4);
		}
		[CLSCompliant(false)]
		public static void Write(this System.IO.Stream stream, uint value)
		{
			stream.Write(BitConverter.GetBytes(value), 0, 4);
		}
		public static void Write(this System.IO.Stream stream, byte[] values)
		{
			stream.Write(values, 0, values.Length);
		}
		public static void Write(this System.IO.Stream stream, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				stream.WriteByte((byte)value[i]);
			}
		}
		public static void Write(this System.IO.Stream stream, string value, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (i < value.Length)
				{
					stream.WriteByte((byte)value[i]);
				}
				else
				{
					stream.WriteByte(0);
				}
			}
		}
		public static void Write(this System.IO.Stream stream, string value, int strLength, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (i < value.Length && i < strLength)
				{
					stream.WriteByte((byte)value[i]);
				}
				else
				{
					stream.WriteByte(0);
				}
			}
		}
		public static void Write(this System.IO.Stream stream, string value, int strLength, int length, Encoding encoding)
		{
			byte[] bytes = encoding.GetBytes(value);
			int num = 0;
			while (bytes.Length > strLength)
			{
				num++;
				bytes = encoding.GetBytes(value.Substring(0, value.Length - num));
			}
			for (int i = 0; i < length; i++)
			{
				if (i < bytes.Length && i < strLength)
				{
					stream.WriteByte(bytes[i]);
				}
				else
				{
					stream.WriteByte(0);
				}
			}
		}
		public static void Write(this System.IO.Stream output, System.IO.Stream input)
		{
			byte[] array = new byte[4096];
			input.Position = 0L;
			int count;
			while ((count = input.Read(array, 0, array.Length)) > 0)
			{
				output.Write(array, 0, count);
			}
		}
		public static void Write(this System.IO.Stream output, System.IO.MemoryStream input)
		{
			input.Position = 0L;
			input.WriteTo(output);
		}
		public static void Write(this System.IO.Stream output, System.IO.Stream input, long offset, long length)
		{
			byte[] array = new byte[4096];
			input.Position = offset;
			int count;
			while ((count = input.Read(array, 0, (input.Position + (long)array.Length > offset + length) ? ((int)(offset + length - input.Position)) : ((int)((long)array.Length)))) > 0)
			{
				output.Write(array, 0, count);
			}
		}
		public static System.IO.MemoryStream Copy(this System.IO.Stream input)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream((int)input.Length);
			byte[] array = new byte[4096];
			input.Position = 0L;
			int count;
			while ((count = input.Read(array, 0, array.Length)) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			return memoryStream;
		}
		public static System.IO.MemoryStream Copy(this System.IO.Stream input, long offset, int length)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(length);
			byte[] array = new byte[4096];
			input.Position = offset;
			int count;
			while ((count = input.Read(array, 0, (input.Position + (long)array.Length > offset + (long)length) ? ((int)(offset + (long)length - input.Position)) : ((int)((long)array.Length)))) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			return memoryStream;
		}
		[CLSCompliant(false)]
		public static System.IO.MemoryStream Copy(this System.IO.Stream input, long offset, uint length)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream((int)length);
			byte[] array = new byte[4096];
			input.Position = offset;
			int count;
			while ((count = input.Read(array, 0, (input.Position + (long)array.Length > offset + (long)((ulong)length)) ? ((int)(offset + (long)((ulong)length) - input.Position)) : ((int)((long)array.Length)))) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			return memoryStream;
		}
		#endregion
		#region Strings
		public static bool IsAllUpperCase(this string str)
		{
			return (!(new System.Text.RegularExpressions.Regex("[a-z]")).IsMatch(str));
		}
		public static string UrlEncode(this string value)
		{
			return value;
		}
		public static string UrlDecode(this string input)
		{
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '%')
				{
					char c = input[i + 1];
					string arg_45_0 = c.ToString();
					c = input[i + 2];
					string numeric = arg_45_0 + c.ToString();
					int hexcode = int.Parse(numeric, System.Globalization.NumberStyles.HexNumber);
					i += 2;
					output.Append((char)hexcode);
				}
				else
				{
					output.Append(input[i]);
				}
			}
			return output.ToString();
		}
		public static bool Match(this string input, params string[] filters)
		{
			foreach (string filter in filters)
			{
				if (input.Match(filter)) return true;
			}
			return false;
		}
		public static bool Match(this string input, string filter)
		{
			if (filter == null)
				return false;

			string wildcardToRegex = "^" + System.Text.RegularExpressions.Regex.Escape(filter).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			return new System.Text.RegularExpressions.Regex(wildcardToRegex).IsMatch(input);
		}

		/// <summary>
		/// Removes all characters past and including the first occurrence of a
		/// NUL (0x00) byte from the given string.
		/// </summary>
		/// <returns>
		/// All characters before the first occurrence of a NUL (0x00) byte in
		/// <paramref name="value" />.
		/// </returns>
		/// <param name="value">The string to trim.</param>
		public static string TrimNull(this string value)
		{
			int i = value.IndexOf('\0');
			if (i > -1) return value.Substring(0, i);
			return value;
		}

		public static string Format(this string input, Dictionary<string, string> formatWhat)
		{
			return Format(input, formatWhat, "$(", ")");
		}
		public static string Format(this string input, Dictionary<string, string> formatWhat, string formatBegin, string formatEnd)
		{
			string val = input;
			foreach (KeyValuePair<string, string> kvp in formatWhat)
			{
				val = val.Replace(formatBegin + kvp.Key + formatEnd, kvp.Value);
			}
			return val;
		}
		/// <summary>
		/// Inserts the specified value "count" times, with "spacing" characters between.
		/// </summary>
		/// <param name="count">The number of times to insert value.</param>
		/// <param name="spacing">The amount of characters to leave between insertions.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns></returns>
		public static string Insert(this string input, int count, int spacing, string value)
		{
			int j = 0;
			string retval = String.Empty;
			for (int i = 0; i < count; i++)
			{
				retval += input.Substring(j, spacing) + value;
				j += spacing;
			}
			retval += input.Substring(j);
			return retval;
		}
		#endregion

		#region Dictionary
		public static KeyValuePair<TKey, TValue>[] ToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			System.Collections.Generic.List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
			foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
			{
				list.Add(kvp);
			}
			return list.ToArray();
		}
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value = default(TValue))
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			return value;
		}
		#endregion

		#region Arrays
		public static void Fill<T>(this T[] data, T value)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = value;
			}
		}
		#endregion

		public static long RoundToNearestPowerOf2(this long input)
		{
			long v = input;
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;
			return v;
		}
		[CLSCompliant(false)]
		public static ulong RoundToNearestPowerOf2(this ulong input)
		{
			ulong v = input;
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;
			return v;
		}
	}
}
