using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
    public enum UVSColorspace
    {
        None = 0x00000000,
        /// <summary>
        /// Unspecified R'G'B'
        /// </summary>
        UnspecifiedRGB = 0x00000001,
        /// <summary>
        /// Unspecified Y'CbCr
        /// </summary>
        UnspecifiedYCbCr = 0x00000002
    }
}
