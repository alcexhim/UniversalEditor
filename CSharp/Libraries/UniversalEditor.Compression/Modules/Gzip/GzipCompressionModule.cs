using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.Gzip
{
	public class GzipCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "gzip"; }
		}

        public const int BUFFERSIZE = 4096;

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            System.IO.Compression.GZipStream dst = new System.IO.Compression.GZipStream(outputStream, System.IO.Compression.CompressionMode.Compress);
            int read = 0;
            int start = 0;
            do
            {
                byte[] buffer = new byte[BUFFERSIZE];
                read = inputStream.Read(buffer, start, buffer.Length);
                dst.Write(buffer, 0, read);
            }
            while (read == BUFFERSIZE);
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {
            System.IO.Compression.GZipStream dst = new System.IO.Compression.GZipStream(inputStream, System.IO.Compression.CompressionMode.Decompress);
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
