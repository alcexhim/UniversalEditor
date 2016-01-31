using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
    public struct PEOptionalHeader
    {
        public bool enabled;
        public ushort magic;
        public ushort unknown1;
        public uint unknown2;
        public uint unknown3;
        public uint unknown4;
        public uint entryPointAddr;
        public uint unknown5;
        public uint unknown6;
        public uint imageBase;
        public uint sectionAlignment;
        public uint fileAlignment;
        public uint unknown7;
        public uint unknown8;
        public ushort majorSubsystemVersion; // 4 = NT 4 or later
        public ushort unknown9;
        public uint unknown10;
        public uint imageSize;
        public uint headerSize;
        public uint unknown11;
        public ushort subsystem;
        public ushort unknown12;
        public uint unknown13;
        public uint unknown14;
        public uint unknown15;
        public uint unknown16;
        public uint unknown17;
		public uint rvaCount;

		public uint[] rvas1;
		public uint[] rvas2;
    }
}
