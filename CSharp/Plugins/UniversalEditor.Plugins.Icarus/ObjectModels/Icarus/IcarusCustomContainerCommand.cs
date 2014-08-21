using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public class IcarusCustomContainerCommand : IcarusCustomCommand, IIcarusContainerCommand
    {
        private IcarusCommand.IcarusCommandCollection mvarCommands = new IcarusCommandCollection();
        public IcarusCommand.IcarusCommandCollection Commands { get { return mvarCommands; } }
    }
}
