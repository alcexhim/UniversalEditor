using System;
using System.Collections;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface IListViewNativeImplementation
		{
			void UpdateTreeModel ();
			void UpdateTreeModel(NativeControl handle, TreeModelChangedEventArgs e);

			void UpdateTreeModelColumn(TreeModelRowColumn rc);

			SelectionMode GetSelectionMode();
			void SetSelectionMode(SelectionMode value);

			ListViewHitTestInfo HitTest(double x, double y);

			bool IsColumnReorderable(ListViewColumn column);
			void SetColumnReorderable(ListViewColumn column, bool value);

			bool IsColumnResizable(ListViewColumn column);
			void SetColumnResizable(ListViewColumn column, bool value);
			void SetColumnEditable(ListViewColumn column, bool value);

			void AddColumnValidValues(ListViewColumnText column, IList items);
			void RemoveColumnValidValues(ListViewColumnText column, IList items);
		}
	}

	public delegate void ListViewRowActivatedEventHandler(object sender, ListViewRowActivatedEventArgs e);
	public class ListViewRowActivatedEventArgs : EventArgs
	{
		/// <summary>
		/// The row that was activated.
		/// </summary>
		/// <value>The row that was activated.</value>
		public TreeModelRow Row { get; private set; } = null;

		public ListViewRowActivatedEventArgs(TreeModelRow row)
		{
			Row = row;
		}
	}

	public abstract class ListViewColumn
	{
		public class ListViewColumnCollection
			: System.Collections.ObjectModel.Collection<ListViewColumn>
		{
			private ListView _parent = null;
			public ListViewColumnCollection(ListView parent)
			{
				_parent = parent;
			}

			protected override void InsertItem(int index, ListViewColumn item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
					this[i].Parent = null;
				base.ClearItems();
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
		}

		public ListView Parent { get; private set; } = null;

		private TreeModelColumn mvarColumn = null;
		public TreeModelColumn Column { get { return mvarColumn; } set { mvarColumn = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private bool _Resizable = true;
		public bool Resizable
		{
			get
			{

				if (Parent != null)
				{
					if (Parent.IsCreated)
					{
						_Resizable = ((Parent.ControlImplementation as Native.IListViewNativeImplementation)?.IsColumnResizable(this)).GetValueOrDefault(_Resizable);
					}
				}
				return _Resizable;
			}
			set
			{
				if (Parent != null)
				{
					if (Parent.IsCreated)
					{
						(Parent.ControlImplementation as Native.IListViewNativeImplementation)?.SetColumnResizable(this, value);
					}
				}
				_Resizable = value;
			}
		}

		private bool _Reorderable = true;
		public bool Reorderable
		{
			get
			{
				if (Parent != null)
				{
					if (Parent.IsCreated)
					{
						_Reorderable = ((Parent.ControlImplementation as Native.IListViewNativeImplementation)?.IsColumnReorderable(this)).GetValueOrDefault(_Reorderable);
					}
				}
				return _Reorderable;
			}
			set
			{
				if (Parent != null)
				{
					if (Parent.IsCreated)
					{
						(Parent.ControlImplementation as Native.IListViewNativeImplementation)?.SetColumnReorderable(this, value);
					}
				}
				_Reorderable = value;
			}
		}

		private bool _Editable = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:MBS.Framework.UserInterface.Controls.ListViewColumn"/>
		/// is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		public bool Editable
		{
			get { return _Editable; }
			set
			{
				(Parent?.ControlImplementation as Native.IListViewNativeImplementation)?.SetColumnEditable(this, value);
				_Editable = value;
			}
		}

		public ListViewColumn(TreeModelColumn column, string title = "")
		{
			mvarColumn = column;
			mvarTitle = title;
		}
	}
	public class ListViewColumnCheckBox
		: ListViewColumn
	{
		public ListViewColumnCheckBox(TreeModelColumn column, string title = "") : base(column, title)
		{
		}
	}
	public class ListViewColumnText
		: ListViewColumn
	{
		/// <summary>
		/// Gets a collection of <see cref="String" /> values that are valid for this <see cref="ListViewColumn" />.
		/// </summary>
		/// <value>The valid values.</value>
		public System.Collections.ObjectModel.ObservableCollection<string> ValidValues { get; } = new System.Collections.ObjectModel.ObservableCollection<string>();

		public ListViewColumnText(TreeModelColumn column, string title = "", string[] validValues = null) : base(column, title)
		{
			ValidValues.CollectionChanged += ValidValues_CollectionChanged;
			if (validValues != null)
			{
				for (int i = 0; i < validValues.Length; i++)
				{
					ValidValues.Add(validValues[i]);
				}
			}
		}

		private void ValidValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
				{
					(Parent.ControlImplementation as Native.IListViewNativeImplementation)?.AddColumnValidValues(this, e.NewItems);
					break;
				}
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
				{
					(Parent.ControlImplementation as Native.IListViewNativeImplementation)?.RemoveColumnValidValues(this, e.OldItems);
					break;
				}
			}
		}

	}
	public class ListView : SystemControl
	{
		public ListView()
		{
			this.SelectedRows = new TreeModelRow.TreeModelSelectedRowCollection(this);
			mvarColumns = new ListViewColumn.ListViewColumnCollection(this);
		}

		private SelectionMode mvarSelectionMode = SelectionMode.Single;
		public SelectionMode SelectionMode
		{
			get
			{
				try
				{
					if (this.IsCreated)
						mvarSelectionMode = (ControlImplementation as Native.IListViewNativeImplementation).GetSelectionMode();
				}
				catch (Exception)
				{
				}
				return mvarSelectionMode;
			}
			set
			{
				if (this.IsCreated)
					(ControlImplementation as Native.IListViewNativeImplementation).SetSelectionMode(value);
				mvarSelectionMode = value;
			}
		}

		private void RecursiveSetControlParent (TreeModelRow row)
		{
			row.ParentControl = this;
			foreach (TreeModelRow row2 in row.Rows) {
				RecursiveSetControlParent (row2);
			}
		}

		private DefaultTreeModel mvarModel = null;
		public DefaultTreeModel Model
		{
			get { return mvarModel; }
			set
			{
				mvarModel = value;
				if (mvarModel != null)
				{
					mvarModel.TreeModelChanged += MvarModel_TreeModelChanged;
					foreach (TreeModelRow row in mvarModel.Rows)
					{
						RecursiveSetControlParent(row);
					}
				}
				(ControlImplementation as Native.IListViewNativeImplementation)?.UpdateTreeModel();
			}
		}

		public ListViewHitTestInfo LastHitTest { get; private set; } = new ListViewHitTestInfo(null, null);
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			LastHitTest = HitTest(e.X, e.Y);
		}

		public event TreeModelChangedEventHandler TreeModelChanged;
		public void OnTreeModelChanged(object sender, TreeModelChangedEventArgs e)
		{
			TreeModelChanged?.Invoke(sender, e);
		}
		public event ListViewRowActivatedEventHandler RowActivated;
		public virtual void OnRowActivated(ListViewRowActivatedEventArgs e)
		{
			RowActivated?.Invoke(this, e);
		}

		public event EventHandler SelectionChanged;
		public virtual void OnSelectionChanged(EventArgs e)
		{
			SelectionChanged?.Invoke(this, e);
		}

		private void MvarModel_TreeModelChanged(object sender, TreeModelChangedEventArgs e)
		{
			OnTreeModelChanged(sender, e);

			switch (e.Action)
			{
				case TreeModelChangedAction.Add:
				{
					foreach (TreeModelRow row in e.Rows)
					{
						row.ParentControl = this;
					}
					break;
				}
			}

			(ControlImplementation as Native.IListViewNativeImplementation)?.UpdateTreeModel(ControlImplementation.Handle, e);
		}

		private ListViewColumn.ListViewColumnCollection mvarColumns = null;
		public ListViewColumn.ListViewColumnCollection Columns { get { return mvarColumns; } }

		public ColumnHeaderStyle HeaderStyle { get; set; } = ColumnHeaderStyle.Clickable;
		public TreeModelRow.TreeModelSelectedRowCollection SelectedRows { get; private set; } = null;

		public ListViewMode Mode { get; set; } = ListViewMode.Detail;

		/// <summary>
		/// Hits the test.
		/// </summary>
		/// <returns>A <see cref="ListViewHitTestInfo" /> indicating the results of the hit test. For <see cref="ListView" /> instances with a <see cref="ControlImplementation" />, this method SHOULD NEVER return null.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public ListViewHitTestInfo HitTest(double x, double y)
		{
			Native.IListViewNativeImplementation impl = (ControlImplementation as Native.IListViewNativeImplementation);
			if (impl != null)
				return impl.HitTest(x, y);
			return null;
		}

		/// <summary>
		/// Selects the specified <see cref="TreeModelRow"/>.
		/// </summary>
		/// <param name="row">Tree model row.</param>
		public void Select(TreeModelRow row)
		{
			SelectedRows.Add(row);
		}

		public event TreeModelRowColumnEditingEventHandler RowColumnEditing;
		protected virtual void OnRowColumnEditing(TreeModelRowColumnEditingEventArgs e)
		{
			RowColumnEditing?.Invoke(this, e);
		}
		public event TreeModelRowColumnEditedEventHandler RowColumnEdited;
		protected virtual void OnRowColumnEdited(TreeModelRowColumnEditedEventArgs e)
		{
			RowColumnEdited?.Invoke(this, e);
		}
	}
}

