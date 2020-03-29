using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandSet : IcarusPredefinedCommand
    {
		public IcarusCommandSet()
		{
			Parameters.Add(new IcarusGenericParameter("ObjectName"));
			Parameters.Add(new IcarusGenericParameter("Value"));
		}

        public override string Name
        {
            get { return "set"; }
        }

        public override object Clone()
        {
			IcarusCommandSet clone = new IcarusCommandSet();
			clone.ObjectName = (ObjectName.Clone() as IcarusExpression);
			clone.Value = (Value.Clone() as IcarusExpression);
			return clone;
        }

        public IcarusExpression ObjectName { get { return Parameters["ObjectName"].Value; } set { Parameters["ObjectName"].Value = value; } }
        public IcarusExpression Value { get { return Parameters["Value"].Value; } set { Parameters["Value"].Value = value; } }
    }
}
