//
//  Writer.cs - input/output module for writing binary or text data
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

namespace UniversalEditor.IO
{
	public class Writer : ReaderWriterBase
	{

		public Writer(Accessor accessor)
			: base(accessor)
		{
		}

		public void Write(byte[] buffer, int start, int count)
		{
			base.Accessor.WriteInternal(buffer, start, count);
		}

		public void WriteBoolean(bool value)
		{
			WriteByte(value ? (byte)1 : (byte)0);
		}
		public void WriteByte(byte value)
		{
			WriteBytes(new byte[] { value });
		}

		[CLSCompliant(false)]
		public void WriteSByte(sbyte value)
		{
			WriteBytes(new byte[] { (byte)value });
		}

		private int xorkey_index = 0;
		public void WriteBytes(byte[] data)
		{
			if (data == null) return;

			for (int i = 0; i < Transformations.Count; i++)
			{
				data = Transformations[i].Transform(data);
			}
			Write(data, 0, data.Length);

			if (AutoFlush)
				Flush();
		}

		public bool AutoFlush { get; set; } = false;

		[CLSCompliant(false)]
		public void WriteSBytes(sbyte[] data)
		{
			if (data == null) return;

			// thanks https://stackoverflow.com/questions/829983/how-to-convert-a-sbyte-to-byte-in-c
			byte[] realdata = (byte[])(Array)data;

			for (int i = 0; i < Transformations.Count; i++)
			{
				realdata = Transformations[i].Transform(realdata);
			}
			Write(realdata, 0, realdata.Length);
		}

		public void WriteFixedLengthBytes(byte[] data, int count)
		{
			if (data == null) data = new byte[0];
			byte[] realdata = new byte[count];
			Array.Copy(data, 0, realdata, 0, Math.Min(realdata.Length, count));
			WriteBytes(realdata);
		}

		public void WriteChar(char value)
		{
			WriteChar(value, base.Accessor.DefaultEncoding);
		}
		public void WriteChar(char value, Encoding encoding)
		{
			byte[] data = encoding.GetBytes(new char[] { value });
			WriteBytes(data);
		}
		public void WriteCharArray(char[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteChar(values[i]);
			}
		}

		public void Write(char value)
		{
			Write(value.ToString());
		}
		public void Write(string value)
		{
			WriteFixedLengthString(value);
		}
		public void WriteLine()
		{
			WriteLine(String.Empty);
		}
		public void WriteLine(char value)
		{
			WriteLine(value.ToString());
		}
		public void WriteLine(string value)
		{
			WriteFixedLengthString(value + GetNewLineSequence());
		}

