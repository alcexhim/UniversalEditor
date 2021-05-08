//
//  IDOFFSET.cs - internal structure for representing an ID, offset, and size for a file in a CPK archive
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

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK
{
	/// <summary>
	/// Internal structure for representing an ID, offset, and size for a file in a CPK archive.
	/// </summary>
	internal struct IDOFFSET
	{
		public int INDEX;
		public uint ID;
		public ulong OFFSET;
		public ulong SIZE;

		public IDOFFSET(int index, uint id, ulong offset, ulong size)
		{
			INDEX = index;
			ID = id;
			OFFSET = offset;
			SIZE = size;
		}
	}
}
