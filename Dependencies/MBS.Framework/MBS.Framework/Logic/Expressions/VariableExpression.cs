using System;
namespace MBS.Framework.Logic.Expressions
{
	public class VariableExpression : Expression
	{
		public VariableExpression(string variableName)
		{
			VariableName = variableName;
		}

		public string VariableName { get; set; } = null;

		public override Expression Evaluate(ExpressionContext context)
		{
			Variable varr = context.Variables[VariableName];
			if (varr != null)
			{
				return varr.Expression;
			}
			return null;
		}
	}
}
