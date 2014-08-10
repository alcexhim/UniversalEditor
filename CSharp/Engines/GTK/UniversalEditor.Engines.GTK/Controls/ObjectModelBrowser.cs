//  
//  ObjectModelBrowser.cs
//  
//  Author:
//       beckermj <${AuthorEmail}>
// 
//  Copyright (c) 2014 beckermj
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
namespace UniversalEditor.Engines.GTK
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ObjectModelBrowser : Gtk.Bin
	{
		public ObjectModelBrowser()
		{
			this.Build();
			
			this.txtSearch.Changed += txtSearch_Changed;
			
			this.tv.AppendColumn("Title", new Gtk.CellRendererText(), "text", 1);
			this.tv.AppendColumn("Description", new Gtk.CellRendererText(), "text", 2);
			
			UpdateTreeView();
		}

		private void txtSearch_Changed(object sender, EventArgs e)
		{
			UpdateTreeView();
		}
		private void UpdateTreeView()
		{
			ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
			Gtk.TreeStore ts = new Gtk.TreeStore(typeof(ObjectModel), typeof(string), typeof(string));
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr == null) continue;
				bool found = false;
				if (!String.IsNullOrEmpty(txtSearch.Text))
				{
					if (omr.Title != null)
					{
						if (omr.Title.ToLower().Contains(txtSearch.Text.ToLower())) found = true;
					}
					if (omr.Description != null)
					{
						if (omr.Description.ToLower().Contains(txtSearch.Text.ToLower())) found = true;
					}
				}
				else
				{
					found = true;
				}
				if (found)
				{
					ts.AppendValues(omr, omr.Title, omr.Description);
				}
			}
			tv.Model = ts;
		}
	}
}

