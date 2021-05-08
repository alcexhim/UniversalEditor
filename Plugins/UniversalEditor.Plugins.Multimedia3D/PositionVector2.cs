using Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public struct PositionVector2 : ICloneable
	{
		private double mvarX;
		private double mvarY;

		private bool mvarIsEmpty;
		public bool IsEmpty { get { return mvarIsEmpty; } }

		private PositionVector2(bool empty)
		{
			mvarX = 0;
			mvarY = 0;
			mvarIsEmpty = empty;
		}

		/// <summary>
		/// Represents the empty <see cref="PositionVector2" />. This field is read-only.
		/// </summary>
		public static readonly PositionVector2 Empty = new PositionVector2(true);

		public double X { get { return mvarX; } set { mvarX = value; } }
		public double Y { get { return mvarY; } set { mvarY = value; } }

		public PositionVector2(float x, float y)
		{
			mvarX = x;
			mvarY = y;
			mvarIsEmpty = false;
		}
		public PositionVector2(double x, double y)
		{
			mvarX = x;
			mvarY = y;
			mvarIsEmpty = false;
		}

		public double[] ToDoubleArray()
		{
			return new double[] { mvarX, mvarY };
		}
		public float[] ToFloatArray()
		{
			return new float[] { (float)mvarX, (float)mvarY };
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

		public static PositionVector2 Transform(PositionVector2 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + matrix[3, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + matrix[3, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2] + matrix[3, 2];
			return new PositionVector2(newX, newY);
		}
		public static PositionVector2 Rotate(PositionVector2 input, Matrix matrix)
		{
			double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0];
			double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1];
			double newZ = input.X * matrix[0, 2] + input.Y * matrix[1, 2];
			return new PositionVector2(newX, newY);
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
			if (encloseEnd != null)
			{
				sb.Append(encloseEnd);
			}
			return sb.ToString();
		}

		public object Clone()
		{
			PositionVector2 clone = new PositionVector2();
			clone.X = mvarX;
			clone.Y = mvarY;
			return clone;
		}

		public double GetLargestComponentValue()
		{
			if (mvarX > mvarY) return mvarX;
			if (mvarY > mvarX) return mvarY;
			return 0.0;
		}
	}
}
