using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandKill : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "kill"; }
        }

        private IcarusExpression mvarTarget = null;
        public IcarusExpression Target { get { return mvarTarget; } set { mvarTarget = value; } }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
