//
//  ALDFEntryStruct.cs - represents an entry in a Kronosaur ALDF / TDB archive
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

namespace UniversalEditor.DataFormats.FileSystem.Kronosaur.TDB
{
	/// <summary>
	/// Represents an entry in a Kronosaur ALDF / TDB archive.
	/// </summary>
	public struct ALDFEntryStruct
	{
		/// <summary>
		/// Block Number (-1 = unused)
		/// </summary>
		public int dwBlock;
		/// <summary>
		/// Number of blocks reserved for entry
		/// </summary>
		public int dwBlockCount;
		/// <summary>
		/// Size of entry
		/// </summary>
		public int dwSize;
		/// <summary>
		/// Version number
		/// </summary>
		public int dwVersion;
		/// <summary>
		/// Previous version
		/// </summary>
		public int dwPrevEntry;
		/// <summary>
		/// Latest entry (-1 if this is latest)
		/// </summary>
		public int dwLatestEntry;
		/// <summary>
		/// Misc flags
		/// </summary>
		public ALDFEntryFlags dwFlags;
	}
}
