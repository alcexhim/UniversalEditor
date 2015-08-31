using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.ELF
{
    /// <summary>
    /// Identifies the object file type.
    /// </summary>
    public enum ELFObjectFileType : ushort
    {
        /// <summary>
        /// No file type
        /// </summary>
        None = 0,
        /// <summary>
        /// Relocatable file
        /// </summary>
        Relocatable = 1,
        /// <summary>
        /// Executable file
        /// </summary>
        Executable = 2,
        /// <summary>
        /// Shared object file
        /// </summary>
        SharedObject = 3,
        /// <summary>
        /// Core file
        /// </summary>
        Core = 4,
        /// <summary>
        /// Processor-specific
        /// </summary>
        ProcessorSpecificFF00 = 0xFF00,
        /// <summary>
        /// Processor-specific
        /// </summary>
        ProcessorSpecificFFFF = 0xFFFF
    }
}
