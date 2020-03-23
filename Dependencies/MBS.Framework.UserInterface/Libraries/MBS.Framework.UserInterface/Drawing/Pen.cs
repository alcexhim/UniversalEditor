using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Drawing
{
	public enum PenStyle
	{
		Custom = -1,
		Solid = 1,
		Dash,
		DashDot,
		DashDotDot,
		Dot
	}
	public struct Pen
	{
		private PenStyle mvarStyle;
		public PenStyle Style { get { return mvarStyle; } set { mvarStyle = value; } }

		private Measurement mvarWidth;
		public Measurement Width { get { return mvarWidth; } set { mvarWidth = value; } }

		private Color mvarColor;
		public Color Color { get { return mvarColor; } set { mvarColor = value; } }

		public Pen(Color color, Measurement width = default(Measurement), PenStyle style = PenStyle.Solid)
		{
			mvarStyle = style;
			if (width.Equals(default(Measurement))) width = new Measurement(1.0, MeasurementUnit.Pixel);
			mvarWidth = width;
			mvarColor = color;
		}
	}
}
