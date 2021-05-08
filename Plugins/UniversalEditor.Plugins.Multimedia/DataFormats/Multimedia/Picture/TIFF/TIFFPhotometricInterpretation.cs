//
//  TIFFPhotometricInterpretation.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public enum TIFFPhotometricInterpretation
	{
		/// <summary>
		/// For bilevel and grayscale images: 0 is imaged as white. The maximum value is imaged as black. This is the normal value for Compression = 2.
		/// </summary>
		WhiteIsZero = 0x00,
		/// <summary>
		/// For bilevel and grayscale images: 0 is imaged as black. The maximum value is imaged as white. If this value is specified for Compression = 2, the
		/// image should display and print reversed.
		/// </summary>
		BlackIsZero = 0x01,
		/// <summary>
		/// Indicates a full-color image. Each component is 8 bits deep in a Baseline TIFF RGB image.
		/// </summary>
		RGB = 0x02,
		/// <summary>
		/// Indicates a Palette-color image.
		/// </summary>
		PaletteColor = 0x03,
		TransparencyMask = 0x04,
		CMYK = 0x05,
		YCbCr = 0x06,
		CIELab = 0x08
	}
}
