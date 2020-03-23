using System;
namespace MBS.Framework.Logic.Expressions
{
	public class LiteralExpression<T> : Expression
	{
		public T Value { get; set; } = default(T);

		public LiteralExpression(T value = default(T))
		{
			Value = value;
		}

		public override Expression Evaluate(ExpressionContext context)
		{
			return this;
		}
	}
}
