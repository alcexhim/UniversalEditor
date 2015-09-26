using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.CoreObject
{
	public class CoreObjectGroup : ICloneable
	{
		public class CoreObjectGroupCollection
			: System.Collections.ObjectModel.Collection<CoreObjectGroup>
		{
			private CoreObjectGroup _parent = null;
			public CoreObjectGroupCollection(CoreObjectGroup parent = null)
			{
				_parent = parent;
			}

			public CoreObjectGroup Add(string name)
			{
				CoreObjectGroup item = new CoreObjectGroup();
				item.Name = name;
				Add(item);
				return item;
			}

			private Dictionary<string, CoreObjectGroup> _itemsByName = new Dictionary<string, CoreObjectGroup>();

			public CoreObjectGroup this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name)) return _itemsByName[name];
					return null;
				}
			}

			protected override void InsertItem(int index, CoreObjectGroup item)
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
			protected override void SetItem(int index, CoreObjectGroup item)
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
				foreach (CoreObjectGroup item in this)
				{
					item.ParentGroup = null;
				}
				_itemsByName.Clear();
				base.ClearItems();
			}

		}

		public CoreObjectGroup()
		{
			mvarGroups = new CoreObjectGroupCollection(this);
			mvarProperties = new CoreObjectProperty.CoreObjectPropertyCollection(this);
		}

		private CoreObjectGroup mvarParentGroup = null;
		public CoreObjectGroup ParentGroup
		{
			get { return mvarParentGroup; }
			internal set
			{
				mvarParentGroup = value;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private CoreObjectGroup.CoreObjectGroupCollection mvarGroups = null;
		public CoreObjectGroup.CoreObjectGroupCollection Groups { get { return mvarGroups; } }

		private CoreObjectProperty.CoreObjectPropertyCollection mvarProperties = new CoreObjectProperty.CoreObjectPropertyCollection();
		public CoreObjectProperty.CoreObjectPropertyCollection Properties { get { return mvarProperties; } }

		public object Clone()
		{
			CoreObjectGroup clone = new CoreObjectGroup();
			clone.Name = (mvarName.Clone() as string);
			foreach (CoreObjectGroup item in mvarGroups)
			{
				clone.Groups.Add(item.Clone() as CoreObjectGroup);
			}
			foreach (CoreObjectProperty item in mvarProperties)
			{
				clone.Properties.Add(item.Clone() as CoreObjectProperty);
			}
			return clone;
		}
	}
}
