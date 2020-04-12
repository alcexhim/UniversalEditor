//
//  ModelSurface.cs - represents a surface (collection of triangles and vertices) for a 3D model
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a surface (collection of triangles and vertices) for a 3D model.
	/// </summary>
	public class ModelSurface : ICloneable
	{
		public class ModelSurfaceCollection
			: System.Collections.ObjectModel.Collection<ModelSurface>
		{
		}

		/// <summary>
		/// Gets or sets the name of the surface.
		/// </summary>
		/// <value>The name of the surface.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="ModelTriangle" /> instances representing the sets of three vertices per triangle for the surface.
		/// </summary>
		/// <value>The sets of three vertices per triangle for the surface.</value>
		public ModelTriangle.ModelTriangleCollection Triangles { get; } = new ModelTriangle.ModelTriangleCollection();
		/// <summary>
		/// Gets a collection of <see cref="ModelVertex" /> instances representing all of the vertices in this surface.
		/// </summary>
		/// <value>All of the vertices in this surface.</value>
		public ModelVertex.ModelVertexCollection Vertices { get; } = new ModelVertex.ModelVertexCollection();

		public object Clone()
		{
			ModelSurface surface = new ModelSurface();
			surface.Name = (Name.Clone() as string);
			foreach (ModelTriangle triangle in Triangles)
			{
				surface.Triangles.Add(triangle.Clone() as ModelTriangle);
			}
			foreach (ModelVertex vertex in Vertices)
			{
				surface.Vertices.Add(vertex.Clone() as ModelVertex);
			}
			return surface;
		}

		public void Update()
		{
			for (int i = 0; i < Vertices.Count; i++)
			{
				double px = Vertices[i].OriginalPosition.X, py = Vertices[i].OriginalPosition.Y, pz = Vertices[i].OriginalPosition.Z;
				double nx = Vertices[i].OriginalNormal.X, ny = Vertices[i].OriginalNormal.Y, nz = Vertices[i].OriginalNormal.Z;

				// TODO: Figure out transformations of bones, etc.

				px = ((Vertices[i].Bone0.Position.X + Vertices[i].Bone0.Vector3Offset.X) * Vertices[i].Bone0Weight);
				py = ((Vertices[i].Bone0.Position.Y + Vertices[i].Bone0.Vector3Offset.Y) * Vertices[i].Bone0Weight);
				pz = ((Vertices[i].Bone0.Position.Z + Vertices[i].Bone0.Vector3Offset.Z) * Vertices[i].Bone0Weight);

				px += ((Vertices[i].Bone1.Position.X + Vertices[i].Bone1.Vector3Offset.X) * Vertices[i].Bone1Weight);
				py += ((Vertices[i].Bone1.Position.Y + Vertices[i].Bone1.Vector3Offset.Y) * Vertices[i].Bone1Weight);
				pz += ((Vertices[i].Bone1.Position.Z + Vertices[i].Bone1.Vector3Offset.Z) * Vertices[i].Bone1Weight);

				Vertices[i].Position = new PositionVector3(px, py, pz);
				Vertices[i].Normal = new PositionVector3(nx, ny, nz);

				/*
                if (mvarVertices[i].Bone0Weight == 0.0f)
                {
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, mvarVertices[i].Bone1.GetSkinningMatrix());
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, mvarVertices[i].Bone1.GetSkinningMatrix());
                }
                else if (mvarVertices[i].Bone0Weight >= 1.0f)
                {
                    Neo.Matrix matrix = mvarVertices[i].Bone0.GetSkinningMatrix();
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, matrix);
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, matrix);
                }
                else
                {
                    Matrix matTemp = Matrix.Lerp(mvarVertices[i].Bone0.GetSkinningMatrix(), mvarVertices[i].Bone1.GetSkinningMatrix(), mvarVertices[i].Bone0Weight);
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, matTemp);
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, matTemp);
                }
                */
			}
		}

		public void Reset()
		{
			foreach (ModelVertex vtx in Vertices)
			{
				vtx.Reset();
			}
		}
	}
}
