using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public class TreeModelRowColumn
	{
		public class TreeModelRowColumnCollection
			: System.Collections.ObjectModel.Collection<TreeModelRowColumn>
		{
			private TreeModelRow _parent = null;
			public TreeModelRowColumnCollection(TreeModelRow parent)
			{
				_parent = parent;
			}

			private Dictionary<TreeModelColumn, TreeModelRowColumn> _ItemsByColumn = new Dictionary<TreeModelColumn, TreeModelRowColumn>();
			public TreeModelRowColumn this[TreeModelColumn c]
			{
				get
				{
					if (_ItemsByColumn.ContainsKey(c))
						return _ItemsByColumn[c];
					return null;
				}
			}

			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
					this[i].Parent = null;

				_ItemsByColumn.Clear();
				base.ClearItems();
			}
			protected override void InsertItem(int index, TreeModelRowColumn item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;

				if (item.Column != null)
					_ItemsByColumn[item.Column] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (this[index].Column != null)
					_ItemsByColumn[this[index].Column] = null;

				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, TreeModelRowColumn item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = _parent;
			}
		}

		public TreeModelRow Parent { get; private set; } = null;

		private TreeModelColumn mvarColumn = null;
		public TreeModelColumn Column { get { return mvarColumn; } }
		private object mvarValue = null;
		public object Value
		{
			get { return mvarValue; }
			set
			{
				mvarValue = value;
				if (Parent != null)
					Parent.UpdateColumnValue(this);
			}
		}

		private object _RawValue = null;
		/// <summary>
		/// Gets the value that is used to sort this <see cref="TreeModelRowColumn" />.
		/// </summary>
		/// <value>The raw value.</value>
		public object RawValue
		{
			get
			{
				if (_RawValue != null)
					return _RawValue;
				return Value;
			}
			set
			{ _RawValue = value; }
		}

		public TreeModelRowColumn(TreeModelColumn column, object value)
		{
			mvarColumn = column;
			mvarValue = value;
		}
	}
}
