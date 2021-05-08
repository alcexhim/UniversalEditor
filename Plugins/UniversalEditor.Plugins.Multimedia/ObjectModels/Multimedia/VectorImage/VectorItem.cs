//
//  VectorItem.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Multimedia.VectorImage
{
	public abstract class VectorItem : ICloneable
	{
		public class VectorItemCollection : System.Collections.ObjectModel.Collection<VectorItem>
		{

		}

		public Rectangle Bounds { get; set; } = Rectangle.Empty;
		public VectorImageStyle Style { get; set; } = new VectorImageStyle();

		public abstract object Clone();

		protected virtual bool ContainsInternal(Vector2D point)
		{
			return Bounds.Contains(point);
		}
		public bool Contains(Vector2D point)
		{
			return ContainsInternal(point);
		}
	}
}
