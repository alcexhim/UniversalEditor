//
//  TargaHorizontalTransferOrder.cs - the left-to-right ordering in which pixel data is transferred from the file to the screen
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
	/// The left-to-right ordering in which pixel data is transferred from the file to the screen.
	/// </summary>
	public enum TargaHorizontalTransferOrder
	{
		/// <summary>
		/// Unknown transfer order.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Transfer order of pixels is from the right to left.
		/// </summary>
		RightToLeft = 0,
		/// <summary>
		/// Transfer order of pixels is from the left to right.
		/// </summary>
		LeftToRight = 1
	}
}
