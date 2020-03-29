using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandWait : IcarusPredefinedCommand
    {
		public IcarusCommandWait()
		{
			Parameters.Add(new IcarusFloatParameter("Duration", new IcarusConstantExpression(0.0f)));
		}
        public override string Name { get { return "wait"; } }

        public IcarusExpression Duration { get { return Parameters["Duration"].Value; } set { Parameters["Duration"].Value = value; } }
        
        public override object Clone()
        {
            IcarusCommandWait clone = new IcarusCommandWait();
            clone.Duration = (Duration.Clone() as IcarusExpression);
            return clone;
        }
    }
}
