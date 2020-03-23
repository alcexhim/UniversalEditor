using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandSignal : IcarusPredefinedCommand
    {
        public override string Name { get { return "signal"; } }

        private string mvarTarget = String.Empty;
        public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            IcarusCommandSignal clone = new IcarusCommandSignal();
            clone.Target = (mvarTarget.Clone() as string);
			return clone;
        }
    }
}
