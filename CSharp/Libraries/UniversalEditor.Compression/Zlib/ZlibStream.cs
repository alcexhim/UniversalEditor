using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Zlib
{
    public class ZlibStream
    {
        public static byte[] Compress(string input)
        {
            return Compress(input, Encoding.Default);
        }
        public static byte[] Compress(string input, Encoding encoding)
        {
            return Compress(encoding.GetBytes(input));
        }
        public static byte[] Compress(byte[] input)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Internal.ZOutputStream zout = new Internal.ZOutputStream(ms, 5);
            zout.Write(input, 0, input.Length);
            zout.Flush();
            zout.Close();

            return ms.ToArray();
        }
        public static byte[] Decompress(byte[] input)
        {
            int data = 0;
            int stopByte = -1;
            System.IO.MemoryStream outFileStream = new System.IO.MemoryStream();
            Internal.ZInputStream inZStream = new Internal.ZInputStream(new System.IO.MemoryStream(input));
            while (stopByte != (data = inZStream.Read()))
            {
                byte _dataByte = (byte)data;
                outFileStream.WriteByte(_dataByte);
            }

            inZStream.Close();
            outFileStream.Close();

            return outFileStream.ToArray();
        }
    }
}
