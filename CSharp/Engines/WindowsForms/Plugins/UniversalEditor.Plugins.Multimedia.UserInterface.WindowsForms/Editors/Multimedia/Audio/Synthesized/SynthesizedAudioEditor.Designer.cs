namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
    partial class SynthesizedAudioEditor
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
			this.pnlTrackEditor = new System.Windows.Forms.Panel();
			this.pnlPianoRoll = new UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll.PianoRollControl();
			this.mnuContextTreeView = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddExistingItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddNewFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewAddTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewAddInstrument = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewExclude = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlTrackEditor.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.pnlTrackEditor);
			this.splitContainer1.Size = new System.Drawing.Size(631, 375);
			this.splitContainer1.SplitterDistance = 210;
			this.splitContainer1.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(210, 375);
			this.tv.TabIndex = 0;
			// 
			// pnlTrackEditor
			// 
			this.pnlTrackEditor.Controls.Add(this.pnlPianoRoll);
			this.pnlTrackEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTrackEditor.Location = new System.Drawing.Point(0, 0);
			this.pnlTrackEditor.Name = "pnlTrackEditor";
			this.pnlTrackEditor.Size = new System.Drawing.Size(417, 375);
			this.pnlTrackEditor.TabIndex = 0;
			// 
			// pnlPianoRoll
			// 
			this.pnlPianoRoll.BackColor = System.Drawing.SystemColors.Window;
			this.pnlPianoRoll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlPianoRoll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPianoRoll.KeyboardWidth = 64;
			this.pnlPianoRoll.Location = new System.Drawing.Point(0, 0);
			this.pnlPianoRoll.Name = "pnlPianoRoll";
			this.pnlPianoRoll.QuantizationSize = new System.Drawing.Size(16, 16);
			this.pnlPianoRoll.ShowKeyboard = true;
			this.pnlPianoRoll.Size = new System.Drawing.Size(417, 375);
			this.pnlPianoRoll.TabIndex = 0;
			this.pnlPianoRoll.ZoomFactor = 1D;
			// 
			// mnuContextTreeView
			// 
			this.mnuContextTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.mnuContextTreeViewSep1,
            this.mnuContextTreeViewExclude,
            this.toolStripMenuItem3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripMenuItem4,
            this.propertiesToolStripMenuItem});
			this.mnuContextTreeView.Name = "mnuContextTreeView";
			this.mnuContextTreeView.Size = new System.Drawing.Size(194, 198);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewAddNewItem,
            this.mnuContextTreeViewAddExistingItem,
            this.mnuContextTreeViewAddNewFolder,
            this.mnuContextTreeViewAddSep1,
            this.mnuContextTreeViewAddTrack,
            this.mnuContextTreeViewAddInstrument});
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.addToolStripMenuItem.Text = "A&dd";
			// 
			// mnuContextTreeViewAddNewItem
			// 
			this.mnuContextTreeViewAddNewItem.Name = "mnuContextTreeViewAddNewItem";
			this.mnuContextTreeViewAddNewItem.ShortcutKeyDisplayString = "Ctrl+Ins";
			this.mnuContextTreeViewAddNewItem.Size = new System.Drawing.Size(231, 22);
			this.mnuContextTreeViewAddNewItem.Text = "Ne&w Item...";
			// 
			// mnuContextTreeViewAddExistingItem
			// 
			this.mnuContextTreeViewAddExistingItem.Name = "mnuContextTreeViewAddExistingItem";
			this.mnuContextTreeViewAddExistingItem.ShortcutKeyDisplayString = "Ctrl+Shift+Ins";
			this.mnuContextTreeViewAddExistingItem.Size = new System.Drawing.Size(231, 22);
			this.mnuContextTreeViewAddExistingItem.Text = "Existin&g Item...";
			// 
			// mnuContextTreeViewAddNewFolder
			// 
			this.mnuContextTreeViewAddNewFolder.Name = "mnuContextTreeViewAddNewFolder";
			this.mnuContextTreeViewAddNewFolder.Size = new System.Drawing.Size(231, 22);
			this.mnuContextTreeViewAddNewFolder.Text = "New Fol&der";
			// 
			// mnuContextTreeViewAddSep1
			// 
			this.mnuContextTreeViewAddSep1.Name = "mnuContextTreeViewAddSep1";
			this.mnuContextTreeViewAddSep1.Size = new System.Drawing.Size(228, 6);
			// 
			// mnuContextTreeViewAddTrack
			// 
			this.mnuContextTreeViewAddTrack.Name = "mnuContextTreeViewAddTrack";
			this.mnuContextTreeViewAddTrack.Size = new System.Drawing.Size(231, 22);
			this.mnuContextTreeViewAddTrack.Text = "&Track";
			// 
			// mnuContextTreeViewAddInstrument
			// 
			this.mnuContextTreeViewAddInstrument.Name = "mnuContextTreeViewAddInstrument";
			this.mnuContextTreeViewAddInstrument.Size = new System.Drawing.Size(231, 22);
			this.mnuContextTreeViewAddInstrument.Text = "&Instrument";
			// 
			// mnuContextTreeViewSep1
			// 
			this.mnuContextTreeViewSep1.Name = "mnuContextTreeViewSep1";
			this.mnuContextTreeViewSep1.Size = new System.Drawing.Size(190, 6);
			// 
			// mnuContextTreeViewExclude
			// 
			this.mnuContextTreeViewExclude.Name = "mnuContextTreeViewExclude";
			this.mnuContextTreeViewExclude.Size = new System.Drawing.Size(193, 22);
			this.mnuContextTreeViewExclude.Text = "Exclude from Pro&ject";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(190, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.deleteToolStripMenuItem.Text = "&Delete";
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeyDisplayString = "F3";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.renameToolStripMenuItem.Text = "Rena&me";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(190, 6);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.ShortcutKeyDisplayString = "Alt+Enter";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.propertiesToolStripMenuItem.Text = "P&roperties...";
			// 
			// SynthesizedAudioEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "SynthesizedAudioEditor";
			this.Size = new System.Drawing.Size(631, 375);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.pnlTrackEditor.ResumeLayout(false);
			this.mnuContextTreeView.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private AwesomeControls.CommandBars.CBContextMenu mnuContextTreeView;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddNewItem;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddExistingItem;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddNewFolder;
        private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewAddSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddTrack;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAddInstrument;
        private System.Windows.Forms.ToolStripSeparator mnuContextTreeViewSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewExclude;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlTrackEditor;
		private UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll.PianoRollControl pnlPianoRoll;
    }
}
