//
//  TargaRLEPacketType.cs - indicates the RLE packet type used in a RLE compressed image
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
	/// Indicates the RLE packet type used in a RLE compressed image.
	/// </summary>
	public enum TargaRLEPacketType
	{
		/// <summary>
		/// A raw RLE packet type.
		/// </summary>
		Uncompressed = 0,
		/// <summary>
		/// A run-length RLE packet type.
		/// </summary>
		Compressed = 1
	}
}
