namespace UniversalEditor.Editors
{
	partial class CodeEditor
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
			this.tvExplorer = new System.Windows.Forms.TreeView();
			this.lvEnum = new System.Windows.Forms.ListView();
			this.chEnumValueName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chEnumValueValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvExplorer = new System.Windows.Forms.ListView();
			this.mnuContextTreeView = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewClass = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewInterface = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewNamespace = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewMethod = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewMethodCall = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewNewVariable = new System.Windows.Forms.ToolStripMenuItem();
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
			this.splitContainer1.Panel1.Controls.Add(this.tvExplorer);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lvEnum);
			this.splitContainer1.Panel2.Controls.Add(this.lvExplorer);
			this.splitContainer1.Size = new System.Drawing.Size(568, 387);
			this.splitContainer1.SplitterDistance = 189;
			this.splitContainer1.TabIndex = 1;
			// 
			// tvExplorer
			// 
			this.tvExplorer.ContextMenuStrip = this.mnuContextTreeView;
			this.tvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExplorer.HideSelection = false;
			this.tvExplorer.Location = new System.Drawing.Point(0, 0);
			this.tvExplorer.Name = "tvExplorer";
			this.tvExplorer.Size = new System.Drawing.Size(189, 387);
			this.tvExplorer.TabIndex = 0;
			this.tvExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExplorer_AfterSelect);
			// 
			// lvEnum
			// 
			this.lvEnum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEnumValueName,
            this.chEnumValueValue});
			this.lvEnum.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvEnum.FullRowSelect = true;
			this.lvEnum.GridLines = true;
			this.lvEnum.HideSelection = false;
			this.lvEnum.Location = new System.Drawing.Point(0, 0);
			this.lvEnum.Name = "lvEnum";
			this.lvEnum.Size = new System.Drawing.Size(375, 387);
			this.lvEnum.TabIndex = 1;
			this.lvEnum.UseCompatibleStateImageBehavior = false;
			this.lvEnum.View = System.Windows.Forms.View.Details;
			this.lvEnum.Visible = false;
			// 
			// chEnumValueName
			// 
			this.chEnumValueName.Text = "Name";
			// 
			// chEnumValueValue
			// 
			this.chEnumValueValue.Text = "Value";
			// 
			// lvExplorer
			// 
			this.lvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvExplorer.FullRowSelect = true;
			this.lvExplorer.GridLines = true;
			this.lvExplorer.HideSelection = false;
			this.lvExplorer.Location = new System.Drawing.Point(0, 0);
			this.lvExplorer.Name = "lvExplorer";
			this.lvExplorer.Size = new System.Drawing.Size(375, 387);
			this.lvExplorer.TabIndex = 0;
			this.lvExplorer.UseCompatibleStateImageBehavior = false;
			this.lvExplorer.Visible = false;
			// 
			// mnuContextTreeView
			// 
			this.mnuContextTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
			this.mnuContextTreeView.Name = "mnuContextTreeView";
			this.mnuContextTreeView.Size = new System.Drawing.Size(96, 26);
			this.mnuContextTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextTreeView_Opening);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewNewNamespace,
            this.mnuContextTreeViewNewClass,
            this.mnuContextTreeViewNewInterface,
            this.mnuContextTreeViewNewMethod,
            this.mnuContextTreeViewNewMethodCall,
            this.mnuContextTreeViewNewVariable});
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
			this.newToolStripMenuItem.Text = "Ne&w";
			// 
			// mnuContextTreeViewNewClass
			// 
			this.mnuContextTreeViewNewClass.Name = "mnuContextTreeViewNewClass";
			this.mnuContextTreeViewNewClass.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewClass.Text = "&Class";
			this.mnuContextTreeViewNewClass.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// mnuContextTreeViewNewInterface
			// 
			this.mnuContextTreeViewNewInterface.Name = "mnuContextTreeViewNewInterface";
			this.mnuContextTreeViewNewInterface.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewInterface.Text = "&Interface";
			this.mnuContextTreeViewNewInterface.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// mnuContextTreeViewNewNamespace
			// 
			this.mnuContextTreeViewNewNamespace.Name = "mnuContextTreeViewNewNamespace";
			this.mnuContextTreeViewNewNamespace.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewNamespace.Text = "&Namespace";
			this.mnuContextTreeViewNewNamespace.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// mnuContextTreeViewNewMethod
			// 
			this.mnuContextTreeViewNewMethod.Name = "mnuContextTreeViewNewMethod";
			this.mnuContextTreeViewNewMethod.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewMethod.Text = "&Method";
			this.mnuContextTreeViewNewMethod.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// mnuContextTreeViewNewMethodCall
			// 
			this.mnuContextTreeViewNewMethodCall.Name = "mnuContextTreeViewNewMethodCall";
			this.mnuContextTreeViewNewMethodCall.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewMethodCall.Text = "Method &Call";
			this.mnuContextTreeViewNewMethodCall.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// mnuContextTreeViewNewVariable
			// 
			this.mnuContextTreeViewNewVariable.Name = "mnuContextTreeViewNewVariable";
			this.mnuContextTreeViewNewVariable.Size = new System.Drawing.Size(152, 22);
			this.mnuContextTreeViewNewVariable.Text = "&Variable";
			this.mnuContextTreeViewNewVariable.Click += new System.EventHandler(this.mnuContextTreeViewNew_Click);
			// 
			// CodeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "CodeEditor";
			this.Size = new System.Drawing.Size(568, 387);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.mnuContextTreeView.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tvExplorer;
		private System.Windows.Forms.ListView lvExplorer;
		private System.Windows.Forms.ListView lvEnum;
		private System.Windows.Forms.ColumnHeader chEnumValueName;
		private System.Windows.Forms.ColumnHeader chEnumValueValue;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextTreeView;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewNamespace;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewClass;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewInterface;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewMethod;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewMethodCall;
		private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewNewVariable;
	}
}
