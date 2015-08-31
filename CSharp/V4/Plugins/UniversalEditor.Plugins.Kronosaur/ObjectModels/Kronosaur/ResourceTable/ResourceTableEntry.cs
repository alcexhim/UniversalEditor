using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Kronosaur.ResourceTable
{
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
