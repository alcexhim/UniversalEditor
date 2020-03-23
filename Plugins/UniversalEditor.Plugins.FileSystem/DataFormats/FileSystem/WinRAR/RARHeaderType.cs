using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
    public enum RARHeaderType : byte
    {
        Marker = 0x72,
        Archive = 0x73,
        File = 0x74,
        OldComment = 0x75,
        OldAuthenticity = 0x76,
        OldSubblock = 0x77,
        OldRecoveryRecord = 0x78,
        OldAuthenticity2 = 0x79,
        Subblock = 0x7A
    }
}
