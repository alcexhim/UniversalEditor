using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    [Flags()]
    public enum MapLoseCondition
    {
        None = 0x0000,
        All = 0x0100,
        Town = 0x0200,
        Hero = 0x0400,
        Time = 0x0800,
        StartingHero = 0x1000,
        Any = All | Town | Hero | Time | StartingHero
    }
}
