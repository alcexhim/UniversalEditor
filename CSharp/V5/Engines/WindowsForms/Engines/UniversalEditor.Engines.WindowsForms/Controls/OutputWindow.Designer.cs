namespace UniversalEditor.Engines.WindowsForms.Controls
{
	partial class OutputWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputWindow));
			this.cbToolBar1 = new AwesomeControls.CommandBars.CBToolBar();
			this.lblShowOutputFrom = new System.Windows.Forms.ToolStripLabel();
			this.cboShowOutputFrom = new System.Windows.Forms.ToolStripComboBox();
			this.tsbSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbClearAllPanes = new System.Windows.Forms.ToolStripButton();
			this.tsbSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbFindMessageInDocument = new System.Windows.Forms.ToolStripButton();
			this.tsbSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbGoToPreviousMessage = new System.Windows.Forms.ToolStripButton();
			this.tsbGoToNextMessage = new System.Windows.Forms.ToolStripButton();
			this.tsbSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbClearAll = new System.Windows.Forms.ToolStripButton();
			this.tsbSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbToggleWordWrap = new System.Windows.Forms.ToolStripButton();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.cbToolBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbToolBar1
			// 
			this.cbToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblShowOutputFrom,
            this.cboShowOutputFrom,
            this.tsbSep1,
            this.tsbClearAllPanes,
            this.tsbSep2,
            this.tsbFindMessageInDocument,
            this.tsbSep3,
            this.tsbGoToPreviousMessage,
            this.tsbGoToNextMessage,
            this.tsbSep4,
            this.tsbClearAll,
            this.tsbSep5,
            this.tsbToggleWordWrap});
			this.cbToolBar1.Location = new System.Drawing.Point(0, 0);
			this.cbToolBar1.Name = "cbToolBar1";
			this.cbToolBar1.Size = new System.Drawing.Size(551, 25);
			this.cbToolBar1.TabIndex = 0;
			this.cbToolBar1.Text = "cbToolBar1";
			// 
			// lblShowOutputFrom
			// 
			this.lblShowOutputFrom.Name = "lblShowOutputFrom";
			this.lblShowOutputFrom.Size = new System.Drawing.Size(107, 22);
			this.lblShowOutputFrom.Text = "&Show output from:";
			// 
			// cboShowOutputFrom
			// 
			this.cboShowOutputFrom.Items.AddRange(new object[] {
            "Build",
            "Build Order",
            "Debug",
            "Refactor",
            "Project Management"});
			this.cboShowOutputFrom.Name = "cboShowOutputFrom";
			this.cboShowOutputFrom.Size = new System.Drawing.Size(121, 25);
			// 
			// tsbSep1
			// 
			this.tsbSep1.Name = "tsbSep1";
			this.tsbSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbClearAllPanes
			// 
			this.tsbClearAllPanes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClearAllPanes.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearAllPanes.Image")));
			this.tsbClearAllPanes.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbClearAllPanes.Name = "tsbClearAllPanes";
			this.tsbClearAllPanes.Size = new System.Drawing.Size(23, 22);
			this.tsbClearAllPanes.Text = "Clear All Panes";
			this.tsbClearAllPanes.Visible = false;
			// 
			// tsbSep2
			// 
			this.tsbSep2.Name = "tsbSep2";
			this.tsbSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbFindMessageInDocument
			// 
			this.tsbFindMessageInDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFindMessageInDocument.Image = ((System.Drawing.Image)(resources.GetObject("tsbFindMessageInDocument.Image")));
			this.tsbFindMessageInDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFindMessageInDocument.Name = "tsbFindMessageInDocument";
			this.tsbFindMessageInDocument.Size = new System.Drawing.Size(23, 22);
			this.tsbFindMessageInDocument.Text = "Find Message in Document";
			// 
			// tsbSep3
			// 
			this.tsbSep3.Name = "tsbSep3";
			this.tsbSep3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbGoToPreviousMessage
			// 
			this.tsbGoToPreviousMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbGoToPreviousMessage.Image = ((System.Drawing.Image)(resources.GetObject("tsbGoToPreviousMessage.Image")));
			this.tsbGoToPreviousMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbGoToPreviousMessage.Name = "tsbGoToPreviousMessage";
			this.tsbGoToPreviousMessage.Size = new System.Drawing.Size(23, 22);
			this.tsbGoToPreviousMessage.Text = "Go to Previous Message";
			// 
			// tsbGoToNextMessage
			// 
			this.tsbGoToNextMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbGoToNextMessage.Image = ((System.Drawing.Image)(resources.GetObject("tsbGoToNextMessage.Image")));
			this.tsbGoToNextMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbGoToNextMessage.Name = "tsbGoToNextMessage";
			this.tsbGoToNextMessage.Size = new System.Drawing.Size(23, 22);
			this.tsbGoToNextMessage.Text = "Go to Next Message";
			// 
			// tsbSep4
			// 
			this.tsbSep4.Name = "tsbSep4";
			this.tsbSep4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbClearAll
			// 
			this.tsbClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearAll.Image")));
			this.tsbClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbClearAll.Name = "tsbClearAll";
			this.tsbClearAll.Size = new System.Drawing.Size(23, 22);
			this.tsbClearAll.Text = "Clear All";
			// 
			// tsbSep5
			// 
			this.tsbSep5.Name = "tsbSep5";
			this.tsbSep5.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbToggleWordWrap
			// 
			this.tsbToggleWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbToggleWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("tsbToggleWordWrap.Image")));
			this.tsbToggleWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbToggleWordWrap.Name = "tsbToggleWordWrap";
			this.tsbToggleWordWrap.Size = new System.Drawing.Size(23, 22);
			this.tsbToggleWordWrap.Text = "Toggle Word Wrap";
			// 
			// txtOutput
			// 
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(0, 25);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(551, 228);
			this.txtOutput.TabIndex = 1;
			// 
			// OutputWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.cbToolBar1);
			this.Name = "OutputWindow";
			this.Size = new System.Drawing.Size(551, 253);
			this.cbToolBar1.ResumeLayout(false);
			this.cbToolBar1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AwesomeControls.CommandBars.CBToolBar cbToolBar1;
		private System.Windows.Forms.ToolStripLabel lblShowOutputFrom;
		private System.Windows.Forms.ToolStripComboBox cboShowOutputFrom;
		private System.Windows.Forms.ToolStripSeparator tsbSep1;
		private System.Windows.Forms.ToolStripButton tsbClearAllPanes;
		private System.Windows.Forms.ToolStripSeparator tsbSep2;
		private System.Windows.Forms.ToolStripButton tsbFindMessageInDocument;
		private System.Windows.Forms.ToolStripSeparator tsbSep3;
		private System.Windows.Forms.ToolStripButton tsbGoToPreviousMessage;
		private System.Windows.Forms.ToolStripButton tsbGoToNextMessage;
		private System.Windows.Forms.ToolStripSeparator tsbSep4;
		private System.Windows.Forms.ToolStripButton tsbClearAll;
		private System.Windows.Forms.ToolStripSeparator tsbSep5;
		private System.Windows.Forms.ToolStripButton tsbToggleWordWrap;
		private System.Windows.Forms.TextBox txtOutput;
	}
}
