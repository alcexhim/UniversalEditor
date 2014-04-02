using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.StructuredStorage
{
    public enum StructuredStorageDirectoryType
    {
        Invalid = 0,
        Storage = 1,
        Stream = 2,
        Lockbytes = 3,
        Property = 4,
        Root = 5
    }
}
