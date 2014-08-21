using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public abstract class IcarusCommandContainerCommand : IcarusCommand
    {
        private IcarusCommand.IcarusCommandCollection mvarCommands = new IcarusCommand.IcarusCommandCollection();
        public IcarusCommand.IcarusCommandCollection Commands { get { return mvarCommands; } }
    }
}
