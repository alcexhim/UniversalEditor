using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web
{
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
