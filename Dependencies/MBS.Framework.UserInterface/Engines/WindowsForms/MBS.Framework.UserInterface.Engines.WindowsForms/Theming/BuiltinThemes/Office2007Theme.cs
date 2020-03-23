using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class Office2007Theme : Office2003Theme
	{
		public override void DrawRibbonTabPageBackground(Graphics graphics, Rectangle rectangle)
		{
			SolidBrush b = new SolidBrush(ColorTable.RibbonTabPageBackgroundGradientStart);
			LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, rectangle.Width, rectangle.Height), ColorTable.RibbonTabPageBackgroundGradientMiddle, ColorTable.RibbonTabPageBackgroundGradientEnd, LinearGradientMode.Vertical);

			Rectangle rectTop = new Rectangle(rectangle.Location, new Size(rectangle.Width, 17));
			Rectangle rectBottom = new Rectangle(rectangle.X, rectangle.Y + 17, rectangle.Width, rectangle.Height - 17);

			graphics.FillRectangle(b, rectTop);
			graphics.FillRectangle(lgb, rectBottom);
			graphics.DrawLine(new Pen(ColorTable.RibbonTabBarBorderBottom), rectangle.Left, rectangle.Bottom - 1, rectangle.Right, rectangle.Bottom - 1);
		}
		public override void DrawRibbonControlGroup(Graphics g, Rectangle rect, MBS.Framework.UserInterface.Controls.Ribbon.RibbonTabGroup group)
		{
			base.DrawRibbonControlGroup(g, rect, group);
			
			Pen p = new Pen(new LinearGradientBrush(rect, ColorTable.RibbonControlGroupBorderGradientBegin, ColorTable.RibbonControlGroupBorderGradientEnd, LinearGradientMode.Vertical));
			g.DrawRoundedRectangle(p, rect.X, rect.Y, rect.Width, rect.Height);
		}

		protected override void InitBlueLunaColors()
		{
			ColorTable.CommandBarBackground = Color.FromArgb(189, 219, 255);
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(189, 219, 255);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(189, 219, 255);

			ColorTable.CommandBarControlText = Color.Black;

			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(106, 155, 219);

			ColorTable.CommandBarControlBorderHover = Color.FromArgb(249, 186, 113);

			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(255, 243, 199);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(255, 217, 123);

			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(231, 239, 255);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(181, 214, 255);

			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(255, 183, 0);

			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(255, 243, 199);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(255, 217, 123);
		}
		protected override void InitAeroColors()
		{
			base.InitAeroColors();

			InitBlueLunaColors();
		}

		public override void DrawMenuItemBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			if (item.IsOnDropDown)
			{
				Padding dropDownMenuItemPaintPadding = new Padding(2, 0, 1, 0);
				rectangle = DeflateRect(rectangle, dropDownMenuItemPaintPadding);
				if (item.Selected)
				{
					Color color = this.ColorTable.CommandBarControlBorderSelected;

					using (Brush brush = new SolidBrush(Color.White))
					{
						graphics.FillRectangle(brush, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 4, rectangle.Height - 4);
					}

					int h = ((rectangle.Height - 5) / 2) + 1;
					using (Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, Color.FromArgb(253, 241, 227), Color.FromArgb(253, 223, 187), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
					{
						graphics.FillRectangle(brush, rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 5, h);
					}
					using (Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, Color.FromArgb(255, 206, 105), Color.FromArgb(255, 234, 175), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
					{
						graphics.FillRectangle(brush, rectangle.X + 2, h, rectangle.Width - 5, h + 1);
					}

					using (Pen pen = new Pen(color))
					{
						graphics.DrawRoundedRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
						return;
					}
				}
				Rectangle rectangle2 = rectangle;
				if (item.BackgroundImage != null)
				{
					DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
					return;
				}
				if (item.Owner == null || !(item.BackColor != item.Owner.BackColor))
				{
					return;
				}
				using (Brush brush2 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush2, rectangle2);
					return;
				}
			}
			base.DrawMenuItemBackground(graphics, item);
		}
	}
}
