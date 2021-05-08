//
//  Measurement.cs - provides a tuple and enumeration describing a measurement value
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Describes the unit of measure in a <see cref="Measurement" />.
	/// </summary>
	public enum MeasurementUnit
	{
		Percentage,
		Pixel,
		Unknown
	}
	/// <summary>
	/// Represents a measurement of a particular value in a specific unit of measure.
	/// </summary>
	/// <remarks>
	/// A similar structure or class has been developed for the UniversalEditor Web Plugin for CSS support; we may wish to merge the two into a single
	/// structure or class and place it in MBS.Framework.Drawing for easier re-use in other projects.
	/// </remarks>
	public struct Measurement
	{
		/// <summary>
		/// Gets the unit of measure for this <see cref="Measurement" />.
		/// </summary>
		/// <value>The unit of measure for this <see cref="Measurement" />.</value>
		public MeasurementUnit Unit { get; }
		/// <summary>
		/// Gets or sets the value of this <see cref="Measurement" />.
		/// </summary>
		/// <value>The value of this <see cref="Measurement" />.</value>
		public double Value { get; set; }

		private bool mvarIsNotEmpty;
		public bool IsEmpty { get { return !mvarIsNotEmpty; } }

		public Measurement(string value)
		{
			if (value.EndsWith("px"))
			{
				string v = value.Substring(0, value.Length - 2);
				Value = double.Parse(v);
				Unit = MeasurementUnit.Pixel;
			}
			else if (value.EndsWith("%"))
			{
				string v = value.Substring(0, value.Length - 1);
				Value = double.Parse(v);
				Unit = MeasurementUnit.Percentage;
			}
			throw new FormatException("The string could not be parsed as a valid Measurement.");
		}
		public Measurement(double value)
		{
			mvarIsNotEmpty = true;
			Value = value;
			Unit = MeasurementUnit.Pixel;
		}
		public Measurement(double value, MeasurementUnit unit)
		{
			mvarIsNotEmpty = true;
			Value = value;
			Unit = unit;
		}
	}
}
