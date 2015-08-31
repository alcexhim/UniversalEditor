using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.ELF
{
    /// <summary>
    /// Specifies the required architecture for the executable file.
    /// </summary>
    public enum ELFMachine : ushort
    {
        None = 0,
        /// <summary>
        /// AT&T WE 32100
        /// </summary>
        M32 = 1,
        /// <summary>
        /// SPARC
        /// </summary>
        SPARC = 2,
        /// <summary>
        /// Intel 80386
        /// </summary>
        Intel80386 = 3,
        /// <summary>
        /// Motorola 68K
        /// </summary>
        Motorola68000 = 4,
        /// <summary>
        /// Motorola 88K
        /// </summary>
        Motorola88000 = 5,
        /// <summary>
        /// Intel 80860
        /// </summary>
        Intel80860 = 7,
        /// <summary>
        /// MIPS RS3000
        /// </summary>
        MIPS = 8
    }
}
