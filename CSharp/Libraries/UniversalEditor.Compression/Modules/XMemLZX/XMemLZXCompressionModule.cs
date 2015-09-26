using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.XMemLZX
{
    public class XMemLZXCompressionModule : CompressionModule
    {
        public override string Name
        {
            get { return "XMEMLZX"; }
        }

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
        }
		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {

        }

    }
}
