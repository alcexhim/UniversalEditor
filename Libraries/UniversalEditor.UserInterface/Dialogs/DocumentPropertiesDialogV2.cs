//
//  DocumentPropertiesDialogV2.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.UserInterface.Controls.FileBrowser;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.UserInterface.Controls;
using MBS.Framework;
using MBS.Framework.Settings;

namespace UniversalEditor.UserInterface.Dialogs
{
	public class DocumentPropertiesDialogV2 : Dialog
	{
		public DocumentPropertiesDialogV2 ()
		{
			this.InitializeComponent ();
			this.Buttons [0].Click += cmdOK_Click;
		}

		protected override void OnCreating (EventArgs e)
		{
			base.OnCreating (e);
			this.Buttons [0].ResponseValue = (int)DialogResult.OK;
			this.Buttons [1].ResponseValue = (int)DialogResult.Cancel;

			switch (Mode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					this.Text = "Open Document";
					this.Buttons [0].StockType = StockType.Open;
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					this.Text = "Save Document";
					this.Buttons [0].StockType = StockType.Save;
					break;
				}
			}
		}

		void cmdOK_Click (object sender, EventArgs e)
		{
			StackSidebarPanel pnl = sidebar.SelectedPanel;
			if (pnl == null) return;

			Container ct = (pnl.Control as Container);

			AccessorReference accref = pnl.GetExtraData<AccessorReference> ("ar");
			CustomOptionCompatSettingsProvider coll = new CustomOptionCompatSettingsProvider();
			foreach (Control ctl in ct.Controls) {
				Setting eo = (ctl.GetExtraData ("eo") as Setting);
				if (eo == null)
					continue;

				if (ctl is CheckBox) {
					CheckBox itm = (ctl as CheckBox);
					(eo as BooleanSetting).SetValue<bool>(itm.Checked);
				}
				/*
					else if (ctl is ComboBox)
					{
						ChoiceSetting.ChoiceSettingValue choice = ((ctl as ComboBox).SelectedItem as ChoiceSetting.ChoiceSettingValue);
						(eo as ChoiceSetting).Value = choice;
					}
					*/
				else if (ctl is TextBox) {
					TextBox itm = (ctl as TextBox);
					if (eo is TextSetting) {
						(eo as TextSetting).SetValue<string>(itm.Text);
					}
				} else if (ctl is UniversalEditorFileBrowserControl) {
					UniversalEditorFileBrowserControl fbc = (ctl as UniversalEditorFileBrowserControl);
					if (eo is FileSetting)
					{
						(eo as FileSetting).SetValue<string>(fbc.SelectedFileNames[0]);
					}
				}

				coll.SettingsGroups[0].Settings.Add(eo);
			}

			Accessor acc = accref.Create ();
			((EditorApplication)Application.Instance).ApplyCustomOptions(acc, coll);

			Accessor = acc;

			this.DialogResult = DialogResult.OK;
			this.Close ();
		}

		public DocumentPropertiesDialogMode Mode { get; set; } = DocumentPropertiesDialogMode.Open;

