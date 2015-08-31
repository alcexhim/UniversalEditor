using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression
{
	public static class CompressionModules
	{

		private static Modules.Bzip2.Bzip2CompressionModule mvarBzip2 = new Modules.Bzip2.Bzip2CompressionModule();
		public static Modules.Bzip2.Bzip2CompressionModule Bzip2 { get { return mvarBzip2; } }

		private static Modules.Deflate.DeflateCompressionModule mvarDeflate = new Modules.Deflate.DeflateCompressionModule();
		public static Modules.Deflate.DeflateCompressionModule Deflate { get { return mvarDeflate; } }

		private static Modules.Gzip.GzipCompressionModule mvarGzip = new Modules.Gzip.GzipCompressionModule();
		public static Modules.Gzip.GzipCompressionModule Gzip { get { return mvarGzip; } }

		private static Modules.Zlib.ZlibCompressionModule mvarZlib = new Modules.Zlib.ZlibCompressionModule();
		public static Modules.Zlib.ZlibCompressionModule Zlib { get { return mvarZlib; } }
	}
}
