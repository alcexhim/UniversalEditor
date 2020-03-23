using System;
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Layouts
{
	public class GridLayout : Layout
	{
		public class Constraints : MBS.Framework.UserInterface.Constraints
		{
			private int mvarRow = 0;
			public int Row { get { return mvarRow; } set { mvarRow = value; } }

			private int mvarColumn = 0;
			public int Column { get { return mvarColumn; } set { mvarColumn = value; } }

			private int mvarRowSpan = 0;
			public int RowSpan { get { return mvarRowSpan; } set { mvarRowSpan = value; } }

			private int mvarColumnSpan = 0;
			public int ColumnSpan { get { return mvarColumnSpan; } set { mvarColumnSpan = value; } }

			public ExpandMode Expand { get; set; } = ExpandMode.None;

			public Constraints(int row, int column, int rowSpan = 1, int columnSpan = 1, ExpandMode expand = ExpandMode.None)
			{
				mvarRow = row;
				mvarColumn = column;
				mvarRowSpan = rowSpan;
				mvarColumnSpan = columnSpan;
				Expand = expand;
			}
		}

		private int mvarRowSpacing = 6;
		public int RowSpacing { get { return mvarRowSpacing; } set { mvarRowSpacing = value; } }

		private int mvarColumnSpacing = 6;
		public int ColumnSpacing { get { return mvarColumnSpacing; } set { mvarColumnSpacing = value; } }

		protected override Rectangle GetControlBoundsInternal (Control ctl)
		{
			throw new NotImplementedException ();
		}
		protected override void ResetControlBoundsInternal (Control ctl)
		{
			throw new NotImplementedException ();
		}
	}
}

