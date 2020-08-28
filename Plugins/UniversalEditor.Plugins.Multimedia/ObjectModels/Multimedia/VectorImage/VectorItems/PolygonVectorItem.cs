//
//  Polygon.cs
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
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems
{
	public class PolygonVectorItem : VectorItem
	{
		public double X { get; set; } = 0;
		public double Y { get; set; } = 0;

		public VectorImageStyle Style { get; set; } = new VectorImageStyle();

		public List<PositionVector2> Points { get; } = new List<PositionVector2>();

		public override object Clone()
		{
			PolygonVectorItem clone = new PolygonVectorItem();
			clone.X = X;
			clone.Y = Y;
			clone.Style = Style.Clone() as VectorImageStyle;
			for (int i = 0; i < Points.Count; i++)
			{
				clone.Points.Add((PositionVector2)Points[i].Clone());
			}
			return clone;
		}
	}
}
