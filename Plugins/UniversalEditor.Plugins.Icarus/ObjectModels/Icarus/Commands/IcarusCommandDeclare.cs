using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
    public class IcarusCommandDeclare : IcarusPredefinedCommand
    {
		public IcarusCommandDeclare()
		{
			Parameters.Add(new IcarusChoiceParameter("DataType", new IcarusConstantExpression("FLOAT"), new IcarusChoiceParameterValue[]
			{
				new IcarusChoiceParameterValue("FLOAT", new IcarusConstantExpression(IcarusVariableDataType.Float)),
				new IcarusChoiceParameterValue("STRING", new IcarusConstantExpression(IcarusVariableDataType.String)),
				new IcarusChoiceParameterValue("VECTOR", new IcarusConstantExpression(IcarusVariableDataType.Vector))
			}));
			Parameters.Add(new IcarusGenericParameter("VariableName", new IcarusConstantExpression("variablename")));
		}

        public override string Name { get { return "declare"; } }

        public IcarusVariableDataType DataType { get { return (IcarusVariableDataType) ((IcarusConstantExpression)Parameters["DataType"].Value)?.Value; } set { Parameters["DataType"].Value = new IcarusConstantExpression(value); } }
		public IcarusExpression VariableName { get { return Parameters["VariableName"].Value; } set { Parameters["VariableName"].Value = value; } }

		public override object Clone()
        {
            IcarusCommandDeclare clone = new IcarusCommandDeclare();
            clone.VariableName = (VariableName?.Clone() as IcarusExpression);
            clone.DataType = DataType;
            return clone;
        }
    }
}
