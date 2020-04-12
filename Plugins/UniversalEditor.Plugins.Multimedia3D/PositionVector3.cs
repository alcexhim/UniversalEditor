//
//  PositionVector3.cs - represents a tuple containing X, Y, and Z coordinates
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

using Neo;

namespace UniversalEditor
{
	/// <summary>
	/// Represents a tuple containing X, Y, and Z coordinates.
	/// </summary>
	public struct PositionVector3 : ICloneable
	{
		public bool IsEmpty { get; }

		private PositionVector3(bool empty)
		{
			X = 0;
			Y = 0;
			Z = 0;
			IsEmpty = empty;
		}

		/// <summary>
		/// Represents the empty <see cref="PositionVector3" />. This field is read-only.
		/// </summary>
		public static readonly PositionVector3 Empty = new PositionVector3(true);

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public PositionVector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
			IsEmpty = false;
		}
		public PositionVector3(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
			IsEmpty = false;
		}

		public double[] ToDoubleArray()
		{
			return new double[] { X, Y, Z };
		}
		public float[] ToFloatArray()
		{
			return new float[] { (float)X, (float)Y, (float)Z };
		}

		public static PositionVector3 operator +(PositionVector3 left, PositionVector3 right)
		{
			return new PositionVector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		public static PositionVector3 operator -(PositionVector3 left, PositionVector3 right)
		{
			return new PositionVector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		public static PositionVector3 operator *(PositionVector3 left, PositionVector3 right)
		{
			return new PositionVector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		public static PositionVector3 operator /(PositionVector3 left, PositionVector3 right)
		{
			return new PositionVector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		public static PositionVector3 Transform(PositionVector3 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + input.Z * matrix[2, 0] + matrix[3, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + input.Z * matrix[2, 1] + matrix[3, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2] + input.Z * matrix[2, 2] + matrix[3, 2];
			return new PositionVector3(newX, newY, newZ);
		}
		public static PositionVector3 Rotate(PositionVector3 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + input.Z * matrix[2, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + input.Z * matrix[2, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2] + input.Z * matrix[2, 2];
			return new PositionVector3(newX, newY, newZ);
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
			sb.Append(separator);
			sb.Append(String.Format("{0:0.0#####################}", Z));
			if (encloseEnd != null)
			{
				sb.Append(encloseEnd);
			}
			return sb.ToString();
		}

		public object Clone()
		{
			PositionVector3 clone = new PositionVector3();
			clone.X = X;
			clone.Y = Y;
			clone.Z = Z;
			return clone;
		}

		public override bool Equals(object obj)
		{
			PositionVector3 pv = (PositionVector3)obj;
			try
			{
				return (pv.X == X && pv.Y == Y && pv.Z == Z);
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static bool operator ==(PositionVector3 left, PositionVector3 right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(PositionVector3 left, PositionVector3 right)
		{
			return !left.Equals(right);
		}

		public double GetLargestComponentValue()
		{
			if (X > Y && X > Z) return X;
			if (Y > X && Y > Z) return Y;
			if (Z > X && Z > Y) return Z;
			return 0.0;
		}
	}
}