		public void WriteGuid(Guid guid)
		{
			WriteBytes(guid.ToByteArray());
		}
		public void WriteFixedLengthString(string value)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding);
		}
		public void WriteFixedLengthString(string value, int length)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length);
		}
		[CLSCompliant(false)]
		public void WriteFixedLengthString(string value, uint length)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length);
		}
		public void WriteFixedLengthString(string value, Encoding encoding)
		{
			if (value == null)
				return;

			byte[] data = encoding.GetBytes(value);
			WriteBytes(data);
		}
		public void WriteFixedLengthString(string value, int length, char paddingChar)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length, paddingChar);
		}
		[CLSCompliant(false)]
		public void WriteFixedLengthString(string value, uint length, char paddingChar)
		{
			this.WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length, paddingChar);
		}
		public void WriteFixedLengthString(string value, Encoding encoding, int length)
		{
			this.WriteFixedLengthString(value, encoding, length, '\0');
		}
		[CLSCompliant(false)]
		public void WriteFixedLengthString(string value, Encoding encoding, uint length)
		{
			WriteFixedLengthString(value, encoding, length, '\0');
		}
		public void WriteFixedLengthString(string value, Encoding encoding, int length, char paddingChar)
		{
			WriteFixedLengthString(value, encoding, (uint)length, paddingChar);
		}
		[CLSCompliant(false)]
		public void WriteFixedLengthString(string value, Encoding encoding, uint length, char paddingChar)
		{
			if (value == null)
				return;

			string v = value;
			if (v == null) v = String.Empty;
			byte[] data = encoding.GetBytes(v);
			byte[] realData = new byte[length];

			uint realLength = length;
			if (data.Length < realLength)
			{
				realLength = (uint)data.Length;
			}
			Array.Copy(data, 0, realData, 0, realLength);

			for (int i = data.Length; i < realData.Length; i++)
			{
				realData[i] = (byte)paddingChar;
			}
			WriteBytes(realData);
		}

		public void WriteLengthPrefixedString(string value)
		{
			WriteLengthPrefixedString(value, base.Accessor.DefaultEncoding);
		}
		public void WriteLengthPrefixedString(string value, Encoding encoding)
		{
			Write7BitEncodedInt32(value.Length);
			WriteFixedLengthString(value);
		}

		public void Write7BitEncodedInt32(int value)
		{
			// TODO: verify this actually works
			uint v = (uint)value;
			while (v >= 0x80)
			{
				WriteByte((byte)(v | 0x80));
				v >>= 7;
			}
			WriteByte((byte)v);
		}
		public int Calculate7BitEncodedInt32Size(int value)
		{
			// TODO: verify this actually works
			int size = 1;
			uint v = (uint)value;
			while (v >= 0x80)
			{
				size++;
				v >>= 7;
			}
			return size;
		}

		public void WriteNullTerminatedString(string sz)
		{
			WriteNullTerminatedString(sz, Encoding.UTF8);
		}
		public void WriteNullTerminatedString(string sz, Encoding encoding)
		{
			byte[] values = encoding.GetBytes(sz + '\0');
			WriteBytes(values);
		}
		public void WriteNullTerminatedString(string sz, int length)
		{
			// TODO: not sure how to handle this, should "length" refer to just the string length (data length) or should it include the null-terminator (field length)?
			string ssz = sz.Substring(0, Math.Min(sz.Length, length) - 1);
			WriteNullTerminatedString(ssz);
		}
		public void WriteNullTerminatedString(string sz, Encoding encoding, int length)
		{
			// TODO: not sure how to handle this, should "length" refer to just the string length (data length) or should it include the null-terminator (field length)?
			string ssz = sz.Substring(0, Math.Min(sz.Length, length) - 1);
			WriteNullTerminatedString(ssz, encoding);
		}

		/// <summary>
		/// Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.
		/// </summary>
		/// <param name="value">The two-byte signed integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt16(short value)
		{
			byte[] _buffer = BitConverter.GetBytes(value);
			byte[] buffer = new byte[2];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[1] = _buffer[0];
				buffer[0] = _buffer[1];
			}
			else
			{
				buffer[0] = _buffer[0];
				buffer[1] = _buffer[1];
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of two-byte signed integers to the current stream and advances the stream position by two bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of two-byte signed integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt16Array(short[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteInt16(values[i]);
			}
		}

		/// <summary>
		/// Writes a variable-length unsigned integer to the current stream and advances the stream position by the number of bytes written.
		/// </summary>
		/// <param name="value">The value to write.</param>
		/// <returns>a <see cref="Int32" /> representing the number of bytes written to the stream for the given <paramref name="value" /></returns>
		/// <remarks>This code is taken from the answer on StackOverflow https://stackoverflow.com/q/3564685</remarks>
		public int WriteVariableLengthInt32(int value)
		{
			// thx stackoverflow :) https://stackoverflow.com/q/3564685
			int count = 0;
			bool first = true;
			while (first || value > 0)
			{
				first = false;
				byte lower7bits = (byte)(value & 0x7f);
				value >>= 7;
				if (value > 0)
					lower7bits |= 128;
				WriteByte(lower7bits);
				count++;
			}
			return count;
		}

		/// <summary>
		/// Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.
		/// </summary>
		/// <param name="value">The two-byte unsigned integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt16(ushort value)
		{
			byte[] _buffer = BitConverter.GetBytes(value);
			byte[] buffer = new byte[2];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[1] = _buffer[0];
				buffer[0] = _buffer[1];
			}
			else
			{
				buffer[0] = _buffer[0];
				buffer[1] = _buffer[1];
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of two-byte unsigned integers to the current stream and advances the stream position by two bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of two-byte unsigned integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt16Array(ushort[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteUInt16(values[i]);
			}
		}
		/// <summary>
		/// Writes a three-byte signed integer to the current stream and advances the stream position by three bytes.
		/// </summary>
		/// <param name="value">The three-byte signed integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt24(int value)
		{
			byte[] buffer = new byte[3];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[2] = (byte)value;
				buffer[1] = (byte)(value >> 8);
				buffer[0] = (byte)(value >> 16);
			}
			else
			{
				buffer[0] = (byte)value;
				buffer[1] = (byte)(value >> 8);
				buffer[2] = (byte)(value >> 16);
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of three-byte signed integers to the current stream and advances the stream position by three bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of three-byte signed integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt24Array(int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteInt24(values[i]);
			}
		}
		/// <summary>
		/// Writes a three-byte unsigned integer to the current stream and advances the stream position by three bytes.
		/// </summary>
		/// <param name="value">The three-byte unsigned integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt24(uint value)
		{
			byte[] buffer = new byte[3];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[2] = (byte)value;
				buffer[1] = (byte)(value >> 8);
				buffer[0] = (byte)(value >> 16);
			}
			else
			{
				buffer[0] = (byte)value;
				buffer[1] = (byte)(value >> 8);
				buffer[2] = (byte)(value >> 16);
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of three-byte unsigned integers to the current stream and advances the stream position by three bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of three-byte unsigned integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt24Array(uint[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteUInt24(values[i]);
			}
		}
		/// <summary>
		/// Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.
		/// </summary>
		/// <param name="value">The four-byte signed integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt32(int value)
		{
			byte[] _buffer = BitConverter.GetBytes(value);
			byte[] buffer = new byte[4];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[3] = _buffer[0];
				buffer[2] = _buffer[1];
				buffer[1] = _buffer[2];
				buffer[0] = _buffer[3];
			}
			else
			{
				buffer[0] = _buffer[0];
				buffer[1] = _buffer[1];
				buffer[2] = _buffer[2];
				buffer[3] = _buffer[3];
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of four-byte signed integers to the current stream and advances the stream position by four bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of four-byte signed integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt32Array(int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteInt32(values[i]);
			}
		}
		/// <summary>
		/// Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.
		/// </summary>
		/// <param name="value">The four-byte unsigned integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt32(uint value)
		{
			byte[] _buffer = BitConverter.GetBytes(value);
			byte[] buffer = new byte[4];
			if (base.Endianness == Endianness.BigEndian)
			{
				buffer[3] = _buffer[0];
				buffer[2] = _buffer[1];
				buffer[1] = _buffer[2];
				buffer[0] = _buffer[3];
			}
			else
			{
				buffer[0] = _buffer[0];
				buffer[1] = _buffer[1];
				buffer[2] = _buffer[2];
				buffer[3] = _buffer[3];
			}
			WriteBytes(buffer);
		}
		/// <summary>
		/// Writes an array of four-byte unsigned integers to the current stream and advances the stream position by four bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of four-byte unsigned integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt32Array(uint[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteUInt32(values[i]);
			}
		}
		/// <summary>
		/// Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.
		/// </summary>
		/// <param name="value">The eight-byte signed integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt64(long value)
		{
			byte[] _buffer = new byte[8];
			if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = (byte)(value >> 56);
				_buffer[1] = (byte)(value >> 48);
				_buffer[2] = (byte)(value >> 40);
				_buffer[3] = (byte)(value >> 32);
				_buffer[4] = (byte)(value >> 24);
				_buffer[5] = (byte)(value >> 16);
				_buffer[6] = (byte)(value >> 8);
				_buffer[7] = (byte)value;
			}
			else
			{
				_buffer[0] = (byte)value;
				_buffer[1] = (byte)(value >> 8);
				_buffer[2] = (byte)(value >> 16);
				_buffer[3] = (byte)(value >> 24);
				_buffer[4] = (byte)(value >> 32);
				_buffer[5] = (byte)(value >> 40);
				_buffer[6] = (byte)(value >> 48);
				_buffer[7] = (byte)(value >> 56);
			}
			WriteBytes(_buffer);
		}
		/// <summary>
		/// Writes an array of eight-byte signed integers to the current stream and advances the stream position by eight bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of eight-byte signed integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteInt64Array(long[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteInt64(values[i]);
			}
		}
		/// <summary>
		/// Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.
		/// </summary>
		/// <param name="value">The eight-byte unsigned integer to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt64(ulong value)
		{
			byte[] _buffer = new byte[8];
			if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = (byte)(value >> 56);
				_buffer[1] = (byte)(value >> 48);
				_buffer[2] = (byte)(value >> 40);
				_buffer[3] = (byte)(value >> 32);
				_buffer[4] = (byte)(value >> 24);
				_buffer[5] = (byte)(value >> 16);
				_buffer[6] = (byte)(value >> 8);
				_buffer[7] = (byte)value;
			}
			else
			{
				_buffer[0] = (byte)value;
				_buffer[1] = (byte)(value >> 8);
				_buffer[2] = (byte)(value >> 16);
				_buffer[3] = (byte)(value >> 24);
				_buffer[4] = (byte)(value >> 32);
				_buffer[5] = (byte)(value >> 40);
				_buffer[6] = (byte)(value >> 48);
				_buffer[7] = (byte)(value >> 56);
			}
			WriteBytes(_buffer);
		}
		/// <summary>
		/// Writes an array of eight-byte unsigned integers to the current stream and advances the stream position by eight bytes times the number of values written.
		/// </summary>
		/// <param name="values">The array of eight-byte unsigned integers to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		[CLSCompliant(false)]
		public void WriteUInt64Array(ulong[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteUInt64(values[i]);
			}
		}
		/// <summary>
		/// Writes an arbitrary object to the current stream and advances the stream position by the number of bytes needed to store the object.
		/// </summary>
		/// <param name="value">The object to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteObject(object value)
		{
			if (value is object[])
			{
				object[] array = (object[])value;
				for (int i = 0; i < array.Length; i++)
				{
					WriteObject(array[i]);
				}
				return;
			}

			Type objectType = value.GetType();

			if (objectType == typeof(Byte))
			{
				WriteByte((byte)value);
				return;
			}
			else if (objectType == typeof(Byte[]))
			{
				WriteBytes((byte[])value);
				return;
			}
			else if (objectType == typeof(SByte))
			{
				WriteSByte((sbyte)value);
				return;
			}
			else if (objectType == typeof(String))
			{
				WriteLengthPrefixedString((string)value);
				return;
			}
			else if (objectType == typeof(Char))
			{
				WriteChar((char)value);
				return;
			}
			else if (objectType == typeof(Char[]))
			{
				WriteCharArray((char[])value);
				return;
			}
			else if (objectType == typeof(Single))
			{
				WriteSingle((float)value);
				return;
			}
			else if (objectType == typeof(Double))
			{
				WriteDouble((double)value);
				return;
			}
			else if (objectType == typeof(Int16))
			{
				WriteInt16((short)value);
				return;
			}
			else if (objectType == typeof(Int32))
			{
				WriteInt32((int)value);
				return;
			}
			else if (objectType == typeof(Int64))
			{
				WriteInt64((long)value);
				return;
			}
			else if (objectType == typeof(UInt16))
			{
				WriteUInt16((ushort)value);
				return;
			}
			else if (objectType == typeof(UInt32))
			{
				WriteUInt32((uint)value);
				return;
			}
			else if (objectType == typeof(UInt64))
			{
				WriteUInt64((ulong)value);
				return;
			}
			else if (objectType == typeof(DateTime))
			{
				WriteDateTime((DateTime)value);
				return;
			}

			System.Reflection.FieldInfo[] fis = (objectType.GetFields(System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
			foreach (System.Reflection.FieldInfo fi in fis)
			{
				// Type fieldType = fi.FieldType;
				WriteObject(fi.GetValue(value));
			}
		}
		/// <summary>
		/// Writes an array of arbitrary objects to the current stream and advances the stream position by the number of bytes needed to store the objects.
		/// </summary>
		/// <param name="values">The array of objects to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteObjectArray(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				WriteObject(values[i]);
			}
		}

		/// <summary>
		/// Writes a <see cref="DateTime" /> in a format that encodes the <see cref="System.DateTime.Kind" /> property in a 2-bit field
		/// and the <see cref="System.DateTime.Ticks" /> property in a 62-bit field.
		/// </summary>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteDateTime(DateTime value)
		{
			WriteInt64(value.ToBinary());
		}

		/// <summary>
		/// Encodes a <see cref="DateTime" /> into a 32-bit value as used in MS-DOS and Windows FAT directory entries.
		/// </summary>
		/// <param name="dateTime">Date time.</param>
		/// <remarks>
		/// Each portion of the time stamp (year, month etc.) is encoded within specific bits of the 32-bit timestamp. The epoch for this time format is 1980. This format has a granularity of 2 seconds.
		/// </remarks>
		public void WriteDOSFileTime(DateTime dateTime)
		{
			// The 32 bit date and time format used in the MSDOS and Windows FAT directory entry

			//		Year    Month   Day     Hour    Min     Seconds / 2
			// Bits    31-25   24-21   20-16   15-11   10-5    4-0

			/*
			cal.set(Calendar.YEAR, (int)((dosTime >> 25) & 0x7F) + 1980);
			cal.set(Calendar.MONTH, (int)((dosTime >> 21) & 0x0f) - 1);
			cal.set(Calendar.DATE, (int)(dosTime >> 16) & 0x1f);
			cal.set(Calendar.HOUR_OF_DAY, (int)(dosTime >> 11) & 0x1f);
			cal.set(Calendar.MINUTE, (int)(dosTime >> 5) & 0x3f);
			cal.set(Calendar.SECOND, (int)(dosTime << 1) & 0x3e);
			cal.set(Calendar.MILLISECOND, 0);
			*/

			uint seconds = (uint)(dateTime.Second / 2);
			uint min = (uint)dateTime.Minute;
			uint hour = (uint)dateTime.Hour;
			uint day = (uint)dateTime.Day;
			uint month = (uint)dateTime.Month;
			uint year = (uint)dateTime.Year - 1973; // huh?

			uint value = 0;//e (seconds | (min << 6) | (hour << 11) | (day << 16) | (month << 21) | (year << 25));
			WriteUInt32(value);  // (int)(dateTime.Second / 2));
		}

		/// <summary>
		/// Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.
		/// </summary>
		/// <param name="value">The four-byte floating-point value to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteSingle(float value)
		{
			byte[] buffer = BitConverter.GetBytes(value);
			byte[] _buffer = new byte[4];
			if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = buffer[3];
				_buffer[1] = buffer[2];
				_buffer[2] = buffer[1];
				_buffer[3] = buffer[0];
			}
			else
			{
				_buffer[0] = buffer[0];
				_buffer[1] = buffer[1];
				_buffer[2] = buffer[2];
				_buffer[3] = buffer[3];
			}
			WriteBytes(_buffer);
		}
		public void WriteSingleArray(float[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				WriteSingle(value[i]);
			}
		}

		/// <summary>
		/// Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.
		/// </summary>
		/// <param name="value">The eight-byte floating-point value to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteDouble(double value)
		{
			byte[] buffer = BitConverter.GetBytes(value);
			byte[] _buffer = new byte[8];
			if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = buffer[7];
				_buffer[1] = buffer[6];
				_buffer[2] = buffer[5];
				_buffer[3] = buffer[4];
				_buffer[4] = buffer[3];
				_buffer[5] = buffer[2];
				_buffer[6] = buffer[1];
				_buffer[7] = buffer[0];
			}
			else
			{
				_buffer[0] = buffer[0];
				_buffer[1] = buffer[1];
				_buffer[2] = buffer[2];
				_buffer[3] = buffer[3];
				_buffer[4] = buffer[4];
				_buffer[5] = buffer[5];
				_buffer[6] = buffer[6];
				_buffer[7] = buffer[7];
			}
			WriteBytes(_buffer);
		}
		/// <summary>
		/// Writes a decimal value to the current stream and advances the stream position by sixteen bytes.
		/// </summary>
		/// <param name="value">The decimal value to write.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		public void WriteDecimal(decimal value)
		{
			int[] bits = decimal.GetBits(value);
			int lo = bits[0], mid = bits[1], hi = bits[2], flags = bits[3];

			byte[] buffer = new byte[16];
			if (base.Endianness == Endianness.LittleEndian)
			{
				buffer[0] = (byte)lo;
				buffer[1] = (byte)(lo >> 8);
				buffer[2] = (byte)(lo >> 16);
				buffer[3] = (byte)(lo >> 24);
				buffer[4] = (byte)mid;
				buffer[5] = (byte)(mid >> 8);
				buffer[6] = (byte)(mid >> 16);
				buffer[7] = (byte)(mid >> 24);
				buffer[8] = (byte)hi;
				buffer[9] = (byte)(hi >> 8);
				buffer[10] = (byte)(hi >> 16);
				buffer[11] = (byte)(hi >> 24);
				buffer[12] = (byte)flags;
				buffer[13] = (byte)(flags >> 8);
				buffer[14] = (byte)(flags >> 16);
				buffer[15] = (byte)(flags >> 24);
			}
			else
			{
				buffer[15] = (byte)lo;
				buffer[14] = (byte)(lo >> 8);
				buffer[13] = (byte)(lo >> 16);
				buffer[12] = (byte)(lo >> 24);
				buffer[11] = (byte)mid;
				buffer[10] = (byte)(mid >> 8);
				buffer[9] = (byte)(mid >> 16);
				buffer[9] = (byte)(mid >> 24);
				buffer[7] = (byte)hi;
				buffer[6] = (byte)(hi >> 8);
				buffer[5] = (byte)(hi >> 16);
				buffer[4] = (byte)(hi >> 24);
				buffer[3] = (byte)flags;
				buffer[2] = (byte)(flags >> 8);
				buffer[1] = (byte)(flags >> 16);
				buffer[0] = (byte)(flags >> 24);
			}
			WriteBytes(buffer);
		}

		public void WriteVersion(Version version)
		{
			WriteVersion(version, 4);
		}
		public void WriteVersion(Version version, int count)
		{
			switch (count)
			{
				case 1:
				{
					WriteByte(1);
					WriteInt32(version.Major);
					break;
				}
				case 2:
				{
					WriteByte(2);
					WriteInt32(version.Major);
					WriteInt32(version.Minor);
					break;
				}
				case 3:
				{
					WriteByte(3);
					WriteInt32(version.Major);
					WriteInt32(version.Minor);
					WriteInt32(version.Build);
					break;
				}
				case 4:
				{
					WriteByte(4);
					WriteInt32(version.Major);
					WriteInt32(version.Minor);
					WriteInt32(version.Build);
					WriteInt32(version.Revision);
					break;
				}
			}
		}

		public long CountAlignment(int paddingCount)
		{
			return CountAlignment(paddingCount, 0);
		}
		public long CountAlignment(int paddingCount, int dataCount)
		{
			long position = (base.Accessor.Position + dataCount);
			int num = (int)(position % paddingCount);
			return num;
		}

		public void WriteDoubleEndianInt16(short value)
		{
			WriteInt16(value);
			SwapEndianness();
			WriteInt16(value);
			SwapEndianness();
		}
		public void WriteDoubleEndianInt32(int value)
		{
			WriteInt32(value);
			SwapEndianness();
			WriteInt32(value);
			SwapEndianness();
		}
		public void WriteDoubleEndianInt64(long value)
		{
			WriteInt64(value);
			SwapEndianness();
			WriteInt64(value);
			SwapEndianness();
		}
		[CLSCompliant(false)]
		public void WriteDoubleEndianUInt16(ushort value)
		{
			WriteUInt16(value);
			SwapEndianness();
			WriteUInt16(value);
			SwapEndianness();
		}
		[CLSCompliant(false)]
		public void WriteDoubleEndianUInt32(uint value)
		{
			WriteUInt32(value);
			SwapEndianness();
			WriteUInt32(value);
			SwapEndianness();
		}
		[CLSCompliant(false)]
		public void WriteDoubleEndianUInt64(ulong value)
		{
			WriteUInt64(value);
			SwapEndianness();
			WriteUInt64(value);
			SwapEndianness();
		}

		public void WriteUInt16String(string value)
		{
			WriteUInt16String(value, base.Accessor.DefaultEncoding);
		}
		public void WriteUInt16String(string value, Encoding encoding)
		{
			ushort length = (ushort)value.Length;
			byte[] input = encoding.GetBytes(value);
			byte[] output = new byte[length];
			Array.Copy(input, 0, output, 0, output.Length);
			WriteUInt16(length);
			WriteBytes(output);
		}

		public void WriteUInt16SizedString(string value)
		{
			WriteUInt16SizedString(value, base.Accessor.DefaultEncoding);
		}
		public void WriteUInt16SizedString(string value, Encoding encoding)
		{
			ushort length = (ushort)value.Length;
			byte[] input = encoding.GetBytes(value);
			byte[] output = new byte[length];
			Array.Copy(input, 0, output, 0, output.Length);
			WriteBytes(output);
		}

		public void WriteInt64String(string value)
		{
			WriteInt64(value.Length);
			WriteFixedLengthString(value);
		}

		/// <summary>
		/// Aligns the <see cref="Reader" /> to the specified number of bytes. If the current
		/// position of the <see cref="Reader" /> is not a multiple of the specified number of bytes,
		/// the position will be increased by the amount of bytes necessary to bring it to the
		/// aligned position.
		/// </summary>
		/// <param name="alignTo">The number of bytes on which to align the <see cref="Reader"/>.</param>
		/// <param name="extraPadding">Any additional padding bytes that should be included after aligning to the specified boundary.</param>
		public void Align(int alignTo, int extraPadding = 0)
		{
			if (alignTo == 0)
				return;

			long paddingCount = ((alignTo - (Accessor.Position % alignTo)) % alignTo);
			paddingCount += extraPadding;

			if (Accessor.Position == Accessor.Length)
			{
				Accessor.Writer.WriteBytes(new byte[paddingCount]);
			}
			else
			{
				Accessor.Position += paddingCount;
			}
		}

		/// <summary>
		/// Clears all buffers for the associated <see cref="Accessor" /> and causes any buffered data to be written to the underlying device.
		/// </summary>
		public void Flush()
		{
			base.Accessor.Flush();
		}
		/// <summary>
		/// Closes the associated <see cref="Accessor" /> and the underlying stream.
		/// </summary>
		public void Close()
		{
			base.Accessor.Close();
		}
	}
}
