using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(ListView))]
	public class ListViewImplementation : WindowsFormsNativeImplementation, IListViewNativeImplementation, MBS.Framework.UserInterface.Native.ITreeModelRowCollectionNativeImplementation
	{
		private enum ImplementedAsType
		{
			/// <summary>
			/// The control can be implemented as a System.Windows.Forms.TreeView control.
			/// </summary>
			TreeView,
			/// <summary>
			/// The control can be implemented as a System.Windows.Forms.ListView control.
			/// </summary>
			ListView
		}
		private static ImplementedAsType ImplementedAs(ListView tv)
		{
			bool rowsHaveChildren = false;
			if (tv.Model != null)
			{
				for (int i = 0; i < tv.Model.Rows.Count; i++)
				{
					if (tv.Model.Rows[i].Rows.Count > 0)
					{
						rowsHaveChildren = true;
						break;
					}
				}
			}

			if (rowsHaveChildren && tv.Columns.Count == 1)
			{
				// ListView cannot have child rows, and we only have one detail column, so...
				// might as well implement it using native TreeView
				return ImplementedAsType.TreeView;
			}

			// we may or may not have to build our own fake-treeview
			return ImplementedAsType.ListView;
		}
		
		public ListViewImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		private SelectionMode _SelectionMode = SelectionMode.Single;
		private void SetSelectionModeInternal(System.Windows.Forms.Control handle, ListView tv, SelectionMode value)
		{
			_SelectionMode = value;
			switch (value)
			{
				case SelectionMode.Browse:
				{
					break;
				}
				case SelectionMode.Multiple:
				{
					if (handle is System.Windows.Forms.TreeView)
					{
					}
					else if (handle is System.Windows.Forms.ListView)
					{
						(handle as System.Windows.Forms.ListView).MultiSelect = true;
					}
					break;
				}
				case SelectionMode.Single:
				{
					if (handle is System.Windows.Forms.TreeView)
					{
					}
					else if (handle is System.Windows.Forms.ListView)
					{
						(handle as System.Windows.Forms.ListView).MultiSelect = false;
					}
					break;
				}
			}
		}

		private SelectionMode GetSelectionModeInternal(System.Windows.Forms.Control handle, ListView tv)
		{
			switch (ImplementedAs(tv))
			{
				case ImplementedAsType.TreeView:
				{
					break;
				}
				case ImplementedAsType.ListView:
				{
					if (_SelectionMode != SelectionMode.None && _SelectionMode != SelectionMode.Browse)
					{
						if ((handle as System.Windows.Forms.ListView).MultiSelect)
						{
							_SelectionMode = SelectionMode.Multiple;
						}
						else
						{
							_SelectionMode = SelectionMode.Single;
						}
					}
					break;
				}
			}
			return _SelectionMode;
		}

		public void SetSelectionMode(SelectionMode value)
		{
			System.Windows.Forms.Control handle = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Control);
			ListView tv = Control as ListView;
			SetSelectionModeInternal(handle, tv, value);
		}
		public SelectionMode GetSelectionMode()
		{
			System.Windows.Forms.Control handle = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Control);
			ListView tv = Control as ListView;
			return GetSelectionModeInternal(handle, tv);
		}

		public ListViewHitTestInfo HitTest(double x, double y)
		{
			return HitTestInternal((Handle as WindowsFormsNativeControl).Handle, x, y);
		}
		private ListViewHitTestInfo HitTestInternal(System.Windows.Forms.Control handle, double x, double y)
		{
			TreeModelRow row = null;
			TreeModelColumn column = null;
			if (handle is System.Windows.Forms.ListView)
			{
				System.Windows.Forms.ListViewHitTestInfo info = (handle as System.Windows.Forms.ListView).HitTest((int)x, (int)y);
				if (info?.Item != null)
				{
					row = (info.Item.Tag as TreeModelRow);
				}
				if (info?.SubItem != null)
				{
					if (info.SubItem.Tag is TreeModelRow)
					{
					}
					else if (info.SubItem.Tag is TreeModelRowColumn)
					{
						column = (info.SubItem.Tag as TreeModelRowColumn).Column;
					}
				}
			}
			else if (handle is System.Windows.Forms.TreeView)
			{
				System.Windows.Forms.TreeViewHitTestInfo info = (handle as System.Windows.Forms.TreeView).HitTest((int)x, (int)y);
				if (info?.Node != null)
				{
					row = (info.Node.Tag as TreeModelRow);
				}
			}
			return new ListViewHitTestInfo(row, column);
		}

		public void UpdateTreeModel ()
		{
			UpdateTreeModel ((Handle as WindowsFormsNativeControl).Handle);
		}

		public void UpdateTreeModelColumn(TreeModelRowColumn rc)
		{
			TreeModel tm = (rc.Parent.ParentControl as ListView).Model;

			// Internal.GTK.Methods.GtkTreeStore.gtk_tree_store_set_value(hTreeStore, ref hIter, tm.Columns.IndexOf(rc.Column), ref val);
		}

		private Dictionary<ListViewColumn, IntPtr> _ColumnHandles = new Dictionary<ListViewColumn, IntPtr>();
		private Dictionary<IntPtr, ListViewColumn> _HandleColumns = new Dictionary<IntPtr, ListViewColumn>();
		private IntPtr GetHandleForTreeViewColumn(ListViewColumn column)
		{
			if (!_ColumnHandles.ContainsKey(column))
				return IntPtr.Zero;
			return _ColumnHandles[column];
		}
		private ListViewColumn GetTreeViewColumnForHandle(IntPtr handle)
		{
			if (!_HandleColumns.ContainsKey(handle))
				return null;
			return _HandleColumns[handle];
		}
		private void RegisterTreeViewColumn(ListViewColumn column, IntPtr handle)
		{
			_ColumnHandles[column] = handle;
			_HandleColumns[handle] = column;
		}

		private Dictionary<ListViewColumn, bool> _IsColumnResizable = new Dictionary<ListViewColumn, bool>();
		public bool IsColumnResizable(ListViewColumn column)
		{
			if (_IsColumnResizable.ContainsKey(column))
				return _IsColumnResizable[column];
			return true; // most Windows Forms ListView columns are
		}
		public void SetColumnResizable(ListViewColumn column, bool value)
		{
			_IsColumnResizable[column] = value;
		}

		private bool? _IsColumnReorderableSet = null;
		private Dictionary<ListViewColumn, bool> _IsColumnReorderable = new Dictionary<ListViewColumn, bool>();
		public bool IsColumnReorderable(ListViewColumn column)
		{
			System.Windows.Forms.ListView lv = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.ListView);

			if (_IsColumnReorderableSet == null)
			{
				/*
				bool reorderable = lv.AllowColumnReorder, oops = false;
				for (int i = 0; i < lv.Columns.Count; i++)
				{
					if ((lv.Columns[i].Tag as ListViewColumn).Reorderable != reorderable)
					{
						oops = true;
						break;
					}
				}

				if (oops)
				{
					_IsColumnReorderableSet = true;
				}
				else
				{
					_IsColumnReorderableSet = false;
				}
				*/
			}

			if (_IsColumnReorderableSet == true)
			{
				return _IsColumnReorderable[column];
			}
			return lv.AllowColumnReorder;
		}
		public void SetColumnReorderable(ListViewColumn column, bool value)
		{
			Console.WriteLine("SetColumnReorderable: in function");
			System.Windows.Forms.ListView lv = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.ListView);
			if (lv == null)
				return;

			Console.WriteLine("lv is a ListView");
			bool reorderable = lv.AllowColumnReorder, oops = false;
			for (int i = 0; i < lv.Columns.Count; i++)
			{
				if ((lv.Columns[i].Tag as ListViewColumn).Reorderable != reorderable)
				{
					oops = true;
					break;
				}
			}

			if (oops)
			{
				Console.WriteLine("oops");
				_IsColumnReorderableSet = true;
				_IsColumnReorderable[column] = value;
			}
			else
			{
				Console.WriteLine("ok");
				lv.AllowColumnReorder = value;
			}
		}

		protected virtual void OnRowColumnEditing(TreeModelRowColumnEditingEventArgs e)
		{
			InvokeMethod((Control as ListView), "OnRowColumnEditing", new object[] { e });
		}
		protected virtual void OnRowColumnEdited(TreeModelRowColumnEditedEventArgs e)
		{
			InvokeMethod((Control as ListView), "OnRowColumnEdited", new object[] { e });
		}

		protected void UpdateTreeModel (System.Windows.Forms.Control handle)
		{
			ListView tv = (Control as ListView);

			if (tv.Model != null)
			{
				tv.Model.TreeModelChanged += Model_TreeModelChanged;

				switch (ImplementedAs(tv))
				{
					case ImplementedAsType.ListView:
					{
						System.Windows.Forms.ListView lv = (handle as System.Windows.Forms.ListView);

						foreach (ListViewColumn tvc in tv.Columns)
						{
							TreeModelColumn c = tvc.Column;
							lv.Columns.Add(tvc.Title).Tag = tvc;
							SetColumnEditable(tvc, tvc.Editable);
						}

						for (int i = 0; i < tv.Model.Rows.Count; i++)
						{
							System.Windows.Forms.ListViewItem lvi = TreeModelRowToListViewItem(tv.Model.Rows[i]);
							lv.Items.Add(lvi);
						}
						break;
					}
					case ImplementedAsType.TreeView:
					{
						System.Windows.Forms.TreeView natv = (handle as System.Windows.Forms.TreeView);

						for (int i = 0; i < tv.Model.Rows.Count; i++)
						{
							RecursiveTreeStoreInsertRow(tv.Model, tv.Model.Rows[i], null, null);
						}
						break;
					}
				}
			}
		}

		void Model_TreeModelChanged(object sender, TreeModelChangedEventArgs e)
		{
			TreeModelRow.TreeModelRowCollection coll = (sender as TreeModelRow.TreeModelRowCollection);
			UpdateTreeModel(coll.Model, e);
		}


		private Dictionary<ListViewColumnText, DefaultTreeModel> TreeModelForListViewColumn = new Dictionary<ListViewColumnText, DefaultTreeModel>();
		public void AddColumnValidValues(ListViewColumnText tvc, System.Collections.IList list)
		{
			if (!TreeModelForListViewColumn.ContainsKey(tvc))
				return;

			DefaultTreeModel model = TreeModelForListViewColumn[tvc];
			for (int i = 0; i < list.Count; i++)
			{
				model.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(model.Columns[0], list[i])
				}));
			}
		}
		public void RemoveColumnValidValues(ListViewColumnText tvc, System.Collections.IList list)
		{
			if (!TreeModelForListViewColumn.ContainsKey(tvc))
				return;

			DefaultTreeModel model = TreeModelForListViewColumn[tvc];
			return;

			for (int i = 0; i < list.Count; i++)
			{
				model.Rows.Remove(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(model.Columns[0], list[i])
				}));
			}
		}

		private Dictionary<ListViewColumn, bool> _ColumnsEditable = new Dictionary<ListViewColumn, bool>();
		public void SetColumnEditable(ListViewColumn tvc, bool editable)
		{
			_ColumnsEditable[tvc] = editable;
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			ListView tv = (control as ListView);
			System.Windows.Forms.Control handle = null;

			// TODO: fix GtkTreeView implementation
			// Internal.GTK.Methods.Methods.gtk_tree_store_insert_with_valuesv(hTreeStore, ref hIterFirst, IntPtr.Zero, 0, ref columns, values, values.Length);
			tv.SelectedRows.ItemRequested += SelectedRows_ItemRequested;
			tv.SelectedRows.Cleared += SelectedRows_Cleared;

			if (tv.Model != null && !TreeModelAssociatedControls.ContainsKey(tv.Model))
			{
				TreeModelAssociatedControls.Add(tv.Model, new List<System.Windows.Forms.Control>());
			}

			switch (ImplementedAs (tv))
			{
				case ImplementedAsType.TreeView:
				{
					handle = new System.Windows.Forms.TreeView();
					handle.Tag = tv;
					(handle as System.Windows.Forms.TreeView).AfterLabelEdit += tv_AfterLabelEdit;

					(handle as System.Windows.Forms.TreeView).NodeMouseDoubleClick += tv_NodeMouseDoubleClick;
					(handle as System.Windows.Forms.TreeView).BeforeSelect += tv_BeforeSelect;
					(handle as System.Windows.Forms.TreeView).AfterSelect += tv_AfterSelect;

					if (tv.Model != null && !TreeModelAssociatedControls[tv.Model].Contains(handle))
					{
						TreeModelAssociatedControls[tv.Model].Add(handle);
					}
					break;
				}
				case ImplementedAsType.ListView:
				{
					handle = new System.Windows.Forms.ListView();
					handle.Tag = tv;
					(handle as System.Windows.Forms.ListView).HeaderStyle = WindowsFormsEngine.HeaderStyleToSWFHeaderStyle(tv.HeaderStyle);
					(handle as System.Windows.Forms.ListView).ItemActivate += lv_ItemActivate;
					(handle as System.Windows.Forms.ListView).ItemSelectionChanged += lv_ItemSelectionChanged;
					(handle as System.Windows.Forms.ListView).FullRowSelect = true;
					(handle as System.Windows.Forms.ListView).View = System.Windows.Forms.View.Details;

					if (tv.Model != null && !TreeModelAssociatedControls[tv.Model].Contains(handle))
					{
						TreeModelAssociatedControls[tv.Model].Add(handle);
					}
					break;
				}
			}

			if (tv.Model != null)
				UpdateTreeModel (handle);

			SetSelectionModeInternal(handle, tv, tv.SelectionMode);
			
			return new WindowsFormsNativeControl(handle);
		}

		void lv_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
		{
			System.Windows.Forms.ListView _lv = (sender as System.Windows.Forms.ListView);
			ListView lv = (Control as ListView);

			Console.WriteLine("selected rows: {0}", lv.SelectedRows.Count);
			InvokeMethod(lv, "OnSelectionChanged", new object[] { e });
		}


		void tv_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			((sender as System.Windows.Forms.TreeView).Tag as ListView).OnSelectionChanged(e);
		}


		void tv_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			/*
			ListViewSelectionChangingEventArgs ee = new ListViewSelectionChangingEventArgs();
			((sender as System.Windows.Forms.TreeView).Tag as ListView).OnSelectionChanging(ee);
			e.Cancel = ee.Cancel;
			*/		
		}


		private void lv_ItemActivate(object sender, EventArgs e)
		{
			System.Windows.Forms.ListView handle = (sender as System.Windows.Forms.ListView);
			ListView lv = (handle.Tag as ListView);

			if (handle.SelectedItems.Count > 0)
			{
				lv.OnRowActivated(new ListViewRowActivatedEventArgs(handle.SelectedItems[0].Tag as TreeModelRow));
			}
		}
		private void tv_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			System.Windows.Forms.TreeView tv = (sender as System.Windows.Forms.TreeView);
			ListView lv = (tv.Tag as ListView);
			TreeModelRow row = (e.Node.Tag as TreeModelRow);
			lv.OnRowActivated(new ListViewRowActivatedEventArgs(row));
		}


		private void tv_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			ListView lv = (Control as ListView);
			if (lv.Model == null)
				return;

			TreeModelRow row = (e.Node.Tag as TreeModelRow);
			TreeModelRowColumn rc = row.RowColumns[0];
			if (row != null)
			{
				TreeModelRowColumnEditingEventArgs ee = new TreeModelRowColumnEditingEventArgs(row, rc, rc.Value, e.Label);
				OnRowColumnEditing(ee);
				if (!ee.Cancel)
				{
					rc.Value = e.Label;
					OnRowColumnEdited(new TreeModelRowColumnEditedEventArgs(row, rc, rc.Value, e.Label));
				}
			}
		}


		private static void SelectedRows_ItemRequested(object sender, TreeModelRowItemRequestedEventArgs e)
		{
			TreeModelRow.TreeModelSelectedRowCollection coll = (sender as TreeModelRow.TreeModelSelectedRowCollection);
			ControlImplementation impl = coll.Parent.ControlImplementation;


			if (coll.Parent != null)
			{
				switch (ImplementedAs(coll.Parent))
				{
					case ImplementedAsType.ListView:
					{
						System.Windows.Forms.ListView lv = ((coll.Parent.ControlImplementation?.Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.ListView);
						
						if (e.Index == -1 && e.Count == 1 && e.Item != null)
						{
							// we are adding a new row to the selected collection
							// GetListViewItem(e.Item)
							// lv.Items[index].Selected = true;
							return;
						}
						else
						{
							e.Count = lv.SelectedItems.Count;
							if (e.Count > 0 && e.Index > -1)
							{
								TreeModelRow row = (lv.SelectedItems[e.Index].Tag as TreeModelRow);
								e.Item = row;
							}
							else if (e.Count > 0 && e.Index == -1 && e.Item != null)
							{
								// we are checking if selection contains a row
								bool found = false;
								for (int i = 0; i < lv.SelectedItems.Count; i++)
								{
									TreeModelRow rowtest = (lv.SelectedItems[i].Tag as TreeModelRow);
									if (rowtest == e.Item)
									{
										found = true;
										break;
									}
								}
								if (!found) e.Item = null;
							}
							else
							{
								e.Item = null;
							}
						}
						break;
					}
					case ImplementedAsType.TreeView:
					{
						System.Windows.Forms.TreeView tv = ((coll.Parent.ControlImplementation?.Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.TreeView);

						if (e.Index == -1 && e.Count == 1 && e.Item != null)
						{
							// we are adding a new row to the selected collection
							// GetListViewItem(e.Item)
							// lv.Items[index].Selected = true;
							return;
						}
						else if (tv.SelectedNode != null)
						{
							e.Count = 1;
							if (e.Count > 0 && e.Index > -1)
							{
								TreeModelRow row = (tv.SelectedNode.Tag as TreeModelRow);
								e.Item = row;
							}
							else if (e.Count > 0 && e.Index == -1 && e.Item != null)
							{
								// we are checking if selection contains a row
								if (tv.SelectedNode.Tag != e.Item)
								{
									e.Item = null;
								}
							}
							else
							{
								e.Item = null;
							}
						}
						break;
					}
				}
			}
		}
		private static void SelectedRows_Cleared(object sender, EventArgs e)
		{
			TreeModelRow.TreeModelSelectedRowCollection coll = (sender as TreeModelRow.TreeModelSelectedRowCollection);
			if (coll.Parent != null)
			{
				ImplementedAsType implementedAs = ImplementedAs(coll.Parent);
				if (implementedAs == ImplementedAsType.TreeView)
				{
					System.Windows.Forms.TreeView tv = ((coll.Parent.ControlImplementation?.Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.TreeView);
					if (tv != null)
						tv.SelectedNode = null;
				}
				else if (implementedAs == ImplementedAsType.ListView)
				{
					System.Windows.Forms.ListView lv = ((coll.Parent.ControlImplementation?.Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.ListView);
					if (lv != null)
						lv.SelectedItems.Clear();
				}
			}
		}

		protected override void OnKeyDown (KeyEventArgs e)
		{
			base.OnKeyDown (e);

			ListView lv = Control as ListView;
			if (lv == null) return;

			if (lv.SelectedRows.Count != 1) return;

			if (e.Key == KeyboardKey.ArrowRight) {
				lv.SelectedRows [0].Expanded = true;
				e.Cancel = true;
			}
			else if (e.Key == KeyboardKey.ArrowLeft) {
				if (!lv.SelectedRows [0].Expanded) {
					// we're already closed, so move selection up to our parent
					TreeModelRow rowCurrent = lv.SelectedRows [0];
					if (rowCurrent.ParentRow != null) {
						lv.SelectedRows.Clear ();
						lv.SelectedRows.Add (rowCurrent.ParentRow);
					}
				} else {
					lv.SelectedRows [0].Expanded = false;
				}
				e.Cancel = true;
			}
		}

		private Dictionary<TreeModel, List<System.Windows.Forms.Control>> TreeModelAssociatedControls = new Dictionary<TreeModel, List<System.Windows.Forms.Control>>();

		private System.Windows.Forms.ListViewItem TreeModelRowToListViewItem(TreeModelRow row)
		{
			System.Windows.Forms.ListViewItem tn = new System.Windows.Forms.ListViewItem();
			if (row.RowColumns.Count > 0)
			{
				tn.Text = row.RowColumns[0].Value?.ToString();
			}
			for (int i = 1; i < row.RowColumns.Count; i++)
			{
				tn.SubItems.Add(row.RowColumns[i].Value?.ToString());
			}
			tn.Tag = row;
			return tn;
		}

		private System.Windows.Forms.TreeNode TreeModelRowToTreeNode(TreeModelRow row)
		{
			System.Windows.Forms.TreeNode tn = new System.Windows.Forms.TreeNode();
			if (row.RowColumns.Count > 0)
			{
				tn.Text = row.RowColumns[0].Value?.ToString();
			}
			tn.Tag = row;

			foreach (TreeModelRow row2 in row.Rows)
			{
				tn.Nodes.Add(TreeModelRowToTreeNode(row2));
			}
			return tn;
		}

		public void UpdateTreeModel(TreeModel tm, TreeModelChangedEventArgs e)
		{
			if (!TreeModelAssociatedControls.ContainsKey(tm))
				return;

			List<System.Windows.Forms.Control> list = TreeModelAssociatedControls[tm];
			switch (e.Action)
			{
				case TreeModelChangedAction.Add:
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] is System.Windows.Forms.TreeView)
						{
							System.Windows.Forms.TreeView tv = (list[i] as System.Windows.Forms.TreeView);
							for (int j = 0; j < e.Rows.Count; j++)
							{
								tv.Nodes.Add(TreeModelRowToTreeNode(e.Rows[j]));
							}
						}
						else if (list[i] is System.Windows.Forms.ListView)
						{
							System.Windows.Forms.ListView lv = (list[i] as System.Windows.Forms.ListView);
							for (int j = 0; j < e.Rows.Count; j++)
							{
								lv.Items.Add(TreeModelRowToListViewItem(e.Rows[j]));
							}
						}
					}

					break;
				}
				case TreeModelChangedAction.Remove:
				{
					foreach (TreeModelRow row in e.Rows)
					{
						
						// Internal.GTK.Structures.GtkTreeIter iter = (Engine as GTKEngine).GetGtkTreeIterForTreeModelRow(row);
						// Internal.GTK.Methods.GtkTreeStore.gtk_tree_store_remove(hTreeModel, ref iter);
						// (Engine as GTKEngine).UnregisterGtkTreeIter(iter);
					}
					break;
				}
				case TreeModelChangedAction.Clear:
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] is System.Windows.Forms.TreeView)
						{
							(list[i] as System.Windows.Forms.TreeView).Nodes.Clear();
						}
						else if (list[i] is System.Windows.Forms.ListView)
						{
							(list[i] as System.Windows.Forms.ListView).Items.Clear();
						}
					}
					break;
				}
			}
		}

		public void UpdateTreeModel(NativeControl handle, TreeModelChangedEventArgs e)
		{
			System.Windows.Forms.Control hctrl = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Control);
			if (hctrl == null) return;

			if (hctrl is System.Windows.Forms.TreeView)
			{

			}
			else if (hctrl is System.Windows.Forms.ListView)
			{
			}

			UpdateTreeModel((Control as ListView).Model, e);
		}


		public bool IsRowExpanded(TreeModelRow row)
		{
			ListView lv = Control as ListView;
			if (lv == null)
				return false;

			if ((Handle as WindowsFormsNativeControl).Handle is System.Windows.Forms.TreeView)
			{
				if (_NodesForRow.ContainsKey(row))
				{
					return _NodesForRow[row].IsExpanded;
				}
			}
			return false;
		}

		private Dictionary<TreeModelRow, System.Windows.Forms.TreeNode> _NodesForRow = new Dictionary<TreeModelRow, System.Windows.Forms.TreeNode>();
		public void SetRowExpanded(TreeModelRow row, bool expanded)
		{
			ListView lv = Control as ListView;
			if (lv == null)
				return;

			if ((Handle as WindowsFormsNativeControl).Handle is System.Windows.Forms.TreeView)
			{
				if (_NodesForRow.ContainsKey(row))
				{
					if (expanded)
					{
						_NodesForRow[row].Expand();
					}
					else
					{
						_NodesForRow[row].Collapse();
					}
				}
			}
		}

		private void RecursiveTreeStoreInsertRow(TreeModel tm, TreeModelRow row, System.Windows.Forms.TreeView parentView, System.Windows.Forms.TreeNode parentNode, int position = -1)
		{
			System.Windows.Forms.TreeNode tn = TreeModelRowToTreeNode(row);
			if (parentNode == null)
			{
				if (position == -1)
				{
					parentView.Nodes.Add(tn);
				}
				else
				{
					parentView.Nodes.Insert(position, tn);
				}
			}
			else
			{
				if (position == -1)
				{
					parentNode.Nodes.Add(tn);
				}
				else
				{
					parentNode.Nodes.Insert(position, tn);
				}
			}
		}
	}
}
