// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Multimedia.Audio.Voicebank
{
	// TODO: Create a file-format-independent explorer listview.
	
	// The VoiceDatabaseEditor uses an Explorer listview interface. This means that
	// PhonemeGroups are represented as folders, and Phonemes within those PhonemeGroups
	// are represented as files. This makes it easy for a user to add/delete these kinds
	// of items without having to learn complex interfaces.
	
	// Where practical, I'd like to implement this interface in editors. If there is more
	// than one editor that can be used to edit a particular type of file (for example,
	// VoiceDatabaseProgrammerEditor and VoiceDatabaseExplorerEditor), the different
	// editors will be listed when GetAvailableEditors (...) is called.
	
	// For our test case (EDSEdit), these editors will be listed as tabs near the bottom
	// of the screen. The upside to this model of course is that there will be a common
	// editor for these types of files.
	
	// The ExplorerEditorBase will expose a Folders collection and a Files collection.
	// The ExplorerEditorBase will also have a built-in search box (which can be hidden
	// if developing an app that has its own search methods) and a built-in search
	// function Search(string text) (which can be used when developing aformentioned app)
	
	// When user clicks on a folder (or a file), the ExplorerView will fire an ItemClick
	// event (or for double-clicks, an ItemDoubleClick event) that the author of the Editor
	// will handle by overriding the OnItemClick (or OnItemDoubleClick) event. The item
	// that had been clicked will automatically go into the navigation stack (so a Back/Up
	// button can be implemented without too much difficulty) and the author will be
	// responsible for populating the next set of items in the hierarchy.
	
	// Of course, the entire idea behind this is that EDS will become a drop-in replacement
	// for any explorer on any system, and will be able to read and edit any files without
	// difficulty.
	
	/// <summary>
	/// Description of VoiceDatabaseEditor.
	/// </summary>
	public partial class VoicebankEditor : Editor
	{
		public VoicebankEditor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			base.SupportedObjectModels.Add(typeof(VoicebankObjectModel));
			
			// Select "Explorer" view by default, it looks nice~
			tabControl1.SelectedIndex = 1;
		}

		/*
		public override bool NavigateImaginaryPath(string path)
		{
			if (base.ObjectModel is VoicebankObjectModel)
			{
				VoicebankObjectModel ddi = (base.ObjectModel as VoicebankObjectModel);
				PhonemeGroup pg = ddi.PhonemeGroups[path];
				if (pg != null)
				{
					lvGroups.Items[ddi.PhonemeGroups.IndexOf(pg)].Selected = true;
				}
				else
				{
					MessageBox.Show("Phoneme group " + path + " does not exist in this file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return false;
		}
		*/


		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			
			if (base.ObjectModel is VoicebankObjectModel)
			{
				VoicebankObjectModel ddi = (base.ObjectModel as VoicebankObjectModel);
				lvGroups.Items.Clear();
				lvPhonemes.Items.Clear();
				
				foreach(PhonemeGroup pg in ddi.PhonemeGroups)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = pg.Title;
					lvi.SubItems.Add(pg.Phonemes.Count.ToString());
					// lvi.SubItems.Add(pg.Data1.ToString());
					// lvi.SubItems.Add(pg.Data2.ToString());
					lvGroups.Items.Add(lvi);
					
					ListViewItem lvie = new ListViewItem();
					lvie.Text = pg.Title;
					lvie.SubItems.Add(pg.Phonemes.Count.ToString());
					// lvie.SubItems.Add(pg.Data1.ToString());
					// lvie.SubItems.Add(pg.Data2.ToString());
					lvie.ImageKey = "folder_closed";
					lvExplorer.Items.Add(lvie);
				}
			}
		}
		
		void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (base.ObjectModel is VoicebankObjectModel)
			{
				VoicebankObjectModel ddi = (base.ObjectModel as VoicebankObjectModel);
				lvPhonemes.Items.Clear();
				if (lvGroups.SelectedIndices.Count == 1)
				{
					foreach (Phoneme p in ddi.PhonemeGroups[lvGroups.SelectedIndices[0]].Phonemes)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = p.Title;
						// lvi.SubItems.Add(p.Data.ToString());
						lvPhonemes.Items.Add(lvi);
					}
				}
			}
		}
	}
}
