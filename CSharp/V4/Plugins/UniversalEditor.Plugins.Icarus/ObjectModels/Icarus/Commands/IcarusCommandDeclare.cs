using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandDeclare : IcarusPredefinedCommand
    {
        public override string Name { get { return "declare"; } }

        private string mvarVariableName = String.Empty;
        public string VariableName { get { return mvarVariableName; } set { mvarVariableName = value; } }

        private IcarusVariableDataType mvarDataType = IcarusVariableDataType.Float;
        public IcarusVariableDataType DataType { get { return mvarDataType; } set { mvarDataType = value; } }

        public override object Clone()
        {
            IcarusCommandDeclare clone = new IcarusCommandDeclare();
            clone.VariableName = (mvarVariableName.Clone() as string);
            clone.DataType = mvarDataType;
            return clone;
        }
    }
}
