using System;

namespace UniversalEditor.Plugins.FileSystem.VMware
{
    public class VMDKDiskGeometry
    {
        private int mvarCylinders = 0;
        private int mvarHeads = 0;
        private int mvarSectors = 0;

        public int Cylinders
        {
            get
            {
                return this.mvarCylinders;
            }
            set
            {
                this.mvarCylinders = value;
            }
        }

        public int Heads
        {
            get
            {
                return this.mvarHeads;
            }
            set
            {
                this.mvarHeads = value;
            }
        }

        public int Sectors
        {
            get
            {
                return this.mvarSectors;
            }
            set
            {
                this.mvarSectors = value;
            }
        }
    }
}

