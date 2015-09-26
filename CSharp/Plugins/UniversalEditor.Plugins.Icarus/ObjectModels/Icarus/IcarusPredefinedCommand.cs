using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public abstract class IcarusPredefinedCommand : IcarusCommand
    {
        public abstract string Name { get; }
    }
}
