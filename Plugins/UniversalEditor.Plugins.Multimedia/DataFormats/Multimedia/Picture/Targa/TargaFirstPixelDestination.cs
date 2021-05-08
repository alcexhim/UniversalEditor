//
//  TargaFirstPixelDestination.cs - screen destination of first pixel based on the VerticalTransferOrder and HorizontalTransferOrder
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
	/// Screen destination of first pixel based on the VerticalTransferOrder and HorizontalTransferOrder.
	/// </summary>
	public enum TargaFirstPixelDestination
	{
		/// <summary>
		/// Unknown first pixel destination.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// First pixel destination is the top-left corner of the image.
		/// </summary>
		TopLeft = 1,
		/// <summary>
		/// First pixel destination is the top-right corner of the image.
		/// </summary>
		TopRight = 2,
		/// <summary>
		/// First pixel destination is the bottom-left corner of the image.
		/// </summary>
		BottomLeft = 3,
		/// <summary>
		/// First pixel destination is the bottom-right corner of the image.
		/// </summary>
		BottomRight = 4
	}
}
