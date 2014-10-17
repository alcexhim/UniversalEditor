using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankIndex;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.Editors.Multimedia.Audio.Voicebank
{
	public partial class VoicebankIndexEditor : Editor
	{
		public VoicebankIndexEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(VoicebankIndexObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			lvPhonemes.Items.Clear();
			tvPhonemeGroups.Nodes.Clear();

			VoicebankIndexObjectModel dbse = (base.ObjectModel as VoicebankIndexObjectModel);
			if (dbse == null) return;

			foreach (Phoneme p in dbse.Phonemes)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = p.Title;
				lvi.Tag = p;
				lvPhonemes.Items.Add(lvi);
			}
			foreach (PhonemeGroup pg in dbse.Groups)
			{
				TreeNode tn = new TreeNode();
				tn.Text = pg.Title;
				tn.Tag = pg;

				foreach (Phoneme p in pg.Phonemes)
				{
					TreeNode tn1 = new TreeNode();
					tn1.Text = p.Title;
					tn1.Tag = p;
					tn.Nodes.Add(tn1);
				}

				tvPhonemeGroups.Nodes.Add(tn);
			}
		}
	}
}
