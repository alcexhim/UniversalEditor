//
//  MSCompressedKWAJCompressionMethod.cs - indicates the compression method used in an MSCompressed KWAJ archive
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.MSCompressed
{
	/// <summary>
	/// Indicates the compression method used in an MSCompressed KWAJ archive.
	/// </summary>
	public enum MSCompressedKWAJCompressionMethod
	{
		/// <summary>
		/// No compression
		/// </summary>
		None = 0,
		/// <summary>
		/// No compression, data is XORed with byte 0xFF
		/// </summary>
		XOR = 1,
		/// <summary>
		/// The same compression method as regular SZDD
		/// </summary>
		SZDD = 2,
		/// <summary>
		/// LZ + Huffman "Jeff Johnson" compression
		/// </summary>
		JeffJohnson = 3,
		/// <summary>
		/// MS-ZIP
		/// </summary>
		MSZIP = 4
	}
}
