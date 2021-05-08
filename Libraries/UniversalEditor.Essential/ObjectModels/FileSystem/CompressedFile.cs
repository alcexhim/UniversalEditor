using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
#if Compression_Supported
	public class CompressedFile : File
	{
		private Compression.CompressionMethod mvarCompressionMethod = Compression.CompressionMethod.None;
		public Compression.CompressionMethod CompressionMethod { get { return mvarCompressionMethod; } set { mvarCompressionMethod = value; } }
	}
#endif
}
