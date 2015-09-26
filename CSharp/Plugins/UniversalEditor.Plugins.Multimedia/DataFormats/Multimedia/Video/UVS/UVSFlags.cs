using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
    [Flags()]
    public enum UVSFlags : uint
    {
        None = 0x00000000,
        Interlaced = 0x00000001
    }
}
