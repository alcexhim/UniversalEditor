namespace UniversalEditor.Editors.Executable
{
    partial class ExecutableEditor
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
			this.pnlSections = new System.Windows.Forms.Panel();
			this.lvSections = new System.Windows.Forms.ListView();
			this.chSectionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSectionOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSectionLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mnuContextListViewSections = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextListViewSectionsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextListViewSectionsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextListViewSectionsImport = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextListViewSectionsExport = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlSection = new System.Windows.Forms.Panel();
			this.lblSectionName = new System.Windows.Forms.Label();
			this.txtSectionName = new System.Windows.Forms.TextBox();
			this.fraSectionCharacteristics = new System.Windows.Forms.GroupBox();
			this.lvSectionCharacteristics = new System.Windows.Forms.ListView();
			this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlSections.SuspendLayout();
			this.mnuContextListViewSections.SuspendLayout();
			this.pnlSection.SuspendLayout();
			this.fraSectionCharacteristics.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.pnlSection);
			this.splitContainer1.Panel2.Controls.Add(this.pnlSections);
			this.splitContainer1.Size = new System.Drawing.Size(502, 247);
			this.splitContainer1.SplitterDistance = 167;
			this.splitContainer1.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(167, 247);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// pnlSections
			// 
			this.pnlSections.Controls.Add(this.lvSections);
			this.pnlSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSections.Location = new System.Drawing.Point(0, 0);
			this.pnlSections.Name = "pnlSections";
			this.pnlSections.Size = new System.Drawing.Size(331, 247);
			this.pnlSections.TabIndex = 0;
			// 
			// lvSections
			// 
			this.lvSections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSectionName,
            this.chSectionOffset,
            this.chSectionLength});
			this.lvSections.ContextMenuStrip = this.mnuContextListViewSections;
			this.lvSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSections.FullRowSelect = true;
			this.lvSections.GridLines = true;
			this.lvSections.HideSelection = false;
			this.lvSections.Location = new System.Drawing.Point(0, 0);
			this.lvSections.Name = "lvSections";
			this.lvSections.Size = new System.Drawing.Size(331, 247);
			this.lvSections.TabIndex = 0;
			this.lvSections.UseCompatibleStateImageBehavior = false;
			this.lvSections.View = System.Windows.Forms.View.Details;
			// 
			// chSectionName
			// 
			this.chSectionName.Text = "Name";
			this.chSectionName.Width = 191;
			// 
			// chSectionOffset
			// 
			this.chSectionOffset.Text = "Offset";
			// 
			// chSectionLength
			// 
			this.chSectionLength.Text = "Length";
			// 
			// mnuContextListViewSections
			// 
			this.mnuContextListViewSections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextListViewSectionsDelete,
            this.mnuContextListViewSectionsSep1,
            this.mnuContextListViewSectionsImport,
            this.mnuContextListViewSectionsExport});
			this.mnuContextListViewSections.Name = "mnuContextListViewSections";
			this.mnuContextListViewSections.Size = new System.Drawing.Size(119, 76);
			this.mnuContextListViewSections.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextListViewSections_Opening);
			// 
			// mnuContextListViewSectionsDelete
			// 
			this.mnuContextListViewSectionsDelete.Name = "mnuContextListViewSectionsDelete";
			this.mnuContextListViewSectionsDelete.Size = new System.Drawing.Size(118, 22);
			this.mnuContextListViewSectionsDelete.Text = "&Delete";
			this.mnuContextListViewSectionsDelete.Click += new System.EventHandler(this.mnuContextListViewSectionsDelete_Click);
			// 
			// mnuContextListViewSectionsSep1
			// 
			this.mnuContextListViewSectionsSep1.Name = "mnuContextListViewSectionsSep1";
			this.mnuContextListViewSectionsSep1.Size = new System.Drawing.Size(115, 6);
			// 
			// mnuContextListViewSectionsImport
			// 
			this.mnuContextListViewSectionsImport.Name = "mnuContextListViewSectionsImport";
			this.mnuContextListViewSectionsImport.Size = new System.Drawing.Size(118, 22);
			this.mnuContextListViewSectionsImport.Text = "&Import...";
			this.mnuContextListViewSectionsImport.Click += new System.EventHandler(this.mnuContextListViewSectionsImport_Click);
			// 
			// mnuContextListViewSectionsExport
			// 
			this.mnuContextListViewSectionsExport.Enabled = false;
			this.mnuContextListViewSectionsExport.Name = "mnuContextListViewSectionsExport";
			this.mnuContextListViewSectionsExport.Size = new System.Drawing.Size(118, 22);
			this.mnuContextListViewSectionsExport.Text = "&Export...";
			this.mnuContextListViewSectionsExport.Click += new System.EventHandler(this.mnuContextListViewSectionsExport_Click);
			// 
			// pnlSection
			// 
			this.pnlSection.Controls.Add(this.fraSectionCharacteristics);
			this.pnlSection.Controls.Add(this.txtSectionName);
			this.pnlSection.Controls.Add(this.lblSectionName);
			this.pnlSection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSection.Location = new System.Drawing.Point(0, 0);
			this.pnlSection.Name = "pnlSection";
			this.pnlSection.Size = new System.Drawing.Size(331, 247);
			this.pnlSection.TabIndex = 1;
			// 
			// lblSectionName
			// 
			this.lblSectionName.AutoSize = true;
			this.lblSectionName.Location = new System.Drawing.Point(3, 6);
			this.lblSectionName.Name = "lblSectionName";
			this.lblSectionName.Size = new System.Drawing.Size(75, 13);
			this.lblSectionName.TabIndex = 0;
			this.lblSectionName.Text = "Section &name:";
			// 
			// txtSectionName
			// 
			this.txtSectionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSectionName.Location = new System.Drawing.Point(84, 3);
			this.txtSectionName.Name = "txtSectionName";
			this.txtSectionName.Size = new System.Drawing.Size(244, 20);
			this.txtSectionName.TabIndex = 1;
			// 
			// fraSectionCharacteristics
			// 
			this.fraSectionCharacteristics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraSectionCharacteristics.Controls.Add(this.lvSectionCharacteristics);
			this.fraSectionCharacteristics.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraSectionCharacteristics.Location = new System.Drawing.Point(3, 29);
			this.fraSectionCharacteristics.Name = "fraSectionCharacteristics";
			this.fraSectionCharacteristics.Size = new System.Drawing.Size(325, 161);
			this.fraSectionCharacteristics.TabIndex = 2;
			this.fraSectionCharacteristics.TabStop = false;
			this.fraSectionCharacteristics.Text = "Characteristics";
			// 
			// lvSectionCharacteristics
			// 
			this.lvSectionCharacteristics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvSectionCharacteristics.CheckBoxes = true;
			this.lvSectionCharacteristics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTitle});
			this.lvSectionCharacteristics.FullRowSelect = true;
			this.lvSectionCharacteristics.GridLines = true;
			this.lvSectionCharacteristics.HideSelection = false;
			this.lvSectionCharacteristics.Location = new System.Drawing.Point(6, 19);
			this.lvSectionCharacteristics.Name = "lvSectionCharacteristics";
			this.lvSectionCharacteristics.Size = new System.Drawing.Size(313, 136);
			this.lvSectionCharacteristics.TabIndex = 0;
			this.lvSectionCharacteristics.UseCompatibleStateImageBehavior = false;
			this.lvSectionCharacteristics.View = System.Windows.Forms.View.Details;
			// 
			// chTitle
			// 
			this.chTitle.Text = "Characteristic";
			this.chTitle.Width = 300;
			// 
			// ExecutableEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ExecutableEditor";
			this.Size = new System.Drawing.Size(502, 247);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.pnlSections.ResumeLayout(false);
			this.mnuContextListViewSections.ResumeLayout(false);
			this.pnlSection.ResumeLayout(false);
			this.pnlSection.PerformLayout();
			this.fraSectionCharacteristics.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Panel pnlSections;
		private System.Windows.Forms.ListView lvSections;
		private System.Windows.Forms.ColumnHeader chSectionName;
		private System.Windows.Forms.ColumnHeader chSectionOffset;
		private System.Windows.Forms.ColumnHeader chSectionLength;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextListViewSections;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewSectionsExport;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewSectionsImport;
		private System.Windows.Forms.ToolStripMenuItem mnuContextListViewSectionsDelete;
		private System.Windows.Forms.ToolStripSeparator mnuContextListViewSectionsSep1;
		private System.Windows.Forms.Panel pnlSection;
		private System.Windows.Forms.TextBox txtSectionName;
		private System.Windows.Forms.Label lblSectionName;
		private System.Windows.Forms.GroupBox fraSectionCharacteristics;
		private System.Windows.Forms.ListView lvSectionCharacteristics;
		private System.Windows.Forms.ColumnHeader chTitle;
    }
}
