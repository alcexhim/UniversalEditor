namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll
{
    partial class PianoRollControl
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
			this.txtLyric = new System.Windows.Forms.TextBox();
			this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLyric
			// 
			this.txtLyric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLyric.Enabled = false;
			this.txtLyric.Location = new System.Drawing.Point(16, 45);
			this.txtLyric.Name = "txtLyric";
			this.txtLyric.Size = new System.Drawing.Size(71, 20);
			this.txtLyric.TabIndex = 0;
			this.txtLyric.Visible = false;
			this.txtLyric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLyric_KeyDown);
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextCut,
            this.mnuContextCopy,
            this.mnuContextPaste,
            this.mnuContextDelete,
            this.mnuContextSep1,
            this.mnuContextProperties});
			this.mnuContext.Name = "mnuContext";
			this.mnuContext.Size = new System.Drawing.Size(194, 120);
			this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
			// 
			// mnuContextCut
			// 
			this.mnuContextCut.Name = "mnuContextCut";
			this.mnuContextCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.mnuContextCut.Size = new System.Drawing.Size(193, 22);
			this.mnuContextCut.Text = "Cu&t";
			this.mnuContextCut.Click += new System.EventHandler(this.mnuContextCut_Click);
			// 
			// mnuContextCopy
			// 
			this.mnuContextCopy.Name = "mnuContextCopy";
			this.mnuContextCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.mnuContextCopy.Size = new System.Drawing.Size(193, 22);
			this.mnuContextCopy.Text = "&Copy";
			this.mnuContextCopy.Click += new System.EventHandler(this.mnuContextCopy_Click);
			// 
			// mnuContextPaste
			// 
			this.mnuContextPaste.Name = "mnuContextPaste";
			this.mnuContextPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.mnuContextPaste.Size = new System.Drawing.Size(193, 22);
			this.mnuContextPaste.Text = "&Paste";
			this.mnuContextPaste.Click += new System.EventHandler(this.mnuContextPaste_Click);
			// 
			// mnuContextDelete
			// 
			this.mnuContextDelete.Name = "mnuContextDelete";
			this.mnuContextDelete.ShortcutKeyDisplayString = "Del";
			this.mnuContextDelete.Size = new System.Drawing.Size(193, 22);
			this.mnuContextDelete.Text = "&Delete";
			this.mnuContextDelete.Click += new System.EventHandler(this.mnuContextDelete_Click);
			// 
			// mnuContextSep1
			// 
			this.mnuContextSep1.Name = "mnuContextSep1";
			this.mnuContextSep1.Size = new System.Drawing.Size(190, 6);
			// 
			// mnuContextProperties
			// 
			this.mnuContextProperties.Name = "mnuContextProperties";
			this.mnuContextProperties.ShortcutKeyDisplayString = "Alt+Enter";
			this.mnuContextProperties.Size = new System.Drawing.Size(193, 22);
			this.mnuContextProperties.Text = "P&roperties...";
			this.mnuContextProperties.Click += new System.EventHandler(this.mnuContextProperties_Click);
			// 
			// PianoRollControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.mnuContext;
			this.Controls.Add(this.txtLyric);
			this.Name = "PianoRollControl";
			this.mnuContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox txtLyric;
		private AwesomeControls.CommandBars.CBContextMenu mnuContext;
		private System.Windows.Forms.ToolStripMenuItem mnuContextCut;
		private System.Windows.Forms.ToolStripMenuItem mnuContextCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuContextPaste;
		private System.Windows.Forms.ToolStripMenuItem mnuContextDelete;
		private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextProperties;
    }
}
