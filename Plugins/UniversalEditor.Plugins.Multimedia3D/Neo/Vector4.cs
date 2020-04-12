//
//  Vector4.cs - represents a tuple containing X, Y, Z, and W coordinates
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

namespace Neo
{
	/// <summary>
	/// Represents a tuple containing X, Y, Z, and W coordinates.
	/// </summary>
	public struct Vector4 : ICloneable
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }
		public double W { get; set; }

		public Vector4(float x, float y, float z) : this(x, y, z, 1.0f) { }
		public Vector4(double x, double y, double z) : this(x, y, z, 1.0) { }
		public Vector4(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		public Vector4(double x, double y, double z, double w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
		public override string ToString()
		{
			return ToString(", ", "(", ")", 4);
		}
		public string ToString(string separator, string encloseStart, string encloseEnd)
		{
			return ToString(separator, encloseStart, encloseEnd, 4);
		}
		public string ToString(string separator, string encloseStart, string encloseEnd, int maxCount)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if (encloseStart != null)
			{
				sb.Append(encloseStart);
			}
			if (maxCount >= 1)
			{
				sb.Append(String.Format("{0:0.0#####################}", X));
			}
			if (maxCount >= 2)
			{
				sb.Append(separator);
				sb.Append(String.Format("{0:0.0#####################}", Y));
			}
			if (maxCount >= 3)
			{
				sb.Append(separator);
				sb.Append(String.Format("{0:0.0#####################}", Z));
			}
			if (maxCount >= 4)
			{
				sb.Append(separator);
				sb.Append(String.Format("{0:0.0#####################}", W));
			}
			if (encloseEnd != null)
			{
				sb.Append(encloseEnd);
			}
			return sb.ToString();
		}

		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		public double[] ToDoubleArray()
		{
			return new double[] { X, Y, Z, W };
		}
		public float[] ToFloatArray()
		{
			return new float[] { (float)X, (float)Y, (float)Z, (float)W };
		}

		public Matrix ToMatrix()
		{
			Matrix matrix = new Matrix(4, 4);

			double x2 = X * X * 2.0f;
			double y2 = Y * Y * 2.0f;
			double z2 = Z * Z * 2.0f;
			double xy = X * Y * 2.0f;
			double yz = Y * Z * 2.0f;
			double zx = Z * X * 2.0f;
			double xw = X * W * 2.0f;
			double yw = Y * W * 2.0f;
			double zw = Z * W * 2.0f;

			matrix[0, 0] = 1.0f - y2 - z2;
			matrix[0, 1] = xy + zw;
			matrix[0, 2] = zx - yw;
			matrix[1, 0] = xy - zw;
			matrix[1, 1] = 1.0f - z2 - x2;
			matrix[1, 2] = yz + xw;
			matrix[2, 0] = zx + yw;
			matrix[2, 1] = yz - xw;
			matrix[2, 2] = 1.0f - x2 - y2;

			matrix[0, 3] = 0.0f;
			matrix[1, 3] = 0.0f;
			matrix[2, 3] = 0.0f;
			matrix[3, 0] = 0.0f;
			matrix[3, 1] = 0.0f;
			matrix[3, 2] = 0.0f;
			matrix[3, 3] = 1.0f;
			return matrix;
		}

		public Vector4 Slerp(Vector4 pvec4Src2, float fLerpValue)
		{
			// Slerp
			float dot = (float)((X * pvec4Src2.X) + (Y * pvec4Src2.Y) + (Z * pvec4Src2.Z) + (W * pvec4Src2.W));

			// îΩì]èàóù
			Vector4 vec4CorrectTarget;

			if (dot < 0.0f)
			{
				double correctTargetX = -pvec4Src2.X;
				double correctTargetY = -pvec4Src2.Y;
				double correctTargetZ = -pvec4Src2.Z;
				double correctTargetW = -pvec4Src2.W;
				vec4CorrectTarget = new Vector4(correctTargetX, correctTargetY, correctTargetZ, correctTargetW);

				dot = -dot;
			}
			else
			{
				vec4CorrectTarget = pvec4Src2;
			}

			// åÎç∑ëŒçÙ
			if (dot >= 1.0f) { dot = 1.0f; }
			float radian = (float)Math.Acos(dot);

			if (Math.Abs(radian) < 0.0000000001f) { return vec4CorrectTarget; }

			float inverseSin = (float)(1.0f / Math.Sin(radian));
			float t0 = (float)(Math.Sin((1.0f - fLerpValue) * radian) * inverseSin);
			float t1 = (float)(Math.Sin(fLerpValue * radian) * inverseSin);

			double x = (X * t0 + vec4CorrectTarget.X * t1);
			double y = (Y * t0 + vec4CorrectTarget.Y * t1);
			double z = (Z * t0 + vec4CorrectTarget.Z * t1);
			double w = (W * t0 + vec4CorrectTarget.W * t1);
			return new Vector4(x, y, z, w);
		}

		public Vector4 Qlerp(Vector4 pvec4Src2, float fLerpValue)
		{
			// Qlerp
			float qr = (float)(X * pvec4Src2.X + Y * pvec4Src2.Y + Z * pvec4Src2.Z + W * pvec4Src2.W);
			float t0 = 1.0f - fLerpValue;

			double x, y, z, w;

			if (qr < 0)
			{
				x = X * t0 - pvec4Src2.X * fLerpValue;
				y = Y * t0 - pvec4Src2.Y * fLerpValue;
				z = Z * t0 - pvec4Src2.Z * fLerpValue;
				w = W * t0 - pvec4Src2.W * fLerpValue;
			}
			else
			{
				x = X * t0 + pvec4Src2.X * fLerpValue;
				y = Y * t0 + pvec4Src2.Y * fLerpValue;
				z = Z * t0 + pvec4Src2.Z * fLerpValue;
				w = W * t0 + pvec4Src2.W * fLerpValue;
			}

			Vector4 temp = new Vector4(x, y, z, w);
			temp = temp.Normalize();
			return temp;
		}

		public Vector4 Normalize()
		{
			double x, y, z, w;
			float fSqr = (float)(1.0f / Math.Sqrt(X * X + Y * Y + Z * Z + W * W));

			x = X * fSqr;
			y = Y * fSqr;
			z = Z * fSqr;
			w = W * fSqr;

			return new Vector4(x, y, z, w);
		}

		public object Clone()
		{
			Vector4 clone = new Vector4(X, Y, Z, W);
			return clone;
		}
	}
}
