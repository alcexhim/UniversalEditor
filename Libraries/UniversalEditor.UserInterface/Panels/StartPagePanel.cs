﻿//
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
	[ContainerLayout("~/Panels/StartPage.glade", "GtkWindow")]
	public class StartPagePanel : Panel
	{
		private Button cmdCreateNewProject;
		private Button cmdOpenExistingProject;
		private Container ctHeaderImage;
		private Container ctHeaderText;
		private PictureFrame imgHeader;
		private Label lblHeader;
		private Label lblNewsTitle;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			cmdCreateNewProject.Click += cmdCreateNewProject_Click;
			cmdOpenExistingProject.Click += cmdOpenExistingProject_Click;
			lblHeader.Text = String.Format(lblHeader.Text, Application.Title);
			lblNewsTitle.Text = String.Format(lblNewsTitle.Text, Application.Title);

			string header_bmp = Application.ExpandRelativePath("~/header.bmp");
			if (System.IO.File.Exists(header_bmp))
			{
				imgHeader.Image = MBS.Framework.UserInterface.Drawing.Image.FromFile(header_bmp);
				ctHeaderImage.Visible = true;
				ctHeaderText.Visible = false;
			}
			else
			{
				ctHeaderImage.Visible = false;
				ctHeaderText.Visible = true;
			}
		}

		private void cmdCreateNewProject_Click(object sender, EventArgs e)
		{
			HostApplication.CurrentWindow?.NewProject();
		}
		private void cmdOpenExistingProject_Click(object sender, EventArgs e)
		{
			HostApplication.CurrentWindow?.OpenProject();
		}
	}
}