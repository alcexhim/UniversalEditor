using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public abstract class Fill : ICloneable
	{
		public abstract object Clone();
	}
	public class SolidFill : Fill
	{
		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		public SolidFill()
		{

		}
		public SolidFill(string color)
		{
			mvarColor = color;
		}

		public override object Clone()
		{
			SolidFill clone = new SolidFill();
			clone.Color = (mvarColor.Clone() as string);
			return clone;
		}
	}
	public enum LinearGradientFillOrientation
	{
		BackwardDiagonal,
		ForwardDiagonal,
		Horizontal,
		Vertical
	}
	public class LinearGradientFillColorStop : ICloneable
	{
		public class LinearGradientFillColorStopCollection
			: System.Collections.ObjectModel.Collection<LinearGradientFillColorStop>
		{

		}

		private string mvarPosition = String.Empty;
		public string Position { get { return mvarPosition; } set { mvarPosition = value; } }

		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		public LinearGradientFillColorStop()
		{
		}
		public LinearGradientFillColorStop(string position, string color)
		{
			mvarPosition = position;
			mvarColor = color;
		}

		public object Clone()
		{
			LinearGradientFillColorStop clone = new LinearGradientFillColorStop();
			clone.Color = (mvarColor.Clone() as string);
			clone.Position = (mvarPosition.Clone() as string);
			return clone;
		}
	}
	public class LinearGradientFill : Fill
	{
		private LinearGradientFillOrientation mvarOrientation = LinearGradientFillOrientation.Horizontal;
		public LinearGradientFillOrientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }

		private LinearGradientFillColorStop.LinearGradientFillColorStopCollection mvarColorStops = new LinearGradientFillColorStop.LinearGradientFillColorStopCollection();
		public LinearGradientFillColorStop.LinearGradientFillColorStopCollection ColorStops { get { return mvarColorStops; } }

		public override object Clone()
		{
			LinearGradientFill clone = new LinearGradientFill();
			clone.Orientation = mvarOrientation;
			foreach (LinearGradientFillColorStop item in mvarColorStops)
			{
				clone.ColorStops.Add(item.Clone() as LinearGradientFillColorStop);
			}
			return clone;
		}
	}
}
