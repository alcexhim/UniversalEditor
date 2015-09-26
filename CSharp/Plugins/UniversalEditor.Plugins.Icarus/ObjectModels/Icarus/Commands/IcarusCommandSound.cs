using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandSound : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "sound"; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
