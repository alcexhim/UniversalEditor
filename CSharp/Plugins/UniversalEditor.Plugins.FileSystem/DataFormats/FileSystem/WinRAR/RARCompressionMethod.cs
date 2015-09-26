using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
    public enum RARCompressionMethod
    {
        Store = 0x30,
        Fastest = 0x31,
        Fast = 0x32,
        Normal = 0x33,
        Good = 0x34,
        Best = 0x35
    }
}
