//
//  BootstrapScriptEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;
using MBS.Framework;
using MBS.Framework.Settings;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Setup.UserInterface
{
	[ContainerLayout("~/Editors/Setup/BootstrapScript/BootstrapScriptEditor.glade")]
	public class BootstrapScriptEditor : Editor
	{
		private ListViewControl tvPlatforms;
		private ListViewControl tvParameters;
		private ListViewControl tvFiles;

		private Toolbar tbPlatforms;
		private Toolbar tbParameters;
		private Toolbar tbFiles;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(BootstrapScriptObjectModel));
			}
			return _er;
		}

		[EventHandler(nameof(tvPlatforms), "SelectionChanged")]
		private void tvPlatforms_SelectionChanged(object sender, EventArgs e)
		{
			tvParameters.Model.Rows.Clear();
			tvFiles.Model.Rows.Clear();

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");

			foreach (KeyValuePair<string, string> kvp in os.Parameters)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvParameters.Model.Columns[0], kvp.Key),
					new TreeModelRowColumn(tvParameters.Model.Columns[1], kvp.Value)
				});
				row.SetExtraData<KeyValuePair<string, string>>("item", kvp);
				tvParameters.Model.Rows.Add(row);
			}
			foreach (BootstrapFile file in os.Files)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvFiles.Model.Columns[0], file.SourceFileName),
					new TreeModelRowColumn(tvFiles.Model.Columns[1], file.DestinationFileName)
				});
				row.SetExtraData<BootstrapFile>("item", file);
				tvFiles.Model.Rows.Add(row);
			}
		}

		[EventHandler(nameof(tvPlatforms), "RowActivated")]
		private void tvPlatforms_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row == null) return;
			tsbPlatformEdit_Click(sender, e);
		}

		private void tsbPlatformAdd_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			SettingsDialog dlg = new SettingsDialog(new SettingsProvider[]
			{
				new CustomSettingsProvider(new SettingsGroup[]
				{
					new SettingsGroup("Platform", new Setting[]
					{
						new TextSetting("Name", "Platform _name")
					})
				})
			});

			dlg.Text = "Platform Properties";
			dlg.EnableProfiles = false;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string platformName = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();

				BootstrapOperatingSystem os = new BootstrapOperatingSystem();
				os.Name = platformName;

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvPlatforms.Model.Columns[0], platformName)
				});
				row.SetExtraData<BootstrapOperatingSystem>("item", os);
				tvPlatforms.Model.Rows.Add(row);

				BeginEdit();
				setup.OperatingSystems.Add(os);
				EndEdit();
			}
		}

		private void tsbPlatformEdit_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			SettingsDialog dlg = new SettingsDialog(new SettingsProvider[]
			{
				new CustomSettingsProvider(new SettingsGroup[]
				{
					new SettingsGroup("Platform", new Setting[]
					{
						new TextSetting("Name", "Platform _name")
					})
				})
			});
			dlg.Text = "Platform Properties";
			dlg.EnableProfiles = false;

			dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].SetValue(os.Name);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string platformName = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();

				BeginEdit();
				os.Name = platformName;
				EndEdit();

				tvPlatforms.SelectedRows[0].RowColumns[0].Value = platformName;
			}
		}

		private void tsbPlatformRemove_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			if (MessageDialog.ShowDialog("Removing the selected platform will remove all associated parameters and files. Continue?", "Remove Platform", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) != DialogResult.Yes)
				return;

			BeginEdit();
			setup.OperatingSystems.Remove(os);
			EndEdit();

			tvPlatforms.Model.Rows.Remove(tvPlatforms.SelectedRows[0]);
		}

		[EventHandler(nameof(tvParameters), "RowActivated")]
		private void tvParameters_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row == null) return;
			tsbParameterEdit_Click(sender, e);
		}

		private void tsbParameterAdd_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			SettingsDialog dlg = new SettingsDialog(new SettingsProvider[]
			{
				new CustomSettingsProvider(new SettingsGroup[]
				{
					new SettingsGroup("Parameter", new Setting[]
					{
						new TextSetting("Name", "Parameter _name"),
						new TextSetting("Value", "_Value")
					})
				})
			});
			dlg.Text = "Parameter Properties";
			dlg.EnableProfiles = false;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string name = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();
				string value = dlg.SettingsProviders[0].SettingsGroups[0].Settings[1].GetValue()?.ToString();

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvParameters.Model.Columns[0], name),
					new TreeModelRowColumn(tvParameters.Model.Columns[1], value)
				});
				row.SetExtraData<KeyValuePair<string, string>>("item", new KeyValuePair<string, string>(name, value));
				tvParameters.Model.Rows.Add(row);

				BeginEdit();
				os.Parameters.Add(name, value);
				EndEdit();
			}
		}

		private void tsbParameterEdit_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			string oldname = tvParameters.SelectedRows[0].GetExtraData<KeyValuePair<string, string>>("item").Key;

			SettingsDialog dlg = new SettingsDialog(new SettingsProvider[]
			{
				new CustomSettingsProvider(new SettingsGroup[]
				{
					new SettingsGroup("Parameter", new Setting[]
					{
						new TextSetting("Name", "Parameter _name"),
						new TextSetting("Value", "_Value")
					})
				})
			});
			dlg.Text = "Parameter Properties";
			dlg.EnableProfiles = false;

			dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].SetValue(oldname);
			dlg.SettingsProviders[0].SettingsGroups[0].Settings[1].SetValue(os.Parameters[oldname]);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string name = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();
				string value = dlg.SettingsProviders[0].SettingsGroups[0].Settings[1].GetValue()?.ToString();

				if (os.Parameters.ContainsKey(oldname))
				{
					os.Parameters.Remove(oldname);
				}

				BeginEdit();
				os.Parameters[name] = value;
				EndEdit();

				tvParameters.SelectedRows[0].RowColumns[0].Value = name;
				tvParameters.SelectedRows[0].RowColumns[1].Value = value;
			}
		}

		private void tsbParameterRemove_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			string name = tvParameters.SelectedRows[0].GetExtraData<KeyValuePair<string, string>>("item").Key;

			if (MessageDialog.ShowDialog(String.Format("Are you sure you want to remove the '{0}' parameter?", name), "Remove Parameter", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) != DialogResult.Yes)
				return;

			BeginEdit();
			os.Parameters.Remove(name);
			EndEdit();

			tvParameters.Model.Rows.Remove(tvParameters.SelectedRows[0]);
		}

		[EventHandler(nameof(tvFiles), "RowActivated")]
		private void tvFiles_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row == null) return;
			tsbFileEdit_Click(sender, e);
		}

		private void tsbFileAdd_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			SettingsDialog dlg = new SettingsDialog();
			dlg.Text = "File Properties";
			dlg.EnableProfiles = false;

			dlg.SettingsProviders.Clear();
			dlg.SettingsProviders.Add(new CustomSettingsProvider(new SettingsGroup[]
			{
				new SettingsGroup("File", new Setting[]
				{
					new FileSetting("SourceFileName", "_Source", null, false),
					new FileSetting("DestinationFileName", "_Destination", null, false)
				})
			}));
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string name = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();
				string value = dlg.SettingsProviders[0].SettingsGroups[0].Settings[1].GetValue()?.ToString();

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvFiles.Model.Columns[0], name),
					new TreeModelRowColumn(tvFiles.Model.Columns[1], value)
				});

				BootstrapFile file = new BootstrapFile();
				file.SourceFileName = name;
				file.DestinationFileName = value;
				row.SetExtraData<BootstrapFile>("item", file);
				tvFiles.Model.Rows.Add(row);

				BeginEdit();
				os.Files.Add(file);
				EndEdit();
			}
		}

		private void tsbFileEdit_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			if (tvFiles.SelectedRows.Count < 1) return;

			BootstrapFile file = tvFiles.SelectedRows[0].GetExtraData<BootstrapFile>("item");

			SettingsDialog dlg = new SettingsDialog();
			dlg.Text = "File Properties";
			dlg.EnableProfiles = false;

			dlg.SettingsProviders.Clear();
			dlg.SettingsProviders.Add(new CustomSettingsProvider(new SettingsGroup[]
			{
				new SettingsGroup("File", new Setting[]
				{
					new FileSetting("SourceFileName", "_Source", file.SourceFileName, false),
					new FileSetting("DestinationFileName", "_Destination", file.DestinationFileName, false)
				})
			}));
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string name = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue()?.ToString();
				string value = dlg.SettingsProviders[0].SettingsGroups[0].Settings[1].GetValue()?.ToString();

				BeginEdit();
				file.SourceFileName = name;
				file.DestinationFileName = value;
				EndEdit();

				tvFiles.SelectedRows[0].RowColumns[0].Value = name;
				tvFiles.SelectedRows[0].RowColumns[1].Value = value;
			}
		}

		private void tsbFileRemove_Click(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			if (tvPlatforms.SelectedRows.Count < 1) return;

			BootstrapOperatingSystem os = tvPlatforms.SelectedRows[0].GetExtraData<BootstrapOperatingSystem>("item");
			if (os == null) return;

			BootstrapFile file = tvFiles.SelectedRows[0].GetExtraData<BootstrapFile>("item");

			if (MessageDialog.ShowDialog(String.Format("Are you sure you want to remove the '{0}' file?", file.SourceFileName), "Remove File", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) != DialogResult.Yes)
				return;

			BeginEdit();
			os.Files.Remove(file);
			EndEdit();

			tvFiles.Model.Rows.Remove(tvFiles.SelectedRows[0]);
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			(tbPlatforms.Items["tsbPlatformAdd"] as ToolbarItemButton).Click += tsbPlatformAdd_Click;
			(tbPlatforms.Items["tsbPlatformEdit"] as ToolbarItemButton).Click += tsbPlatformEdit_Click;
			(tbPlatforms.Items["tsbPlatformRemove"] as ToolbarItemButton).Click += tsbPlatformRemove_Click;

			(tbParameters.Items["tsbParameterAdd"] as ToolbarItemButton).Click += tsbParameterAdd_Click;
			(tbParameters.Items["tsbParameterEdit"] as ToolbarItemButton).Click += tsbParameterEdit_Click;
			(tbParameters.Items["tsbParameterRemove"] as ToolbarItemButton).Click += tsbParameterRemove_Click;

			(tbFiles.Items["tsbFileAdd"] as ToolbarItemButton).Click += tsbFileAdd_Click;
			(tbFiles.Items["tsbFileEdit"] as ToolbarItemButton).Click += tsbFileEdit_Click;
			(tbFiles.Items["tsbFileRemove"] as ToolbarItemButton).Click += tsbFileRemove_Click;

			OnObjectModelChanged(e);
		}
		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			tvPlatforms.Model.Rows.Clear();
			tvParameters.Model.Rows.Clear();
			tvFiles.Model.Rows.Clear();

			BootstrapScriptObjectModel setup = (ObjectModel as BootstrapScriptObjectModel);
			if (setup == null) return;

			foreach (BootstrapOperatingSystem os in setup.OperatingSystems)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvPlatforms.Model.Columns[0], os.Name)
				});
				row.SetExtraData<BootstrapOperatingSystem>("item", os);
				tvPlatforms.Model.Rows.Add(row);
			}
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

	}
}
