using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public abstract class Outline : ICloneable
	{
		private float mvarWidth = 1.0f;
		public float Width { get { return mvarWidth; } set { mvarWidth = value; } }

		public abstract object Clone();
	}
	public class SolidOutline : Outline
	{
		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		public override object Clone()
		{
			SolidOutline clone = new SolidOutline();
			clone.Color = (mvarColor.Clone() as string);
			clone.Width = this.Width;
			return clone;
		}
	}

	public enum ThreeDOutlineType
	{
		Outset,
		Inset
	}
	public class ThreeDOutline : Outline
	{
		private ThreeDOutlineType mvarType = ThreeDOutlineType.Outset;
		public ThreeDOutlineType Type { get { return mvarType; } set { mvarType = value; } }


		private string mvarLightColor = String.Empty;
		public string LightColor { get { return mvarLightColor; } set { mvarLightColor = value; } }

		private string mvarDarkColor = String.Empty;
		public string DarkColor { get { return mvarDarkColor; } set { mvarDarkColor = value; } }

		public override object Clone()
		{
			ThreeDOutline clone = new ThreeDOutline();
			clone.DarkColor = (mvarDarkColor.Clone() as string);
			clone.LightColor = (mvarLightColor.Clone() as string);
			clone.Type = mvarType;
			clone.Width = this.Width;
			return clone;
		}

	}
}
