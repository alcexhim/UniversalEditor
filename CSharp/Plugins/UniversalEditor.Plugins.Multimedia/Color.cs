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
		public int RedInt32 { get { return (int)(mvarRed * 255); } set { mvarRed = (double)value / 255.0; } }

		private double mvarGreen;
		public double Green { get { return mvarGreen; } set { mvarGreen = value; } }
		public int GreenInt32 { get { return (int)(mvarGreen * 255); } set { mvarGreen = (double)value / 255.0; } }

		private double mvarBlue;
		public double Blue { get { return mvarBlue; } set { mvarBlue = value; } }
		public int BlueInt32 { get { return (int)(mvarBlue * 255); } set { mvarBlue = (double)value / 255.0; } }

		private double mvarAlpha;
		public double Alpha { get { return mvarAlpha; } set { mvarAlpha = value; } }
		public int AlphaInt32 { get { return (int)(mvarAlpha * 255); } set { mvarAlpha = (double)value / 255.0; } }

		public double Hue
		{
			get
			{
				int r = this.RedInt32;
				int g = this.GreenInt32;
				int b = this.BlueInt32;

				byte minRGB = (byte)Math.Min (r, Math.Min (g, b));
				byte maxRGB = (byte)Math.Max (r, Math.Max (g, b));
				if (maxRGB == minRGB) return 0.0;

				float num = (float)(maxRGB - minRGB);
				float redFraction = (float)((int)maxRGB - r) / num;
				float greenFraction = (float)((int)maxRGB - g) / num;
				float blueFraction = (float)((int)maxRGB - b) / num;
				float hue = 0f;
				if (r == (int)maxRGB)
				{
					hue = 60f * (6f + blueFraction - greenFraction);
				}
				if (g == (int)maxRGB)
				{
					hue = 60f * (2f + redFraction - blueFraction);
				}
				if (b == (int)maxRGB)
				{
					hue = 60f * (4f + greenFraction - redFraction);
				}
				if (hue > 360f)
				{
					hue -= 360f;
				}
				return (double)(hue / HSL_SCALE);
			}
			set { UpdateHSL (value, Saturation, Luminosity); }
		}
		public int HueInt32
		{
			get { return (int)(Hue * HSL_SCALE); }
			set { Hue = CheckRange ((double)value / HSL_SCALE); }
		}
		public double Saturation
		{
			get
			{
				int minRGB = Math.Min (this.RedInt32, Math.Min (this.GreenInt32, this.BlueInt32));
				int maxRGB = Math.Max (this.RedInt32, Math.Max (this.GreenInt32, this.BlueInt32));
				if (maxRGB == minRGB) return 0.0;

				int num = (int)(maxRGB + minRGB);
				if (num > 255)
				{
					num = 510 - num;
				}
				return (double)((double)(maxRGB - minRGB) / (double)num) / HSL_SCALE;
			}
			set { UpdateHSL (Hue, value, Luminosity); }
		}
		public int SaturationInt32
		{
			get { return (int)(Saturation * HSL_SCALE); }
			set { Saturation = CheckRange ((double)value / HSL_SCALE); }
		}
		public double Luminosity
		{
			get
			{
				int minRGB = Math.Min (this.RedInt32, Math.Min (this.GreenInt32, this.BlueInt32));
				int maxRGB = Math.Max (this.RedInt32, Math.Max (this.GreenInt32, this.BlueInt32));
				return (double)((double)(maxRGB + minRGB) / 510.0) / HSL_SCALE;
			}
			set { UpdateHSL (Hue, Saturation, value); }
		}
		public int LuminosityInt32
		{
			get { return (int)(Luminosity * HSL_SCALE); }
			set { Luminosity = CheckRange ((double)value / HSL_SCALE); }
		}

		private void UpdateHSL(double h, double s, double l)
		{
			if (l != 0)
			{
				if (s == 0)
					mvarRed = mvarGreen = mvarBlue = l;
				else
				{
					double temp2 = GetTemp2(h, s, l);
					double temp1 = 2.0 * l - temp2;

					mvarRed = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
					mvarGreen = GetColorComponent(temp1, temp2, h);
					mvarBlue = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
				}
			}
			else
			{
				mvarRed = 0.0;
				mvarGreen = 0.0;
				mvarBlue = 0.0;
			}
		}

		private const double HSL_SCALE = 240.0;
		private double CheckRange(double value)
		{
			if (value < 0.0)
				value = 0.0;
			else if (value > 1.0)
				value = 1.0;
			return value;
		}

		public static Color FromHSL(int h, int s, int l)
		{
			return FromHSL ((double)h / HSL_SCALE, (double)s / HSL_SCALE, (double)l / HSL_SCALE);
		}

		public static Color FromHSL(double h, double s, double l)
		{
			double r = 0, g = 0, b = 0;
			if (l != 0)
			{
				if (s == 0)
					r = g = b = l;
				else
				{
					double temp2 = GetTemp2(h, s, l);
					double temp1 = 2.0 * l - temp2;

					r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
					g = GetColorComponent(temp1, temp2, h);
					b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
				}
			}
			return Color.FromRGBA((int)(255 * r), (int)(255 * g), (int)(255 * b));
		}

		private static double GetColorComponent(double temp1, double temp2, double temp3)
		{
			temp3 = MoveIntoRange(temp3);
			if (temp3 < 1.0 / 6.0)
				return temp1 + (temp2 - temp1) * 6.0 * temp3;
			else if (temp3 < 0.5)
				return temp2;
			else if (temp3 < 2.0 / 3.0)
				return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
			else
				return temp1;
		}
		private static double MoveIntoRange(double temp3)
		{
			if (temp3 < 0.0)
				temp3 += 1.0;
			else if (temp3 > 1.0)
				temp3 -= 1.0;
			return temp3;
		}
		private static double GetTemp2(double h, double s, double l)
		{
			double temp2;
			if (l < 0.5)  //<=??
				temp2 = l * (1.0 + s);
			else
				temp2 = l + s - (l * s);
			return temp2;
		}

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

		public static Color FromString(string value)
		{
			if (value.StartsWith("#"))
			{
				// hex string
				value = value.Substring(1);

				string rr = value.Substring(0, 2);
				string gg = value.Substring(2, 2);
				string bb = value.Substring(4, 2);

				byte r = Byte.Parse(rr, System.Globalization.NumberStyles.HexNumber);
				byte g = Byte.Parse(gg, System.Globalization.NumberStyles.HexNumber);
				byte b = Byte.Parse(bb, System.Globalization.NumberStyles.HexNumber);

				return Color.FromRGBA(r, g, b);
			}
			return Color.Empty;
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
