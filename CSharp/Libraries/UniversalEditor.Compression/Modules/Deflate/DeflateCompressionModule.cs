using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Permissions;

namespace UniversalEditor.Compression.Modules.Deflate
{
    /// <summary>
    /// UE wrapper around System.IO.Compression.DeflateStream
    /// </summary>
	public class DeflateCompressionModule : CompressionModule
	{
        public override string Name { get { return "DEFLATE"; } }

        public const int BUFFERSIZE = 4096;

        public override void Compress(Stream inputStream, Stream outputStream)
        {
            System.IO.Compression.DeflateStream dst = new System.IO.Compression.DeflateStream(inputStream, System.IO.Compression.CompressionMode.Compress);
            int read = 0;
            int start = 0;
            do
            {
                byte[] buffer = new byte[BUFFERSIZE];
                read = dst.Read(buffer, start, buffer.Length);
                outputStream.Write(buffer, 0, read);
            }
            while (read == BUFFERSIZE);
        }
        public override void Decompress(Stream inputStream, Stream outputStream, int inputLength, int outputLength)
        {
            System.IO.Compression.DeflateStream dst = new System.IO.Compression.DeflateStream(inputStream, System.IO.Compression.CompressionMode.Decompress);
            int read = 0;
            int start = 0;
            do
            {
                byte[] buffer = new byte[BUFFERSIZE];
                read = dst.Read(buffer, start, buffer.Length);
                outputStream.Write(buffer, 0, read);
            }
            while (read == BUFFERSIZE);
        }
	}
}
