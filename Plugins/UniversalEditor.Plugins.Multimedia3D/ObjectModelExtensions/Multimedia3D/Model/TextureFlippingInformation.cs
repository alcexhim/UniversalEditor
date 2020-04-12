//
//  TextureFlippingInformation.cs - provides information about animated textures for 3D models
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

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
	public class TextureFlippingInformation : ICloneable
	{
		public bool Enabled { get; set; } = false;
		public TextureFlippingBlock.TextureFlippingBlockCollection Blocks { get; } = new TextureFlippingBlock.TextureFlippingBlockCollection();

		public object Clone()
		{
			TextureFlippingInformation clone = new TextureFlippingInformation();
			clone.Enabled = Enabled;
			foreach (TextureFlippingBlock block in Blocks)
			{
				clone.Blocks.Add(block.Clone() as TextureFlippingBlock);
			}
			return clone;
		}
	}
}
