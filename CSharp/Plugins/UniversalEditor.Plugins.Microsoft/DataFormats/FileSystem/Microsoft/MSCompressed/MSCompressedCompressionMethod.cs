using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.MSCompressed
{
    public enum MSCompressedCompressionMethod
    {
        None,
        XOR,
        SZDD,
        SZ,
        JeffJohnson,
        MSZIP
    }
}
