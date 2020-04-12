//
//  PSDLayerMaskFlags.cs - indicates attributes for a layer mask in an Adobe Photoshop PSD image file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop
{
	/// <summary>
	/// Indicates attributes for a layer mask in an Adobe Photoshop PSD image file.
	/// </summary>
	public enum PSDLayerMaskFlags
	{
		/// <summary>
		/// position relative to layer
		/// </summary>
		PositionRelativeToLayer = 0x01,
		/// <summary>
		/// layer mask disabled
		/// </summary>
		LayerMaskDisabled = 0x02,
		/// <summary>
		/// invert layer mask when blending (Obsolete)
		/// </summary>
		InvertLayerMaskWhenBlending = 0x04,
		/// <summary>
		/// indicates that the user mask actually came from rendering other data
		/// </summary>
		UserMaskFromRenderingOtherData = 0x08,
		/// <summary>
		/// indicates that the user and/or vector masks have parameters applied to them
		/// </summary>
		ContainsParameter = 0x10
	}
}
