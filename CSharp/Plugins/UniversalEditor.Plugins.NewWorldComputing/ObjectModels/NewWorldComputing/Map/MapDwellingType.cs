using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    [Flags()]
    public enum MapDwellingType
    {
        None = 0,
        Dwelling1 = 8,
        Dwelling2 = 16,
        Dwelling3 = 32,
        Dwelling4 = 64,
        Dwelling5 = 128,
        Dwelling6 = 256,
        UpgradedDwelling2 = 512,
        UpgradedDwelling3 = 1024,
        UpgradedDwelling4 = 2048,
        UpgradedDwelling5 = 4096,
        UpgradedDwelling6 = 8192
    }
}
