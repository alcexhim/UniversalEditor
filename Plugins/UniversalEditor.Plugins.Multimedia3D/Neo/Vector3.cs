//
//  Vector3.cs - represents a tuple containing X, Y, and Z coordinates
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
	public struct Vector3 : ICloneable
	{
		private double mvarX;
		private double mvarY;
		private double mvarZ;

		public double X { get { return mvarX; } set { mvarX = value; } }
		public double Y { get { return mvarY; } set { mvarY = value; } }
		public double Z { get { return mvarZ; } set { mvarZ = value; } }

		public Vector3(float x, float y, float z)
		{
			mvarX = x;
			mvarY = y;
			mvarZ = z;
		}
		public Vector3(double x, double y, double z)
		{
			mvarX = x;
			mvarY = y;
			mvarZ = z;
		}

		public double[] ToDoubleArray()
		{
			return new double[] { mvarX, mvarY, mvarZ };
		}
		public float[] ToFloatArray()
		{
			return new float[] { (float)mvarX, (float)mvarY, (float)mvarZ };
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		public static Vector3 Transform(Vector3 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + input.Z * matrix[2, 0] + matrix[3, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + input.Z * matrix[2, 1] + matrix[3, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2] + input.Z * matrix[2, 2] + matrix[3, 2];
			return new Vector3(newX, newY, newZ);
		}
		public static Vector3 Rotate(Vector3 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + input.Z * matrix[2, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + input.Z * matrix[2, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2] + input.Z * matrix[2, 2];
			return new Vector3(newX, newY, newZ);
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
			sb.Append(String.Format("{0:0.0#####################}", mvarX));
			sb.Append(separator);
			sb.Append(String.Format("{0:0.0#####################}", mvarY));
			sb.Append(separator);
			sb.Append(String.Format("{0:0.0#####################}", mvarZ));
			if (encloseEnd != null)
			{
				sb.Append(encloseEnd);
			}
			return sb.ToString();
		}

		public object Clone()
		{
			Vector3 clone = new Vector3();
			clone.X = mvarX;
			clone.Y = mvarY;
			clone.Z = mvarZ;
			return clone;
		}

		public double GetLargestComponentValue()
		{
			if (mvarX > mvarY && mvarX > mvarZ) return mvarX;
			if (mvarY > mvarX && mvarY > mvarZ) return mvarY;
			if (mvarZ > mvarX && mvarZ > mvarY) return mvarZ;
			return 0.0;
		}

		public static double Dot(Vector3 v1, Vector3 v2)
		{
			return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
		}
	}
}
