using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandTask : IcarusPredefinedContainerCommand
    {
        public override string Name
        {
            get { return "task"; }
        }

        private string mvarTaskName = String.Empty;
        public string TaskName { get { return mvarTaskName; } set { mvarTaskName = value; } }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
