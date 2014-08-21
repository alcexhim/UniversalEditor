using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public interface IIcarusContainerCommand
    {
        IcarusCommand.IcarusCommandCollection Commands { get; }
    }
}
