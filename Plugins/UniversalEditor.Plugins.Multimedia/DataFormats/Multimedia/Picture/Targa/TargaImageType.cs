//
//  TargaImageType.cs - indicates the type of pixel data stored in the TGA image file
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
	/// <summary>
	/// Indicates the type of pixel data stored in the TGA image file.
	/// </summary>
	public enum TargaImageType
	{
		/// <summary>
		/// No image data was found in file.
		/// </summary>
		None = 0,
		/// <summary>
		/// Image is an uncompressed, indexed color-mapped image.
		/// </summary>
		UncompressedIndexed = 1,
		/// <summary>
		/// Image is an uncompressed, RGB image.
		/// </summary>
		UncompressedTrueColor = 2,
		/// <summary>
		/// Image is an uncompressed, grayscale image.
		/// </summary>
		UncompressedGrayscale = 3,
		/// <summary>
		/// Image is a compressed, indexed color-mapped image.
		/// </summary>
		CompressedIndexed = 9,
		/// <summary>
		/// Image is a compressed, RGB image.
		/// </summary>
		CompressedTrueColor = 10,
		/// <summary>
		/// Image is a compressed, grayscale image.
		/// </summary>
		CompressedGrayscale = 11
	}
}
