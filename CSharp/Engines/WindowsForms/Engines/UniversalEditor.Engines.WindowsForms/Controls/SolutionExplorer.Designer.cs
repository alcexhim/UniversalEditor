using System.Drawing;
namespace UniversalEditor.Engines.WindowsForms.Controls
{
    partial class SolutionExplorer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionExplorer));
			this.cbToolBar1 = new AwesomeControls.CommandBars.CBToolBar();
			this.tsbBack = new System.Windows.Forms.ToolStripButton();
			this.tsbForward = new System.Windows.Forms.ToolStripButton();
			this.tsbHome = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbFilter = new System.Windows.Forms.ToolStripSplitButton();
			this.pendingChangesFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFilesFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
			this.tsbShowAllFiles = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbViewCode = new System.Windows.Forms.ToolStripButton();
			this.tsbProperties = new System.Windows.Forms.ToolStripButton();
			this.tsbPreviewSelectedItems = new System.Windows.Forms.ToolStripButton();
			this.txtFilter = new AwesomeControls.TextBox.TextBoxControl();
			this.tv = new System.Windows.Forms.TreeView();
			this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextAddNewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextAddExistingProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextAddSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextAddExistingItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextAddSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextAddSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextAddNewFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.cbToolBar1.SuspendLayout();
			this.mnuContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbToolBar1
			// 
			this.cbToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.cbToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward,
            this.tsbHome,
            this.toolStripSeparator1,
            this.tsbFilter,
            this.tsbRefresh,
            this.tsbCollapseAll,
            this.tsbShowAllFiles,
            this.toolStripSeparator2,
            this.tsbViewCode,
            this.tsbProperties,
            this.tsbPreviewSelectedItems});
			this.cbToolBar1.Location = new System.Drawing.Point(0, 0);
			this.cbToolBar1.Name = "cbToolBar1";
			this.cbToolBar1.Size = new System.Drawing.Size(322, 25);
			this.cbToolBar1.TabIndex = 0;
			this.cbToolBar1.Text = "cbToolBar1";
			// 
			// tsbBack
			// 
			this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
			this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbBack.Name = "tsbBack";
			this.tsbBack.Size = new System.Drawing.Size(23, 22);
			this.tsbBack.Text = "Back";
			// 
			// tsbForward
			// 
			this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbForward.Image")));
			this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbForward.Name = "tsbForward";
			this.tsbForward.Size = new System.Drawing.Size(23, 22);
			this.tsbForward.Text = "Forward";
			// 
			// tsbHome
			// 
			this.tsbHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbHome.Image = ((System.Drawing.Image)(resources.GetObject("tsbHome.Image")));
			this.tsbHome.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbHome.Name = "tsbHome";
			this.tsbHome.Size = new System.Drawing.Size(23, 22);
			this.tsbHome.Text = "Home";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbFilter
			// 
			this.tsbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pendingChangesFilterToolStripMenuItem,
            this.openFilesFilterToolStripMenuItem});
			this.tsbFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbFilter.Image")));
			this.tsbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilter.Name = "tsbFilter";
			this.tsbFilter.Size = new System.Drawing.Size(32, 22);
			this.tsbFilter.Text = "Pending Changes Filter";
			// 
			// pendingChangesFilterToolStripMenuItem
			// 
			this.pendingChangesFilterToolStripMenuItem.Name = "pendingChangesFilterToolStripMenuItem";
			this.pendingChangesFilterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.pendingChangesFilterToolStripMenuItem.Text = "Pending Changes Filter";
			// 
			// openFilesFilterToolStripMenuItem
			// 
			this.openFilesFilterToolStripMenuItem.Name = "openFilesFilterToolStripMenuItem";
			this.openFilesFilterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.openFilesFilterToolStripMenuItem.Text = "Open Files Filter";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
			this.tsbRefresh.Text = "Refresh";
			// 
			// tsbCollapseAll
			// 
			this.tsbCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollapseAll.Image")));
			this.tsbCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCollapseAll.Name = "tsbCollapseAll";
			this.tsbCollapseAll.Size = new System.Drawing.Size(23, 22);
			this.tsbCollapseAll.Text = "Collapse All";
			// 
			// tsbShowAllFiles
			// 
			this.tsbShowAllFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbShowAllFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowAllFiles.Image")));
			this.tsbShowAllFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbShowAllFiles.Name = "tsbShowAllFiles";
			this.tsbShowAllFiles.Size = new System.Drawing.Size(23, 22);
			this.tsbShowAllFiles.Text = "Show All Files";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbViewCode
			// 
			this.tsbViewCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbViewCode.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewCode.Image")));
			this.tsbViewCode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbViewCode.Name = "tsbViewCode";
			this.tsbViewCode.Size = new System.Drawing.Size(23, 22);
			this.tsbViewCode.Text = "View Code";
			// 
			// tsbProperties
			// 
			this.tsbProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbProperties.Image")));
			this.tsbProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbProperties.Name = "tsbProperties";
			this.tsbProperties.Size = new System.Drawing.Size(23, 22);
			this.tsbProperties.Text = "Properties";
			// 
			// tsbPreviewSelectedItems
			// 
			this.tsbPreviewSelectedItems.CheckOnClick = true;
			this.tsbPreviewSelectedItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbPreviewSelectedItems.Image = ((System.Drawing.Image)(resources.GetObject("tsbPreviewSelectedItems.Image")));
			this.tsbPreviewSelectedItems.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPreviewSelectedItems.Name = "tsbPreviewSelectedItems";
			this.tsbPreviewSelectedItems.Size = new System.Drawing.Size(23, 22);
			this.tsbPreviewSelectedItems.Text = "Preview Selected Items";
			// 
			// txtFilter
			// 
			this.txtFilter.AcceptReturn = true;
			this.txtFilter.AutoIndentEnabled = true;
			this.txtFilter.AutoSuggestFilter = true;
			this.txtFilter.AutoSuggestMode = AwesomeControls.TextBox.TextBoxAutoSuggestMode.None;
			this.txtFilter.BackColor = System.Drawing.SystemColors.Window;
			this.txtFilter.CaretBlinkInterval = 530;
			this.txtFilter.CaretColor = System.Drawing.Color.Black;
			this.txtFilter.CaretOrientation = System.Windows.Forms.Orientation.Vertical;
			this.txtFilter.CaretSize = 1;
			this.txtFilter.CaseSensitive = true;
			this.txtFilter.CharacterSpacing = 0;
			this.txtFilter.CurrentLineIndex = 0;
			this.txtFilter.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtFilter.EnableCaret = true;
			this.txtFilter.EnableCaretBlink = true;
			this.txtFilter.EnableMultiSelection = true;
			this.txtFilter.EnableOutlining = false;
			this.txtFilter.EnableOverwrite = false;
			this.txtFilter.EnableOverwriteShortcut = true;
			this.txtFilter.EnableSelection = true;
			this.txtFilter.EnableSyntaxHighlight = false;
			this.txtFilter.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtFilter.HideSelection = true;
			this.txtFilter.LineSeparator = AwesomeControls.TextBox.TextBoxLineSeparator.Default;
			this.txtFilter.LineSeparatorString = "\r\n";
			this.txtFilter.Location = new System.Drawing.Point(0, 25);
			this.txtFilter.Multiline = false;
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.PlaceholderText = "";
			this.txtFilter.ReplaceTabsWithSpaces = true;
			this.txtFilter.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtFilter.SelectionStart = 0;
			this.txtFilter.Size = new System.Drawing.Size(322, 23);
			this.txtFilter.TabIndex = 1;
			this.txtFilter.TabSize = 4;
			this.txtFilter.WordSpacing = 0;
			// 
			// tv
			// 
			this.tv.ContextMenuStrip = this.mnuContext;
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.FullRowSelect = true;
			this.tv.HideSelection = false;
			this.tv.ImageIndex = 0;
			this.tv.ImageList = this.imlSmallIcons;
			this.tv.LabelEdit = true;
			this.tv.Location = new System.Drawing.Point(0, 48);
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = 0;
			this.tv.Size = new System.Drawing.Size(322, 331);
			this.tv.TabIndex = 2;
			this.tv.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_BeforeLabelEdit);
			this.tv.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_AfterLabelEdit);
			this.tv.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeSelect);
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			this.tv.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseDoubleClick);
			this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextAdd,
            this.mnuContextSep1,
            this.mnuContextCut,
            this.mnuContextCopy,
            this.mnuContextPaste,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.mnuContextProperties});
			this.mnuContext.Name = "cbContextMenu1";
			this.mnuContext.Size = new System.Drawing.Size(190, 148);
			this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
			// 
			// mnuContextAdd
			// 
			this.mnuContextAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextAddNewProject,
            this.mnuContextAddExistingProject,
            this.mnuContextAddSep1,
            this.mnuContextAddNewItem,
            this.mnuContextAddExistingItem,
            this.mnuContextAddSep2,
            this.mnuContextAddSep3,
            this.mnuContextAddNewFolder});
			this.mnuContextAdd.Name = "mnuContextAdd";
			this.mnuContextAdd.Size = new System.Drawing.Size(189, 22);
			this.mnuContextAdd.Text = "A&dd";
			// 
			// mnuContextAddNewProject
			// 
			this.mnuContextAddNewProject.Name = "mnuContextAddNewProject";
			this.mnuContextAddNewProject.Size = new System.Drawing.Size(156, 22);
			this.mnuContextAddNewProject.Text = "&New Project...";
			this.mnuContextAddNewProject.Click += new System.EventHandler(this.mnuContextAddNewProject_Click);
			// 
			// mnuContextAddExistingProject
			// 
			this.mnuContextAddExistingProject.Name = "mnuContextAddExistingProject";
			this.mnuContextAddExistingProject.Size = new System.Drawing.Size(156, 22);
			this.mnuContextAddExistingProject.Text = "&Existing Project..";
			this.mnuContextAddExistingProject.Click += new System.EventHandler(this.mnuContextAddExistingProject_Click);
			// 
			// mnuContextAddSep1
			// 
			this.mnuContextAddSep1.Name = "mnuContextAddSep1";
			this.mnuContextAddSep1.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextAddNewItem
			// 
			this.mnuContextAddNewItem.Name = "mnuContextAddNewItem";
			this.mnuContextAddNewItem.Size = new System.Drawing.Size(156, 22);
			this.mnuContextAddNewItem.Text = "Ne&w Item...";
			this.mnuContextAddNewItem.Click += new System.EventHandler(this.mnuContextAddNewItem_Click);
			// 
			// mnuContextAddExistingItem
			// 
			this.mnuContextAddExistingItem.Name = "mnuContextAddExistingItem";
			this.mnuContextAddExistingItem.Size = new System.Drawing.Size(156, 22);
			this.mnuContextAddExistingItem.Text = "Existin&g Item...";
			this.mnuContextAddExistingItem.Click += new System.EventHandler(this.mnuContextAddExistingItem_Click);
			// 
			// mnuContextAddSep2
			// 
			this.mnuContextAddSep2.Name = "mnuContextAddSep2";
			this.mnuContextAddSep2.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextAddSep3
			// 
			this.mnuContextAddSep3.Name = "mnuContextAddSep3";
			this.mnuContextAddSep3.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuContextAddNewFolder
			// 
			this.mnuContextAddNewFolder.Name = "mnuContextAddNewFolder";
			this.mnuContextAddNewFolder.Size = new System.Drawing.Size(156, 22);
			this.mnuContextAddNewFolder.Text = "New Fol&der";
			this.mnuContextAddNewFolder.Click += new System.EventHandler(this.mnuContextAddNewFolder_Click);
			// 
			// mnuContextSep1
			// 
			this.mnuContextSep1.Name = "mnuContextSep1";
			this.mnuContextSep1.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuContextCut
			// 
			this.mnuContextCut.Name = "mnuContextCut";
			this.mnuContextCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.mnuContextCut.Size = new System.Drawing.Size(189, 22);
			this.mnuContextCut.Text = "Cu&t";
			// 
			// mnuContextCopy
			// 
			this.mnuContextCopy.Name = "mnuContextCopy";
			this.mnuContextCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.mnuContextCopy.Size = new System.Drawing.Size(189, 22);
			this.mnuContextCopy.Text = "&Copy";
			// 
			// mnuContextPaste
			// 
			this.mnuContextPaste.Name = "mnuContextPaste";
			this.mnuContextPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.mnuContextPaste.Size = new System.Drawing.Size(189, 22);
			this.mnuContextPaste.Text = "&Paste";
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.deleteToolStripMenuItem.Text = "&Delete";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(186, 6);
			// 
			// mnuContextProperties
			// 
			this.mnuContextProperties.Name = "mnuContextProperties";
			this.mnuContextProperties.ShortcutKeyDisplayString = "Alt+Enter";
			this.mnuContextProperties.Size = new System.Drawing.Size(189, 22);
			this.mnuContextProperties.Text = "P&roperties...";
			// 
			// imlSmallIcons
			// 
			this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SolutionExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tv);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.cbToolBar1);
			this.Name = "SolutionExplorer";
			this.Size = new System.Drawing.Size(322, 379);
			this.cbToolBar1.ResumeLayout(false);
			this.cbToolBar1.PerformLayout();
			this.mnuContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private AwesomeControls.CommandBars.CBToolBar cbToolBar1;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbHome;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tsbFilter;
        private System.Windows.Forms.ToolStripMenuItem pendingChangesFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFilesFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbCollapseAll;
        private System.Windows.Forms.ToolStripButton tsbShowAllFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbViewCode;
        private System.Windows.Forms.ToolStripButton tsbProperties;
        private System.Windows.Forms.ToolStripButton tsbPreviewSelectedItems;
        private AwesomeControls.TextBox.TextBoxControl txtFilter;
        private System.Windows.Forms.TreeView tv;
        private AwesomeControls.CommandBars.CBContextMenu mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddNewItem;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddExistingItem;
        private System.Windows.Forms.ToolStripSeparator mnuContextAddSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuContextAddNewFolder;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCut;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuContextPaste;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuContextProperties;
		private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.ToolStripSeparator mnuContextAddSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuContextAddNewProject;
		private System.Windows.Forms.ToolStripMenuItem mnuContextAddExistingProject;
		private System.Windows.Forms.ToolStripSeparator mnuContextAddSep1;
    }
}
