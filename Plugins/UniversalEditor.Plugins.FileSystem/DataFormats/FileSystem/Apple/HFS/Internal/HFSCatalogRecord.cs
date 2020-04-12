//
//  HFSCatalogRecord.cs - the abstract base class from which all HFS catalog records derive
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

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	/// <summary>
	/// The type of the HFS catalog record.
	/// </summary>
	internal enum HFSCatalogRecordType : short
	{
		/// <summary>
		/// HFS+ folder record
		/// </summary>
		ExtendedDirectory = 0x0001,
		/// <summary>
		/// HFS+ file record
		/// </summary>
		ExtendedFile = 0x0002,
		/// <summary>
		/// HFS+ folder thread record
		/// </summary>
		ExtendedDirectoryThread = 0x0003,
		/// <summary>
		/// HFS+ file thread record
		/// </summary>
		ExtendedFileThread = 0x0004,
		/// <summary>
		/// HFS folder record
		/// </summary>
		Directory = 0x0100,
		/// <summary>
		/// HFS file record
		/// </summary>
		File = 0x0200,
		/// <summary>
		/// HFS folder thread record
		/// </summary>
		DirectoryThread = 0x0300,
		/// <summary>
		/// HFS file thread record
		/// </summary>
		FileThread = 0x0400
	}
	/// <summary>
	/// The abstract base class from which all HFS catalog records derive.
	/// </summary>
	internal class HFSCatalogRecord
	{
		public HFSCatalogRecordType type;
	}
}
