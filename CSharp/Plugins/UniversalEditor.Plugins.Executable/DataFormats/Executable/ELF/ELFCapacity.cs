using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.ELF
{
    public enum ELFCapacity : byte
    {
        elfInvalid = 0,
        elf32Bit = 1,
        elf64Bit = 2
    }
}
