//
//  TextureFlippingBlock.cs - describes texture animation settings for a 3D model texture
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
	/// <summary>
	/// Describes texture animation settings for a 3D model texture.
	/// </summary>
	public class TextureFlippingBlock : ICloneable
	{
		public class TextureFlippingBlockCollection : System.Collections.ObjectModel.Collection<TextureFlippingBlock>
		{
		}

		/// <summary>
		/// Gets or sets the material to assign for this texture animation block.
		/// </summary>
		/// <value>The material to assign for this texture animation block.</value>
		public ModelMaterial Material { get; set; } = null;
		/// <summary>
		/// Gets a collection of <see cref="TextureFlippingFrame" /> instances representing the individual texture image frames used in the animation process.
		/// </summary>
		/// <value>The individual texture image frames used in the animation process.</value>
		public TextureFlippingFrame.TextureFlippingFrameCollection Frames { get; } = new TextureFlippingFrame.TextureFlippingFrameCollection();

		public object Clone()
		{
			TextureFlippingBlock clone = new TextureFlippingBlock();
			clone.Material = Material;
			foreach (TextureFlippingFrame frame in Frames)
			{
				clone.Frames.Add(frame.Clone() as TextureFlippingFrame);
			}
			return clone;
		}
	}
}
