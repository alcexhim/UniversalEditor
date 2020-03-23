using System;
namespace MBS.Framework.Drawing
{
	public struct Dimension3D : ICloneable
	{
		public static readonly Dimension3D Empty;

		public double Width { get; set; }
		public double Height { get; set; }
		public double Depth { get; set; }

		public Dimension3D(double width, double height, double depth)
		{
			Width = width;
			Height = height;
			Depth = depth;
		}

		public object Clone()
		{
			Dimension3D clone = new Dimension3D(Width, Height, Depth);
			return clone;
		}
	}
}
