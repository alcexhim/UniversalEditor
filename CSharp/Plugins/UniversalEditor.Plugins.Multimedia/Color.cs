using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public struct Color
	{
		private double mvarRed;
		public double Red { get { return mvarRed; } set { mvarRed = value; } }

		private double mvarGreen;
		public double Green { get { return mvarGreen; } set { mvarGreen = value; } }

		private double mvarBlue;
		public double Blue { get { return mvarBlue; } set { mvarBlue = value; } }

		private double mvarAlpha;
		public double Alpha { get { return mvarAlpha; } set { mvarAlpha = value; } }

		public static readonly Color Empty;

		public override bool Equals(object obj)
		{
			if (obj is Color)
			{
				Color color = (Color)obj;
				return ((mvarRed == color.mvarRed) && (mvarGreen == color.mvarGreen) && (mvarBlue == color.mvarBlue) && (mvarAlpha == color.mvarAlpha));
			}
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static Color FromRGBA(double red, double green, double blue, double alpha = 1.0)
		{
			Color color = new Color();
			color.Red = red;
			color.Green = green;
			color.Blue = blue;
			color.Alpha = alpha;
			return color;
		}
		public static Color FromRGBA(float red, float green, float blue, float alpha = 1.0f)
		{
			Color color = new Color();
			color.Red = red;
			color.Green = green;
			color.Blue = blue;
			color.Alpha = alpha;
			return color;
		}
		public static Color FromRGBA(byte red, byte green, byte blue, byte alpha = 255)
		{
			Color color = new Color();
			color.Red = ((double)red / 255);
			color.Green = ((double)green / 255);
			color.Blue = ((double)blue / 255);
			color.Alpha = ((double)alpha / 255);
			return color;
		}
		public static Color FromRGBA(int red, int green, int blue, int alpha = 255)
		{
			Color color = new Color();
			color.Red = ((double)red / 255);
			color.Green = ((double)green / 255);
			color.Blue = ((double)blue / 255);
			color.Alpha = ((double)alpha / 255);
			return color;
		}
		
		public float[] ToFloatRGB()
		{
			return new float[] { (float)mvarRed, (float)mvarGreen, (float)mvarBlue };
		}
		public float[] ToFloatRGBA()
		{
			return new float[] { (float)mvarRed, (float)mvarGreen, (float)mvarBlue, (float)mvarAlpha };
		}

		public int ToInt32()
		{
			byte a = (byte)(mvarAlpha * 255);
			byte r = (byte)(mvarRed * 255);
			byte g = (byte)(mvarGreen * 255);
			byte b = (byte)(mvarBlue * 255);
			return (((a | (r << 8)) | (g << 0x10)) | (b << 0x18));
		}

		public int CompareTo(Color other)
		{
			int thisVal = ToInt32();
			int otherVal = other.ToInt32();
			return thisVal.CompareTo(otherVal);
		}

		public static bool operator ==(Color left, Color right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Color left, Color right)
		{
			return !left.Equals(right);
		}
	}
	public static class Colors
	{
		private static Color m_White = Color.FromRGBA(255, 255, 255);
		public static Color White { get { return m_White; } }
		private static Color m_Black = Color.FromRGBA(0, 0, 0);
		public static Color Black { get { return m_Black; } }
		private static Color m_Transparent = Color.FromRGBA(255, 255, 255, 0);
		public static Color Transparent { get { return m_Transparent; } }

		private static Color m_DarkBlue = Color.FromRGBA(0, 0, 64, 255);
		public static Color DarkBlue { get { return m_DarkBlue; } }

		private static Color m_Red = Color.FromRGBA(255, 0, 0);
		public static Color Red { get { return m_Red; } }
		private static Color m_Green = Color.FromRGBA(0, 255, 0);
		public static Color Green { get { return m_Green; } }
		private static Color m_Blue = Color.FromRGBA(0, 0, 255);
		public static Color Blue { get { return m_Blue; } }

		private static Color m_Cyan = Color.FromRGBA(0, 255, 255);
		public static Color Cyan { get { return m_Cyan; } }

		private static Color m_LightGreen = Color.FromRGBA(144, 238, 144);
		public static Color LightGreen { get { return m_LightGreen; } }
		private static Color m_Coral = Color.FromRGBA(255, 127, 80);
		public static Color Coral { get { return m_Coral; } }
		private static Color m_LightGoldenrodYellow = Color.FromRGBA(250, 250, 210);
		public static Color LightGoldenrodYellow { get { return m_LightGoldenrodYellow; } }
		private static Color m_LightGray = Color.FromRGBA(211, 211, 211);
		public static Color LightGray { get { return m_LightGray; } }
		
		private static Color m_Silver = Color.FromRGBA(192, 192, 192);
		public static Color Silver { get { return m_Silver; } }

		private static Color m_GhostWhite = Color.FromRGBA(0xF8, 0xF8, 0xFF);
		public static Color GhostWhite { get { return m_GhostWhite; } }
		
		private static Color m_Gainsboro = Color.FromRGBA(0xDC, 0xDC, 0xDC);
		public static Color Gainsboro { get { return m_Gainsboro; } }

		private static Color m_DarkGray = Color.FromRGBA(128, 128, 128);
		public static Color DarkGray { get { return m_DarkGray; } }
	}
}
