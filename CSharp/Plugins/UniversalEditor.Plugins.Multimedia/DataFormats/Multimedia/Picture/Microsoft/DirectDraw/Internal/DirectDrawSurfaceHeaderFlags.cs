using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
    public enum DirectDrawSurfaceHeaderFlags : uint
    {
        Caps = 0x00000001,
        Height = 0x00000002,
        Width = 0x00000004,
        Pitch = 0x00000008,
        PixelFormat = 0x00001000,
        MipMapCount = 0x00020000,
        LinearSize = 0x00080000,
        Depth = 0x00800000
    }
}
