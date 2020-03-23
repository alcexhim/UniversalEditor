using System;
using System.Collections.Generic;
using System.Diagnostics;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Engines.GTK.Drawing
{
	public class GTKGraphics : Graphics
	{
		private IntPtr mvarCairoContext = IntPtr.Zero;

		public GTKGraphics(IntPtr cairoContext)
		{
			mvarCairoContext = cairoContext;
		}

		protected override void DrawImageInternal(Image image, double x, double y, double width, double height)
		{

		}

		protected override void DrawLineInternal(Pen pen, double x1, double y1, double x2, double y2)
		{
			SelectPen(pen);

			Internal.Cairo.Methods.cairo_move_to(mvarCairoContext, x1, y1);
			CheckStatus();

			Internal.Cairo.Methods.cairo_line_to(mvarCairoContext, x2, y2);
			CheckStatus();

			Internal.Cairo.Methods.cairo_stroke(mvarCairoContext);
			CheckStatus();
		}

		protected override void DrawTextInternal(string value, Font font, Vector2D location, Brush brush, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			SelectBrush(brush);

			Internal.Cairo.Methods.cairo_move_to(mvarCairoContext, location.X, location.Y);
			CheckStatus();

			SelectFont(font);

			// this is for RENDERING text - textually
			Internal.Cairo.Methods.cairo_show_text(mvarCairoContext, value);
			CheckStatus();

			/*
			 // this is for DRAWING text - graphically
			Internal.Cairo.Methods.cairo_text_path(mvarCairoContext, value);
			CheckStatus();

			Internal.Cairo.Methods.cairo_fill(mvarCairoContext);
			CheckStatus();
			*/

			// there IS a difference - textually-rendered text is selectable when rendering PDFs; graphically-drawn text is not
		}

		private void SelectFont(Font font)
		{
			if (font == null)
				font = Font.FromFamily("Sans", 10);

			Internal.Cairo.Methods.cairo_select_font_face(mvarCairoContext, font.FamilyName, (font.Italic ? Internal.Cairo.Constants.CairoFontSlant.Italic : Internal.Cairo.Constants.CairoFontSlant.Normal), (font.Weight == 800 ? Internal.Cairo.Constants.CairoFontWeight.Bold : Internal.Cairo.Constants.CairoFontWeight.Normal));
			CheckStatus();

			Internal.Cairo.Methods.cairo_set_font_size(mvarCairoContext, font.Size);
			CheckStatus();
		}

		protected override void DrawTextInternal(string value, Font font, Rectangle rectangle, Brush brush, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			SelectBrush(brush);

			Internal.Cairo.Methods.cairo_move_to(mvarCairoContext, rectangle.X, rectangle.Y);
			CheckStatus();

			SelectFont(font);

			// this is for RENDERING text - textually
			Internal.Cairo.Methods.cairo_show_text(mvarCairoContext, value);
			CheckStatus();

			/*
			 // this is for DRAWING text - graphically
			Internal.Cairo.Methods.cairo_text_path(mvarCairoContext, value);
			CheckStatus();

			Internal.Cairo.Methods.cairo_fill(mvarCairoContext);
			CheckStatus();
			*/

			// there IS a difference - textually-rendered text is selectable when rendering PDFs; graphically-drawn text is not
		}

		[DebuggerNonUserCode()]
		private void CheckStatus()
		{
			Internal.Cairo.Constants.CairoStatus status = Internal.Cairo.Methods.cairo_status(mvarCairoContext);
			switch (status)
			{
				case Internal.Cairo.Constants.CairoStatus.Success:
					return;
			}
			throw new Exception(status.ToString());
		}

		private void SelectBrush(Brush brush)
		{
			IntPtr hPattern = IntPtr.Zero;
			if (brush is SolidBrush)
			{
				SolidBrush sb = (brush as SolidBrush);
				hPattern = Internal.Cairo.Methods.cairo_pattern_create_rgba(sb.Color.R, sb.Color.G, sb.Color.B, sb.Color.A);
				CheckStatus();
			}
			else if (brush is TextureBrush)
			{
				TextureBrush tb = (brush as TextureBrush);
				if (tb.Image == null)
				{
					throw new NullReferenceException();
				}

				PatternIndices++;
				hPattern = Internal.Cairo.Methods.cairo_pattern_create_raster_source(new IntPtr(PatternIndices), Internal.Cairo.Constants.CairoContent.Both, tb.Image.Width, tb.Image.Height);
				CheckStatus();

				PatternTextures[PatternIndices] = tb;

				Internal.Cairo.Methods.cairo_raster_source_pattern_set_acquire(hPattern, hPattern_Acquire, hPattern_Release);
				CheckStatus();
			}

			if (hPattern == IntPtr.Zero)
			{
				throw new NotImplementedException("Brush type " + brush.GetType().FullName);
			}
			else
			{
				Internal.Cairo.Methods.cairo_set_source(mvarCairoContext, hPattern);
				CheckStatus();
			}
		}

		private int PatternIndices = 0;
		private static Dictionary<int, TextureBrush> PatternTextures = new Dictionary<int, TextureBrush>();

		private static IntPtr hPattern_Acquire(IntPtr /*cairo_pattern_t*/ pattern, IntPtr /*void*/ callback_data, IntPtr /*cairo_surface_t*/ target,  Internal.Cairo.Structures.cairo_rectangle_int_t extents)
		{
			TextureBrush tb = PatternTextures[callback_data.ToInt32()];
			IntPtr hImage = (tb.Image as CairoImage).Handle;

			return hImage;
		}
		private static void hPattern_Release(IntPtr /*cairo_pattern_t*/ pattern, IntPtr /*void*/ callback_data, IntPtr /*cairo_surface_t*/ target)
		{
		}

		private void SelectPen(Pen pen)
		{
			IntPtr hPattern = Internal.Cairo.Methods.cairo_pattern_create_rgba(pen.Color.R, pen.Color.G, pen.Color.B, pen.Color.A);
			Internal.Cairo.Methods.cairo_set_source(mvarCairoContext, hPattern);
			Internal.Cairo.Methods.cairo_set_line_width(mvarCairoContext, pen.Width.ConvertTo(MeasurementUnit.Pixel).Value);

			CheckStatus();
		}

		protected override void DrawRectangleInternal(Pen pen, double x, double y, double width, double height)
		{
			SelectPen(pen);

			Internal.Cairo.Methods.cairo_rectangle(mvarCairoContext, x, y, width, height);
			CheckStatus();

			Internal.Cairo.Methods.cairo_stroke(mvarCairoContext);
			CheckStatus();
		}

		protected override void FillRectangleInternal(Brush brush, double x, double y, double width, double height)
		{
			SelectBrush(brush);

			Internal.Cairo.Methods.cairo_rectangle(mvarCairoContext, x, y, width, height);
			CheckStatus();

			Internal.Cairo.Methods.cairo_fill(mvarCairoContext);
			CheckStatus();
		}
	}
}
