using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.NewWorldComputing.AGG.Internal
{
    public struct AGGFileEntry
    {
        public uint hash;
        public uint offset;
        public uint size;
        public string name;
    }
}
