//
//  IcarusScriptEditor.Designer.cs - UWT designer initialization for IcarusScriptEditor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Icarus
{
	partial class IcarusScriptEditor
	{
		/// <summary>
		/// UWT designer initialization for <see cref="IcarusScriptEditor" />.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			this.tm = new DefaultTreeModel(new Type[] { typeof(string) });

			this.tv = new ListViewControl();
			this.tv.ContextMenuCommandID = "Icarus_ContextMenu";
			this.tv.Model = this.tm;
			this.tv.Columns.Add(new ListViewColumnText(this.tm.Columns[0], "Command"));
			this.tv.RowActivated += new ListViewRowActivatedEventHandler(this.tv_RowActivated);
			this.Controls.Add(this.tv, new BoxLayout.Constraints(true, true));
		}

		private DefaultTreeModel tm;
		private ListViewControl tv;
	}
}
