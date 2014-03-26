using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Permissions;

namespace UniversalEditor.Compression.Deflate
{
    /// <summary>
    /// UE wrapper around System.IO.Compression.DeflateStream
    /// </summary>
	public class DeflateStream
	{
        public const int BUFFERSIZE = 4096;

		public static byte[] Compress(byte[] source)
		{
			MemoryStream ms = new MemoryStream();
            System.IO.Compression.DeflateStream dst = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Compress); // new DeflateStream(ms, CompressionMode.Compress);
			dst.Write(source, 0, source.Length);
			dst.Flush();
			dst.Close();
			return ms.ToArray();
		}
        public static byte[] Decompress(byte[] source)
        {
            return Decompress(source, 0);
        }
		public static byte[] Decompress(byte[] source, int start)
        {
			MemoryStream ms = new MemoryStream(source);
			MemoryStream msOut = new MemoryStream();
            System.IO.Compression.DeflateStream dst = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress);
			int read;
			do
			{
				byte[] buffer = new byte[BUFFERSIZE];
				read = dst.Read(buffer, start, buffer.Length);
				msOut.Write(buffer, 0, read);
			}
			while (read == BUFFERSIZE);
			dst.Close();
			ms.Close();
			msOut.Flush();
			msOut.Close();
			return msOut.ToArray();
		}
		public static byte[] Decompress(byte[] source, int start, int uncompressedLength)
		{
			byte[] dest = new byte[uncompressedLength];
			MemoryStream ms = new MemoryStream(source);
			System.IO.Compression.DeflateStream dst = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress);
			int read = dst.Read(dest, start, uncompressedLength);
			dst.Flush();
            dst.Close();
			return dest;
		}
	}
}
