using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.KnowledgeAdventure.Actor;

namespace UniversalEditor.Plugins.KnowledgeAdventure.UserInterface.WindowsForms.Editors.KnowledgeAdventure.Actor
{
	public partial class ActorEditor : Editor
	{
		public ActorEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ActorObjectModel));
			}
			return _er;
		}

		private void cmdBrowseImageFileName_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			if (ObjectModel.Accessor != null)
			{
				string fileName = ObjectModel.Accessor.GetFileName();
				if (System.IO.File.Exists(fileName))
				{
					ofd.InitialDirectory = System.IO.Path.GetDirectoryName(fileName);
				}
			}

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				txtImageFileName.Text = System.IO.Path.GetFileName(ofd.FileName);
			}
		}
	}
}
