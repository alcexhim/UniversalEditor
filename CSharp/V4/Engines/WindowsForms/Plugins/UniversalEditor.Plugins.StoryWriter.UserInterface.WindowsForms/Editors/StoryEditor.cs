using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.StoryWriter.Story;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors
{
	public partial class StoryEditor : Editor
	{
		public StoryEditor()
		{
			InitializeComponent();
			IconMethods.PopulateSystemIcons(ref imlSmallIcons);
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Story";
				_er.SupportedObjectModels.Add(typeof(StoryObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			StoryObjectModel story = (base.ObjectModel as StoryObjectModel);

			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();

			#region Universe
			{
				TreeNode tnUniverse = tv.Nodes.Add("tnUniverse", "Universe", "generic-folder-closed");
			
				TreeNode tnCharacters = tnUniverse.Nodes.Add("tnCharacters", "Characters", "generic-folder-closed");
				foreach (Character chara in story.Characters)
				{
					TreeNode tn = new TreeNode();
					tn.Text = chara.Name.ToString();
					tn.ImageKey = "character";
					tnCharacters.Nodes.Add(tn);
				}

				tnUniverse.Nodes.Add("tnDevices", "Devices", "generic-folder-closed");
				tnUniverse.Nodes.Add("tnLocations", "Locations", "generic-folder-closed");
				tnUniverse.Nodes.Add("tnOrganizations", "Organizations", "generic-folder-closed");
				tnUniverse.Nodes.Add("tnVehicles", "Vehicles", "generic-folder-closed");
			}
			#endregion
			#region Books
			{
				tv.Nodes.Add("tnBooks", "Books", "generic-folder-closed");
			}
			#endregion

			tv.ExpandAll();
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private void mnuContextTreeView_Opening(object sender, CancelEventArgs e)
		{
			if (tv.SelectedNode.Tag is Chapter)
			{

			}
		}

		private void tv_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TreeNode tn = tv.HitTest(e.Location).Node;
				if (tn != null) tv.SelectedNode = tn;
			}
		}
	}
}
