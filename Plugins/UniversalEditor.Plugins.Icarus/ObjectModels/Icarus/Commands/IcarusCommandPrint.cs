using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandPrint : IcarusPredefinedCommand
    {
		public IcarusCommandPrint()
		{
			Parameters.Add(new IcarusGenericParameter("Text", new IcarusConstantExpression("DEFAULT")));
		}

        public override string Name { get { return "print"; } }

        public IcarusExpression Text { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }

        public override object Clone()
        {
            IcarusCommandPrint clone = new IcarusCommandPrint();
            clone.Text = (Text.Clone() as IcarusExpression);
            return clone;
        }
    }
}
