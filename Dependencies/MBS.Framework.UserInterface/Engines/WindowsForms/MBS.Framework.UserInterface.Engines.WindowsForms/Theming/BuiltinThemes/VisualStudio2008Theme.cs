using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class VisualStudio2008Theme : VisualStudio2005Theme
	{
		protected override void InitBlueLunaColors()
		{
			base.InitBlueLunaColors();

			ColorTable.CommandBarMenuControlBorderOuter = Color.FromArgb(249, 249, 249);
			ColorTable.CommandBarMenuControlBorderCorner = Color.FromArgb(149, 178, 223);
			ColorTable.CommandBarMenuControlBorderInner = Color.FromArgb(49, 106, 197);
		}

		public override void DrawMenuItemBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			if (item.Selected)
			{
				if (item.IsOnDropDown)
				{

				}
				else
				{
					graphics.DrawRoundedRectangle(new Pen(ColorTable.CommandBarMenuControlBorderOuter), 0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
					graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuControlBorderCorner), 1, 1, item.Bounds.Width - 2, item.Bounds.Height - 2);
					graphics.DrawRoundedRectangle(new Pen(ColorTable.CommandBarMenuControlBorderInner), 1, 1, item.Bounds.Width - 2, item.Bounds.Height - 2);
				}
			}
		}
	}
}
