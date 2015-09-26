namespace UniversalEditor.Editors.Multimedia.Audio.Voicebank
{
	partial class VoicebankIndexEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tbsTabs = new System.Windows.Forms.TabControl();
			this.tabPhonemes = new System.Windows.Forms.TabPage();
			this.tabPhonemeGroups = new System.Windows.Forms.TabPage();
			this.cmdPhonemeAdd = new System.Windows.Forms.Button();
			this.cmdPhonemeModify = new System.Windows.Forms.Button();
			this.cmdPhonemeRemove = new System.Windows.Forms.Button();
			this.cmdPhonemeClear = new System.Windows.Forms.Button();
			this.lvPhonemes = new System.Windows.Forms.ListView();
			this.chPhonemeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chPhonemeData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tvPhonemeGroups = new System.Windows.Forms.TreeView();
			this.mnuContextPhonemeGroups = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextPhonemeGroupsNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsAddGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsAddPhoneme = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextPhonemeGroupsCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPhonemeGroupsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextPhonemeGroupsProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.tbsTabs.SuspendLayout();
			this.tabPhonemes.SuspendLayout();
			this.tabPhonemeGroups.SuspendLayout();
			this.mnuContextPhonemeGroups.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbsTabs
			// 
			this.tbsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbsTabs.Controls.Add(this.tabPhonemes);
			this.tbsTabs.Controls.Add(this.tabPhonemeGroups);
			this.tbsTabs.Location = new System.Drawing.Point(3, 3);
			this.tbsTabs.Name = "tbsTabs";
			this.tbsTabs.SelectedIndex = 0;
			this.tbsTabs.Size = new System.Drawing.Size(490, 303);
			this.tbsTabs.TabIndex = 0;
			// 
			// tabPhonemes
			// 
			this.tabPhonemes.Controls.Add(this.lvPhonemes);
			this.tabPhonemes.Controls.Add(this.cmdPhonemeClear);
			this.tabPhonemes.Controls.Add(this.cmdPhonemeRemove);
			this.tabPhonemes.Controls.Add(this.cmdPhonemeModify);
			this.tabPhonemes.Controls.Add(this.cmdPhonemeAdd);
			this.tabPhonemes.Location = new System.Drawing.Point(4, 22);
			this.tabPhonemes.Name = "tabPhonemes";
			this.tabPhonemes.Padding = new System.Windows.Forms.Padding(3);
			this.tabPhonemes.Size = new System.Drawing.Size(482, 277);
			this.tabPhonemes.TabIndex = 0;
			this.tabPhonemes.Text = "Phonemes";
			this.tabPhonemes.UseVisualStyleBackColor = true;
			// 
			// tabPhonemeGroups
			// 
			this.tabPhonemeGroups.Controls.Add(this.tvPhonemeGroups);
			this.tabPhonemeGroups.Location = new System.Drawing.Point(4, 22);
			this.tabPhonemeGroups.Name = "tabPhonemeGroups";
			this.tabPhonemeGroups.Padding = new System.Windows.Forms.Padding(3);
			this.tabPhonemeGroups.Size = new System.Drawing.Size(482, 277);
			this.tabPhonemeGroups.TabIndex = 1;
			this.tabPhonemeGroups.Text = "Phoneme Groups";
			this.tabPhonemeGroups.UseVisualStyleBackColor = true;
			// 
			// cmdPhonemeAdd
			// 
			this.cmdPhonemeAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPhonemeAdd.Location = new System.Drawing.Point(6, 6);
			this.cmdPhonemeAdd.Name = "cmdPhonemeAdd";
			this.cmdPhonemeAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdPhonemeAdd.TabIndex = 0;
			this.cmdPhonemeAdd.Text = "&Add";
			this.cmdPhonemeAdd.UseVisualStyleBackColor = true;
			// 
			// cmdPhonemeModify
			// 
			this.cmdPhonemeModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPhonemeModify.Location = new System.Drawing.Point(87, 6);
			this.cmdPhonemeModify.Name = "cmdPhonemeModify";
			this.cmdPhonemeModify.Size = new System.Drawing.Size(75, 23);
			this.cmdPhonemeModify.TabIndex = 0;
			this.cmdPhonemeModify.Text = "&Modify";
			this.cmdPhonemeModify.UseVisualStyleBackColor = true;
			// 
			// cmdPhonemeRemove
			// 
			this.cmdPhonemeRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPhonemeRemove.Location = new System.Drawing.Point(168, 6);
			this.cmdPhonemeRemove.Name = "cmdPhonemeRemove";
			this.cmdPhonemeRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdPhonemeRemove.TabIndex = 0;
			this.cmdPhonemeRemove.Text = "&Remove";
			this.cmdPhonemeRemove.UseVisualStyleBackColor = true;
			// 
			// cmdPhonemeClear
			// 
			this.cmdPhonemeClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPhonemeClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPhonemeClear.Location = new System.Drawing.Point(401, 6);
			this.cmdPhonemeClear.Name = "cmdPhonemeClear";
			this.cmdPhonemeClear.Size = new System.Drawing.Size(75, 23);
			this.cmdPhonemeClear.TabIndex = 0;
			this.cmdPhonemeClear.Text = "&Clear";
			this.cmdPhonemeClear.UseVisualStyleBackColor = true;
			// 
			// lvPhonemes
			// 
			this.lvPhonemes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvPhonemes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPhonemeName,
            this.chPhonemeData});
			this.lvPhonemes.FullRowSelect = true;
			this.lvPhonemes.GridLines = true;
			this.lvPhonemes.HideSelection = false;
			this.lvPhonemes.Location = new System.Drawing.Point(6, 35);
			this.lvPhonemes.Name = "lvPhonemes";
			this.lvPhonemes.Size = new System.Drawing.Size(470, 236);
			this.lvPhonemes.TabIndex = 1;
			this.lvPhonemes.UseCompatibleStateImageBehavior = false;
			this.lvPhonemes.View = System.Windows.Forms.View.Details;
			// 
			// chPhonemeName
			// 
			this.chPhonemeName.Text = "Name";
			this.chPhonemeName.Width = 396;
			// 
			// chPhonemeData
			// 
			this.chPhonemeData.Text = "Data1";
			// 
			// tvPhonemeGroups
			// 
			this.tvPhonemeGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tvPhonemeGroups.ContextMenuStrip = this.mnuContextPhonemeGroups;
			this.tvPhonemeGroups.Location = new System.Drawing.Point(6, 6);
			this.tvPhonemeGroups.Name = "tvPhonemeGroups";
			this.tvPhonemeGroups.Size = new System.Drawing.Size(470, 265);
			this.tvPhonemeGroups.TabIndex = 6;
			// 
			// mnuContextPhonemeGroups
			// 
			this.mnuContextPhonemeGroups.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextPhonemeGroupsNew,
            this.mnuContextPhonemeGroupsSep1,
            this.mnuContextPhonemeGroupsCut,
            this.mnuContextPhonemeGroupsCopy,
            this.mnuContextPhonemeGroupsPaste,
            this.mnuContextPhonemeGroupsDelete,
            this.mnuContextPhonemeGroupsSep2,
            this.mnuContextPhonemeGroupsProperties});
			this.mnuContextPhonemeGroups.Name = "mnuContextPhonemeGroups";
			this.mnuContextPhonemeGroups.Size = new System.Drawing.Size(137, 148);
			// 
			// mnuContextPhonemeGroupsNew
			// 
			this.mnuContextPhonemeGroupsNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextPhonemeGroupsAddGroup,
            this.mnuContextPhonemeGroupsAddPhoneme});
			this.mnuContextPhonemeGroupsNew.Name = "mnuContextPhonemeGroupsNew";
			this.mnuContextPhonemeGroupsNew.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsNew.Text = "Ne&w";
			// 
			// mnuContextPhonemeGroupsAddGroup
			// 
			this.mnuContextPhonemeGroupsAddGroup.Name = "mnuContextPhonemeGroupsAddGroup";
			this.mnuContextPhonemeGroupsAddGroup.Size = new System.Drawing.Size(161, 22);
			this.mnuContextPhonemeGroupsAddGroup.Text = "Phoneme &Group";
			// 
			// mnuContextPhonemeGroupsAddPhoneme
			// 
			this.mnuContextPhonemeGroupsAddPhoneme.Name = "mnuContextPhonemeGroupsAddPhoneme";
			this.mnuContextPhonemeGroupsAddPhoneme.Size = new System.Drawing.Size(161, 22);
			this.mnuContextPhonemeGroupsAddPhoneme.Text = "&Phoneme";
			// 
			// mnuContextPhonemeGroupsSep1
			// 
			this.mnuContextPhonemeGroupsSep1.Name = "mnuContextPhonemeGroupsSep1";
			this.mnuContextPhonemeGroupsSep1.Size = new System.Drawing.Size(133, 6);
			// 
			// mnuContextPhonemeGroupsCut
			// 
			this.mnuContextPhonemeGroupsCut.Enabled = false;
			this.mnuContextPhonemeGroupsCut.Name = "mnuContextPhonemeGroupsCut";
			this.mnuContextPhonemeGroupsCut.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsCut.Text = "Cu&t";
			// 
			// mnuContextPhonemeGroupsCopy
			// 
			this.mnuContextPhonemeGroupsCopy.Enabled = false;
			this.mnuContextPhonemeGroupsCopy.Name = "mnuContextPhonemeGroupsCopy";
			this.mnuContextPhonemeGroupsCopy.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsCopy.Text = "&Copy";
			// 
			// mnuContextPhonemeGroupsPaste
			// 
			this.mnuContextPhonemeGroupsPaste.Enabled = false;
			this.mnuContextPhonemeGroupsPaste.Name = "mnuContextPhonemeGroupsPaste";
			this.mnuContextPhonemeGroupsPaste.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsPaste.Text = "&Paste";
			// 
			// mnuContextPhonemeGroupsDelete
			// 
			this.mnuContextPhonemeGroupsDelete.Enabled = false;
			this.mnuContextPhonemeGroupsDelete.Name = "mnuContextPhonemeGroupsDelete";
			this.mnuContextPhonemeGroupsDelete.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsDelete.Text = "&Delete";
			this.mnuContextPhonemeGroupsDelete.Visible = false;
			// 
			// mnuContextPhonemeGroupsSep2
			// 
			this.mnuContextPhonemeGroupsSep2.Name = "mnuContextPhonemeGroupsSep2";
			this.mnuContextPhonemeGroupsSep2.Size = new System.Drawing.Size(133, 6);
			// 
			// mnuContextPhonemeGroupsProperties
			// 
			this.mnuContextPhonemeGroupsProperties.Enabled = false;
			this.mnuContextPhonemeGroupsProperties.Name = "mnuContextPhonemeGroupsProperties";
			this.mnuContextPhonemeGroupsProperties.Size = new System.Drawing.Size(136, 22);
			this.mnuContextPhonemeGroupsProperties.Text = "P&roperties...";
			// 
			// VoicebankIndexEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbsTabs);
			this.Name = "VoicebankIndexEditor";
			this.Size = new System.Drawing.Size(496, 309);
			this.tbsTabs.ResumeLayout(false);
			this.tabPhonemes.ResumeLayout(false);
			this.tabPhonemeGroups.ResumeLayout(false);
			this.mnuContextPhonemeGroups.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tbsTabs;
		private System.Windows.Forms.TabPage tabPhonemes;
		private System.Windows.Forms.TabPage tabPhonemeGroups;
		private System.Windows.Forms.Button cmdPhonemeClear;
		private System.Windows.Forms.Button cmdPhonemeRemove;
		private System.Windows.Forms.Button cmdPhonemeModify;
		private System.Windows.Forms.Button cmdPhonemeAdd;
		private System.Windows.Forms.ListView lvPhonemes;
		private System.Windows.Forms.ColumnHeader chPhonemeName;
		private System.Windows.Forms.ColumnHeader chPhonemeData;
		private System.Windows.Forms.TreeView tvPhonemeGroups;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextPhonemeGroups;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsNew;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsAddGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsAddPhoneme;
		private System.Windows.Forms.ToolStripSeparator mnuContextPhonemeGroupsSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsCut;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsPaste;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsDelete;
		private System.Windows.Forms.ToolStripSeparator mnuContextPhonemeGroupsSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPhonemeGroupsProperties;
	}
}
