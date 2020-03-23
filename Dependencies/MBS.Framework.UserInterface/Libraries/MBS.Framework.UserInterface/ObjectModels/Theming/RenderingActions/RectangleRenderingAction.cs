using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingActions
{
	public class RectangleRenderingAction : RenderingAction
	{
		private RenderingExpression mvarX = null;
		public RenderingExpression X { get { return mvarX; } set { mvarX = value; } }
		
		private RenderingExpression mvarY = null;
		public RenderingExpression Y { get { return mvarY; } set { mvarY = value; } }
		
		private RenderingExpression mvarWidth = null;
		public RenderingExpression Width { get { return mvarWidth; } set { mvarWidth = value; } }

		private RenderingExpression mvarHeight = null;
		public RenderingExpression Height { get { return mvarHeight; } set { mvarHeight = value; } }

		private Outline mvarOutline = null;
		public Outline Outline { get { return mvarOutline; } set { mvarOutline = value; } }

		private Fill mvarFill = null;
		public Fill Fill { get { return mvarFill; } set { mvarFill = value; } }

		public override object Clone()
		{
			RectangleRenderingAction clone = new RectangleRenderingAction();
			clone.X = (mvarX.Clone() as RenderingExpression);
			clone.Y = (mvarY.Clone() as RenderingExpression);
			clone.Width = (mvarWidth.Clone() as RenderingExpression);
			clone.Height = (mvarHeight.Clone() as RenderingExpression);

			clone.Outline = (mvarOutline.Clone() as Outline);
			clone.Fill = (mvarFill.Clone() as Fill);
			return clone;
		}
	}
}
