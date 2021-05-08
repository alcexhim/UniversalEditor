//
//  Reader.cs - input/output module for reading binary or text data
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
using System.Diagnostics;
using System.Text;

namespace UniversalEditor.IO
{
	// [DebuggerNonUserCode()]
	public class Reader : ReaderWriterBase
	{
		public Reader(Accessor input)
			: base(input)
		{
		}

		public void Read(byte[] buffer, int start, int length)
		{
			base.Accessor.ReadInternal(buffer, start, length);
		}

		public bool ReadBoolean()
		{
			return (ReadBytes(1)[0] != 0);
		}

		public byte ReadByte()
		{
			return ReadBytes(1)[0];
		}

		string charBuffer = null;
		int charBufferIndex = 0;

		public char ReadChar(Encoding encoding = null)
		{
			if (encoding == null)
				encoding = Accessor.DefaultEncoding;

			charBuffer = null;
			if (charBuffer == null)
			{
				int maxByteCount = encoding.GetMaxByteCount(1);
				byte[] bytes = PeekBytes(maxByteCount);
				charBuffer = encoding.GetString(bytes);
				charBufferIndex = 0;
			}

			char c = charBuffer[charBufferIndex];
			charBufferIndex++;

			int ct = encoding.GetByteCount(c);
			Seek(ct, SeekOrigin.Current);

			if (charBufferIndex + 1 > charBuffer.Length)
			{
				charBuffer = null;
			}
			return c;
		}
		public byte PeekByte()
		{
			byte b = ReadByte();
			base.Accessor.Seek(-1, SeekOrigin.Current);
			return b;
		}
		public byte[] PeekBytes(int length)
		{
			byte[] buffer = new byte[length];
			int len = base.Accessor.ReadInternal(buffer, 0, length);
			base.Accessor.Seek(-len, SeekOrigin.Current);
			return buffer;
		}
		public char PeekChar()
		{
			return (char)PeekByte();
		}
		[CLSCompliant(false)]
		public sbyte ReadSByte()
		{
			return (sbyte)(ReadBytes(1)[0]);
		}

		private int xorkey_index = 0;

		[CLSCompliant(false)]
		public byte[] ReadBytes(uint length)
		{
			byte[] buf = new byte[length];
			uint lastct = 0;
			while (lastct < length)
			{
				int ct = (int)length;
				byte[] buf2 = new byte[ct];
				Read(buf2, 0, ct);

				Array.Copy(buf2, 0, buf, lastct, buf2.Length);
				lastct += (uint)ct;
			}

			for (int i = 0; i < Transformations.Count; i++)
			{
				buf = Transformations[i].Transform(buf);
			}
			return buf;
		}

		[CLSCompliant(false)]
		public byte[] ReadBytes(ulong length)
		{
			byte[] buf = new byte[length];
			for (ulong i = 0L; i < length; i += (ulong)1L)
			{
				buf[(int)i] = ReadByte();
			}
			return buf;
		}

		[DebuggerNonUserCode()]
		public byte[] ReadBytes(long length)
		{
			byte[] buffer = new byte[length];
			base.Accessor.ReadInternal(buffer, 0, (int)length);
			return buffer;
		}

		[CLSCompliant(false)]
		public sbyte[] ReadSBytes(long length)
		{
			byte[] buffer = ReadBytes(length);
			return (sbyte[])(Array)buffer;
		}

		public int ReadCompactInt32()
		{
			int multiplier = 1;
			byte b1 = this.ReadByte();
			if ((b1 & 0x80) == 1)
			{
				multiplier = -1;
			}
			if ((b1 & 0x40) == 1)
			{
				byte b2 = this.ReadByte();
				if ((b2 & 0x80) == 1)
				{
					byte b3 = this.ReadByte();
					if ((b2 & 0x80) == 1)
					{
						byte b4 = this.ReadByte();
						return (multiplier * (((b1 | (b2 << 8)) | (b3 << 0x10)) | (b4 << 0x18)));
					}
					return (multiplier * ((b1 | (b2 << 8)) | (b3 << 0x10)));
				}
				return (multiplier * (b1 | (b2 << 8)));
			}
			return (multiplier * b1);
		}

		/// <summary>
		/// Reads a <see cref="DateTime" /> in a format that encodes the <see cref="System.DateTime.Kind" /> property in a 2-bit field
		/// and the <see cref="System.DateTime.Ticks" /> property in a 62-bit field.
		/// </summary>
		/// <returns>An object that is equivalent to the System.DateTime object that was serialized by the <see cref="System.DateTime.ToBinary" /> method.</returns>
		/// <exception cref="System.ArgumentException">
		///	The serialized <see cref="Int64" /> value is less than <see cref="System.DateTime.MinValue" />  or greater than <see cref="System.DateTime.MaxValue" />.
		///	</exception>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public DateTime ReadDateTime()
		{
			return ReadDateTime64();
		}
		/// <summary>
		/// Reads a <see cref="DateTime" /> in a format that encodes the <see cref="System.DateTime.Kind" /> property in a 2-bit field
		/// and the <see cref="System.DateTime.Ticks" /> property in a 62-bit field.
		/// </summary>
		/// <returns>An object that is equivalent to the System.DateTime object that was serialized by the <see cref="System.DateTime.ToBinary" /> method.</returns>
		/// <exception cref="System.ArgumentException">
		///	The serialized <see cref="Int64" /> value is less than <see cref="System.DateTime.MinValue" />  or greater than <see cref="System.DateTime.MaxValue" />.
		///	</exception>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public DateTime ReadDateTime64()
		{
			long l = ReadInt64();
			return DateTime.FromBinary(l);
		}
		/// <summary>
		/// Reads a <see cref="DateTime" /> in a format that encodes the <see cref="System.DateTime.Kind" /> property in a 2-bit field
		/// and the <see cref="System.DateTime.Ticks" /> property in a 30-bit field.
		/// </summary>
		/// <returns>An object that is equivalent to the System.DateTime object that was serialized by the <see cref="System.DateTime.ToBinary" /> method.</returns>
		/// <exception cref="System.ArgumentException">
		///	The serialized <see cref="Int32" /> value is less than <see cref="System.DateTime.MinValue" />  or greater than <see cref="System.DateTime.MaxValue" />.
		///	</exception>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public DateTime ReadDateTime32()
		{
			int l = ReadInt32();
			return DateTime.FromBinary(l);
		}

