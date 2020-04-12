//
//  PMAXAdvancedTextureBlock.cs - represents a Concertroid PMAX advanced texture block
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.PMAXPatch
{
	/// <summary>
	/// Represents a Concertroid PMAX advanced texture block.
	/// </summary>
	public class PMAXAdvancedTextureBlock : ICloneable
	{
		public class PMAXAdvancedTextureBlockCollection
			: System.Collections.ObjectModel.Collection<PMAXAdvancedTextureBlock>
		{
		}

		public int MaterialID { get; set; } = 0;
		/// <summary>
		/// Gets a collection of <see cref="PMAXAdvancedTextureBlockImage" /> instances representing the images for the animated texture.
		/// </summary>
		/// <value>The images for the animated texture.</value>
		public PMAXAdvancedTextureBlockImage.PMAXAdvancedTextureBlockImageCollection Images { get; } = new PMAXAdvancedTextureBlockImage.PMAXAdvancedTextureBlockImageCollection();

		public object Clone()
		{
			PMAXAdvancedTextureBlock clone = new PMAXAdvancedTextureBlock();
			foreach (PMAXAdvancedTextureBlockImage image in Images)
			{
				clone.Images.Add(image.Clone() as PMAXAdvancedTextureBlockImage);
			}
			clone.MaterialID = MaterialID;
			return clone;
		}

		/// <summary>
		/// Gets or sets a value indicating whether the texture represented by this
		/// <see cref="PMAXAdvancedTextureBlock"/> is always lit by the ambient light regardless of whether the light
		/// setting is on or off.
		/// </summary>
		/// <value><c>true</c> if always lit; otherwise, <c>false</c>.</value>
		public bool AlwaysLight { get; set; } = false;
	}
}
