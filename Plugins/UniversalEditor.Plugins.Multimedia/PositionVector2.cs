//
//  PositionVector2.cs - provides a tuple indicating X and Y position
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

namespace UniversalEditor
{
	/// <summary>
	/// Provides a tuple indicating X and Y position.
	/// </summary>
	public struct PositionVector2 : ICloneable
	{
		public bool IsEmpty { get; }

		private PositionVector2(bool empty)
		{
			X = 0;
			Y = 0;
			IsEmpty = empty;
		}

		/// <summary>
		/// Represents the empty <see cref="PositionVector2" />. This field is read-only.
		/// </summary>
		public static readonly PositionVector2 Empty = new PositionVector2(true);

		public double X { get; set; }
		public double Y { get; set; }

		public PositionVector2(float x, float y)
		{
			X = x;
			Y = y;
			IsEmpty = false;
		}
		public PositionVector2(double x, double y)
		{
			X = x;
			Y = y;
			IsEmpty = false;
		}

		public double[] ToDoubleArray()
		{
			return new double[] { X, Y };
		}
		public float[] ToFloatArray()
		{
			return new float[] { (float)X, (float)Y };
		}

		public static PositionVector2 operator +(PositionVector2 left, PositionVector2 right)
		{
			return new PositionVector2(left.X + right.X, left.Y + right.Y);
		}
		public static PositionVector2 operator -(PositionVector2 left, PositionVector2 right)
		{
			return new PositionVector2(left.X - right.X, left.Y - right.Y);
		}
		public static PositionVector2 operator *(PositionVector2 left, PositionVector2 right)
		{
			return new PositionVector2(left.X * right.X, left.Y * right.Y);
		}
		public static PositionVector2 operator /(PositionVector2 left, PositionVector2 right)
		{
			return new PositionVector2(left.X / right.X, left.Y / right.Y);
		}

		public override string ToString()
		{
			return ToString(", ", "(", ")");
		}
		public string ToString(string separator, string encloseStart, string encloseEnd)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if (encloseStart != null)
			{
				sb.Append(encloseStart);
			}
			sb.Append(String.Format("{0:0.0#####################}", X));
			sb.Append(separator);
			sb.Append(String.Format("{0:0.0#####################}", Y));
			if (encloseEnd != null)
			{
				sb.Append(encloseEnd);
			}
			return sb.ToString();
		}

		public object Clone()
		{
			PositionVector2 clone = new PositionVector2();
			clone.X = X;
			clone.Y = Y;
			return clone;
		}

		public double GetLargestComponentValue()
		{
			if (X > Y) return X;
			if (Y > X) return Y;
			return 0.0;
		}
	}
}