		/// <summary>
		/// Reads a <see cref="DateTime" /> in ISO-9660 format (yyyyMMddHHMMSSssT).
		/// </summary>
		/// <returns>The <see cref="DateTime" /> read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public DateTime ReadISO9660DateTime()
		{
			string year = ReadFixedLengthString(4);
			int nYear = int.Parse(year);

			string month = ReadFixedLengthString(2);
			int nMonth = int.Parse(month);

			string day = ReadFixedLengthString(2);
			int nDay = int.Parse(day);

			string hour = ReadFixedLengthString(2);
			int nHour = int.Parse(hour);

			string minute = ReadFixedLengthString(2);
			int nMinute = int.Parse(minute);

			string second = ReadFixedLengthString(2);
			int nSecond = int.Parse(second);

			string secondHundredths = ReadFixedLengthString(2);
			int nSecondHundredths = int.Parse(secondHundredths);

			// offset from Greenwich Mean Time, in 15-minute intervals,
			// as a twos complement signed number, positive for time
			// zones east of Greenwich, and negative for time zones
			// west of Greenwich
			sbyte gmtOffset = ReadSByte();

			return new DateTime(nYear, nMonth, nDay, nHour + gmtOffset, nMinute, nSecond, nSecondHundredths, DateTimeKind.Utc);
		}

		public int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			while (num2 != 35)
			{
				byte b = ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
				if ((b & 128) == 0)
				{
					return num;
				}
			}
			throw new ArgumentOutOfRangeException("Invalid 7-bit encoded Int32");
		}

		public string ReadFixedLengthUTF16EndianString(int byteCount, Encoding encoding = null)
		{
			if (byteCount % 2 != 0)
			{
				throw new ArgumentException("byteCount must be an even number");
			}

			if (encoding == null)
				encoding = Accessor.DefaultEncoding;

			byte[] data = ReadBytes(byteCount);

			// swap endians
			if (Endianness == Endianness.BigEndian)
			{
				for (int i = 0; i < data.Length; i += 2)
				{
					byte tmp = data[i + 1];
					data[i + 1] = data[i];
					data[i] = tmp;
				}
			}

			return encoding.GetString(data);
		}

		/// <summary>
		/// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
		/// </summary>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadLengthPrefixedString()
		{
			int num = 0;
			int num2 = Read7BitEncodedInt();
			if (num2 < 0) throw new ArgumentOutOfRangeException("invalid string length");
			if (num2 == 0) return String.Empty;

			int count = (num2 - num > 128) ? 128 : (num2 - num);
			return ReadFixedLengthString(count);
		}

