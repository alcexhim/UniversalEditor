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

namespace UniversalEditor.IO
{
    public abstract class Encoding
    {
        private static Encoding _Default = new DefaultEncoding();
        public static Encoding Default
        {
            get { return _Default; }
        }

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
            string retval = System.String.Empty;
            for (int i = 0; i < chars.Length; i++)
            {
                retval += chars[i].ToString();
            }
            return retval;
        }

        public char[] GetChars(byte[] bytes)
        {
            return GetChars(bytes, 0, bytes.Length);
        }
        public abstract char[] GetChars(byte[] bytes, int index, int count);
    }
    public class DefaultEncoding : Encoding
    {
        public override byte[] GetBytes(char[] chars, int index, int count)
        {
            byte[] bytes = new byte[count];
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)(chars[i]);
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
}