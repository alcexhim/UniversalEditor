using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.CoreObject
{
	public class CoreObjectProperty : ICloneable
	{
		public class CoreObjectPropertyCollection
			: System.Collections.ObjectModel.Collection<CoreObjectProperty>
		{
			private CoreObjectGroup _parent = null;
			public CoreObjectPropertyCollection(CoreObjectGroup parent = null)
			{
				_parent = parent;
			}

			public CoreObjectProperty Add(string name, string[] values = null, CoreObjectAttribute[] attributes = null)
			{
				CoreObjectProperty item = new CoreObjectProperty();
				item.Name = name;
				if (values != null)
				{
					foreach (string value in values)
					{
						item.Values.Add(value);
					}
				}
				if (attributes != null)
				{
					foreach (CoreObjectAttribute att in attributes)
					{
						item.Attributes.Add(att);
					}
				}
				Add(item);
				return item;
			}

			public CoreObjectProperty this[string name]
			{
				get
				{
					foreach (CoreObjectProperty item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}

			protected override void InsertItem(int index, CoreObjectProperty item)
			{
				base.InsertItem(index, item);
				item.ParentGroup = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].ParentGroup = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, CoreObjectProperty item)
			{
				if (index > -1 && index < this.Count - 1)
				{
					this[index].ParentGroup = null;
				}
				base.SetItem(index, item);
				item.ParentGroup = _parent;
			}
			protected override void ClearItems()
			{
				foreach (CoreObjectProperty item in this)
				{
					item.ParentGroup = null;
				}
				base.ClearItems();
			}

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private CoreObjectGroup mvarParentGroup = null;
		public CoreObjectGroup ParentGroup
		{
			get { return mvarParentGroup; }
			internal set
			{
				mvarParentGroup = value;
			}
		}

		private CoreObjectAttribute.CoreObjectAttributeCollection mvarAttributes = new CoreObjectAttribute.CoreObjectAttributeCollection();
		public CoreObjectAttribute.CoreObjectAttributeCollection Attributes { get { return mvarAttributes; } }

		private System.Collections.Specialized.StringCollection mvarValues = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Values { get { return mvarValues; } }

		public object Clone()
		{
			CoreObjectProperty clone = new CoreObjectProperty();
			clone.Name = (mvarName.Clone() as string);
			foreach (CoreObjectAttribute item in mvarAttributes)
			{
				clone.Attributes.Add(item.Clone() as CoreObjectAttribute);
			}
			foreach (string item in mvarValues)
			{
				clone.Values.Add(item);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			if (mvarAttributes.Count > 0)
			{
				sb.Append(";");
				for (int i = 0; i < mvarAttributes.Count; i++)
				{
					CoreObjectAttribute att = mvarAttributes[i];
					sb.Append(att.Name);
					if (att.Values.Count > 0)
					{
						sb.Append("=");
						for (int j = 0; j < att.Values.Count; j++)
						{
							sb.Append(att.Values[j]);
							if (j < att.Values.Count - 1) sb.Append(',');
						}
					}
					if (i < mvarAttributes.Count - 1) sb.Append(";");
				}
				if (mvarValues.Count > 0)
				{
					sb.Append(":");
					for (int i = 0; i < mvarValues.Count; i++)
					{
						sb.Append(mvarValues[i]);
						if (i < mvarValues.Count - 1) sb.Append(";");
					}
				}
			}
			return sb.ToString();
		}
	}
}
