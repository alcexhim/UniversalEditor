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

			private Dictionary<string, CoreObjectProperty> _itemsByName = new Dictionary<string, CoreObjectProperty>();

			public CoreObjectProperty this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name)) return _itemsByName[name];
					return null;
				}
			}

			protected override void InsertItem(int index, CoreObjectProperty item)
			{
				base.InsertItem(index, item);
				item.ParentGroup = _parent;
				_itemsByName.Add(item.Name, item);
			}
			protected override void RemoveItem(int index)
			{
				this[index].ParentGroup = null;
				_itemsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, CoreObjectProperty item)
			{
				if (index > -1 && index < this.Count - 1)
				{
					this[index].ParentGroup = null;
					_itemsByName.Remove(this[index].Name);
				}
				base.SetItem(index, item);
				_itemsByName.Add(item.Name, item);
				item.ParentGroup = _parent;
			}
			protected override void ClearItems()
			{
				foreach (CoreObjectProperty item in this)
				{
					item.ParentGroup = null;
				}
				_itemsByName.Clear();
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
				foreach (CoreObjectAttribute att in mvarAttributes)
				{
					sb.Append(att.Name);
					if (att.Values.Count > 0)
					{
						sb.Append("=");
						for (int i = 0; i < att.Values.Count; i++)
						{
							sb.Append(att.Values[i]);
							if (i < att.Values.Count - 1) sb.Append(',');
						}
					}
				}
			}
			return sb.ToString();
		}
	}
}
