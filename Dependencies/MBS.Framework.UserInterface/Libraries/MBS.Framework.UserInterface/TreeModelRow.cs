using System;
using System.Collections;
using System.Collections.Generic;
using UniversalEditor;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface
{
	namespace Native
	{
		public interface ITreeModelRowCollectionNativeImplementation
		{
			bool IsRowExpanded(TreeModelRow row);
			void SetRowExpanded(TreeModelRow row, bool expanded);
		}
	}
	public class TreeModelRow
	{
		public class TreeModelSelectedRowCollection : IEnumerable<TreeModelRow>
		{
			public class TreeModelSelectedRowCollectionEnumerator : IEnumerator<TreeModelRow>
			{
				private int _index = -1;
				private TreeModelSelectedRowCollection _parent = null;
				internal TreeModelSelectedRowCollectionEnumerator(TreeModelSelectedRowCollection parent)
				{
					_parent = parent;
				}

				public TreeModelRow Current => _parent[_index];
				object IEnumerator.Current => _parent[_index];

				public void Dispose()
				{
				}

				public bool MoveNext()
				{
					_index++;
					return _index < _parent.Count;
				}

				public void Reset()
				{
					_index = -1;
				}
			}

			public ListView Parent { get; private set; }
			public TreeModelSelectedRowCollection(ListView parent)
			{
				Parent = parent;
			}

			private List<TreeModelRow> _list = new List<TreeModelRow>();

			public void Add(TreeModelRow row)
			{
				OnItemRequested(new TreeModelRowItemRequestedEventArgs(row, 1, -1));
			}
			public TreeModelRow this[int index]
			{
				get
				{
					TreeModelRow item = (index < _list.Count ? _list[index] : null);
					TreeModelRowItemRequestedEventArgs e = new TreeModelRowItemRequestedEventArgs(item, _list.Count, index);
					OnItemRequested(e);
					if (e.Cancel) return item;
					return e.Item;
				}
			}
			public int Count
			{
				get
				{
					TreeModelRowItemRequestedEventArgs e = new TreeModelRowItemRequestedEventArgs(null, _list.Count, -1);
					OnItemRequested(e);
					if (e.Cancel) return _list.Count;
					return e.Count;
				}
			}

			public event TreeModelRowItemRequestedEventHandler ItemRequested;
			protected virtual void OnItemRequested(TreeModelRowItemRequestedEventArgs e)
			{
				ItemRequested?.Invoke(this, e);
			}
			public event EventHandler Cleared;
			protected virtual void OnCleared(EventArgs e)
			{
				Cleared?.Invoke(this, e);
			}

			public bool Contains(TreeModelRow tn)
			{
				TreeModelRowItemRequestedEventArgs e = new TreeModelRowItemRequestedEventArgs(tn, _list.Count, -1);
				OnItemRequested(e);

				if (e.Cancel) return _list.Contains(tn);
				if (e.Count == 0 || e.Item == null) return false;

				return (e.Item == tn);
			}

			public void Clear()
			{
				OnCleared(EventArgs.Empty);
			}

			public IEnumerator<TreeModelRow> GetEnumerator()
			{
				return new TreeModelSelectedRowCollectionEnumerator(this);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new TreeModelSelectedRowCollectionEnumerator(this);
			}
		}

		internal void UpdateColumnValue(TreeModelRowColumn rc)
		{
			(ParentControl?.ControlImplementation as Controls.Native.IListViewNativeImplementation)?.UpdateTreeModelColumn(rc);
		}

		public class TreeModelRowCollection
			: System.Collections.ObjectModel.ObservableCollection<TreeModelRow>
		{
			public TreeModel Model { get; private set; } = null;
			public TreeModelRowCollection(TreeModel model = null)
			{
				Model = model;
			}

			private Dictionary<string, TreeModelRow> _itemsByName = new Dictionary<string, TreeModelRow>();
			public TreeModelRow this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
					{
						return _itemsByName[name];
					}
					return null;
				}
			}
			public bool Contains(string name)
			{
				if (String.IsNullOrEmpty(name))
				{
					Console.Error.WriteLine("uwt: warning: 'name' for TreeModelRow is empty");
					return false;
				}

				return _itemsByName.ContainsKey(name);
			}

			public void AddRange(TreeModelRow[] items)
			{
				for (int i = 0; i < items.Length; i++)
				{
					Add(items[i]);
				}
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByName.Clear();
			}
			protected override void InsertItem(int index, TreeModelRow item)
			{
				base.InsertItem(index, item);
				if (item.Name != null)
					_itemsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (this[index].Name != null)
					_itemsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, TreeModelRow item)
			{
				if (this[index].Name != null)
					_itemsByName.Remove(this[index].Name);
				base.SetItem(index, item);
				if (item.Name != null)
					_itemsByName[item.Name] = item;
			}

			public new int Count
			{
				get
				{
					TreeModelRowItemRequestedEventArgs e = new TreeModelRowItemRequestedEventArgs(null, base.Count, -1);
					OnItemRequested(e);
					if (e.Cancel) return base.Count;
					return e.Count;
				}
			}
			public new TreeModelRow this[int index]
			{
				get
				{
					TreeModelRow originalItem = index < base.Count ? base[index] : null;
					TreeModelRowItemRequestedEventArgs e = new TreeModelRowItemRequestedEventArgs(originalItem, base.Count, index);
					OnItemRequested(e);
					if (e.Cancel) return originalItem;
					return e.Item;
				}
			}

			public event TreeModelRowItemRequestedEventHandler ItemRequested;
			private void OnItemRequested(TreeModelRowItemRequestedEventArgs e)
			{
				ItemRequested?.Invoke(this, e);
			}
		}

		public void ExpandAll()
		{
			Expanded = true;
			for (int i = 0; i < Rows.Count; i++)
			{
				Rows[i].ExpandAll();
			}
		}
		public void CollapseAll()
		{
			Expanded = false;
			for (int i = 0; i < Rows.Count; i++)
			{
				Rows[i].CollapseAll();
			}
		}

		public TreeModelRow.TreeModelRowCollection Rows { get; } = new TreeModelRowCollection();

		private TreeModelRowColumn.TreeModelRowColumnCollection mvarRowColumns = null;
		public TreeModelRowColumn.TreeModelRowColumnCollection RowColumns { get { return mvarRowColumns; } }

		public TreeModelRow(TreeModelRowColumn[] rowColumns = null)
		{
			this.Rows.CollectionChanged += Rows_CollectionChanged;
			mvarRowColumns = new TreeModelRowColumn.TreeModelRowColumnCollection(this);
			if (rowColumns != null)
			{
				foreach (TreeModelRowColumn rc in rowColumns)
				{
					mvarRowColumns.Add(rc);
				}
			}
		}

		private Control _ParentControl = null;
		public Control ParentControl
		{
			get { return _ParentControl; }
			internal set
			{
				_ParentControl = value;
				for (int i = 0; i < this.Rows.Count; i++)
				{
					this.Rows[i].ParentControl = value;
				}
			}
		}
		public TreeModelRow ParentRow { get; private set; }
		public string Name { get; set; }

		void Rows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
			case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
				{
					List<TreeModelRow> list = new List<TreeModelRow>();
					foreach (TreeModelRow row in e.NewItems)
					{
						row.ParentRow = this;
						row.ParentControl = this.ParentControl;
						list.Add(row);
					}
					if (ParentControl != null)
					{
						(ParentControl.ControlImplementation as MBS.Framework.UserInterface.Controls.Native.IListViewNativeImplementation)?.UpdateTreeModel(ParentControl.ControlImplementation.Handle, new TreeModelChangedEventArgs(TreeModelChangedAction.Add, list.ToArray(), this));
					}
					break;
				}
			case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
				{
					List<TreeModelRow> list = new List<TreeModelRow>();
					foreach (TreeModelRow row in e.NewItems)
					{
						row.ParentRow = this;
						row.ParentControl = this.ParentControl;
						list.Add(row);
					}
					if (ParentControl != null)
					{
						(ParentControl.ControlImplementation as MBS.Framework.UserInterface.Controls.Native.IListViewNativeImplementation)?.UpdateTreeModel(ParentControl.ControlImplementation.Handle, new TreeModelChangedEventArgs(TreeModelChangedAction.Remove, list.ToArray(), this));
					}
					break;
				}
			}
		}

		private bool mvarExpanded = false;
		public bool Expanded
		{
			get
			{
				mvarExpanded = ((ParentControl?.ControlImplementation as Native.ITreeModelRowCollectionNativeImplementation)?.IsRowExpanded(this)).GetValueOrDefault(false);
				return mvarExpanded;
			}
			set
			{
				if (ParentControl == null)
				{
					Console.Error.WriteLine("uwt: TreeModelRow: parent control is NULL");
				}
				else if (ParentControl.ControlImplementation == null)
				{
					Console.Error.WriteLine("uwt: TreeModelRow: NativeImplementation is NULL");
				}
				(ParentControl?.ControlImplementation as Native.ITreeModelRowCollectionNativeImplementation)?.SetRowExpanded(this, value);
				mvarExpanded = value;
			}
		}

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key))
				return (T)_ExtraData[key];
			return defaultValue;
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			return GetExtraData<object>(key, defaultValue);
		}
		public void SetExtraData(string key, object value)
		{
			SetExtraData<object>(key, value);
		}
	}
}
