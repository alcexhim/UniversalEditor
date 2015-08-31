using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.VersatileContainer
{
	public class VersatileContainerProperty
	{
		public class VersatileContainerPropertyCollection
			: System.Collections.ObjectModel.Collection<VersatileContainerProperty>
		{
			public VersatileContainerProperty Add(string name, string value)
			{
				VersatileContainerProperty property = new VersatileContainerProperty();
				property.Name = name;
				property.Value = value;
				base.Add(property);
				return property;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }
	}
}
