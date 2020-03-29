using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Icarus
{
	partial class IcarusScriptEditor
	{
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			this.tm = new DefaultTreeModel(new Type[] { typeof(string) });

			this.tv = new ListView();
			this.tv.ContextMenuCommandID = "Icarus_ContextMenu";
			this.tv.Model = this.tm;
			this.tv.Columns.Add(new ListViewColumnText(this.tm.Columns[0], "Command"));
			this.tv.RowActivated += new ListViewRowActivatedEventHandler(this.tv_RowActivated);
			this.Controls.Add(this.tv, new BoxLayout.Constraints(true, true));
		}

		private DefaultTreeModel tm;
		private ListView tv;
	}
}
