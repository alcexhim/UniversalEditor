using System;
using System.Collections.Generic;
using MBS.Framework.Logic.Expressions;

namespace MBS.Framework.Logic
{
	public abstract class Expression
	{
		public static Expression Empty { get; } = new EmptyExpression();

		public class ExpressionCollection
			: System.Collections.ObjectModel.Collection<Expression>
		{

		}

		/// <summary>
		/// Parses the expression represented by <paramref name="value"/>.
		/// </summary>
		/// <returns>The parsed <see cref="Expression" />.</returns>
		/// <param name="value">The value to parse.</param>
		public static Expression Parse(string value)
		{
			Expression expr = Expression.Empty;
			if (String.IsNullOrEmpty(value))
				return expr;

			if (value.ToLower() == "true")
			{
				return new BooleanExpression(true);
			}
			else if (value.ToLower() == "false")
			{
				return new BooleanExpression(false);
			}

			// see if there are any variables
			if (value.Contains("$(") && value.Contains(")"))
			{
				int variableStartIndex = value.IndexOf("$(", StringComparison.Ordinal);
				int variableEndIndex = value.IndexOf(")", variableStartIndex, StringComparison.Ordinal);

				string wleft = value.Substring(0, variableStartIndex);
				string wvar = value.Substring(variableStartIndex + 2, variableEndIndex - 2);
				string wright = value.Substring(variableEndIndex + 1);

				Expression exprLeft = Expression.Parse(wleft);
				Expression exprRight = Expression.Parse(wright);

				List<Expression> list = new List<Expression>();
				if (exprLeft != Expression.Empty) list.Add(exprLeft);
				list.Add(new VariableExpression(wvar));
				if (exprRight != Expression.Empty) list.Add(exprRight);

				if (list.Count == 1)
					return list[0];

				return new ComplexExpression(list.ToArray());
			}
			else
			{
				// first try parsing it as a double
				double doublevalue = 0.0;
				if (Double.TryParse(value, out doublevalue))
				{
					return new LiteralExpression<double>(doublevalue);
				}

				// otherwise, idk
				return new LiteralExpression<string>(value);
			}
			return expr;
		}

		public abstract Expression Evaluate(ExpressionContext context);

		public T Evaluate<T>(ExpressionContext context)
		{
			Expression exprResult = Evaluate(context);
			if (exprResult is LiteralExpression<T>)
			{
				return ((LiteralExpression<T>)exprResult).Value;
			}
			throw new InvalidCastException("expression cannot be evaluated to LiteralExpression<" + typeof(T).Name + ">");
		}
	}
}
