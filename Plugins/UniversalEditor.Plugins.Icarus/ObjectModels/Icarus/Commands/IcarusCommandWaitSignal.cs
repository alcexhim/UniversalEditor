using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandWaitSignal : IcarusPredefinedCommand
    {
        public override string Name { get { return "waitsignal"; } }
        public IcarusExpression Target { get { return Parameters["Target"].Value; } set { Parameters["Target"].Value = value; } }

		public IcarusCommandWaitSignal()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

        public override object Clone()
        {
            IcarusCommandWaitSignal clone = new IcarusCommandWaitSignal();
            clone.Target = (Target.Clone() as IcarusExpression);
            return clone;
        }
    }
}
