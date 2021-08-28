using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;

using UniversalEditor.ObjectModels.TranslationSet;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Strip
{
	[ContainerLayout("~/Editors/RavenSoftware/Strip/StripEditor.glade")]
	public class StripEditor : Editor
	{
		private NumericTextBox txtID;
		private TextBox txtReference;
		private TextBox txtDescription;

		private Toolbar tbEntries;

		private ListViewControl tv;

		[EventHandler(nameof(txtID), nameof(TextBox.Changed))]
		private void txtID_Changed(object sender, EventArgs e)
		{
			BeginEdit();
			(ObjectModel as TranslationSetObjectModel).ID = Int32.Parse(txtID.Text);
			EndEdit();
		}
		[EventHandler(nameof(txtReference), nameof(TextBox.Changed))]
		private void txtReference_Changed(object sender, EventArgs e)
		{
			BeginEdit();
			(ObjectModel as TranslationSetObjectModel).Reference = txtReference.Text;
			EndEdit();
		}
		[EventHandler(nameof(txtDescription), nameof(TextBox.Changed))]
		private void txtDescription_Changed(object sender, EventArgs e)
		{
			BeginEdit();
			(ObjectModel as TranslationSetObjectModel).Description = txtDescription.Text;
			EndEdit();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Translation Set Editor";
				_er.SupportedObjectModels.Add(typeof(TranslationSetObjectModel));
			}
			return _er;
		}

		private void ToolsCheckMissingTranslationSets_Click(object sender, EventArgs e)
		{
			MessageDialog.ShowDialog("There are no missing translation sets in the translation file. (Good job, this means your task as a translator is complete!)", "RavenTech Stripper", MessageDialogButtons.OK, MessageDialogIcon.Information);
		}

		public StripEditor()
		{
			Context.AttachCommandEventHandler("ToolsCheckMissingTranslationSets", ToolsCheckMissingTranslationSets_Click);
		}

		private void mnuHelpAboutStripper_Click(object sender, EventArgs e)
		{
			MessageDialog.ShowDialog("RavenTech Stripper (Translation Strip Editor)\r\nCopyright (c)2013 Mike Becker's Software\r\nLicensed under the GNU General Public License\r\n\r\nDesigned for use with the RavenTech game engine and Raven Software's customized Jedi Knight II/Jedi Academy engine.", "RavenTech Stripper", MessageDialogButtons.OK, MessageDialogIcon.Information);
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated)
				return;

			tv.Model.Rows.Clear();

			TranslationSetObjectModel xlate = (ObjectModel as TranslationSetObjectModel);
			if (xlate == null) return;

			txtID.Value = xlate.ID;
			txtReference.Text = xlate.Reference;
			txtDescription.Text = xlate.Description;

			foreach (TranslationSetEntry entry in xlate.Entries)
			{
				// cboGroup.Items.Add(group);

				foreach (TranslationSetValue value in entry.Values)
				{
					TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tv.Model.Columns[0], entry.Reference),
						new TreeModelRowColumn(tv.Model.Columns[1], value.LanguageIndex),
						new TreeModelRowColumn(tv.Model.Columns[2], value.Value),
						// new TreeModelRowColumn(tv.Model.Columns[2], value.Flags.ToString()),
					});
					tv.Model.Rows.Add(row);
				}
			}
		}

		private void tv_SelectionChanged(object sender, EventArgs e)
		{
			/*
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
			*/
		}

		private void cmdUpdate_Click(object sender, EventArgs e)
		{
			// Entry entry = (lv.SelectedItems[0].Tag as Entry);
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{

		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}
