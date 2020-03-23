using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing.Drawing2D;

namespace MBS.Framework.UserInterface.Drawing
{
	public class TextureBrush : Brush
	{
		public Image Image { get; private set; } = null;

		public WrapMode WrapMode { get; set; } = WrapMode.Tile;
		/*
		public Matrix Transform
		{
			get
			{
				throw null;
			}
			set
			{
			}
		}
		*/

		public TextureBrush(Image image)
		{
			Image = image;
		}

		public TextureBrush(Image image, WrapMode wrapMode)
		{
			Image = image;
			WrapMode = wrapMode;
		}

		public TextureBrush(Image image, WrapMode wrapMode, Rectangle dstRect)
		{
			Image = image;
			WrapMode = wrapMode;
		}

		public TextureBrush(Image image, Rectangle dstRect)
		{
			Image = image;
		}
		/*
		[System.MonoLimitation("ImageAttributes are ignored when using libgdiplus")]
		public TextureBrush(Image image, Rectangle dstRect, ImageAttributes imageAttr)
		{
		}

		[System.MonoLimitation("ImageAttributes are ignored when using libgdiplus")]
		public TextureBrush(Image image, RectangleF dstRect, ImageAttributes imageAttr)
		{
		}
			
		public override object Clone()
		{
			throw null;
		}

		public void MultiplyTransform(Matrix matrix)
		{
		}

		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
		}

		public void ResetTransform()
		{
		}

		public void RotateTransform(float angle)
		{
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
		}

		public void ScaleTransform(float sx, float sy)
		{
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
		}

		public void TranslateTransform(float dx, float dy)
		{
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
		}
		*/
	}
	public enum LinearGradientBrushOrientation
	{
		BackwardDiagonal,
		ForwardDiagonal,
		Horizontal,
		Vertical
	}
	public class LinearGradientBrushColorStop
	{
		public class LinearGradientBrushColorStopCollection
			: System.Collections.ObjectModel.Collection<LinearGradientBrushColorStop>
		{
			public LinearGradientBrushColorStop Add(Color color, double percentagePosition)
			{
				return Add(color, new Measurement(percentagePosition, MeasurementUnit.Percentage));
			}
			public LinearGradientBrushColorStop Add(Color color, Measurement position)
			{
				LinearGradientBrushColorStop item = new LinearGradientBrushColorStop();
				item.Color = color;
				item.Position = position;
				Add(item);
				return item;
			}
		}
		public Color Color { get; set; } = Color.Empty;

		private Measurement mvarPosition = Measurement.Empty;
		public Measurement Position { get { return mvarPosition; } set { mvarPosition = value; } }
	}
	public class LinearGradientBrush : Brush
	{
		private Rectangle mvarBounds = new Rectangle();
		public Rectangle Bounds { get { return mvarBounds; } set { mvarBounds = value; } }

		private LinearGradientBrushColorStop.LinearGradientBrushColorStopCollection mvarColorStops = new LinearGradientBrushColorStop.LinearGradientBrushColorStopCollection();
		public LinearGradientBrushColorStop.LinearGradientBrushColorStopCollection ColorStops { get { return mvarColorStops; } }

		private LinearGradientBrushOrientation mvarOrientation = LinearGradientBrushOrientation.Horizontal;
		public LinearGradientBrushOrientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }

		public LinearGradientBrush(Rectangle bounds, LinearGradientBrushOrientation orientation = LinearGradientBrushOrientation.Horizontal)
		{
			mvarBounds = bounds;
			mvarOrientation = orientation;
		}
		public LinearGradientBrush(Rectangle bounds, Color startColor, Color endColor, LinearGradientBrushOrientation orientation = LinearGradientBrushOrientation.Horizontal)
		{
			mvarBounds = bounds;
			mvarColorStops.Add(startColor, new Measurement(0, MeasurementUnit.Percentage));
			mvarColorStops.Add(endColor, 1.0);
			mvarOrientation = orientation;
		}
	}
	public class SolidBrush : Brush
	{
		private Color mvarColor = Color.Empty;
		public Color Color { get { return mvarColor; } }
		public SolidBrush(Color color)
		{
			mvarColor = color;
		}
	}
	public abstract class Brush
	{
	}
}
