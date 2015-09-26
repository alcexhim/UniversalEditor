using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    [Flags()]
    public enum MapKingdomColor
    {
        None = 0x00,
        Blue = 0x01,
        Green = 0x02,
        Red = 0x04,
        Yellow = 0x08,
        Orange = 0x10,
        Purple = 0x20,
        Unused = 0x80,
        All = Blue | Green | Red | Yellow | Orange | Purple
    }
}
