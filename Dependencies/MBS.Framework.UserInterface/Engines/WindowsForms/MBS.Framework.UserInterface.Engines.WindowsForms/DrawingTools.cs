using System;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	internal sealed class DrawingTools
	{
		public sealed class HSLColor
		{
			// Private data members below are on scale 0-1
			// They are scaled for use externally based on scale
			private double hue = 1.0;
			private double saturation = 1.0;
			private double luminosity = 1.0;

			private const double scale = 240.0;

			public double Hue
			{
				get { return hue * scale; }
				set { hue = CheckRange(value / scale); }
			}
			public double Saturation
			{
				get { return saturation * scale; }
				set { saturation = CheckRange(value / scale); }
			}
			public double Luminosity
			{
				get { return luminosity * scale; }
				set { luminosity = CheckRange(value / scale); }
			}

			private double CheckRange(double value)
			{
				if (value < 0.0)
					value = 0.0;
				else if (value > 1.0)
					value = 1.0;
				return value;
			}

			public override string ToString()
			{
				return String.Format("H: {0:#0.##} S: {1:#0.##} L: {2:#0.##}", Hue, Saturation, Luminosity);
			}

			public string ToRGBString()
			{
				Color color = (Color)this;
				return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##}", color.R, color.G, color.B);
			}

			#region Casts to/from System.Drawing.Color
			public static implicit operator Color(HSLColor hslColor)
			{
				double r = 0, g = 0, b = 0;
				if (hslColor.luminosity != 0)
				{
					if (hslColor.saturation == 0)
						r = g = b = hslColor.luminosity;
					else
					{
						double temp2 = GetTemp2(hslColor);
						double temp1 = 2.0 * hslColor.luminosity - temp2;

						r = GetColorComponent(temp1, temp2, hslColor.hue + 1.0 / 3.0);
						g = GetColorComponent(temp1, temp2, hslColor.hue);
						b = GetColorComponent(temp1, temp2, hslColor.hue - 1.0 / 3.0);
					}
				}
				return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
			}

			private static double GetColorComponent(double temp1, double temp2, double temp3)
			{
				temp3 = MoveIntoRange(temp3);
				if (temp3 < 1.0 / 6.0)
					return temp1 + (temp2 - temp1) * 6.0 * temp3;
				else if (temp3 < 0.5)
					return temp2;
				else if (temp3 < 2.0 / 3.0)
					return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
				else
					return temp1;
			}
			private static double MoveIntoRange(double temp3)
			{
				if (temp3 < 0.0)
					temp3 += 1.0;
				else if (temp3 > 1.0)
					temp3 -= 1.0;
				return temp3;
			}
			private static double GetTemp2(HSLColor hslColor)
			{
				double temp2;
				if (hslColor.luminosity < 0.5)  //<=??
					temp2 = hslColor.luminosity * (1.0 + hslColor.saturation);
				else
					temp2 = hslColor.luminosity + hslColor.saturation - (hslColor.luminosity * hslColor.saturation);
				return temp2;
			}

			public static implicit operator HSLColor(Color color)
			{
				HSLColor hslColor = new HSLColor();
				hslColor.hue = color.GetHue() / 360.0; // we store hue as 0-1 as opposed to 0-360 
				hslColor.luminosity = color.GetBrightness();
				hslColor.saturation = color.GetSaturation();
				return hslColor;
			}
			#endregion

			public void SetRGB(int red, int green, int blue)
			{
				HSLColor hslColor = (HSLColor)Color.FromArgb(red, green, blue);
				this.hue = hslColor.hue;
				this.saturation = hslColor.saturation;
				this.luminosity = hslColor.luminosity;
			}

			public HSLColor() { }
			public HSLColor(Color color)
			{
				SetRGB(color.R, color.G, color.B);
			}
			public HSLColor(int red, int green, int blue)
			{
				SetRGB(red, green, blue);
			}
			public HSLColor(double hue, double saturation, double luminosity)
			{
				this.Hue = hue;
				this.Saturation = saturation;
				this.Luminosity = luminosity;
			}


		}

		public enum Alignment2D
		{
			None = 0,
			Left = 3,
			Right = 4
		}

		public enum Alignment4D
		{
			None = 0,
			Top = 1,
			Bottom = 2,
			Left = 3,
			Right = 4
		}

		public enum Direction
		{
			None = 0,
			Up = 1,
			Down = 2,
			Left = 3,
			Right = 4
		}

		public sealed class Pens
		{
			public static Pen ControlTextPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.ControlText)); }
			}
			public static Pen ControlPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.Control)); }
			}
			public static Pen HighlightPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.ControlLightLight)); }
			}
			public static Pen LightShadowPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.ControlLight)); }
			}
			public static Pen ShadowPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.ControlDark)); }
			}
			public static Pen DarkShadowPen
			{
				get { return new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark)); }
			}

			private static Pen mvarFocusPen = null;
			public static Pen FocusPen
			{
				get
				{
					if (mvarFocusPen == null)
					{
						mvarFocusPen = new Pen(Color.Black);
						mvarFocusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
					}
					return mvarFocusPen;
				}
			}

			private static Pen mvarDragPen = null;
			public static Pen DragPen
			{
				get
				{
					if (mvarDragPen == null)
					{
						mvarDragPen = new Pen(Color.FromArgb(128, 0, 0, 0), 2);
					}
					return mvarDragPen;
				}
			}
		}
		public sealed class Brushes
		{
			public static Brush ControlBrush
			{
				get { return new SolidBrush(Color.FromKnownColor(KnownColor.Control)); }
			}
			public static Brush HighlightBrush
			{
				get { return new SolidBrush(Color.FromKnownColor(KnownColor.ControlLightLight)); }
			}
			public static Brush LightShadowBrush
			{
				get { return new SolidBrush(Color.FromKnownColor(KnownColor.ControlLight)); }
			}
			public static Brush ShadowBrush
			{
				get { return new SolidBrush(Color.FromKnownColor(KnownColor.ControlDark)); }
			}
			public static Brush DarkShadowBrush
			{
				get { return new SolidBrush(Color.FromKnownColor(KnownColor.ControlDarkDark)); }
			}
		}

		public static Region GetRoundedRectangleRegion(Rectangle r, int cornerSize)
		{
			// Create 4 10 x 10 rectangles for our arcs to fit in. tl=topleft, br=bottomright
			int x = r.Left;
			int y = r.Top;
			int w = r.Width;
			int h = r.Height;
			int xr = (w - (x + cornerSize));
			int yr = (h - (y + cornerSize));
			int iw = (w - xr);
			int ih = (h - yr);

			Rectangle tl = new Rectangle(x, y, cornerSize, cornerSize);
			Rectangle tr = new Rectangle(xr, y, cornerSize, cornerSize);
			Rectangle bl = new Rectangle(x, yr, cornerSize, cornerSize);
			Rectangle br = new Rectangle(xr, yr, cornerSize, cornerSize);

			// Create an inner rectangle to fill the middle
			Rectangle innerRect = new Rectangle(x, y + cornerSize, x + xr, yr - y);

			// Here's how it is all bound together.  We need to add the arcs and the
			// inner rectangle to a single GraphicsPath object.  This allows us to call
			// the .FillPath method to only fill in the section inside the arcs and our rectangle.

			GraphicsPath path = new GraphicsPath();

			float sweepAngle = 90;

			path.AddArc(tl, 180, sweepAngle);
			path.AddArc(tr, 270, sweepAngle);
			path.AddRectangle(innerRect);
			path.AddArc(bl, 90, sweepAngle);
			path.AddArc(br, 360, sweepAngle);

			return new Region(path);
		}

		public static void DrawRaisedBorder(Graphics g, Rectangle r)
		{
			g.DrawLine(Pens.HighlightPen, r.Left, r.Top, r.Right - 2, r.Top);
			g.DrawLine(Pens.HighlightPen, r.Left, r.Top, r.Left, r.Bottom - 2);

			g.DrawLine(Pens.ControlPen, r.Left + 1, r.Top + 1, r.Right - 3, r.Top + 1);
			g.DrawLine(Pens.ControlPen, r.Left + 1, r.Top + 1, r.Left + 1, r.Bottom - 3);

			g.DrawLine(Pens.ShadowPen, r.Left + 2, r.Bottom - 3, r.Right - 3, r.Bottom - 3);
			g.DrawLine(Pens.ShadowPen, r.Right - 3, r.Top + 2, r.Right - 3, r.Bottom - 3);

			g.DrawLine(Pens.DarkShadowPen, r.Left + 1, r.Bottom - 2, r.Right - 2, r.Bottom - 2);
			g.DrawLine(Pens.DarkShadowPen, r.Right - 2, r.Top + 1, r.Right - 2, r.Bottom - 2);
		}
		public static void DrawSunkenBorder(Graphics g, Rectangle r)
		{
			g.DrawLine(Pens.ShadowPen, r.Left, r.Top, r.Right - 1, r.Top);
			g.DrawLine(Pens.ShadowPen, r.Left, r.Top, r.Left, r.Bottom - 1);

			g.DrawLine(Pens.DarkShadowPen, r.Left + 1, r.Top + 1, r.Right - 2, r.Top + 1);
			g.DrawLine(Pens.DarkShadowPen, r.Left + 1, r.Top + 1, r.Left + 1, r.Bottom - 2);

			g.DrawLine(Pens.ControlPen, r.Left + 1, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
			g.DrawLine(Pens.ControlPen, r.Right - 1, r.Top + 1, r.Right - 1, r.Bottom - 1);

			g.DrawLine(Pens.HighlightPen, r.Left, r.Bottom, r.Right, r.Bottom);
			g.DrawLine(Pens.HighlightPen, r.Right, r.Top, r.Right, r.Bottom);
		}

		public static void DrawRaisedBorderMini(Graphics g, Rectangle r)
		{
			g.DrawLine(Pens.HighlightPen, r.Left, r.Top, r.Right - 2, r.Top);
			g.DrawLine(Pens.HighlightPen, r.Left, r.Top, r.Left, r.Bottom - 2);

			g.DrawLine(Pens.ShadowPen, r.Left, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
			g.DrawLine(Pens.ShadowPen, r.Right - 1, r.Top, r.Right - 1, r.Bottom - 1);
		}
		public static void DrawSunkenBorderMini(Graphics g, Rectangle r)
		{
			g.DrawLine(Pens.ShadowPen, r.Left, r.Top, r.Right - 1, r.Top);
			g.DrawLine(Pens.ShadowPen, r.Left, r.Top, r.Left, r.Bottom - 1);

			g.DrawLine(Pens.HighlightPen, r.Left + 1, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
			g.DrawLine(Pens.HighlightPen, r.Right - 1, r.Top + 1, r.Right - 1, r.Bottom - 1);
		}

		public static void DrawCheckBox(Graphics graphics, Rectangle rect)
		{
			DrawCheckBox(graphics, rect, false);
		}
		public static void DrawCheckBox(Graphics graphics, Rectangle rect, bool state)
		{
			DrawCheckBox(graphics, rect, (state ? CheckState.Checked : CheckState.Unchecked));
		}
		public static void DrawCheckBox(Graphics graphics, Rectangle rect, CheckState state)
		{
			if (state == CheckState.Indeterminate)
			{
				graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), rect);
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Window)), rect);
			}
			graphics.DrawLine(DrawingTools.Pens.ShadowPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
			graphics.DrawLine(DrawingTools.Pens.ShadowPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
			graphics.DrawLine(DrawingTools.Pens.DarkShadowPen, rect.Left + 1, rect.Top + 1, rect.Right - 3, rect.Top + 1);
			graphics.DrawLine(DrawingTools.Pens.DarkShadowPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 3);
			graphics.DrawLine(DrawingTools.Pens.HighlightPen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
			graphics.DrawLine(DrawingTools.Pens.HighlightPen, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

			if (state != CheckState.Unchecked)
			{
				Color color = Color.Black;
				if (state == CheckState.Indeterminate) color = Color.FromKnownColor(KnownColor.ControlDark);
				DrawCheckMark(graphics, new Pen(color), new Rectangle(rect.Left + 3, rect.Top + 3, rect.Width, rect.Height));
			}
		}

		public static void DrawCheckMark(Graphics graphics, Pen pen, Rectangle rect)
		{
			graphics.DrawLine(pen, rect.Left, rect.Top + 2, rect.Left, rect.Top + 4);
			graphics.DrawLine(pen, rect.Left + 1, rect.Top + 3, rect.Left + 1, rect.Top + 5);
			graphics.DrawLine(pen, rect.Left + 2, rect.Top + 4, rect.Left + 2, rect.Top + 6);
			graphics.DrawLine(pen, rect.Left + 3, rect.Top + 3, rect.Left + 3, rect.Top + 5);
			graphics.DrawLine(pen, rect.Left + 4, rect.Top + 2, rect.Left + 4, rect.Top + 4);
			graphics.DrawLine(pen, rect.Left + 5, rect.Top + 1, rect.Left + 5, rect.Top + 3);
			graphics.DrawLine(pen, rect.Left + 6, rect.Top, rect.Left + 6, rect.Top + 2);
		}

		public static void DrawArrow(Graphics graphics, Color color, Direction direction, int x, int y, int size)
		{
			Pen p = new Pen(color);
			int xpos = x, ypos = y, o = 2;

			switch (direction)
			{
			case Direction.Up:
				graphics.FillPolygon(new SolidBrush(color), new Point[]
				{
					new Point((int)(x + (double)(size / 2)), y - 1),
					new Point((x - size), y + size),
					new Point((x + size), y + size)
				});
				break;
			case Direction.Down:
				graphics.FillPolygon(new SolidBrush(color), new Point[]
				{
					new Point((x - size), y),
					new Point((x + size + size), y),
					new Point((int)(x + (double)(size / 2)), y + size + 2)
				});
				break;
			case Direction.Left:

				break;
			case Direction.Right:
				for (int i = 0; i < size; i++)
				{
					// Intermediate lines
					graphics.DrawLine(p, xpos, ypos, xpos, ypos + (size + o));
					xpos++;
					ypos++;
					o -= 2;
				}
				// Last line
				graphics.DrawLine(p, x, y + (size - 1), x + (size - 1), y + (size - 1));
				break;
			}
		}
		public static void FillWithDoubleGradient(Color beginColor, Color middleColor, Color endColor, Graphics g, Rectangle bounds, int firstGradientSize, int secondGradientSize, LinearGradientMode mode, bool rightToLeft)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			Rectangle rect = bounds;
			Rectangle rect2 = bounds;
			bool flag = true;
			if (mode == LinearGradientMode.Horizontal)
			{
				if (rightToLeft)
				{
					Color color = endColor;
					endColor = beginColor;
					beginColor = color;
				}
				rect2.Width = firstGradientSize;
				rect.Width = secondGradientSize + 1;
				rect.X = bounds.Right - rect.Width;
				flag = (bounds.Width > firstGradientSize + secondGradientSize);
			}
			else
			{
				rect2.Height = firstGradientSize;
				rect.Height = secondGradientSize + 1;
				rect.Y = bounds.Bottom - rect.Height;
				flag = (bounds.Height > firstGradientSize + secondGradientSize);
			}
			if (flag)
			{
				using (Brush brush = new SolidBrush(middleColor))
				{
					g.FillRectangle(brush, bounds);
				}
				using (Brush brush2 = new LinearGradientBrush(rect2, beginColor, middleColor, mode))
				{
					g.FillRectangle(brush2, rect2);
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, middleColor, endColor, mode))
				{
					if (mode == LinearGradientMode.Horizontal)
					{
						rect.X++;
						rect.Width--;
					}
					else
					{
						rect.Y++;
						rect.Height--;
					}
					g.FillRectangle(linearGradientBrush, rect);
					return;
				}
				goto IL_150;
			}
			goto IL_150;
			return;
		IL_150:
			using (Brush brush3 = new LinearGradientBrush(bounds, beginColor, endColor, mode))
			{
				g.FillRectangle(brush3, bounds);
			}
		}
		public static void FillWithShinyGradient(Graphics g, Color beginColor, Color middleColor, Color endColor, Rectangle bounds, int firstGradientSize, int secondGradientSize, LinearGradientMode mode)
		{
			Rectangle firstGradientBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
			Rectangle secondGradientBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height);

			if (mode == LinearGradientMode.Horizontal)
			{
				firstGradientBounds.Height = firstGradientSize;
				secondGradientBounds.Y += firstGradientSize;
				secondGradientBounds.Height -= firstGradientSize;
			}
			else if (mode == LinearGradientMode.Vertical)
			{
				firstGradientBounds.Width = firstGradientSize;
				secondGradientBounds.X += firstGradientSize;
				secondGradientBounds.Width -= firstGradientSize;
			}

			LinearGradientBrush firstGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, firstGradientBounds.Width, firstGradientBounds.Height), beginColor, middleColor, mode);
			g.FillRectangle(firstGradientBrush, firstGradientBounds);

			SolidBrush secondGradientBrush = new SolidBrush(endColor);
			g.FillRectangle(secondGradientBrush, secondGradientBounds);
		}

		public static void FillWithFourColorGradient(Graphics g, Rectangle rect, Color topGradientStart, Color topGradientEnd, Color bottomGradientStart, Color bottomGradientEnd, LinearGradientMode mode)
		{
			FillWithFourColorGradient(g, rect, topGradientStart, topGradientEnd, bottomGradientStart, bottomGradientEnd, mode, 0.5);
		}
		public static void FillWithFourColorGradient(Graphics g, Rectangle rect, Color topGradientStart, Color topGradientEnd, Color bottomGradientStart, Color bottomGradientEnd, LinearGradientMode mode, double factor)
		{
			Rectangle topRectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
			Rectangle bottomRectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

			if (mode == LinearGradientMode.Horizontal)
			{
				topRectangle.Width = (int)(topRectangle.Width * factor);
				bottomRectangle.X = topRectangle.Width;
				bottomRectangle.Width = (int)(bottomRectangle.Width * factor);
			}
			else if (mode == LinearGradientMode.Vertical)
			{
				topRectangle.Height = (int)(topRectangle.Height * factor);
				bottomRectangle.Y = topRectangle.Bottom;
				bottomRectangle.Height = (int)(bottomRectangle.Height * factor);
			}

			LinearGradientBrush innerBorderGradientTopBrush = new LinearGradientBrush(topRectangle, topGradientStart, topGradientEnd, mode);
			LinearGradientBrush innerBorderGradientBottomBrush = new LinearGradientBrush(bottomRectangle, bottomGradientStart, bottomGradientEnd, mode);

			g.FillRectangle(innerBorderGradientTopBrush, topRectangle);
			g.FillRectangle(innerBorderGradientBottomBrush, bottomRectangle);
		}

		public static void DrawRaisedLineMini(Graphics g, int x, int y, int size, Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Horizontal:
				{
					g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.ControlDark)), x, y, x + size, y);
					g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.ControlLightLight)), x, y + 1, x + size, y + 1);
					break;
				}
			case Orientation.Vertical:
				{
					g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.ControlDark)), x, y, x, y + size);
					g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.ControlLightLight)), x + 1, y, x + 1, y + size);
					break;
				}
			}
		}
	}
}