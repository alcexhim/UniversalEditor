//
//  NameTableEntry.cs - represents an entry in the name table of an Unreal Engine package file
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
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Indicates attributes for this <see cref="NameTableEntry" />.
	/// </summary>
	[Flags()]
	public enum NameTableEntryFlags
	{
		None = 0
	}
	/// <summary>
	/// Represents an entry in the name table of an Unreal Engine package file.
	/// </summary>
	public class NameTableEntry : ICloneable
	{
		public class NameTableEntryCollection
			: System.Collections.ObjectModel.Collection<NameTableEntry>
		{
			public NameTableEntry Add(string name, NameTableEntryFlags flags = NameTableEntryFlags.None)
			{
				NameTableEntry entry = new NameTableEntry();
				entry.Name = name;
				entry.Flags = flags;
				Add(entry);
				return entry;
			}
		}

		/// <summary>
		/// Gets or sets the name (value) of this <see cref="NameTableEntry" />.
		/// </summary>
		/// <value>The name (value) of this <see cref="NameTableEntry" />.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the attributes for this <see cref="NameTableEntry" />.
		/// </summary>
		/// <value>The attributes for this <see cref="NameTableEntry" />.</value>
		public NameTableEntryFlags Flags { get; set; } = NameTableEntryFlags.None;

		public object Clone()
		{
			NameTableEntry entry = new NameTableEntry();
			entry.Name = (Name.Clone() as string);
			entry.Flags = Flags;
			return entry;
		}

		public override string ToString()
		{
			return ToString(true);
		}
		public string ToString(bool includeFlags)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			return sb.ToString();
		}
	}
}
