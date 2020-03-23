using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandRotate : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "rotate"; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
