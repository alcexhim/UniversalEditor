using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Drawing
{
	public enum MeasurementUnit
	{
		/// <summary>
		/// Measurement is in pixels.
		/// </summary>
		Pixel = 1,
		/// <summary>
		/// Measurement is in points. A point represents 1/72 of an inch.
		/// </summary>
		Point,
		/// <summary>
		/// Measurement is in picas. A pica represents 12 points.
		/// </summary>
		Pica,
		/// <summary>
		/// Measurement is in inches.
		/// </summary>
		Inch,
		/// <summary>
		/// Measurement is in millimeters.
		/// </summary>
		Mm,
		/// <summary>
		/// Measurement is in centimeters.
		/// </summary>
		Cm,
		/// <summary>
		/// Measurement is a percentage relative to the parent element.
		/// </summary>
		Percentage,
		/// <summary>
		/// Measurement is relative to the height of the parent element's font.
		/// </summary>
		Em,
		/// <summary>
		/// Measurement is relative to the height of the lowercase letter x of the parent element's font.
		/// </summary>
		Ex,
		/// <summary>
		/// Measurement is a decimal representation of a percentage relative to the parent element.
		/// </summary>
		Decimal
	}
	public struct Measurement
	{
		public static readonly Measurement Empty = new Measurement(0, (MeasurementUnit)0);
		public static readonly Measurement Invalid = new Measurement(0, (MeasurementUnit)(-1));

		public bool IsInvalid
		{
			get { return mvarUnit == (MeasurementUnit)(-1); }
		}
		public bool IsEmpty
		{
			get { return mvarUnit == (MeasurementUnit)0; }
		}

		private MeasurementUnit mvarUnit;
		public MeasurementUnit Unit { get { return mvarUnit; } set { mvarUnit = value; } }

		private double mvarValue;
		public double Value { get { return mvarValue; } set { mvarValue = value; } }

		public Measurement(double value, MeasurementUnit unit)
		{
			mvarValue = value;
			mvarUnit = unit;
		}

		public static Measurement FromString(string value)
		{
			string unit = value.Substring(value.Length - 2);
			if (unit.EndsWith("%")) unit = "%";

			double val = Double.Parse(value.Substring(0, value.Length - unit.Length));

			switch (unit.ToLower())
			{
				case "px":
				{
					return new Measurement(val, MeasurementUnit.Percentage);
				}
				case "%":
				{
					return new Measurement(val, MeasurementUnit.Percentage);
				}
			}
			return Measurement.Invalid;
		}

		public Measurement ConvertTo(MeasurementUnit unit)
		{
			double multiplier = 1.0;

			switch (mvarUnit)
			{
				case MeasurementUnit.Decimal:
				{
					switch (unit)
					{
						case MeasurementUnit.Percentage:
						{
							multiplier = 100.0;
							break;
						}
					}
					break;
				}
				case MeasurementUnit.Percentage:
				{
					switch (unit)
					{
						case MeasurementUnit.Decimal:
						{
							multiplier = 0.01;
							break;
						}
					}
					break;
				}
				case MeasurementUnit.Pixel:
				{
					switch (unit)
					{
						case MeasurementUnit.Pixel:
						{
							multiplier = 1.0;
							break;
						}
					}
					break;
				}
			}

			return new Measurement(mvarValue * multiplier, unit);
		}
	}
}
