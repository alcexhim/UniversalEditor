//
//  Color.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System.Text;

namespace MBS.Framework.Drawing
{
	public struct Color
	{
		public static readonly Color Empty;

		private double mvarR;
		public double R { get { return mvarR; } set { mvarR = value; } }

		private double mvarG;
		public double G { get { return mvarG; } set { mvarG = value; } }

		private double mvarB;
		public double B { get { return mvarB; } set { mvarB = value; } }

		private double mvarA;
		public double A { get { return mvarA; } set { mvarA = value; } }


		public static Color FromRGBASingle(float r, float g, float b, float a = 1.0f)
		{
			return Color.FromRGBADouble(r, g, b, a);
		}
		public static Color FromRGBADouble(double r, double g, double b, double a = 1.0)
		{
			Color color = new Color();
			color.R = r;
			color.G = g;
			color.B = b;
			color.A = a;
			return color;
		}

		public static Color FromRGBAByte(byte r, byte g, byte b, byte a = 255)
		{
			return FromRGBAInt32((int)r, (int)g, (int)b, (int)a);
		}
		public static Color FromRGBAInt32(int r, int g, int b, int a = 255)
		{
			Color color = new Color();
			color.R = ((double)r / 255);
			color.G = ((double)g / 255);
			color.B = ((double)b / 255);
			color.A = ((double)a / 255);
			return color;
		}

		public static Color FromString(string value)
		{
			/*
			if (value.StartsWith("@"))
			{
				if (ThemeManager.CurrentTheme != null) return ThemeManager.CurrentTheme.GetColorFromString(value);
			}
			else  */ if (value.StartsWith("#") && value.Length == 7)
			{
				string RRGGBB = value.Substring(1);
				byte RR = Byte.Parse(RRGGBB.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
				byte GG = Byte.Parse(RRGGBB.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
				byte BB = Byte.Parse(RRGGBB.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
				return Color.FromRGBAByte(RR, GG, BB);
			}
			else if (value.StartsWith("rgb(") && value.EndsWith(")"))
			{
				string r_g_b = value.Substring(3, value.Length - 4);
				string[] rgb = r_g_b.Split(new char[] { ',' });
				if (rgb.Length == 3)
				{
					byte r = Byte.Parse(rgb[0].Trim());
					byte g = Byte.Parse(rgb[1].Trim());
					byte b = Byte.Parse(rgb[2].Trim());
					return Color.FromRGBAByte(r, g, b);
				}
			}
			else if (value.StartsWith("rgba(") && value.EndsWith(")"))
			{
				string r_g_b = value.Substring(3, value.Length - 4);
				string[] rgb = r_g_b.Split(new char[] { ',' });
				if (rgb.Length == 3)
				{
					byte r = Byte.Parse(rgb[0].Trim());
					byte g = Byte.Parse(rgb[1].Trim());
					byte b = Byte.Parse(rgb[2].Trim());
					byte a = Byte.Parse(rgb[3].Trim());
					return Color.FromRGBAByte(r, g, b, a);
				}
			}
			else
			{
				/*
				try
				{
					System.Drawing.Color color = System.Drawing.Color.FromName(value);
					return color;
				}
				catch
				{

				}
				*/
			}
			return Color.Empty;
		}

		public byte GetRedByte()
		{
			return (byte)(mvarR * 255);
		}
		public byte GetGreenByte()
		{
			return (byte)(mvarG * 255);
		}
		public byte GetBlueByte()
		{
			return (byte)(mvarB * 255);
		}
		public byte GetAlphaByte()
		{
			return (byte)(mvarA * 255);
		}

		public string ToHexadecimalVB()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("&H");
			sb.Append(GetRedByte().ToString("X"));
			sb.Append(GetGreenByte().ToString("X"));
			sb.Append(GetBlueByte().ToString("X"));
			sb.Append(GetAlphaByte().ToString("X"));
			return sb.ToString();
		}
		public string ToHexadecimalHTML()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append('#');
			sb.Append(GetRedByte().ToString("x").PadLeft(2, '0'));
			sb.Append(GetGreenByte().ToString("x").PadLeft(2, '0'));
			sb.Append(GetBlueByte().ToString("x").PadLeft(2, '0'));
			return sb.ToString().ToUpper();
		}

		public float[] ToFloatRGB()
		{
			return new float[] { (float)R, (float)G, (float)B };
		}
		public float[] ToFloatRGBA()
		{
			return new float[] { (float)R, (float)G, (float)B, (float)A };
		}

		public int ToInt32()
		{
			return BitConverter.ToInt32(new byte[] { (byte)mvarA, (byte)mvarB, (byte)mvarG, (byte)mvarR }, 0);
		}

		public override string ToString()
		{
			return ToHexadecimalHTML();
		}

		/*private double mvarRed;
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
		*/

		public override bool Equals(object obj)
		{
			Color c = (Color)obj;
			try
			{
				return (c.R == R && c.G == G && c.B == B && c.A == A);
			}
			catch (Exception ex)
			{
			}
			return base.Equals(obj);
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
}
