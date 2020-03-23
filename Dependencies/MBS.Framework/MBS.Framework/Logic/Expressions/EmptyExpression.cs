using System;
namespace MBS.Framework.Logic.Expressions
{
	public class EmptyExpression : Expression
	{
		public override Expression Evaluate(ExpressionContext context)
		{
			return Expression.Empty;
		}
	}
}
