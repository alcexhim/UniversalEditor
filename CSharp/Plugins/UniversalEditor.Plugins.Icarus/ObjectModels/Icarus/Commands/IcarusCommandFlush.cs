using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandFlush : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "flush"; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
