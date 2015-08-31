using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandSet : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "set"; }
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        private string mvarObjectName = String.Empty;
        public string ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        private object mvarValue = null;
        public object Value { get { return mvarValue; } set { mvarValue = value; } }
    }
}
