using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.StoryWriter.Story;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors
{
	public partial class StoryEditor : Editor
	{
		public StoryEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			StoryObjectModel story = (base.ObjectModel as StoryObjectModel);

			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();

			TreeNode tnUniverse = tv.Nodes.Add("tnUniverse", "Universe", "generic-folder-closed");
			
			TreeNode tnCharacters = tnUniverse.Nodes.Add("tnCharacters", "Characters", "generic-folder-closed");
			foreach (Character chara in story.Characters)
			{
				TreeNode tn = new TreeNode();
				tn.Text = chara.Name;
				tn.ImageKey = "character";
				tnCharacters.Nodes.Add(tn);
			}

			tnUniverse.Nodes.Add("tnDevices", "Devices", "generic-folder-closed");
			tnUniverse.Nodes.Add("tnLocations", "Locations", "generic-folder-closed");
			tnUniverse.Nodes.Add("tnOrganizations", "Organizations", "generic-folder-closed");
			tnUniverse.Nodes.Add("tnVehicles", "Vehicles", "generic-folder-closed");

			tv.Nodes.Add("tnBooks", "Books", "generic-folder-closed");

			tv.ExpandAll();
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}
	}
}
