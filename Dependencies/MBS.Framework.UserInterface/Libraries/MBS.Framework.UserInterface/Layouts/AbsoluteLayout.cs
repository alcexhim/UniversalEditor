using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Layouts
{
	public class AbsoluteLayout : Layout
	{
		public class Constraints : MBS.Framework.UserInterface.Constraints
		{
			private int mvarX = 0;
			public int X { get { return mvarX; } set { mvarX = value; } }

			private int mvarY = 0;
			public int Y { get { return mvarY; } set { mvarY = value; } }

			private int mvarWidth = 0;
			public int Width { get { return mvarWidth; } set { mvarWidth = value; } }

			private int mvarHeight = 0;
			public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

			public Constraints(int x, int y, int width, int height)
			{
				mvarX = x;
				mvarY = y;
				mvarWidth = width;
				mvarHeight = height;
			}
		}

		private Dictionary<Control, Rectangle> mvarControlBounds = new Dictionary<Control, Rectangle>();

		protected override void ResetControlBoundsInternal(Control ctl = null)
		{
		}

		public void SetControlBounds(Control ctl, Rectangle bounds)
		{
			mvarControlBounds[ctl] = bounds;
		}

		protected override Rectangle GetControlBoundsInternal(Control ctl)
		{
			if (mvarControlBounds.ContainsKey(ctl)) return mvarControlBounds[ctl];
			return Rectangle.Empty;
		}
	}
}
