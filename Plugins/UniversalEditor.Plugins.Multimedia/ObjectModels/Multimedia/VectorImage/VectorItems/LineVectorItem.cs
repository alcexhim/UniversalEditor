//
//  LineVectorItem.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems
{
	public class LineVectorItem : VectorItem
	{
		public Measurement X1 { get; set; } = Measurement.Empty;
		public Measurement X2 { get; set; } = Measurement.Empty;
		public Measurement Y1 { get; set; } = Measurement.Empty;
		public Measurement Y2 { get; set; } = Measurement.Empty;

		protected override bool ContainsInternal(Vector2D point)
		{
			double m = (Y2.GetValue(MeasurementUnit.Pixel) - Y1.GetValue(MeasurementUnit.Pixel)) / (X2.GetValue(MeasurementUnit.Pixel) - X1.GetValue(MeasurementUnit.Pixel));
			double b = (Y2.GetValue(MeasurementUnit.Pixel) - (m * (X2.GetValue(MeasurementUnit.Pixel))));

			double y = (m * point.X) + b;
			double pb = 4;
			return (y >= point.Y - pb) && (y <= point.Y + pb);
		}

		public override object Clone()
		{
			LineVectorItem clone = new LineVectorItem();
			clone.X1 = X1;
			clone.X2 = X2;
			clone.Y1 = Y1;
			clone.Y2 = Y2;
			return clone;
		}
	}
}
