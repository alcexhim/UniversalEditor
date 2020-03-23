using System;
using System.Collections.Generic;

namespace MBS.Framework.Logic
{
	public class ExpressionContext
	{
		public Variable.VariableCollection Variables { get; } = new Variable.VariableCollection();

		public void SetVariableValue<T>(string variableName, T value)
		{
			Variables[variableName].Expression = new Expressions.LiteralExpression<T>(value);
		}

		public event VariableRequestedEventHandler VariableRequested;
		protected virtual void OnVariableRequested(VariableRequestedEventArgs e)
		{
			VariableRequested?.Invoke(this, e);
		}
	}
}
