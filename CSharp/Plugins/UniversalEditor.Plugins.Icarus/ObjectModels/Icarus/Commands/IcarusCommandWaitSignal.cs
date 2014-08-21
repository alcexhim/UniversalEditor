using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandWaitSignal : IcarusPredefinedCommand
    {
        public override string Name { get { return "waitsignal"; } }

        private string mvarTarget = String.Empty;
        public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            IcarusCommandWaitSignal clone = new IcarusCommandWaitSignal();
            clone.Target = (mvarTarget.Clone() as string);
            return clone;
        }
    }
}
