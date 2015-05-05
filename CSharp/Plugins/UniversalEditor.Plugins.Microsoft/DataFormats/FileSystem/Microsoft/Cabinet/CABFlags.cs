using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet
{
    [Flags()]
    public enum CABFlags
    {
        HasPreviousCabinet = 0x0001,
        HasNextCabinet = 0x0002,
        HasReservedArea = 0x0004
    }
}
