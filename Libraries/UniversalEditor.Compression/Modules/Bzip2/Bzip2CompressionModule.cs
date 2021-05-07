using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Modules.Bzip2
{
	public class Bzip2CompressionModule : CompressionModule
	{
		private string mvarName = "bzip2";
		public override string Name { get { return mvarName; } }

		public byte[] Compress(byte[] inputData, int level)
		{
			MemoryStream msIn = new MemoryStream(inputData);
			MemoryStream msOut = new MemoryStream();
			Compress(msIn, msOut, level);
			return msOut.ToArray();
		}

		public void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream, int level)
		{
			ICSharpCode.SharpZipLib.BZip2.BZip2.Compress(inputStream, outputStream, false, level);
		}

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			Compress(inputStream, outputStream, 5);
		}
		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress(inputStream, outputStream, false);
		}
	}
}
