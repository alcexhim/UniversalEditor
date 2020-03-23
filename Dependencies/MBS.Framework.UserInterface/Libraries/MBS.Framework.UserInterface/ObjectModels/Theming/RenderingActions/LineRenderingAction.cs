using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming.RenderingActions
{
	public class LineRenderingAction : RenderingAction
	{
		private RenderingExpression mvarX1 = null;
		public RenderingExpression X1 { get { return mvarX1; } set { mvarX1 = value; } }
		private RenderingExpression mvarX2 = null;
		public RenderingExpression X2 { get { return mvarX2; } set { mvarX2 = value; } }
		private RenderingExpression mvarY1 = null;
		public RenderingExpression Y1 { get { return mvarY1; } set { mvarY1 = value; } }
		private RenderingExpression mvarY2 = null;
		public RenderingExpression Y2 { get { return mvarY2; } set { mvarY2 = value; } }
		
		private Outline mvarOutline = null;
		public Outline Outline { get { return mvarOutline; } set { mvarOutline = value; } }

		public override object Clone()
		{
			LineRenderingAction clone = new LineRenderingAction();
			clone.X1 = (mvarX1.Clone() as RenderingExpression);
			clone.X2 = (mvarX2.Clone() as RenderingExpression);
			clone.Y1 = (mvarY1.Clone() as RenderingExpression);
			clone.Y2 = (mvarY2.Clone() as RenderingExpression);
			clone.Outline = (mvarOutline.Clone() as Outline);
			return clone;
		}
	}
}
