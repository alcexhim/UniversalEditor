using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UniversalWidgetToolkit.Drawing;

namespace UniversalWidgetToolkit.Engines.Win32.Drawing
{
	public class Win32Graphics : Graphics
	{
		private IntPtr mvarHwnd = IntPtr.Zero;
		private IntPtr mvarHdc = IntPtr.Zero;
		private System.Drawing.Graphics mvarGraphics = null;

		public Win32Graphics(IntPtr hWnd)
		{
			mvarHwnd = hWnd;
			mvarHdc = Internal.Windows.Methods.User32.GetDC(mvarHwnd);
			mvarGraphics = System.Drawing.Graphics.FromHdc(mvarHdc);
		}

		protected override void DrawLineInternal(Pen pen, double x1, double y1, double x2, double y2)
		{
			mvarGraphics.DrawLine(PenToNativePen(pen), (float)x1, (float)y1, (float)x2, (float)y2);
		}

		protected override void DrawRectangleInternal(Pen pen, double x, double y, double width, double height)
		{
			mvarGraphics.DrawRectangle(PenToNativePen(pen), (float)x, (float)y, (float)width, (float)height);
		}

		private System.Drawing.Pen PenToNativePen(Pen pen)
		{
			System.Drawing.Pen retval = new System.Drawing.Pen(ColorToNativeColor(pen.Color), (float)(pen.Width.ConvertTo(MeasurementUnit.Pixel).Value));
			retval.DashStyle = PenStyleToDashStyle(pen.Style);
			return retval;
		}

		private System.Drawing.Color ColorToNativeColor(Color color)
		{
			return System.Drawing.Color.FromArgb(color.GetAlphaByte(), color.GetRedByte(), color.GetGreenByte(), color.GetBlueByte());
		}

		private System.Drawing.Drawing2D.DashStyle PenStyleToDashStyle(PenStyle penStyle)
		{
			switch (penStyle)
			{
				case PenStyle.Custom: return System.Drawing.Drawing2D.DashStyle.Custom;
				case PenStyle.Dash: return System.Drawing.Drawing2D.DashStyle.Dash;
				case PenStyle.DashDot: return System.Drawing.Drawing2D.DashStyle.DashDot;
				case PenStyle.DashDotDot: return System.Drawing.Drawing2D.DashStyle.DashDotDot;
				case PenStyle.Dot: return System.Drawing.Drawing2D.DashStyle.Dot;
				case PenStyle.Solid: return System.Drawing.Drawing2D.DashStyle.Solid;
			}
			return System.Drawing.Drawing2D.DashStyle.Solid;
		}

		protected override void FillRectangleInternal(Brush brush, double x, double y, double width, double height)
		{
			mvarGraphics.FillRectangle(BrushToNativeBrush(brush), (float)x, (float)y, (float)width, (float)height);
		}

		protected override void DrawTextInternal(string value, Font font, Rectangle rectangle, Color color, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			System.Windows.Forms.TextFormatFlags flags = System.Windows.Forms.TextFormatFlags.Left;
			switch (horizontalAlignment)
			{
				case HorizontalAlignment.Center:
				{
					flags |= System.Windows.Forms.TextFormatFlags.HorizontalCenter;
					break;
				}
				case HorizontalAlignment.Right:
				{
					flags |= System.Windows.Forms.TextFormatFlags.Right;
					break;
				}
			}
			switch (verticalAlignment)
			{
				case VerticalAlignment.Bottom:
				{
					flags |= System.Windows.Forms.TextFormatFlags.Bottom;
					break;
				}
				case VerticalAlignment.Middle:
				{
					flags |= System.Windows.Forms.TextFormatFlags.VerticalCenter;
					break;
				}
			}
			System.Windows.Forms.TextRenderer.DrawText(mvarGraphics, value, FontToNativeFont(font), RectangleToNativeRectangle(rectangle), ColorToNativeColor(color), flags);
		}

		private System.Drawing.Font FontToNativeFont(Font font)
		{
			// TODO: get rid of this hardcoding and actually create real Font objects from system fonts
			if (font == SystemFonts.MenuFont)
			{
				return System.Drawing.SystemFonts.MenuFont;
			}
			else if (font == null)
			{
				return System.Drawing.SystemFonts.DefaultFont;
			}
			return new System.Drawing.Font(font.FamilyName, (float)font.Size, GetNativeFontStyle(font));
		}

		private System.Drawing.FontStyle GetNativeFontStyle(Font font)
		{
			System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
			if (font.Italic) style |= System.Drawing.FontStyle.Italic;
			if (font.Weight >= FontWeights.Bold) style |= System.Drawing.FontStyle.Bold;
			return style;
		}

		private System.Drawing.Brush BrushToNativeBrush(Brush brush)
		{
			if (brush is SolidBrush)
			{
				return new System.Drawing.SolidBrush(ColorToNativeColor((brush as SolidBrush).Color));
			}
			else if (brush is LinearGradientBrush)
			{
				LinearGradientBrush b = (brush as LinearGradientBrush);
				System.Drawing.Drawing2D.LinearGradientBrush lgb = new System.Drawing.Drawing2D.LinearGradientBrush(RectangleToNativeRectangleF(b.Bounds), ColorToNativeColor(b.ColorStops[0].Color), ColorToNativeColor(b.ColorStops[b.ColorStops.Count - 1].Color), LinearGradientBrushOrientationToLinearGradientMode(b.Orientation));
				if (b.ColorStops.Count > 2)
				{
					List<System.Drawing.Color> colorList = new List<System.Drawing.Color>();
					List<float> positionList = new List<float>();

					for (int i = 0; i < b.ColorStops.Count; i++)
					{
						colorList.Add(ColorToNativeColor(b.ColorStops[i].Color));
						positionList.Add((float)(b.ColorStops[i].Position.ConvertTo(MeasurementUnit.Decimal).Value));
					}

					System.Drawing.Drawing2D.ColorBlend blend = new System.Drawing.Drawing2D.ColorBlend(b.ColorStops.Count);
					blend.Colors = colorList.ToArray();
					blend.Positions = positionList.ToArray();
					lgb.InterpolationColors = blend;
				}
				return lgb;
			}
			return null;
		}

		private System.Drawing.Drawing2D.LinearGradientMode LinearGradientBrushOrientationToLinearGradientMode(LinearGradientBrushOrientation orientation)
		{
			switch (orientation)
			{
				case LinearGradientBrushOrientation.BackwardDiagonal: return System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
				case LinearGradientBrushOrientation.ForwardDiagonal: return System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
				case LinearGradientBrushOrientation.Horizontal: return System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
				case LinearGradientBrushOrientation.Vertical: return System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			}
			return System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
		}

		private System.Drawing.Rectangle RectangleToNativeRectangle(Rectangle rectangle)
		{
			return new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
		}
		private System.Drawing.RectangleF RectangleToNativeRectangleF(Rectangle rectangle)
		{
			return new System.Drawing.RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height);
		}
	}
}
