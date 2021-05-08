//
//  BezierCurve.cs - describes points for a Bezier curve
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace Neo
{
	/// <summary>
	/// Describes points for a Bezier curve.
	/// </summary>
	public class BezierCurve
	{
		public class BezierCurveCollection
			: System.Collections.ObjectModel.Collection<BezierCurve>
		{
		}

		public string Name { get; set; } = String.Empty;
		public byte X1 { get; set; } = 0;
		public byte X2 { get; set; } = 0;
		public byte Y1 { get; set; } = 0;
		public byte Y2 { get; set; } = 0;

		public override string ToString()
		{
			return Name + ": (" + X1.ToString() + ", " + Y1.ToString() + ")-(" + X2.ToString() + ", " + Y2.ToString();
		}
	}
}
