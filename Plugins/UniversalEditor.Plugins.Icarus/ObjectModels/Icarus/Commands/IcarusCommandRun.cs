using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandRun : IcarusPredefinedCommand
    {
        public override string Name { get { return "run"; } }

        private string mvarTarget = String.Empty;
        public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            IcarusCommandRun clone = new IcarusCommandRun();
            clone.Target = (mvarTarget.Clone() as string);
            return clone;
        }
    }
}
