using System;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Dialogs
{
	/// <summary>
	/// A dialog for configuring Universal Widget Toolkit <see cref="Option" />s.
	/// Option groups defined in <see cref="OptionProvider"/>s added to the <see cref="OptionProviders" /> collection will appear in this dialog for configuration.
	/// </summary>
	public class SettingsDialog : Dialog
	{
		private DefaultTreeModel tmOptionGroups = null;
		private ListView tv = null;
		private SplitContainer vpaned = null;

		private StackSidebar sidebar = null;

		public SettingsDialog()
		{
			tmOptionGroups = new DefaultTreeModel (new Type[] { typeof(string) });

			this.Layout = new BoxLayout(Orientation.Vertical);

			this.Buttons.Add(new Button(StockType.OK, DialogResult.OK));
			this.Buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));

			this.Buttons[0].Click += cmdOK_Click;

			this.DefaultButton = this.Buttons[0];

			this.Text = "Options";
			this.Size = new Dimension2D(600, 400);

			foreach (SettingsProvider provider in Application.SettingsProviders) {
				this.SettingsProviders.Add (provider);
			}
		}

		private void cmdOK_Click (object sender, EventArgs e)
		{
			if (sidebar == null) {
				foreach (Container ct in vpaned.Panel2.Controls) {
					foreach (Control ctl in ct.Controls) {
						SaveSettingForControl (ctl);
					}
				}
			} else {
				foreach (StackSidebarPanel panel in sidebar.Items) {
					Container ct = (panel.Control as Container);
					if (ct == null)
						continue;
					
					foreach (Control ctl in ct.Controls) {
						SaveSettingForControl (ctl);
					}
				}
			}
		}

		private void SaveSettingForControl(Control ctl)
		{
			if (ctl is Label)
				return;

			Setting setting = ctl.GetExtraData<Setting> ("setting");
			if (setting == null)
				return;

			if (ctl is CheckBox) {
				setting.SetValue ((ctl as CheckBox).Checked);
			} else if (ctl is TextBox) {
				setting.SetValue ((ctl as TextBox).Text);
			}
		}

		private void CreateVSLayout()
		{
			vpaned = new SplitContainer(Orientation.Vertical);
			vpaned.Panel1.Layout = new Layouts.BoxLayout(Orientation.Horizontal);
			vpaned.Panel2.Layout = new Layouts.BoxLayout(Orientation.Horizontal);

			vpaned.SplitterPosition = 240;

			this.Controls.Add(vpaned, new BoxLayout.Constraints(true, true, 0, BoxLayout.PackType.Start));

			tv = new ListView();
			tv.Model = tmOptionGroups;
			tv.Columns.Add (new ListViewColumnText (tmOptionGroups.Columns [0], "Group"));
			tv.HeaderStyle = ColumnHeaderStyle.None;
			tv.SelectionChanged += tv_SelectionChanged;

			vpaned.Panel1.Controls.Add(tv, new BoxLayout.Constraints(true, true));
		}
		private void CreateGNOMELayout()
		{
			sidebar = new StackSidebar ();
			sidebar.Style.Classes.Add ("view");

			Controls.Add (sidebar, new BoxLayout.Constraints (true, true));
		}

		/// <summary>
		/// Contains the <see cref="OptionProvider" />s used to display options in this <see cref="OptionsDialog" />.
		/// </summary>
		/// <value>The collection of <see cref="OptionProvider" />s used to display options in this <see cref="OptionsDialog" />.</value>
		public SettingsProvider.SettingsProviderCollection SettingsProviders { get; } = new SettingsProvider.SettingsProviderCollection();

		Container ctDefault = new Container ();
		internal protected override void OnCreating (EventArgs e)
		{
			base.OnCreating (e);

			CreateVSLayout();
			// CreateGNOMELayout();

			Label lblNoOptions = new Label ("The selected group has no options available to configure");
			lblNoOptions.HorizontalAlignment = HorizontalAlignment.Center;
			lblNoOptions.VerticalAlignment = VerticalAlignment.Middle;
			ctDefault.Controls.Add(lblNoOptions, new BoxLayout.Constraints(true, true));

			if (sidebar == null) {
				vpaned.Panel2.Controls.Add (ctDefault, new BoxLayout.Constraints (true, true));
			} else {
			}

			System.Collections.Generic.List<SettingsGroup> grps = new System.Collections.Generic.List<SettingsGroup> ();
			foreach (SettingsProvider provider in SettingsProviders) {
				provider.Initialize();
				foreach (SettingsGroup grp in provider.SettingsGroups) {
					if (grps.Contains (grp))
						continue;
					
					grps.Add (grp);

					Container ct = new Container ();
					ct.Layout = new GridLayout ();
					ct.Padding = new Padding (8);
					int iRow = 0;
					foreach (Setting opt in grp.Settings) {
						LoadOptionIntoGroup(opt, ct, iRow);
						iRow++;
					}

					if (sidebar == null) {
						vpaned.Panel2.Controls.Add (ct, new BoxLayout.Constraints (true, true));
					} else {
						if (grp.Path != null && grp.Path.Length > 0) {
							StackSidebarPanel ctp = new StackSidebarPanel ();
							ct.Name = String.Join (":", grp.Path);
							ct.Text = grp.Path [grp.Path.Length - 1];
							ctp.Control = ct;
							sidebar.Items.Add (ctp);
						}
					}

					optionGroupContainers [grp] = ct;
				}
			}
			grps.Sort ();
			foreach (SettingsGroup grp in grps) {
				AddOptionGroupPathPart (grp, grp.Path, 0);
			}
		}

		private System.Collections.Generic.Dictionary<SettingsGroup, Container> optionGroupContainers = new System.Collections.Generic.Dictionary<SettingsGroup, Container>();

		private void LoadOptionIntoGroup(Setting opt, Container ct, int iRow)
		{
			if (opt is TextSetting)
			{
				TextSetting o = (opt as TextSetting);

				Label lbl = new Label();
				lbl.HorizontalAlignment = HorizontalAlignment.Left;
				lbl.Text = o.Title + ": ";
				ct.Controls.Add(lbl, new GridLayout.Constraints(iRow, 0, 1, 1));
				TextBox txt = new TextBox();
				txt.Text = o.GetValue<string>();
				txt.SetExtraData<Setting>("setting", o);
				ct.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
			}
			else if (opt is BooleanSetting)
			{
				BooleanSetting o = (opt as BooleanSetting);
				CheckBox chk = new CheckBox();
				chk.Text = o.Title;
				chk.Checked = o.GetValue<bool>();
				chk.SetExtraData<Setting>("setting", o);
				ct.Controls.Add(chk, new GridLayout.Constraints(iRow, 0, 1, 2, ExpandMode.Horizontal));
			}
			else if (opt is ChoiceSetting)
			{
				ChoiceSetting o = (opt as ChoiceSetting);

				Label lbl = new Label();
				lbl.HorizontalAlignment = HorizontalAlignment.Left;
				lbl.Text = o.Title;
				ct.Controls.Add(lbl, new GridLayout.Constraints(iRow, 0));
				ComboBox cbo = new ComboBox();
				cbo.ReadOnly = true; // o.RequireSelectionFromList;
				DefaultTreeModel tm = new DefaultTreeModel(new Type[] { typeof(string) });
				foreach (ChoiceSetting.ChoiceSettingValue value in o.ValidValues)
				{
					tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tm.Columns[0], value.Title)
					}));
				}
				cbo.Model = tm;
				cbo.Text = o.GetValue<string>();
				cbo.SetExtraData<Setting>("setting", o);
				ct.Controls.Add(cbo, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
			}
			else if (opt is GroupSetting)
			{
				GroupSetting o = (opt as GroupSetting);

				GroupBox ct1 = new GroupBox();
				ct1.Text = opt.Title;
				ct1.Layout = new GridLayout();
				for (int j = 0; j < o.Options.Count; j++)
				{
					LoadOptionIntoGroup(((GroupSetting)opt).Options[j], ct1, j);
				}

				ct.Controls.Add(ct1, new GridLayout.Constraints(iRow, 0, 1, 2, ExpandMode.Horizontal));
			}
			else if (opt is RangeSetting)
			{
				RangeSetting o = (opt as RangeSetting);

				Label lbl = new Label();
				lbl.HorizontalAlignment = HorizontalAlignment.Left;
				lbl.Text = o.Title;
				ct.Controls.Add(lbl, new GridLayout.Constraints(iRow, 0));

				NumericTextBox txt = new NumericTextBox();
				txt.Minimum = o.MinimumValue;
				txt.Maximum = o.MaximumValue;
				txt.Value = o.GetValue<double>();
				ct.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
			}
		}

		private void AddOptionGroupPathPart(SettingsGroup grp, string[] path, int index, TreeModelRow parent = null)
		{
			if (index > path.Length - 1)
				return;

			string strpath = String.Join (":", path, 0, index + 1);
			TreeModelRow row = null;

			if (parent == null) {
				row = tmOptionGroups.Rows [strpath];
			} else {
				row = parent.Rows [strpath];
			}

			if (row == null) {
				row = new TreeModelRow (new TreeModelRowColumn[] { new TreeModelRowColumn (tmOptionGroups.Columns [0], path [index]) });
				row.Name = strpath;
				if (parent == null) {
					tmOptionGroups.Rows.Add(row);
				}
				else {
					parent.Rows.Add(row);
				}
			}

			if (index + 1 > path.Length - 1) {
				row.SetExtraData<SettingsGroup> ("group", grp);
			}

			AddOptionGroupPathPart (grp, path, index + 1, row);
		}

		private void tv_SelectionChanged(object sender, EventArgs e)
		{
			ctDefault.Visible = false;
			foreach (Control ctl in vpaned.Panel2.Controls) {
				ctl.Visible = false;
			}

			SettingsGroup thegrp = tv.SelectedRows [0].GetExtraData<SettingsGroup> ("group");
			if (thegrp == null) {
				ctDefault.Visible = true;
				return;
			}
			
			if (optionGroupContainers.ContainsKey (thegrp)) {
				optionGroupContainers [thegrp].Visible = true;
			} else {
				ctDefault.Visible = true;
			}
		}
	}
}
