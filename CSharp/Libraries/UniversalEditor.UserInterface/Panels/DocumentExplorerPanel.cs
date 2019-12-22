using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class DocumentExplorerPanel : Panel
	{
		private ListView lv = null;
		private DefaultTreeModel tm = null;

		public DocumentExplorerPanel()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			lv = new ListView();

			tm = new DefaultTreeModel(new Type[] { typeof(string) });
			lv.Model = tm;
			lv.HeaderStyle = ColumnHeaderStyle.None;
			lv.Columns.Add(new ListViewColumnText(tm.Columns[0], "Item"));

			Controls.Add(lv, new BoxLayout.Constraints(true, true));
		}
	}
}
