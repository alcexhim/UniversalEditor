//
//  ModelMorphMaterial.cs - represents a 3D model morph that affects the way a material is rendered
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

using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	/// <summary>
	/// Represents a 3D model morph that affects the way a material is rendered.
	/// </summary>
	public class ModelMorphMaterial : ModelMorph
	{
		/// <summary>
		/// Gets or sets the index of the affected material.
		/// </summary>
		/// <value>The index of the affected material.</value>
		public long MaterialIndex { get; set; } = 0L;

		/// <summary>
		/// Gets or sets the diffuse color for the associated material.
		/// </summary>
		/// <value>The diffuse color for the associated material.</value>
		public Color DiffuseColor { get; set; } = Color.Empty;
		/// <summary>
		/// Gets or sets the specular color for the associated material.
		/// </summary>
		/// <value>The specular color for the associated material.</value>
		public Color SpecularColor { get; set; } = Color.Empty;

		/// <summary>
		/// Gets or sets the specular coefficient for the associated material.
		/// </summary>
		/// <value>The specular coefficient for the associated material.</value>
		public float SpecularCoefficient { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the ambient color for the associated material.
		/// </summary>
		/// <value>The ambient color for the associated material.</value>
		public Color AmbientColor { get; set; } = Color.Empty;

		/// <summary>
		/// Gets or sets the edge color for the associated material.
		/// </summary>
		/// <value>The edge color for the associated material.</value>
		public Color EdgeColor { get; set; }
		/// <summary>
		/// Gets or sets the edge size for the associated material.
		/// </summary>
		/// <value>The edge size for the associated material.</value>
		public float EdgeSize { get; set; } = 0f;


		/// <summary>
		/// Gets or sets the texture coefficient color for the associated material.
		/// </summary>
		/// <value>The texture coefficient color for the associated material.</value>
		public Color TextureCoefficient { get; set; } = Color.Empty;
		/// <summary>
		/// Gets or sets the sphere coefficient color for the associated material.
		/// </summary>
		/// <value>The sphere coefficient color for the associated material.</value>
		public Color SphereCoefficient { get; set; } = Color.Empty;
		/// <summary>
		/// Gets or sets the toon texture coefficient color for the associated material.
		/// </summary>
		/// <value>The toon texture coefficient color for the associated material.</value>
		public Color ToonTextureCoefficient { get; set; } = Color.Empty;

		public override object Clone()
		{
			return new ModelMorphMaterial
			{
				AmbientColor = this.AmbientColor,
				DiffuseColor = this.DiffuseColor,
				EdgeColor = this.EdgeColor,
				EdgeSize = this.EdgeSize,
				MaterialIndex = this.MaterialIndex,
				Name = base.Name,
				SpecularCoefficient = this.SpecularCoefficient,
				SpecularColor = this.SpecularColor,
				SphereCoefficient = this.SphereCoefficient,
				TextureCoefficient = this.TextureCoefficient,
				ToonTextureCoefficient = this.ToonTextureCoefficient
			};
		}
	}
}
