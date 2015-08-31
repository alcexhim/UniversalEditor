using System;
namespace UniversalEditor
{
	public struct TextureVector2
	{
		private double mvarU;
		private double mvarV;
		public double U
		{
			get
			{
				return this.mvarU;
			}
			set
			{
				this.mvarU = value;
			}
		}
		public double V
		{
			get
			{
				return this.mvarV;
			}
			set
			{
				this.mvarV = value;
			}
		}
		public TextureVector2(float u, float v)
		{
			this.mvarU = u;
			this.mvarV = v;
		}
        public TextureVector2(double u, double v)
        {
            this.mvarU = u;
            this.mvarV = v;
        }
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.mvarU, this.mvarV);
		}
	}
}
