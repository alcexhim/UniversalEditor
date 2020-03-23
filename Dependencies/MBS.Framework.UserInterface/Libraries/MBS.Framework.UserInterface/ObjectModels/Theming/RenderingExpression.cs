using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBS.Framework.UserInterface.ObjectModels.Theming.RenderingExpressionItems;
namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class RenderingExpression : ICloneable
	{
		private RenderingExpression()
		{

		}

		private static bool IsNumeric(char value)
		{
			return (value >= '0' && value <= '9');
		}
		
		public static RenderingExpression Parse(string value)
		{
			RenderingExpression expr = new RenderingExpression();

			bool escaping = false;

			StringBuilder sbLiteral = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (IsNumeric(value[i]) || (value[i] == '.' && sbLiteral.Length > 0))
				{
					sbLiteral.Append(value[i]);
				}
				else if (value[i] == ' ')
				{
					continue;
				}
				else if (value[i] == '-')
				{
					expr.Items.Add(new OperatorRenderingExpressionItem(OperatorType.Subtract));
				}
				else if (value[i] == '\\')
				{
					if (escaping)
					{
						sbLiteral.Append(value[i]);
						escaping = false;
						continue;
					}
					escaping = true;
				}
				else if (value[i] == '$')
				{
					if (escaping)
					{
						sbLiteral.Append(value[i]);
						escaping = false;
						continue;
					}
					if (value[i + 1] == '(')
					{
						int start = i + 2;
						int end = value.IndexOf(')', i + 2) - start;

						string variableName = value.Substring(start, end);
						i += 2;
						i += variableName.Length;
						expr.Items.Add(new VariableRenderingExpressionItem(variableName));
					}
				}
				else
				{
					if (sbLiteral.Length > 0)
					{
						float val = Single.Parse(sbLiteral.ToString());
						sbLiteral = new StringBuilder();
						expr.Items.Add(new LiteralRenderingExpressionItem(val));
					}
				}
			}

			// final loop
			if (sbLiteral.Length > 0)
			{
				float val = Single.Parse(sbLiteral.ToString());
				sbLiteral = new StringBuilder();
				expr.Items.Add(new LiteralRenderingExpressionItem(val));
			}

			return expr;
		}

		private RenderingExpressionItem.RenderingExpressionItemCollection mvarItems = new RenderingExpressionItem.RenderingExpressionItemCollection();
		public RenderingExpressionItem.RenderingExpressionItemCollection Items { get { return mvarItems; } }

		public float Evaluate(Dictionary<string, object> variables)
		{
			float value = 0.0f;
			OperatorType _op = OperatorType.Add;

			foreach (RenderingExpressionItem item in mvarItems)
			{
				if (item is OperatorRenderingExpressionItem)
				{
					OperatorRenderingExpressionItem op = (item as OperatorRenderingExpressionItem);
					_op = op.Type;
					continue;
				}

				switch (_op)
				{
					case OperatorType.Add:
					{
						value += item.Evaluate(variables);
						break;
					}
					case OperatorType.Subtract:
					{
						value -= item.Evaluate(variables);
						break;
					}
					case OperatorType.Multiply:
					{
						value *= item.Evaluate(variables);
						break;
					}
					case OperatorType.Divide:
					{
						value /= item.Evaluate(variables);
						break;
					}
				}
			}
			return value;
		}

		public object Clone()
		{
			RenderingExpression clone = new RenderingExpression();
			foreach (RenderingExpressionItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as RenderingExpressionItem);
			}
			return clone;
		}
	}
}
