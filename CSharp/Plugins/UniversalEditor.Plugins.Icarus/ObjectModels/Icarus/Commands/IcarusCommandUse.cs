using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandUse : IcarusPredefinedCommand
    {
        public override string Name { get { return "use"; } }

        private IcarusExpression mvarTarget = null;
        public IcarusExpression Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            IcarusCommandUse command = new IcarusCommandUse();
            command.Target = (mvarTarget.Clone() as IcarusExpression);
            return command;
        }
    }
}
