using System;

namespace UniversalEditor.Plugins.FileSystem.VMware
{
    public enum VMDKCreateType
    {
        monolithicSparse,
        vmfsSparse,
        monolithicFlat,
        vmfs,
        twoGbMaxExtentSparse,
        twoGbMaxExtentFlat,
        fullDevice,
        vmfsRaw,
        partitionedDevice,
        vmfsRawDeviceMap,
        vmfsPassthroughRawDeviceMap,
        streamOptimized
    }
}

