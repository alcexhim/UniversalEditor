using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
    public enum CFLPreprocessorFlags
    {
        /// <summary>
        /// No preprocess
        /// </summary>
        None    = 0x00000000,
        /// <summary>
        /// 8-bit delta encoding
        /// </summary>
        DeltaEncoding8Bit = 0x00010000,
        /// <summary>
        /// 16-bit delta encoding
        /// </summary>
        DeltaEncoding16Bit = 0x00020000,
        /// <summary>
        /// 32-bit delta encoding
        /// </summary>
        DeltaEncoding32Bit = 0x00030000,
        /// <summary>
        /// Burrows-Wheeler transform
        /// </summary>
        BurrowsWheelerTransform     = 0x00040000,
        /// <summary>
        /// 8-bit 'turn' encoding
        /// </summary>
        TurnEncoding8Bit   = 0x00050000,
        /// <summary>
        /// 16-bit 'turn' encoding
        /// </summary>
        TurnEncoding16Bit = 0x00060000,
        /// <summary>
        /// 24-bit 'turn' encoding
        /// </summary>
        TurnEncoding24Bit  = 0x00070000,
        /// <summary>
        /// 32-bit 'turn' encoding
        /// </summary>
        TurnEncoding32Bit = 0x00080000,
    }
}
