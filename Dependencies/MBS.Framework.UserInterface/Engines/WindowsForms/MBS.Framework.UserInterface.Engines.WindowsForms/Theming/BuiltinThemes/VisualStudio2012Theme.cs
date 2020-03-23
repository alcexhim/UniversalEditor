using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MBS.Framework.UserInterface.Theming;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class VisualStudio2012Theme : VisualStudio2010Theme
	{
		public enum ColorMode
		{
			Light,
			Dark,
			Blue
		}
		public enum StatusBarState
		{
			Initial,
			SolutionLoaded,
			Building,
			Debugging
		}

		private ColorTable mvarLightColorTable = new ColorTable();
		private ColorTable mvarDarkColorTable = new ColorTable();
		private ColorTable mvarBlueColorTable = new ColorTable();

		public void SetColorMode(ColorMode colorMode)
		{
			switch (colorMode)
			{
				case ColorMode.Light:
				{
					this.ColorTable = mvarLightColorTable;
					return;
				}
				case ColorMode.Dark:
				{
					this.ColorTable = mvarDarkColorTable;
					return;
				}
				case ColorMode.Blue:
				{
					this.ColorTable = mvarBlueColorTable;
					return;
				}
			}
			throw new ArgumentException("Invalid color mode");
		}

		public override void DrawSplitButtonBackground(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
			base.DrawSplitButtonBackground(graphics, item, parent);
			if (item is ToolStripSplitButton)
			{
				ToolStripSplitButton tssb = (item as ToolStripSplitButton);
				if (tssb.ButtonPressed)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 122, 204)), new Rectangle(0, 0, tssb.ButtonBounds.Width, tssb.ButtonBounds.Height));
				}
				else if (tssb.Pressed)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 122, 204)), new Rectangle(0, 0, tssb.Bounds.Width, tssb.Bounds.Height));
					graphics.FillArrow(tssb.DropDownButtonBounds, ArrowDirection.Down, Brushes.White);
				}
				else if (tssb.Selected)
				{
					graphics.DrawLine(new Pen(ColorTable.CommandBarMenuBackground), tssb.DropDownButtonBounds.Left, tssb.DropDownButtonBounds.Top, tssb.DropDownButtonBounds.Left, tssb.DropDownButtonBounds.Bottom);
					graphics.FillArrow(tssb.DropDownButtonBounds, ArrowDirection.Down, new SolidBrush(Color.FromArgb(0, 122, 204)));
				}
				else
				{
					graphics.FillArrow(tssb.DropDownButtonBounds, ArrowDirection.Down, new SolidBrush(Color.FromArgb(153, 153, 153)));
				}
			}
		}

		public override void DrawSizingGrip(Graphics graphics, Rectangle gripBounds)
		{
			Rectangle rect = gripBounds;
			rect = new Rectangle(rect.Width - 13, rect.Height - 13, 8, 8);

			DrawingTools.HSLColor color = new DrawingTools.HSLColor(ColorTable.StatusBarBackground);
			DrawingTools.HSLColor darkColor = new DrawingTools.HSLColor(color);
			DrawingTools.HSLColor lightColor = new DrawingTools.HSLColor(color);
			darkColor.Luminosity -= 20;
			lightColor.Saturation -= 56;
			lightColor.Luminosity += 10;

			for (int i = 0; i < 4; i += 2)
			{
				// top line
				graphics.DrawLine(new Pen(darkColor), rect.Right - (2 + i), rect.Y + (i - 1), rect.Left + (i - 1) + 1, rect.Bottom);
			}
		}

		// TODO: Fix the toplevel window border drawing before enabling this feature!
		public override bool HasCustomToplevelWindowFrame { get { return false; } }

		public override void DrawToplevelWindowBorder(Graphics g, Rectangle rectangle, string titleText, bool isActive)
		{
			base.DrawToplevelWindowBorder(g, rectangle, titleText, isActive);

			// BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(eg, rectangle);
			// Graphics g = bg.Graphics;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

			Bitmap bitmap = new Bitmap(rectangle.Width, SystemInformation.CaptionHeight + 8);
			Graphics cg = Graphics.FromImage(bitmap);
			cg.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.CommandBarGradientMenuBarBackgroundBegin), new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, SystemInformation.CaptionHeight + 8));
			TextRenderer.DrawText(cg, titleText, System.Drawing.SystemFonts.CaptionFont, new Point(42, 10), isActive ? Theming.Theme.CurrentTheme.ColorTable.WindowTitlebarForeground : Theming.Theme.CurrentTheme.ColorTable.WindowTitlebarForeground);
			cg.Flush();
			g.DrawImage(bitmap, 0, 0);

			Color c = ColorTable.StatusBarBackground;

			// g.DrawRectangle(new Pen(Color.Magenta, 16), rectangle);
			g.DrawRectangle(new Pen(c, 1), new Rectangle(rectangle.Left + 7, rectangle.Top + 7, rectangle.Width - 15, rectangle.Height - 15));
			
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

			/*
			int i = 7;
			c = Color.FromArgb(128, c.R, c.G, c.B);
			g.DrawRectangle(new Pen(c, 1), new Rectangle(rectangle.Left + i, rectangle.Top + i, (rectangle.Width - 8) - i, (rectangle.Height - 8) - i));
			*/

			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

			//bg.Render();
		}

		public override CommandBarMenuAnimationType CommandBarMenuAnimationType { get { return Theming.CommandBarMenuAnimationType.None; } }
		
		public VisualStudio2012Theme(ColorMode colorMode = ColorMode.Light)
		{
			SetColorMode(colorMode);
			InitializeMetricTable();
		}

		protected virtual void InitializeMetricTable()
		{
			MetricTable.DropDownButtonPadding = new Padding(0, 0, 0, 0);
		}

		protected virtual void InitializeLightColorTable()
		{
			mvarLightColorTable.FocusedHighlightedBackground = Color.FromArgb(160, 201, 221);
			mvarLightColorTable.FocusedHighlightedForeground = Color.FromKnownColor(KnownColor.ControlText);
			mvarLightColorTable.FocusedHighlightedBorder = Color.FromArgb(216, 235, 250);

			mvarLightColorTable.CommandBarMenuBorder = Color.FromArgb(89, 89, 89);

			mvarLightColorTable.CommandBarGradientMenuBarBackgroundBegin = Color.FromArgb(58, 58, 58);
			mvarLightColorTable.CommandBarGradientMenuBarBackgroundEnd = Color.FromArgb(58, 58, 58);

			mvarLightColorTable.CommandBarGradientMenuBackgroundBegin = Color.FromArgb(58, 58, 58);
			mvarLightColorTable.CommandBarGradientMenuBackgroundEnd = Color.FromArgb(58, 58, 58);

			mvarLightColorTable.CommandBarPanelGradientBegin = Color.FromArgb(68, 68, 68);
			mvarLightColorTable.CommandBarPanelGradientEnd = Color.FromArgb(68, 68, 68);

			mvarLightColorTable.CommandBarControlText = Color.FromArgb(255, 255, 255);
			mvarLightColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			mvarLightColorTable.CommandBarControlTextPressed = ColorTable.CommandBarControlText;

			mvarLightColorTable.CommandBarControlBorderHover = Color.FromArgb(189, 189, 189);
			mvarLightColorTable.CommandBarControlBorderSelected = Color.FromArgb(229, 195, 101);
			mvarLightColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(189, 189, 189);

			mvarLightColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(255, 232, 166);
			mvarLightColorTable.CommandBarControlBorderPressed = Color.FromArgb(229, 195, 101);

			mvarLightColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(255, 239, 187);
			mvarLightColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(189, 189, 189);

			mvarLightColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(189, 189, 189);
			mvarLightColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(189, 189, 189);
			mvarLightColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(189, 189, 189);

			mvarLightColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(255, 232, 166);
			// ColorTable.CommandBarControlBackgroundPressedGradientMiddle = Color.FromArgb(255, 232, 166);
			mvarLightColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(255, 232, 166);

			mvarLightColorTable.CommandBarBackground = Color.FromArgb(188, 189, 216);
			mvarLightColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(188, 199, 216);
			mvarLightColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(188, 199, 216);
			mvarLightColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(188, 199, 216);

			mvarLightColorTable.CommandBarDragHandle = Color.FromArgb(96, 114, 140);
			mvarLightColorTable.CommandBarDragHandleShadow = Color.Transparent;

			mvarLightColorTable.CommandBarMenuSplitterLine = Color.FromArgb(190, 195, 203);
			mvarLightColorTable.CommandBarMenuSplitterLineHighlight = Color.Transparent;

			mvarLightColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(133, 145, 162);
			mvarLightColorTable.CommandBarToolbarSplitterLineHighlight = Color.Transparent;


			mvarLightColorTable.CommandBarMenuControlText = ColorTable.CommandBarControlText;
			mvarLightColorTable.CommandBarMenuControlTextHighlight = ColorTable.CommandBarControlTextHover;
			mvarLightColorTable.CommandBarMenuControlTextPressed = ColorTable.CommandBarControlTextPressed;
			mvarLightColorTable.CommandBarMenuControlTextDisabled = ColorTable.CommandBarControlTextDisabled;

			mvarLightColorTable.ListViewItemHoverBackgroundGradientBegin = Color.FromArgb(255, 254, 249);
			mvarLightColorTable.ListViewItemHoverBackgroundGradientMiddle = Color.FromArgb(255, 236, 181);
			mvarLightColorTable.ListViewItemHoverBackgroundGradientEnd = Color.FromArgb(255, 237, 184);
			mvarLightColorTable.ListViewItemHoverBorder = Color.FromArgb(229, 195, 101);

			mvarLightColorTable.StatusBarBackground = System.Drawing.Color.FromArgb(41, 57, 85);
			mvarLightColorTable.StatusBarText = System.Drawing.Color.White;

			mvarLightColorTable.DocumentTabBackground = System.Drawing.Color.FromArgb(75, 94, 128);
			mvarLightColorTable.DocumentTabBorder = System.Drawing.Color.FromArgb(54, 78, 111);

			mvarLightColorTable.DocumentTabBorderHover = System.Drawing.Color.FromArgb(155, 167, 183);
			mvarLightColorTable.DocumentTabBackgroundHoverGradientBegin = System.Drawing.Color.FromArgb(111, 119, 118);
			mvarLightColorTable.DocumentTabBackgroundHoverGradientEnd = System.Drawing.Color.FromArgb(76, 92, 116);

			mvarLightColorTable.DocumentTabBorderSelected = Color.Transparent;
			mvarLightColorTable.DocumentTabBackgroundSelectedGradientBegin = Color.FromArgb(255, 252, 243);
			mvarLightColorTable.DocumentTabBackgroundSelectedGradientMiddle = Color.FromArgb(255, 243, 207);
			mvarLightColorTable.DocumentTabBackgroundSelectedGradientEnd = Color.FromArgb(255, 232, 166);

			mvarLightColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(255, 252, 242);
			mvarLightColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(255, 238, 186);
			mvarLightColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(255, 232, 166);

			mvarLightColorTable.DocumentTabInactiveBackgroundSelectedGradientBegin = Color.FromArgb(252, 252, 252);
			mvarLightColorTable.DocumentTabInactiveBackgroundSelectedGradientMiddle = Color.FromArgb(215, 220, 229);
			mvarLightColorTable.DocumentTabInactiveBackgroundSelectedGradientEnd = Color.FromArgb(206, 212, 223);

			mvarLightColorTable.DocumentTabText = Color.White; // Color.FromArgb(206, 212, 221);
			mvarLightColorTable.DocumentTabTextSelected = Color.Black;

			mvarLightColorTable.TextBoxBorderHover = Color.FromArgb(229, 195, 101);
			mvarLightColorTable.TextBoxBorderNormal = Color.FromArgb(133, 145, 162);
		}
		protected virtual void InitializeDarkColorTable()
		{
			mvarDarkColorTable.CommandBarPanelGradientBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarPanelGradientEnd = Color.FromArgb(45, 45, 48);
			// mvarDarkColorTable.CommandBarDropDownBackground = Color.FromArgb(51, 51, 55);
			// mvarDarkColorTable.CommandBarDropDownBorder = Color.FromArgb(67, 67, 70);
			// mvarDarkColorTable.TextBoxBackground = Color.FromArgb(30, 30, 30);
			mvarDarkColorTable.CommandBarDragHandle = Color.FromArgb(70, 70, 74);
			mvarDarkColorTable.CommandBarDragHandleShadow = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarMainMenuBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarGradientMenuBackgroundBegin = Color.FromArgb(27, 27, 28);
			mvarDarkColorTable.CommandBarGradientMenuBackgroundEnd = Color.FromArgb(27, 27, 28);

			mvarDarkColorTable.CommandBarMenuBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarMenuBorder = Color.FromArgb(51, 51, 55);

			mvarDarkColorTable.CommandBarGradientMenuBarBackgroundBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarGradientMenuBarBackgroundEnd = Color.FromArgb(45, 45, 48);

			mvarDarkColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(45, 45, 48);

			mvarDarkColorTable.CommandBarImageMarginBackground = Color.FromArgb(27, 27, 28);

			mvarDarkColorTable.CommandBarMenuSplitterLine = Color.FromArgb(51, 51, 55);
			// mvarDarkColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(51, 51, 55);

			mvarDarkColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(34, 34, 34);
			mvarDarkColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(70, 70, 74);

			mvarDarkColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarMenuIconBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.ToastGradientBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.ToastGradientEnd = Color.FromArgb(45, 45, 48);

			mvarDarkColorTable.CommandBarControlBackgroundHover = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(62, 62, 64);

			mvarDarkColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(27, 27, 28);
			mvarDarkColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(27, 27, 28);
			// mvarDarkColorTable.CommandBarControlBackgroundPressedGradientMiddle = Color.FromArgb(27, 27, 28);
			mvarDarkColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(27, 27, 28);

			mvarDarkColorTable.CommandBarMenuControlText = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.CommandBarMenuControlTextHighlight = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.CommandBarMenuControlTextPressed = Color.FromArgb(255, 255, 255);

			mvarDarkColorTable.CommandBarControlText = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.CommandBarControlTextHover = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.CommandBarControlTextPressed = Color.FromArgb(255, 255, 255);

			mvarDarkColorTable.WindowTitlebarForeground = Color.FromArgb(153, 153, 153);

			mvarDarkColorTable.StatusBarText = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.StatusBarBackground = Color.FromArgb(0, 122, 204);

			mvarDarkColorTable.ContentAreaBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.ContentAreaBackgroundDark = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.ContentAreaBackgroundLight = Color.FromArgb(45, 45, 48);

			mvarDarkColorTable.WindowBackground = Color.FromArgb(37, 37, 38);
			mvarDarkColorTable.WindowForeground = Color.FromArgb(255, 255, 255);

			// TODO: this should be Hover background, not Selected!
			mvarDarkColorTable.ListViewItemSelectedBackground = Color.FromArgb(63, 63, 64);
			mvarDarkColorTable.ListViewItemHoverBackgroundGradientBegin = Color.FromArgb(63, 63, 64);
			mvarDarkColorTable.ListViewItemHoverBackgroundGradientEnd = Color.FromArgb(63, 63, 64);
			mvarDarkColorTable.ListViewItemHoverBackgroundGradientMiddle = Color.FromArgb(63, 63, 64);

			mvarDarkColorTable.ListViewItemSelectedBackground = Color.FromArgb(51, 153, 255);
			mvarDarkColorTable.ListViewItemSelectedForeground = Color.FromArgb(255, 255, 255);

			mvarDarkColorTable.ListViewRangeSelectionBackground = Color.FromArgb(80, 27, 27, 28);
			mvarDarkColorTable.ListViewRangeSelectionBorder = Color.FromArgb(51, 51, 55);

			#region Docking Windows
			#region Tabs
			#region Active
			#region Normal
			mvarDarkColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(37, 37, 38);
			mvarDarkColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(37, 37, 38);
			mvarDarkColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(37, 37, 38);
			mvarDarkColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(14, 151, 221);
			#endregion
			#region Hover
			mvarDarkColorTable.DockingWindowActiveTabBackgroundHoverGradientBegin = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.DockingWindowActiveTabBackgroundHoverGradientMiddle = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.DockingWindowActiveTabBackgroundHoverGradientEnd = Color.FromArgb(62, 62, 64);
			mvarDarkColorTable.DockingWindowActiveTabTextHover = Color.FromArgb(77, 170, 228);
			#endregion
			#endregion

			mvarDarkColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DockingWindowInactiveTabText = Color.FromArgb(208, 208, 208);
			#endregion
			#region Titlebars
			mvarDarkColorTable.DockingWindowActiveTitlebarBackgroundGradientBegin = Color.FromArgb(0, 122, 204);
			mvarDarkColorTable.DockingWindowActiveTitlebarBackgroundGradientMiddle = Color.FromArgb(0, 122, 204);
			mvarDarkColorTable.DockingWindowActiveTitlebarBackgroundGradientEnd = Color.FromArgb(0, 122, 204);
			mvarDarkColorTable.DockingWindowInactiveTitlebarBackgroundGradientBegin = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DockingWindowInactiveTitlebarBackgroundGradientMiddle = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DockingWindowInactiveTitlebarBackgroundGradientEnd = Color.FromArgb(45, 45, 48);
			#endregion
			#endregion
			#region Document Tabs
			mvarDarkColorTable.DocumentTabBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DocumentTabBackgroundHover = Color.FromArgb(28, 151, 234);
			mvarDarkColorTable.DocumentTabBackgroundSelected = Color.FromArgb(0, 122, 204);
			mvarDarkColorTable.DocumentTabText = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.DocumentTabTextHover = Color.FromArgb(255, 255, 255);
			mvarDarkColorTable.DocumentTabTextSelected = Color.FromArgb(255, 255, 255);

			FontTable.DocumentTabTextSelected = new Font(FontTable.Default, FontStyle.Bold);
			#endregion
			#region Tab Switcher
			mvarDarkColorTable.DocumentSwitcherBackground = Color.FromArgb(45, 45, 48);
			mvarDarkColorTable.DocumentSwitcherBorder = Color.FromArgb(63, 63, 70);
			mvarDarkColorTable.DocumentSwitcherText = Color.FromArgb(255, 255, 255);

			mvarDarkColorTable.DocumentSwitcherSelectionBackground = Color.FromArgb(51, 153, 255);
			mvarDarkColorTable.DocumentSwitcherSelectionBorder = Color.FromArgb(63, 63, 70);
			mvarDarkColorTable.DocumentSwitcherSelectionText = Color.FromArgb(255, 255, 255);
			#endregion

			#region DropDown
			{
				mvarDarkColorTable.DropDownBackgroundColorNormal = Color.FromArgb(51, 51, 55);
				mvarDarkColorTable.DropDownForegroundColorNormal = Color.FromArgb(241, 241, 241);
				mvarDarkColorTable.DropDownBorderColorNormal = Color.FromArgb(67, 67, 70);

				mvarDarkColorTable.DropDownBackgroundColorHover = Color.FromArgb(63, 63, 70);
				mvarDarkColorTable.DropDownForegroundColorHover = Color.FromArgb(255, 255, 255);
				mvarDarkColorTable.DropDownBorderColorHover = Color.FromArgb(67, 67, 70);

				mvarDarkColorTable.DropDownBackgroundColorPressed = Color.FromArgb(63, 63, 70); // Color.FromKnownColor(KnownColor.Highlight);
				mvarDarkColorTable.DropDownForegroundColorPressed = Color.FromArgb(255, 255, 255); // Color.FromKnownColor(KnownColor.HighlightText);
				mvarDarkColorTable.DropDownBorderColorPressed = Color.FromArgb(51, 51, 55);

				mvarDarkColorTable.DropDownMenuBackground = Color.FromArgb(37, 37, 38);
				mvarDarkColorTable.DropDownMenuBorder = Color.FromArgb(100, 100, 100);
			}
			#endregion
			#region ListView
			{
				mvarDarkColorTable.ListViewColumnHeaderBackgroundNormal = Color.FromArgb(37, 37, 38);
				mvarDarkColorTable.ListViewColumnHeaderBackgroundHover = Color.FromArgb(62, 62, 64);
				mvarDarkColorTable.ListViewColumnHeaderBackgroundSelected = Color.FromArgb(0, 122, 204);
				mvarDarkColorTable.ListViewColumnHeaderForegroundNormal = Color.FromArgb(255, 255, 255);
				mvarDarkColorTable.ListViewColumnHeaderForegroundHover = Color.FromArgb(255, 255, 255);
				mvarDarkColorTable.ListViewColumnHeaderForegroundSelected = Color.FromArgb(255, 255, 255);
				mvarDarkColorTable.ListViewColumnHeaderBorder = Color.FromArgb(63, 63, 70);
				mvarDarkColorTable.ListViewColumnHeaderArrowNormal = Color.FromArgb(241, 241, 241);
			}
			#endregion

			#region Property Grid
			{
				mvarDarkColorTable.PropertyGridBackgroundColor = Color.FromArgb(37, 37, 38);
				mvarDarkColorTable.PropertyGridBorderColor = Color.FromArgb(45, 45, 48);
				mvarDarkColorTable.PropertyGridDisabledForegroundColor = Color.FromArgb(153, 153, 153);
				mvarDarkColorTable.PropertyGridForegroundColor = Color.FromArgb(241, 241, 241);
				mvarDarkColorTable.PropertyGridItemHighlightBackgroundColor = Color.FromArgb(51, 153, 255);
				mvarDarkColorTable.PropertyGridItemHighlightForegroundColor = Color.FromArgb(255, 255, 255);
			}
			#endregion

			#region ToolTip
			mvarDarkColorTable.ToolTipBackgroundGradientBegin = Color.FromArgb(66, 66, 69);
			mvarDarkColorTable.ToolTipBackgroundGradientMiddle = Color.FromArgb(66, 66, 69);
			mvarDarkColorTable.ToolTipBackgroundGradientEnd = Color.FromArgb(66, 66, 69);
			mvarDarkColorTable.ToolTipBorder = Color.FromArgb(77, 77, 80);
			#endregion
		}
		protected virtual void InitializeBlueColorTable()
		{
			mvarBlueColorTable.CommandBarPanelGradientBegin = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarPanelGradientEnd = Color.FromArgb(214, 219, 233);
			// mvarBlueColorTable.CommandBarDropDownBackground = Color.FromArgb(51, 51, 55);
			// mvarBlueColorTable.CommandBarDropDownBorder = Color.FromArgb(67, 67, 70);
			// mvarBlueColorTable.TextBoxBackground = Color.FromArgb(30, 30, 30);
			mvarBlueColorTable.CommandBarDragHandle = Color.FromArgb(96, 114, 140);
			mvarBlueColorTable.CommandBarDragHandleShadow = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarMainMenuBackground = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarGradientMenuBackgroundBegin = Color.FromArgb(233, 236, 238);
			mvarBlueColorTable.CommandBarGradientMenuBackgroundEnd = Color.FromArgb(208, 215, 226);

			mvarBlueColorTable.CommandBarMenuBorder = Color.FromArgb(155, 167, 183);

			mvarBlueColorTable.CommandBarGradientMenuBarBackgroundBegin = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarGradientMenuBarBackgroundEnd = Color.FromArgb(214, 219, 233);

			mvarBlueColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(214, 219, 233);
			mvarBlueColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(214, 219, 233);

			mvarBlueColorTable.CommandBarImageMarginBackground = Color.FromArgb(233, 236, 238);

			mvarBlueColorTable.CommandBarMenuSplitterLine = Color.FromArgb(190, 195, 203);
			// mvarBlueColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(51, 51, 55);

			mvarBlueColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(133, 145, 162);
			mvarBlueColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(214, 219, 233);

			// ### FINISH

			mvarBlueColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.CommandBarMenuIconBackground = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.ToastGradientBegin = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.ToastGradientEnd = Color.FromArgb(45, 45, 48);

			mvarBlueColorTable.CommandBarControlBackgroundHover = Color.FromArgb(62, 62, 64);
			mvarBlueColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(62, 62, 64);
			mvarBlueColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(62, 62, 64);
			mvarBlueColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(62, 62, 64);

			mvarBlueColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(27, 27, 28);
			mvarBlueColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(27, 27, 28);
			// mvarDarkColorTable.CommandBarControlBackgroundPressedGradientMiddle = Color.FromArgb(27, 27, 28);
			mvarBlueColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(27, 27, 28);

			mvarBlueColorTable.CommandBarMenuControlText = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.CommandBarMenuControlTextHighlight = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.CommandBarMenuControlTextPressed = Color.FromArgb(255, 255, 255);

			mvarBlueColorTable.CommandBarControlText = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.CommandBarControlTextHover = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.CommandBarControlTextPressed = Color.FromArgb(255, 255, 255);

			mvarBlueColorTable.WindowTitlebarForeground = Color.FromArgb(153, 153, 153);

			mvarBlueColorTable.StatusBarText = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.StatusBarBackground = Color.FromArgb(0, 122, 204);

			mvarBlueColorTable.ContentAreaBackground = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.ContentAreaBackgroundDark = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.ContentAreaBackgroundLight = Color.FromArgb(45, 45, 48);

			mvarBlueColorTable.WindowBackground = Color.FromArgb(37, 37, 38);
			mvarBlueColorTable.WindowForeground = Color.FromArgb(255, 255, 255);

			// TODO: this should be Hover background, not Selected!
			mvarBlueColorTable.ListViewItemSelectedBackground = Color.FromArgb(63, 63, 64);
			mvarBlueColorTable.ListViewItemHoverBackgroundGradientBegin = Color.FromArgb(63, 63, 64);
			mvarBlueColorTable.ListViewItemHoverBackgroundGradientEnd = Color.FromArgb(63, 63, 64);
			mvarBlueColorTable.ListViewItemHoverBackgroundGradientMiddle = Color.FromArgb(63, 63, 64);

			mvarBlueColorTable.ListViewItemSelectedBackground = Color.FromArgb(51, 153, 255);
			mvarBlueColorTable.ListViewItemSelectedForeground = Color.FromArgb(255, 255, 255);

			mvarBlueColorTable.ListViewRangeSelectionBackground = Color.FromArgb(80, 27, 27, 28);
			mvarBlueColorTable.ListViewRangeSelectionBorder = Color.FromArgb(51, 51, 55);

			#region Docking Windows
			#region Tabs
			mvarBlueColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(37, 37, 38);
			mvarBlueColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(37, 37, 38);
			mvarBlueColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(37, 37, 38);
			mvarBlueColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(14, 151, 221);

			mvarBlueColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DockingWindowInactiveTabText = Color.FromArgb(208, 208, 208);
			#endregion
			#region Titlebars
			mvarBlueColorTable.DockingWindowActiveTitlebarBackgroundGradientBegin = Color.FromArgb(0, 122, 204);
			mvarBlueColorTable.DockingWindowActiveTitlebarBackgroundGradientMiddle = Color.FromArgb(0, 122, 204);
			mvarBlueColorTable.DockingWindowActiveTitlebarBackgroundGradientEnd = Color.FromArgb(0, 122, 204);
			mvarBlueColorTable.DockingWindowInactiveTitlebarBackgroundGradientBegin = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DockingWindowInactiveTitlebarBackgroundGradientMiddle = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DockingWindowInactiveTitlebarBackgroundGradientEnd = Color.FromArgb(45, 45, 48);
			#endregion
			#endregion
			#region Document Tabs
			mvarBlueColorTable.DocumentTabBackground = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DocumentTabBackgroundHover = Color.FromArgb(28, 151, 234);
			mvarBlueColorTable.DocumentTabBackgroundSelected = Color.FromArgb(0, 122, 204);
			mvarBlueColorTable.DocumentTabText = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.DocumentTabTextHover = Color.FromArgb(255, 255, 255);
			mvarBlueColorTable.DocumentTabTextSelected = Color.FromArgb(255, 255, 255);

			FontTable.DocumentTabTextSelected = new Font(FontTable.Default, FontStyle.Bold);
			#endregion
			#region Tab Switcher
			mvarBlueColorTable.DocumentSwitcherBackground = Color.FromArgb(45, 45, 48);
			mvarBlueColorTable.DocumentSwitcherBorder = Color.FromArgb(63, 63, 70);
			mvarBlueColorTable.DocumentSwitcherText = Color.FromArgb(255, 255, 255);

			mvarBlueColorTable.DocumentSwitcherSelectionBackground = Color.FromArgb(51, 153, 255);
			mvarBlueColorTable.DocumentSwitcherSelectionBorder = Color.FromArgb(63, 63, 70);
			mvarBlueColorTable.DocumentSwitcherSelectionText = Color.FromArgb(255, 255, 255);
			#endregion
		}

		protected override void InitCommonColors()
		{
			base.InitCommonColors();

			InitializeLightColorTable();
			InitializeDarkColorTable();
			InitializeBlueColorTable();
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
			Rectangle rect = new Rectangle(new Point(0, 0), rectangle.Size);
			if (parent is MenuStrip)
			{
				LinearGradientBrush brush = new LinearGradientBrush(rect, ColorTable.CommandBarGradientMenuBarBackgroundBegin, ColorTable.CommandBarGradientMenuBarBackgroundEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(brush, rect);
				return;
			}
			else if (parent is ToolStripDropDown)
			{
				LinearGradientBrush brush = new LinearGradientBrush(rect, ColorTable.CommandBarGradientMenuBackgroundBegin, ColorTable.CommandBarGradientMenuBackgroundEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(brush, rect);
				graphics.DrawRectangle(new Pen(Color.FromArgb(155, 167, 183)), rect);
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

		public override void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted)
		{
			// draws the column header background, should this be called DrawColumnHeaderBackground?
			switch (state)
			{
				case ControlState.Normal:
				{
					g.FillRectangle(new SolidBrush(ColorTable.ListViewColumnHeaderBackgroundNormal), rect);
					break;
				}
				case ControlState.Hover:
				{
					g.FillRectangle(new SolidBrush(ColorTable.ListViewColumnHeaderBackgroundHover), rect);
					break;
				}
				case ControlState.Pressed:
				{
					g.FillRectangle(new SolidBrush(ColorTable.ListViewColumnHeaderBackgroundSelected), rect);
					break;
				}
			}
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
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuBorder), rect);
			}
			else if (item.Selected || (item.Pressed && item.IsOnDropDown))
			{
				/*
				if ((item.IsOnDropDown && (item is AwesomeControls.CommandBars.CBMenuItem) && (item as AwesomeControls.CommandBars.CBMenuItem).Hidden))
				{

				}
				else
				*/			
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

					graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), r);
				}
			}
			/*
			else if (item.IsOnDropDown && (item is AwesomeControls.CommandBars.CBMenuItem) && (item as AwesomeControls.CommandBars.CBMenuItem).Hidden)
			{
				Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height);
				Rectangle rectFill = rect;
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rectFill);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuBorder), rect);
			}
			*/
		}

		#region DockPanel
		#region Document Tabs
		public override void DrawDocumentTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
			Color BackColor = ColorTable.DocumentTabBackground;
			if (selected)
			{
				BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackgroundSelected;
			}
			else if (controlState == ControlState.Hover)
			{
				BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackgroundHover;
			}
			else
			{
				BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackground;
			}

			Brush brushFill = new SolidBrush(BackColor);
			Pen penLine = new System.Drawing.Pen(ColorTable.DocumentTabBorder);

			g.FillRectangle(brushFill, rectTab);
			/*
			if (selected)
			{
				if (focused)
				{
					DrawingTools.FillWithShinyGradient(g, ColorTable.DocumentTabInactiveBackgroundSelectedGradientBegin, ColorTable.DocumentTabInactiveBackgroundSelectedGradientMiddle, ColorTable.DocumentTabInactiveBackgroundSelectedGradientEnd, rectTab, (int)(rectTab.Height / 2), (int)((rectTab.Height / 2) + 1), LinearGradientMode.Horizontal);
					g.DrawRectangle(new System.Drawing.Pen(ColorTable.DocumentTabInactiveBorderSelected), rectTab);
				}
				else
				{
					DrawingTools.FillWithShinyGradient(g, ColorTable.DocumentTabBackgroundSelectedGradientBegin, ColorTable.DocumentTabBackgroundSelectedGradientMiddle, ColorTable.DocumentTabBackgroundSelectedGradientEnd, rectTab, (int)(rectTab.Height / 2), (int)((rectTab.Height / 2) + 1), LinearGradientMode.Horizontal);
					g.DrawRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderSelected), rectTab);
				}
				return;
			}

			switch (position)
			{
				case DockingWindows.DockPosition.Left:
				{
					switch (controlState)
					{
						case ControlState.Normal:
						{
							g.FillRectangle(brushFill, rectTab);
							g.DrawRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorder), rectTab);
							break;
						}
						case ControlState.Hover:
						{
							g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DocumentTabBackgroundHoverGradientBegin), rectTab);
							g.DrawRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderHover), rectTab);
							break;
						}
					}
					break;
				}
				case DockingWindows.DockPosition.Center:
				{
					switch (controlState)
					{
						case ControlState.Hover:
						{
							g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DocumentTabBackgroundHoverGradientBegin), rectTab);
							g.DrawRectangle(new System.Drawing.Pen(ColorTable.DocumentTabBorderHover), rectTab);
							break;
						}
					}
					break;
				}
			}
			 */
		}
		public override void DrawDocumentSwitcherBackground(Graphics graphics, Rectangle rectangle)
		{
			graphics.FillRectangle(new SolidBrush(ColorTable.DocumentSwitcherBackground), rectangle);
			graphics.DrawRoundedRectangle(new Pen(ColorTable.DocumentSwitcherBorder), rectangle, 3);
		}
		#endregion
		public override void DrawDockPanelTitleBarBackground(Graphics g, Rectangle rect, bool focused)
		{
			if (focused)
			{
				DrawingTools.FillWithDoubleGradient(ColorTable.DockingWindowActiveTitlebarBackgroundGradientBegin, ColorTable.DockingWindowActiveTitlebarBackgroundGradientMiddle, ColorTable.DockingWindowActiveTitlebarBackgroundGradientEnd, g, rect, rect.Height / 2, rect.Height / 2, LinearGradientMode.Horizontal, false);
			}
			else
			{
				g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DockingWindowInactiveTitlebarBackgroundGradientBegin), rect);
			}
		}
		public override void DrawDockPanelTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
			if (selected)
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb(37, 37, 38)), rectTab);
			}
			else
			{
				switch (controlState)
				{
					case ControlState.Normal:
					{
						g.FillRectangle(new SolidBrush(Color.FromArgb(45, 45, 48)), rectTab);
						break;
					}
					case ControlState.Hover:
					{
						g.FillRectangle(new SolidBrush(Color.FromArgb(62, 62, 64)), rectTab);
						break;
					}
				}
			}
		}
		#endregion
		#region Accordion
		public override void DrawAccordionBackground(Graphics graphics, Rectangle rectangle)
		{
			graphics.FillRectangle(new SolidBrush(ColorTable.WindowBackground), rectangle);
			
		}
		#endregion

		private bool mvarUseAllCapsMenus = true;
		public bool UseAllCapsMenus { get { return mvarUseAllCapsMenus; } set { mvarUseAllCapsMenus = value; } }

		public void SetStatusBarState(StatusBarState value)
		{
			switch (value)
			{
				case StatusBarState.Initial:
				{
					ColorTable.StatusBarBackground = Color.FromArgb(104, 33, 122);
					break;
				}
				case StatusBarState.Building:
				{
					ColorTable.StatusBarBackground = Color.FromArgb(14, 99, 156);
					break;
				}
				case StatusBarState.SolutionLoaded:
				{
					ColorTable.StatusBarBackground = Color.FromArgb(0, 122, 204);
					break;
				}
				case StatusBarState.Debugging:
				{
					ColorTable.StatusBarBackground = Color.FromArgb(202, 81, 0);
					break;
				}
			}
		}

		public override void DrawText(Graphics graphics, string text, Color color, Font font, Rectangle textRectangle, TextFormatFlags textFormat, ToolStripTextDirection textDirection, ToolStripItem item)
		{
			if (mvarUseAllCapsMenus && (item is ToolStripMenuItem && !item.IsOnDropDown))
			{
				text = text.ToUpper();
			}
			base.DrawText(graphics, text, color, font, textRectangle, textFormat, textDirection, item);
		}

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
			
			g.DrawRectangle(new Pen(BorderColor), rect);
		}
		#endregion
		#endregion
	}
}
