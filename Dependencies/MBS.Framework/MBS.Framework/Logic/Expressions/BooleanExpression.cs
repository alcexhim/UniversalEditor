using System;
namespace MBS.Framework.Logic.Expressions
{
	public enum BooleanExpressionComparison
	{
		Equal,
		LessThan,
		GreaterThan,
		LessThanOrEqual,
		GreaterThanOrEqual
	}
	public class BooleanExpression : Expression
	{
		public BooleanExpression(bool value = false)
		{
			Value = value;
		}
		public bool Value { get; set; } = false;

		public override Expression Evaluate(ExpressionContext context)
		{
			return this;
		}
	}
}
