using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class DocumentExplorerPanel : Panel
	{
		private ListViewControl lv = null;
		public ListViewControl ListView { get { return lv; } }

		private DefaultTreeModel tm = null;

		public DocumentExplorerPanel()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			lv = new ListViewControl();
			lv.BeforeContextMenu += lv_BeforeContextMenu;
			lv.SelectionChanged += lv_SelectionChanged;

			tm = new DefaultTreeModel(new Type[] { typeof(string) });
			lv.Model = tm;
			lv.HeaderStyle = ColumnHeaderStyle.None;
			lv.Columns.Add(new ListViewColumnText(tm.Columns[0], "Item"));

			Controls.Add(lv, new BoxLayout.Constraints(true, true));
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
				new TreeModelRowColumn(tm.Columns[0], node.Text)
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
