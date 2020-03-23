using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingActions
{
	public class TextRenderingAction : RenderingAction
	{
		private RenderingExpression mvarX = null;
		public RenderingExpression X { get { return mvarX; } set { mvarX = value; } }

		private RenderingExpression mvarY = null;
		public RenderingExpression Y { get { return mvarY; } set { mvarY = value; } }

		private RenderingExpression mvarWidth = null;
		public RenderingExpression Width { get { return mvarWidth; } set { mvarWidth = value; } }

		private RenderingExpression mvarHeight = null;
		public RenderingExpression Height { get { return mvarHeight; } set { mvarHeight = value; } }

		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		private string mvarFont = String.Empty;
		public string Font { get { return mvarFont; } set { mvarFont = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object Clone()
		{
			TextRenderingAction clone = new TextRenderingAction();
			clone.X = (mvarX.Clone() as RenderingExpression);
			clone.Y = (mvarY.Clone() as RenderingExpression);
			clone.Width = (mvarWidth.Clone() as RenderingExpression);
			clone.Height = (mvarHeight.Clone() as RenderingExpression);
			clone.Color = (mvarColor.Clone() as string);
			clone.Value = (mvarValue.Clone() as string);
			clone.HorizontalAlignment = mvarHorizontalAlignment;
			clone.VerticalAlignment = mvarVerticalAlignment;
			return clone;
		}

		private HorizontalAlignment mvarHorizontalAlignment = HorizontalAlignment.Left;
		public HorizontalAlignment HorizontalAlignment { get { return mvarHorizontalAlignment; } set { mvarHorizontalAlignment = value; } }

		private VerticalAlignment mvarVerticalAlignment = VerticalAlignment.Top;
		public VerticalAlignment VerticalAlignment { get { return mvarVerticalAlignment; } set { mvarVerticalAlignment = value; } }
	}
}
