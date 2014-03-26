// Universal Editor input/output module for reading binary data
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
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace UniversalEditor.IO
{
	// [DebuggerNonUserCode()]
	public class Reader
	{
        private Endianness mvarEndianness = Endianness.LittleEndian;
        public Endianness Endianness { get { return mvarEndianness; } set { mvarEndianness = value; } }

        private bool mvarReverse = false;
        public bool Reverse { get { return mvarReverse; } }

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } }

        private NewLineSequence mvarNewLineSequence = NewLineSequence.Default;
        public NewLineSequence NewLineSequence { get { return mvarNewLineSequence; } set { mvarNewLineSequence = value; } }
        public string GetNewLineSequence()
        {
            string newline = System.Environment.NewLine;
            switch (mvarNewLineSequence)
            {
                case IO.NewLineSequence.CarriageReturn:
                {
                    newline = "\r";
                    break;
                }
                case IO.NewLineSequence.LineFeed:
                {
                    newline = "\n";
                    break;
                }
                case IO.NewLineSequence.CarriageReturnLineFeed:
                {
                    newline = "\r\n";
                    break;
                }
            }
            return newline;
        }

		public Reader(Accessor input)
		{
            this.mvarAccessor = input;
			this.mvarEndianness = Endianness.LittleEndian;
			this.mvarReverse = false;
		}

        public void Read(byte[] buffer, int start, int length)
        {
            mvarAccessor.ReadInternal(buffer, start, length);
        }

		public bool ReadBoolean()
		{
			return (ReadBytes(1)[0] != 0);
		}

        public byte ReadByte()
        {
            return ReadBytes(1)[0];
        }
        public char ReadChar()
        {
            return (char)ReadByte();
        }
        public byte PeekByte()
        {
            byte b = ReadByte();
            mvarAccessor.Seek(-1, SeekOrigin.Current);
            return b;
        }
        public byte[] PeekBytes(int length)
        {
            byte[] buffer = new byte[length];
            int len = mvarAccessor.ReadInternal(buffer, 0, length);
            mvarAccessor.Seek(-len, SeekOrigin.Current);
            return buffer;
        }
        public char PeekChar()
        {
            return (char)PeekByte();
        }
        public sbyte ReadSByte()
        {
            return (sbyte)(ReadBytes(1)[0]);
        }
		public byte[] ReadBytes (uint length)
		{
			byte[] buf = new byte[length];
			uint lastct = 0;
			while (lastct < length)
			{
				int ct = (int)length;
				byte[] buf2 = new byte[ct];
				Read(buf2, 0, ct);

				Array.Copy (buf2, 0, buf, lastct, buf2.Length);
				lastct += (uint)ct;
			}
			return buf;
		}

		public byte[] ReadBytes(ulong length)
		{
			byte[] buf = new byte[length];
			for (ulong i = 0L; i < length; i += (ulong) 1L)
			{
				buf[(int)i] = ReadByte();
			}
			return buf;
		}

		[DebuggerNonUserCode()]
		public byte[] ReadBytes(long length)
		{
            byte[] buffer = new byte[length];
            mvarAccessor.ReadInternal(buffer, 0, (int)length);
			return buffer;
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
		
		public DateTime ReadISO9660DateTime ()
		{
			string year = ReadFixedLengthString (4);
			int nYear = int.Parse (year);
			
			string month = ReadFixedLengthString (2);
			int nMonth = int.Parse (month);
			
			string day = ReadFixedLengthString (2);
			int nDay = int.Parse (day);
			
			string hour = ReadFixedLengthString (2);
			int nHour = int.Parse (hour);
			
			string minute = ReadFixedLengthString (2);
			int nMinute = int.Parse (minute);
			
			string second = ReadFixedLengthString (2);
			int nSecond = int.Parse (second);
			
			string secondHundredths = ReadFixedLengthString (2);
			int nSecondHundredths = int.Parse (secondHundredths);
			
			// offset from Greenwich Mean Time, in 15-minute intervals,
			// as a twos complement signed number, positive for time
			// zones east of Greenwich, and negative for time zones
			// west of Greenwich
			sbyte gmtOffset = ReadSByte ();
			
			return new DateTime (nYear, nMonth, nDay, nHour + gmtOffset, nMinute, nSecond, nSecondHundredths, DateTimeKind.Utc);
		}

        protected internal int Read7BitEncodedInt()
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

        private byte[] m_charBytes = null;
        private char[] m_charBuffer = null;
        private int m_maxCharsSize = 256;
        public string ReadLengthPrefixedString()
        {
            /*
            int num = 0;
            int num2 = Read7BitEncodedInt();
            if (num2 < 0) throw new ArgumentOutOfRangeException("invalid string length");

            if (num2 == 0) return String.Empty;

            if (this.m_charBytes == null)
            {
                this.m_charBytes = new byte[128];
            }
            if (this.m_charBuffer == null)
            {
                this.m_charBuffer = new char[this.m_maxCharsSize];
            }
            StringBuilder stringBuilder = null;
            int chars;
            while (true)
            {
                int count = (num2 - num > 128) ? 128 : (num2 - num);
                int num3 = mvarAccessor.Read(m_charBytes, 0, count);
                if (num3 == 0) throw new EndOfStreamException();

                chars = this.m_decoder.GetChars(this.m_charBytes, 0, num3, this.m_charBuffer, 0);
                if (num == 0 && num3 == num2)
                {
                    break;
                }
                stringBuilder.Append(this.m_charBuffer, 0, chars);
                num += num3;
                if (num >= num2)
                {
                    goto Block_11;
                }
            }
            return new string(this.m_charBuffer, 0, chars);
             * */
            throw new NotImplementedException();
        }

		public string ReadFixedLengthString(byte length)
		{
			return this.ReadFixedLengthString(length, mvarAccessor.DefaultEncoding);
		}

		public string ReadFixedLengthString(int length)
		{
			return ReadFixedLengthString(length, mvarAccessor.DefaultEncoding);
		}

		public string ReadFixedLengthString(uint length)
		{
			return this.ReadFixedLengthString(length, mvarAccessor.DefaultEncoding);
		}

		public string ReadFixedLengthString(byte length, Encoding encoding)
		{
			return this.ReadFixedLengthString((int) length, encoding);
		}

		public string ReadFixedLengthString(int length, Encoding encoding)
		{
			byte[] id = ReadBytes(length);
			return encoding.GetString(id);
		}

		public string ReadFixedLengthString(uint length, Encoding encoding)
		{
			int l1 = (int) length;
			int l2 = ((int)(length - l1));
			byte[] id = ReadBytes(l1);
			if (l2 > 0)
			{
				Array.Resize(ref id, id.Length + l2);
				Array.Copy(ReadBytes(l2), 0, id, id.Length - l2, l2);
			}
			return encoding.GetString(id);
		}
		public string ReadFixedLengthString(long length)
		{
			return ReadFixedLengthString(length, mvarAccessor.DefaultEncoding);
		}
		public string ReadFixedLengthString(long length, Encoding encoding)
		{
			return encoding.GetString(ReadBytes((ulong)length));
		}

		/// <summary>
		/// Reads a 16-byte (128-bit) GUID value from the stream.
		/// </summary>
		/// <returns></returns>
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
			if (!this.mvarReverse)
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
		public Guid[] ReadGuidArray(int count)
		{
			Guid[] retval = new Guid[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadGuid();
			}
			return retval;
		}

		public short ReadInt16()
        {
            byte[] buffer = ReadBytes((uint)2);
            byte[] _buffer = new byte[2];
            if (this.mvarEndianness == Endianness.LittleEndian)
            {
                _buffer[0] = buffer[0];
                _buffer[1] = buffer[1];
            }
            else if (mvarEndianness == IO.Endianness.BigEndian)
            {
                _buffer[0] = buffer[1];
                _buffer[1] = buffer[0];
            }
            return BitConverter.ToInt16(_buffer, 0);
		}
		public short[] ReadInt16Array(int count)
		{
			short[] retval = new short[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt16();
			}
			return retval;
		}

		public int ReadInt24()
		{
            byte[] buffer = ReadBytes((uint)3);
            byte[] _buffer = new byte[3];
            if (this.mvarEndianness == Endianness.LittleEndian)
            {
                _buffer[0] = buffer[0];
                _buffer[1] = buffer[1];
                _buffer[2] = buffer[2];
                _buffer[3] = 0;
            }
            else if (mvarEndianness == IO.Endianness.BigEndian)
            {
                _buffer[0] = 0;
                _buffer[1] = buffer[2];
                _buffer[2] = buffer[1];
                _buffer[3] = buffer[0];
            }
            return BitConverter.ToInt32(_buffer, 0);
		}
		public int[] ReadInt24Array(int count)
		{
			int[] retval = new int[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt24();
			}
			return retval;
		}

		public int ReadInt32()
        {
            byte[] buffer = ReadBytes((uint)4);
            byte[] _buffer = new byte[4];
            if (this.mvarEndianness == Endianness.LittleEndian)
            {
                _buffer[0] = buffer[0];
                _buffer[1] = buffer[1];
                _buffer[2] = buffer[2];
                _buffer[3] = buffer[3];
            }
            else if (mvarEndianness == IO.Endianness.BigEndian)
            {
                _buffer[0] = buffer[3];
                _buffer[1] = buffer[2];
                _buffer[2] = buffer[1];
                _buffer[3] = buffer[0];
            }
            return BitConverter.ToInt32(_buffer, 0);
		}
		public int[] ReadInt32Array(int count)
		{
			int[] retval = new int[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt32();
			}
			return retval;
		}

		public long ReadInt64()
        {
            byte[] buffer = ReadBytes((uint)8);
            byte[] _buffer = new byte[8];
			if (this.mvarEndianness == Endianness.LittleEndian)
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
            else if (mvarEndianness == IO.Endianness.BigEndian)
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
		public long[] ReadInt64Array(int count)
		{
			long[] retval = new long[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadInt64();
			}
			return retval;
		}

		public float ReadSingle()
        {
            byte[] buffer = ReadBytes((uint)4);
            byte[] _buffer = new byte[4];
            if (mvarEndianness == Endianness.BigEndian)
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

		public double ReadDouble()
		{
			byte[] buffer = ReadBytes((uint)8);
            byte[] _buffer = new byte[8];
            if (mvarEndianness == Endianness.BigEndian)
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

		public ushort ReadUInt16 ()
        {
            byte[] buffer = ReadBytes(2);
			if (mvarEndianness == Endianness.LittleEndian)
            {
                return (ushort)(buffer[0] | (buffer[1] << 8));
            }
            return (ushort)(buffer[1] | (buffer[0] << 8));
		}
		public ushort[] ReadUInt16Array(int count)
		{
			ushort[] retval = new ushort[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt16();
			}
			return retval;
		}

		public uint ReadUInt24()
		{
            // TODO: Test this out!
            byte[] buffer = ReadBytes(3);
			if (mvarEndianness == Endianness.LittleEndian)
			{
				return (uint)((buffer[2] << 16) | (buffer[1] << 8) | (buffer[0]));
			}
            return (uint)((buffer[2]) | (buffer[1] << 8) | (buffer[0] << 16));
		}
		public uint[] ReadUInt24Array(int count)
		{
			uint[] retval = new uint[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt24();
			}
			return retval;
		}

		public uint ReadUInt32()
        {
            byte[] buffer = ReadBytes((uint)4);
			if (this.mvarEndianness == Endianness.LittleEndian)
            {
                return (uint)(((buffer[0] | (buffer[1] << 8)) | (buffer[2] << 0x10)) | (buffer[3] << 0x18));
			}
			return (uint)(((buffer[3] | (buffer[2] << 8)) | (buffer[1] << 0x10)) | (buffer[0] << 0x18));
		}
		public uint[] ReadUInt32Array(int count)
		{
			uint[] retval = new uint[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt32();
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

		public ulong ReadUInt48()
		{
			byte[] buffer = ReadBytes((uint)6);
			if (this.mvarEndianness == Endianness.LittleEndian)
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
		public ulong[] ReadUInt48Array(int count)
		{
			ulong[] retval = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt48();
			}
			return retval;
		}

		public ulong ReadUInt64()
        {
            byte[] buffer = ReadBytes((uint)8);
			if (this.mvarEndianness == Endianness.LittleEndian)
            {
                uint num = (uint)(((buffer[0] | (buffer[1] << 8)) | (buffer[2] << 0x10)) | (buffer[3] << 0x18));
                uint num2 = (uint)(((buffer[4] | (buffer[5] << 8)) | (buffer[6] << 0x10)) | (buffer[7] << 0x18));
                return (ulong)(num << 0x20 | num2);
			}
            else if (mvarEndianness == IO.Endianness.BigEndian)
            {
                uint num = (uint)(((buffer[7] | (buffer[6] << 8)) | (buffer[5] << 0x10)) | (buffer[4] << 0x18));
                uint num2 = (uint)(((buffer[3] | (buffer[2] << 8)) | (buffer[1] << 0x10)) | (buffer[0] << 0x18));
                return (ulong)(num << 0x20 | num2);
            }
            throw new InvalidOperationException();
		}
		public ulong[] ReadUInt64Array(int count)
		{
			ulong[] retval = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				retval[i] = ReadUInt64();
			}
			return retval;
		}

		public string ReadNullTerminatedString()
		{
			return this.ReadNullTerminatedString(mvarAccessor.DefaultEncoding);
		}

		public string ReadNullTerminatedString(int maxLength)
		{
			return this.ReadNullTerminatedString(maxLength, mvarAccessor.DefaultEncoding);
		}

		public string ReadNullTerminatedString(Encoding encoding)
		{
			List<byte> r = new List<byte>();
			while (true)
			{
				byte nextChar = ReadByte();
				if (nextChar == 0)
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
		/// Reads a length-prefixed string that is prefixed with a short (2-byte) length, rather than an int (4-byte) length.
		/// </summary>
		/// <returns></returns>
		public string ReadInt16String()
		{
			short length = ReadInt16();
			return this.ReadFixedLengthString((int)length);
		}

		public string ReadUInt16String()
		{
			ushort length = ReadUInt16();
			return this.ReadFixedLengthString((uint)length);
		}

		public byte[] ReadToEnd()
		{
            return ReadBytes(mvarAccessor.Remaining);
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
				if (mvarAccessor.Remaining >= sequence.Length)
				{
					byte[] seq = ReadBytes(sequence.Length);
					if (sequence.Match(seq))
					{
						if (!includeSequence) mvarAccessor.Seek((-1 * sequence.Length), SeekOrigin.Current);
						return w;
					}
					mvarAccessor.Seek((-1 * sequence.Length), SeekOrigin.Current);
				}
				else
				{
					return null;
				}
			}
			return w;
		}
		public string ReadUntil(string sequence)
		{
			return ReadUntil(sequence, mvarAccessor.DefaultEncoding);
		}
		public byte[] ReadUntil(string sequence, bool includeSequence)
		{
			return ReadUntil(sequence, mvarAccessor.DefaultEncoding, includeSequence);
		}
		public string ReadUntil(string sequence, Encoding encoding)
		{
			return encoding.GetString(ReadUntil(sequence.ToCharArray(), encoding));
		}
		public byte[] ReadUntil(string sequence, Encoding encoding, bool includeSequence)
		{
			return ReadUntil(sequence.ToCharArray(), encoding, includeSequence);
		}
		public byte[] ReadUntil(char[] sequence)
		{
			return this.ReadUntil(sequence, mvarAccessor.DefaultEncoding);
		}
		public byte[] ReadUntil(char[] sequence, bool includeSequence)
		{
			return this.ReadUntil(sequence, mvarAccessor.DefaultEncoding, includeSequence);
		}
		public byte[] ReadUntil(char[] sequence, Encoding encoding)
		{
			return this.ReadUntil(encoding.GetBytes(sequence));
		}
		public byte[] ReadUntil(char[] sequence, Encoding encoding, bool includeSequence)
		{
			return this.ReadUntil(encoding.GetBytes(sequence), includeSequence);
		}
		public string ReadStringUntil(string sequence)
		{
			return ReadStringUntil(sequence, mvarAccessor.DefaultEncoding, mvarAccessor.DefaultEncoding);
		}
		public string ReadStringUntil(string sequence, bool includeSequence)
		{
			return ReadStringUntil(sequence, mvarAccessor.DefaultEncoding, mvarAccessor.DefaultEncoding, includeSequence);
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
			return ReadStringUntil(sequence, mvarAccessor.DefaultEncoding, mvarAccessor.DefaultEncoding);
		}
		public string ReadStringUntil(char[] sequence, bool includeSequence)
		{
			return ReadStringUntil(sequence, mvarAccessor.DefaultEncoding, mvarAccessor.DefaultEncoding, includeSequence);
		}
		public string ReadStringUntil(char[] sequence, Encoding inputEncoding, Encoding outputEncoding)
		{
			return outputEncoding.GetString(ReadUntil(inputEncoding.GetBytes(sequence)));
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
			while (mvarAccessor.Remaining < stringTableSize)
			{
				list.Add(ReadNullTerminatedString());
			}
			return list.ToArray();
		}

		public DateTime ReadDateTime()
		{
			return ReadDateTime64();
		}
		public DateTime ReadDateTime64()
		{
			long l = ReadInt64 ();
			return DateTime.FromBinary(l);
		}
		public DateTime ReadDateTime32()
		{
			int l = ReadInt32();
			return DateTime.FromBinary(l);
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
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}
			short value2 = ReadInt16();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}
		public ushort ReadDoubleEndianUInt16()
		{
			ushort value1 = ReadUInt16();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}
			ushort value2 = ReadUInt16();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
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
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}
			int value2 = ReadInt32();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}

			if (value2 != value1)
			{
				throw new InvalidOperationException("Big-endian value does not match little-endian value");
			}
			return value1;
		}
		public uint ReadDoubleEndianUInt32()
		{
			uint value1 = ReadUInt32();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
			}
			uint value2 = ReadUInt32();
			if (mvarEndianness == Endianness.LittleEndian)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				mvarEndianness = Endianness.LittleEndian;
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
			return ReadInt32String(mvarAccessor.DefaultEncoding);
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
			SeekUntil(find, mvarAccessor.DefaultEncoding);
		}
		public void SeekUntil(string find, Encoding encoding)
		{
			byte[] data = encoding.GetBytes(find);
			byte[] dataCmp = new byte[data.Length];
            while (mvarAccessor.Position <= (mvarAccessor.Length - data.Length))
			{
                long rest = mvarAccessor.Remaining;

                mvarAccessor.ReadInternal(dataCmp, 0, dataCmp.Length);
                mvarAccessor.Seek(-(dataCmp.Length - 1), SeekOrigin.Current);

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
                    mvarAccessor.Seek(-1, SeekOrigin.Current);
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

				if (mvarEndianness == Endianness.LittleEndian)
				{
					buffer2[index] = (short)(b1 | (b2 << 8));
				}
				else if (mvarEndianness == Endianness.BigEndian)
				{
					buffer2[index] = (short)(b2 | (b1 << 8));
				}
			}
			return buffer2;
		}

		public void Align(int alignTo)
		{
            long paddingCount = ((alignTo - (mvarAccessor.Position % alignTo)) % alignTo);
            mvarAccessor.Position += paddingCount;
		}

		public string PeekFixedLengthString(int count)
		{
			return PeekFixedLengthString(count, mvarAccessor.DefaultEncoding);
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
			if (mvarEndianness == Endianness.LittleEndian)
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

		private int bitReader_LastByteRead = 0;
		private int bitReader_CurrentBit = 0;

		public int ReadAtMostBytes(byte[] buffer, int count)
		{
            if (mvarAccessor.Remaining == 0) return 0;

			if (count < mvarAccessor.Remaining)
			{
				Read(buffer, 0, count);
				return count;
			}
			else
			{
				Read(buffer, 0, (int)mvarAccessor.Remaining);
				return (int)mvarAccessor.Remaining;
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
	
			for (i = 0 ; i < count; i++)
			{
				if ( getbit_mask == 0 )
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

        public bool EndOfStream { get { return mvarAccessor.Remaining == 0; } }

        public string ReadInt64String()
        {
            long length = ReadInt64();
            string value = ReadFixedLengthString(length);
            return value;
        }

        public string ReadUntil(string[] until)
        {
            return ReadUntil(until, null, null);
        }
        public string ReadUntil(string until, string ignoreBegin, string ignoreEnd)
        {
            return ReadUntil(new string[] { until }, ignoreBegin, ignoreEnd);
        }
        public string ReadUntil(string[] until, string ignoreBegin, string ignoreEnd)
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
                        return w.Substring(0, w.Length - 1);
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
                    BaseStream.Seek(-1 * lengthToBackUp, System.IO.SeekOrigin.Current);
                    break;
                }
				sb.Append(w);
                */
            }
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
            return ReadUntil(GetNewLineSequence());
        }

		public void Seek(int length, SeekOrigin origin)
		{
			mvarAccessor.Seek(length, origin);
		}
		public void Seek(long length, SeekOrigin origin)
		{
			mvarAccessor.Seek(length, origin);
		}

		public void Close()
		{
			mvarAccessor.Close();
		}

		public long Remaining { get { return mvarAccessor.Remaining; } }
	}
}

