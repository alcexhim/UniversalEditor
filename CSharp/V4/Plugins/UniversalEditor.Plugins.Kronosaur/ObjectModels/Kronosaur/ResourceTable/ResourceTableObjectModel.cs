using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Kronosaur.ResourceTable
{
	public class ResourceTableObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
			}
			return _omr;
		}

		private ResourceTableEntry.ResourceTableEntryCollection mvarEntries = new ResourceTableEntry.ResourceTableEntryCollection();
		public ResourceTableEntry.ResourceTableEntryCollection Entries { get { return mvarEntries; } }

		public override void Clear()
		{
			mvarEntries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ResourceTableObjectModel clone = (where as ResourceTableObjectModel);
			foreach (ResourceTableEntry item in mvarEntries)
			{
				clone.Entries.Add(item.Clone() as ResourceTableEntry);
			}
		}
	}
}
