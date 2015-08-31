using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
    public enum TapeArchiveRecordType : int
    {
        Normal = 0,
        HardLink = 1,
        SymbolicLink = 2,
        CharacterSpecial = 3,
        BlockSpecial = 4,
        Directory = 5,
        NamedPipe = 6,
        ContiguousFile = 7,
        GlobalExtendedHeader = (int)'g',
        NextFileExtendedHeader = (int)'x',
        VendorSpecificExtensionA = (int)'A',
        VendorSpecificExtensionZ = (int)'Z'
    }
}
