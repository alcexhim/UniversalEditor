//
//  UnrealPackageObjectModel.cs - provides an ObjectModel for manipulating Unreal Engine package files
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
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Unreal Engine package files.
	/// </summary>
	public class UnrealPackageObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Unreal Engine package";
				_omr.Path = new string[] { "Game development", "Unreal Engine", "Package" };
			}
			return _omr;
		}

		/// <summary>
		/// Unique identifiers used for package downloading from servers.
		/// </summary>
		public List<Guid> PackageGUIDs { get; set; } = new List<Guid>();

		/// <summary>
		/// This is the licensee number, different for each game.
		/// </summary>
		public ushort LicenseeNumber { get; set; } = 0;

		/// <summary>
		/// Global package flags; i.e., if a package may be downloaded from a game server, etc.
		/// </summary>
		public PackageFlags PackageFlags { get; set; } = PackageFlags.None;

		/// <summary>
		/// The name-table can be considered an index of all unique names used for objects and
		/// references within the file. Later on, you'll often find indexes into this table instead of
		/// a string containing the object-name.
		/// </summary>
		public NameTableEntry.NameTableEntryCollection NameTableEntries { get; } = new NameTableEntry.NameTableEntryCollection();

		/// <summary>
		/// The export-table is an index for all objects within the package. Every object in the body
		/// of the file has a corresponding entry in this table, with information like offset within
		/// the file etc.
		/// </summary>
		public ExportTableEntry.ExportTableEntryCollection ExportTableEntries { get; } = new ExportTableEntry.ExportTableEntryCollection();

		public ImportTableEntry.ImportTableEntryCollection ImportTableEntries { get; } = new ImportTableEntry.ImportTableEntryCollection();

		public Generation.GenerationCollection Generations { get; } = new Generation.GenerationCollection();

		public override void Clear()
		{
			LicenseeNumber = 0;
			PackageFlags = UnrealEngine.PackageFlags.None;
			PackageGUIDs.Clear();
			NameTableEntries.Clear();
			ExportTableEntries.Clear();
			ImportTableEntries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			UnrealPackageObjectModel clone = (where as UnrealPackageObjectModel);
			clone.LicenseeNumber = LicenseeNumber;
			clone.PackageFlags = PackageFlags;
			foreach (Guid guid in PackageGUIDs)
			{
				clone.PackageGUIDs.Add(guid);
			}
			foreach (NameTableEntry entry in NameTableEntries)
			{
				clone.NameTableEntries.Add(entry.Clone() as NameTableEntry);
			}
			foreach (ExportTableEntry entry in ExportTableEntries)
			{
				clone.ExportTableEntries.Add(entry.Clone() as ExportTableEntry);
			}
			foreach (ImportTableEntry entry in ImportTableEntries)
			{
				clone.ImportTableEntries.Add(entry.Clone() as ImportTableEntry);
			}
		}
	}
}
