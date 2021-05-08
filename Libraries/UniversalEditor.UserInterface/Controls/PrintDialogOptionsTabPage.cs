//
//  PrintDialogOptionsTab.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using UniversalEditor.Printing;

namespace UniversalEditor.UserInterface.Controls
{
	[ContainerLayout(typeof(PrintDialogOptionsTabPage), "UniversalEditor.UserInterface.Controls.PrintDialogOptionsTabPage.glade")]
	public class PrintDialogOptionsTabPage : TabPage
	{
		private GroupBox fraCustomOptions;
		private ComboBox cboPrintHandler;

		public PrintHandlerReference[] PrintHandlers { get; set; } = null;
		public PrintHandlerReference SelectedPrintHandler { get; set; } = null;

		public PrintDialogOptionsTabPage()
		{
			Text = "Options";
		}

		[EventHandler(nameof(cboPrintHandler), "Changed")]
		private void cboPrintHandler_Changed(object sender, EventArgs e)
		{
			fraCustomOptions.Controls.Clear();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			if (PrintHandlers != null)
			{
				for (int i = 0; i < PrintHandlers.Length; i++)
				{
					(cboPrintHandler.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(cboPrintHandler.Model.Columns[0], PrintHandlers[i].TypeName)
					}));
				}

				if (SelectedPrintHandler != null)
				{
					cboPrintHandler.SelectedItem = (cboPrintHandler.Model as DefaultTreeModel).Rows[Array.IndexOf<PrintHandlerReference>(PrintHandlers, SelectedPrintHandler)];
				}
			}
		}
	}
}
