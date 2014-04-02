using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG
{
    [Flags()]
    public enum ALZipFileNameFlags
    {
        None = 0x0,
        Encrypted = 0x4,
        UseAreaCode = 0x8,
        RelativePath = 0x10
    }
}
