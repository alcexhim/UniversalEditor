using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public enum MapWinCondition
    {
        None = 0x0000,
        All = 0x0001,
        Town = 0x0002,
        Hero = 0x0004,
        Artifact = 0x0008,
        Side = 0x0010,
        Gold = 0x0020,
        Any = All | Town | Hero | Artifact | Side | Gold
    }
}
