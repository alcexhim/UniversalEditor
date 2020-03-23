//
//  WindowsFormsNativeGraphics.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class WindowsFormsNativeGraphics : Graphics
	{
		public System.Drawing.Graphics Handle { get; private set; } = null;
		public WindowsFormsNativeGraphics(System.Drawing.Graphics g)
		{
			Handle = g;
		}

		private System.Drawing.Image ImageToNativeImage(Image image)
		{
			return (image as WindowsFormsNativeImage).Handle;
		}
		private System.Drawing.Pen PenToNativePen(Pen pen)
		{
			return new System.Drawing.Pen(ColorToNativeColor(pen.Color), (float) pen.Width.ConvertTo(MeasurementUnit.Pixel).Value);
		}

		protected override void DrawImageInternal(Image image, double x, double y, double width, double height)
		{
			Handle.DrawImage(ImageToNativeImage(image), new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height));
		}

		protected override void DrawLineInternal(Pen pen, double x1, double y1, double x2, double y2)
		{
			Handle.DrawLine(PenToNativePen(pen), (float)x1, (float)y1, (float)x2, (float)y2);
		}

		protected override void DrawRectangleInternal(Pen pen, double x, double y, double width, double height)
		{
			Handle.DrawRectangle(PenToNativePen(pen), new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height));
		}

		protected override void FillRectangleInternal(Brush brush, double x, double y, double width, double height)
		{
			Handle.FillRectangle(BrushToNativeBrush(brush), new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height));
		}

		private System.Drawing.StringAlignment AlignmentToStringAlignment(HorizontalAlignment alignment)
		{
			switch (alignment)
			{
				case HorizontalAlignment.Left: return System.Drawing.StringAlignment.Near;
				case HorizontalAlignment.Center: return System.Drawing.StringAlignment.Center;
				case HorizontalAlignment.Right: return System.Drawing.StringAlignment.Far;
			}
			return System.Drawing.StringAlignment.Near;
		}
		private System.Drawing.StringAlignment AlignmentToStringAlignment(VerticalAlignment alignment)
		{
			switch (alignment)
			{
				case VerticalAlignment.Top: return System.Drawing.StringAlignment.Near;
				case VerticalAlignment.Middle: return System.Drawing.StringAlignment.Center;
				case VerticalAlignment.Bottom: return System.Drawing.StringAlignment.Far;
			}
			return System.Drawing.StringAlignment.Near;
		}

		private System.Drawing.Color ColorToNativeColor(Color color)
		{
			return System.Drawing.Color.FromArgb(color.GetAlphaByte(), color.GetRedByte(), color.GetGreenByte(), color.GetBlueByte());
		}
		private System.Drawing.Font FontToNativeFont(Font font)
		{
			return new System.Drawing.Font(font.FamilyName, (float)font.Size);
		}
		private System.Drawing.Brush BrushToNativeBrush(Brush brush)
		{
			if (brush is SolidBrush)
			{
				return new System.Drawing.SolidBrush(ColorToNativeColor((brush as SolidBrush).Color));
			}
			return null;
		}

		private System.Drawing.PointF Vector2DToNativePointF(Vector2D point)
		{
			return new System.Drawing.PointF((float)point.X, (float)point.Y);
		}

		protected override void DrawTextInternal(string value, Font font, Vector2D location, Brush brush, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			System.Drawing.StringFormat format = System.Drawing.StringFormat.GenericDefault;
			format.Alignment = AlignmentToStringAlignment(horizontalAlignment);
			format.LineAlignment = AlignmentToStringAlignment(verticalAlignment);
			Handle.DrawString(value, FontToNativeFont(font), BrushToNativeBrush(brush), Vector2DToNativePointF(location), format);
		}

		protected override void DrawTextInternal(string value, Font font, Rectangle rectangle, Brush brush, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			System.Drawing.StringFormat format = System.Drawing.StringFormat.GenericDefault;
			format.Alignment = AlignmentToStringAlignment(horizontalAlignment);
			format.LineAlignment = AlignmentToStringAlignment(verticalAlignment);
			Handle.DrawString(value, FontToNativeFont(font), BrushToNativeBrush(brush), new System.Drawing.RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height), format);
		}
	}
}
