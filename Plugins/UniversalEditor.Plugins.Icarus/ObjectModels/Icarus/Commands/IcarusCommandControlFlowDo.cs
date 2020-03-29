using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandControlFlowDo : IcarusPredefinedCommand
    {
		public IcarusCommandControlFlowDo()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

        public override string Name
        {
            get { return "do"; }
        }

        public override object Clone()
        {
			IcarusCommandControlFlowDo clone = new IcarusCommandControlFlowDo();
			clone.Target = (Target.Clone() as IcarusExpression);
			return clone;
        }

        public IcarusExpression Target { get { return Parameters["Target"].Value; } set { Parameters["Target"].Value = value; } }
    }
}
