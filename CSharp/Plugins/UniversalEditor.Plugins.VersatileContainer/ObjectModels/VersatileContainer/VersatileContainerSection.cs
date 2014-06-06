using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.ObjectModels.VersatileContainer.Sections;

namespace UniversalEditor.ObjectModels.VersatileContainer
{
	public abstract class VersatileContainerSection : ICloneable
	{
		public class VersatileContainerSectionCollection
			: System.Collections.ObjectModel.Collection<VersatileContainerSection>
		{
			private Dictionary<string, VersatileContainerSection> sectionsByName = new Dictionary<string, VersatileContainerSection>();

			public VersatileContainerSection this[string Name]
			{
				get
				{
					if (sectionsByName.ContainsKey(Name))
					{
						return sectionsByName[Name];
					}
					return null;
				}
			}

			protected override void InsertItem(int index, VersatileContainerSection item)
			{
                if (!String.IsNullOrEmpty(item.Name)) sectionsByName.Add(item.Name, item);
				base.InsertItem(index, item);
			}
			protected override void RemoveItem(int index)
			{
                if (!String.IsNullOrEmpty(this[index].Name)) sectionsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}


			public VersatileContainerContentSection Add(string Name, byte[] Data)
			{
				return Add(Name, String.Empty, Data);
			}
            public VersatileContainerContentSection Add(string Name, string ClassName, byte[] Data)
            {
                VersatileContainerContentSection content = new VersatileContainerContentSection();
                content.Name = Name;
                content.ClassName = ClassName;
                content.Data = Data;
                Add(content);
                return content;
            }
        }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

        public abstract object Clone();
	}
}
