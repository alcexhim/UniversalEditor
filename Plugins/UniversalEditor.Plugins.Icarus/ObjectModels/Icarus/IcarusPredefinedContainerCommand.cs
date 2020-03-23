using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public abstract class IcarusPredefinedContainerCommand : IcarusPredefinedCommand, IIcarusContainerCommand
    {
        private IcarusCommand.IcarusCommandCollection mvarCommands = new IcarusCommandCollection();
        public IcarusCommand.IcarusCommandCollection Commands { get { return mvarCommands; } }
    }
}
