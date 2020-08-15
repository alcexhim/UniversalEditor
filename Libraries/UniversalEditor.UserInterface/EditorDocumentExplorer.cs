//
//  EditorDocumentExplorer.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;

namespace UniversalEditor.UserInterface
{
	public class EditorDocumentExplorer
	{
		public Editor Parent { get; private set; } = null;
		public EditorDocumentExplorer(Editor parent)
		{
			Parent = parent;
			Nodes = new EditorDocumentExplorerNode.EditorDocumentExplorerNodeCollection();
		}

		public EditorDocumentExplorerNode.EditorDocumentExplorerNodeCollection Nodes { get; private set; } = null;
		public EditorDocumentExplorerNode SelectedNode
		{
			get
			{
				ListViewControl lv = ((MainWindow)HostApplication.CurrentWindow).DocumentExplorerPanel.ListView;
				if (lv.SelectedRows.Count > 0)
				{
					return lv.SelectedRows[0].GetExtraData<EditorDocumentExplorerNode>("node");
				}
				return null;
			}
		}

		public event EditorDocumentExplorerBeforeContextMenuEventHandler BeforeContextMenu;

		internal void FireBeforeContextMenu(EditorDocumentExplorerBeforeContextMenuEventArgs e)
		{
			BeforeContextMenu?.Invoke(this, e);
		}
	}
}
