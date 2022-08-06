//
//  CompoundDocumentStorageHeader.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	public struct CompoundDocumentStorageHeader
	{
		public string Name { get; set; }
		public CompoundDocumentStorageType StorageType { get; set; }
		public CompoundDocumentStorageColor NodeColor { get; set; }
		public int LeftChildNodeDirectoryID { get; set; }
		public int RightChildNodeDirectoryID { get; set; }
		public int RootNodeEntryDirectoryID { get; set; }
		public Guid UniqueIdentifier { get; set; }
		public CompoundDocumentStorageFlags Flags { get; set; }
		public DateTime CreationTimestamp { get; set; }
		public DateTime ModificationTimestamp { get; set; }
		public int FirstSectorIndex { get; set; }
		public int Length { get; set; }
		public int Unused3 { get; set; }
	}
}
