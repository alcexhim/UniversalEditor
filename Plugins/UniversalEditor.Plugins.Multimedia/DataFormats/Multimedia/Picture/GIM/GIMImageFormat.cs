//
//  GIMImageFormat.cs - indicates the format for pixel data in a GIM image
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Indicates the format for pixel data in a GIM image.
	/// </summary>
	public enum GIMImageFormat
	{
		/// <summary>
		/// Pixels are comprised of 5 bits Red, 6 bits Green, 5 bits Blue, 0 bits
		/// Alpha.
		/// </summary>
		RGBA5650 = 0x00,
		/// <summary>
		/// Pixels are comprised of 5 bits Red, 5 bits Green, 5 bits Blue, 1 bit
		/// Alpha.
		/// </summary>
		RGBA5551 = 0x01,
		/// <summary>
		/// Pixels are comprised of 4 bits Red, 4 bits Green, 4 bits Blue, 4 bits
		/// Alpha.
		/// </summary>
		RGBA4444 = 0x02,
		/// <summary>
		/// Pixels are comprised of 8 bits Red, 8 bits Green, 8 bits Blue, 8 bits
		/// Alpha.
		/// </summary>
		RGBA8888 = 0x03,
		/// <summary>
		/// Pixels are comprised of 4-bit indices into the palette color table.
		/// </summary>
		Index4 = 0x04,
		/// <summary>
		/// Pixels are comprised of 8-bit indices into the palette color table.
		/// </summary>
		Index8 = 0x05,
		/// <summary>
		/// Pixels are comprised of 16-bit indices into the palette color table.
		/// </summary>
		Index16 = 0x06,
		/// <summary>
		/// Pixels are comprised of 32-bit indices into the palette color table.
		/// </summary>
		Index32 = 0x07
	}
}
