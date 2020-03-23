using System;
namespace MBS.Framework.Logic.Expressions
{
	public class ComplexExpression : Expression
	{
		public ComplexExpression(Expression[] exprs)
		{
			for (int i = 0; i < exprs.Length; i++)
			{
				Expressions.Add(exprs[i]);
			}
		}

		public Expression.ExpressionCollection Expressions { get; } = new Expression.ExpressionCollection();

		public override Expression Evaluate(ExpressionContext context)
		{
			return null;
		}
	}
}
