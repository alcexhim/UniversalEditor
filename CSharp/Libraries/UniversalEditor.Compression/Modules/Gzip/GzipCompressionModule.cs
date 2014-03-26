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

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			throw new NotImplementedException();
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			throw new NotImplementedException();
		}
	}
}
