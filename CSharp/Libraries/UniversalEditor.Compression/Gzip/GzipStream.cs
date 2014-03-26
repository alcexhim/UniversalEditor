using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

namespace UniversalEditor.Compression.Gzip
{
    /// <summary>
    /// UE wrapper around System.IO.Compression.GZipStream
    /// </summary>
	public class GzipStream
	{
		public static byte[] Compress(byte[] source)
		{
			MemoryStream ms = new MemoryStream();
            System.IO.Compression.GZipStream gst = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);
			gst.Write(source, 0, source.Length);
			gst.Flush();
			gst.Close();
			return ms.ToArray();
		}
        public static byte[] Decompress(byte[] source)
        {
            return Decompress(source, 0);
        }
		public static byte[] Decompress(byte[] source, int start)
		{
			MemoryStream msDest = new MemoryStream();
			MemoryStream ms = new MemoryStream(source);
            System.IO.Compression.GZipStream gst = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
			int read;
			do
			{
				byte[] buffer = new byte[4096];
				read = gst.Read(buffer, start, buffer.Length);
				msDest.Write(buffer, 0, read);
			}
			while (read != 0);
			gst.Close();
			return msDest.ToArray();
		}
		public static byte[] Decompress(byte[] source, int start, int uncompressedLength)
		{
			byte[] dest = new byte[uncompressedLength];
			MemoryStream ms = new MemoryStream(source);
            System.IO.Compression.GZipStream gst = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
			int read = gst.Read(dest, start, uncompressedLength);
			gst.Flush();
			gst.Close();
			return dest;
		}
	}
}
