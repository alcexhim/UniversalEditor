//
//  TIFFExtraSamplesType.cs
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
using System;
namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public enum TIFFExtraSamplesType : ushort
	{
		/// <summary>
		/// Unspecified data
		/// </summary>
		Unspecified = 0x00,
		/// <summary>
		/// Associated alpha data (with pre-multiplied color). Note that including both unassociated and associated alpha is undefined because associated alpha specifies that
		/// color components are pre-multiplied by the alpha component, while unassociated alpha specifies the opposite.
		/// </summary>
		AssociatedAlpha = 0x01,
		/// <summary>
		/// Transparency information that logically exists independent of an image; it is commonly called a soft matte. Note that including both unassociated and associated alpha
		/// is undefined because associated alpha specifies that color components are pre-multiplied by the alpha component, while unassociated alpha specifies the opposite.
		/// </summary>
		UnassociatedAlpha = 0x02,
	}
}
