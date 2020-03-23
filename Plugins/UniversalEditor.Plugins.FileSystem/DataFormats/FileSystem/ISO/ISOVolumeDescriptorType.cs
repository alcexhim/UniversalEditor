using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ISO
{
    public enum ISOVolumeDescriptorType
    {
        BootRecord = 0,
        Primary = 1,
        Supplementary = 2,
        VolumePartition = 3,
        Terminator = 255
    }
}
