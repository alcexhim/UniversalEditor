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
			this.pnlSections = new System.Windows.Forms.Panel();
			this.tv = new System.Windows.Forms.TreeView();
			this.lvSections = new System.Windows.Forms.ListView();
			this.chSectionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSectionOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSectionLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mnuContextListViewSections = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextListViewSectionsExport = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextListViewSectionsImport = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlSections.SuspendLayout();
			this.mnuContextListViewSections.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.pnlSections);
			this.splitContainer1.Size = new System.Drawing.Size(502, 247);
			this.splitContainer1.SplitterDistance = 167;
			this.splitContainer1.TabIndex = 0;
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
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(167, 247);
			this.tv.TabIndex = 0;
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
            this.mnuContextListViewSectionsImport,
            this.mnuContextListViewSectionsExport});
			this.mnuContextListViewSections.Name = "mnuContextListViewSections";
			this.mnuContextListViewSections.Size = new System.Drawing.Size(153, 70);
			this.mnuContextListViewSections.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextListViewSections_Opening);
			// 
			// mnuContextListViewSectionsExport
			// 
			this.mnuContextListViewSectionsExport.Enabled = false;
			this.mnuContextListViewSectionsExport.Name = "mnuContextListViewSectionsExport";
			this.mnuContextListViewSectionsExport.Size = new System.Drawing.Size(152, 22);
			this.mnuContextListViewSectionsExport.Text = "&Export...";
			this.mnuContextListViewSectionsExport.Click += new System.EventHandler(this.mnuContextListViewSectionsExport_Click);
			// 
			// mnuContextListViewSectionsImport
			// 
			this.mnuContextListViewSectionsImport.Name = "mnuContextListViewSectionsImport";
			this.mnuContextListViewSectionsImport.Size = new System.Drawing.Size(152, 22);
			this.mnuContextListViewSectionsImport.Text = "&Import...";
			this.mnuContextListViewSectionsImport.Click += new System.EventHandler(this.mnuContextListViewSectionsImport_Click);
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
    }
}
