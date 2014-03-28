using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal
{
    internal class RawMotionFrameComparer : Comparer<RawMotionFrame>
    {
        public override int Compare(RawMotionFrame x, RawMotionFrame y)
        {
            return x.Index.CompareTo(y.Index);
        }
    }
}
