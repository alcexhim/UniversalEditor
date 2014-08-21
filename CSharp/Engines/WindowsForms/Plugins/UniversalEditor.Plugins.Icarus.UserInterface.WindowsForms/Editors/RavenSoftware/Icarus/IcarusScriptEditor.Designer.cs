namespace UniversalEditor.Editors.RavenSoftware.Icarus
{
    partial class IcarusScriptEditor
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
            this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
            this.mnuContextRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.ContextMenuStrip = this.mnuContext;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.FullRowSelect = true;
            this.tv.HideSelection = false;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.ShowLines = false;
            this.tv.Size = new System.Drawing.Size(530, 367);
            this.tv.TabIndex = 0;
            this.tv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDoubleClick);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextRun,
            this.mnuContextSep1,
            this.mnuContextCut,
            this.mnuContextCopy,
            this.mnuContextPaste,
            this.mnuContextDelete,
            this.toolStripMenuItem2,
            this.mnuContextProperties});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(194, 148);
            // 
            // mnuContextRun
            // 
            this.mnuContextRun.Name = "mnuContextRun";
            this.mnuContextRun.ShortcutKeyDisplayString = "F5";
            this.mnuContextRun.Size = new System.Drawing.Size(193, 22);
            this.mnuContextRun.Text = "&Run";
            // 
            // mnuContextSep1
            // 
            this.mnuContextSep1.Name = "mnuContextSep1";
            this.mnuContextSep1.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuContextCut
            // 
            this.mnuContextCut.Name = "mnuContextCut";
            this.mnuContextCut.ShortcutKeyDisplayString = "Ctrl+X";
            this.mnuContextCut.Size = new System.Drawing.Size(193, 22);
            this.mnuContextCut.Text = "Cu&t";
            // 
            // mnuContextCopy
            // 
            this.mnuContextCopy.Name = "mnuContextCopy";
            this.mnuContextCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.mnuContextCopy.Size = new System.Drawing.Size(193, 22);
            this.mnuContextCopy.Text = "&Copy";
            // 
            // mnuContextPaste
            // 
            this.mnuContextPaste.Name = "mnuContextPaste";
            this.mnuContextPaste.ShortcutKeyDisplayString = "Ctrl+V";
            this.mnuContextPaste.Size = new System.Drawing.Size(193, 22);
            this.mnuContextPaste.Text = "&Paste";
            // 
            // mnuContextDelete
            // 
            this.mnuContextDelete.Name = "mnuContextDelete";
            this.mnuContextDelete.ShortcutKeyDisplayString = "Del";
            this.mnuContextDelete.Size = new System.Drawing.Size(193, 22);
            this.mnuContextDelete.Text = "&Delete";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuContextProperties
            // 
            this.mnuContextProperties.Name = "mnuContextProperties";
            this.mnuContextProperties.ShortcutKeyDisplayString = "Alt+Enter";
            this.mnuContextProperties.Size = new System.Drawing.Size(193, 22);
            this.mnuContextProperties.Text = "P&roperties...";
            // 
            // IcarusScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tv);
            this.Name = "IcarusScriptEditor";
            this.Size = new System.Drawing.Size(530, 367);
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private AwesomeControls.CommandBars.CBContextMenu mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextRun;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCut;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuContextPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuContextDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuContextProperties;
    }
}
