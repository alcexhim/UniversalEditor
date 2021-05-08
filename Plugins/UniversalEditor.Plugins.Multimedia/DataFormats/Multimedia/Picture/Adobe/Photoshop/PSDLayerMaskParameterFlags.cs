//
//  PSDLayerMaskParameterFlags.cs - indicates parameter attributes for a layer mask in an Adobe Photoshop PSD image file
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
	/// Indicates parameter attributes for a layer mask in an Adobe Photoshop PSD image file.
	/// </summary>
	public enum PSDLayerMaskParameterFlags
	{
		/// <summary>
		/// user mask density, 1 byte
		/// </summary>
		UserMaskDensity = 0x01,
		/// <summary>
		/// user mask feather, 8 byte, double
		/// </summary>
		UserMaskFeather = 0x02,
		/// <summary>
		/// vector mask density, 1 byte
		/// </summary>
		VectorMaskDensity = 0x04,
		/// <summary>
		/// vector mask feather, 8 bytes, double
		/// </summary>
		VectorMaskFeather = 0x08
	}
}
