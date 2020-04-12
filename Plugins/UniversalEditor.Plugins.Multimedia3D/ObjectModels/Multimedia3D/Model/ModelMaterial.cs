//
//  ModelMaterial.cs - describes information about a material for a 3D model
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Describes information about a material for a 3D model.
	/// </summary>
	public class ModelMaterial : ICloneable
	{
		public class ModelMaterialCollection : Collection<ModelMaterial>
		{
		}

		private uint? mvarMapID = null;

		/// <summary>
		/// Gets or sets the name of the material.
		/// </summary>
		/// <value>The name of the material.</value>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the diffuse color of the material.
		/// </summary>
		/// <value>The diffuse color of the material.</value>
		public Color DiffuseColor { get; set; } = Colors.Black;
		/// <summary>
		/// Gets or sets the specular color of the material.
		/// </summary>
		/// <value>The specular color of the material.</value>
		public Color SpecularColor { get; set; } = Colors.Black;
		/// <summary>
		/// Gets or sets the emissive color of the material.
		/// </summary>
		/// <value>The emissive color of the material.</value>
		public Color EmissiveColor { get; set; } = Colors.Black;
		/// <summary>
		/// Gets or sets the ambient color of the material.
		/// </summary>
		/// <value>The ambient color of the material.</value>
		public Color AmbientColor { get; set; } = Colors.Black;

		/// <summary>
		/// Gets or sets the index of the toon texture to use for this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>The index of the toon texture to use for this <see cref="ModelMaterial" />.</value>
		public sbyte ToonNumber { get; set; } = 0;
		/// <summary>
		/// Gets or sets the shininess of the material.
		/// </summary>
		/// <value>The shininess of the material.</value>
		public float Shininess { get; set; } = 0f;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ModelMaterial"/> participates in edge detection for toon rendering.
		/// </summary>
		/// <value><c>true</c> if this <see cref="ModelMaterial" /> participates in edge detection for toon rendering; otherwise, <c>false</c>.</value>
		public bool EdgeFlag { get; set; } = false;
		/// <summary>
		/// Gets or sets the number of vertices affected by this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>The number of vertices affected by this <see cref="ModelMaterial" />.</value>
		public uint IndexCount { get; set; } = 0u;
		/// <summary>
		/// Gets or sets a user-defined comment for this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>A user-defined comment for this <see cref="ModelMaterial" />.</value>
		public string Comment { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the color of the toon edge line when <see cref="EdgeFlag" /> is set to <c>true</c>.
		/// </summary>
		/// <value>The color of the toon edge line when <see cref="EdgeFlag" /> is set to <c>true</c>.</value>
		public Color EdgeColor { get; set; } = Color.Empty;
		/// <summary>
		/// Gets or sets the size of the toon edge line when <see cref="EdgeFlag" /> is set to <c>true</c>.
		/// </summary>
		/// <value>The size of the toon edge line when <see cref="EdgeFlag" /> is set to <c>true</c>.</value>
		public float EdgeSize { get; set; } = 0f;

		public object Clone()
		{
			ModelMaterial clone = new ModelMaterial();
			clone.AmbientColor = AmbientColor;
			clone.DiffuseColor = DiffuseColor;
			clone.EdgeColor = EdgeColor;
			clone.EdgeFlag = EdgeFlag;
			clone.EdgeSize = EdgeSize;
			clone.IndexCount = IndexCount;
			clone.Name = Name;
			clone.Shininess = Shininess;
			clone.SpecularColor = SpecularColor;

			foreach (ModelTexture texture in Textures)
			{
				clone.Textures.Add(texture.Clone() as ModelTexture);
			}

			return clone;
		}

		/// <summary>
		/// Gets a collection of <see cref="ModelTexture" /> instances representing the texture animation frames contained in this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>The texture animation frames contained in this <see cref="ModelMaterial" />.</value>
		public ModelTexture.ModelTextureCollection Textures { get; } = new ModelTexture.ModelTextureCollection();
		/// <summary>
		/// Gets or sets the index of the default texture frame for this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>The index of the default texture frame for this <see cref="ModelMaterial" />.</value>
		public int TextureIndex { get; set; } = 0;

		public override string ToString()
		{
			return Name + " (" + ((Textures.Count > 0) ? (!String.IsNullOrEmpty(Textures[0].TextureFileName) ? Textures[0].TextureFileName : Textures[0].MapFileName) : AmbientColor.ToString()) + ")";
		}
		/// <summary>
		/// Gets or sets a value indicating whether the lights-out glow effect is always lit for this <see cref="ModelMaterial" /> on supported renderers.
		/// </summary>
		/// <value><c>true</c> if the lights-out glow effect is always lit for this <see cref="ModelMaterial" />; otherwise, <c>false</c>.</value>
		public bool AlwaysLight { get; set; } = false;
		/// <summary>
		/// Gets a collection of <see cref="ModelTriangle" /> instances representing the faces affected by this <see cref="ModelMaterial" />.
		/// </summary>
		/// <value>The triangles.</value>
		public ModelTriangle.ModelTriangleCollection Triangles { get; } = new ModelTriangle.ModelTriangleCollection();
		/// <summary>
		/// Gets or sets a value indicating whether the lights-out glow effect should be enabled for this <see cref="ModelMaterial" /> on supported renderers.
		/// </summary>
		/// <value><c>true</c> if the lights-out glow effect should be enabled for this <see cref="ModelMaterial" />; otherwise, <c>false</c>.</value>
		public bool EnableGlow { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether the multiple-texture animation feature should be enabled for this <see cref="ModelMaterial" /> on supported
		/// renderers.
		/// </summary>
		/// <value><c>true</c> if the multiple-texture animation feature should be enabled for this <see cref="ModelMaterial" />; otherwise, <c>false</c>.</value>
		public bool EnableAnimation { get; set; } = false;
	}
}
