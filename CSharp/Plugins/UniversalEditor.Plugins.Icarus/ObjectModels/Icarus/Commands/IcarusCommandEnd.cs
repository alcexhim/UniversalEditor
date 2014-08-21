using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandEnd : IcarusPredefinedCommand
    {
        public override string Name { get { return "end"; } }

        public override object Clone()
        {
            IcarusCommandEnd clone = new IcarusCommandEnd();
            return clone;
        }
    }
}
