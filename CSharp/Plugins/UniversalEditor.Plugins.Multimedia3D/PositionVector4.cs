using System;

using Neo;

namespace UniversalEditor
{
	public struct PositionVector4 : ICloneable
	{
        public static readonly PositionVector4 Empty;

        private double mvarX;
        public double X { get { return mvarX; } set { mvarX = value; } }

        private double mvarY;
        public double Y { get { return mvarY; } set { mvarY = value; } }

        private double mvarZ;
        public double Z { get { return mvarZ; } set { mvarZ = value; } }

        private double mvarW;
        public double W { get { return mvarW; } set { mvarW = value; } }

        public PositionVector4(float x, float y, float z) : this(x, y, z, 1.0f) { }
        public PositionVector4(double x, double y, double z) : this(x, y, z, 1.0) { }
		public PositionVector4(float x, float y, float z, float w)
		{
			mvarX = x;
			mvarY = y;
			mvarZ = z;
			mvarW = w;
		}
        public PositionVector4(double x, double y, double z, double w)
        {
            mvarX = x;
            mvarY = y;
            mvarZ = z;
            mvarW = w;
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
                sb.Append(String.Format("{0:0.0#####################}", mvarX));
            }
            if (maxCount >= 2)
            {
                sb.Append(separator);
                sb.Append(String.Format("{0:0.0#####################}", mvarY));
            }
            if (maxCount >= 3)
            {
                sb.Append(separator);
                sb.Append(String.Format("{0:0.0#####################}", mvarZ));
            }
            if (maxCount >= 4)
            {
                sb.Append(separator);
                sb.Append(String.Format("{0:0.0#####################}", mvarW));
            }
            if (encloseEnd != null)
            {
                sb.Append(encloseEnd);
            }
            return sb.ToString();
        }

        public static PositionVector4 operator +(PositionVector4 left, PositionVector4 right)
        {
            return new PositionVector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }
        public static PositionVector4 operator -(PositionVector4 left, PositionVector4 right)
        {
            return new PositionVector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }
        public static PositionVector4 operator *(PositionVector4 left, PositionVector4 right)
        {
            return new PositionVector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }
        public static PositionVector4 operator /(PositionVector4 left, PositionVector4 right)
        {
            return new PositionVector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
        }

        public double[] ToDoubleArray()
        {
            return new double[] { mvarX, mvarY, mvarZ, mvarW };
        }
        public float[] ToFloatArray()
        {
            return new float[] { (float)mvarX, (float)mvarY, (float)mvarZ, (float)mvarW };
        }

		public Matrix ToMatrix()
		{
            Matrix matrix = new Matrix(4, 4);

            double x2 = mvarX * mvarX * 2.0f;
            double y2 = mvarY * mvarY * 2.0f;
            double z2 = mvarZ * mvarZ * 2.0f;
            double xy = mvarX * mvarY * 2.0f;
            double yz = mvarY * mvarZ * 2.0f;
            double zx = mvarZ * mvarX * 2.0f;
            double xw = mvarX * mvarW * 2.0f;
            double yw = mvarY * mvarW * 2.0f;
            double zw = mvarZ * mvarW * 2.0f;

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

		public PositionVector4 Slerp(PositionVector4 pvec4Src2, float fLerpValue)
		{
			// Slerp
			float	dot = (float)((mvarX * pvec4Src2.X) + (mvarY * pvec4Src2.Y) + (mvarZ * pvec4Src2.Z) + (mvarW * pvec4Src2.W));

			// îΩì]èàóù
			PositionVector4	vec4CorrectTarget;

			if(dot < 0.0f)
			{
				double correctTargetX = -pvec4Src2.X;
				double correctTargetY = -pvec4Src2.Y;
				double correctTargetZ = -pvec4Src2.Z;
				double correctTargetW = -pvec4Src2.W;
				vec4CorrectTarget = new PositionVector4(correctTargetX, correctTargetY, correctTargetZ, correctTargetW);

				dot = -dot;
			}
			else
			{
				vec4CorrectTarget = pvec4Src2;
			}

			// åÎç∑ëŒçÙ
			if(dot >= 1.0f){ dot = 1.0f; }
			float radian = (float)Math.Acos(dot);

			if (Math.Abs(radian) < 0.0000000001f) { return vec4CorrectTarget; }

			float inverseSin = (float)(1.0f / Math.Sin(radian));
			float t0 = (float)(Math.Sin((1.0f - fLerpValue) * radian) * inverseSin);
			float t1 = (float)(Math.Sin(fLerpValue * radian) * inverseSin);

			double x = (mvarX * t0 + vec4CorrectTarget.X * t1);
			double y = (mvarY * t0 + vec4CorrectTarget.Y * t1);
			double z = (mvarZ * t0 + vec4CorrectTarget.Z * t1);
			double w = (mvarW * t0 + vec4CorrectTarget.W * t1);
			return new PositionVector4(x, y, z, w);
		}

		public PositionVector4 Qlerp(PositionVector4 pvec4Src2, float fLerpValue)
		{
			// Qlerp
			float qr = (float)(mvarX * pvec4Src2.X + mvarY * pvec4Src2.Y + mvarZ * pvec4Src2.Z + mvarW * pvec4Src2.W);
			float t0 = 1.0f - fLerpValue;

			double x, y, z, w;

			if (qr < 0)
			{
				x = mvarX * t0 - pvec4Src2.X * fLerpValue;
				y = mvarY * t0 - pvec4Src2.Y * fLerpValue;
				z = mvarZ * t0 - pvec4Src2.Z * fLerpValue;
				w = mvarW * t0 - pvec4Src2.W * fLerpValue;
			}
			else
			{
				x = mvarX * t0 + pvec4Src2.X * fLerpValue;
				y = mvarY * t0 + pvec4Src2.Y * fLerpValue;
				z = mvarZ * t0 + pvec4Src2.Z * fLerpValue;
				w = mvarW * t0 + pvec4Src2.W * fLerpValue;
			}

			PositionVector4 temp = new PositionVector4(x, y, z, w);
			temp = temp.Normalize();
			return temp;
		}

		public PositionVector4 Normalize()
		{
			double x, y, z, w;
			float fSqr = (float)(1.0f / Math.Sqrt(mvarX * mvarX + mvarY * mvarY + mvarZ * mvarZ + mvarW * mvarW));

			x = mvarX * fSqr;
			y = mvarY * fSqr;
			z = mvarZ * fSqr;
			w = mvarW * fSqr;

			return new PositionVector4(x, y, z, w);
		}

        public object Clone()
        {
            PositionVector4 clone = new PositionVector4(mvarX, mvarY, mvarZ, mvarW);
            return clone;
        }

		public override bool Equals(object obj)
		{
			PositionVector4 pv = (PositionVector4)obj;
			try
			{
				return (pv.X == X && pv.Y == Y && pv.Z == Z && pv.W == W);
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static bool operator ==(PositionVector4 left, PositionVector4 right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(PositionVector4 left, PositionVector4 right)
		{
			return !left.Equals(right);
		}
	}
}
