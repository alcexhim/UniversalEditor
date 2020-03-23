using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public enum ThreeDBorderStyle
	{
		Inset,
		Outset
	}
	public enum ThreeDBorderThickness
	{
		Single,
		Double
	}
	public static class GraphicsExtensions
	{
		private static Bitmap pixelBitmap = new Bitmap(1, 1);

		public static void DrawPixel(this Graphics graphics, Color color, int x, int y)
		{
			pixelBitmap.SetPixel(0, 0, color);
			graphics.DrawImage(pixelBitmap, x, y, 1, 1);
		}

		public static void DrawThreeDRaisedBorder(this Graphics graphics, Rectangle rectangle, ThreeDBorderThickness thickness, ThreeDBorderStyle style)
		{
			Pen penOuterTopLeft = DrawingTools.Pens.ControlPen;
			Pen penInnerTopLeft = DrawingTools.Pens.HighlightPen;
			Pen penInnerBottomRight = DrawingTools.Pens.ShadowPen;
			Pen penOuterBottomRight = DrawingTools.Pens.DarkShadowPen;

			switch (style)
			{
			case ThreeDBorderStyle.Inset:
				{
					penInnerBottomRight = DrawingTools.Pens.ControlPen;
					penOuterBottomRight = DrawingTools.Pens.HighlightPen;
					penOuterTopLeft = DrawingTools.Pens.ShadowPen;
					penInnerTopLeft = DrawingTools.Pens.DarkShadowPen;
					break;
				}
			case ThreeDBorderStyle.Outset:
				{
					penOuterTopLeft = DrawingTools.Pens.ControlPen;
					penInnerTopLeft = DrawingTools.Pens.HighlightPen;
					penInnerBottomRight = DrawingTools.Pens.ShadowPen;
					penOuterBottomRight = DrawingTools.Pens.DarkShadowPen;
					break;
				}
			}

			// outer top
			graphics.DrawLine(penOuterTopLeft, rectangle.X, rectangle.Y, rectangle.Right - 2, rectangle.Y);
			// outer left
			graphics.DrawLine(penOuterTopLeft, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
			if (thickness == ThreeDBorderThickness.Double)
			{
				// inner top
				graphics.DrawLine(penInnerTopLeft, rectangle.X + 1, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
				// inner left
				graphics.DrawLine(penInnerTopLeft, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
				// inner bottom
				graphics.DrawLine(penInnerBottomRight, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				// inner right
				graphics.DrawLine(penInnerBottomRight, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 2);
			}
			// outer bottom
			graphics.DrawLine(penOuterBottomRight, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
			// outer right
			graphics.DrawLine(penOuterBottomRight, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
		}

		public static void FillArrow(this Graphics graphics, Rectangle rectangle, System.Windows.Forms.ArrowDirection direction, Brush brush)
		{
			Point point = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);
			Point[] points = null;
			switch (direction)
			{
			case System.Windows.Forms.ArrowDirection.Left:
				{
					points = new Point[]
					{
						new Point(point.X + 2, point.Y - 4),
						new Point(point.X + 2, point.Y + 4),
						new Point(point.X - 2, point.Y)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Up:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y + 1),
						new Point(point.X + 3, point.Y + 1),
						new Point(point.X, point.Y - 2)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Right:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y - 4),
						new Point(point.X - 2, point.Y + 4),
						new Point(point.X + 2, point.Y)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Down:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y - 1),
						new Point(point.X + 3, point.Y - 1),
						new Point(point.X, point.Y + 2)
					};
					break;
				}
			}
			graphics.FillPolygon(brush, points);
		}
		public static void DrawArrow(this Graphics graphics, Rectangle rectangle, System.Windows.Forms.ArrowDirection direction, Pen pen)
		{
			Point point = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);
			Point[] points = null;
			switch (direction)
			{
			case System.Windows.Forms.ArrowDirection.Left:
				{
					points = new Point[]
					{
						new Point(point.X + 2, point.Y - 4),
						new Point(point.X + 2, point.Y + 4),
						new Point(point.X - 2, point.Y)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Up:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y + 1),
						new Point(point.X + 3, point.Y + 1),
						new Point(point.X, point.Y - 2)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Right:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y - 4),
						new Point(point.X - 2, point.Y + 4),
						new Point(point.X + 2, point.Y)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Down:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y - 1),
						new Point(point.X + 3, point.Y - 1),
						new Point(point.X, point.Y + 2)
					};
					break;
				}
			}
			graphics.DrawPolygon(pen, points);
		}



		public static void FillEquilateralTriangle(this Graphics graphics, Brush brush, Rectangle rectangle, System.Windows.Forms.ArrowDirection direction)
		{
			Point[] points = null;
			switch (direction)
			{
			case System.Windows.Forms.ArrowDirection.Left:
				{
					points = new Point[]
					{
						new Point(rectangle.X, rectangle.Bottom / 2),
						new Point(rectangle.Right / 2, rectangle.Y ),
						new Point(rectangle.Right - 1, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Up:
				{
					points = new Point[]
					{
						new Point(rectangle.Right / 2, rectangle.Y),
						new Point(rectangle.X, rectangle.Bottom - 1),
						new Point(rectangle.Right - 1, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Right:
				{
					points = new Point[]
					{
						new Point(rectangle.Right - 1, rectangle.Bottom / 2),
						new Point(rectangle.X, rectangle.Y),
						new Point(rectangle.X, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Down:
				{
					points = new Point[]
					{
						new Point(rectangle.Right / 2, rectangle.Bottom - 1),
						new Point(rectangle.X, rectangle.Y),
						new Point(rectangle.Right - 1, rectangle.Y)
					};
					break;
				}
			}
			graphics.FillPolygon(brush, points);
		}
		public static void DrawEquilateralTriangle(this Graphics graphics, Pen pen, Rectangle rectangle, System.Windows.Forms.ArrowDirection direction)
		{
			Point[] points = null;
			switch (direction)
			{
			case System.Windows.Forms.ArrowDirection.Left:
				{
					points = new Point[]
					{
						new Point(rectangle.X, rectangle.Y + (rectangle.Height / 2)),
						new Point(rectangle.Right - 1, rectangle.Y ),
						new Point(rectangle.Right - 1, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Up:
				{
					points = new Point[]
					{
						new Point(rectangle.X + (rectangle.Width / 2), rectangle.Y),
						new Point(rectangle.X, rectangle.Bottom - 1),
						new Point(rectangle.Right - 1, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Right:
				{
					points = new Point[]
					{
						new Point(rectangle.Right - 1, rectangle.Y + (rectangle.Height / 2)),
						new Point(rectangle.X, rectangle.Y ),
						new Point(rectangle.X, rectangle.Bottom - 1)
					};
					break;
				}
			case System.Windows.Forms.ArrowDirection.Down:
				{
					points = new Point[]
					{
						new Point(rectangle.Right / 2, rectangle.Bottom - 1),
						new Point(rectangle.X, rectangle.Y),
						new Point(rectangle.Right - 1, rectangle.Y)
					};
					break;
				}
			}
			graphics.DrawPolygon(pen, points);
		}

		public static void DrawWavyLine(this Graphics graphics, Pen pen, Point ptStart, Point ptEnd)
		{
			DrawWavyLine(graphics, pen, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
		}
		public static void DrawWavyLine(this Graphics graphics, Pen pen, int x1, int y1, int x2, int y2)
		{
			int o = 0;
			int d = 1;
			for (int x = x1; x <= x2; x += 2)
			{
				graphics.DrawLine(pen, x, y1 - o + 1, x + 1, y1 - o + 1);
				o += d;
				if (o == 1 || o == 0) d *= -1;
			}
		}

		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle)
		{
			DrawRoundedRectangle(graphics, pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}
		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius)
		{
			DrawRoundedRectangle(graphics, pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius);
		}
		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radiusTopLeft, int radiusTopRight, int radiusBottomLeft, int radiusBottomRight)
		{
			DrawRoundedRectangle(graphics, pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radiusTopLeft, radiusTopRight, radiusBottomLeft, radiusBottomRight);
		}
		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, int x, int y, int width, int height)
		{
			DrawRoundedRectangle(graphics, pen, x, y, width, height, 1);
		}
		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, int x, int y, int width, int height, int radius)
		{
			DrawRoundedRectangle(graphics, pen, x, y, width, height, radius, radius, radius, radius);
		}
		public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, int x, int y, int width, int height, int radiusTopLeft, int radiusTopRight, int radiusBottomLeft, int radiusBottomRight)
		{
			int right = x + width;
			int bottom = y + height;

			// top line
			graphics.DrawLine(pen, x + radiusTopLeft, y, right - radiusTopRight - 1, y);
			// top-left connector
			graphics.DrawLine(pen, x, y + radiusTopLeft, x + radiusTopLeft, y); //added
			// left line
			graphics.DrawLine(pen, x, y + radiusTopLeft, x, bottom - radiusBottomRight - 1);
			// bottom line
			graphics.DrawLine(pen, x + radiusTopLeft, bottom - radiusBottomLeft, right - radiusBottomRight, bottom - radiusBottomLeft);
			graphics.DrawLine(pen, right - radiusTopRight - 1, y, right - 1, y + radiusTopRight); //added
			graphics.DrawLine(pen, right - radiusBottomRight, y + radiusTopLeft, right - radiusBottomRight, bottom - radiusBottomRight);
		}

		public static void DrawText(this Graphics graphics, string text, System.Drawing.Font font, System.Drawing.Rectangle bounds, System.Drawing.Color foreColor, RotateFlipType rotateFlip)
		{
			DrawText(graphics, text, font, bounds, foreColor, System.Drawing.Color.Transparent, rotateFlip, System.Windows.Forms.TextFormatFlags.Default);
		}
		public static void DrawText(this Graphics graphics, string text, System.Drawing.Font font, System.Drawing.Rectangle bounds, System.Drawing.Color foreColor, RotateFlipType rotateFlip, System.Windows.Forms.TextFormatFlags flags)
		{
			DrawText(graphics, text, font, bounds, foreColor, System.Drawing.Color.Transparent, rotateFlip, flags);
		}
		public static void DrawText(this Graphics graphics, string text, System.Drawing.Font font, System.Drawing.Rectangle bounds, System.Drawing.Color foreColor, System.Drawing.Color backColor, RotateFlipType rotateFlip, System.Windows.Forms.TextFormatFlags flags)
		{
			if (rotateFlip == RotateFlipType.RotateNoneFlipNone)
			{
				System.Windows.Forms.TextRenderer.DrawText(graphics, text, font, bounds, foreColor, backColor, flags);
				return;
			}

			System.Drawing.Bitmap bmp = null;
			System.Drawing.Rectangle rectTextSource;
			if (rotateFlip == RotateFlipType.Rotate270FlipNone || rotateFlip == RotateFlipType.Rotate270FlipX || rotateFlip == RotateFlipType.Rotate270FlipXY || rotateFlip == RotateFlipType.Rotate270FlipY || rotateFlip == RotateFlipType.Rotate90FlipNone || rotateFlip == RotateFlipType.Rotate90FlipX || rotateFlip == RotateFlipType.Rotate90FlipXY || rotateFlip == RotateFlipType.Rotate90FlipY)
			{
				bmp = new System.Drawing.Bitmap(bounds.Height, bounds.Width);
				rectTextSource = new System.Drawing.Rectangle(0, 0, bounds.Height, bounds.Width);
			}
			else
			{
				bmp = new System.Drawing.Bitmap(bounds.Width, bounds.Height);
				rectTextSource = new System.Drawing.Rectangle(0, 0, bounds.Width, bounds.Height);
			}
			System.Drawing.Graphics gra = System.Drawing.Graphics.FromImage(bmp);

			bool useTextRenderer = false;
			if (useTextRenderer)
			{
				System.Windows.Forms.TextRenderer.DrawText(gra, text, font, rectTextSource, foreColor, backColor, flags);
			}
			else
			{
				// TODO: complete conversion of TextFormatFlags into StringFormatFlags
				StringFormatFlags formatFlags = (StringFormatFlags)0;
				if ((flags & System.Windows.Forms.TextFormatFlags.SingleLine) == System.Windows.Forms.TextFormatFlags.SingleLine) formatFlags &= StringFormatFlags.NoWrap;
				StringFormat format = new StringFormat(formatFlags);
				gra.DrawString(text, font, new SolidBrush(foreColor), rectTextSource, format);
			}
			gra.Flush();

			bmp.RotateFlip(rotateFlip);

			Rectangle rectBounds = bounds;
			if (rotateFlip == RotateFlipType.Rotate270FlipNone || rotateFlip == RotateFlipType.Rotate270FlipX || rotateFlip == RotateFlipType.Rotate270FlipXY || rotateFlip == RotateFlipType.Rotate270FlipY || rotateFlip == RotateFlipType.Rotate90FlipNone || rotateFlip == RotateFlipType.Rotate90FlipX || rotateFlip == RotateFlipType.Rotate90FlipXY || rotateFlip == RotateFlipType.Rotate90FlipY)
			{
				rectBounds.Height = bounds.Width;
				rectBounds.Width = bounds.Height;
			}
			graphics.DrawImage(bmp, bounds);
		}
	}
}