		/// <summary>
		/// Reads a string of the specified length from the current stream. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(byte length)
		{
			return this.ReadFixedLengthString(length, base.Accessor.DefaultEncoding);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(int length)
		{
			return ReadFixedLengthString(length, base.Accessor.DefaultEncoding);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public string ReadFixedLengthString(uint length)
		{
			return this.ReadFixedLengthString(length, base.Accessor.DefaultEncoding);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(byte length, Encoding encoding)
		{
			return this.ReadFixedLengthString((int)length, encoding);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream using the specified encoding. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <param name="encoding">The <see cref="Encoding" /> to use to convert the bytes read into a <see cref="String" /> instance.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(int length, Encoding encoding)
		{
			byte[] id = ReadBytes(length);
			return encoding.GetString(id);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream using the specified encoding. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <param name="encoding">The <see cref="Encoding" /> to use to convert the bytes read into a <see cref="String" /> instance.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public string ReadFixedLengthString(uint length, Encoding encoding)
		{
			int l1 = (int)length;
			int l2 = ((int)(length - l1));
			byte[] id = ReadBytes(l1);
			if (l2 > 0)
			{
				Array.Resize(ref id, id.Length + l2);
				Array.Copy(ReadBytes(l2), 0, id, id.Length - l2, l2);
			}
			return encoding.GetString(id);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(long length)
		{
			return ReadFixedLengthString(length, base.Accessor.DefaultEncoding);
		}
		/// <summary>
		/// Reads a string of the specified length from the current stream using the specified encoding. This method does not trim null characters; use <see cref="String.TrimNull" /> to do this.
		/// </summary>
		/// <param name="length">The length of the string to read.</param>
		/// <param name="encoding">The <see cref="Encoding" /> to use to convert the bytes read into a <see cref="String" /> instance.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public string ReadFixedLengthString(long length, Encoding encoding)
		{
			return encoding.GetString(ReadBytes((ulong)length));
		}

		/// <summary>
		/// Reads a 16-byte (128-bit) <see cref="Guid" /> value from the current stream and advances the current position of the stream by sixteen bytes.
		/// </summary>
		/// <returns>A 16-byte (128-bit) <see cref="Guid" /> value read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public Guid ReadGuid()
		{
			uint a = 0;
			ushort b = 0;
			ushort c = 0;
			byte d = 0;
			byte e = 0;
			byte f = 0;
			byte g = 0;
			byte h = 0;
			byte i = 0;
			byte j = 0;
			byte k = 0;
			if (!this.Reverse)
			{
				a = ReadUInt32();
				b = ReadUInt16();
				c = ReadUInt16();
				d = ReadByte();
				e = ReadByte();
				f = ReadByte();
				g = ReadByte();
				h = ReadByte();
				i = ReadByte();
				j = ReadByte();
				k = ReadByte();
			}
			else
			{
				k = ReadByte();
				j = ReadByte();
				i = ReadByte();
				h = ReadByte();
				g = ReadByte();
				f = ReadByte();
				e = ReadByte();
				d = ReadByte();
				c = ReadUInt16();
				b = ReadUInt16();
				a = ReadUInt32();
			}
			return new Guid(a, b, c, d, e, f, g, h, i, j, k);
		}
		/// <summary>
		/// Reads an array of 16-byte (128-bit) <see cref="Guid" /> values from the current stream and advances the current position of the stream by sixteen bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 16-byte (128-bit) <see cref="Guid" /> values read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public Guid[] ReadGuidArray(int count)
		{
			Guid[] retval = new Guid[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadGuid();
			}
			return retval;
		}

		/// <summary>
		/// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
		/// </summary>
		/// <returns>A 2-byte signed integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public short ReadInt16()
		{
			byte[] buffer = ReadBytes((uint)2);
			byte[] _buffer = new byte[2];
			if (base.Endianness == Endianness.LittleEndian)
			{
				_buffer[0] = buffer[0];
				_buffer[1] = buffer[1];
			}
			else if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = buffer[1];
				_buffer[1] = buffer[0];
			}
			return BitConverter.ToInt16(_buffer, 0);
		}
		/// <summary>
		/// Reads an array of 2-byte signed integers from the current stream and advances the current position of the stream by two bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 2-byte signed integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public short[] ReadInt16Array(int count)
		{
			short[] retval = new short[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt16();
			}
			return retval;
		}
		/// <summary>
		/// Reads a 2-byte unsigned integer from the current stream and advances the current position of the stream by two bytes.
		/// </summary>
		/// <returns>A 2-byte unsigned integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public ushort ReadUInt16()
		{
			byte[] buffer = ReadBytes(2);
			if (base.Endianness == Endianness.LittleEndian)
			{
				return (ushort)(buffer[0] | (buffer[1] << 8));
			}
			return (ushort)(buffer[1] | (buffer[0] << 8));
		}
		/// <summary>
		/// Reads an array of 2-byte unsigned integers from the current stream and advances the current position of the stream by two bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 2-byte unsigned integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public ushort[] ReadUInt16Array(int count)
		{
			ushort[] retval = new ushort[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt16();
			}
			return retval;
		}
		/// <summary>
		/// Reads a 3-byte signed integer from the current stream and advances the current position of the stream by three bytes.
		/// </summary>
		/// <returns>A 3-byte signed integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public int ReadInt24()
		{
			byte[] buffer = ReadBytes((uint)3);
			byte[] _buffer = new byte[3];
			if (base.Endianness == Endianness.LittleEndian)
			{
				_buffer[0] = buffer[0];
				_buffer[1] = buffer[1];
				_buffer[2] = buffer[2];
				_buffer[3] = 0;
			}
			else if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = 0;
				_buffer[1] = buffer[2];
				_buffer[2] = buffer[1];
				_buffer[3] = buffer[0];
			}
			return BitConverter.ToInt32(_buffer, 0);
		}
		/// <summary>
		/// Reads an array of 3-byte signed integers from the current stream and advances the current position of the stream by three bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 3-byte signed integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public int[] ReadInt24Array(int count)
		{
			int[] retval = new int[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt24();
			}
			return retval;
		}
		/// <summary>
		/// Reads a 3-byte unsigned integer from the current stream and advances the current position of the stream by three bytes.
		/// </summary>
		/// <returns>A 3-byte unsigned integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public uint ReadUInt24()
		{
			// TODO: Test this out!
			byte[] buffer = ReadBytes(3);
			if (base.Endianness == Endianness.LittleEndian)
			{
				return (uint)((buffer[2] << 16) | (buffer[1] << 8) | (buffer[0]));
			}
			return (uint)((buffer[2]) | (buffer[1] << 8) | (buffer[0] << 16));
		}
		/// <summary>
		/// Reads an array of 3-byte unsigned integers from the current stream and advances the current position of the stream by three bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 3-byte unsigned integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public uint[] ReadUInt24Array(int count)
		{
			uint[] retval = new uint[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt24();
			}
			return retval;
		}
		/// <summary>
		/// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.
		/// </summary>
		/// <returns>A 4-byte signed integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public int ReadInt32()
		{
			byte[] buffer = ReadBytes((uint)4);
			byte[] _buffer = new byte[4];
			if (base.Endianness == Endianness.LittleEndian)
			{
				_buffer[0] = buffer[0];
				_buffer[1] = buffer[1];
				_buffer[2] = buffer[2];
				_buffer[3] = buffer[3];
			}
			else if (base.Endianness == Endianness.BigEndian)
			{
				_buffer[0] = buffer[3];
				_buffer[1] = buffer[2];
				_buffer[2] = buffer[1];
				_buffer[3] = buffer[0];
			}
			return BitConverter.ToInt32(_buffer, 0);
		}
		/// <summary>
		/// Reads an array of 4-byte signed integers from the current stream and advances the current position of the stream by four bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 4-byte signed integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public int[] ReadInt32Array(int count)
		{
			int[] retval = new int[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt32();
			}
			return retval;
		}
		/// <summary>
		/// Reads a 4-byte unsigned integer from the current stream but does not advance the current position of the stream.
		/// </summary>
		/// <returns>A 4-byte unsigned integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public uint PeekUInt32()
		{
			uint value = ReadUInt32();
			Seek(-4, SeekOrigin.Current);
			return value;
		}
		/// <summary>
		/// Reads a 4-byte unsigned integer from the current stream and advances the current position of the stream by four bytes.
		/// </summary>
		/// <returns>A 4-byte unsigned integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public uint ReadUInt32()
		{
			byte[] buffer = ReadBytes((uint)4);
			if (base.Endianness == Endianness.LittleEndian)
			{
				return (uint)(((buffer[0] | (buffer[1] << 8)) | (buffer[2] << 0x10)) | (buffer[3] << 0x18));
			}
			return (uint)(((buffer[3] | (buffer[2] << 8)) | (buffer[1] << 0x10)) | (buffer[0] << 0x18));
		}
		/// <summary>
		/// Reads an array of 4-byte unsigned integers from the current stream and advances the current position of the stream by four bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 4-byte unsigned integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public uint[] ReadUInt32Array(int count)
		{
			uint[] retval = new uint[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt32();
			}
			return retval;
		}
		/// <summary>
		/// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.
		/// </summary>
		/// <returns>An 8-byte signed integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public long ReadInt64()
		{
			byte[] buffer = ReadBytes((uint)8);
			byte[] _buffer = new byte[8];
			if (base.Endianness == Endianness.LittleEndian)
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
			else if (base.Endianness == Endianness.BigEndian)
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
			return BitConverter.ToInt64(_buffer, 0);
		}
		/// <summary>
		/// Reads an array of 8-byte signed integers from the current stream and advances the current position of the stream by eight bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 8-byte signed integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		public long[] ReadInt64Array(int count)
		{
			long[] retval = new long[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt64();
			}
			return retval;
		}
		/// <summary>
		/// Reads an 8-byte unsigned integer from the current stream and advances the current position of the stream by eight bytes.
		/// </summary>
		/// <returns>An 8-byte unsigned integer read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public ulong ReadUInt64()
		{
			byte[] buffer = ReadBytes((uint)8);
			byte[] _buffer = new byte[8];
			if (base.Endianness == Endianness.LittleEndian)
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
			else if (base.Endianness == Endianness.BigEndian)
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
			return BitConverter.ToUInt64(_buffer, 0);
		}
		/// <summary>
		/// Reads an array of 8-byte unsigned integers from the current stream and advances the current position of the stream by eight bytes times the number of values read.
		/// </summary>
		/// <param name="count">The number of values to read from the current stream.</param>
		/// <returns>An array of 8-byte unsigned integers read from the current stream.</returns>
		/// <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		[CLSCompliant(false)]
		public ulong[] ReadUInt64Array(int count)
		{
			ulong[] retval = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt64();
			}
			return retval;
		}

		public float ReadSingle()
		{
			byte[] buffer = ReadBytes((uint)4);
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
			return BitConverter.ToSingle(_buffer, 0);
		}
		public float[] ReadSingleArray(int count)
		{
			float[] retval = new float[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadSingle();
			}
			return retval;
		}

		/// <summary>
		/// Reads a 64-bit floating-point value.
		/// </summary>
		/// <returns>The double.</returns>
		public double ReadDouble()
		{
			byte[] buffer = ReadBytes((uint)8);
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
			return BitConverter.ToDouble(_buffer, 0);
		}
		public double[] ReadDoubleArray(int count)
		{
			double[] retval = new double[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadDouble();
			}
			return retval;
		}


		public int ReadVariableLengthInt32()
		{
			int value = ReadByte();
			byte c = 0;

			if ((value & 0x80) == 0x80)
			{
				value &= 0x7F;
				do
				{
					value = (value << 7) + ((c = ReadByte()) & 0x7F);
				}
				while ((c & 0x80) == 0x80);
			}

			return value;
		}
		public int[] ReadVariableLengthInt32Array(int count)
		{
			int[] retval = new int[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadVariableLengthInt32();
			}
			return retval;
		}

		[CLSCompliant(false)]
		public ulong ReadUInt48()
		{
			byte[] buffer = ReadBytes((uint)6);
			if (base.Endianness == Endianness.LittleEndian)
			{
				uint num = (uint)(((buffer[0] << 0x10)) | (buffer[1] << 0x18));
				uint num2 = (uint)(((buffer[2] | (buffer[3] << 8)) | (buffer[4] << 0x10)) | (buffer[5] << 0x18));
				return (ulong)(num | num2 << 0x20);
			}
			else
			{
				uint num = (uint)(((buffer[5] << 0x10)) | (buffer[4] << 0x18));
				uint num2 = (uint)(((buffer[3] | (buffer[2] << 8)) | (buffer[1] << 0x10)) | (buffer[0] << 0x18));
				return (ulong)(num << 0x20 | num2);
			}
		}
		[CLSCompliant(false)]
		public ulong[] ReadUInt48Array(int count)
		{
			ulong[] retval = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt48();
			}
			return retval;
		}


		public string ReadNullTerminatedString()
		{
			return this.ReadNullTerminatedString(base.Accessor.DefaultEncoding);
		}

		public string ReadNullTerminatedString(int maxLength)
		{
			return this.ReadNullTerminatedString(maxLength, base.Accessor.DefaultEncoding);
		}

		public string ReadNullTerminatedString(Encoding encoding)
		{
			List<byte> r = new List<byte>();
			while (true)
			{
				byte nextChar = ReadByte();
				if ((nextChar == 0 && !(encoding == Encoding.UTF16LittleEndian)) || ((encoding == Encoding.UTF16LittleEndian) && (nextChar == 0 && (r.Count > 2 && r[r.Count - 1] == 0))))
				{
					string result = encoding.GetString(r.ToArray());
					return result;
				}
				r.Add(nextChar);
			}
		}

		public string ReadNullTerminatedString(int maxLength, Encoding encoding)
		{
			string ret = this.ReadNullTerminatedString(encoding);
			if (ret.Length > maxLength)
			{
				return ret.Substring(0, maxLength);
			}
			if (ret.Length < maxLength)
			{
				ReadBytes((maxLength - ret.Length) - 1);
			}
			return ret;
		}

		/// <summary>
		/// Reads a length-prefixed string that is prefixed with a signed short (2-byte) length, rather than an int (4-byte) length.
		/// </summary>
		/// <returns></returns>
		public string ReadInt16String()
		{
			short length = ReadInt16();
			return this.ReadFixedLengthString((int)length);
		}
		/// <summary>
		/// Reads a length-prefixed string that is prefixed with an unsigned short (2-byte) length, rather than an int (4-byte) length.
		/// </summary>
		/// <returns></returns>
		public string ReadUInt16String()
		{
			ushort length = ReadUInt16();
			return this.ReadFixedLengthString((uint)length);
		}

		public byte[] ReadToEnd()
		{
			return ReadBytes(base.Accessor.Remaining);
		}
		public string ReadStringToEnd(Encoding encoding = null)
		{
			if (encoding == null) encoding = Encoding.Default;
			byte[] data = ReadToEnd();
			return encoding.GetString(data);
		}

		public byte[] ReadUntil(byte[] sequence)
		{
			return ReadUntil(sequence, false);
		}
		public byte[] ReadUntil(byte[] sequence, bool includeSequence)
		{
			byte[] w = new byte[0];
			while (!EndOfStream)
			{
				Array.Resize(ref w, w.Length + 1);
				w[w.Length - 1] = ReadByte();

				bool matches = true;
				for (int i = 0; i < sequence.Length; i++)
				{
					if (w.Length < sequence.Length)
					{
						matches = false;
						break;
					}
					if (w[w.Length - (sequence.Length - i)] != sequence[i])
					{
						matches = false;
						break;
					}
				}

				if (matches)
				{
					if (!includeSequence)
					{
						Array.Resize(ref w, w.Length - sequence.Length);
						Seek(-sequence.Length, SeekOrigin.Current);
					}
					return w;
				}
			}
			return w;
		}
		public string ReadUntil(string sequence)
		{
			return ReadUntil(sequence, base.Accessor.DefaultEncoding);
		}
		public string ReadUntil(string sequence, bool includeSequence)
		{
			return ReadUntil(sequence, base.Accessor.DefaultEncoding, includeSequence);
		}
		public string ReadUntil(string sequence, Encoding encoding)
		{
			return encoding.GetString(ReadUntil(sequence.ToCharArray(), encoding));
		}
		public string ReadUntil(string sequence, Encoding encoding, bool includeSequence)
		{
			return new string(ReadUntil(sequence.ToCharArray(), encoding, includeSequence));
		}
		public byte[] ReadUntil(char[] sequence)
		{
			return this.ReadUntil(sequence, base.Accessor.DefaultEncoding);
		}
		public char[] ReadUntil(char[] sequence, bool includeSequence)
		{
			return this.ReadUntil(sequence, base.Accessor.DefaultEncoding, includeSequence);
		}
		public byte[] ReadUntil(char[] sequence, Encoding encoding)
		{
			return this.ReadUntil(encoding.GetBytes(sequence));
		}
		public char[] ReadUntil(char[] sequence, Encoding encoding, bool includeSequence)
		{
			return encoding.GetChars(this.ReadUntil(encoding.GetBytes(sequence), includeSequence));
		}
		public string ReadStringUntil(string sequence)
		{
			return ReadStringUntil(sequence, base.Accessor.DefaultEncoding, base.Accessor.DefaultEncoding);
		}
		public string ReadStringUntil(string sequence, bool includeSequence)
		{
			return ReadStringUntil(sequence, base.Accessor.DefaultEncoding, base.Accessor.DefaultEncoding, includeSequence);
		}
		public string ReadStringUntil(string sequence, Encoding inputEncoding, Encoding outputEncoding)
		{
			return ReadStringUntil(sequence.ToCharArray(), inputEncoding, outputEncoding);
		}
		public string ReadStringUntil(string sequence, Encoding inputEncoding, Encoding outputEncoding, bool includeSequence)
		{
			return ReadStringUntil(sequence.ToCharArray(), inputEncoding, outputEncoding, includeSequence);
		}
		public string ReadStringUntil(char[] sequence)
		{
			return ReadStringUntil(sequence, base.Accessor.DefaultEncoding, base.Accessor.DefaultEncoding);
		}
		public string ReadStringUntil(char[] sequence, bool includeSequence)
		{
			return ReadStringUntil(sequence, base.Accessor.DefaultEncoding, base.Accessor.DefaultEncoding, includeSequence);
		}
		public string ReadStringUntil(char[] sequence, Encoding inputEncoding, Encoding outputEncoding)
		{
			byte[] bytes = ReadUntil(inputEncoding.GetBytes(sequence));
			return outputEncoding.GetString(bytes);
		}
		public string ReadStringUntil(char[] sequence, Encoding inputEncoding, Encoding outputEncoding, bool includeSequence)
		{
			return outputEncoding.GetString(ReadUntil(inputEncoding.GetBytes(sequence), includeSequence));
		}

		public void SeekUntilFirstNonNull()
		{
			while (PeekByte() == 0)
			{
				ReadChar();
			}
		}

		public string[] ReadNullTerminatedStringArray(int stringTableSize)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			long endpos = base.Accessor.Position + stringTableSize;
			while (base.Accessor.Position < endpos)
			{
				list.Add(ReadNullTerminatedString());
			}
			return list.ToArray();
		}

		// TODO: TEST THIS!!
		public decimal ReadDecimal()
		{
			byte[] buffer = ReadBytes(16);
			int num = (int)buffer[0] | (int)buffer[1] << 8 | (int)buffer[2] << 16 | (int)buffer[3] << 24;
			int num2 = (int)buffer[4] | (int)buffer[5] << 8 | (int)buffer[6] << 16 | (int)buffer[7] << 24;
			int num3 = (int)buffer[8] | (int)buffer[9] << 8 | (int)buffer[10] << 16 | (int)buffer[11] << 24;
			int flags = (int)buffer[12] | (int)buffer[13] << 8 | (int)buffer[14] << 16 | (int)buffer[15] << 24;

			bool isNegative = ((flags & -2147483648) == -2147483648);
			byte scale = (byte)(flags >> 16);

			if ((flags & 2130771967) == 0 && (flags & 16711680) <= 1835008)
			{
				return new Decimal(num, num2, num3, isNegative, scale);
			}
			throw new ArgumentOutOfRangeException("Invalid decimal");
		}

		public string ReadByteSizedString()
		{
			byte len = ReadByte();
			return ReadFixedLengthString(len);
		}

		public short ReadDoubleEndianInt16()
		{
			short value1 = ReadInt16();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}
			short value2 = ReadInt16();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}
		[CLSCompliant(false)]
		public ushort ReadDoubleEndianUInt16()
		{
			ushort value1 = ReadUInt16();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}
			ushort value2 = ReadUInt16();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}
		public int ReadDoubleEndianInt32()
		{
			int value1 = ReadInt32();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}
			int value2 = ReadInt32();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}
		[CLSCompliant(false)]
		public uint ReadDoubleEndianUInt32()
		{
			uint value1 = ReadUInt32();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}
			uint value2 = ReadUInt32();
			if (base.Endianness == Endianness.LittleEndian)
			{
				base.Endianness = Endianness.BigEndian;
			}
			else
			{
				base.Endianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}

		private int ToInt32(byte[] buffer)
		{
			int ret = 0;
			int mode = 0;
			for (int i = 0; i < Math.Min(4, buffer.Length); i++)
			{
				ret |= (buffer[i] << mode);
				mode += 8;
			}
			return ret;
		}

		public int ReadCompactInt32New()
		{
			byte[] buffer = new byte[2];
			int start = 0;
			int length = buffer.Length;
			while (true)
			{
				Read(buffer, start, length);
				if (buffer[buffer.Length - 1] == 0 || (buffer.Length > 4))
				{
					return ToInt32(buffer);
				}
				else
				{
					start = buffer.Length;
					length = 1;
					Array.Resize(ref buffer, buffer.Length + 1);
				}
			}
		}

		public object ReadBEncodedObject()
		{
			char w = (char)PeekChar();
			switch (w)
			{
				case 'd':
					{
						// Read the starting 'd'
						w = ReadChar();

						Dictionary<string, object> dict = new Dictionary<string, object>();
						while (w != 'e')
						{
							string key = (string)ReadBEncodedObject();
							object value = ReadBEncodedObject();
							w = (char)PeekChar();
							dict.Add(key, value);
						}

						// Read the final 'e'
						w = ReadChar();

						return dict;
					}
				case 'l':
					{
						// Read the starting 'l'
						w = ReadChar();

						List<object> list = new List<object>();
						while (w != 'e')
						{
							object item = ReadBEncodedObject();
							w = (char)PeekChar();

							list.Add(item);
						}

						// Read the final 'e'
						w = ReadChar();
						return list;
					}
				case 'i':
					{
						// Read the starting 'i'
						w = ReadChar();
						string num = String.Empty;
						while (w != 'e')
						{
							w = ReadChar();
							if (w != 'e')
							{
								num += w;
							}
						}
						// Already read the final 'e'

						return Int32.Parse(num);
					}
				default:
					{
						// Assume a string
						w = (char)PeekChar();
						string num = String.Empty;
						string val = String.Empty;
						while (w != ':')
						{
							w = ReadChar();
							if (w != ':')
							{
								num += w;
							}
						}

						uint nnum = UInt32.Parse(num);
						val = ReadFixedLengthString(nnum);

						return val;
					}
			}
		}
		/// <summary>
		/// Reads a 32-bit integer length-prefixed string using the system default encoding.
		/// </summary>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public string ReadInt32String()
		{
			return ReadInt32String(base.Accessor.DefaultEncoding);
		}
		/// <summary>
		/// Reads a 32-bit integer length-prefixed string using the specified encoding.
		/// </summary>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public string ReadInt32String(Encoding encoding)
		{
			int length = ReadInt32();
			return ReadFixedLengthString(length);
		}

		/// <summary>
		/// Reads a <see cref="Version" /> from the
		/// </summary>
		/// <returns></returns>
		public Version ReadVersion()
		{
			byte parts = ReadByte();
			switch (parts)
			{
				case 1:
					{
						int vmaj = ReadInt32();
						return new Version(vmaj, 0);
					}
				case 2:
					{
						int vmaj = ReadInt32();
						int vmin = ReadInt32();
						return new Version(vmaj, vmin);
					}
				case 3:
					{
						int vmaj = ReadInt32();
						int vmin = ReadInt32();
						int vbld = ReadInt32();
						return new Version(vmaj, vmin, vbld);
					}
				case 4:
					{
						int vmaj = ReadInt32();
						int vmin = ReadInt32();
						int vbld = ReadInt32();
						int vrev = ReadInt32();

						if (vbld > -1)
						{
							if (vrev > -1)
							{
								return new Version(vmaj, vmin, vbld, vrev);
							}
							else
							{
								return new Version(vmaj, vmin, vbld);
							}
						}
						else
						{
							return new Version(vmaj, vmin);
						}
					}
			}
			return new Version();
		}

		public void SeekUntil(string find)
		{
			SeekUntil(find, base.Accessor.DefaultEncoding);
		}
		public void SeekUntil(string find, Encoding encoding)
		{
			byte[] data = encoding.GetBytes(find);
			byte[] dataCmp = new byte[data.Length];
			while (base.Accessor.Position <= (base.Accessor.Length - data.Length))
			{
				long rest = base.Accessor.Remaining;

				base.Accessor.ReadInternal(dataCmp, 0, dataCmp.Length);
				base.Accessor.Seek(-(dataCmp.Length - 1), SeekOrigin.Current);

				bool bad = false;
				for (int i = 0; i < data.Length; i++)
				{
					if (dataCmp[i] != data[i])
					{
						bad = true;
						break;
					}
				}
				if (!bad)
				{
					base.Accessor.Seek(-1, SeekOrigin.Current);
					break;
				}
			}
		}

		private short[] ReadInt16ArrayWTF(int count)
		{
			byte[] buffer = new byte[count * 2];
			Read(buffer, 0, buffer.Length);

			short[] buffer2 = new short[count];
			for (int i = 0; i < buffer.Length; i += 2)
			{
				byte b1 = buffer[i];
				byte b2 = buffer[i + 1];
				int index = (int)(i / 2);

				if (base.Endianness == Endianness.LittleEndian)
				{
					buffer2[index] = (short)(b1 | (b2 << 8));
				}
				else if (base.Endianness == Endianness.BigEndian)
				{
					buffer2[index] = (short)(b2 | (b1 << 8));
				}
			}
			return buffer2;
		}

		public string PeekFixedLengthString(int count)
		{
			return PeekFixedLengthString(count, base.Accessor.DefaultEncoding);
		}
		public string PeekFixedLengthString(int count, Encoding encoding)
		{
			byte[] data = PeekBytes(count);
			return encoding.GetString(data);
		}

		/// <summary>
		/// Reads a half (2 bytes/half instead of 4 bytes/single) as a floating-point value.
		/// </summary>
		/// <returns></returns>
		public float ReadHalf()
		{
			byte[] buffer = ReadBytes(2);
			byte[] buffer2 = new byte[4];
			if (base.Endianness == Endianness.LittleEndian)
			{
				buffer2[0] = 0;
				buffer2[1] = 0;
				buffer2[2] = buffer[0];
				buffer2[3] = buffer[1];
			}
			else
			{
				buffer2[0] = buffer[0];
				buffer2[1] = buffer[1];
				buffer2[2] = 0;
				buffer2[3] = 0;
			}
			return BitConverter.ToSingle(buffer2, 0);
		}

		public int ReadAtMostBytes(byte[] buffer, int count)
		{
			if (base.Accessor.Remaining == 0) return 0;

			if (count < base.Accessor.Remaining)
			{
				Read(buffer, 0, count);
				return count;
			}
			else
			{
				Read(buffer, 0, (int)base.Accessor.Remaining);
				return (int)base.Accessor.Remaining;
			}
		}

		private byte[] read_buf = new byte[4096];
		private int getbit_buf = 0;
		private int getbit_len = 0;
		private int getbit_count = 0;
		private int getbit_mask = 0;

		public int ReadBitsAsInt32(int count)
		{
			int i, x = 0;

			for (i = 0; i < count; i++)
			{
				if (getbit_mask == 0)
				{
					if (getbit_len == getbit_count)
					{
						getbit_len = ReadAtMostBytes(read_buf, 4096);
						if (getbit_len == 0) throw new EndOfStreamException();
						getbit_count = 0;
					}

					getbit_buf = read_buf[getbit_count++];
					getbit_mask = 128;
				}
				x <<= 1;
				if ((getbit_buf & getbit_mask) != 0) x |= 1;
				getbit_mask >>= 1;
			}
			return x;
		}

		public bool EndOfStream { get { return base.Accessor.Remaining == 0; } }

		public string ReadInt64String()
		{
			long length = ReadInt64();
			string value = ReadFixedLengthString(length);
			return value;
		}

		public string ReadUntil(string[] until)
		{
			string rest = null;
			return ReadUntil(until, out rest);
		}
		public string ReadUntil(string[] until, out string rest)
		{
			return ReadUntil(until, null, null, out rest);
		}
		public string ReadUntil(string until, string ignoreBegin, string ignoreEnd)
		{
			return ReadUntil(new string[] { until }, ignoreBegin, ignoreEnd);
		}
		public string ReadUntil(string[] until, string ignoreBegin, string ignoreEnd)
		{
			string rest = null;
			return ReadUntil(until, ignoreBegin, ignoreEnd, out rest);
		}
		public string ReadUntil(string[] until, string ignoreBegin, string ignoreEnd, out string rest)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			while (!EndOfStream)
			{
				sb.Append(ReadChar());

				foreach (string s in until)
				{
					if (sb.ToString().EndsWith(s))
					{
						string w = sb.ToString();
						string retval = w.Substring(0, w.Length - 1);
						rest = w.Substring(w.Length - 1);
						return retval;
					}
				}

				/*
				char[] buffer = new char[until.Length * 2];
				ReadBlock(buffer, 0, until.Length * 2);

				string w = new string(buffer);
				if (w.Contains(until))
				{
					string ww = w.Substring(0, w.IndexOf(until));
					sb.Append(ww);

					// back up the stream reader
					int indexOfUntil = (w.IndexOf(until) + until.Length);
					int lengthToBackUp = w.Length - indexOfUntil;
					BaseStream.Seek(-1 * lengthToBackUp, SeekOrigin.Current);
					break;
				}
				sb.Append(w);
				*/
			}
			rest = null;
			return sb.ToString();
		}

		public string ReadBetween(string start, string end, bool discard)
		{
			string nextstr = String.Empty;
			bool inside = false;
			// 0000000-3842-17774-}ehaomfd
			while (!EndOfStream)
			{
				nextstr += ReadChar();
				if (!inside)
				{
					if (nextstr.EndsWith(start))
					{
						inside = true;
						nextstr = String.Empty;
						if (!discard) nextstr += start;
					}
				}
				else
				{
					if (nextstr.EndsWith(end))
					{
						if (discard)
						{
							nextstr = nextstr.Substring(0, nextstr.Length - end.Length);
						}
						return nextstr;
					}
				}
			}
			return String.Empty;
		}

		public string ReadLine()
		{
			string line = ReadUntil(GetNewLineSequence());
			ReadBytes(GetNewLineSequence().Length);
			if (line.EndsWith("\r"))
				line = line.Substring(0, line.Length - 1);
			return line;
		}

		/// <summary>
		/// Sets the current position of the associated <see cref="Accessor" />.
		/// </summary>
		/// <param name="offset">The offset at which to place the associated <see cref="Accessor" /> relative to the specified <see cref="SeekOrigin" />.</param>
		/// <param name="origin">The <see cref="SeekOrigin" /> that the <see cref="offset" /> is relative to.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output.</exception>
		/// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		public void Seek(int offset, SeekOrigin origin)
		{
			base.Accessor.Seek(offset, origin);
		}
		/// <summary>
		/// Sets the current position of the associated <see cref="Accessor" />.
		/// </summary>
		/// <param name="offset">The offset at which to place the associated <see cref="Accessor" /> relative to the specified <see cref="SeekOrigin" />.</param>
		/// <param name="origin">The <see cref="SeekOrigin" /> that the <see cref="offset" /> is relative to.</param>
		/// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output.</exception>
		/// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		public void Seek(long offset, SeekOrigin origin)
		{
			base.Accessor.Seek(offset, origin);
		}
		/// <summary>
		/// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
		/// </summary>
		public void Close()
		{
			base.Accessor.Close();
		}

		/// <summary>
		/// Gets the amount of data remaining to be read by the associated <see cref="Accessor" />.
		/// </summary>
		/// <exception cref="System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output.</exception>
		/// <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		public long Remaining { get { return base.Accessor.Remaining; } }

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
			long paddingCount = ((alignTo - (Accessor.Position % alignTo)) % alignTo);
			paddingCount += extraPadding;

			if (Accessor.Position + paddingCount < Accessor.Length)
				Accessor.Position += paddingCount;
		}

		public string ReadStringUntilAny(char[] anyOf)
		{
			StringBuilder sb = new StringBuilder();
			while (!EndOfStream)
			{
				char c = ReadChar();
				bool found = false;
				for (int i = 0; i < anyOf.Length; i++)
				{
					if (c == anyOf[i])
					{
						found = true;
						break;
					}
				}
				if (found) break;
				sb.Append(c);
			}
			Seek(-1, SeekOrigin.Current);
			return sb.ToString();
		}
	}
}
