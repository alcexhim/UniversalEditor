using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.MotionVectorData.Internal
{
    public class MVDBoneData
    {
        private uint mvarFrameIndex = 0;
        public uint FrameIndex { get { return mvarFrameIndex; } set { mvarFrameIndex = value; } }

        private string mvarBoneName = String.Empty;
        public string BoneName { get { return mvarBoneName; } set { mvarBoneName = value; } }

        public override string ToString()
        {
            return mvarFrameIndex.ToString() + ": " + mvarBoneName;
        }
    }
}
