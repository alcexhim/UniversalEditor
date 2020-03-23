using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingExpressionItems
{
	public class VariableRenderingExpressionItem : RenderingExpressionItem
	{
		private string mvarVariableName = String.Empty;
		public string VariableName { get { return mvarVariableName; } set { mvarVariableName = value; } }

		public VariableRenderingExpressionItem(string variableName)
		{
			mvarVariableName = variableName;
		}

		public override float Evaluate(Dictionary<string, object> variables)
		{
			if (variables.ContainsKey(mvarVariableName))
			{
				float result = 0.0f;
				if (Single.TryParse(variables[mvarVariableName].ToString(), out result))
				{
					return result;
				}
			}
			return 0.0f;
		}

		public override object Clone()
		{
			VariableRenderingExpressionItem clone = new VariableRenderingExpressionItem(mvarVariableName);
			return clone;
		}
	}
}
