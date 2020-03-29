using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandLoop : IcarusPredefinedContainerCommand
    {
		public IcarusCommandLoop()
		{
			Parameters.Add(new IcarusGenericParameter("Count", new IcarusConstantExpression((float)0)));
		}

        public override string Name
        {
            get { return "loop"; }
        }

        public override object Clone()
        {
			IcarusCommandLoop clone = new IcarusCommandLoop();
			clone.Count = (Count.Clone() as IcarusExpression);
			return clone;
        }

        public IcarusExpression Count { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }
    }
}
