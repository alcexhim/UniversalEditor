using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
    internal struct CFHEADER
    {
        public string signature;
        public uint reserved1;
        public uint cabinetFileSize;
        public uint reserved2;
        public uint firstFileOffset;
        public uint reserved3;
        public byte versionMinor;
        public byte versionMajor;

        public ushort folderCount;
        public ushort fileCount;
        public CABFlags flags;

        public ushort setID;
        public ushort iCabinet;

        public ushort cabinetReservedAreaSize;
        public byte folderReservedAreaSize;
        public byte datablockReservedAreaSize;

        public byte[] reservedArea;

        public string previousCabinetName;
        public string previousDiskName;

        public string nextCabinetName;
        public string nextDiskName;
    }
}