		private ObjectModel mvarInitialObjectModel = null;

		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; mvarInitialObjectModel = value; } }

		private DataFormat mvarInitialDataFormat = null;

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; mvarInitialDataFormat = value; } }

		private Accessor mvarInitialAccesor = null;

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; mvarInitialAccesor = value; } }

		private StackSidebar sidebar = null;

		private void fbc_ItemActivated (object sender, EventArgs e)
		{
			cmdOK_Click (sender, e);
		}

		private void InitializeComponent()
		{
			sidebar = new StackSidebar ();
			this.Layout = new BoxLayout (Orientation.Vertical);

			AccessorReference[] accessors = UniversalEditor.Common.Reflection.GetAvailableAccessors ();
			foreach (AccessorReference acc in accessors) {
				if (String.IsNullOrEmpty (acc.Title)) {
					// the accessor doesn't have a title - interpret this to
					// mean we shouldn't be showing it in the UI
					continue;
				}
				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.SetExtraData<AccessorReference> ("ar", acc);

				Container ct = new Container ();
				ct.Layout = new GridLayout ();
				ct.Name = acc.AccessorType.FullName;
				ct.Text = acc.Title;

				SettingsProvider coll = null;
				switch (Mode)
				{
					case DocumentPropertiesDialogMode.Open:
					{
						coll = acc.ImportOptions;
						break;
					}
					case DocumentPropertiesDialogMode.Save:
					{
						coll = acc.ExportOptions;
						break;
					}
				}

				int iRow = 0;
				foreach (SettingsGroup sg in coll.SettingsGroups)
				{
					foreach (Setting eo in sg.Settings)
					{
						// do not render the CustomOption if it's supposed to be invisible
						if (!eo.Visible) continue;

						if (!(eo is BooleanSetting) && (!(eo is FileSetting)))
						{
							Label lbl = new Label();
							// lbl.FlatStyle = FlatStyle.System;
							// lbl.AutoSize = true;
							// lbl.Dock = DockStyle.None;
							// lbl.Anchor = AnchorStyles.Left;
							lbl.UseMnemonic = true;
							lbl.Text = eo.Title; // .Replace("_", "&"); // only for WinForms
							ct.Controls.Add(lbl, new GridLayout.Constraints(iRow, 0, 1, 1, ExpandMode.None));
						}

						if (eo is ChoiceSetting)
						{
							ChoiceSetting option = (eo as ChoiceSetting);

							ComboBox cbo = new ComboBox();
							cbo.Model = new DefaultTreeModel(new Type[] { typeof(string) });
							cbo.Renderers.Add(new CellRendererText(cbo.Model.Columns[0]));

							// if (option.RequireChoice) cbo.DropDownStyle = ComboBoxStyle.DropDownList;

							foreach (ChoiceSetting.ChoiceSettingValue choice in option.ValidValues)
							{
								TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
								{
									new TreeModelRowColumn((cbo.Model as DefaultTreeModel).Columns[0], choice.Title)
								});
								row.SetExtraData<ChoiceSetting.ChoiceSettingValue>("choice", choice);
								(cbo.Model as DefaultTreeModel).Rows.Add(row);
							}

							ct.Controls.Add(cbo, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
						}
						else if (eo is RangeSetting)
						{
							RangeSetting option = (eo as RangeSetting);

							TextBox txt = new TextBox(); // NumericUpDown txt = new NumericUpDown();
							txt.SetExtraData("eo", option);
							if (option.MaximumValue.HasValue)
							{
								// txt.Maximum = option.MaximumValue.Value;
							}
							else
							{
								// txt.Maximum = Decimal.MaxValue;
							}
							if (option.MinimumValue.HasValue)
							{
								// txt.Minimum = option.MinimumValue.Value;
							}
							else
							{
								// txt.Minimum = Decimal.MinValue;
							}
							// txt.Value = option.DefaultValue;

							ct.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
						}
						else if (eo is TextSetting)
						{
							TextSetting option = (eo as TextSetting);

							TextBox txt = new TextBox();
							txt.SetExtraData("eo", option);
							txt.Text = (string)option.DefaultValue;
							if (option.MaximumLength.HasValue) txt.MaxLength = option.MaximumLength.Value;
							ct.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
						}
						else if (eo is BooleanSetting)
						{
							BooleanSetting option = (eo as BooleanSetting);

							CheckBox chk = new CheckBox();
							chk.SetExtraData("eo", option);
							chk.Text = option.Title;

							ct.Controls.Add(chk, new GridLayout.Constraints(iRow, 0, 1, 2, ExpandMode.Horizontal));
						}
						else if (eo is FileSetting)
						{
							FileSetting option = (eo as FileSetting);

							UniversalEditorFileBrowserControl fbc = new UniversalEditorFileBrowserControl();
							fbc.SetExtraData("eo", option);
							fbc.ItemActivated += fbc_ItemActivated;
							// TextBox cmd = new TextBox();
							// AwesomeControls.FileTextBox.FileTextBoxControl cmd = new AwesomeControls.FileTextBox.FileTextBoxControl();
							// cmd.Click += cmdFileBrowse_Click;
							// cmd.Dock = DockStyle.Fill;
							fbc.SetExtraData<Setting>("eo", eo);
							switch (option.Mode)
							{
								case FileSettingMode.CreateFolder: fbc.Mode = FileBrowserMode.CreateFolder; break;
								case FileSettingMode.Open: fbc.Mode = FileBrowserMode.Open; break;
								case FileSettingMode.Save: fbc.Mode = FileBrowserMode.Save; break;
								case FileSettingMode.SelectFolder: fbc.Mode = FileBrowserMode.SelectFolder; break;
							}
							ct.Controls.Add(fbc, new GridLayout.Constraints(iRow, 0, 1, 2, ExpandMode.Both));
						}

						// tbl.ColumnCount = 2;
						// tbl.RowCount = CustomOptionControls.Count;
						iRow++;
					}
				}

				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			this.Controls.Add (sidebar, new BoxLayout.Constraints(true, true));

			this.Buttons.Add (new Button (StockType.OK));
			this.Buttons [0].ResponseValue = (int)DialogResult.OK;
			this.Buttons.Add (new Button (StockType.Cancel));
			this.Buttons [1].ResponseValue = (int)DialogResult.Cancel;

			switch (Mode)
			{
				case DocumentPropertiesDialogMode.Open:
				{
					this.Text = "Open Document";
					this.Buttons [0].StockType = StockType.Open;
					break;
				}
				case DocumentPropertiesDialogMode.Save:
				{
					this.Text = "Save Document";
					this.Buttons [0].StockType = StockType.Save;
					break;
				}
			}

			MinimumSize = new Dimension2D (300, 200);
			Size = new Dimension2D (600, 400);
		}
	}
}
