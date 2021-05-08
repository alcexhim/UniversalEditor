//
//  CFLCompressionMethod.cs - indicates the compression method of a CFL file
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

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
	/// <summary>
	/// Indicates the compression method of a CFL file.
	/// </summary>
	public enum CFLCompressionMethod
	{
		/// <summary>
		/// No compression
		/// </summary>
		None = 0x00000000,
		/// <summary>
		/// Zlib, normal compression level
		/// </summary>
		ZlibNormal = 0x00000001,
		/// <summary>
		/// Zlib, maximum compression level
		/// </summary>
		ZlibMaximum = 0x00000901,
		/// <summary>
		/// LZSS
		/// </summary>
		LZSS = 0x00000002,
		/// <summary>
		/// Bzip2
		/// </summary>
		BZip2 = 0x00000003,
		/// <summary>
		/// Tries out all registered compressors and uses the best result.
		/// </summary>
		Best = 0x0000FFFF
	}
}
