namespace UniversalEditor.Editors.UnrealEngine
{
    partial class UnrealPackageEditor
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
			this.tv = new System.Windows.Forms.TreeView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pnlExportTable = new System.Windows.Forms.Panel();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.lvExportTable = new System.Windows.Forms.ListView();
			this.chExportObjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chExportObjectParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chExportObjectClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chExportObjectFlags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chExportObjectOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chExportObjectSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlHeritageTable = new System.Windows.Forms.Panel();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.lvHeritageTable = new System.Windows.Forms.ListView();
			this.chHeritageGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlImportTable = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.lvImportTable = new System.Windows.Forms.ListView();
			this.chImportPackageName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chImportObjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chImportClassName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlNameTable = new System.Windows.Forms.Panel();
			this.cmdNameClear = new System.Windows.Forms.Button();
			this.cmdNameRemove = new System.Windows.Forms.Button();
			this.cmdNameModify = new System.Windows.Forms.Button();
			this.cmdNameAdd = new System.Windows.Forms.Button();
			this.lvNameTable = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chFlags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mnuContextExportTable = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuExportTableCopyTo = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlExportTable.SuspendLayout();
			this.pnlHeritageTable.SuspendLayout();
			this.pnlImportTable.SuspendLayout();
			this.pnlNameTable.SuspendLayout();
			this.mnuContextExportTable.SuspendLayout();
			this.SuspendLayout();
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(170, 267);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
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
			this.splitContainer1.Panel2.Controls.Add(this.pnlExportTable);
			this.splitContainer1.Panel2.Controls.Add(this.pnlHeritageTable);
			this.splitContainer1.Panel2.Controls.Add(this.pnlImportTable);
			this.splitContainer1.Panel2.Controls.Add(this.pnlNameTable);
			this.splitContainer1.Size = new System.Drawing.Size(558, 267);
			this.splitContainer1.SplitterDistance = 170;
			this.splitContainer1.TabIndex = 1;
			// 
			// pnlExportTable
			// 
			this.pnlExportTable.Controls.Add(this.button5);
			this.pnlExportTable.Controls.Add(this.button6);
			this.pnlExportTable.Controls.Add(this.button7);
			this.pnlExportTable.Controls.Add(this.button8);
			this.pnlExportTable.Controls.Add(this.lvExportTable);
			this.pnlExportTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExportTable.Enabled = false;
			this.pnlExportTable.Location = new System.Drawing.Point(0, 0);
			this.pnlExportTable.Name = "pnlExportTable";
			this.pnlExportTable.Size = new System.Drawing.Size(384, 267);
			this.pnlExportTable.TabIndex = 2;
			this.pnlExportTable.Visible = false;
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button5.Location = new System.Drawing.Point(306, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 1;
			this.button5.Text = "&Clear";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button6.Location = new System.Drawing.Point(165, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 23);
			this.button6.TabIndex = 1;
			this.button6.Text = "&Remove...";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button7.Location = new System.Drawing.Point(84, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(75, 23);
			this.button7.TabIndex = 1;
			this.button7.Text = "&Modify...";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// button8
			// 
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button8.Location = new System.Drawing.Point(3, 3);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(75, 23);
			this.button8.TabIndex = 1;
			this.button8.Text = "&Add...";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// lvExportTable
			// 
			this.lvExportTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvExportTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExportObjectName,
            this.chExportObjectParent,
            this.chExportObjectClass,
            this.chExportObjectFlags,
            this.chExportObjectOffset,
            this.chExportObjectSize});
			this.lvExportTable.ContextMenuStrip = this.mnuContextExportTable;
			this.lvExportTable.FullRowSelect = true;
			this.lvExportTable.GridLines = true;
			this.lvExportTable.HideSelection = false;
			this.lvExportTable.Location = new System.Drawing.Point(3, 32);
			this.lvExportTable.Name = "lvExportTable";
			this.lvExportTable.Size = new System.Drawing.Size(378, 232);
			this.lvExportTable.TabIndex = 0;
			this.lvExportTable.UseCompatibleStateImageBehavior = false;
			this.lvExportTable.View = System.Windows.Forms.View.Details;
			this.lvExportTable.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvExportTable_ItemDrag);
			// 
			// chExportObjectName
			// 
			this.chExportObjectName.Text = "Object";
			this.chExportObjectName.Width = 84;
			// 
			// chExportObjectParent
			// 
			this.chExportObjectParent.Text = "Parent";
			this.chExportObjectParent.Width = 53;
			// 
			// chExportObjectClass
			// 
			this.chExportObjectClass.Text = "Class";
			this.chExportObjectClass.Width = 81;
			// 
			// chExportObjectFlags
			// 
			this.chExportObjectFlags.Text = "Flags";
			this.chExportObjectFlags.Width = 49;
			// 
			// chExportObjectOffset
			// 
			this.chExportObjectOffset.Text = "Offset";
			this.chExportObjectOffset.Width = 45;
			// 
			// chExportObjectSize
			// 
			this.chExportObjectSize.Text = "Size";
			// 
			// pnlHeritageTable
			// 
			this.pnlHeritageTable.Controls.Add(this.button9);
			this.pnlHeritageTable.Controls.Add(this.button10);
			this.pnlHeritageTable.Controls.Add(this.button11);
			this.pnlHeritageTable.Controls.Add(this.button12);
			this.pnlHeritageTable.Controls.Add(this.lvHeritageTable);
			this.pnlHeritageTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeritageTable.Enabled = false;
			this.pnlHeritageTable.Location = new System.Drawing.Point(0, 0);
			this.pnlHeritageTable.Name = "pnlHeritageTable";
			this.pnlHeritageTable.Size = new System.Drawing.Size(384, 267);
			this.pnlHeritageTable.TabIndex = 3;
			this.pnlHeritageTable.Visible = false;
			// 
			// button9
			// 
			this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button9.Location = new System.Drawing.Point(306, 3);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(75, 23);
			this.button9.TabIndex = 1;
			this.button9.Text = "&Clear";
			this.button9.UseVisualStyleBackColor = true;
			// 
			// button10
			// 
			this.button10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button10.Location = new System.Drawing.Point(165, 3);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(75, 23);
			this.button10.TabIndex = 1;
			this.button10.Text = "&Remove...";
			this.button10.UseVisualStyleBackColor = true;
			// 
			// button11
			// 
			this.button11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button11.Location = new System.Drawing.Point(84, 3);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(75, 23);
			this.button11.TabIndex = 1;
			this.button11.Text = "&Modify...";
			this.button11.UseVisualStyleBackColor = true;
			// 
			// button12
			// 
			this.button12.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button12.Location = new System.Drawing.Point(3, 3);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(75, 23);
			this.button12.TabIndex = 1;
			this.button12.Text = "&Add...";
			this.button12.UseVisualStyleBackColor = true;
			// 
			// lvHeritageTable
			// 
			this.lvHeritageTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvHeritageTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHeritageGUID});
			this.lvHeritageTable.FullRowSelect = true;
			this.lvHeritageTable.GridLines = true;
			this.lvHeritageTable.HideSelection = false;
			this.lvHeritageTable.Location = new System.Drawing.Point(3, 32);
			this.lvHeritageTable.Name = "lvHeritageTable";
			this.lvHeritageTable.Size = new System.Drawing.Size(378, 232);
			this.lvHeritageTable.TabIndex = 0;
			this.lvHeritageTable.UseCompatibleStateImageBehavior = false;
			this.lvHeritageTable.View = System.Windows.Forms.View.Details;
			// 
			// chHeritageGUID
			// 
			this.chHeritageGUID.Text = "GUID";
			this.chHeritageGUID.Width = 354;
			// 
			// pnlImportTable
			// 
			this.pnlImportTable.Controls.Add(this.button1);
			this.pnlImportTable.Controls.Add(this.button2);
			this.pnlImportTable.Controls.Add(this.button3);
			this.pnlImportTable.Controls.Add(this.button4);
			this.pnlImportTable.Controls.Add(this.lvImportTable);
			this.pnlImportTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlImportTable.Enabled = false;
			this.pnlImportTable.Location = new System.Drawing.Point(0, 0);
			this.pnlImportTable.Name = "pnlImportTable";
			this.pnlImportTable.Size = new System.Drawing.Size(384, 267);
			this.pnlImportTable.TabIndex = 1;
			this.pnlImportTable.Visible = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(306, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "&Clear";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(165, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "&Remove...";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button3.Location = new System.Drawing.Point(84, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "&Modify...";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button4.Location = new System.Drawing.Point(3, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 1;
			this.button4.Text = "&Add...";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// lvImportTable
			// 
			this.lvImportTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvImportTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chImportPackageName,
            this.chImportObjectName,
            this.chImportClassName});
			this.lvImportTable.FullRowSelect = true;
			this.lvImportTable.GridLines = true;
			this.lvImportTable.HideSelection = false;
			this.lvImportTable.Location = new System.Drawing.Point(3, 32);
			this.lvImportTable.Name = "lvImportTable";
			this.lvImportTable.Size = new System.Drawing.Size(378, 232);
			this.lvImportTable.TabIndex = 0;
			this.lvImportTable.UseCompatibleStateImageBehavior = false;
			this.lvImportTable.View = System.Windows.Forms.View.Details;
			// 
			// chImportPackageName
			// 
			this.chImportPackageName.Text = "Package";
			this.chImportPackageName.Width = 97;
			// 
			// chImportObjectName
			// 
			this.chImportObjectName.Text = "Object";
			this.chImportObjectName.Width = 134;
			// 
			// chImportClassName
			// 
			this.chImportClassName.Text = "Class";
			this.chImportClassName.Width = 140;
			// 
			// pnlNameTable
			// 
			this.pnlNameTable.Controls.Add(this.cmdNameClear);
			this.pnlNameTable.Controls.Add(this.cmdNameRemove);
			this.pnlNameTable.Controls.Add(this.cmdNameModify);
			this.pnlNameTable.Controls.Add(this.cmdNameAdd);
			this.pnlNameTable.Controls.Add(this.lvNameTable);
			this.pnlNameTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlNameTable.Enabled = false;
			this.pnlNameTable.Location = new System.Drawing.Point(0, 0);
			this.pnlNameTable.Name = "pnlNameTable";
			this.pnlNameTable.Size = new System.Drawing.Size(384, 267);
			this.pnlNameTable.TabIndex = 0;
			this.pnlNameTable.Visible = false;
			// 
			// cmdNameClear
			// 
			this.cmdNameClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNameClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNameClear.Location = new System.Drawing.Point(306, 3);
			this.cmdNameClear.Name = "cmdNameClear";
			this.cmdNameClear.Size = new System.Drawing.Size(75, 23);
			this.cmdNameClear.TabIndex = 1;
			this.cmdNameClear.Text = "&Clear";
			this.cmdNameClear.UseVisualStyleBackColor = true;
			// 
			// cmdNameRemove
			// 
			this.cmdNameRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNameRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdNameRemove.Name = "cmdNameRemove";
			this.cmdNameRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdNameRemove.TabIndex = 1;
			this.cmdNameRemove.Text = "&Remove...";
			this.cmdNameRemove.UseVisualStyleBackColor = true;
			// 
			// cmdNameModify
			// 
			this.cmdNameModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNameModify.Location = new System.Drawing.Point(84, 3);
			this.cmdNameModify.Name = "cmdNameModify";
			this.cmdNameModify.Size = new System.Drawing.Size(75, 23);
			this.cmdNameModify.TabIndex = 1;
			this.cmdNameModify.Text = "&Modify...";
			this.cmdNameModify.UseVisualStyleBackColor = true;
			// 
			// cmdNameAdd
			// 
			this.cmdNameAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdNameAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdNameAdd.Name = "cmdNameAdd";
			this.cmdNameAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdNameAdd.TabIndex = 1;
			this.cmdNameAdd.Text = "&Add...";
			this.cmdNameAdd.UseVisualStyleBackColor = true;
			// 
			// lvNameTable
			// 
			this.lvNameTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvNameTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chFlags});
			this.lvNameTable.FullRowSelect = true;
			this.lvNameTable.GridLines = true;
			this.lvNameTable.HideSelection = false;
			this.lvNameTable.Location = new System.Drawing.Point(3, 32);
			this.lvNameTable.Name = "lvNameTable";
			this.lvNameTable.Size = new System.Drawing.Size(378, 232);
			this.lvNameTable.TabIndex = 0;
			this.lvNameTable.UseCompatibleStateImageBehavior = false;
			this.lvNameTable.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 231;
			// 
			// chFlags
			// 
			this.chFlags.Text = "Flags";
			this.chFlags.Width = 134;
			// 
			// mnuContextExportTable
			// 
			this.mnuContextExportTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportTableCopyTo});
			this.mnuContextExportTable.Name = "mnuContextExportTable";
			this.mnuContextExportTable.Size = new System.Drawing.Size(153, 48);
			// 
			// mnuExportTableCopyTo
			// 
			this.mnuExportTableCopyTo.Name = "mnuExportTableCopyTo";
			this.mnuExportTableCopyTo.Size = new System.Drawing.Size(152, 22);
			this.mnuExportTableCopyTo.Text = "&Copy To...";
			this.mnuExportTableCopyTo.Click += new System.EventHandler(this.mnuExportTableCopyTo_Click);
			// 
			// UnrealPackageEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "UnrealPackageEditor";
			this.Size = new System.Drawing.Size(558, 267);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.pnlExportTable.ResumeLayout(false);
			this.pnlHeritageTable.ResumeLayout(false);
			this.pnlImportTable.ResumeLayout(false);
			this.pnlNameTable.ResumeLayout(false);
			this.mnuContextExportTable.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlNameTable;
        private System.Windows.Forms.ListView lvNameTable;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chFlags;
        private System.Windows.Forms.Button cmdNameClear;
        private System.Windows.Forms.Button cmdNameRemove;
        private System.Windows.Forms.Button cmdNameModify;
        private System.Windows.Forms.Button cmdNameAdd;
        private System.Windows.Forms.Panel pnlImportTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView lvImportTable;
        private System.Windows.Forms.ColumnHeader chImportPackageName;
        private System.Windows.Forms.ColumnHeader chImportObjectName;
        private System.Windows.Forms.ColumnHeader chImportClassName;
        private System.Windows.Forms.Panel pnlExportTable;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListView lvExportTable;
        private System.Windows.Forms.ColumnHeader chExportObjectParent;
        private System.Windows.Forms.ColumnHeader chExportObjectClass;
        private System.Windows.Forms.ColumnHeader chExportObjectName;
        private System.Windows.Forms.ColumnHeader chExportObjectFlags;
        private System.Windows.Forms.ColumnHeader chExportObjectOffset;
        private System.Windows.Forms.ColumnHeader chExportObjectSize;
        private System.Windows.Forms.Panel pnlHeritageTable;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.ListView lvHeritageTable;
        private System.Windows.Forms.ColumnHeader chHeritageGUID;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextExportTable;
		private System.Windows.Forms.ToolStripMenuItem mnuExportTableCopyTo;
    }
}
