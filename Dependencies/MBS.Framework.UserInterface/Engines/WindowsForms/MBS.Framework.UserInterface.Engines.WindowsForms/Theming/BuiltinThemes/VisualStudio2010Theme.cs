using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MBS.Framework.UserInterface.Theming;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class VisualStudio2010Theme : VisualStudio2008Theme
	{
		private Bitmap ContentAreaBackgroundImage = new Bitmap(4, 4);
		private TextureBrush tContentAreaBackgroundImage = null;

		protected override void InitCommonColors()
		{
			base.InitCommonColors();

			ColorTable.FocusedHighlightedBackground = Color.FromArgb(160, 201, 221);
			ColorTable.FocusedHighlightedForeground = Color.FromKnownColor(KnownColor.ControlText);
			ColorTable.FocusedHighlightedBorder = Color.FromArgb(216, 235, 250);

			ColorTable.CommandBarMenuBorder = Color.FromArgb(155, 167, 183);

			ColorTable.CommandBarGradientMenuBarBackgroundBegin = Color.FromArgb(202, 211, 226);
			ColorTable.CommandBarGradientMenuBarBackgroundEnd = Color.FromArgb(174, 185, 205);

			ColorTable.CommandBarGradientMenuBackgroundBegin = Color.FromArgb(233, 236, 238);
			ColorTable.CommandBarGradientMenuBackgroundEnd = Color.FromArgb(208, 215, 226);

			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(156, 170, 193);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(156, 170, 193);

			ColorTable.DialogBackground = Color.FromArgb(156, 170, 193);

			ColorTable.CommandBarControlTextHover = ColorTable.CommandBarControlText;
			ColorTable.CommandBarControlTextPressed = ColorTable.CommandBarControlText;

			ColorTable.CommandBarControlBorderHover = Color.FromArgb(229, 195, 101);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(229, 195, 101);
			ColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(229, 195, 101);

			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(255, 232, 166);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(229, 195, 101);

			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(255, 239, 187);
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(255, 252, 244);

			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(255, 252, 242);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(255, 243, 207);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(255, 236, 181);
			
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(255, 232, 166);
			// ColorTable.CommandBarControlBackgroundPressedGradientMiddle = Color.FromArgb(255, 232, 166);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(255, 232, 166);

			ColorTable.CommandBarBackground = Color.FromArgb(188, 189, 216);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(188, 199, 216);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(188, 199, 216);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(188, 199, 216);

			ColorTable.CommandBarDragHandle = Color.FromArgb(96, 114, 140);
			ColorTable.CommandBarDragHandleShadow = Color.Transparent;

			ColorTable.CommandBarMenuSplitterLine = Color.FromArgb(190, 195, 203);
			ColorTable.CommandBarMenuSplitterLineHighlight = Color.Transparent;

			ColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(133, 145, 162);
			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.Transparent;

			ColorTable.CommandBarImageMarginBackground = Color.FromArgb(233, 236, 238);

			ColorTable.ContentAreaBackground = Color.FromArgb(44, 61, 91);
			ColorTable.ContentAreaBackgroundDark = Color.FromArgb(41, 57, 85);
			ColorTable.ContentAreaBackgroundLight = Color.FromArgb(53, 73, 106);

			ColorTable.ListViewItemSelectedBackground = Color.FromArgb(255, 232, 166);
			ColorTable.ListViewItemHoverBackgroundGradientBegin = Color.FromArgb(255, 254, 249);
			ColorTable.ListViewItemHoverBackgroundGradientMiddle = Color.FromArgb(255, 236, 181);
			ColorTable.ListViewItemHoverBackgroundGradientEnd = Color.FromArgb(255, 237, 184);
			ColorTable.ListViewItemHoverBorder = Color.FromArgb(229, 195, 101);
			ColorTable.ListViewRangeSelectionBorder = Color.FromArgb(229, 195, 101);
			ColorTable.ListViewRangeSelectionBackground = Color.FromArgb(80, 255, 232, 166);

			ColorTable.StatusBarBackground = System.Drawing.Color.FromArgb(41, 57, 85);
			ColorTable.StatusBarText = System.Drawing.Color.White;

			ColorTable.DocumentTabBackground = System.Drawing.Color.FromArgb(75, 94, 128);
			ColorTable.DocumentTabBorder = System.Drawing.Color.FromArgb(54, 78, 111);
			
			ColorTable.DocumentTabBorderHover = System.Drawing.Color.FromArgb(155, 167, 183);
			ColorTable.DocumentTabBackgroundHoverGradientBegin = System.Drawing.Color.FromArgb(111, 119, 118);
			ColorTable.DocumentTabBackgroundHoverGradientEnd = System.Drawing.Color.FromArgb(76, 92, 116);

			ColorTable.DocumentTabBorderSelected = Color.Transparent;
			ColorTable.DocumentTabBackgroundSelectedGradientBegin = Color.FromArgb(255, 252, 243);
			ColorTable.DocumentTabBackgroundSelectedGradientMiddle = Color.FromArgb(255, 243, 207);
			ColorTable.DocumentTabBackgroundSelectedGradientEnd = Color.FromArgb(255, 232, 166);

			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(255, 252, 242);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(255, 238, 186);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(255, 232, 166);

			ColorTable.DocumentTabInactiveBackgroundSelectedGradientBegin = Color.FromArgb(252, 252, 252);
			ColorTable.DocumentTabInactiveBackgroundSelectedGradientMiddle = Color.FromArgb(215, 220, 229);
			ColorTable.DocumentTabInactiveBackgroundSelectedGradientEnd = Color.FromArgb(206, 212, 223);

			ColorTable.DocumentTabText = Color.White; // Color.FromArgb(206, 212, 221);
			ColorTable.DocumentTabTextSelected = Color.Black;

			ColorTable.TextBoxBorderHover = Color.FromArgb(229, 195, 101);
			ColorTable.TextBoxBorderNormal = Color.FromArgb(133, 145, 162);
		}

		public override void DrawCheck(Graphics graphics, ToolStripItem item, Rectangle rect)
		{
			Rectangle rect1 = rect;
			rect1.X += 2;
			rect1.Y++;
			rect1.Height -= 2;

			base.DrawCheck(graphics, item, rect1);

			Rectangle checkRect = new Rectangle(rect1.X + 4, rect1.Y + 4, 10, 11);
			
			DrawingTools.DrawCheckMark(graphics, Pens.Black, checkRect);
			return;

			for (int i = 0; i < 3; i++)
			{
				graphics.DrawLine(Pens.Black, checkRect.X + (4 + i), checkRect.Y + (5 - i), checkRect.X + (4 + i), checkRect.Y + (8 - i));
			}
			graphics.DrawLine(Pens.Black, checkRect.X + 8, checkRect.Y + 0, checkRect.X + 8, checkRect.Y + 2);

			graphics.DrawLine(Pens.Black, checkRect.X + 1, checkRect.Y + 5, checkRect.X + 3, checkRect.Y + 7);
			graphics.DrawLine(Pens.Black, checkRect.X + 0, checkRect.Y + 5, checkRect.X + 3, checkRect.Y + 8);
			graphics.DrawLine(Pens.Black, checkRect.X + 0, checkRect.Y + 6, checkRect.X + 3, checkRect.Y + 9);
			graphics.DrawLine(Pens.Black, checkRect.X + 1, checkRect.Y + 8, checkRect.X + 3, checkRect.Y + 10);
		}
		public override void DrawCommandBarBackground(Graphics graphics, System.Drawing.Rectangle rectangle, Orientation orientation, ToolStrip parent)
		{
			if (parent is MenuStrip)
			{
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(0, 0), parent.Bounds.Size), ColorTable.CommandBarGradientMenuBarBackgroundBegin, ColorTable.CommandBarGradientMenuBarBackgroundEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(brush, rectangle);
				return;
			}
			else if (parent is ToolStripDropDown)
			{
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(0, 0), parent.Bounds.Size), ColorTable.CommandBarGradientMenuBackgroundBegin, ColorTable.CommandBarGradientMenuBackgroundEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(brush, rectangle);
				graphics.DrawRectangle(new Pen(Color.FromArgb(155, 167, 183)), rectangle);
				return;
			}
			else if (parent is StatusStrip)
			{
				graphics.Clear(ColorTable.StatusBarBackground);
				return;
			}
			base.DrawCommandBarBackground(graphics, rectangle, orientation, parent);
		}
		public override void DrawCommandBarBorder(Graphics graphics, ToolStrip toolStrip, Rectangle connectedArea)
		{
			base.DrawCommandBarBorder(graphics, toolStrip, connectedArea);

			if (toolStrip is ToolStripDropDown)
			{
				graphics.DrawLine(new Pen(ColorTable.CommandBarGradientMenuBackgroundBegin), connectedArea.X, connectedArea.Y, connectedArea.Right - 1, connectedArea.Y);
				graphics.DrawLine(new Pen(ColorTable.CommandBarGradientMenuBackgroundBegin), connectedArea.X, connectedArea.Y + 1, connectedArea.Right - 1, connectedArea.Y + 1);
			}
		}

		public override void DrawCommandButtonBackground(Graphics graphics, ToolStripButton item, ToolStrip parent)
		{
			Rectangle r = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);

			if (item.Checked)
			{
				if (item.Selected)
				{
					SolidBrush b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundSelectedHover);
					graphics.FillRectangle(b1, r);

					graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderSelectedHover), r);
				}
				else
				{
					SolidBrush b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundSelected);
					graphics.FillRectangle(b1, r);

					graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderSelected), r);
				}
			}
			else if (item.Pressed)
			{
				SolidBrush b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundPressed);
				graphics.FillRectangle(b1, r);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), r);
			}
			else if (item.Selected)
			{
				LinearGradientBrush b1 = new LinearGradientBrush(r, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
				SolidBrush b2 = new SolidBrush(ColorTable.CommandBarControlBackgroundHoverGradientEnd);

				Rectangle r1 = new Rectangle(0, 0, r.Width, r.Height / 2);
				Rectangle r2 = new Rectangle(0, r.Height / 2, r.Width, (r.Height / 2) + 1);
				graphics.FillRectangle(b1, r1);
				graphics.FillRectangle(b2, r2);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r);
			}
		}
		public override void DrawDropDownButtonBackground(Graphics graphics, ToolStripDropDownButton item, ToolStrip parent)
		{
			Rectangle r = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);

			if (item.Pressed)
			{
				SolidBrush b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundPressed);
				graphics.FillRectangle(b1, r);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), r);
			}
			else if (item.Selected)
			{
				LinearGradientBrush b1 = new LinearGradientBrush(r, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
				SolidBrush b2 = new SolidBrush(ColorTable.CommandBarControlBackgroundHoverGradientEnd);

				Rectangle r1 = new Rectangle(0, 0, r.Width, r.Height / 2);
				Rectangle r2 = new Rectangle(0, r.Height / 2, r.Width, (r.Height / 2) + 1);
				graphics.FillRectangle(b1, r1);
				graphics.FillRectangle(b2, r2);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r);
			}
		}
		public override void DrawSplitButtonBackground(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
			Rectangle r = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);

			ToolStripSplitButton tssb = (item as ToolStripSplitButton);
			
			int w = tssb.DropDownButtonWidth;
				
			if (tssb.ButtonPressed)
			{
				Brush b1 = new LinearGradientBrush(r, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
				SolidBrush b2 = new SolidBrush(ColorTable.CommandBarControlBackgroundHoverGradientEnd);

				Rectangle r1 = new Rectangle(0, 0, r.Width, r.Height / 2);
				Rectangle r2 = new Rectangle(0, r.Height / 2, r.Width, (r.Height / 2) + 1);
				graphics.FillRectangle(b1, r1);
				graphics.FillRectangle(b2, r2);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r);
				graphics.DrawLine(new Pen(ColorTable.CommandBarControlBorderHover), r.Right - w, r.Top, r.Right - w, r.Bottom);
				
				r.Width -= w;
				
				b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundPressed);
				graphics.FillRectangle(b1, r);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), r);
			}
			else if (tssb.Pressed)
			{
				SolidBrush b1 = new SolidBrush(ColorTable.CommandBarControlBackgroundPressed);
				graphics.FillRectangle(b1, r);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), r);
			}
			else if (tssb.Selected)
			{
				LinearGradientBrush b1 = new LinearGradientBrush(r, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
				SolidBrush b2 = new SolidBrush(ColorTable.CommandBarControlBackgroundHoverGradientEnd);

				Rectangle r1 = new Rectangle(0, 0, r.Width, r.Height / 2);
				Rectangle r2 = new Rectangle(0, r.Height / 2, r.Width, (r.Height / 2) + 1);
				graphics.FillRectangle(b1, r1);
				graphics.FillRectangle(b2, r2);

				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r);
				graphics.DrawLine(new Pen(ColorTable.CommandBarControlBorderHover), r.Right - w, r.Top, r.Right - w, r.Bottom);
			}

			DrawArrow(graphics, tssb.Enabled, tssb.DropDownButtonBounds, ArrowDirection.Down);
		}

		public override void DrawMenuItemBackground(Graphics graphics, ToolStripItem item)
		{
			Rectangle r = new Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height - 1);
			if (item.IsOnDropDown)
			{
				r.X += 3;
				r.Width -= 4;
			}

			if (item.Pressed && !item.IsOnDropDown)
			{
				Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height);
				Rectangle rectFill = rect;
				rectFill.X += 1;
				rectFill.Y += 1;
				rectFill.Width -= 1;
				rectFill.Height -= 1;

				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarGradientMenuBackgroundBegin), rectFill);

				graphics.DrawRoundedRectangle(new Pen(ColorTable.CommandBarMenuBorder), rect, 2, 1, 0, 0);
			}
			else if (item.Selected || (item.Pressed && item.IsOnDropDown))
			{
				LinearGradientBrush b1 = new LinearGradientBrush(r, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
				SolidBrush b2 = new SolidBrush(ColorTable.CommandBarControlBackgroundHoverGradientEnd);

				Rectangle r1 = new Rectangle(r.X, r.Y, r.Width, r.Height / 2);
				Rectangle r2 = new Rectangle(r.X, r.Height / 2, r.Width, (r.Height / 2) + 1);

				r1.Inflate(-1, 0);
				r2.Inflate(-1, 0);

				graphics.FillRectangle(b1, r1);
				graphics.FillRectangle(b2, r2);

				r.Height += 1;

				graphics.DrawRoundedRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r, 1);
			}
		}

		public override void DrawImageMargin(Graphics graphics, Rectangle affectedBounds, ToolStrip toolStrip)
		{
			graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarImageMarginBackground), affectedBounds);
		}

		public override void DrawContentAreaBackground(Graphics graphics, Rectangle rectangle)
		{
			if (tContentAreaBackgroundImage == null)
			{
				Graphics gContentAreaBackgroundImage = Graphics.FromImage(ContentAreaBackgroundImage);

				Pen pDark = new Pen(ColorTable.ContentAreaBackgroundDark);
				Pen pLight = new Pen(ColorTable.ContentAreaBackgroundLight);
				Pen pBack = new Pen(ColorTable.ContentAreaBackground);

				gContentAreaBackgroundImage.Clear(ColorTable.ContentAreaBackground);
				gContentAreaBackgroundImage.DrawLine(pDark, 0, 0, 2, 2);
				gContentAreaBackgroundImage.DrawLine(pLight, 0, 1, 2, 3);
				gContentAreaBackgroundImage.DrawLine(pBack, 1, 0, 1, 3);
				gContentAreaBackgroundImage.Flush();

				tContentAreaBackgroundImage = new TextureBrush(ContentAreaBackgroundImage);
			}
			graphics.FillRectangle(tContentAreaBackgroundImage, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height));
		}

		#region DockPanel
		public override void DrawDocumentTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
			Brush brushFill = new SolidBrush(ColorTable.DocumentTabBackground);
			Pen penLine = new System.Drawing.Pen(ColorTable.DocumentTabBorder);
			
			if (selected)
			{
				if (focused)
				{
					DrawingTools.FillWithShinyGradient(g, ColorTable.DocumentTabInactiveBackgroundSelectedGradientBegin, ColorTable.DocumentTabInactiveBackgroundSelectedGradientMiddle, ColorTable.DocumentTabInactiveBackgroundSelectedGradientEnd, rectTab, (int)(rectTab.Height / 2), (int)((rectTab.Height / 2) + 1), LinearGradientMode.Horizontal);
					g.DrawRoundedRectangle(new System.Drawing.Pen(ColorTable.DocumentTabInactiveBorderSelected), rectTab, 0, 1, 0, 1);
				}
				else
				{
					DrawingTools.FillWithShinyGradient(g, ColorTable.DocumentTabBackgroundSelectedGradientBegin, ColorTable.DocumentTabBackgroundSelectedGradientMiddle, ColorTable.DocumentTabBackgroundSelectedGradientEnd, rectTab, (int)(rectTab.Height / 2), (int)((rectTab.Height / 2) + 1), LinearGradientMode.Horizontal);
					g.DrawRoundedRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderSelected), rectTab, 0, 1, 0, 1);
				}
				return;
			}

			switch (position)
			{
				case UserInterface.Controls.Docking.DockingItemPlacement.Left:
				{
					switch (controlState)
					{
						case ControlState.Normal:
						{
							g.FillRectangle(brushFill, rectTab);
							g.DrawRoundedRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorder), rectTab, 0, 1, 0, 1);
							break;
						}
						case ControlState.Hover:
						{
							g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DocumentTabBackgroundHoverGradientBegin), rectTab);
							g.DrawRoundedRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderHover), rectTab, 0, 1, 0, 1);
							break;
						}
					}
					break;
				}
				case UserInterface.Controls.Docking.DockingItemPlacement.Center:
				{
					switch (controlState)
					{
						case ControlState.Hover:
						{
							g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DocumentTabBackgroundHoverGradientBegin), rectTab);
							g.DrawRoundedRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderHover), rectTab, 1, 1, 0, 0);
							break;
						}
					}
					break;
				}
			}
		}
		public override void DrawDockPanelTitleBarBackground(Graphics g, Rectangle rect, bool focused)
		{
			if (focused)
			{
				DrawingTools.FillWithDoubleGradient(ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin, ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle, ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd, g, rect, rect.Height / 2, rect.Height / 2, LinearGradientMode.Horizontal, false);
			}
			else
			{
				g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DockingWindowInactiveTabBackgroundGradientBegin), rect);
			}
		}
		#endregion


		#region System Controls
		#region TextBox
		public override void DrawTextBoxBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state)
		{
			Color BorderColor = ColorTable.TextBoxBorderNormal;
			switch (state)
			{
				case ControlState.Hover:
				{
					BorderColor = ColorTable.TextBoxBorderHover;
					break;
				}
			}
			
			g.DrawRoundedRectangle(new Pen(BorderColor), rect, 1);
		}
		#endregion
		#region ListView
		public override void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused)
		{
			switch (state)
			{
				case ControlState.Hover:
				{
					DrawingTools.FillWithDoubleGradient(ColorTable.ListViewItemHoverBackgroundGradientBegin, ColorTable.ListViewItemHoverBackgroundGradientMiddle, ColorTable.ListViewItemHoverBackgroundGradientEnd, g, rect, rect.Height / 2, rect.Height / 2, LinearGradientMode.Vertical, false);
					g.DrawRectangle(new Pen(ColorTable.ListViewItemHoverBorder), rect);
					break;
				}
			}
			if (selected)
			{
				g.FillRectangle(new SolidBrush(ColorTable.ListViewItemSelectedBackground), rect);
				g.DrawRectangle(new Pen(ColorTable.ListViewItemHoverBorder), rect);
			}
		}
		public override void DrawListSelectionRectangle(Graphics g, Rectangle rect)
		{
			Color border = ColorTable.ListViewRangeSelectionBorder;
			Color fill = ColorTable.ListViewRangeSelectionBackground;
			
			g.FillRectangle(new SolidBrush(fill), rect);
			g.DrawRectangle(new Pen(border), rect);

			// g.FillRectangle(new SolidBrush(Color.FromArgb(128, 162, 175, 198)), rect);
			// g.DrawRectangle(new Pen(Color.FromArgb(44, 61, 91)), rect);
		}
		#endregion
		#region ProgressBar
		public override void DrawProgressBarBackground(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		public override void DrawProgressBarChunk(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		public override void DrawProgressBarPulse(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		#endregion
		#endregion
	}
}
