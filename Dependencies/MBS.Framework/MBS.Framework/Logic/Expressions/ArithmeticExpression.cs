using System;
namespace MBS.Framework.Logic.Expressions
{
	public enum ArithmeticOperation
	{
		None = 0,
		Addition,
		Subtraction,
		Multiplication,
		Division,
		And,
		Or,
		Xor,
		Modulus
	}
	public class ArithmeticExpression : Expression
	{
		public ArithmeticOperation Operation { get; set; } = ArithmeticOperation.Addition;
		public Expression PrimaryExpression { get; set; } = null;
		public Expression SecondaryExpression { get; set; } = null;

		public ArithmeticExpression()
		{
		}
		public ArithmeticExpression(ArithmeticOperation operation, Expression primary, Expression secondary)
		{
			Operation = operation;
			PrimaryExpression = primary;
			SecondaryExpression = secondary;
		}

		public override Expression Evaluate(ExpressionContext context)
		{
			// TODO: implement Evaluate() on ArithmeticExpression
			return null;
		}
	}
}
