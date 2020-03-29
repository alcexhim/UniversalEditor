using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandTask : IcarusPredefinedContainerCommand
    {
		public IcarusCommandTask()
		{
			Parameters.Add(new IcarusGenericParameter("TaskName", new IcarusConstantExpression("DEFAULT")));
		}

        public override string Name
        {
            get { return "task"; }
        }

        public IcarusExpression TaskName { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }

        public override object Clone()
        {
			IcarusCommandTask clone = new IcarusCommandTask();
			clone.TaskName = (TaskName.Clone() as IcarusExpression);
			return clone;
		}
    }
}
