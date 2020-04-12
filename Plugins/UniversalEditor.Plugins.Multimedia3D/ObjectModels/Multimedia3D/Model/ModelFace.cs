//
//  ModelFace.cs - represents a triangle (face) in a 3D model
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
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a triangle (face) in a 3D model.
	/// </summary>
	public class ModelTriangle : ICloneable
	{
		public class ModelTriangleCollection : Collection<ModelTriangle>
		{
			public ModelTriangle Add(ModelVertex vertex1, ModelVertex vertex2, ModelVertex vertex3)
			{
				ModelTriangle triangle = new ModelTriangle(vertex1, vertex2, vertex3);
				Add(triangle);
				return triangle;
			}
		}

		public ModelTriangle()
		{
		}
		public ModelTriangle(ModelVertex vertex1, ModelVertex vertex2, ModelVertex vertex3)
		{
			Vertex1 = vertex1;
			Vertex2 = vertex2;
			Vertex3 = vertex3;
		}

		/// <summary>
		/// Gets or sets the first vertex of the triangle.
		/// </summary>
		/// <value>The first vertex of the triangle.</value>
		public ModelVertex Vertex1 { get; set; } = null;
		/// <summary>
		/// Gets or sets the second vertex of the triangle.
		/// </summary>
		/// <value>The second vertex of the triangle.</value>
		public ModelVertex Vertex2 { get; set; } = null;
		/// <summary>
		/// Gets or sets the third vertex of the triangle.
		/// </summary>
		/// <value>The third vertex of the triangle.</value>
		public ModelVertex Vertex3 { get; set; } = null;

		public object Clone()
		{
			return new ModelTriangle
			{
				Vertex1 = (Vertex1.Clone() as ModelVertex),
				Vertex2 = (Vertex2.Clone() as ModelVertex),
				Vertex3 = (Vertex3.Clone() as ModelVertex)
			};
		}
	}
}
