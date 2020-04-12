//
//  Measurement.cs - represents a tuple of a numeric value and a unit of measure
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

namespace UniversalEditor.ObjectModels.Web
{
	/// <summary>
	/// Represents a tuple of a numeric value and a unit of measure.
	/// </summary>
	public struct Measurement
    {
        public static readonly Measurement Empty;

        public Measurement(string value)
        {
            DoubleStringSplitterResult dssr = NumericStringSplitter.SplitDoubleStringParts(value);
            mvarValue = dssr.DoublePart;
            switch (dssr.StringPart.ToLower())
            {
                case "cm": mvarUnit = MeasurementUnit.Cm; break;
                case "em": mvarUnit = MeasurementUnit.Em; break;
                case "ex": mvarUnit = MeasurementUnit.Ex; break;
                case "in": mvarUnit = MeasurementUnit.Inch; break;
                case "mm": mvarUnit = MeasurementUnit.Mm; break;
                case "%": mvarUnit = MeasurementUnit.Percentage; break;
                case "pc": mvarUnit = MeasurementUnit.Pica; break;
                case "px": mvarUnit = MeasurementUnit.Pixel; break;
                case "pt": mvarUnit = MeasurementUnit.Point; break;
                default: mvarUnit = MeasurementUnit.Pixel; break;
            }
            mvarIsFull = true;
        }
        public Measurement(double value, MeasurementUnit unit)
        {
            mvarUnit = unit;
            mvarValue = value;
            mvarIsFull = true;
        }

        private double mvarValue;
        public double Value { get { return mvarValue; } set { mvarValue = value; } }

        private MeasurementUnit mvarUnit;
        public MeasurementUnit Unit { get { return mvarUnit; } set { mvarUnit = value; } }

        private bool mvarIsFull;
        public bool IsEmpty { get { return !mvarIsFull; } }
    }
}
