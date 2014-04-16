namespace UniversalEditor.UserInterface.WindowsForms.Editors
{
	partial class PropertyListEditor
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
            this.mnuContextTreeViewNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextTreeViewNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextTreeViewNewProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextTreeViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextTreeViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.lv = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuContextListView = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextListViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextListViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextListViewNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextListViewNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextListViewNewProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextListViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextListViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mnuContextTreeView.SuspendLayout();
            this.mnuContextListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Size = new System.Drawing.Size(557, 318);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 0;
            // 
            // tv
            // 
            this.tv.ContextMenuStrip = this.mnuContextTreeView;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.LabelEdit = true;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(185, 318);
            this.tv.TabIndex = 0;
            this.tv.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_BeforeLabelEdit);
            this.tv.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_AfterLabelEdit);
            this.tv.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
            this.tv.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterExpand);
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
            // 
            // mnuContextTreeView
            // 
            this.mnuContextTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewNew,
            this.mnuContextTreeViewSep1,
            this.mnuContextTreeViewProperties});
            this.mnuContextTreeView.Name = "mnuContextTreeView";
            this.mnuContextTreeView.Size = new System.Drawing.Size(194, 54);
            // 
            // mnuContextTreeViewNew
            // 
            this.mnuContextTreeViewNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewNewGroup,
            this.mnuContextTreeViewNewProperty});
            this.mnuContextTreeViewNew.Name = "mnuContextTreeViewNew";
            this.mnuContextTreeViewNew.Size = new System.Drawing.Size(193, 22);
            this.mnuContextTreeViewNew.Text = "Ne&w";
            // 
            // mnuContextTreeViewNewGroup
            // 
            this.mnuContextTreeViewNewGroup.Name = "mnuContextTreeViewNewGroup";
            this.mnuContextTreeViewNewGroup.ShortcutKeyDisplayString = "Shift+Ins";
            this.mnuContextTreeViewNewGroup.Size = new System.Drawing.Size(161, 22);
            this.mnuContextTreeViewNewGroup.Text = "&Group";
            this.mnuContextTreeViewNewGroup.Click += new System.EventHandler(this.mnuContextListViewNewGroup_Click);
            // 
            // mnuContextTreeViewNewProperty
            // 
            this.mnuContextTreeViewNewProperty.Name = "mnuContextTreeViewNewProperty";
            this.mnuContextTreeViewNewProperty.ShortcutKeyDisplayString = "Ins";
            this.mnuContextTreeViewNewProperty.Size = new System.Drawing.Size(161, 22);
            this.mnuContextTreeViewNewProperty.Text = "&Property";
            this.mnuContextTreeViewNewProperty.Click += new System.EventHandler(this.mnuContextListViewNewProperty_Click);
            // 
            // mnuContextTreeViewSep1
            // 
            this.mnuContextTreeViewSep1.Name = "mnuContextTreeViewSep1";
            this.mnuContextTreeViewSep1.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuContextTreeViewProperties
            // 
            this.mnuContextTreeViewProperties.Name = "mnuContextTreeViewProperties";
            this.mnuContextTreeViewProperties.ShortcutKeyDisplayString = "Alt+Enter";
            this.mnuContextTreeViewProperties.Size = new System.Drawing.Size(193, 22);
            this.mnuContextTreeViewProperties.Text = "P&roperties...";
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chValue,
            this.chType});
            this.lv.ContextMenuStrip = this.mnuContextListView;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(368, 318);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.ItemActivate += new System.EventHandler(this.lv_ItemActivate);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 160;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 111;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 87;
            // 
            // mnuContextListView
            // 
            this.mnuContextListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.mnuContextListViewSep1,
            this.selectAllToolStripMenuItem,
            this.invertSelectionToolStripMenuItem,
            this.mnuContextListViewSep2,
            this.mnuContextListViewNew,
            this.mnuContextListViewSep3,
            this.mnuContextListViewProperties});
            this.mnuContextListView.Name = "mnuContextListView";
            this.mnuContextListView.Size = new System.Drawing.Size(194, 198);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            // 
            // mnuContextListViewSep1
            // 
            this.mnuContextListViewSep1.Name = "mnuContextListViewSep1";
            this.mnuContextListViewSep1.Size = new System.Drawing.Size(190, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // invertSelectionToolStripMenuItem
            // 
            this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.invertSelectionToolStripMenuItem.Text = "&Invert Selection";
            // 
            // mnuContextListViewSep2
            // 
            this.mnuContextListViewSep2.Name = "mnuContextListViewSep2";
            this.mnuContextListViewSep2.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuContextListViewNew
            // 
            this.mnuContextListViewNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextListViewNewGroup,
            this.mnuContextListViewNewProperty});
            this.mnuContextListViewNew.Name = "mnuContextListViewNew";
            this.mnuContextListViewNew.Size = new System.Drawing.Size(193, 22);
            this.mnuContextListViewNew.Text = "Ne&w";
            // 
            // mnuContextListViewNewGroup
            // 
            this.mnuContextListViewNewGroup.Name = "mnuContextListViewNewGroup";
            this.mnuContextListViewNewGroup.ShortcutKeyDisplayString = "Shift+Ins";
            this.mnuContextListViewNewGroup.Size = new System.Drawing.Size(161, 22);
            this.mnuContextListViewNewGroup.Text = "&Group";
            this.mnuContextListViewNewGroup.Click += new System.EventHandler(this.mnuContextListViewNewGroup_Click);
            // 
            // mnuContextListViewNewProperty
            // 
            this.mnuContextListViewNewProperty.Name = "mnuContextListViewNewProperty";
            this.mnuContextListViewNewProperty.ShortcutKeyDisplayString = "Ins";
            this.mnuContextListViewNewProperty.Size = new System.Drawing.Size(161, 22);
            this.mnuContextListViewNewProperty.Text = "&Property";
            this.mnuContextListViewNewProperty.Click += new System.EventHandler(this.mnuContextListViewNewProperty_Click);
            // 
            // mnuContextListViewSep3
            // 
            this.mnuContextListViewSep3.Name = "mnuContextListViewSep3";
            this.mnuContextListViewSep3.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuContextListViewProperties
            // 
            this.mnuContextListViewProperties.Name = "mnuContextListViewProperties";
            this.mnuContextListViewProperties.ShortcutKeyDisplayString = "Alt+Enter";
            this.mnuContextListViewProperties.Size = new System.Drawing.Size(193, 22);
            this.mnuContextListViewProperties.Text = "P&roperties...";
            this.mnuContextListViewProperties.Click += new System.EventHandler(this.mnuContextListViewProperties_Click);
            // 
            // PropertyListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PropertyListEditor";
            this.Size = new System.Drawing.Size(557, 318);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.mnuContextTreeView.ResumeLayout(false);
            this.mnuContextListView.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chValue;
		private System.Windows.Forms.ColumnHeader chType;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextTreeView;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextListView;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewNew;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewNewGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewNewProperty;
		private System.Windows.Forms.ToolStripSeparator mnuContextListViewSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewProperties;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator mnuContextListViewSep2;
		private System.Windows.Forms.ToolStripSeparator mnuContextListViewSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNew;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewProperty;
		private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewProperties;
	}
}
