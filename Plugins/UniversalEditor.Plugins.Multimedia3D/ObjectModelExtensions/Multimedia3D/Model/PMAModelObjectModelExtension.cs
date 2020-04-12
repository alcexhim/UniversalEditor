//
//  PMAModelObjectModelExtension.cs - provides an ObjectModelExtension that extends a ModelObjectModel with additional information pertaining to the Polygon Movie Maker ALCETECH format
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

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
	/// <summary>
	/// Provides an <see cref="ObjectModelExtension"/> that extends a ModelObjectModel with additional information pertaining to the Polygon Movie Maker ALCETECH format.
	/// </summary>
	public class PMAModelObjectModelExtension // : ObjectModelExtension
	{
		/*
        public override void Clear()
        {
            mvarEnabled = false;
            mvarVersion = new Version(1, 0, 0, 0);
            mvarTextureFlipping = new TextureFlippingInformation();
        }
        public override void CopyTo(ObjectModelExtension where)
        {
            PMAModelObjectModelExtension clone = (where as PMAModelObjectModelExtension);
            if (clone == null) return;

            clone.Enabled = mvarEnabled;
            clone.Version = (mvarVersion.Clone() as Version);
            clone.mvarTextureFlipping = (mvarTextureFlipping.Clone() as TextureFlippingInformation);
        }
		*/

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PMAModelObjectModelExtension"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if this <see cref="PMAModelObjectModelExtension"/> is enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; set; } = false;
		/// <summary>
		/// Gets or sets the version of this <see cref="PMAModelObjectModelExtension" />.
		/// </summary>
		/// <value>The version of this <see cref="PMAModelObjectModelExtension" />.</value>
		public Version Version { get; set; } = new Version(1, 0, 0, 0);
		/// <summary>
		/// Gets a <see cref="TextureFlippingInformation" /> instance that describes texture animation settings.
		/// </summary>
		/// <value>The texture animation settings associated with this <see cref="PMAModelObjectModelExtension" />.</value>
		public TextureFlippingInformation TextureFlipping { get; } = new TextureFlippingInformation();
	}
}
