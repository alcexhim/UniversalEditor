using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class DocumentExplorerPanel : Panel
	{
		public static readonly Guid ID = new Guid("{5410f224-d594-4b6c-b31d-ac70e09b6a00}");

		private ListViewControl lv = null;
		public ListViewControl ListView { get { return lv; } }

		private DefaultTreeModel tm = null;

		public DocumentExplorerPanel()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			lv = new ListViewControl();
			lv.BeforeContextMenu += lv_BeforeContextMenu;
			lv.SelectionChanged += lv_SelectionChanged;

			tm = new DefaultTreeModel(new Type[] { typeof(string), typeof(MBS.Framework.UserInterface.Drawing.Image) });
			lv.Model = tm;
			lv.HeaderStyle = ColumnHeaderStyle.None;
			lv.Columns.Add(new ListViewColumn("Item", new CellRenderer[] { new CellRendererImage(tm.Columns[1]), new CellRendererText(tm.Columns[0]) }));

			Controls.Add(lv, new BoxLayout.Constraints(true, true));
		}

		protected override void OnEditorChanged(EditorChangedEventArgs e)
		{
			base.OnEditorChanged(e);
			CurrentEditor = e.CurrentEditor;
		}

		private void lv_BeforeContextMenu(object sender, EventArgs e)
		{
			if (lv.SelectedRows.Count == 0)
				return;

			EditorDocumentExplorerNode node = lv.SelectedRows[0].GetExtraData<EditorDocumentExplorerNode>("node");
			EditorDocumentExplorerBeforeContextMenuEventArgs ee = new EditorDocumentExplorerBeforeContextMenuEventArgs(node);

			CurrentEditor.DocumentExplorer.FireBeforeContextMenu(ee);

			lv.ContextMenuCommandID = ee.ContextMenuCommandID;
		}

		private void lv_SelectionChanged(object sender, EventArgs e)
		{
			if (lv.SelectedRows.Count == 0)
				return;

			if (CurrentEditor == null)
				return;

			EditorDocumentExplorerNode node = lv.SelectedRows[0].GetExtraData<EditorDocumentExplorerNode>("node");
			CurrentEditor.OnDocumentExplorerSelectionChanged(new EditorDocumentExplorerSelectionChangedEventArgs(node));
		}


		private Editor _CurrentEditor = null;
		public Editor CurrentEditor { get { return _CurrentEditor; } set { _CurrentEditor = value; RefreshDocumentExplorer(); } }

		private void InitDocumentExplorerRow(EditorDocumentExplorerNode node, TreeModelRow parent = null)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], node.Text),
				new TreeModelRowColumn(tm.Columns[1], MBS.Framework.UserInterface.Drawing.Image.FromStock(node.StockType, 16))
			});
			row.SetExtraData<EditorDocumentExplorerNode>("node", node);
			for (int i = 0; i < node.Nodes.Count; i++)
			{
				InitDocumentExplorerRow(node.Nodes[i], row);
			}
			if (parent == null)
			{
				tm.Rows.Add(row);
			}
			else
			{
				parent.Rows.Add(row);
			}
		}

		private void RefreshDocumentExplorer()
		{
			tm.Rows.Clear();

			if (CurrentEditor == null)
				return;

			TreeModelRow rowDocument = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], CurrentEditor.Title)
			});
			for (int i = 0; i < CurrentEditor.DocumentExplorer.Nodes.Count; i++)
			{
				InitDocumentExplorerRow(CurrentEditor.DocumentExplorer.Nodes[i], rowDocument);
			}
			tm.Rows.Add(rowDocument);
		}
	}
}
