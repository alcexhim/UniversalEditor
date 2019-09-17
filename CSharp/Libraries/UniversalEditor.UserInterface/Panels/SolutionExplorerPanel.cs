//
//  SolutionExplorerPanel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class SolutionExplorerPanel : Panel
	{
		private DefaultTreeModel tmSolutionExplorer = null;
		private ListView tvSolutionExplorer = new ListView();

		public SolutionExplorerPanel()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			tmSolutionExplorer = new DefaultTreeModel(new Type[] { typeof(string) });
			tvSolutionExplorer.Model = tmSolutionExplorer;

			tvSolutionExplorer.Columns.Add(new ListViewColumnText(tmSolutionExplorer.Columns[0], "File Name"));

			this.Controls.Add(tvSolutionExplorer, new BoxLayout.Constraints(true, true));
		}
	}
}
