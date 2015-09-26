using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomDataFormatStructureInstance
	{
		private CustomDataFormatStructure mvarStructure = null;
		public CustomDataFormatStructure Structure { get { return mvarStructure; } }

		private CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection mvarItems = null;
		public CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection Items { get { return mvarItems; } }

		internal CustomDataFormatStructureInstance(CustomDataFormatStructure structure)
		{
			mvarStructure = structure;

			List<CustomDataFormatItem> items = new List<CustomDataFormatItem>();
			foreach (CustomDataFormatItem item in structure.Items)
			{
				items.Add(item);
			}
			mvarItems = new CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection(items);
		}
	}
}
