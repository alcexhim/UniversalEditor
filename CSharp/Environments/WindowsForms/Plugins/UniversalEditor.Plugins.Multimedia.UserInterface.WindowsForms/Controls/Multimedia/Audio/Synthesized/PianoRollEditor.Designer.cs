namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized
{
    partial class PianoRollEditor
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
            this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.mnuContextSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextErase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextSelect,
            this.mnuContextDraw,
            this.mnuContextErase,
            this.mnuContextSep1,
            this.mnuContextProperties});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(153, 120);
            this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
            // 
            // mnuContextSelect
            // 
            this.mnuContextSelect.Name = "mnuContextSelect";
            this.mnuContextSelect.Size = new System.Drawing.Size(152, 22);
            this.mnuContextSelect.Text = "&Select";
            this.mnuContextSelect.Click += new System.EventHandler(this.mnuContextSelect_Click);
            // 
            // mnuContextDraw
            // 
            this.mnuContextDraw.Checked = true;
            this.mnuContextDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuContextDraw.Name = "mnuContextDraw";
            this.mnuContextDraw.Size = new System.Drawing.Size(152, 22);
            this.mnuContextDraw.Text = "&Draw";
            this.mnuContextDraw.Click += new System.EventHandler(this.mnuContextDraw_Click);
            // 
            // mnuContextErase
            // 
            this.mnuContextErase.Name = "mnuContextErase";
            this.mnuContextErase.Size = new System.Drawing.Size(152, 22);
            this.mnuContextErase.Text = "&Erase";
            this.mnuContextErase.Click += new System.EventHandler(this.mnuContextErase_Click);
            // 
            // mnuContextSep1
            // 
            this.mnuContextSep1.Name = "mnuContextSep1";
            this.mnuContextSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuContextProperties
            // 
            this.mnuContextProperties.Enabled = false;
            this.mnuContextProperties.Name = "mnuContextProperties";
            this.mnuContextProperties.Size = new System.Drawing.Size(152, 22);
            this.mnuContextProperties.Text = "P&roperties...";
            // 
            // PianoRollEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ContextMenuStrip = this.mnuContext;
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "PianoRollEditor";
            this.Size = new System.Drawing.Size(574, 347);
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AwesomeControls.CommandBars.CBContextMenu mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextSelect;
        private System.Windows.Forms.ToolStripMenuItem mnuContextDraw;
        private System.Windows.Forms.ToolStripMenuItem mnuContextErase;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextProperties;
    }
}
