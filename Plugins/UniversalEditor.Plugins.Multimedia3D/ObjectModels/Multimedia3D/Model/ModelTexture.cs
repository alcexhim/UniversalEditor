//
//  ModelTexture.cs - represents a texture image for a 3D model
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
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Indicates attributes for a texture.
	/// </summary>
	[Flags()]
	public enum ModelTextureFlags : int
	{
		None = 0,
		/// <summary>
		/// The texture is a regular texture.
		/// </summary>
		Texture = 1,
		/// <summary>
		/// The texture is a sphere map.
		/// </summary>
		Map = 2,
		/// <summary>
		/// The texture is an additive sphere map.
		/// </summary>
		AddMap = 4
	}
	/// <summary>
	/// Represents a texture image for a 3D model.
	/// </summary>
	public class ModelTexture : ICloneable
	{
		public class ModelTextureCollection
			: System.Collections.ObjectModel.Collection<ModelTexture>
		{
			public ModelTexture Add(string TextureFileName, string MapFileName, ModelTextureFlags Flags)
			{
				ModelTexture tex = new ModelTexture();
				tex.TextureFileName = TextureFileName;
				tex.MapFileName = MapFileName;
				tex.Flags = Flags;

				base.Add(tex);
				return tex;
			}
		}
		/// <summary>
		/// Gets or sets the OpenGL index of the texture.
		/// </summary>
		/// <value>The OpenGL index of the texture.</value>
		public uint? TextureID { get; set; } = null;
		/// <summary>
		/// Gets or sets the OpenGL index of the sphere map texture.
		/// </summary>
		/// <value>The OpenGL index of the sphere map texture.</value>
		public uint? MapID { get; set; } = null;
		/// <summary>
		/// Gets or sets the full path to the sphere map image file.
		/// </summary>
		/// <value>The full path to the sphere map image file.</value>
		public string MapFileName { get; set; } = null;
		/// <summary>
		/// Gets or sets the full path to the texture image file.
		/// </summary>
		/// <value>The full path to the texture image file.</value>
		public string TextureFileName { get; set; } = null;
		/// <summary>
		/// Gets or sets the attributes associated with this texture.
		/// </summary>
		/// <value>The attributes associated with this texture.</value>
		public ModelTextureFlags Flags { get; set; } = ModelTextureFlags.None;
		/// <summary>
		/// How long this texture image frame will appear on the associated material, in milliseconds.
		/// </summary>
		public int Duration { get; set; } = 100;

		public object Clone()
		{
			ModelTexture texture = new ModelTexture();
			texture.MapFileName = MapFileName;
			texture.TextureFileName = TextureFileName;
			texture.Flags = Flags;
			texture.MapID = MapID;
			texture.TextureID = TextureID;
			return texture;
		}
		/// <summary>
		/// Gets or sets the <see cref="PictureObjectModel" /> representing the image to use for the texture image of this texture.
		/// </summary>
		/// <value>The image to use for the texture image of this texture.</value>
		public PictureObjectModel TexturePicture { get; set; } = null;
		/// <summary>
		/// Gets or sets the <see cref="PictureObjectModel" /> representing the image to use for the sphere map of this texture.
		/// </summary>
		/// <value>The image to use for the sphere map of this texture.</value>
		public PictureObjectModel MapPicture { get; set; } = null;
	}
}
