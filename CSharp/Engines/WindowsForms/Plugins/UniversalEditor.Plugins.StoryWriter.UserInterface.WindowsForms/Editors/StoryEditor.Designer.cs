namespace UniversalEditor.Editors
{
	partial class StoryEditor
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.mnuContextTreeView = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextTreeViewAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddExistingItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewExclude = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.lv = new System.Windows.Forms.ListView();
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.imlLargeIcons = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.mnuContextTreeView.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tv);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lv);
			this.splitContainer1.Size = new System.Drawing.Size(616, 306);
			this.splitContainer1.SplitterDistance = 205;
			this.splitContainer1.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.ContextMenuStrip = this.mnuContextTreeView;
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(205, 306);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
			// 
			// mnuContextTreeView
			// 
			this.mnuContextTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewAdd,
            this.mnuContextTreeViewSep1,
            this.mnuContextTreeViewExclude,
            this.mnuContextTreeViewSep2,
            this.mnuContextTreeViewCut,
            this.mnuContextTreeViewCopy,
            this.mnuContextTreeViewPaste,
            this.mnuContextTreeViewDelete,
            this.mnuContextTreeViewRename,
            this.mnuContextTreeViewSep3,
            this.mnuContextTreeViewProperties});
			this.mnuContextTreeView.Name = "mnuContextTreeView";
			this.mnuContextTreeView.Size = new System.Drawing.Size(190, 198);
			this.mnuContextTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextTreeView_Opening);
			// 
			// mnuContextTreeViewAdd
			// 
			this.mnuContextTreeViewAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewAddNewItem,
            this.mnuContextTreeViewAddExistingItem});
			this.mnuContextTreeViewAdd.Name = "mnuContextTreeViewAdd";
			this.mnuContextTreeViewAdd.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewAdd.Text = "A&dd";
			// 
			// mnuContextTreeViewAddNewItem
			// 
			this.mnuContextTreeViewAddNewItem.Name = "mnuContextTreeViewAddNewItem";
			this.mnuContextTreeViewAddNewItem.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewAddNewItem.Text = "Ne&w Item...";
			// 
			// mnuContextTreeViewAddExistingItem
			// 
			this.mnuContextTreeViewAddExistingItem.Name = "mnuContextTreeViewAddExistingItem";
			this.mnuContextTreeViewAddExistingItem.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewAddExistingItem.Text = "Existin&g Item...";
			// 
			// mnuContextTreeViewSep1
			// 
			this.mnuContextTreeViewSep1.Name = "mnuContextTreeViewSep1";
			this.mnuContextTreeViewSep1.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuContextTreeViewExclude
			// 
			this.mnuContextTreeViewExclude.Name = "mnuContextTreeViewExclude";
			this.mnuContextTreeViewExclude.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewExclude.Text = "Exclude From Pro&ject";
			// 
			// mnuContextTreeViewSep2
			// 
			this.mnuContextTreeViewSep2.Name = "mnuContextTreeViewSep2";
			this.mnuContextTreeViewSep2.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuContextTreeViewCut
			// 
			this.mnuContextTreeViewCut.Name = "mnuContextTreeViewCut";
			this.mnuContextTreeViewCut.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewCut.Text = "Cu&t";
			// 
			// mnuContextTreeViewCopy
			// 
			this.mnuContextTreeViewCopy.Name = "mnuContextTreeViewCopy";
			this.mnuContextTreeViewCopy.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewCopy.Text = "&Copy";
			// 
			// mnuContextTreeViewPaste
			// 
			this.mnuContextTreeViewPaste.Name = "mnuContextTreeViewPaste";
			this.mnuContextTreeViewPaste.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewPaste.Text = "&Paste";
			// 
			// mnuContextTreeViewDelete
			// 
			this.mnuContextTreeViewDelete.Name = "mnuContextTreeViewDelete";
			this.mnuContextTreeViewDelete.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewDelete.Text = "&Delete";
			// 
			// mnuContextTreeViewRename
			// 
			this.mnuContextTreeViewRename.Name = "mnuContextTreeViewRename";
			this.mnuContextTreeViewRename.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewRename.Text = "Rena&me";
			// 
			// mnuContextTreeViewSep3
			// 
			this.mnuContextTreeViewSep3.Name = "mnuContextTreeViewSep3";
			this.mnuContextTreeViewSep3.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuContextTreeViewProperties
			// 
			this.mnuContextTreeViewProperties.Name = "mnuContextTreeViewProperties";
			this.mnuContextTreeViewProperties.ShortcutKeyDisplayString = "Alt+Enter";
			this.mnuContextTreeViewProperties.Size = new System.Drawing.Size(189, 22);
			this.mnuContextTreeViewProperties.Text = "P&roperties...";
			// 
			// lv
			// 
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.Location = new System.Drawing.Point(0, 0);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(407, 306);
			this.lv.TabIndex = 0;
			this.lv.UseCompatibleStateImageBehavior = false;
			// 
			// imlSmallIcons
			// 
			this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imlLargeIcons
			// 
			this.imlLargeIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlLargeIcons.ImageSize = new System.Drawing.Size(32, 32);
			this.imlLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// StoryEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "StoryEditor";
			this.Size = new System.Drawing.Size(616, 306);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.mnuContextTreeView.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ImageList imlSmallIcons;
		private System.Windows.Forms.ImageList imlLargeIcons;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextTreeView;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAdd;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddNewItem;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddExistingItem;
		private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewProperties;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewCut;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewPaste;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewDelete;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewRename;
		private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewExclude;
		private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewSep2;
	}
}
