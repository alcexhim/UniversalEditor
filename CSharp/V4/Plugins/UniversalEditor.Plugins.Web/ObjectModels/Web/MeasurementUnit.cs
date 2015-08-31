using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web
{
    public enum MeasurementUnit
    {
        /// <summary>Measurement is in pixels.</summary>
        Pixel = 1,
        /// <summary>Measurement is in points. A point represents 1/72 of an inch.</summary>
        Point,
        /// <summary>Measurement is in picas. A pica represents 12 points.</summary>
        Pica,
        /// <summary>Measurement is in inches.</summary>
        Inch,
        /// <summary>Measurement is in millimeters.</summary>
        Mm,
        /// <summary>Measurement is in centimeters.</summary>
        Cm,
        /// <summary>Measurement is a percentage relative to the parent element.</summary>
        Percentage,
        /// <summary>Measurement is relative to the height of the parent element's font.</summary>
        Em,
        /// <summary>Measurement is relative to the height of the lowercase letter x of the parent element's font.</summary>
        Ex
    }
}
