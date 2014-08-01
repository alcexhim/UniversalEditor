using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.ELF
{
    [Flags()]
    public enum ELFSectionFlags : uint
    {
        /// <summary>
        /// The section contains data that should be writable during process execution.
        /// </summary>
        Writable = 0x1,
        /// <summary>
        /// The section occupies memory during process execution. Some control sections do
        /// not reside in the memory image of an object file; this attribute is off for
        /// those sections.
        /// </summary>
        Allocated = 0x2,
        /// <summary>
        /// The section contains executable machine instructions.
        /// </summary>
        Executable = 0x4,
        /// <summary>
        /// All bits included in this mask are reserved for processor-specific semantics.
        /// </summary>
        ProcessorSpecificMask = 0xF0000000
    }
}
