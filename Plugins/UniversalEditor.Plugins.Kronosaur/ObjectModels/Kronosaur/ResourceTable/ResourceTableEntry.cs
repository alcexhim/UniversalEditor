//
//  ResourceTableEntry.cs - represents an entry in a Kronosaur TRDB resource table
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

namespace UniversalEditor.ObjectModels.Kronosaur.ResourceTable
{
	/// <summary>
	/// Represents an entry in a Kronosaur TRDB resource table.
	/// </summary>
	public class ResourceTableEntry : ICloneable
	{
		public class ResourceTableEntryCollection
			: System.Collections.ObjectModel.Collection<ResourceTableEntry>
		{
			public ResourceTableEntry Add(string name, int entryID, ResourceTableEntryFlags flags)
			{
				ResourceTableEntry item = new ResourceTableEntry();
				item.Name = name;
				item.EntryID = entryID;
				item.Flags = flags;
				Add(item);
				return item;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private int mvarEntryID = -1;
		public int EntryID { get { return mvarEntryID; } set { mvarEntryID = value; } }

		private ResourceTableEntryFlags mvarFlags = ResourceTableEntryFlags.None;
		public ResourceTableEntryFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		public object Clone()
		{
			ResourceTableEntry clone = new ResourceTableEntry();
			clone.Name = (mvarName.Clone() as string);
			clone.EntryID = mvarEntryID;
			clone.Flags = mvarFlags;
			return clone;
		}
	}
}
