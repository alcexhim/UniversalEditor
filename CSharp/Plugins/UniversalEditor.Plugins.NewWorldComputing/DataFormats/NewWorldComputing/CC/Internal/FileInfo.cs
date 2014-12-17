using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing.CC
{
    partial class CCDataFormat
    {
        private struct FileInfo
        {
            public ushort hash;
            public string filename;
            public uint offset;
            public ushort size;
            public byte nul;
        }
    }
}
