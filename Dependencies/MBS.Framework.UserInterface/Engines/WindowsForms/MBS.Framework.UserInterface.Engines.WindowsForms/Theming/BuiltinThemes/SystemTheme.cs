using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Drawing.Drawing2D;
using MBS.Framework.UserInterface.Theming;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class SystemTheme : ClassicTheme
	{
		private static class VisualStyleRenderers
		{
			public static class Button
			{
				public static class PushButton
				{
					private static VisualStyleRenderer _Normal = null;
					public static VisualStyleRenderer Normal
					{
						get
						{ 
							if (_Normal == null)
							{
								_Normal = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
							}
							return _Normal;
						}
					}
					private static VisualStyleRenderer _Hot = null;
					public static VisualStyleRenderer Hot
					{
						get
						{
							if (_Hot == null)
							{
								_Hot = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot);
							}
							return _Hot;
						}
					}
					private static VisualStyleRenderer _Pressed = null;
					public static VisualStyleRenderer Pressed
					{
						get
						{
							if (_Pressed == null)
							{
								_Pressed = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed);
							}
							return _Pressed;
						}
					}
				}
			}
			public static class TreeView
			{
				public static class Glyph
				{
					public static class Opened
					{
						private static VisualStyleRenderer _Normal = null;
						public static VisualStyleRenderer Normal
						{
							get
							{
								if (_Normal == null)
								{
									if (System.Environment.OSVersion.Platform == PlatformID.Win32NT && System.Environment.OSVersion.Version.Major >= 6)
									{
										_Normal = new VisualStyleRenderer("Explorer::TreeView", 2, 2);
									}
									else
									{
										_Normal = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
									}
								}
								return _Normal;
							}
						}
					}
					public static class Closed
					{
						private static VisualStyleRenderer _Normal = null;
						public static VisualStyleRenderer Normal
						{
							get
							{
								if (_Normal == null)
								{
									if (System.Environment.OSVersion.Platform == PlatformID.Win32NT && System.Environment.OSVersion.Version.Major >= 6)
									{
										_Normal = new VisualStyleRenderer("Explorer::TreeView", 2, 1);
									}
									else
									{
										_Normal = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
									}
								}
								return _Normal;
							}
						}
					}
				}
			}
			public static class ListView
			{
				public static class Item
				{
					private static VisualStyleRenderer _Normal = null;
					public static VisualStyleRenderer Normal
					{
						get
						{
							if (_Normal == null)
							{
								_Normal = new VisualStyleRenderer("Explorer::ListView", 1, 1);
							}
							return _Normal;
						}
					}
					private static VisualStyleRenderer _Hot = null;
					public static VisualStyleRenderer Hot
					{
						get
						{
							if (_Hot == null)
							{
								_Hot = new VisualStyleRenderer("Explorer::ListView", 1, 2);
							}
							return _Hot;
						}
					}
					private static VisualStyleRenderer _SelectedFocused = null;
					public static VisualStyleRenderer SelectedFocused
					{
						get
						{
							if (_SelectedFocused == null)
							{
								_SelectedFocused = new VisualStyleRenderer("Explorer::ListView", 1, 3);
							}
							return _SelectedFocused;
						}
					}
					private static VisualStyleRenderer _SelectedHot = null;
					public static VisualStyleRenderer SelectedHot
					{
						get
						{
							if (_SelectedHot == null)
							{
								_SelectedHot = new VisualStyleRenderer("Explorer::ListView", 1, 6);
							}
							return _SelectedHot;
						}
					}

					private static VisualStyleRenderer _SelectedUnfocused = null;
					public static VisualStyleRenderer SelectedUnfocused
					{
						get
						{
							if (_SelectedUnfocused == null)
							{
								_SelectedUnfocused = new VisualStyleRenderer("Explorer::ListView", 1, 5);
							}
							return _SelectedUnfocused;
						}
					}
				}
			}
		}

		protected override void InitCommonColors ()
		{
			base.InitCommonColors();
			ColorTable.ListViewItemSelectedForeground = Color.FromKnownColor(KnownColor.HighlightText);
			ColorTable.ListViewItemSelectedForeground = Color.FromKnownColor(KnownColor.ControlText);

			ColorTable.CommandBarToolbarSplitterLine = Color.FromKnownColor(KnownColor.ControlDark);
			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromKnownColor(KnownColor.ControlLight);
		}
		protected override void InitBlueLunaColors()
		{
			base.InitBlueLunaColors();
			
			ColorTable.CommandBarControlTextHover = ColorTable.CommandBarControlText;
			ColorTable.CommandBarMenuControlTextHighlight = Color.FromKnownColor(KnownColor.HighlightText);
			ColorTable.CommandBarMenuControlTextPressed = Color.FromKnownColor(KnownColor.HighlightText);
		}

		protected override void InitAeroColors()
		{
			base.InitAeroColors();

			ColorTable.CommandBarControlText = System.Drawing.Color.FromKnownColor(KnownColor.ControlText);
			ColorTable.CommandBarControlTextHover = ColorTable.CommandBarControlText;
			Console.WriteLine("CommandBarControlTextHover: {0}", ColorTable.CommandBarControlTextHover);

			ColorTable.CommandBarMenuControlTextHighlight = ColorTable.CommandBarControlText;
			ColorTable.CommandBarControlTextPressed = ColorTable.CommandBarControlText;
			ColorTable.CommandBarMenuControlTextPressed = ColorTable.CommandBarControlText;

			ColorTable.ListViewItemSelectedForeground = Color.FromKnownColor(KnownColor.ControlText);
		}

		public override void DrawGrip(Graphics graphics, Rectangle gripBounds, Orientation orientation, bool rtl)
		{
			switch (orientation)
			{
				case Orientation.Horizontal:
				{
					VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Rebar.GripperVertical.Normal);
					vsr.DrawBackground(graphics, gripBounds);
					break;
				}
				case Orientation.Vertical:
				{
					VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Rebar.Gripper.Normal);
					vsr.DrawBackground(graphics, gripBounds);
					break;
				}
			}
		}

		public override void DrawContentAreaBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle)
		{
			graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.AppWorkspace)), rectangle);
		}
		public override void DrawCommandBarPanelBackground (System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripPanel toolStripPanel)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawCommandBarPanelBackground(graphics, toolStripPanel);
				return;
			}

			if (System.Environment.OSVersion.Platform == PlatformID.Win32NT ||
				System.Environment.OSVersion.Platform == PlatformID.Win32S ||
				System.Environment.OSVersion.Platform == PlatformID.Win32Windows ||
				System.Environment.OSVersion.Platform == PlatformID.WinCE)
			{
				if (System.Environment.OSVersion.Version.Major > 5)
				{
					// Windows Vista changed the names of the Visual Style classes, and they're not supported
					// by .NET VisualStyleRenderer either
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 7, 1));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, toolStripPanel.Width, toolStripPanel.Height));
				}
				else
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("REBAR", 0, 0));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, toolStripPanel.Width, toolStripPanel.Height));
				}
			}
			else
			{
				base.DrawCommandBarPanelBackground(graphics, toolStripPanel);
				return;
			}
		}
		public override void DrawCommandBarBackground (System.Drawing.Graphics graphics, System.Drawing.Rectangle rectangle, Orientation orientation, System.Windows.Forms.ToolStrip parent)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawCommandBarBackground(graphics, rectangle, orientation, parent);
				return;
			}

			if (Environment.OSVersion.Platform == PlatformID.Win32NT ||
				Environment.OSVersion.Platform == PlatformID.Win32S ||
				Environment.OSVersion.Platform == PlatformID.Win32Windows ||
				Environment.OSVersion.Platform == PlatformID.WinCE)
			{
				if (Environment.OSVersion.Version.Major > 5)
				{
					if (parent is System.Windows.Forms.ToolStripDropDownMenu)
					{
						// Windows Vista changed the names of the Visual Style classes, and they're not supported
						// by .NET VisualStyleRenderer either
						VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 9, 1));
						renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, parent.Width, parent.Height));
					}
					else if (!(parent is System.Windows.Forms.ToolStripDropDownMenu))
					{
						VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 7, 1));
						renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, parent.Width, parent.Height));
					}
				}
				else if (!(parent is System.Windows.Forms.ToolStripDropDownMenu))
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("REBAR", 0, 0));
					int w = parent.Width, h = parent.Height;
					if (parent.Parent != null)
					{
						w = parent.Parent.Width;
						h = parent.Parent.Height;
					}
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(-parent.Left, -parent.Top, w, h));
				}
			}
			
			if (parent is System.Windows.Forms.ToolStripDropDownMenu)
			{
				graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Menu)), new Rectangle(0, 0, parent.Bounds.Width, parent.Bounds.Height));
			}
			else
			{
				base.DrawCommandBarBackground(graphics, rectangle, orientation, parent);
				return;
			}
		}
		public override void DrawCommandBarBorder (System.Drawing.Graphics graphics, System.Windows.Forms.ToolStrip toolStrip, System.Drawing.Rectangle connectedArea)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawCommandBarBorder(graphics, toolStrip, connectedArea);
				return;
			}
			// NOTE: Hasn't been tested on versions 5!
			if (Environment.OSVersion.Version.Major > 5)
			{
				if (toolStrip is System.Windows.Forms.ToolStripDropDownMenu)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 10, 1));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, toolStrip.Width - 1, 1));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, 1, toolStrip.Height));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, toolStrip.Height - 1, toolStrip.Width, toolStrip.Height - 1));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(toolStrip.Width - 1, 0, toolStrip.Width - 1, toolStrip.Height - 1));
				}
			}
			else
			{
				base.DrawCommandBarBorder(graphics, toolStrip, connectedArea);
				return;
			}
		}
		public override void DrawCommandButtonBackground (System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawCommandButtonBackground(graphics, item, parent);
				return;
			}
			if (Environment.OSVersion.Version.Major > 4)
			{
				// Not tested on versions below 4
				Rectangle rect = new System.Drawing.Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height);
				if (item.Pressed || (item is System.Windows.Forms.ToolStripButton && (item as System.Windows.Forms.ToolStripButton).Checked))
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 3));
					renderer.DrawBackground(graphics, rect);
				}
				else if (item.Selected)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 2));
					renderer.DrawBackground(graphics, rect);
				}
			}
		}
		public override void DrawImageMargin (System.Drawing.Graphics graphics, System.Drawing.Rectangle affectedBounds, System.Windows.Forms.ToolStrip toolStrip)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawImageMargin(graphics, affectedBounds, toolStrip);
				return;
			}
			// NOTE: Hasn't been tested on versions 5!
			if (Environment.OSVersion.Version.Major > 5)
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 13, 1));
				renderer.DrawBackground(graphics, new System.Drawing.Rectangle(affectedBounds.Left + 6, affectedBounds.Top, affectedBounds.Width, affectedBounds.Height));
			}
			else
			{
				base.DrawImageMargin(graphics, affectedBounds, toolStrip);
				return;
			}
		}
		public override void DrawMenuItemBackground (System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawMenuItemBackground(graphics, item);
				return;
			}
			// NOTE: Hasn't been tested on versions 5!
			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height);
			if (item.IsOnDropDown)
			{
				rect.X += 2;
				rect.Width -= 3;
			}

			if (((Environment.OSVersion.Platform == PlatformID.Win32NT)
				|| (Environment.OSVersion.Platform == PlatformID.Win32S)
				|| (Environment.OSVersion.Platform == PlatformID.Win32Windows)
				|| (Environment.OSVersion.Platform == PlatformID.WinCE))
				&& Environment.OSVersion.Version.Major == 5)
			{
				rect.Y -= 0;
				rect.Height--;
			}

			if (Environment.OSVersion.Version.Major > 4)
			{
				if (item.Pressed && !item.IsOnDropDown)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 3));
					renderer.DrawBackground(graphics, rect);
				}
				else if (item.Selected)
				{
					if (item.IsOnDropDown)
					{
						if (Environment.OSVersion.Version.Major > 5)
						{
							VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("MENU", 14, 2));
							renderer.DrawBackground(graphics, rect);
						}
						else
						{
							base.DrawMenuItemBackground(graphics, item);
							return;
						}
					}
					else
					{
						VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 2));
						renderer.DrawBackground(graphics, rect);
					}
				}
			}
		}

		public override void DrawCheck(Graphics graphics, System.Windows.Forms.ToolStripItem item, Rectangle imageRectangle)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawCheck(graphics, item, imageRectangle);
				return;
			}

			Rectangle checkRect = new Rectangle(2, 0, item.Height, item.Height);
			
			if (item is System.Windows.Forms.ToolStripMenuItem)
			{
				System.Windows.Forms.ToolStripMenuItem tsmi = (item as System.Windows.Forms.ToolStripMenuItem);

				if (tsmi.Checked)
				{
					if (tsmi.Selected)
					{
						VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 6));
						renderer.DrawBackground(graphics, checkRect);
					}
					else
					{
						VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 2, 5));
						renderer.DrawBackground(graphics, checkRect);
					}
				}
			}

			if (item.Image == null)
			{
				Rectangle checkMarkRect = checkRect;
				checkMarkRect.Offset(8, 6);
				DrawingTools.DrawCheckMark(graphics, Pens.Black, checkMarkRect);
			}
		}

		public override void DrawDropDownButtonBackground (Graphics graphics, System.Windows.Forms.ToolStripDropDownButton item, System.Windows.Forms.ToolStrip parent)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawDropDownButtonBackground (graphics, item, parent);
				return;
			}
			// NOTE: Hasn't been tested on versions 5!
			if (Environment.OSVersion.Version.Major > 4)
			{
				if (item.Selected)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 2, 2));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, item.Width, item.Height));
				}
			}
		}
		public override void DrawSplitButtonBackground (System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawSplitButtonBackground (graphics, item, parent);
				return;
			}
			// NOTE: Hasn't been tested on versions 5!
			if (Environment.OSVersion.Version.Major > 4)
			{
				if ((item as System.Windows.Forms.ToolStripSplitButton).ButtonPressed)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 3, 3));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, item.Width - 12, item.Height));

					renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 4, 2));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(item.Width - 12, 0, 12, item.Height));
				}
				else if ((item as System.Windows.Forms.ToolStripSplitButton).DropDownButtonPressed)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 1, 3));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, item.Width - 12, item.Height));

					renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 4, 3));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(item.Width - 12, 0, 12, item.Height));
				}
				else if (item.Selected)
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 3, 2));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(0, 0, item.Width - 12, item.Height));

					renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 4, 2));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(item.Width - 12, 0, 12, item.Height));
				}
				else
				{
					VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TOOLBAR", 4, 1));
					renderer.DrawBackground(graphics, new System.Drawing.Rectangle(item.Width - 12, 0, 12, item.Height));
				}
			}
		}
		public override void DrawText (System.Drawing.Graphics graphics, string text, System.Drawing.Color color, System.Drawing.Font font, System.Drawing.Rectangle textRectangle, System.Windows.Forms.TextFormatFlags textFormat, System.Windows.Forms.ToolStripTextDirection textDirection, System.Windows.Forms.ToolStripItem item)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawText(graphics, text, color, font, textRectangle, textFormat, textDirection, item);
				return;
			}

			if (item.Pressed && !item.IsOnDropDown)
			{
				textRectangle.Offset(1, 1);
			}
			if (((System.Environment.OSVersion.Platform == PlatformID.Win32NT)
				|| (System.Environment.OSVersion.Platform == PlatformID.Win32S)
				|| (System.Environment.OSVersion.Platform == PlatformID.Win32Windows)
				|| (System.Environment.OSVersion.Platform == PlatformID.WinCE)
				) && (System.Environment.OSVersion.Version.Major == 5))
			{
				if (!item.IsOnDropDown)
				{
					textRectangle.Offset(0, -1);
				}
			}

			base.DrawText(graphics, text, color, font, textRectangle, textFormat, textDirection, item);
		}
		
		#region DockPanel
		public override void DrawDocumentTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
			Brush brushFill = new SolidBrush(ColorTable.DocumentTabBackground);
			Pen penLine = new System.Drawing.Pen(ColorTable.DocumentTabBorder);

			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawDocumentTabBackground(g, rectTab, controlState, position, selected, focused);
				return;
			}

			switch (position)
			{
				case MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement.Left:
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
				case MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement.Center:
				{
					if (selected)
					{
						VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Tab.TopTabItem.Normal);
						vsr.DrawBackground(g, rectTab);
					}
					else
					{
						switch (controlState)
						{
							case ControlState.Hover:
							{
								VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Hot);
								vsr.DrawBackground(g, rectTab);
								break;
							}
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
				g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin), rect);
			}
			else
			{
				g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DockingWindowInactiveTabBackgroundGradientBegin), rect);
			}
		}
		#endregion

		#region System Controls
		#region Button
		public override void DrawButtonBackground(Graphics g, Rectangle rect, ControlState state)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawButtonBackground(g, rect, state);
				return;
			}

			switch (state)
			{
				case ControlState.Normal:
				{
					VisualStyleRenderers.Button.PushButton.Normal.DrawBackground(g, rect);
					break;
				}
				case ControlState.Hover:
				{
					VisualStyleRenderers.Button.PushButton.Hot.DrawBackground(g, rect);
					break;
				}
				case ControlState.Pressed:
				{
					VisualStyleRenderers.Button.PushButton.Pressed.DrawBackground(g, rect);
					break;
				}
			}
		}
		#endregion
		#region TextBox
		public override void DrawTextBoxBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state)
		{
			if (System.Windows.Forms.TextBoxRenderer.IsSupported)
			{
				TextBoxState tbstate = TextBoxState.Normal;
				switch (state)
				{
					case ControlState.Normal: tbstate = TextBoxState.Normal; break;
					case ControlState.Hover: tbstate = TextBoxState.Hot; break;
					case ControlState.Pressed: tbstate = TextBoxState.Normal; break;
					case ControlState.Disabled: tbstate = TextBoxState.Disabled; break;
				}
				System.Windows.Forms.TextBoxRenderer.DrawTextBox(g, rect, tbstate);
			}
			else
			{
				base.DrawTextBoxBackground(g, rect, state);
			}
		}
		#endregion
		#region ListView
		public override void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawListColumnBackground(g, rect, state, sorted);
				return;
			}

			switch (state)
			{
				case ControlState.Hover:
				{
					VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Header.Item.Hot);
					vsr.DrawBackground(g, rect);
					break;
				}
				case ControlState.Pressed:
				{
					VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Header.Item.Pressed);
					vsr.DrawBackground(g, rect);
					break;
				}
				default:
				{
					VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
					vsr.DrawBackground(g, rect);
					break;
				}
			}
		}
		public override void DrawListViewTreeGlyph(Graphics g, Rectangle rect, ControlState state, bool expanded)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawListViewTreeGlyph(g, rect, state, expanded);
				return;
			}

			switch (state)
			{
				default:
				{
					if (expanded)
					{
						VisualStyleRenderers.TreeView.Glyph.Opened.Normal.DrawBackground(g, rect);
					}
					else
					{
						VisualStyleRenderers.TreeView.Glyph.Closed.Normal.DrawBackground(g, rect);
					}
					break;
				}
			}
		}
		public override void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused)
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				base.DrawListItemBackground(g, rect, state, selected, focused);
				return;
			}

			switch (state)
			{
				case ControlState.Normal:
				{
					if (selected)
					{
						if (focused)
						{
							VisualStyleRenderers.ListView.Item.SelectedFocused.DrawBackground(g, rect);
						}
						else
						{
							VisualStyleRenderers.ListView.Item.SelectedUnfocused.DrawBackground(g, rect);
						}
					}
					break;
				}
				case ControlState.Hover:
				{
					if (selected)
					{
						VisualStyleRenderers.ListView.Item.SelectedHot.DrawBackground(g, rect);
					}
					else
					{
						VisualStyleRenderers.ListView.Item.Hot.DrawBackground(g, rect);
					}
					break;
				}
			}
		}
		public override void DrawListSelectionRectangle(Graphics g, Rectangle rect)
		{
			base.DrawListSelectionRectangle(g, rect);
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
