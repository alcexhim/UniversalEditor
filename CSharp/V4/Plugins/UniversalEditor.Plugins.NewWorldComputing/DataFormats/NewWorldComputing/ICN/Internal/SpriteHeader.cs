using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.NewWorldComputing.ICN.Internal
{
    public struct SpriteHeader
    {
        public short offsetX;
        public short offsetY;
        public ushort width;
        public ushort height;
        public byte type;
        public uint dataOffset;
    }
}
