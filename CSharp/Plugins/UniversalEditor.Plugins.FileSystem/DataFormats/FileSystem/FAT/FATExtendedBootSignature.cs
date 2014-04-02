using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
    public class FATExtendedBootSignature
    {
        private bool mvarEnabled = true;
        public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

        private bool mvarHasPartitonLabelAndFSType = true;
        public bool HasPartitonLabelAndFSType { get { return mvarHasPartitonLabelAndFSType; } set { mvarHasPartitonLabelAndFSType = value; } }

        private int mvarVolumeSerialNumber = 0;
        public int VolumeSerialNumber { get { return mvarVolumeSerialNumber; } set { mvarVolumeSerialNumber = value; } }

        private string mvarPartitionVolumeLabel = String.Empty;
        public string PartitionVolumeLabel { get { return mvarPartitionVolumeLabel; } set { mvarPartitionVolumeLabel = value; } }

        private string mvarFileSystemType = String.Empty;
        public string FileSystemType { get { return mvarFileSystemType; } set { mvarFileSystemType = value; } }
    }
}
