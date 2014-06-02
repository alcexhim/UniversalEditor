using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public enum MeasurementUnit
    {
        Percentage,
        Pixel,
        Unknown
    }
    public struct Measurement
    {
        private MeasurementUnit mvarUnit;
        public MeasurementUnit Unit { get { return mvarUnit; } }

        private double mvarValue;
        public double Value { get { return mvarValue; } set { mvarValue = value; } }

        private bool mvarIsNotEmpty;
        public bool IsEmpty { get { return !mvarIsNotEmpty; } }

        public Measurement(string value)
        {
            if (value.EndsWith("px"))
            {
                string v = value.Substring(0, value.Length - 2);
                mvarValue = double.Parse(v);
                mvarUnit = MeasurementUnit.Pixel;
            }
            else if (value.EndsWith("%"))
            {
                string v = value.Substring(0, value.Length - 1);
                mvarValue = double.Parse(v);
                mvarUnit = MeasurementUnit.Percentage;
            }
            throw new FormatException("The string could not be parsed as a valid Measurement.");
        }
        public Measurement(double value)
        {
            mvarIsNotEmpty = true;
            mvarValue = value;
            mvarUnit = MeasurementUnit.Pixel;
        }
        public Measurement(double value, MeasurementUnit unit)
        {
            mvarIsNotEmpty = true;
            mvarValue = value;
            mvarUnit = unit;
        }
    }
}
