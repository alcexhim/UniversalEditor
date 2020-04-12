//
//  XCOFFDataFormat.cs - provides common functionality for the DataFormats that manipulate 32-bit and 64-bit Extended Common Object File Format executables
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

using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	/// <summary>
	/// Provides common functionality for the DataFormats that manipulate 32-bit and 64-bit Extended Common Object File Format executables.
	/// </summary>
	public abstract class XCOFFDataFormat : DataFormat
	{
		private ushort mvarSectionHeaderCount = 0;
		/// <value>
		/// Specifies the number of section headers contained in the file. The first section header is section header number one; all references to a section are one-based.
		/// </value>
		public ushort SectionHeaderCount { get { return mvarSectionHeaderCount; } set { mvarSectionHeaderCount = value; } }
		
		private DateTime mvarCreationDate = DateTime.Now;
		/// <value>
		///	Specifies when the file was created (number of elapsed seconds since 00:00:00 Universal Coordinated Time (UCT), January 1, 1970). This field should specify either the actual time or be set to a value of 0.
		/// </value>
		public DateTime CreationDate { get { return mvarCreationDate; } set { mvarCreationDate = value; } }
		
		private uint mvarSymbolicEntryCount = 0;
		/// <value>
		/// Specifies the number of entries in the symbol table. Each symbol table entry is 18 bytes long. 
		/// </value>
		public uint SymbolicEntryCount { get { return mvarSymbolicEntryCount; } set { mvarSymbolicEntryCount = value; } }
		
		private XCOFFDocumentFlags mvarFlags = XCOFFDocumentFlags.F_NONE;
		public XCOFFDocumentFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
