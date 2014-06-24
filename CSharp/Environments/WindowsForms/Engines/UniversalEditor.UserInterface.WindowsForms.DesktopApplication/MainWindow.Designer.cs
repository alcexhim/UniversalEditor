using System.Drawing;
namespace UniversalEditor.UserInterface.WindowsForms
{
	partial class MainWindow
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.cbc = new AwesomeControls.CommandBars.CBContainer();
			this.sbStatusBar = new AwesomeControls.CommandBars.CBStatusBar();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pbProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.lblObjectModel = new System.Windows.Forms.ToolStripStatusLabel();
			this.dcc = new AwesomeControls.DockingWindows.DockingContainerControl();
			this.cboAddress = new System.Windows.Forms.ComboBox();
			this.mbMenuBar = new AwesomeControls.CommandBars.CBMenuBar();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNewFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileSaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileCloseFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileCloseProject = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileCloseWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileImport = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuFileExport = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditInvertSelection = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuEditSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditFindReplace = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditGoTo = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuEditSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditPreferences = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewToolbars = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewToolbarsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewToolbarsCustomize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewPanels = new System.Windows.Forms.ToolStripMenuItem();
			this.toolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewPanelsProjectExplorer = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewStartPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProjectAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProjectAddExistingItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProjectSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuProjectExclude = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProjectShowAllFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuProjectSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuProjectProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBookmarks = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBookmarksAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBookmarksAddAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBookmarksSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuBookmarksSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuBookmarksManage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsSessionManager = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuToolsCustomize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowNewWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowSplit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuWindowFloat = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowFloatAll = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowDock = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowAutoHide = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowAutoHideAll = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowHide = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuWindowPinTab = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuWindowNewTabGroupHorizontal = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowNewTabGroupVertical = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowCloseAllDocuments = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowResetLayout = new AwesomeControls.CommandBars.CBMenuItem();
			this.mnuWindowSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuWindowWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.tbStandard = new AwesomeControls.CommandBars.CBToolBar();
			this.tsbStandardNew = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbStandardNewFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardNewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardOpen = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbStandardOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardSave = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbStandardSaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardSaveSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbStandardSaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardSaveSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbStandardSaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbStandardPrint = new System.Windows.Forms.ToolStripButton();
			this.tsbStandardSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbStandardCut = new System.Windows.Forms.ToolStripButton();
			this.tsbStandardCopy = new System.Windows.Forms.ToolStripButton();
			this.tsbStandardPaste = new System.Windows.Forms.ToolStripButton();
			this.tsbStandardSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbStandardHelp = new System.Windows.Forms.ToolStripButton();
			this.tmrToolStripContainerPopup = new System.Windows.Forms.Timer(this.components);
			this.mnuContextDocumentType = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextDocumentTypeDataFormat = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextDocumentTypeSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.cbc.BottomToolStripPanel.SuspendLayout();
			this.cbc.ContentPanel.SuspendLayout();
			this.cbc.TopToolStripPanel.SuspendLayout();
			this.cbc.SuspendLayout();
			this.sbStatusBar.SuspendLayout();
			this.mbMenuBar.SuspendLayout();
			this.tbStandard.SuspendLayout();
			this.mnuContextDocumentType.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbc
			// 
			// 
			// cbc.BottomToolStripPanel
			// 
			this.cbc.BottomToolStripPanel.Controls.Add(this.sbStatusBar);
			// 
			// cbc.ContentPanel
			// 
			this.cbc.ContentPanel.Controls.Add(this.dcc);
			this.cbc.ContentPanel.Controls.Add(this.cboAddress);
			this.cbc.ContentPanel.Size = new System.Drawing.Size(800, 528);
			this.cbc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbc.Location = new System.Drawing.Point(0, 0);
			this.cbc.Name = "cbc";
			this.cbc.Size = new System.Drawing.Size(800, 600);
			this.cbc.TabIndex = 0;
			this.cbc.Text = "cbContainer1";
			// 
			// cbc.TopToolStripPanel
			// 
			this.cbc.TopToolStripPanel.Controls.Add(this.mbMenuBar);
			this.cbc.TopToolStripPanel.Controls.Add(this.tbStandard);
			this.cbc.UseCommandManager = false;
			// 
			// sbStatusBar
			// 
			this.sbStatusBar.Dock = System.Windows.Forms.DockStyle.None;
			this.sbStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.lblStatus,
			this.pbProgress,
			this.lblObjectModel});
			this.sbStatusBar.Location = new System.Drawing.Point(0, 0);
			this.sbStatusBar.Name = "sbStatusBar";
			this.sbStatusBar.ShowItemToolTips = true;
			this.sbStatusBar.Size = new System.Drawing.Size(800, 22);
			this.sbStatusBar.TabIndex = 0;
			this.sbStatusBar.Text = "Status Bar";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(706, 17);
			this.lblStatus.Spring = true;
			this.lblStatus.Text = "Ready";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbProgress
			// 
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(200, 16);
			this.pbProgress.Visible = false;
			// 
			// lblObjectModel
			// 
			this.lblObjectModel.Name = "lblObjectModel";
			this.lblObjectModel.Size = new System.Drawing.Size(79, 17);
			this.lblObjectModel.Text = "Object Model";
			this.lblObjectModel.ToolTipText = "Click to switch object models";
			this.lblObjectModel.Click += new System.EventHandler(this.lblDataFormat_Click);
			// 
			// dcc
			// 
			this.dcc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dcc.Location = new System.Drawing.Point(0, 21);
			this.dcc.Name = "dcc";
			this.dcc.SelectedWindow = null;
			this.dcc.Size = new System.Drawing.Size(800, 507);
			this.dcc.TabIndex = 3;
			this.dcc.SelectedWindowChanged += new System.EventHandler(this.dcc_SelectedWindowChanged);
			this.dcc.WindowClosing += new AwesomeControls.DockingWindows.WindowClosingEventHandler(this.dcc_WindowClosing);
			this.dcc.WindowClosed += new AwesomeControls.DockingWindows.WindowClosedEventHandler(this.dcc_WindowClosed);
			// 
			// cboAddress
			// 
			this.cboAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboAddress.FormattingEnabled = true;
			this.cboAddress.Location = new System.Drawing.Point(0, 0);
			this.cboAddress.Name = "cboAddress";
			this.cboAddress.Size = new System.Drawing.Size(800, 21);
			this.cboAddress.TabIndex = 2;
			this.cboAddress.Visible = false;
			this.cboAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboAddress_KeyDown);
			// 
			// mbMenuBar
			// 
			this.mbMenuBar.Dock = System.Windows.Forms.DockStyle.None;
			this.mbMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFile,
			this.mnuEdit,
			this.mnuView,
			this.mnuProject,
			this.mnuBookmarks,
			this.mnuTools,
			this.mnuWindow});
			this.mbMenuBar.Location = new System.Drawing.Point(0, 0);
			this.mbMenuBar.Name = "mbMenuBar";
			this.mbMenuBar.Size = new System.Drawing.Size(800, 25);
			this.mbMenuBar.TabIndex = 0;
			this.mbMenuBar.Text = "Menu Bar";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFileNew,
			this.mnuFileOpen,
			this.mnuFileSave,
			this.mnuFileClose,
			this.mnuFileSep1,
			this.mnuFilePrint,
			this.mnuFileSep2,
			this.mnuFileImport,
			this.mnuFileExport,
			this.mnuFileSep3,
			this.mnuFileRecentFiles,
			this.mnuFileRecentProjects,
			this.mnuFileSep4,
			this.mnuFileExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 21);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFileNewFile,
			this.mnuFileNewProject});
			this.mnuFileNew.Name = "mnuFileNew";
			this.mnuFileNew.Size = new System.Drawing.Size(156, 22);
			this.mnuFileNew.Text = "&New";
			// 
			// mnuFileNewFile
			// 
			this.mnuFileNewFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileNewFile.Image")));
			this.mnuFileNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuFileNewFile.Name = "mnuFileNewFile";
			this.mnuFileNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuFileNewFile.Size = new System.Drawing.Size(244, 22);
			this.mnuFileNewFile.Text = "&File...";
			this.mnuFileNewFile.Click += new System.EventHandler(this.FileNewFile_Click);
			// 
			// mnuFileNewProject
			// 
			this.mnuFileNewProject.Name = "mnuFileNewProject";
			this.mnuFileNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.N)));
			this.mnuFileNewProject.Size = new System.Drawing.Size(244, 22);
			this.mnuFileNewProject.Text = "&Project/Solution...";
			this.mnuFileNewProject.Click += new System.EventHandler(this.FileNewProject_Click);
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFileOpenFile,
			this.mnuFileOpenProject});
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.Size = new System.Drawing.Size(156, 22);
			this.mnuFileOpen.Text = "&Open";
			// 
			// mnuFileOpenFile
			// 
			this.mnuFileOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileOpenFile.Image")));
			this.mnuFileOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuFileOpenFile.Name = "mnuFileOpenFile";
			this.mnuFileOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuFileOpenFile.Size = new System.Drawing.Size(244, 22);
			this.mnuFileOpenFile.Text = "&File...";
			this.mnuFileOpenFile.Click += new System.EventHandler(this.FileOpenFile_Click);
			// 
			// mnuFileOpenProject
			// 
			this.mnuFileOpenProject.Name = "mnuFileOpenProject";
			this.mnuFileOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.O)));
			this.mnuFileOpenProject.Size = new System.Drawing.Size(244, 22);
			this.mnuFileOpenProject.Text = "&Project/Solution...";
			this.mnuFileOpenProject.Click += new System.EventHandler(this.FileOpenProject_Click);
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFileSaveFile,
			this.mnuFileSaveFileAs,
			this.mnuFileSaveSep1,
			this.mnuFileSaveProject,
			this.mnuFileSaveProjectAs,
			this.mnuFileSaveSep2,
			this.mnuFileSaveAll});
			this.mnuFileSave.Name = "mnuFileSave";
			this.mnuFileSave.Size = new System.Drawing.Size(156, 22);
			this.mnuFileSave.Text = "&Save";
			// 
			// mnuFileSaveFile
			// 
			this.mnuFileSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSaveFile.Image")));
			this.mnuFileSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuFileSaveFile.Name = "mnuFileSaveFile";
			this.mnuFileSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuFileSaveFile.Size = new System.Drawing.Size(263, 22);
			this.mnuFileSaveFile.Text = "&File";
			this.mnuFileSaveFile.Click += new System.EventHandler(this.FileSaveFile_Click);
			// 
			// mnuFileSaveFileAs
			// 
			this.mnuFileSaveFileAs.Name = "mnuFileSaveFileAs";
			this.mnuFileSaveFileAs.Size = new System.Drawing.Size(263, 22);
			this.mnuFileSaveFileAs.Text = "File with Other N&ame...";
			this.mnuFileSaveFileAs.Click += new System.EventHandler(this.FileSaveFileAs_Click);
			// 
			// mnuFileSaveSep1
			// 
			this.mnuFileSaveSep1.Name = "mnuFileSaveSep1";
			this.mnuFileSaveSep1.Size = new System.Drawing.Size(260, 6);
			// 
			// mnuFileSaveProject
			// 
			this.mnuFileSaveProject.Name = "mnuFileSaveProject";
			this.mnuFileSaveProject.Size = new System.Drawing.Size(263, 22);
			this.mnuFileSaveProject.Text = "&Project/Solution";
			this.mnuFileSaveProject.Click += new System.EventHandler(this.FileSaveProject_Click);
			// 
			// mnuFileSaveProjectAs
			// 
			this.mnuFileSaveProjectAs.Name = "mnuFileSaveProjectAs";
			this.mnuFileSaveProjectAs.Size = new System.Drawing.Size(263, 22);
			this.mnuFileSaveProjectAs.Text = "Pro&ject/Solution with Other Name...";
			this.mnuFileSaveProjectAs.Click += new System.EventHandler(this.FileSaveProjectAs_Click);
			// 
			// mnuFileSaveSep2
			// 
			this.mnuFileSaveSep2.Name = "mnuFileSaveSep2";
			this.mnuFileSaveSep2.Size = new System.Drawing.Size(260, 6);
			// 
			// mnuFileSaveAll
			// 
			this.mnuFileSaveAll.Name = "mnuFileSaveAll";
			this.mnuFileSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.S)));
			this.mnuFileSaveAll.Size = new System.Drawing.Size(263, 22);
			this.mnuFileSaveAll.Text = "A&ll Files and Projects";
			this.mnuFileSaveAll.Click += new System.EventHandler(this.FileSaveAll_Click);
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFileCloseFile,
			this.mnuFileCloseProject,
			this.toolStripMenuItem11,
			this.mnuFileCloseWindow});
			this.mnuFileClose.Name = "mnuFileClose";
			this.mnuFileClose.Size = new System.Drawing.Size(156, 22);
			this.mnuFileClose.Text = "&Close";
			// 
			// mnuFileCloseFile
			// 
			this.mnuFileCloseFile.Name = "mnuFileCloseFile";
			this.mnuFileCloseFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.mnuFileCloseFile.Size = new System.Drawing.Size(237, 22);
			this.mnuFileCloseFile.Text = "&File";
			this.mnuFileCloseFile.Click += new System.EventHandler(this.FileCloseFile_Click);
			// 
			// mnuFileCloseProject
			// 
			this.mnuFileCloseProject.Name = "mnuFileCloseProject";
			this.mnuFileCloseProject.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.W)));
			this.mnuFileCloseProject.Size = new System.Drawing.Size(237, 22);
			this.mnuFileCloseProject.Text = "&Project/Solution";
			this.mnuFileCloseProject.Click += new System.EventHandler(this.FileCloseProject_Click);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(234, 6);
			// 
			// mnuFileCloseWindow
			// 
			this.mnuFileCloseWindow.Name = "mnuFileCloseWindow";
			this.mnuFileCloseWindow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuFileCloseWindow.Size = new System.Drawing.Size(237, 22);
			this.mnuFileCloseWindow.Text = "&Window";
			this.mnuFileCloseWindow.Click += new System.EventHandler(this.mnuFileCloseWindow_Click);
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Name = "mnuFileSep1";
			this.mnuFileSep1.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuFilePrint
			// 
			this.mnuFilePrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePrint.Image")));
			this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuFilePrint.Name = "mnuFilePrint";
			this.mnuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.mnuFilePrint.Size = new System.Drawing.Size(156, 22);
			this.mnuFilePrint.Text = "&Print";
			this.mnuFilePrint.Click += new System.EventHandler(this.FilePrint_Click);
			// 
			// mnuFileSep2
			// 
			this.mnuFileSep2.Name = "mnuFileSep2";
			this.mnuFileSep2.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuFileImport
			// 
			this.mnuFileImport.Hidden = true;
			this.mnuFileImport.Name = "mnuFileImport";
			this.mnuFileImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.mnuFileImport.Size = new System.Drawing.Size(156, 22);
			this.mnuFileImport.Text = "&Import...";
			// 
			// mnuFileExport
			// 
			this.mnuFileExport.Hidden = true;
			this.mnuFileExport.Name = "mnuFileExport";
			this.mnuFileExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.mnuFileExport.Size = new System.Drawing.Size(156, 22);
			this.mnuFileExport.Text = "&Export...";
			// 
			// mnuFileSep3
			// 
			this.mnuFileSep3.Name = "mnuFileSep3";
			this.mnuFileSep3.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuFileRecentFiles
			// 
			this.mnuFileRecentFiles.Name = "mnuFileRecentFiles";
			this.mnuFileRecentFiles.Size = new System.Drawing.Size(156, 22);
			this.mnuFileRecentFiles.Text = "Recent &Files";
			// 
			// mnuFileRecentProjects
			// 
			this.mnuFileRecentProjects.Name = "mnuFileRecentProjects";
			this.mnuFileRecentProjects.Size = new System.Drawing.Size(156, 22);
			this.mnuFileRecentProjects.Text = "Recent Pro&jects";
			// 
			// mnuFileSep4
			// 
			this.mnuFileSep4.Name = "mnuFileSep4";
			this.mnuFileSep4.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.mnuFileExit.Size = new System.Drawing.Size(156, 22);
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuEditUndo,
			this.mnuEditRedo,
			this.mnuEditSep1,
			this.mnuEditCut,
			this.mnuEditCopy,
			this.mnuEditPaste,
			this.mnuEditDelete,
			this.mnuEditSep2,
			this.mnuEditSelectAll,
			this.mnuEditInvertSelection,
			this.mnuEditSep3,
			this.mnuEditFindReplace,
			this.mnuEditGoTo,
			this.mnuEditSep4,
			this.mnuEditPreferences});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(39, 21);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEditUndo
			// 
			this.mnuEditUndo.Name = "mnuEditUndo";
			this.mnuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.mnuEditUndo.Size = new System.Drawing.Size(229, 22);
			this.mnuEditUndo.Text = "&Undo";
			this.mnuEditUndo.Click += new System.EventHandler(this.EditUndo_Click);
			// 
			// mnuEditRedo
			// 
			this.mnuEditRedo.Name = "mnuEditRedo";
			this.mnuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.mnuEditRedo.Size = new System.Drawing.Size(229, 22);
			this.mnuEditRedo.Text = "&Redo";
			this.mnuEditRedo.Click += new System.EventHandler(this.EditRedo_Click);
			// 
			// mnuEditSep1
			// 
			this.mnuEditSep1.Name = "mnuEditSep1";
			this.mnuEditSep1.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuEditCut
			// 
			this.mnuEditCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCut.Image")));
			this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuEditCut.Name = "mnuEditCut";
			this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.mnuEditCut.Size = new System.Drawing.Size(229, 22);
			this.mnuEditCut.Text = "Cu&t";
			this.mnuEditCut.Click += new System.EventHandler(this.EditCut_Click);
			// 
			// mnuEditCopy
			// 
			this.mnuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCopy.Image")));
			this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuEditCopy.Name = "mnuEditCopy";
			this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.mnuEditCopy.Size = new System.Drawing.Size(229, 22);
			this.mnuEditCopy.Text = "&Copy";
			this.mnuEditCopy.Click += new System.EventHandler(this.EditCopy_Click);
			// 
			// mnuEditPaste
			// 
			this.mnuEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditPaste.Image")));
			this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuEditPaste.Name = "mnuEditPaste";
			this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.mnuEditPaste.Size = new System.Drawing.Size(229, 22);
			this.mnuEditPaste.Text = "&Paste";
			this.mnuEditPaste.Click += new System.EventHandler(this.EditPaste_Click);
			// 
			// mnuEditDelete
			// 
			this.mnuEditDelete.Name = "mnuEditDelete";
			this.mnuEditDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.mnuEditDelete.Size = new System.Drawing.Size(229, 22);
			this.mnuEditDelete.Text = "&Delete";
			this.mnuEditDelete.Click += new System.EventHandler(this.EditDelete_Click);
			// 
			// mnuEditSep2
			// 
			this.mnuEditSep2.Name = "mnuEditSep2";
			this.mnuEditSep2.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuEditSelectAll
			// 
			this.mnuEditSelectAll.Name = "mnuEditSelectAll";
			this.mnuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.mnuEditSelectAll.Size = new System.Drawing.Size(229, 22);
			this.mnuEditSelectAll.Text = "Select &All";
			// 
			// mnuEditInvertSelection
			// 
			this.mnuEditInvertSelection.Hidden = true;
			this.mnuEditInvertSelection.Name = "mnuEditInvertSelection";
			this.mnuEditInvertSelection.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.A)));
			this.mnuEditInvertSelection.Size = new System.Drawing.Size(229, 22);
			this.mnuEditInvertSelection.Text = "&Invert Selection";
			// 
			// mnuEditSep3
			// 
			this.mnuEditSep3.Name = "mnuEditSep3";
			this.mnuEditSep3.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuEditFindReplace
			// 
			this.mnuEditFindReplace.Name = "mnuEditFindReplace";
			this.mnuEditFindReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.mnuEditFindReplace.Size = new System.Drawing.Size(229, 22);
			this.mnuEditFindReplace.Text = "&Find/Replace...";
			// 
			// mnuEditGoTo
			// 
			this.mnuEditGoTo.Hidden = true;
			this.mnuEditGoTo.Name = "mnuEditGoTo";
			this.mnuEditGoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.mnuEditGoTo.Size = new System.Drawing.Size(229, 22);
			this.mnuEditGoTo.Text = "&Go To...";
			// 
			// mnuEditSep4
			// 
			this.mnuEditSep4.Name = "mnuEditSep4";
			this.mnuEditSep4.Size = new System.Drawing.Size(226, 6);
			// 
			// mnuEditPreferences
			// 
			this.mnuEditPreferences.Name = "mnuEditPreferences";
			this.mnuEditPreferences.Size = new System.Drawing.Size(229, 22);
			this.mnuEditPreferences.Text = "Prefere&nces...";
			this.mnuEditPreferences.Click += new System.EventHandler(this.ToolsOptions_Click);
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuViewToolbars,
			this.mnuViewStatusBar,
			this.mnuViewSep1,
			this.mnuViewPanels,
			this.mnuViewSep2,
			this.mnuViewStartPage,
			this.mnuViewFullScreen});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(44, 21);
			this.mnuView.Text = "&View";
			// 
			// mnuViewToolbars
			// 
			this.mnuViewToolbars.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuViewToolbarsSep1,
			this.mnuViewToolbarsCustomize});
			this.mnuViewToolbars.Name = "mnuViewToolbars";
			this.mnuViewToolbars.Size = new System.Drawing.Size(156, 22);
			this.mnuViewToolbars.Text = "&Toolbars";
			// 
			// mnuViewToolbarsSep1
			// 
			this.mnuViewToolbarsSep1.Name = "mnuViewToolbarsSep1";
			this.mnuViewToolbarsSep1.Size = new System.Drawing.Size(136, 6);
			// 
			// mnuViewToolbarsCustomize
			// 
			this.mnuViewToolbarsCustomize.Name = "mnuViewToolbarsCustomize";
			this.mnuViewToolbarsCustomize.Size = new System.Drawing.Size(139, 22);
			this.mnuViewToolbarsCustomize.Text = "&Customize...";
			this.mnuViewToolbarsCustomize.Click += new System.EventHandler(this.mnuViewToolbarsCustomize_Click);
			// 
			// mnuViewStatusBar
			// 
			this.mnuViewStatusBar.Checked = true;
			this.mnuViewStatusBar.CheckOnClick = true;
			this.mnuViewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuViewStatusBar.Name = "mnuViewStatusBar";
			this.mnuViewStatusBar.Size = new System.Drawing.Size(156, 22);
			this.mnuViewStatusBar.Text = "Status &Bar";
			this.mnuViewStatusBar.Click += new System.EventHandler(this.mnuViewStatusBar_Click);
			// 
			// mnuViewSep1
			// 
			this.mnuViewSep1.Name = "mnuViewSep1";
			this.mnuViewSep1.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuViewPanels
			// 
			this.mnuViewPanels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolboxToolStripMenuItem,
			this.mnuViewPanelsProjectExplorer,
			this.propertiesToolStripMenuItem});
			this.mnuViewPanels.Name = "mnuViewPanels";
			this.mnuViewPanels.Size = new System.Drawing.Size(156, 22);
			this.mnuViewPanels.Text = "&Panels";
			// 
			// toolboxToolStripMenuItem
			// 
			this.toolboxToolStripMenuItem.Name = "toolboxToolStripMenuItem";
			this.toolboxToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.toolboxToolStripMenuItem.Text = "&Toolbox";
			// 
			// mnuViewPanelsProjectExplorer
			// 
			this.mnuViewPanelsProjectExplorer.Name = "mnuViewPanelsProjectExplorer";
			this.mnuViewPanelsProjectExplorer.Size = new System.Drawing.Size(156, 22);
			this.mnuViewPanelsProjectExplorer.Text = "Project E&xplorer";
			this.mnuViewPanelsProjectExplorer.Click += new System.EventHandler(this.mnuViewPanelsProjectExplorer_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.propertiesToolStripMenuItem.Text = "&Properties";
			// 
			// mnuViewSep2
			// 
			this.mnuViewSep2.Name = "mnuViewSep2";
			this.mnuViewSep2.Size = new System.Drawing.Size(153, 6);
			// 
			// mnuViewStartPage
			// 
			this.mnuViewStartPage.Name = "mnuViewStartPage";
			this.mnuViewStartPage.Size = new System.Drawing.Size(156, 22);
			this.mnuViewStartPage.Text = "Start Pa&ge";
			this.mnuViewStartPage.Click += new System.EventHandler(this.mnuViewStartPage_Click);
			// 
			// mnuViewFullScreen
			// 
			this.mnuViewFullScreen.Name = "mnuViewFullScreen";
			this.mnuViewFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.mnuViewFullScreen.Size = new System.Drawing.Size(156, 22);
			this.mnuViewFullScreen.Text = "F&ull Screen";
			this.mnuViewFullScreen.Click += new System.EventHandler(this.mnuViewFullScreen_Click);
			// 
			// mnuProject
			// 
			this.mnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuProjectAddNewItem,
			this.mnuProjectAddExistingItem,
			this.mnuProjectSep1,
			this.mnuProjectExclude,
			this.mnuProjectShowAllFiles,
			this.mnuProjectSep2,
			this.mnuProjectProperties});
			this.mnuProject.Name = "mnuProject";
			this.mnuProject.Size = new System.Drawing.Size(56, 21);
			this.mnuProject.Text = "&Project";
			// 
			// mnuProjectAddNewItem
			// 
			this.mnuProjectAddNewItem.Name = "mnuProjectAddNewItem";
			this.mnuProjectAddNewItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.A)));
			this.mnuProjectAddNewItem.Size = new System.Drawing.Size(245, 22);
			this.mnuProjectAddNewItem.Text = "Add Ne&w Item...";
			this.mnuProjectAddNewItem.Click += new System.EventHandler(this.mnuProjectAddNewItem_Click);
			// 
			// mnuProjectAddExistingItem
			// 
			this.mnuProjectAddExistingItem.Name = "mnuProjectAddExistingItem";
			this.mnuProjectAddExistingItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.A)));
			this.mnuProjectAddExistingItem.Size = new System.Drawing.Size(245, 22);
			this.mnuProjectAddExistingItem.Text = "Add Existin&g Item...";
			this.mnuProjectAddExistingItem.Click += new System.EventHandler(this.mnuProjectAddExistingItem_Click);
			// 
			// mnuProjectSep1
			// 
			this.mnuProjectSep1.Name = "mnuProjectSep1";
			this.mnuProjectSep1.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuProjectExclude
			// 
			this.mnuProjectExclude.Name = "mnuProjectExclude";
			this.mnuProjectExclude.Size = new System.Drawing.Size(245, 22);
			this.mnuProjectExclude.Text = "Exclude from Pro&ject";
			this.mnuProjectExclude.Click += new System.EventHandler(this.mnuProjectExclude_Click);
			// 
			// mnuProjectShowAllFiles
			// 
			this.mnuProjectShowAllFiles.Name = "mnuProjectShowAllFiles";
			this.mnuProjectShowAllFiles.Size = new System.Drawing.Size(245, 22);
			this.mnuProjectShowAllFiles.Text = "Sh&ow All Files";
			this.mnuProjectShowAllFiles.Click += new System.EventHandler(this.mnuProjectShowAllFiles_Click);
			// 
			// mnuProjectSep2
			// 
			this.mnuProjectSep2.Name = "mnuProjectSep2";
			this.mnuProjectSep2.Size = new System.Drawing.Size(242, 6);
			// 
			// mnuProjectProperties
			// 
			this.mnuProjectProperties.Name = "mnuProjectProperties";
			this.mnuProjectProperties.Size = new System.Drawing.Size(245, 22);
			this.mnuProjectProperties.Text = "Project &Properties...";
			this.mnuProjectProperties.Click += new System.EventHandler(this.mnuProjectProperties_Click);
			// 
			// mnuBookmarks
			// 
			this.mnuBookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuBookmarksAdd,
			this.mnuBookmarksAddAll,
			this.mnuBookmarksSep1,
			this.mnuBookmarksSep2,
			this.mnuBookmarksManage});
			this.mnuBookmarks.Name = "mnuBookmarks";
			this.mnuBookmarks.Size = new System.Drawing.Size(78, 21);
			this.mnuBookmarks.Text = "Book&marks";
			// 
			// mnuBookmarksAdd
			// 
			this.mnuBookmarksAdd.Name = "mnuBookmarksAdd";
			this.mnuBookmarksAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.mnuBookmarksAdd.Size = new System.Drawing.Size(263, 22);
			this.mnuBookmarksAdd.Text = "Add to Boo&kmarks";
			this.mnuBookmarksAdd.Click += new System.EventHandler(this.mnuBookmarksAdd_Click);
			// 
			// mnuBookmarksAddAll
			// 
			this.mnuBookmarksAddAll.Name = "mnuBookmarksAddAll";
			this.mnuBookmarksAddAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.D)));
			this.mnuBookmarksAddAll.Size = new System.Drawing.Size(263, 22);
			this.mnuBookmarksAddAll.Text = "Add &All to Bookmarks";
			this.mnuBookmarksAddAll.Click += new System.EventHandler(this.mnuBookmarksAddAll_Click);
			// 
			// mnuBookmarksSep1
			// 
			this.mnuBookmarksSep1.Name = "mnuBookmarksSep1";
			this.mnuBookmarksSep1.Size = new System.Drawing.Size(260, 6);
			// 
			// mnuBookmarksSep2
			// 
			this.mnuBookmarksSep2.Name = "mnuBookmarksSep2";
			this.mnuBookmarksSep2.Size = new System.Drawing.Size(260, 6);
			// 
			// mnuBookmarksManage
			// 
			this.mnuBookmarksManage.Name = "mnuBookmarksManage";
			this.mnuBookmarksManage.Size = new System.Drawing.Size(263, 22);
			this.mnuBookmarksManage.Text = "&Manage Bookmarks...";
			// 
			// mnuTools
			// 
			this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuToolsSessionManager,
			this.mnuToolsSep1,
			this.mnuToolsCustomize,
			this.mnuToolsOptions});
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(48, 21);
			this.mnuTools.Text = "&Tools";
			// 
			// mnuToolsSessionManager
			// 
			this.mnuToolsSessionManager.Name = "mnuToolsSessionManager";
			this.mnuToolsSessionManager.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
			| System.Windows.Forms.Keys.S)));
			this.mnuToolsSessionManager.Size = new System.Drawing.Size(235, 22);
			this.mnuToolsSessionManager.Text = "&Session Manager...";
			this.mnuToolsSessionManager.Click += new System.EventHandler(this.mnuToolsSessionManager_Click);
			// 
			// mnuToolsSep1
			// 
			this.mnuToolsSep1.Name = "mnuToolsSep1";
			this.mnuToolsSep1.Size = new System.Drawing.Size(232, 6);
			// 
			// mnuToolsCustomize
			// 
			this.mnuToolsCustomize.Name = "mnuToolsCustomize";
			this.mnuToolsCustomize.Size = new System.Drawing.Size(235, 22);
			this.mnuToolsCustomize.Text = "&Customize...";
			this.mnuToolsCustomize.Click += new System.EventHandler(this.ToolsCustomize_Click);
			// 
			// mnuToolsOptions
			// 
			this.mnuToolsOptions.Name = "mnuToolsOptions";
			this.mnuToolsOptions.Size = new System.Drawing.Size(235, 22);
			this.mnuToolsOptions.Text = "&Options...";
			this.mnuToolsOptions.Click += new System.EventHandler(this.ToolsOptions_Click);
			// 
			// mnuWindow
			// 
			this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuWindowNewWindow,
			this.mnuWindowSplit,
			this.mnuWindowSep1,
			this.mnuWindowFloat,
			this.mnuWindowFloatAll,
			this.mnuWindowDock,
			this.mnuWindowAutoHide,
			this.mnuWindowAutoHideAll,
			this.mnuWindowHide,
			this.mnuWindowSep2,
			this.mnuWindowPinTab,
			this.mnuWindowSep3,
			this.mnuWindowNewTabGroupHorizontal,
			this.mnuWindowNewTabGroupVertical,
			this.mnuWindowCloseAllDocuments,
			this.mnuWindowResetLayout,
			this.mnuWindowSep4,
			this.mnuWindowWindows});
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Size = new System.Drawing.Size(63, 21);
			this.mnuWindow.Text = "&Window";
			// 
			// mnuWindowNewWindow
			// 
			this.mnuWindowNewWindow.Name = "mnuWindowNewWindow";
			this.mnuWindowNewWindow.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowNewWindow.Text = "&New Window";
			this.mnuWindowNewWindow.Click += new System.EventHandler(this.mnuWindowNewWindow_Click);
			// 
			// mnuWindowSplit
			// 
			this.mnuWindowSplit.Name = "mnuWindowSplit";
			this.mnuWindowSplit.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowSplit.Text = "S&plit";
			// 
			// mnuWindowSep1
			// 
			this.mnuWindowSep1.Name = "mnuWindowSep1";
			this.mnuWindowSep1.Size = new System.Drawing.Size(212, 6);
			// 
			// mnuWindowFloat
			// 
			this.mnuWindowFloat.Hidden = true;
			this.mnuWindowFloat.Name = "mnuWindowFloat";
			this.mnuWindowFloat.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowFloat.Text = "&Float";
			// 
			// mnuWindowFloatAll
			// 
			this.mnuWindowFloatAll.Hidden = true;
			this.mnuWindowFloatAll.Name = "mnuWindowFloatAll";
			this.mnuWindowFloatAll.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowFloatAll.Text = "Float All";
			// 
			// mnuWindowDock
			// 
			this.mnuWindowDock.Hidden = true;
			this.mnuWindowDock.Name = "mnuWindowDock";
			this.mnuWindowDock.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowDock.Text = "Doc&k";
			// 
			// mnuWindowAutoHide
			// 
			this.mnuWindowAutoHide.Hidden = true;
			this.mnuWindowAutoHide.Name = "mnuWindowAutoHide";
			this.mnuWindowAutoHide.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowAutoHide.Text = "&Auto Hide";
			// 
			// mnuWindowAutoHideAll
			// 
			this.mnuWindowAutoHideAll.Hidden = true;
			this.mnuWindowAutoHideAll.Name = "mnuWindowAutoHideAll";
			this.mnuWindowAutoHideAll.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowAutoHideAll.Text = "A&uto Hide All";
			// 
			// mnuWindowHide
			// 
			this.mnuWindowHide.Hidden = true;
			this.mnuWindowHide.Name = "mnuWindowHide";
			this.mnuWindowHide.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowHide.Text = "&Hide";
			// 
			// mnuWindowSep2
			// 
			this.mnuWindowSep2.Name = "mnuWindowSep2";
			this.mnuWindowSep2.Size = new System.Drawing.Size(212, 6);
			// 
			// mnuWindowPinTab
			// 
			this.mnuWindowPinTab.Name = "mnuWindowPinTab";
			this.mnuWindowPinTab.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowPinTab.Text = "&Pin Tab";
			// 
			// mnuWindowSep3
			// 
			this.mnuWindowSep3.Name = "mnuWindowSep3";
			this.mnuWindowSep3.Size = new System.Drawing.Size(212, 6);
			// 
			// mnuWindowNewTabGroupHorizontal
			// 
			this.mnuWindowNewTabGroupHorizontal.Hidden = true;
			this.mnuWindowNewTabGroupHorizontal.Name = "mnuWindowNewTabGroupHorizontal";
			this.mnuWindowNewTabGroupHorizontal.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowNewTabGroupHorizontal.Text = "New Hori&zontal Tab Group";
			// 
			// mnuWindowNewTabGroupVertical
			// 
			this.mnuWindowNewTabGroupVertical.Hidden = true;
			this.mnuWindowNewTabGroupVertical.Name = "mnuWindowNewTabGroupVertical";
			this.mnuWindowNewTabGroupVertical.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowNewTabGroupVertical.Text = "New &Vertical Tab Group";
			// 
			// mnuWindowCloseAllDocuments
			// 
			this.mnuWindowCloseAllDocuments.Hidden = true;
			this.mnuWindowCloseAllDocuments.Name = "mnuWindowCloseAllDocuments";
			this.mnuWindowCloseAllDocuments.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowCloseAllDocuments.Text = "C&lose All Documents";
			// 
			// mnuWindowResetLayout
			// 
			this.mnuWindowResetLayout.Hidden = true;
			this.mnuWindowResetLayout.Name = "mnuWindowResetLayout";
			this.mnuWindowResetLayout.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowResetLayout.Text = "&Reset Window Layout";
			// 
			// mnuWindowSep4
			// 
			this.mnuWindowSep4.Name = "mnuWindowSep4";
			this.mnuWindowSep4.Size = new System.Drawing.Size(212, 6);
			// 
			// mnuWindowWindows
			// 
			this.mnuWindowWindows.Name = "mnuWindowWindows";
			this.mnuWindowWindows.Size = new System.Drawing.Size(215, 22);
			this.mnuWindowWindows.Text = "&Windows...";
			this.mnuWindowWindows.Click += new System.EventHandler(this.mnuWindowWindows_Click);
			// 
			// tbStandard
			// 
			this.tbStandard.Dock = System.Windows.Forms.DockStyle.None;
			this.tbStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbStandardNew,
			this.tsbStandardOpen,
			this.tsbStandardSave,
			this.tsbStandardPrint,
			this.tsbStandardSep1,
			this.tsbStandardCut,
			this.tsbStandardCopy,
			this.tsbStandardPaste,
			this.tsbStandardSep2,
			this.tsbStandardHelp});
			this.tbStandard.Location = new System.Drawing.Point(3, 25);
			this.tbStandard.Name = "tbStandard";
			this.tbStandard.Size = new System.Drawing.Size(235, 25);
			this.tbStandard.TabIndex = 1;
			this.tbStandard.Text = "Standard";
			// 
			// tsbStandardNew
			// 
			this.tsbStandardNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbStandardNewFile,
			this.tsbStandardNewProject});
			this.tsbStandardNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardNew.Image")));
			this.tsbStandardNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardNew.Name = "tsbStandardNew";
			this.tsbStandardNew.Size = new System.Drawing.Size(32, 22);
			this.tsbStandardNew.Text = "&New";
			this.tsbStandardNew.ButtonClick += new System.EventHandler(this.FileNewFile_Click);
			// 
			// tsbStandardNewFile
			// 
			this.tsbStandardNewFile.Name = "tsbStandardNewFile";
			this.tsbStandardNewFile.Size = new System.Drawing.Size(147, 22);
			this.tsbStandardNewFile.Text = "New &File...";
			this.tsbStandardNewFile.Click += new System.EventHandler(this.FileNewFile_Click);
			// 
			// tsbStandardNewProject
			// 
			this.tsbStandardNewProject.Name = "tsbStandardNewProject";
			this.tsbStandardNewProject.Size = new System.Drawing.Size(147, 22);
			this.tsbStandardNewProject.Text = "New &Project...";
			this.tsbStandardNewProject.Click += new System.EventHandler(this.FileNewProject_Click);
			// 
			// tsbStandardOpen
			// 
			this.tsbStandardOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbStandardOpenFile,
			this.tsbStandardOpenProject});
			this.tsbStandardOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardOpen.Image")));
			this.tsbStandardOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardOpen.Name = "tsbStandardOpen";
			this.tsbStandardOpen.Size = new System.Drawing.Size(32, 22);
			this.tsbStandardOpen.Text = "&Open";
			this.tsbStandardOpen.ButtonClick += new System.EventHandler(this.FileOpenFile_Click);
			// 
			// tsbStandardOpenFile
			// 
			this.tsbStandardOpenFile.Name = "tsbStandardOpenFile";
			this.tsbStandardOpenFile.Size = new System.Drawing.Size(152, 22);
			this.tsbStandardOpenFile.Text = "Open &File...";
			this.tsbStandardOpenFile.Click += new System.EventHandler(this.FileOpenFile_Click);
			// 
			// tsbStandardOpenProject
			// 
			this.tsbStandardOpenProject.Name = "tsbStandardOpenProject";
			this.tsbStandardOpenProject.Size = new System.Drawing.Size(152, 22);
			this.tsbStandardOpenProject.Text = "Open &Project...";
			this.tsbStandardOpenProject.Click += new System.EventHandler(this.FileOpenProject_Click);
			// 
			// tsbStandardSave
			// 
			this.tsbStandardSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbStandardSaveFile,
			this.tsbStandardSaveFileAs,
			this.tsbStandardSaveSep1,
			this.tsbStandardSaveProject,
			this.tsbStandardSaveProjectAs,
			this.tsbStandardSaveSep2,
			this.tsbStandardSaveAll});
			this.tsbStandardSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardSave.Image")));
			this.tsbStandardSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardSave.Name = "tsbStandardSave";
			this.tsbStandardSave.Size = new System.Drawing.Size(32, 22);
			this.tsbStandardSave.Text = "&Save";
			this.tsbStandardSave.ButtonClick += new System.EventHandler(this.FileSaveFile_Click);
			// 
			// tsbStandardSaveFile
			// 
			this.tsbStandardSaveFile.Name = "tsbStandardSaveFile";
			this.tsbStandardSaveFile.Size = new System.Drawing.Size(209, 22);
			this.tsbStandardSaveFile.Text = "Save &File";
			this.tsbStandardSaveFile.Click += new System.EventHandler(this.FileSaveFile_Click);
			// 
			// tsbStandardSaveFileAs
			// 
			this.tsbStandardSaveFileAs.Name = "tsbStandardSaveFileAs";
			this.tsbStandardSaveFileAs.Size = new System.Drawing.Size(209, 22);
			this.tsbStandardSaveFileAs.Text = "Save File &As...";
			this.tsbStandardSaveFileAs.Click += new System.EventHandler(this.FileSaveFileAs_Click);
			// 
			// tsbStandardSaveSep1
			// 
			this.tsbStandardSaveSep1.Name = "tsbStandardSaveSep1";
			this.tsbStandardSaveSep1.Size = new System.Drawing.Size(206, 6);
			// 
			// tsbStandardSaveProject
			// 
			this.tsbStandardSaveProject.Name = "tsbStandardSaveProject";
			this.tsbStandardSaveProject.Size = new System.Drawing.Size(209, 22);
			this.tsbStandardSaveProject.Text = "Save &Project";
			// 
			// tsbStandardSaveProjectAs
			// 
			this.tsbStandardSaveProjectAs.Name = "tsbStandardSaveProjectAs";
			this.tsbStandardSaveProjectAs.Size = new System.Drawing.Size(209, 22);
			this.tsbStandardSaveProjectAs.Text = "Save Pro&ject As...";
			// 
			// tsbStandardSaveSep2
			// 
			this.tsbStandardSaveSep2.Name = "tsbStandardSaveSep2";
			this.tsbStandardSaveSep2.Size = new System.Drawing.Size(206, 6);
			// 
			// tsbStandardSaveAll
			// 
			this.tsbStandardSaveAll.Name = "tsbStandardSaveAll";
			this.tsbStandardSaveAll.Size = new System.Drawing.Size(209, 22);
			this.tsbStandardSaveAll.Text = "Save A&ll Files and Projects";
			// 
			// tsbStandardPrint
			// 
			this.tsbStandardPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardPrint.Image")));
			this.tsbStandardPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardPrint.Name = "tsbStandardPrint";
			this.tsbStandardPrint.Size = new System.Drawing.Size(23, 22);
			this.tsbStandardPrint.Text = "&Print";
			this.tsbStandardPrint.Click += new System.EventHandler(this.FilePrint_Click);
			// 
			// tsbStandardSep1
			// 
			this.tsbStandardSep1.Name = "tsbStandardSep1";
			this.tsbStandardSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbStandardCut
			// 
			this.tsbStandardCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardCut.Image")));
			this.tsbStandardCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardCut.Name = "tsbStandardCut";
			this.tsbStandardCut.Size = new System.Drawing.Size(23, 22);
			this.tsbStandardCut.Text = "C&ut";
			this.tsbStandardCut.Click += new System.EventHandler(this.EditCut_Click);
			// 
			// tsbStandardCopy
			// 
			this.tsbStandardCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardCopy.Image")));
			this.tsbStandardCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardCopy.Name = "tsbStandardCopy";
			this.tsbStandardCopy.Size = new System.Drawing.Size(23, 22);
			this.tsbStandardCopy.Text = "&Copy";
			this.tsbStandardCopy.Click += new System.EventHandler(this.EditCopy_Click);
			// 
			// tsbStandardPaste
			// 
			this.tsbStandardPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardPaste.Image")));
			this.tsbStandardPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardPaste.Name = "tsbStandardPaste";
			this.tsbStandardPaste.Size = new System.Drawing.Size(23, 22);
			this.tsbStandardPaste.Text = "&Paste";
			this.tsbStandardPaste.Click += new System.EventHandler(this.EditPaste_Click);
			// 
			// tsbStandardSep2
			// 
			this.tsbStandardSep2.Name = "tsbStandardSep2";
			this.tsbStandardSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbStandardHelp
			// 
			this.tsbStandardHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStandardHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandardHelp.Image")));
			this.tsbStandardHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStandardHelp.Name = "tsbStandardHelp";
			this.tsbStandardHelp.Size = new System.Drawing.Size(23, 22);
			this.tsbStandardHelp.Text = "He&lp";
			this.tsbStandardHelp.Click += new System.EventHandler(this.HelpContents_Click);
			// 
			// tmrToolStripContainerPopup
			// 
			this.tmrToolStripContainerPopup.Interval = 50;
			this.tmrToolStripContainerPopup.Tick += new System.EventHandler(this.tmrToolStripContainerPopup_Tick);
			// 
			// mnuContextDocumentType
			// 
			this.mnuContextDocumentType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuContextDocumentTypeDataFormat,
			this.mnuContextDocumentTypeSep1});
			this.mnuContextDocumentType.Name = "mnuContextDataFormat";
			this.mnuContextDocumentType.Size = new System.Drawing.Size(138, 32);
			// 
			// mnuContextDocumentTypeDataFormat
			// 
			this.mnuContextDocumentTypeDataFormat.Enabled = false;
			this.mnuContextDocumentTypeDataFormat.Name = "mnuContextDocumentTypeDataFormat";
			this.mnuContextDocumentTypeDataFormat.Size = new System.Drawing.Size(137, 22);
			this.mnuContextDocumentTypeDataFormat.Text = "&Data format";
			// 
			// mnuContextDocumentTypeSep1
			// 
			this.mnuContextDocumentTypeSep1.Name = "mnuContextDocumentTypeSep1";
			this.mnuContextDocumentTypeSep1.Size = new System.Drawing.Size(134, 6);
			// 
			// MainWindow
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.cbc);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.mbMenuBar;
			this.Name = "MainWindow";
			this.Text = "Universal Editor";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
			this.cbc.BottomToolStripPanel.ResumeLayout(false);
			this.cbc.BottomToolStripPanel.PerformLayout();
			this.cbc.ContentPanel.ResumeLayout(false);
			this.cbc.TopToolStripPanel.ResumeLayout(false);
			this.cbc.TopToolStripPanel.PerformLayout();
			this.cbc.ResumeLayout(false);
			this.cbc.PerformLayout();
			this.sbStatusBar.ResumeLayout(false);
			this.sbStatusBar.PerformLayout();
			this.mbMenuBar.ResumeLayout(false);
			this.mbMenuBar.PerformLayout();
			this.tbStandard.ResumeLayout(false);
			this.tbStandard.PerformLayout();
			this.mnuContextDocumentType.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ToolStripSeparator mnuContextDocumentTypeSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuContextDocumentTypeDataFormat;
		private AwesomeControls.CommandBars.CBContextMenu mnuContextDocumentType;
		private System.Windows.Forms.ToolStripStatusLabel lblObjectModel;

		#endregion

		private AwesomeControls.CommandBars.CBContainer cbc;
		private AwesomeControls.CommandBars.CBStatusBar sbStatusBar;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private AwesomeControls.CommandBars.CBMenuBar mbMenuBar;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuEditUndo;
		private System.Windows.Forms.ToolStripMenuItem mnuEditRedo;
		private System.Windows.Forms.ToolStripSeparator mnuEditSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
		private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
		private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
		private System.Windows.Forms.ToolStripSeparator mnuEditSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsCustomize;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewToolbars;
		private System.Windows.Forms.ToolStripMenuItem mnuViewStatusBar;
		private AwesomeControls.CommandBars.CBToolBar tbStandard;
		private System.Windows.Forms.ToolStripButton tsbStandardPrint;
		private System.Windows.Forms.ToolStripSeparator tsbStandardSep1;
		private System.Windows.Forms.ToolStripButton tsbStandardCut;
		private System.Windows.Forms.ToolStripButton tsbStandardCopy;
		private System.Windows.Forms.ToolStripButton tsbStandardPaste;
		private System.Windows.Forms.ToolStripSeparator tsbStandardSep2;
		private System.Windows.Forms.ToolStripButton tsbStandardHelp;
		private System.Windows.Forms.ToolStripSeparator mnuViewToolbarsSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuViewToolbarsCustomize;
		private System.Windows.Forms.ToolStripMenuItem mnuEditDelete;
		private System.Windows.Forms.ToolStripSeparator mnuEditSep3;
		private System.Windows.Forms.ToolStripMenuItem mnuEditFindReplace;
		private System.Windows.Forms.ToolStripSeparator mnuViewSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuViewFullScreen;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveFileAs;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAll;
		private System.Windows.Forms.ToolStripSeparator mnuFileSaveSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveProject;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveProjectAs;
		private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
		private System.Windows.Forms.ToolStripMenuItem mnuFileNewFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileNewProject;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpenFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpenProject;
		private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
		private System.Windows.Forms.ToolStripMenuItem mnuFileCloseFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileCloseProject;
		private System.Windows.Forms.ToolStripMenuItem mnuWindow;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowNewWindow;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowSplit;
		private System.Windows.Forms.ToolStripSeparator mnuWindowSep1;
		private System.Windows.Forms.ToolStripSeparator mnuWindowSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowPinTab;
		private System.Windows.Forms.ToolStripSeparator mnuWindowSep3;
		private System.Windows.Forms.ToolStripSeparator mnuWindowSep4;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowWindows;
		private System.Windows.Forms.ToolStripMenuItem mnuViewStartPage;
		private System.Windows.Forms.ToolStripMenuItem mnuFileRecentFiles;
		private System.Windows.Forms.ToolStripMenuItem mnuFileRecentProjects;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep3;
		private System.Windows.Forms.ToolStripSeparator mnuEditSep4;
		private System.Windows.Forms.ToolStripMenuItem mnuEditPreferences;
		private System.Windows.Forms.ToolStripSplitButton tsbStandardNew;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardNewFile;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardNewProject;
		private System.Windows.Forms.ToolStripSplitButton tsbStandardOpen;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardOpenFile;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardOpenProject;
		private System.Windows.Forms.ToolStripSplitButton tsbStandardSave;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardSaveFile;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardSaveFileAs;
		private System.Windows.Forms.ToolStripSeparator tsbStandardSaveSep1;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardSaveProject;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardSaveProjectAs;
		private System.Windows.Forms.ToolStripSeparator tsbStandardSaveSep2;
		private System.Windows.Forms.ToolStripMenuItem tsbStandardSaveAll;
		private System.Windows.Forms.ToolStripMenuItem mnuProject;
		private System.Windows.Forms.ToolStripMenuItem mnuProjectAddNewItem;
		private System.Windows.Forms.ToolStripMenuItem mnuProjectAddExistingItem;
		private System.Windows.Forms.ToolStripSeparator mnuProjectSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuProjectExclude;
		private System.Windows.Forms.ToolStripMenuItem mnuProjectShowAllFiles;
		private System.Windows.Forms.ToolStripSeparator mnuProjectSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuProjectProperties;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep4;
		private System.Windows.Forms.Timer tmrToolStripContainerPopup;
		private System.Windows.Forms.ToolStripMenuItem mnuViewPanels;
		private System.Windows.Forms.ToolStripMenuItem toolboxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuViewPanelsProjectExplorer;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator mnuViewSep2;
		private System.Windows.Forms.ComboBox cboAddress;
		private System.Windows.Forms.ToolStripMenuItem mnuBookmarks;
		private System.Windows.Forms.ToolStripMenuItem mnuBookmarksAdd;
		private System.Windows.Forms.ToolStripMenuItem mnuBookmarksAddAll;
		private System.Windows.Forms.ToolStripSeparator mnuBookmarksSep1;
		private System.Windows.Forms.ToolStripSeparator mnuBookmarksSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuBookmarksManage;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsSessionManager;
		private System.Windows.Forms.ToolStripSeparator mnuToolsSep1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem mnuFileCloseWindow;
		private System.Windows.Forms.ToolStripProgressBar pbProgress;
		private AwesomeControls.DockingWindows.DockingContainerControl dcc;
		private System.Windows.Forms.ToolStripSeparator mnuFileSaveSep2;
		private AwesomeControls.CommandBars.CBMenuItem mnuFileImport;
		private AwesomeControls.CommandBars.CBMenuItem mnuFileExport;
		private AwesomeControls.CommandBars.CBMenuItem mnuEditInvertSelection;
		private AwesomeControls.CommandBars.CBMenuItem mnuEditGoTo;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowFloat;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowFloatAll;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowDock;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowAutoHide;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowAutoHideAll;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowHide;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowNewTabGroupHorizontal;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowNewTabGroupVertical;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowCloseAllDocuments;
		private AwesomeControls.CommandBars.CBMenuItem mnuWindowResetLayout;
	}
}

