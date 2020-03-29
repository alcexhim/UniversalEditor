using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandCamera : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "camera"; }
        }

        public IcarusCameraOperation Operation { get { return (IcarusCameraOperation)((IcarusConstantExpression)Parameters["Operation"].Value)?.Value; } set { ((IcarusConstantExpression)Parameters["Operation"].Value).Value = value; } }

		public IcarusCommandCamera()
		{
			Parameters.Add(new IcarusChoiceParameter("Operation", new IcarusConstantExpression(IcarusCameraOperation.None)));
		}

        public override object Clone()
        {
            IcarusCommandCamera clone = new IcarusCommandCamera();
            clone.Operation = Operation;
            return clone;
        }
    }
}
