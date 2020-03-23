using System;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Layouts;

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.TestProject
{
	public class TestCustomControl : CustomControl
	{
		private int timesPainted = 0;

		public TestCustomControl()
		{
			this.Size = new Dimension2D(200, 200);
		}

		private bool _ShowGreenBox = false;
		public bool ShowGreenBox
		{
			get { return _ShowGreenBox; }
			set { bool changed = (_ShowGreenBox != value); _ShowGreenBox = value; if (changed) Refresh(); }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.DrawText("Sample", Font.FromFamily("Liberation Sans", 26), new Rectangle(64, 64, 200, 200), Brushes.White, HorizontalAlignment.Center, VerticalAlignment.Middle);

			e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, 200, 200));
			e.Graphics.DrawRectangle(Pens.Red, new Rectangle(64, 64, 200 - 128, 200 - 128));

			if (ShowGreenBox)
				e.Graphics.FillRectangle(Brushes.Green, new Rectangle(64, 64, 200 - 128, 200 - 128));
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if ((e.X >= 64 && e.Y >= 64 && e.X <= 64 + (200 - 128) && e.Y <= 64 + (200 - 128)) && ShowGreenBox)
			{
				Cursor = Cursors.Help;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}
	}
}
