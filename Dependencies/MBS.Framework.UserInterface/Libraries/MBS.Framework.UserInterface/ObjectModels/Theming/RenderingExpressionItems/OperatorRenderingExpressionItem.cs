using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingExpressionItems
{
	public enum OperatorType
	{
		Add,
		Subtract,
		Multiply,
		Divide
	}
	public class OperatorRenderingExpressionItem : RenderingExpressionItem
	{
		private OperatorType mvarType = OperatorType.Add;
		public OperatorType Type { get { return mvarType; } set { mvarType = value; } }

		public OperatorRenderingExpressionItem(OperatorType type)
		{
			mvarType = type;
		}

		public override float Evaluate(Dictionary<string, object> variables)
		{
			return 0;
		}
		public override object Clone()
		{
			OperatorRenderingExpressionItem clone = new OperatorRenderingExpressionItem(mvarType);
			return clone;
		}
	}
}
