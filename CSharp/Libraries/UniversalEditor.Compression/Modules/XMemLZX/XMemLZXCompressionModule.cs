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

        public override void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
        }
        public override void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {

        }

    }
}
