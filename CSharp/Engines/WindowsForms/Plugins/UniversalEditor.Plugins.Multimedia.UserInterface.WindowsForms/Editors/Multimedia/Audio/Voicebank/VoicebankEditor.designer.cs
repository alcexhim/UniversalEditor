// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

namespace UniversalEditor.Editors.Multimedia.Audio.Voicebank
{
	partial class VoicebankEditor
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceDatabaseEditor));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.lvPhonemes = new System.Windows.Forms.ListView();
			this.chPhonemeName = new System.Windows.Forms.ColumnHeader();
			this.chPhonemeData = new System.Windows.Forms.ColumnHeader();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lvGroups = new System.Windows.Forms.ListView();
			this.chPhonemeGroupName = new System.Windows.Forms.ColumnHeader();
			this.chPhonemeGroupPhonemeCount = new System.Windows.Forms.ColumnHeader();
			this.chPhonemeGroupData1 = new System.Windows.Forms.ColumnHeader();
			this.chPhonemeGroupData2 = new System.Windows.Forms.ColumnHeader();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lvExplorer = new System.Windows.Forms.ListView();
			this.imlExplorerLargeIcons = new System.Windows.Forms.ImageList(this.components);
			this.imlExplorerSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.lvPhonemes);
			this.groupBox1.Location = new System.Drawing.Point(3, 123);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 175);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Phoneme List";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(6, 19);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
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
			this.lvPhonemes.Location = new System.Drawing.Point(6, 48);
			this.lvPhonemes.Name = "lvPhonemes";
			this.lvPhonemes.Size = new System.Drawing.Size(388, 121);
			this.lvPhonemes.TabIndex = 1;
			this.lvPhonemes.UseCompatibleStateImageBehavior = false;
			this.lvPhonemes.View = System.Windows.Forms.View.Details;
			// 
			// chPhonemeName
			// 
			this.chPhonemeName.Text = "Name";
			this.chPhonemeName.Width = 285;
			// 
			// chPhonemeData
			// 
			this.chPhonemeData.Text = "Data";
			this.chPhonemeData.Width = 94;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.lvGroups);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(397, 114);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Phoneme Groups";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 19);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// lvGroups
			// 
			this.lvGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chPhonemeGroupName,
									this.chPhonemeGroupPhonemeCount,
									this.chPhonemeGroupData1,
									this.chPhonemeGroupData2});
			this.lvGroups.FullRowSelect = true;
			this.lvGroups.GridLines = true;
			this.lvGroups.HideSelection = false;
			this.lvGroups.Location = new System.Drawing.Point(6, 48);
			this.lvGroups.Name = "lvGroups";
			this.lvGroups.Size = new System.Drawing.Size(385, 60);
			this.lvGroups.TabIndex = 1;
			this.lvGroups.UseCompatibleStateImageBehavior = false;
			this.lvGroups.View = System.Windows.Forms.View.Details;
			this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
			// 
			// chPhonemeGroupName
			// 
			this.chPhonemeGroupName.Text = "Name";
			this.chPhonemeGroupName.Width = 190;
			// 
			// chPhonemeGroupPhonemeCount
			// 
			this.chPhonemeGroupPhonemeCount.Text = "Phonemes";
			this.chPhonemeGroupPhonemeCount.Width = 82;
			// 
			// chPhonemeGroupData1
			// 
			this.chPhonemeGroupData1.Text = "Data 1";
			// 
			// chPhonemeGroupData2
			// 
			this.chPhonemeGroupData2.Text = "Data 2";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(414, 327);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(406, 301);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Programmer";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lvExplorer);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(406, 301);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Explorer";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// lvExplorer
			// 
			this.lvExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvExplorer.FullRowSelect = true;
			this.lvExplorer.GridLines = true;
			this.lvExplorer.HideSelection = false;
			this.lvExplorer.LargeImageList = this.imlExplorerLargeIcons;
			this.lvExplorer.Location = new System.Drawing.Point(3, 3);
			this.lvExplorer.Name = "lvExplorer";
			this.lvExplorer.Size = new System.Drawing.Size(400, 295);
			this.lvExplorer.SmallImageList = this.imlExplorerSmallIcons;
			this.lvExplorer.TabIndex = 1;
			this.lvExplorer.UseCompatibleStateImageBehavior = false;
			// 
			// imlExplorerLargeIcons
			// 
			this.imlExplorerLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlExplorerLargeIcons.ImageStream")));
			this.imlExplorerLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlExplorerLargeIcons.Images.SetKeyName(0, "folder_closed");
			this.imlExplorerLargeIcons.Images.SetKeyName(1, "folder_open");
			// 
			// imlExplorerSmallIcons
			// 
			this.imlExplorerSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlExplorerSmallIcons.ImageStream")));
			this.imlExplorerSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlExplorerSmallIcons.Images.SetKeyName(0, "folder_closed");
			this.imlExplorerSmallIcons.Images.SetKeyName(1, "folder_open");
			// 
			// VoiceDatabaseEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "VoiceDatabaseEditor";
			this.Size = new System.Drawing.Size(420, 333);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ImageList imlExplorerSmallIcons;
		private System.Windows.Forms.ImageList imlExplorerLargeIcons;
		private System.Windows.Forms.ListView lvExplorer;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ColumnHeader chPhonemeGroupData2;
		private System.Windows.Forms.ColumnHeader chPhonemeGroupData1;
		private System.Windows.Forms.ListView lvPhonemes;
		private System.Windows.Forms.ColumnHeader chPhonemeGroupPhonemeCount;
		private System.Windows.Forms.ColumnHeader chPhonemeGroupName;
		private System.Windows.Forms.ColumnHeader chPhonemeData;
		private System.Windows.Forms.ColumnHeader chPhonemeName;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListView lvGroups;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
