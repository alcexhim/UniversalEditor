//
//  WaypointEntry.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.ObjectModels.Waypoint
{
	public class WaypointEntry : ICloneable
	{
		public class WaypointEntryCollection
			: System.Collections.ObjectModel.Collection<WaypointEntry>
		{

		}

		public int Index { get; set; } = 0;
		public WaypointType Type { get; set; } = WaypointType.Default;
		public double A { get; set; } = 0.0;

		public double X { get; set; } = 0.0;
		public double Y { get; set; } = 0.0;
		public double Z { get; set; } = 0.0;

		public double Q { get; set; } = 0.0;

		private string DoubleToString(double value)
		{
			return Math.Round(value, 6).ToString().PadRight(8, '0');
		}

		public override string ToString()
		{
			string liststr = String.Empty;
			return String.Format("{0} {1} {2} ({3} {4} {5}) {{ {6} }} {7}", Index, (int)Type, DoubleToString(A), DoubleToString(X), DoubleToString(Y), DoubleToString(Z), liststr, DoubleToString(Q));
		}

		public object Clone()
		{
			WaypointEntry clone = new WaypointEntry();
			clone.Index = Index;
			clone.Type = Type;
			clone.A = A;
			clone.X = X;
			clone.Y = Y;
			clone.Z = Z;
			clone.Q = Q;
			return clone;
		}
	}
}
