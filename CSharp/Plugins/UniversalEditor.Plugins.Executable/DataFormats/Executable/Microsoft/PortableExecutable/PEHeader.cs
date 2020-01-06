using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Microsoft.PortableExecutable
{
    public struct PEHeader
    {
        public bool enabled;
        public string signature;
        public PEMachineType machine;
        public short sectionCount;
        public short timestamp;
        public short symbolTableOffset;
        public short symbolCount;
        public short unknown4;
        public short unknown5;
        public short unknown6;
        public short sizeOfOptionalHeader; // relative offset to sectiontable
        public PECharacteristics characteristics;
    }
}
