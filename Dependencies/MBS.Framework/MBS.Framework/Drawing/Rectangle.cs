//
//  Rectangle.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
	public struct Rectangle : IComparable<Rectangle>, IEquatable<Rectangle>
	{
		public static readonly Rectangle Empty = new Rectangle();

		public Rectangle(Vector2D location, Dimension2D size)
		{
			X = location.X;
			Y = location.Y;
			Width = size.Width;
			Height = size.Height;
		}
		public Rectangle(Vector2D topLeft, Vector2D bottomRight)
		{
			X = topLeft.X;
			Y = topLeft.Y;
			Width = (bottomRight.X - topLeft.X);
			Height = (bottomRight.Y - topLeft.Y);
		}
		public Rectangle(double x, double y, double width, double height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }

		public Vector2D Location
		{
			get { return new Vector2D(X, Y); }
			set { X = value.X; Y = value.Y; }
		}
		public Dimension2D Size
		{
			get { return new Dimension2D(Width, Height); }
			set { Width = value.Width; Height = value.Height; }
		}

		public double Right { get { return X + Width; } set { Width = value - X; } }
		public double Bottom { get { return Y + Height; } set { Height = value - Y; } }

		public Rectangle Deflate(Padding padding)
		{
			Rectangle rect = this;
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Right;
			rect.Height -= padding.Bottom;
			return rect;
		}

		/// <summary>
		/// Normalize this instance.
		/// </summary>
		/// <returns>The normalize.</returns>
		public Rectangle Normalize()
		{
			double x = this.X;
			double y = this.Y;
			double r = this.Right;
			double b = this.Bottom;

			if (x > r)
			{
				double t = x;
				x = r;
				r = t;
			}
			if (y > b)
			{
				double t = y;
				y = b;
				b = t;
			}

			return new Rectangle(x, y, r - x, b - y);
		}


		public bool Contains(double x, double y)
		{
			return (x >= X && y >= Y && x <= Right && y <= Bottom);
		}
		public bool Contains(Vector2D point)
		{
			return Contains(point.X, point.Y);
		}
		public bool Contains(Rectangle rect)
		{
			return rect.X >= X && rect.Y >= Y && rect.Right <= Right && rect.Bottom <= Bottom;
		}


		public int CompareTo(Rectangle other)
		{
			double thisArea = this.Width * this.Height;
			double otherArea = other.Width * other.Height;

			return (int)(thisArea - otherArea);
		}

		#region IEquatable implementation

		public bool Equals(Rectangle other)
		{
			return (this.Width == other.Width && this.Height == other.Height);
		}

		#endregion

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			sb.Append(X.ToString());
			sb.Append(", ");
			sb.Append(Y.ToString());
			sb.Append(")-(");
			sb.Append(Right.ToString());
			sb.Append(", ");
			sb.Append(Bottom.ToString());
			sb.Append(")");
			sb.Append(", ");
			sb.Append(Width.ToString());
			sb.Append("x");
			sb.Append(Height.ToString());
			return sb.ToString();
		}

		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !left.Equals(right);
		}
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool IntersectsWith(Rectangle rect)
		{
			return (rect.X < this.Right) &&
			(this.X < (rect.Right)) &&
			(rect.Y < this.Bottom) &&
			(this.Y < rect.Bottom);
		}
	}
}
