using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandWait : IcarusPredefinedCommand
    {
        public override string Name { get { return "wait"; } }

        private float mvarDuration = 0.0f;
        public float Duration { get { return mvarDuration; } set { mvarDuration = value; } }

        private string mvarTarget = null;
        public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            IcarusCommandWait clone = new IcarusCommandWait();
            clone.Duration = mvarDuration;
            return clone;
        }
    }
}
