using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
    public enum PECompiler
    {
        /// <summary>
        /// Intel C++ 8.0 for Linux
        /// </summary>
        IntelCPP80Linux,
        /// <summary>
        /// HP aC++ A.05.55 IA-64
        /// </summary>
        HPaCppIA64,
        /// <summary>
        /// IAR EWARM C++ 5.4 ARM
        /// </summary>
        IAREwarmCPP54ARM,
        /// <summary>
        /// GCC 3.x and 4.x
        /// </summary>
        GCC3or4x,
        /// <summary>
        /// GCC 2.9x
        /// </summary>
        GCC29x,
        /// <summary>
        /// HP aC++ A.03.45 PA-RISC
        /// </summary>
        HPaCppPARISC,
        /// <summary>
        /// Microsoft Visual C++ v6-v10
        /// </summary>
        MsVCpp,
        /// <summary>
        /// Digital Mars C++
        /// </summary>
        DigitalMars,
        /// <summary>
        /// Borland C++ v3.1
        /// </summary>
        Borland31,
        /// <summary>
        /// OpenVMS C++ V6.5 (ARM mode)
        /// </summary>
        OpenVMS65ARM,
        /// <summary>
        /// OpenVMS C++ V6.5 (ANSI mode)
        /// </summary>
        OpenVMS65ANSI,
        /// <summary>
        /// OpenVMS C++ X7.1 IA-64
        /// </summary>
        OpenVMS71IA64,
        /// <summary>
        /// SunPro CC
        /// </summary>
        SunProCC,
        /// <summary>
        /// Tru64 C++ V6.5 (ARM mode)
        /// </summary>
        Tru64ARM,
        /// <summary>
        /// Tru64 C++ V6.5 (ANSI mode)
        /// </summary>
        Tru64ANSI,
        /// <summary>
        /// Watcom C++ 10.6
        /// </summary>
        Watcom
    }
}
