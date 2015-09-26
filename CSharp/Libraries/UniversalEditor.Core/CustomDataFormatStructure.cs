using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomDataFormatStructure
	{
		public class CustomDataFormatStructureCollection
			: System.Collections.ObjectModel.Collection<CustomDataFormatStructure>
		{
			public CustomDataFormatStructure this[Guid id]
			{
				get
				{
					foreach (CustomDataFormatStructure item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private CustomDataFormatItem.CustomDataFormatItemCollection mvarItems = new CustomDataFormatItem.CustomDataFormatItemCollection();
		public CustomDataFormatItem.CustomDataFormatItemCollection Items
		{
			get { return mvarItems; }
		}

		public CustomDataFormatStructureInstance CreateInstance()
		{
			CustomDataFormatStructureInstance inst = new CustomDataFormatStructureInstance(this);
			return inst;
		}
	}
}
