namespace MBS.Framework.Drawing
{
	public class Dimension2D : Dimension
	{
		private double mvarWidth = 0.0;
		public double Width { get { return mvarWidth; } set { mvarWidth = value; } }
		private double mvarHeight = 0.0;
		public double Height { get { return mvarHeight; } set { mvarHeight = value; } }

		public static Dimension2D Empty = new Dimension2D();

		public Dimension2D()
		{
		}
		public Dimension2D(double width, double height)
		{
			mvarWidth = width;
			mvarHeight = height;
		}

		public Dimension2D Rotate()
		{
			Dimension2D size = this;
			double width = size.Width;
			size.Width = size.Height;
			size.Height = width;
			return size;
		}

		public override string ToString()
		{
			return Width.ToString() + "x" + Height.ToString();
		}
	}
}
