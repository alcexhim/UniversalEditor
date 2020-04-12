//
//  ModelVertex.cs - represents a vertex in a 3D model
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

using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a vertex in a 3D model.
	/// </summary>
	public class ModelVertex
	{
		public class ModelVertexCollection : Collection<ModelVertex>
		{
		}

		public ModelVertex()
		{
		}
		public ModelVertex(float vx, float vy, float vz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
		}
		public ModelVertex(double vx, double vy, double vz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
		}
		public ModelVertex(float vx, float vy, float vz, float tu, float tv)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Texture = new TextureVector2(tu, tv);
		}
		public ModelVertex(double vx, double vy, double vz, double tu, double tv)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Texture = new TextureVector2(tu, tv);
		}
		public ModelVertex(float vx, float vy, float vz, float nx, float ny, float nz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Normal = new PositionVector3(nx, ny, nz);
			OriginalNormal = new PositionVector3(nx, ny, nz);
		}
		public ModelVertex(double vx, double vy, double vz, double nx, double ny, double nz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Normal = new PositionVector3(nx, ny, nz);
			OriginalNormal = new PositionVector3(nx, ny, nz);
		}
		public ModelVertex(float vx, float vy, float vz, float tu, float tv, float nx, float ny, float nz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Texture = new TextureVector2(tu, tv);
			Normal = new PositionVector3(nx, ny, nz);
			OriginalNormal = new PositionVector3(nx, ny, nz);
		}
		public ModelVertex(double vx, double vy, double vz, double tu, double tv, double nx, double ny, double nz)
		{
			Position = new PositionVector3(vx, vy, vz);
			OriginalPosition = new PositionVector3(vx, vy, vz);
			Texture = new TextureVector2(tu, tv);
			Normal = new PositionVector3(nx, ny, nz);
			OriginalNormal = new PositionVector3(nx, ny, nz);
		}

		/// <summary>
		/// Gets or sets the primary bone associated with this <see cref="ModelVertex" />.
		/// </summary>
		/// <value>The primary bone associated with this <see cref="ModelVertex" />.</value>
		public ModelBone Bone0 { get; set; } = null;
		/// <summary>
		/// Gets or sets the secondary bone associated with this <see cref="ModelVertex" />.
		/// </summary>
		/// <value>The secondary bone associated with this <see cref="ModelVertex" />.</value>
		public ModelBone Bone1 { get; set; } = null;

		public PositionVector3 Position { get; set; } = default(PositionVector3);
		public PositionVector3 Normal { get; set; } = default(PositionVector3);
		public TextureVector2 Texture { get; set; } = default(TextureVector2);

		/// <summary>
		/// Gets or sets the weight of the primary bone attached to this <see cref="ModelVertex" />.
		/// </summary>
		/// <value>The weight of the primary bone attached to this <see cref="ModelVertex" />.</value>
		public float Bone0Weight { get; set; } = 1.0f;
		/// <summary>
		/// Gets or sets the weight of the secondary bone attached to this <see cref="ModelVertex" />.
		/// </summary>
		/// <value>The weight of the secondary bone attached to this <see cref="ModelVertex" />.</value>
		public float Bone1Weight
		{
			get { return 1.0f - Bone0Weight; }
			set { Bone0Weight = 1.0f - value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ModelVertex"/> participates in edge detection for toon rendering.
		/// </summary>
		/// <value><c>true</c> if this <see cref="ModelVertex" /> participates in edge detection for toon rendering; otherwise, <c>false</c>.</value>
		public bool EdgeFlag { get; set; } = false;

		public object Clone()
		{
			return new ModelVertex
			{
				Bone0 = (Bone0.Clone() as ModelBone),
				Bone1 = (Bone1.Clone() as ModelBone),
				EdgeFlag = EdgeFlag,
				Normal = Normal,
				Position = Position,
				Texture = Texture,
				Bone0Weight = Bone0Weight
			};
		}

		/// <summary>
		/// Gets or sets the original position of the vertex before any transformations have been applied.
		/// </summary>
		/// <value>The original position of the vertex before any transformations have been applied.</value>
		public PositionVector3 OriginalPosition { get; set; }
		/// <summary>
		/// Gets or sets the original normal of the vertex before any transformations have been applied.
		/// </summary>
		/// <value>The original normal of the vertex before any transformations have been applied.</value>
		public PositionVector3 OriginalNormal { get; set; }

		/// <summary>
		/// Resets the position and normal of the vertex to their original values.
		/// </summary>
		public void Reset()
		{
			Position = OriginalPosition;
			Normal = OriginalNormal;
		}
	}
}
