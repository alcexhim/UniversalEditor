using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandUse : IcarusPredefinedCommand
    {
		public IcarusCommandUse()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

        public override string Name { get { return "use"; } }

        public IcarusExpression Target { get { return Parameters["Target"].Value; } set { Parameters["Target"].Value = value; } }

        public override object Clone()
        {
            IcarusCommandUse command = new IcarusCommandUse();
            command.Target = (Target.Clone() as IcarusExpression);
            return command;
        }
    }
}
