using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.NWCSceneLayout.NewWorldComputing.BIN
{
    public enum BINContainerType
    {
        Toplevel = 0x01,
        Window = 0x02,
        Overlay = 0x08,
        Motion = 0xFF
    }
}
