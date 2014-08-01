using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.ELF
{
    public enum ELFSpecialSectionIndex
    {
        Undefined = 0,
        ReservedLo = 0xFF00,
        ProcLo = 0xFF00,
        ProcHigh = 0xFF1F,
        Abs = 0xFFF1,
        Common = 0xFFF2,
        ReservedHigh = 0xFFFF
    }
}
