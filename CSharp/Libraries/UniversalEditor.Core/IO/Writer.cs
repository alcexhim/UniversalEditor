// Universal Editor input/output module for writing binary data
// Copyright (C) 2011  Mike Becker
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

using System;
using System.Diagnostics;
using System.IO;

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
		public void WriteSByte(sbyte value)
		{
			WriteBytes(new byte[] { (byte)value });
		}
		public void WriteBytes(byte[] data)
		{
			if (data == null) return;
			Write(data, 0, data.Length);
		}

		public void WriteFixedLengthBytes(byte[] data, int count)
		{
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
		public void WriteFixedLengthString(string value, uint length)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length);
		}
		public void WriteFixedLengthString(string value, Encoding encoding)
		{
			byte[] data = encoding.GetBytes(value);
			WriteBytes(data);
		}
		public void WriteFixedLengthString(string value, int length, char paddingChar)
		{
			WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length, paddingChar);
		}
		public void WriteFixedLengthString(string value, uint length, char paddingChar)
		{
			this.WriteFixedLengthString(value, base.Accessor.DefaultEncoding, length, paddingChar);
		}
		public void WriteFixedLengthString(string value, Encoding encoding, int length)
		{
			this.WriteFixedLengthString(value, encoding, length, '\0');
		}
		public void WriteFixedLengthString(string value, Encoding encoding, uint length)
		{
			WriteFixedLengthString(value, encoding, length, '\0');
		}
		public void WriteFixedLengthString(string value, Encoding encoding, int length, char paddingChar)
		{
			WriteFixedLengthString(value, encoding, (uint)length, paddingChar);
		}
		public void WriteFixedLengthString(string value, Encoding encoding, uint length, char paddingChar)
		{
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
			throw new NotImplementedException();
		}

		public void WriteNullTerminatedString(string sz)
		{
			if (sz != null)
			{
				for (int i = 0; i < sz.Length; i++)
				{
					WriteChar(sz[i]);
				}
			}
			WriteChar('\0');
		}
		public void WriteNullTerminatedString(string sz, Encoding encoding)
		{
			byte[] values = encoding.GetBytes(sz);
			
			WriteBytes(values);
			WriteInt16(0);
		}
		public void WriteNullTerminatedString(string sz, int length)
		{
			this.WriteFixedLengthString(sz, length);
		}
		
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
		public void WriteInt24 (int value)
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
		
		public void WriteObject (object value)
		{
			Type objectType = value.GetType ();
			
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
				// WriteCharArray((char[])value);
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
			
			System.Reflection.FieldInfo[] fis = (objectType.GetFields (System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
			foreach (System.Reflection.FieldInfo fi in fis)
			{
				// Type fieldType = fi.FieldType;
				WriteObject (fi.GetValue (value));
			}
		}
		
		public void WriteDateTime(DateTime value)
		{
			WriteInt64(value.ToBinary());
		}

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


		public void Align(int paddingCount)
		{
			switch (paddingCount)
			{
				case 2:
				{
					long position = base.Accessor.Position;
					int num = (int)(position % 2L);
					byte[] array = new byte[num];
					array.Initialize();
					WriteBytes(array);
					break;
				}
				case 4:
				{
					long num = base.Accessor.Position;
					num = (num + 3L & -4L);
					long num2 = num - base.Accessor.Position;
					byte[] array = new byte[num2];
					array.Initialize();
					WriteBytes(array);
					break;
				}
				default:
				{
					long count = (base.Accessor.Position % paddingCount);
					byte[] array = new byte[count];
					WriteBytes(array);
					break;
				}
			}
		}


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
		public void WriteDoubleEndianUInt16(ushort value)
		{
			WriteUInt16(value);
			SwapEndianness();
			WriteUInt16(value);
			SwapEndianness();
		}
		public void WriteDoubleEndianUInt32(uint value)
		{
			WriteUInt32(value);
			SwapEndianness();
			WriteUInt32(value);
			SwapEndianness();
		}
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

		public void Flush()
		{
			base.Accessor.FlushInternal();
		}
		public void Close()
		{
			base.Accessor.Close();
		}
	}
}

