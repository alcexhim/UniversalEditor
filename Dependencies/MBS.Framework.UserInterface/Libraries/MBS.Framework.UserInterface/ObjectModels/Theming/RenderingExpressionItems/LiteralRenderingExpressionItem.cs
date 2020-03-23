using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingExpressionItems
{
	public class LiteralRenderingExpressionItem : RenderingExpressionItem
	{
		private float mvarValue = 0.0f;
		public float Value { get { return mvarValue; } set { mvarValue = value; } }

		public LiteralRenderingExpressionItem(float value)
		{
			mvarValue = value;
		}

		public override float Evaluate(Dictionary<string, object> variables)
		{
			return mvarValue;
		}

		public override object Clone()
		{
			LiteralRenderingExpressionItem clone = new LiteralRenderingExpressionItem(mvarValue);
			return clone;
		}
	}
}
