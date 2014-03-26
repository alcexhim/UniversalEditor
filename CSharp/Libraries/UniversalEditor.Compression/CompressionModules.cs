using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression
{
	public static class CompressionModules
	{
		private static Modules.Gzip.GzipCompressionModule mvarGzip = new Modules.Gzip.GzipCompressionModule();
		public static Modules.Gzip.GzipCompressionModule Gzip { get { return mvarGzip; } }
	}
}
