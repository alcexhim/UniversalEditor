//
//  ErrorListPanel.cs
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
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class ErrorListPanel : Panel
	{
		private Toolbar tbErrorList = new Toolbar();
		private ListViewControl tvErrorList = new ListViewControl();

		private DefaultTreeModel tm = new DefaultTreeModel(new Type[] { typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) });
		   
		public ErrorListPanel()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			ToolbarItemButton tsbErrors = new ToolbarItemButton("tsbErrors", "Errors");
			tsbErrors.CheckOnClick = true;
			ToolbarItemButton tsbWarnings = new ToolbarItemButton("tsbWarnings", "Warnings");
			tsbWarnings.CheckOnClick = true;
			ToolbarItemButton tsbMessages = new ToolbarItemButton("tsbMessages", "Messages");
			tsbMessages.CheckOnClick = true;
			
			tbErrorList.Items.Add(tsbErrors);
			tbErrorList.Items.Add(tsbWarnings);
			tbErrorList.Items.Add(tsbMessages);

			this.Controls.Add(tbErrorList);

			tvErrorList.Model = tm;

			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[0], "Line"));
			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[1], "Description"));
			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[2], "File"));
			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[3], "Project"));
			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[4], "Path"));
			tvErrorList.Columns.Add(new ListViewColumnText(tm.Columns[5], "Category"));

			HostApplication.Messages.MessageAdded += (sender, e) =>
			{
				HostApplicationMessage message = e.Message;
				tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], message.LineNumber),
					new TreeModelRowColumn(tm.Columns[1], message.Description),
					new TreeModelRowColumn(tm.Columns[2], System.IO.Path.GetFileName(message.FileName)),
					new TreeModelRowColumn(tm.Columns[3], message.ProjectName),
					new TreeModelRowColumn(tm.Columns[4], message.FileName),
					new TreeModelRowColumn(tm.Columns[5], message.Severity)
				}));
			};
			HostApplication.Messages.MessageRemoved += (sender, e) =>
			{

			};

			// RefreshList();

			this.Controls.Add(tvErrorList, new BoxLayout.Constraints(true, true));
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			RefreshList();
		}

		public void RefreshList()
		{
			tm.Rows.Clear();

			foreach (HostApplicationMessage message in HostApplication.Messages)
			{
				tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], message.LineNumber),
					new TreeModelRowColumn(tm.Columns[1], message.Description),
					new TreeModelRowColumn(tm.Columns[2], System.IO.Path.GetFileName(message.FileName)),
					new TreeModelRowColumn(tm.Columns[3], message.ProjectName),
					new TreeModelRowColumn(tm.Columns[4], message.FileName),
					new TreeModelRowColumn(tm.Columns[5], message.Severity)
				}));
			}
		}
	}
}
