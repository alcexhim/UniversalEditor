//
//  ExtensionMethods.cs - extension methods for the multimedia plugin
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
using MBS.Framework.Drawing;

namespace UniversalEditor
{
	/// <summary>
	/// Extension methods for the multimedia plugin.
	/// </summary>
	public static class ExtensionMethods
    {
        #region Reader
        /// <summary>
		/// Reads a series of 3 32-bit Single values as a <see cref="Color" />
		/// in RGB order.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBSingle(this IO.Reader br)
		{
            float specularR = br.ReadSingle();
            float specularG = br.ReadSingle();
            float specularB = br.ReadSingle();

            int r = (int)Math.Min(255f, specularR * 255f);
            int g = (int)Math.Min(255f, specularG * 255f);
            int b = (int)Math.Min(255f, specularB * 255f);
            return Color.FromRGBAInt32(r, g, b);
		}
		/// <summary>
		/// Reads a series of 4 32-bit Single values as a <see cref="Color" />
		/// in RGBA order.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBASingle(this IO.Reader br)
		{
            float diffuseR = br.ReadSingle();
            float diffuseG = br.ReadSingle();
            float diffuseB = br.ReadSingle();
            float diffuseA = br.ReadSingle();

            int a = (int)Math.Min(255f, diffuseA * 255f);
            int r = (int)Math.Min(255f, diffuseR * 255f);
            int g = (int)Math.Min(255f, diffuseG * 255f);
            int b = (int)Math.Min(255f, diffuseB * 255f);
            return Color.FromRGBAInt32(r, g, b, a);
		}
        /// <summary>
        /// Reads a series of 4 32-bit Single values as a <see cref="Color" />
        /// in RGBA order.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static Color ReadColorARGBSingle(this IO.Reader br)
        {
            float diffuseA = br.ReadSingle();
            float diffuseR = br.ReadSingle();
            float diffuseG = br.ReadSingle();
            float diffuseB = br.ReadSingle();

            int a = (int)Math.Min(255f, diffuseA * 255f);
            int r = (int)Math.Min(255f, diffuseR * 255f);
            int g = (int)Math.Min(255f, diffuseG * 255f);
            int b = (int)Math.Min(255f, diffuseB * 255f);
            return Color.FromRGBAInt32(r, g, b, a);
        }

		/// <summary>
		/// Reads a <see cref="Color" /> as 4 bits Red, 4 bits Green, 4 bits Blue, and 4 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBA4444(this IO.Reader br)
		{
			// 4 bits R, 4 bits G, 4 bits B, 4 bits alpha
			ushort rgba = br.ReadUInt16();
			byte r = (byte)(rgba.GetBits(0, 4));
			byte g = (byte)(rgba.GetBits(4, 4));
			byte b = (byte)(rgba.GetBits(8, 4));
			byte a = (byte)(rgba.GetBits(12, 4));

			Color color = Color.FromRGBAByte(a, r, g, b);
			return color;
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 5 bits Red, 6 bits Green, 5 bits Blue, and 0 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBA5650(this IO.Reader br)
		{
			// 5 bits R, 6 bits G, 5 bits B, 0 bits alpha
			ushort rgb = br.ReadUInt16();
			byte r = (byte)(rgb.GetBits(0, 5));
			byte g = (byte)(rgb.GetBits(5, 6));
			byte b = (byte)(rgb.GetBits(11, 5));

			Color color = Color.FromRGBAByte(r, g, b);
			return color;
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 8 bits Red, 8 bits Green, 8 bits Blue, and 8 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBA8888(this IO.Reader br)
		{
			// 1 byte each R, G, B, A
			byte r = br.ReadByte();
			byte g = br.ReadByte();
			byte b = br.ReadByte();
			byte a = br.ReadByte();

			Color color = Color.FromRGBAByte(a, r, g, b);
			return color;
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 5 bits Red, 5 bits Green, 5 bits Blue, and 1 bit Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBA5551(this IO.Reader br)
		{
			ushort rgba = br.ReadUInt16();
			byte r = (byte)(rgba.GetBits(0, 5));
			byte g = (byte)(rgba.GetBits(5, 5));
			byte b = (byte)(rgba.GetBits(10, 5));
			byte a = (byte)(rgba.GetBits(15, 1));

			Color color = Color.FromRGBAByte(a, r, g, b);
			return color;
        }
        #endregion
        #region Writer
        /// <summary>
        /// Writes a series of 3 32-bit Single values as a <see cref="Color" />
		/// in RGB order.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static void WriteColorRGBSingle(this IO.Writer bw, Color color)
        {
            bw.WriteSingle((float)color.R);
            bw.WriteSingle((float)color.G);
            bw.WriteSingle((float)color.B);
		}
		/// <summary>
		/// Writes a series of 4 32-bit Single values as a <see cref="Color" />
		/// in RGBA order.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
        public static void WriteColorRGBASingle(this IO.Writer bw, Color color)
        {
            bw.WriteSingle((float)color.R);
            bw.WriteSingle((float)color.G);
            bw.WriteSingle((float)color.B);
            bw.WriteSingle((float)color.A);
		}
        /// <summary>
        /// Writes a series of 4 32-bit Single values as a <see cref="Color" />
        /// in ARGB order.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static void WriteColorARGBSingle(this IO.Writer bw, Color color)
        {
            bw.WriteSingle((float)color.A);
            bw.WriteSingle((float)color.R);
            bw.WriteSingle((float)color.G);
            bw.WriteSingle((float)color.B);
        }

		/// <summary>
        /// Writes a <see cref="Color" /> as 4 bits Red, 4 bits Green, 4 bits Blue, and 4 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static void WriteColorRGBA4444(this IO.Writer bw, Color color)
		{
            throw new NotImplementedException();
            /*
			// 4 bits R, 4 bits G, 4 bits B, 4 bits alpha
			ushort rgba = br.ReadUInt16();
			byte r = (byte)(rgba.GetBits(0, 4));
			byte g = (byte)(rgba.GetBits(4, 4));
			byte b = (byte)(rgba.GetBits(8, 4));
			byte a = (byte)(rgba.GetBits(12, 4));

			Color color = Color.FromRGBA(a, r, g, b);
            */
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 5 bits Red, 6 bits Green, 5 bits Blue, and 0 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static void WriteColorRGBA5650(this IO.Writer bw, Color color)
		{
            throw new NotImplementedException();
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 8 bits Red, 8 bits Green, 8 bits Blue, and 8 bits Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static void WriteColorRGBA8888(this IO.Writer bw, Color color)
		{
            bw.WriteByte((byte)(color.R * 255));
            bw.WriteByte((byte)(color.G * 255));
            bw.WriteByte((byte)(color.B * 255));
            bw.WriteByte((byte)(color.A * 255));
		}

		/// <summary>
		/// Reads a <see cref="Color" /> as 5 bits Red, 5 bits Green, 5 bits Blue, and 1 bit Alpha.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Color ReadColorRGBA5551(this IO.Writer bw, Color color)
		{
            throw new NotImplementedException();
        }
        #endregion
    }
}
