using System;

using Neo;

namespace UniversalEditor
{
	public struct PositionVector3 : ICloneable
	{
		private double mvarX;
		private double mvarY;
		private double mvarZ;

        private bool mvarIsEmpty;
        public bool IsEmpty { get { return mvarIsEmpty; } }

        private PositionVector3(bool empty)
        {
            mvarX = 0;
            mvarY = 0;
            mvarZ = 0;
            mvarIsEmpty = empty;
        }

        /// <summary>
        /// Represents the empty <see cref="PositionVector3" />. This field is read-only.
        /// </summary>
        public static readonly PositionVector3 Empty = new PositionVector3(true);

        public double X { get { return mvarX; } set { mvarX = value; } }
        public double Y { get { return mvarY; } set { mvarY = value; } }
        public double Z { get { return mvarZ; } set { mvarZ = value; } }
		
        public PositionVector3(float x, float y, float z)
		{
			mvarX = x;
			mvarY = y;
			mvarZ = z;
            mvarIsEmpty = false;
		}
        public PositionVector3(double x, double y, double z)
        {
            mvarX = x;
            mvarY = y;
            mvarZ = z;
            mvarIsEmpty = false;
        }

        public double[] ToDoubleArray()
        {
            return new double[] { mvarX, mvarY, mvarZ };
        }
        public float[] ToFloatArray()
        {
            return new float[] { (float)mvarX, (float)mvarY, (float)mvarZ };
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
            PositionVector3 clone = new PositionVector3();
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
    }
}
