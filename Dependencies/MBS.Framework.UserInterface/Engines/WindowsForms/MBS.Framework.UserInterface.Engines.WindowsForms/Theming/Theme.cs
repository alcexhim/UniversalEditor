using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using MBS.Framework.UserInterface.Theming;
using MBS.Framework.UserInterface.ObjectModels.Theming;
using MBS.Framework.UserInterface.DataFormats.Theming;
using UniversalEditor.Accessors;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming
{
	public abstract class Theme
	{
		private Guid mvarID = Guid.Empty;
		public virtual Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
		public void SetColor(string name, string value)
		{
			SetColorInternal(name, value);
		}
		protected virtual void SetColorInternal(string name, string value)
		{

		}

		public void SetFont(string name, string value)
		{
			SetFontInternal(name, value);
		}
		protected virtual void SetFontInternal(string name, string value)
		{

		}

		/// <summary>
		/// Determines the type of animation used for flyout menus.
		/// </summary>
		public virtual CommandBarMenuAnimationType CommandBarMenuAnimationType { get { return CommandBarMenuAnimationType.None; } }

		/// <summary>
		/// Determines whether SpaceSaver menus are enabled; that is, menu items intelligently default
		/// to shown or hidden based on frequency of usage.
		/// </summary>
		public virtual bool EnableSpaceSaverMenus { get { return false; } }

		/// <summary>
		/// Determines whether the top-level window is drawn with a custom frame.
		/// </summary>
		public virtual bool HasCustomToplevelWindowFrame
		{
			get { return false; }
		}

		/// <summary>
		/// Determines if the theme should use a low-resolution equivalent, if supported.
		/// </summary>
		protected bool UseLowResolution
		{
			get { return System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel <= 8; }
		}
		/// <summary>
		/// Determines if the theme should use a high-contrast equivalent, if supported.
		/// </summary>
		protected bool UseHighContrast
		{
			get { return System.Windows.Forms.SystemInformation.HighContrast; }
		}

		public Theme()
		{
			InitThemedColors();
		}

		public virtual string GetBasePath()
		{
			System.Reflection.Assembly entryAsm = System.Reflection.Assembly.GetEntryAssembly();
			if (entryAsm == null) return String.Empty;

			string pathName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				System.IO.Path.GetDirectoryName(entryAsm.Location),
				"Themes",
				this.ID.ToString("B").ToUpper()
			});
			return pathName;
		}
		public Image GetImage(string path)
		{
			string filename = null;
			/*
			if (mvarCurrentTheme is CustomTheme)
			{
				CustomTheme ct = (mvarCurrentTheme as CustomTheme);
				ThemeStockImage img = ct.ThemeDefinition.StockImages[path];
				if (img != null)
				{
					filename = GetBasePath() + System.IO.Path.DirectorySeparatorChar.ToString() + "Images" + System.IO.Path.DirectorySeparatorChar.ToString() + ct.ThemeDefinition.BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + img.ImageFileName;
				}
			}
			*/
			if (filename == null || !System.IO.File.Exists(filename))
			{
				filename = GetBasePath() + System.IO.Path.DirectorySeparatorChar.ToString() + "Images" + System.IO.Path.DirectorySeparatorChar.ToString() + path;
			}

			// normalize directory separator chars (not really necessary but looks pretty)
			filename = filename.Replace("\\", System.IO.Path.DirectorySeparatorChar.ToString());
			filename = filename.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());

			path = path.Replace("\\", System.IO.Path.DirectorySeparatorChar.ToString());
			path = path.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());

			if (System.IO.File.Exists(filename))
			{
				Image image = Image.FromFile(filename);
				return image;
			}
			else
			{
				Console.Write("ac-theme: stock image '" + path + "' not found");
				if (mvarCurrentTheme != null) Console.Write(" for theme '" + mvarCurrentTheme.Name + "'");
				Console.WriteLine();
			}
			return null;
		}

		private void InitThemedColors()
		{
			string colorScheme = System.Windows.Forms.VisualStyles.VisualStyleInformation.ColorScheme;
			string fileName = System.IO.Path.GetFileName(ColorTable.VisualStyleThemeFileName);

			bool flag = false;
			if (string.Equals("luna.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
			{
				if (colorScheme == "NormalColor")
				{
					InitBlueLunaColors();
					ColorTable.UseSystemColors = false;
					flag = true;
				}
				else
				{
					if (colorScheme == "HomeStead")
					{
						InitOliveLunaColors();
						ColorTable.UseSystemColors = false;
						flag = true;
					}
					else
					{
						if (colorScheme == "Metallic")
						{
							InitSilverLunaColors();
							ColorTable.UseSystemColors = false;
							flag = true;
						}
					}
				}
			}
			else
			{
				if (string.Equals("aero.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
				{
					InitAeroColors();
					flag = true;
				}
				else
				{
					if (string.Equals("royale.msstyles", fileName, StringComparison.OrdinalIgnoreCase) && (colorScheme == "NormalColor" || colorScheme == "Royale"))
					{
						InitRoyaleColors();
						ColorTable.UseSystemColors = false;
						flag = true;
					}
				}
			}
			if (!flag)
			{
				InitSystemColors();
				ColorTable.UseSystemColors = true;
			}
			InitCommonColors();
			InitRibbonColors();
		}

		private static Theme mvarCurrentTheme = new BuiltinThemes.VisualStudio2012Theme(BuiltinThemes.VisualStudio2012Theme.ColorMode.Dark);
		public static Theme CurrentTheme { get { return mvarCurrentTheme; } set { mvarCurrentTheme = value; } }

		private ColorTable mvarColorTable = new ColorTable();
		public ColorTable ColorTable { get { return mvarColorTable; } set { mvarColorTable = value; } }

		private FontTable mvarFontTable = new FontTable();
		public FontTable FontTable { get { return mvarFontTable; } set { mvarFontTable = value; } }

		private MetricTable mvarMetricTable = new MetricTable();
		public MetricTable MetricTable { get { return mvarMetricTable; } }

		protected static Rectangle DeflateRect(Rectangle rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}
		protected static Size FlipSize(Size size)
		{
			int width = size.Width;
			size.Width = size.Height;
			size.Height = width;
			return size;
		}
		protected static Rectangle CalculateBackgroundImageRectangle(Rectangle bounds, Image backgroundImage, ImageLayout imageLayout)
		{
			Rectangle result = bounds;
			if (backgroundImage != null)
			{
				switch (imageLayout)
				{
					case ImageLayout.None:
						{
							result.Size = backgroundImage.Size;
							break;
						}
					case ImageLayout.Center:
						{
							result.Size = backgroundImage.Size;
							Size size = bounds.Size;
							if (size.Width > result.Width)
							{
								result.X = (size.Width - result.Width) / 2;
							}
							if (size.Height > result.Height)
							{
								result.Y = (size.Height - result.Height) / 2;
							}
							break;
						}
					case ImageLayout.Stretch:
						{
							result.Size = bounds.Size;
							break;
						}
					case ImageLayout.Zoom:
						{
							Size size2 = backgroundImage.Size;
							float num = (float)bounds.Width / (float)size2.Width;
							float num2 = (float)bounds.Height / (float)size2.Height;
							if (num < num2)
							{
								result.Width = bounds.Width;
								result.Height = (int)((double)((float)size2.Height * num) + 0.5);
								if (bounds.Y >= 0)
								{
									result.Y = (bounds.Height - result.Height) / 2;
								}
							}
							else
							{
								result.Height = bounds.Height;
								result.Width = (int)((double)((float)size2.Width * num2) + 0.5);
								if (bounds.X >= 0)
								{
									result.X = (bounds.Width - result.Width) / 2;
								}
							}
							break;
						}
				}
			}
			return result;
		}
		protected static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect)
		{
			DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, Point.Empty, RightToLeft.No);
		}
		protected static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset)
		{
			DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, scrollOffset, RightToLeft.No);
		}
		protected static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset, RightToLeft rightToLeft)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (backgroundImageLayout == ImageLayout.Tile)
			{
				using (TextureBrush textureBrush = new TextureBrush(backgroundImage, WrapMode.Tile))
				{
					if (scrollOffset != Point.Empty)
					{
						Matrix transform = textureBrush.Transform;
						transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
						textureBrush.Transform = transform;
					}
					g.FillRectangle(textureBrush, clipRect);
					return;
				}
			}
			Rectangle rectangle = CalculateBackgroundImageRectangle(bounds, backgroundImage, backgroundImageLayout);
			if (rightToLeft == RightToLeft.Yes && backgroundImageLayout == ImageLayout.None)
			{
				rectangle.X += clipRect.Width - rectangle.Width;
			}
			using (SolidBrush solidBrush = new SolidBrush(backColor))
			{
				g.FillRectangle(solidBrush, clipRect);
			}
			if (!clipRect.Contains(rectangle))
			{
				if (backgroundImageLayout == ImageLayout.Stretch || backgroundImageLayout == ImageLayout.Zoom)
				{
					rectangle.Intersect(clipRect);
					g.DrawImage(backgroundImage, rectangle);
					return;
				}
				if (backgroundImageLayout == ImageLayout.None)
				{
					rectangle.Offset(clipRect.Location);
					Rectangle destRect = rectangle;
					destRect.Intersect(clipRect);
					Rectangle rectangle2 = new Rectangle(Point.Empty, destRect.Size);
					g.DrawImage(backgroundImage, destRect, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, GraphicsUnit.Pixel);
					return;
				}
				Rectangle destRect2 = rectangle;
				destRect2.Intersect(clipRect);
				Rectangle rectangle3 = new Rectangle(new Point(destRect2.X - rectangle.X, destRect2.Y - rectangle.Y), destRect2.Size);
				g.DrawImage(backgroundImage, destRect2, rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, GraphicsUnit.Pixel);
				return;
			}
			else
			{
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
				g.DrawImage(backgroundImage, rectangle, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttributes);
				imageAttributes.Dispose();
			}
		}

		#region CommandBars
		public virtual void DrawCommandBarBackground(System.Drawing.Graphics graphics, Rectangle rectangle, Orientation orientation, System.Windows.Forms.ToolStrip parent)
		{
		}
		public virtual void DrawCommandButtonBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
		}
		public virtual void DrawMenuItemBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
		}
		public virtual void DrawCommandBarBorder(Graphics graphics, System.Windows.Forms.ToolStrip toolStrip, Rectangle connectedArea)
		{
		}
		public virtual void DrawText(Graphics graphics, string text, Color color, Font font, Rectangle textRectangle, System.Windows.Forms.TextFormatFlags textFormat, System.Windows.Forms.ToolStripTextDirection textDirection, System.Windows.Forms.ToolStripItem item)
		{
            if (color == Color.Empty)
            {
                if (item is ToolStripMenuItem)
                {
                    if (item.IsOnDropDown)
                    {
                        if (item.Pressed)
                        {
                            color = ColorTable.CommandBarMenuControlTextPressed;
                        }
                        else if (item.Selected)
                        {
                            color = ColorTable.CommandBarMenuControlTextHighlight;
                        }
                        else
                        {
                            color = ColorTable.CommandBarMenuControlText;
                        }
                    }
                    else
                    {
                        if (item.Pressed)
                        {
                            color = ColorTable.CommandBarControlTextPressed;
                        }
                        else if (item.Selected)
                        {
                            color = ColorTable.CommandBarControlTextHover;
                        }
                        else
                        {
                            color = ColorTable.CommandBarControlText;
                        }
                    }
                }
                else if (item is ToolStripStatusLabel)
                {
                    color = ColorTable.StatusBarText;
                }

                color = (item.Enabled ? color : System.Drawing.SystemColors.GrayText);
            }

			if (textDirection != ToolStripTextDirection.Horizontal && textRectangle.Width > 0 && textRectangle.Height > 0)
			{
				Size size = FlipSize(textRectangle.Size);
				using (Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppPArgb))
				{
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						graphics2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
						TextRenderer.DrawText(graphics2, text, font, new Rectangle(Point.Empty, size), color, textFormat);
						bitmap.RotateFlip((textDirection == ToolStripTextDirection.Vertical90) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);
						graphics.DrawImage(bitmap, textRectangle);
					}
					return;
				}
			}
			TextRenderer.DrawText(graphics, text, font, textRectangle, color, textFormat);
		}
		public virtual void DrawArrow(Graphics graphics, bool enabled, Rectangle arrowRectangle, ArrowDirection direction)
		{
			Color arrowColor = (enabled ? System.Drawing.SystemColors.ControlText : System.Drawing.SystemColors.ControlDark);
			graphics.FillArrow(arrowRectangle, direction, new SolidBrush(arrowColor));
		}
		public virtual void DrawDropDownButtonBackground(Graphics graphics, ToolStripDropDownButton item, ToolStrip parent)
		{
		}
		public virtual void DrawGrip(Graphics graphics, Rectangle gripBounds, Orientation orientation, bool rtl)
		{
		}
		public virtual void DrawImageMargin(Graphics graphics, Rectangle affectedBounds, ToolStrip toolStrip)
		{
		}
		public virtual void DrawCheck(Graphics graphics, ToolStripItem item, Rectangle imageRectangle)
		{
		}
		public virtual void DrawImage(Graphics graphics, Rectangle imageRectangle, Image image, ToolStripItem item)
		{
			graphics.DrawImage(image, imageRectangle);
		}
		public virtual void DrawLabel(Graphics graphics, ToolStripItem item)
		{
		}
		public virtual void DrawOverflowButton(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
		}
		public virtual void DrawSeparator(Graphics graphics, ToolStripItem item, Rectangle rectangle, bool vertical)
		{
		}
		public virtual void DrawSplitButtonBackground(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
		}
		public virtual void DrawContentAreaBackground(Graphics graphics, Rectangle rectangle)
		{
		}
		
		public abstract void DrawDropDownBackground(Graphics graphics, Rectangle rectangle, ControlState state);
		public abstract void DrawDropDownButton(Graphics graphics, Rectangle rectangle, ControlState dropdownState, ControlState buttonState);
		public abstract void DrawDropDownMenuBackground(Graphics graphics, Rectangle rectangle);

		public virtual void DrawCommandBarPanelBackground(Graphics graphics, ToolStripPanel toolStripPanel)
		{
			DrawCommandBarPanelBackground(graphics, new Rectangle(0, 0, toolStripPanel.Width, toolStripPanel.Height));
		}
		public virtual void DrawCommandBarPanelBackground(Graphics graphics, Rectangle rectangle)
		{
		}
		public virtual void DrawSizingGrip(Graphics graphics, Rectangle gripBounds)
		{
		}
		#endregion

		#region Themed Colors
		protected virtual void InitCommonColors()
		{
		}
		protected virtual void InitSystemColors()
		{
		}
		protected virtual void InitBlueLunaColors()
		{
		}
		protected virtual void InitOliveLunaColors()
		{
		}
		protected virtual void InitRoyaleColors()
		{
		}
		protected virtual void InitSilverLunaColors()
		{
		}
		protected virtual void InitAeroColors()
		{
			InitSystemColors();
			ColorTable.UseSystemColors = true;
			ColorTable.CommandBarControlBackgroundHover = ColorTable.ButtonSelectedHighlight;
			ColorTable.CommandBarControlBackgroundSelected = ColorTable.CommandBarControlBackgroundHover;
		}
		#endregion

		#region Ribbon
		public virtual void DrawRibbonTabBar(Graphics graphics, Rectangle rectangle, bool minimized)
		{
			graphics.FillRectangle(new SolidBrush(ColorTable.RibbonTabBarBackground), rectangle);

			if (minimized)
			{
				graphics.DrawLine(new Pen(ColorTable.RibbonTabBarBorderBottom), rectangle.Left, rectangle.Bottom - 1, rectangle.Right, rectangle.Bottom - 1);
			}
			else
			{
				graphics.DrawLine(new Pen(ColorTable.RibbonTabBarBorderTop), rectangle.Left, rectangle.Bottom - 2, rectangle.Right, rectangle.Bottom - 2);
			}
		}
		public virtual void DrawRibbonTabPageBackground(Graphics graphics, Rectangle rectangle)
		{
			LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, rectangle.Width, rectangle.Height), ColorTable.RibbonTabPageBackgroundGradientStart, ColorTable.RibbonTabPageBackgroundGradientEnd, LinearGradientMode.Vertical);
			graphics.FillRectangle(lgb, rectangle);
			graphics.DrawLine(new Pen(ColorTable.RibbonTabBarBorderBottom), rectangle.Left, rectangle.Bottom - 1, rectangle.Right, rectangle.Bottom - 1);
		}
		public virtual void DrawRibbonApplicationButton(Graphics graphics, Rectangle bounds, ControlState state)
		{
			switch (state)
			{
				case ControlState.Normal:
					{
						int h = (bounds.Height / 2);

						// Outer gradient (border)
						LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bounds.Width - 2, h), ColorTable.RibbonApplicationButtonGradientBorderTopBegin, ColorTable.RibbonApplicationButtonGradientBorderTopEnd, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 1, bounds.Top + 2, bounds.Width - 2, h + 2);

						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBorderBottomBegin, ColorTable.RibbonApplicationButtonGradientBorderBottomEnd, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 1, h, bounds.Width - 2, h);

						// Inner gradient (background)
						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBackgroundTopBegin, ColorTable.RibbonApplicationButtonGradientBackgroundTopEnd, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 2, bounds.Top + 3, bounds.Width - 4, h - 1);

						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBackgroundBottomBegin, ColorTable.RibbonApplicationButtonGradientBackgroundBottomEnd, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 2, h, bounds.Width - 4, h - 1);

						// Border
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderTop), bounds.Left, bounds.Top, bounds.Right - 3, bounds.Top);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorder), bounds.Left, bounds.Top + 1, bounds.Right - 3, bounds.Top + 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorder), bounds.Left, bounds.Top + 1, bounds.Left, bounds.Bottom - 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorder), bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorder), bounds.Right - 1, bounds.Top + 3, bounds.Right - 1, bounds.Bottom - 1);

						// Connector
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorder), bounds.Right - 3, bounds.Top + 1, bounds.Right - 1, bounds.Top + 3);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderTop), bounds.Right - 3, bounds.Top, bounds.Right - 1, bounds.Top + 2);
						break;
					}
				case ControlState.Hover:
					{
						int h = (bounds.Height / 2);

						// Outer gradient (border)
						LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bounds.Width - 2, h), ColorTable.RibbonApplicationButtonGradientBorderTopBegin, ColorTable.RibbonApplicationButtonGradientBorderTopEndHover, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 1, bounds.Top + 2, bounds.Width - 2, h + 2);

						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBorderBottomBegin, ColorTable.RibbonApplicationButtonGradientBorderBottomEndHover, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 1, h, bounds.Width - 2, h);

						// Inner gradient (background)
						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBackgroundTopBegin, ColorTable.RibbonApplicationButtonGradientBackgroundTopEndHover, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 2, bounds.Top + 3, bounds.Width - 4, h - 1);

						brush = new LinearGradientBrush(new Rectangle(0, -1, bounds.Width, h), ColorTable.RibbonApplicationButtonGradientBackgroundBottomBegin, ColorTable.RibbonApplicationButtonGradientBackgroundBottomEndHover, LinearGradientMode.Vertical);
						graphics.FillRectangle(brush, bounds.Left + 2, h, bounds.Width - 4, h - 1);

						// Border
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderTopHover), bounds.Left, bounds.Top, bounds.Right - 3, bounds.Top);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderHover), bounds.Left, bounds.Top + 1, bounds.Right - 3, bounds.Top + 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderHover), bounds.Left, bounds.Top + 1, bounds.Left, bounds.Bottom - 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderHover), bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderHover), bounds.Right - 1, bounds.Top + 3, bounds.Right - 1, bounds.Bottom - 1);

						// Connector
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderHover), bounds.Right - 3, bounds.Top + 1, bounds.Right - 1, bounds.Top + 3);
						graphics.DrawLine(new Pen(ColorTable.RibbonApplicationButtonBorderTopHover), bounds.Right - 3, bounds.Top, bounds.Right - 1, bounds.Top + 2);
						break;
					}
			}
		}
		public virtual void DrawRibbonApplicationButtonImage(Graphics graphics, Rectangle bounds, Image image)
		{
			if (image == null)
			{
				graphics.DrawRectangle(new Pen(Color.FromArgb((255 / 8), 0, 0, 0)), bounds.Left, bounds.Top, bounds.Width, bounds.Height);
				graphics.FillRectangle(Brushes.White, bounds.Left + 1, bounds.Top + 1, bounds.Width - 8, bounds.Height - 2);
				graphics.FillRectangle(Brushes.White, bounds.Left + 1, bounds.Top + 1, bounds.Width - 3, 2);
				graphics.DrawLine(Pens.White, bounds.Left + 1, bounds.Top + 4, bounds.Right - 3, bounds.Top + 4);
				graphics.DrawLine(Pens.White, bounds.Left + 1, bounds.Top + 6, bounds.Right - 3, bounds.Top + 6);
				graphics.DrawLine(Pens.White, bounds.Left + 1, bounds.Top + 8, bounds.Right - 3, bounds.Top + 8);
				graphics.FillRectangle(Brushes.White, bounds.Left + 1, bounds.Top + 10, bounds.Width - 3, 2);

				graphics.FillRectangle(Brushes.White, bounds.Right - 2, bounds.Top + 1, 2, bounds.Height - 1);
			}
			else
			{
			}
		}
		public virtual void DrawRibbonTab(Graphics graphics, Rectangle bounds, ControlState state, MBS.Framework.UserInterface.Controls.Ribbon.RibbonTab tab)
		{
			#region Control state specific
			if ((state & ControlState.Hover) == ControlState.Hover)
			{
				Rectangle rectBorder = new Rectangle(bounds.Left, bounds.Top + 2, bounds.Width, bounds.Height - 2);
				LinearGradientBrush lgbBorder = new LinearGradientBrush(rectBorder, mvarColorTable.RibbonTabGradientBorderHoverStart, mvarColorTable.RibbonTabGradientBorderHoverEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(lgbBorder, rectBorder);

				if ((state & ControlState.Pressed) != ControlState.Pressed)
				{
					Rectangle rectFill = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 1);
					LinearGradientBrush lgbFill = new LinearGradientBrush(rectFill, mvarColorTable.RibbonTabGradientBackgroundHoverStart, mvarColorTable.RibbonTabGradientBackgroundHoverEnd, LinearGradientMode.Vertical);
					graphics.FillRectangle(lgbFill, rectFill);
				}
				// Upper-left corner
				graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Left, bounds.Top + 2, bounds.Left + 2, bounds.Top);

				// Upper-right corner
				graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Right - 1, bounds.Top + 2, bounds.Right - 3, bounds.Top);

				// Top bar
				graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Left + 2, bounds.Top, bounds.Right - 3, bounds.Top);
			}
			if ((state & ControlState.Pressed) == ControlState.Pressed)
			{
				if ((state & ControlState.Hover) != ControlState.Hover)
				{
					Rectangle rectBorder = new Rectangle(bounds.Left, bounds.Top + 2, bounds.Width, bounds.Height - 2);
					LinearGradientBrush lgbBorder = new LinearGradientBrush(rectBorder, mvarColorTable.RibbonTabGradientBorderPressedStart, mvarColorTable.RibbonTabGradientBorderPressedEnd, LinearGradientMode.Vertical);
					graphics.FillRectangle(lgbBorder, rectBorder);
				}
				Rectangle rectFill = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 1);
				LinearGradientBrush lgbFill = new LinearGradientBrush(rectFill, mvarColorTable.RibbonTabGradientBackgroundPressedStart, mvarColorTable.RibbonTabGradientBackgroundPressedEnd, LinearGradientMode.Vertical);
				graphics.FillRectangle(lgbFill, rectFill);
				if ((state & ControlState.Hover) != ControlState.Hover)
				{
					// Upper-left corner
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderPressedStart), bounds.Left, bounds.Top + 2, bounds.Left + 2, bounds.Top);

					// Upper-right corner
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderPressedStart), bounds.Right - 1, bounds.Top + 2, bounds.Right - 3, bounds.Top);

					// Top bar
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderPressedStart), bounds.Left + 2, bounds.Top, bounds.Right - 3, bounds.Top);
				}
				else
				{
					// Upper-left corner
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Left, bounds.Top + 2, bounds.Left + 2, bounds.Top);

					// Upper-right corner
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Right - 1, bounds.Top + 2, bounds.Right - 3, bounds.Top);

					// Top bar
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBorderHoverStart), bounds.Left + 2, bounds.Top, bounds.Right - 3, bounds.Top);
				}
				/*
				if (!tab.Parent.IsMinimized)
				{
					graphics.DrawLine(new Pen(mvarColorTable.RibbonTabGradientBackgroundPressedEnd), bounds.Left + 1, bounds.Bottom, bounds.Right - 2, bounds.Bottom);
				}
				*/
			}
			#endregion
			#region Text
			Font font = System.Drawing.SystemFonts.MenuFont;
			font = new Font(font.FontFamily, 8, FontStyle.Regular);

			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

			TextRenderer.DrawText(graphics, tab.Title, font, bounds, ColorTable.RibbonTabText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			#endregion
		}
		public virtual void DrawRibbonControlGroup(Graphics g, Rectangle rect, MBS.Framework.UserInterface.Controls.Ribbon.RibbonTabGroup group)
		{
			Font font = System.Drawing.SystemFonts.MenuFont;
			font = new Font(font.FontFamily, 8, FontStyle.Regular);

			TextRenderer.DrawText(g, group.Title, font, rect, ColorTable.RibbonControlGroupText, TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter);
		}
		public virtual void DrawRibbonControl(Graphics g, Rectangle rect, MBS.Framework.UserInterface.Controls.Ribbon.RibbonControl ctl)
		{
			/*
			if (ctl.DisplayStyle == MBS.Framework.UserInterface.Controls.Ribbon.RibbonItemDisplayStyle.ImageAboveText)
			{
				if (ctl.ControlState == ControlState.Hover)
				{
					if (ctl is Ribbon.RibbonControlSplitButton)
					{
						Rectangle rectButtonTopTop = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, 24);
						Rectangle rectButtonTopBottom = new Rectangle(rect.X + 1, rect.Y + 1 + 24, rect.Width - 2, 12);
						Rectangle rectButtonBottom = new Rectangle(rect.X + 1, rect.Y + 2 + 24 + 12, rect.Width - 2, 27);

						LinearGradientBrush brhButtonTopTop = new LinearGradientBrush(rectButtonTopTop, mvarColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientBegin, mvarColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientMiddle, LinearGradientMode.Vertical);
						SolidBrush brhButtonTopBottom = new SolidBrush(mvarColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientEnd);

						Pen penBorderTop = new Pen(mvarColorTable.RibbonControlBorderHover);
						Pen penBorderBottom = new Pen(mvarColorTable.RibbonControlBorderHover);
						if (!(ctl as Ribbon.RibbonControlSplitButton).ButtonEnabled)
						{
							brhButtonTopTop = new LinearGradientBrush(rectButtonTopTop, mvarColorTable.RibbonControlBackgroundDisabledBegin, mvarColorTable.RibbonControlBackgroundDisabledMiddle, LinearGradientMode.Vertical);
							brhButtonTopBottom = new SolidBrush(mvarColorTable.RibbonControlBackgroundDisabledEnd);
							penBorderTop = new Pen(mvarColorTable.RibbonControlBorderDisabled);
						}

						LinearGradientBrush brhButtonBottom = new LinearGradientBrush(rectButtonBottom, mvarColorTable.RibbonControlSplitButtonBottomBackgroundHoverGradientBegin, mvarColorTable.RibbonControlSplitButtonBottomBackgroundHoverGradientEnd, LinearGradientMode.Vertical);

						g.FillRectangle(brhButtonTopTop, rectButtonTopTop);
						g.FillRectangle(brhButtonTopBottom, rectButtonTopBottom);
						g.FillRectangle(brhButtonBottom, rectButtonBottom);

						// Border for the top part of the split button
						g.DrawLine(penBorderTop, rect.X + 1, rect.Y, rect.Right - 2, rect.Y);   // top
						g.DrawLine(penBorderTop, rect.X, rect.Y + 1, rect.X, rect.Bottom - 29 - 1); // left
						if ((ctl as Ribbon.RibbonControlSplitButton).ButtonEnabled)
						{
							g.DrawLine(penBorderTop, rect.X, rect.Bottom - 29, rect.Right, rect.Bottom - 29);   // bottom
						}
						else
						{
							g.DrawLine(penBorderTop, rect.X + 1, rect.Bottom - 29, rect.Right - 2, rect.Bottom - 29);   // bottom
						}
						g.DrawLine(penBorderTop, rect.Right - 1, rect.Y + 1, rect.Right - 1, rect.Bottom - 29 - 1); // right

						// Border for the bottom part of the split button
						g.DrawLine(penBorderBottom, rect.X, rect.Bottom - 28, rect.X, rect.Bottom - 2); // left
						g.DrawLine(penBorderBottom, rect.X + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);   // bottom
						g.DrawLine(penBorderBottom, rect.Right - 1, rect.Bottom - 28, rect.Right - 1, rect.Bottom - 2); // right
					}
					else if (ctl is Ribbon.RibbonControlButton)
					{
						Rectangle rectButtonTop = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, 25);
						Rectangle rectButtonBottom = new Rectangle(rect.X + 1, rect.Y + 2 + 25, rect.Width - 2, 39);

						LinearGradientBrush brhButtonTop = new LinearGradientBrush(rectButtonTop, mvarColorTable.RibbonControlBackgroundHoverBegin, mvarColorTable.RibbonControlBackgroundHoverMiddleTop, LinearGradientMode.Vertical);
						LinearGradientBrush brhButtonBottom = new LinearGradientBrush(rectButtonBottom, mvarColorTable.RibbonControlBackgroundHoverMiddleBottom, mvarColorTable.RibbonControlBackgroundHoverEnd, LinearGradientMode.Vertical);

						g.FillRectangle(brhButtonTop, rectButtonTop);
						g.FillRectangle(brhButtonBottom, rectButtonBottom);

						g.DrawRoundedRectangle(new Pen(ColorTable.RibbonControlBorderHover), rect.X, rect.Y, rect.Width, rect.Height);
					}
				}
			}
			else if (ctl.DisplayStyle == Ribbon.RibbonControlDisplayStyle.ImageBesideText || ctl.DisplayStyle == Ribbon.RibbonControlDisplayStyle.TextOnly)
			{
			}

			#region Control Text
			if (ctl.DisplayStyle != Ribbon.RibbonControlDisplayStyle.ImageOnly)
			{
				Font font = System.Drawing.SystemFonts.MenuFont;
				font = new Font(font.FontFamily, 8, FontStyle.Regular);

				Rectangle textRect = rect;
				TextFormatFlags flags = TextFormatFlags.Default;

				switch (ctl.DisplayStyle)
				{
					case Ribbon.RibbonControlDisplayStyle.ImageAboveText:
						{
							flags |= TextFormatFlags.HorizontalCenter;
							textRect.X += 2;
							textRect.Y += 38;
							break;
						}
					case Ribbon.RibbonControlDisplayStyle.ImageBesideText:
						{
							textRect.X += 23;
							textRect.Y += 8;
							break;
						}
					case Ribbon.RibbonControlDisplayStyle.TextOnly:
						{
							textRect.X += 4;
							textRect.Y += 8;
							break;
						}
				}

				TextRenderer.DrawText(g, ctl.Text, font, textRect, ColorTable.RibbonControlText, flags);
			}
			#endregion
			*/		
		}
		public void DrawRibbonControlGroupActionButton(Graphics g, Rectangle rect, ControlState state)
		{
			switch (state)
			{
				case ControlState.Hover:
					{
						break;
					}
				case ControlState.Pressed:
					{
						break;
					}
			}

			Pen pen = new Pen(ColorTable.RibbonControlGroupActionButton);

			// Arrowhead
			g.DrawLine(pen, rect.X + 4, rect.Y + 3, rect.X + 10, rect.Y + 3);
			g.DrawLine(pen, rect.X + 4, rect.Y + 3, rect.X + 4, rect.Y + 8);

			// Back part
			g.DrawLine(pen, rect.X + 7, rect.Y + 6, rect.X + 10, rect.Y + 9);
			g.DrawLine(pen, rect.X + 7, rect.Y + 9, rect.X + 10, rect.Y + 9);

			g.DrawLine(pen, rect.X + 10, rect.Y + 6, rect.X + 10, rect.Y + 9);

			g.DrawLine(pen, rect.X + 8, rect.Y + 7, rect.X + 10, rect.Y + 7);
			g.DrawLine(pen, rect.X + 8, rect.Y + 8, rect.X + 10, rect.Y + 8);
		}
		#endregion

		private static Theme[] mvarAvailableThemes = null;
		public static Theme[] Get()
		{
			if (mvarAvailableThemes == null)
			{
				List<Theme> list = new List<Theme>();
				System.Reflection.Assembly[] asms = MBS.Framework.Reflection.GetAvailableAssemblies();
				foreach (System.Reflection.Assembly asm in asms)
				{
					try
					{
						Type[] types = asm.GetTypes();
						foreach (Type type in types)
						{
							if (type.IsSubclassOf(typeof(Theme)) && !type.IsAbstract && type != typeof(CustomTheme))
							{
								list.Add((Theme)asm.CreateInstance(type.FullName));
							}
						}
					}
					catch (System.Reflection.ReflectionTypeLoadException ex)
					{
						foreach (Type type in ex.Types)
						{
							if (type == null) continue;
							if (type.IsSubclassOf(typeof(Theme)) && !type.IsAbstract && type != typeof(CustomTheme))
							{
								list.Add((Theme)asm.CreateInstance(type.FullName));
							}
						}
					}
				}


				string themeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + System.IO.Path.DirectorySeparatorChar.ToString() + "Themes";
				if (System.IO.Directory.Exists(themeDir))
				{
					string[] themeFileNames = System.IO.Directory.GetFiles(themeDir, "*.xml", System.IO.SearchOption.AllDirectories);
					foreach (string themeFileName in themeFileNames)
					{
						if (System.IO.File.Exists(themeFileName))
						{
							ThemeObjectModel theme = new ThemeObjectModel();
							UniversalEditor.Document.Load(theme, new ThemeXMLDataFormat(), new FileAccessor(themeFileName));
							foreach (MBS.Framework.UserInterface.ObjectModels.Theming.Theme themeDefinition in theme.Themes)
							{
								list.Add(new CustomTheme(themeDefinition));
							}
						}
					}
				}
				mvarAvailableThemes = list.ToArray();

				foreach (Theme theme in mvarAvailableThemes)
				{
					if (theme is CustomTheme)
					{
						CustomTheme ct = (theme as CustomTheme);
						if (ct.ThemeDefinition.InheritsThemeID != Guid.Empty)
						{
							CustomTheme ct1 = (GetByID(ct.ThemeDefinition.InheritsThemeID) as CustomTheme);
							if (ct1 == null)
							{
								Console.WriteLine("ac-theme: custom theme with ID '" + ct.ThemeDefinition.InheritsThemeID.ToString("B").ToUpper() + "' not found");
							}
							else
							{
								ct.ThemeDefinition.InheritsTheme = ct1.ThemeDefinition;
							}
						}
						foreach (ThemeComponent tc in ct.ThemeDefinition.Components)
						{
							if (tc.InheritsComponentID != Guid.Empty)
							{
								tc.InheritsComponent = ct.ThemeDefinition.Components[tc.InheritsComponentID];
							}
						}
					}
				}
			}
			return mvarAvailableThemes;
		}

		public static Theme GetByID(Guid id)
		{
			Theme[] themes = Get();
			foreach (Theme theme in themes)
			{
				if (theme.ID == id) return theme;
			}
			return null;
		}

		#region Ribbon Color

		private RibbonColorScheme mvarRibbonColorScheme = RibbonColorScheme.Blue;
		public RibbonColorScheme RibbonColorScheme
		{
			get { return mvarRibbonColorScheme; }
			set { mvarRibbonColorScheme = value; InitRibbonColors(); }
		}

		private void InitRibbonColors()
		{
			switch (mvarRibbonColorScheme)
			{
				#region Black
				case RibbonColorScheme.Black:
					{
						break;
					}
				#endregion
				#region Blue
				case RibbonColorScheme.Blue:
					{
						ColorTable.RibbonTabGradientBorderHoverStart = Color.FromArgb(212, 188, 141);
						ColorTable.RibbonTabGradientBorderHoverEnd = Color.FromArgb(254, 209, 94);
						ColorTable.RibbonTabGradientBackgroundHoverStart = Color.FromArgb(240, 246, 254);
						ColorTable.RibbonTabGradientBackgroundHoverEnd = Color.FromArgb(226, 235, 247);

						ColorTable.RibbonTabGradientBorderPressedStart = Color.FromArgb(151, 184, 229);
						ColorTable.RibbonTabGradientBorderPressedEnd = Color.FromArgb(146, 185, 230);
						ColorTable.RibbonTabGradientBackgroundPressedStart = Color.FromArgb(244, 249, 255);
						ColorTable.RibbonTabGradientBackgroundPressedEnd = Color.FromArgb(225, 234, 246);

						ColorTable.RibbonTabBarBackground = Color.FromArgb(191, 219, 255);
						ColorTable.RibbonTabPageBackgroundGradientStart = Color.FromArgb(219, 230, 244);
						ColorTable.RibbonTabPageBackgroundGradientMiddle = Color.FromArgb(201, 217, 237);
						ColorTable.RibbonTabPageBackgroundGradientEnd = Color.FromArgb(227, 244, 255);
						ColorTable.RibbonTabBarBorderBottom = Color.FromArgb(192, 249, 255);

						// ColorTable.RibbonControlBorderDisabled = 


						ColorTable.RibbonControlBackgroundHoverBegin = Color.FromArgb(255, 255, 247);
						ColorTable.RibbonControlBackgroundHoverMiddleTop = Color.FromArgb(255, 247, 215);
						ColorTable.RibbonControlBackgroundHoverMiddleBottom = Color.FromArgb(255, 247, 215);
						ColorTable.RibbonControlBackgroundHoverEnd = Color.FromArgb(255, 241, 191);

						ColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientBegin = Color.FromArgb(255, 255, 247);
						ColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientMiddle = Color.FromArgb(255, 247, 215);
						ColorTable.RibbonControlSplitButtonTopBackgroundHoverGradientEnd = Color.FromArgb(255, 241, 191);

						ColorTable.RibbonControlSplitButtonBottomBackgroundHoverGradientBegin = Color.FromArgb(255, 212, 73);
						ColorTable.RibbonControlSplitButtonBottomBackgroundHoverGradientEnd = Color.FromArgb(255, 231, 148);

						ColorTable.RibbonControlText = Color.FromArgb(21, 66, 139);
						ColorTable.RibbonControlDisabledText = Color.FromArgb(141, 141, 141);

						ColorTable.RibbonControlGroupText = Color.FromArgb(62, 106, 170);
						ColorTable.RibbonControlGroupActionButton = Color.FromArgb(102, 142, 175);

						ColorTable.RibbonControlGroupBorderGradientBegin = Color.FromArgb(194, 208, 222);
						ColorTable.RibbonControlGroupBorderGradientEnd = Color.FromArgb(157, 191, 219);
						break;
					}
				#endregion
				#region Silver
				case RibbonColorScheme.Silver:
					{
						break;
					}
				#endregion
			}
		}
		#endregion

		#region System Controls
		#region Button
		public abstract void DrawButtonBackground(Graphics g, Rectangle rect, ControlState state);
		#endregion
		#endregion

		#region DockPanel
		public virtual void DrawDocumentTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
		}
		public virtual void DrawDockPanelTabBackground(Graphics g, Rectangle rectTab, ControlState controlState, MBS.Framework.UserInterface.Controls.Docking.DockingItemPlacement position, bool selected, bool focused)
		{
		}
		#endregion

		public virtual void DrawDockPanelTitleBarBackground(Graphics g, Rectangle rect, bool focused)
		{
			if (focused)
			{
				DrawingTools.FillWithDoubleGradient(ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin, ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle, ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd, g, rect, rect.Height / 2, rect.Height / 2, LinearGradientMode.Vertical, false);
			}
			else
			{
				g.FillRectangle(new System.Drawing.SolidBrush(ColorTable.DockingWindowInactiveTabBackgroundGradientBegin), rect);
			}
		}

		public virtual void DrawActionBarBackground(Graphics g, Rectangle rect)
		{
			Color color1Top = Color.FromArgb(250, 252, 253);
			Color color1Bottom = Color.FromArgb(230, 240, 250);
			Color color2Top = Color.FromArgb(220, 230, 244);
			Color color2Bottom = Color.FromArgb(221, 233, 247);

			Color colorBorderTop = Color.FromArgb(255, 255, 255);
			Color colorBorderBottom = Color.FromArgb(228, 239, 251);

			Rectangle rectBorder = rect;
			LinearGradientBrush borderBrush = new LinearGradientBrush(rectBorder, colorBorderTop, colorBorderBottom, LinearGradientMode.Vertical);
			g.FillRectangle(borderBrush, rectBorder);

			Rectangle rectBackground = rect;
			rectBackground.X++;
			rectBackground.Width -= 2;
			rectBackground.Height -= 3;

			Rectangle rectBackgroundUpperHalf = rectBackground;
			rectBackgroundUpperHalf.Height /= 2;
			rectBackgroundUpperHalf.Height += 1;

			Rectangle rectBackgroundLowerHalf = rectBackgroundUpperHalf;
			rectBackgroundLowerHalf.Y = rectBackgroundUpperHalf.Bottom;

			LinearGradientBrush backgroundBrushUpperHalf = new LinearGradientBrush(rectBackgroundUpperHalf, color1Top, color1Bottom, LinearGradientMode.Vertical);
			g.FillRectangle(backgroundBrushUpperHalf, rectBackgroundUpperHalf);
			LinearGradientBrush backgroundBrushLowerHalf = new LinearGradientBrush(rectBackgroundLowerHalf, color2Top, color2Bottom, LinearGradientMode.Vertical);
			g.FillRectangle(backgroundBrushLowerHalf, rectBackgroundLowerHalf);

			Color colorBorderBottom3 = Color.FromArgb(228, 239, 251);
			g.DrawLine(new Pen(colorBorderBottom3), rect.Left, rect.Bottom - 3, rect.Right, rect.Bottom - 3);
			Color colorBorderBottom1 = Color.FromArgb(205, 218, 234);
			g.DrawLine(new Pen(colorBorderBottom1), rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
			Color colorBorderBottom2 = Color.FromArgb(160, 175, 195);
			g.DrawLine(new Pen(colorBorderBottom2), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
		}
		public virtual void DrawActionBarButtonBackground(Graphics g, Rectangle rect, ControlState state)
		{
			switch (state)
			{
				case ControlState.Hover:
					{
						#region Outer Border
						{
							Rectangle outerBorderRectangle = new Rectangle(rect.X, rect.Y + 2, rect.Width, rect.Height - 2);
							DrawingTools.FillWithFourColorGradient(g, outerBorderRectangle, Color.FromArgb(187, 201, 219), Color.FromArgb(177, 195, 216), Color.FromArgb(170, 188, 211), Color.FromArgb(174, 192, 215), LinearGradientMode.Vertical);

						}
						#endregion
						#region Inner Border
						{
							Rectangle innerBorderRectangle = new Rectangle(rect.X + 1, rect.Y + 3, rect.Width - 2, rect.Height - 4);
							DrawingTools.FillWithFourColorGradient(g, innerBorderRectangle, Color.FromArgb(253, 254, 255), Color.FromArgb(250, 252, 254), Color.FromArgb(245, 248, 252), Color.FromArgb(240, 245, 250), LinearGradientMode.Vertical);
						}
						#endregion
						#region Background
						{
							Rectangle backgroundRectangle = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 3);
							DrawingTools.FillWithFourColorGradient(g, backgroundRectangle, Color.FromArgb(248, 251, 254), Color.FromArgb(237, 242, 250), Color.FromArgb(215, 228, 244), Color.FromArgb(193, 210, 232), LinearGradientMode.Vertical);
						}
						#endregion
						#region Outer Border Pixel Triads
						{
							// Outer top-left corner pixel triad
							g.DrawPixel(Color.FromArgb(215, 225, 236), rect.X + 1, rect.Y);
							g.DrawPixel(Color.FromArgb(212, 222, 235), rect.X, rect.Y + 1);
							g.DrawPixel(Color.FromArgb(216, 224, 235), rect.X, rect.Y + 1);

							// Outer bottom-left corner pixel triad
							g.DrawPixel(Color.FromArgb(194, 208, 228), rect.X, rect.Bottom - 2);
							g.DrawPixel(Color.FromArgb(200, 212, 229), rect.X + 1, rect.Bottom - 2);
							g.DrawPixel(Color.FromArgb(192, 207, 228), rect.X, rect.Bottom - 1);

							// Outer top-right corner pixel triad
							g.DrawPixel(Color.FromArgb(213, 223, 235), rect.Right - 2, rect.Y);
							g.DrawPixel(Color.FromArgb(216, 224, 235), rect.Right - 2, rect.Y + 1);
							g.DrawPixel(Color.FromArgb(212, 222, 235), rect.Right - 1, rect.Y + 1);

							// Outer bottom-right corner pixel triad
							g.DrawPixel(Color.FromArgb(193, 208, 227), rect.Right - 1, rect.Bottom - 2);
							g.DrawPixel(Color.FromArgb(200, 212, 229), rect.Right - 2, rect.Bottom - 2);
							g.DrawPixel(Color.FromArgb(190, 205, 227), rect.Right - 2, rect.Bottom - 1);
						}
						#endregion

						// Inner top-left corner pixel triad
						g.DrawPixel(Color.FromArgb(249, 251, 253), rect.X + 2, rect.Y + 1);
						g.DrawPixel(Color.FromArgb(250, 253, 255), rect.X + 2, rect.Y + 2);
						g.DrawPixel(Color.FromArgb(251, 253, 254), rect.X + 1, rect.Y + 2);

						// Top border
						Pen topBorderPen = new Pen(Color.FromArgb(187, 202, 219));
						g.DrawLine(topBorderPen, rect.X + 1, rect.Y, rect.Right - 3, rect.Y);
						// Bottom border
						Pen bottomBorderPen = new Pen(Color.FromArgb(187, 202, 219));
						g.DrawLine(bottomBorderPen, rect.X + 1, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 1);


						break;
					}
			}
		}

		#region System Controls
		#region TextBox
		public abstract void DrawTextBoxBackground(Graphics g, Rectangle rect, ControlState state);
		#endregion
		#region ListBox
		public abstract void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused);
		public abstract void DrawListSelectionRectangle(Graphics g, Rectangle rect);
		public abstract void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted);
		public abstract void DrawListViewTreeGlyph(Graphics g, Rectangle rect, ControlState state, bool expanded);
		public virtual void DrawListViewBackground(Graphics graphics, Rectangle rectangle)
		{
			graphics.FillRectangle(new SolidBrush(ColorTable.WindowBackground), rectangle);
		}
		#endregion
		#region ProgressBar
		public abstract void DrawProgressBarBackground(Graphics g, Rectangle rect, Orientation orientation);
		public abstract void DrawProgressBarChunk(Graphics g, Rectangle rect, Orientation orientation);
		public abstract void DrawProgressBarPulse(Graphics g, Rectangle rect, Orientation orientation);
		#endregion
		#endregion

		/// <summary>
		/// Draws a custom border for a top-level window.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rectangle"></param>
		public virtual void DrawToplevelWindowBorder(Graphics g, Rectangle rectangle, string titleText, bool isActive)
		{
		}

		/// <summary>
		/// Draws the background of the multiple document switcher window list.
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="rectangle"></param>
		public virtual void DrawDocumentSwitcherBackground(Graphics graphics, Rectangle rectangle)
		{
		}

		#region Accordion
		/// <summary>
		/// Draws the background of an <see cref="MBS.Framework.UserInterface.Accordion.AccordionControl" />.
		/// </summary>
		/// <param name="rectangle"></param>
		public virtual void DrawAccordionBackground(Graphics graphics, Rectangle rectangle)
		{
		}
		#endregion
	}
}
