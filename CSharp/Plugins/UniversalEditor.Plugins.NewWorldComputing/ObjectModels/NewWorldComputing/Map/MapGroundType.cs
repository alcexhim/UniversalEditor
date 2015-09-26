using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    [Flags()]
    public enum MapGroundType
    {
        Unknown = 0x0000,
        Desert = 0x0001,
        Snow = 0x0002,
        Swamp = 0x0004,
        Wasteland = 0x0008,
        Beach = 0x0010,
        Lava = 0x0020,
        Dirt = 0x0040,
        Grass = 0x0080,
        Water = 0x0100
    }
}
