using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
    public class MotionFrame : ICloneable
    {
        public class MotionFrameCollection
            : System.Collections.ObjectModel.Collection<MotionFrame>
        {
        }

        private uint mvarIndex = 0;
        public uint Index { get { return mvarIndex; } set { mvarIndex = value; } }

        private MotionAction.MotionActionCollection mvarActions = new MotionAction.MotionActionCollection();
        public MotionAction.MotionActionCollection Actions { get { return mvarActions; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Frame ");
            sb.Append(mvarIndex);
            sb.Append(" (");
            sb.Append(mvarActions.Count);
            sb.Append(" actions)");
            return sb.ToString();
        }

        public object Clone()
        {
            MotionFrame clone = new MotionFrame();
            clone.Index = mvarIndex;
            foreach (MotionAction action in mvarActions)
            {
                clone.Actions.Add(action.Clone() as MotionAction);
            }
            return clone;
        }
    }
}
