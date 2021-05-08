//
//  CHDCompressionType.cs - indicates the compression type of a CHD archive
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

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	/// <summary>
	/// Indicates the compression type of a CHD archive.
	/// </summary>
	public enum CHDCompressionType
	{
		None = 0,
		Zlib = 1,
		ZlibPlus = 2,
		/// <summary>
		/// Supported as of V4.
		/// </summary>
		AV = 3
	}
}
