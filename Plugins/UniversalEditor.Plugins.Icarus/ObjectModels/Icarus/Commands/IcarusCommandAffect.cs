using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandAffect : IcarusPredefinedContainerCommand
    {
        public override string Name { get { return "affect"; } }

		public IcarusCommandAffect()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
			Parameters.Add(new IcarusChoiceParameter("AffectType",  new IcarusConstantExpression(IcarusAffectType.Flush), new IcarusChoiceParameterValue[]
			{
				new IcarusChoiceParameterValue("Flush", new IcarusConstantExpression(IcarusAffectType.Flush)),
				new IcarusChoiceParameterValue("Insert", new IcarusConstantExpression(IcarusAffectType.Insert))
			}));
		}

        public IcarusExpression Target { get { return Parameters["Target"].Value; } set { Parameters["Target"].Value = value; } }
        public IcarusExpression AffectType { get { return Parameters["AffectType"].Value; } set { Parameters["AffectType"].Value = value; } }

        public override object Clone()
        {
            IcarusCommandAffect clone = new IcarusCommandAffect();
            clone.Target = (Target.Clone() as IcarusExpression);
            clone.AffectType = (AffectType.Clone() as IcarusExpression);
            foreach (IcarusCommand command in Commands)
            {
                clone.Commands.Add(command.Clone() as IcarusCommand);
            }
            return clone;
        }
    }
}
