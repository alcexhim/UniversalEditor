using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandAffect : IcarusPredefinedContainerCommand
    {
        public override string Name { get { return "affect"; } }

        private IcarusExpression mvarTarget = null;
        public IcarusExpression Target { get { return mvarTarget; } set { mvarTarget = value; } }

        private IcarusExpression mvarAffectType = new IcarusExpression(IcarusExpressionType.Float, IcarusAffectType.Flush);
        public IcarusExpression AffectType { get { return mvarAffectType; } set { mvarAffectType = value; } }

        public override object Clone()
        {
            IcarusCommandAffect clone = new IcarusCommandAffect();
            clone.Target = (mvarTarget.Clone() as IcarusExpression);
            clone.AffectType = mvarAffectType;
            foreach (IcarusCommand command in Commands)
            {
                clone.Commands.Add(command.Clone() as IcarusCommand);
            }
            return clone;
        }
    }
}
