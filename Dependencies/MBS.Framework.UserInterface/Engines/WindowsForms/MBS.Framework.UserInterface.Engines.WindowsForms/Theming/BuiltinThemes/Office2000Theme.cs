using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class Office2000Theme : ClassicTheme
	{
		public override bool EnableSpaceSaverMenus { get { return true; } }

		public override void DrawCommandBarBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle, Orientation orientation, System.Windows.Forms.ToolStrip parent)
		{

		}
		public override void DrawCommandButtonBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			if (item.Pressed)
			{
				DrawingTools.DrawSunkenBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
			}
			else if (item.Selected)
			{
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
			}
		}
		public override void DrawDropDownButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripDropDownButton item, System.Windows.Forms.ToolStrip parent)
		{
			if (item.Pressed)
			{
				DrawingTools.DrawSunkenBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
			}
			else if (item.Selected)
			{
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
			}
		}
		public override void DrawSplitButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)
		{
			System.Windows.Forms.ToolStripSplitButton tssb = (item as System.Windows.Forms.ToolStripSplitButton);
			DrawArrow(graphics, tssb.Enabled, new Rectangle(tssb.DropDownButtonBounds.X, tssb.DropDownButtonBounds.Y, tssb.DropDownButtonBounds.Width, tssb.DropDownButtonBounds.Height), System.Windows.Forms.ArrowDirection.Down);

			if (tssb.ButtonPressed)
			{
				DrawingTools.DrawSunkenBorderMini(graphics, new Rectangle(0, 1, tssb.ButtonBounds.Width, item.Bounds.Height - 2));
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(tssb.ButtonBounds.Width, 1, item.Bounds.Width - tssb.ButtonBounds.Width, item.Bounds.Height - 2));
			}
			else if (tssb.DropDownButtonPressed)
			{
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 1, tssb.ButtonBounds.Width, item.Bounds.Height - 2));
				DrawingTools.DrawSunkenBorderMini(graphics, new Rectangle(tssb.ButtonBounds.Width, 1, item.Bounds.Width - tssb.ButtonBounds.Width, item.Bounds.Height - 2));
			}
			else if (item.Selected)
			{
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 1, tssb.ButtonBounds.Width, item.Bounds.Height - 2));
				DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(tssb.ButtonBounds.Width, 1, item.Bounds.Width - tssb.ButtonBounds.Width, item.Bounds.Height - 2));
			}
		}
		public override void DrawMenuItemBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			if (item.Pressed)
			{
				if (item.IsOnDropDown)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1));
				}
				else
				{
					DrawingTools.DrawSunkenBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
				}
			}
			else if (item.Selected)
			{
				if (item.IsOnDropDown)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)), new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1));
				}
				else
				{
					DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 1, item.Bounds.Width - 1, item.Bounds.Height - 2));
				}
			}
		}
		public override void DrawCommandBarBorder(Graphics graphics, System.Windows.Forms.ToolStrip toolStrip, Rectangle connectedArea)
		{
			DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(0, 0, toolStrip.Width, toolStrip.Height));
		}
		public override void DrawText(Graphics graphics, string text, Color color, Font font, Rectangle textRectangle, System.Windows.Forms.TextFormatFlags textFormat, System.Windows.Forms.ToolStripTextDirection textDirection, System.Windows.Forms.ToolStripItem item)
		{
			color = (item.Enabled ? ColorTable.CommandBarControlText : System.Drawing.SystemColors.GrayText);

			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			graphics.DrawText(text, font, textRectangle, color, RotateFlipType.RotateNoneFlipNone, textFormat);
			// System.Windows.Forms.TextRenderer.DrawText(graphics, text, font, textRectangle, color, textFormat);
		}
		public override void DrawSeparator(Graphics graphics, System.Windows.Forms.ToolStripItem item, Rectangle rectangle, bool vertical)
		{
			base.DrawSeparator(graphics, item, rectangle, vertical);
		}
		public override void DrawGrip(Graphics graphics, Rectangle gripBounds, Orientation orientation, bool rtl)
		{
			DrawingTools.DrawRaisedBorderMini(graphics, new Rectangle(gripBounds.X, gripBounds.Y + 3, 3, 19));
		}
	}
}
