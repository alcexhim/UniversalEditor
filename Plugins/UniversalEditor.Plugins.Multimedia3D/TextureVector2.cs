//
//  TextureVector2.cs - represents a tuple containing U and V coordinates
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
namespace UniversalEditor
{
	public struct TextureVector2
	{
		public double U { get; set; }
		public double V { get; set; }

		public TextureVector2(float u, float v)
		{
			this.U = u;
			this.V = v;
		}
		public TextureVector2(double u, double v)
		{
			this.U = u;
			this.V = v;
		}
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.U, this.V);
		}
	}
}
