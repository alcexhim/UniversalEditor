using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandLoop : IcarusPredefinedContainerCommand
    {
        public override string Name
        {
            get { return "loop"; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        private float mvarCount = 0;
        public float Count { get { return mvarCount; } set { mvarCount = value; } }
    }
}
