using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.TLCD.Internal
{
    internal struct TLCSequence
    {
        public string filetype;
        public uint offset;
        public uint length;
        public uint id;
        public uint unknown4;

        public override string ToString()
        {
            return id.ToString() + "." + filetype + ": { " + offset.ToString() + ", " + length.ToString() + " }";
        }
    }
}
