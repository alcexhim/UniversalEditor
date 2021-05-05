using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UniversalEditor.ObjectModels.LanguageTranslation;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.RavenSoftware.Strip
{
	public partial class StripEditor : Editor
	{
		public StripEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(LanguageTranslationObjectModel));
			Font = SystemFonts.MenuFont;

			ActionMenuItem mnuTools = (base.MenuBar.Items.Add("mnuTools", "&Tools") as ActionMenuItem);
			mnuTools.Items.Add("mnuToolsCheckMissingTranslationSets", "Check for missing translation sets", mnuToolsCheckMissingTranslationSets_Click, 3);
			mnuTools.Items.AddSeparator(3);

			ActionMenuItem mnuHelp = (base.MenuBar.Items.Add("mnuHelp", "&Help") as ActionMenuItem);
			mnuHelp.Items.Add("mnuHelpAboutStripper", "About RavenTech &Stripper", mnuHelpAboutStripper_Click, -1);
		}

		private void mnuToolsCheckMissingTranslationSets_Click(object sender, EventArgs e)
		{
			MessageBox.Show("There are no missing translation sets in the translation file. (Good job, this means your task as a translator is complete!)", "RavenTech Stripper", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		private void mnuHelpAboutStripper_Click(object sender, EventArgs e)
		{
			MessageBox.Show("RavenTech Stripper (Translation Strip Editor)\r\nCopyright (c)2013 Mike Becker's Software\r\nLicensed under the GNU General Public License\r\n\r\nDesigned for use with the RavenTech game engine and Raven Software's customized Jedi Knight II/Jedi Academy engine.", "RavenTech Stripper", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			lv.Items.Clear();

			LanguageTranslationObjectModel xlate = (ObjectModel as LanguageTranslationObjectModel);
			if (xlate == null) return;

			Color color = Color.Gainsboro;

			foreach (Group group in xlate.Groups)
			{
				cboGroup.Items.Add(group);

				foreach (Entry entry in group.Entries)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.BackColor = color;
					lvi.Tag = entry;
					lvi.Text = group.Title;
					lvi.SubItems.Add(entry.Language);
					lvi.SubItems.Add(entry.Value);

					if (!cboLanguage.Items.Contains(entry.Language))
					{
						cboLanguage.Items.Add(entry.Language);
					}
					lv.Items.Add(lvi);
				}

				if (color == Color.Gainsboro)
				{
					color = Color.White;
				}
				else
				{
					color = Color.Gainsboro;
				}
			}
			lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboGroup.Text = String.Empty;
			cboLanguage.Text = String.Empty;
			txtValue.Text = String.Empty;

			if (lv.SelectedItems.Count == 1)
			{
				fraEditor.Enabled = true;
				cmdUpdate.Text = "Upd&ate";

				Entry entry = (lv.SelectedItems[0].Tag as Entry);
				cboGroup.Text = entry.Parent.Title;
				cboLanguage.Text = entry.Language;
				txtValue.Text = entry.Value;
			}
			else if (lv.SelectedItems.Count > 1)
			{
				fraEditor.Enabled = false;
			}
			else
			{
				fraEditor.Enabled = true;
				cmdUpdate.Text = "&Add";
			}
		}

		private void cmdUpdate_Click(object sender, EventArgs e)
		{
			Entry entry = (lv.SelectedItems[0].Tag as Entry);
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{

		}
	}
}
