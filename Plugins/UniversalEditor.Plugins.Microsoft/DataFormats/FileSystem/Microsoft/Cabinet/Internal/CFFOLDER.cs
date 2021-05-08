//
//  CFFOLDER.cs - internal structure representing a Microsoft Cabinet folder entry
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
	/// <summary>
	/// Internal structure representing a Microsoft Cabinet folder entry.
	/// </summary>
	internal struct CFFOLDER
	{
		/// <summary>
		/// offset of the first CFDATA block in this folder
		/// </summary>
		public uint firstDataBlockOffset;
		/// <summary>
		/// number of CFDATA blocks in this folder
		/// </summary>
		public ushort dataBlockCount;
		/// <summary>
		/// compression type indicator
		/// </summary>
		public CABCompressionMethod compressionMethod;
		/// <summary>
		/// (optional) per-folder reserved area
		/// </summary>
		public byte[] reservedArea;
	}
}
