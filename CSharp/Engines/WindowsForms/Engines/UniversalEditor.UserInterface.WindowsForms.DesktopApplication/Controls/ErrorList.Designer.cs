namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
	partial class ErrorList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorList));
			this.cbToolBar1 = new AwesomeControls.CommandBars.CBToolBar();
			this.tsbFilter = new System.Windows.Forms.ToolStripSplitButton();
			this.onlyShowOpenDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showEfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.currentDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbErrors = new System.Windows.Forms.ToolStripButton();
			this.tsbSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbWarnings = new System.Windows.Forms.ToolStripButton();
			this.tsbSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbMessages = new System.Windows.Forms.ToolStripButton();
			this.tsbSearch = new System.Windows.Forms.ToolStripButton();
			this.cboSearch = new System.Windows.Forms.ToolStripComboBox();
			this.lv = new System.Windows.Forms.ListView();
			this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chLineNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chColumnNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mnuContext = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextSortBy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextShowColumns = new System.Windows.Forms.ToolStripMenuItem();
			this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.columnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showErrorHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteSpecialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteJSONAsClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.nextErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.previousErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cbToolBar1.SuspendLayout();
			this.mnuContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbToolBar1
			// 
			this.cbToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.cbToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbFilter,
			this.tsbSep1,
			this.tsbErrors,
			this.tsbSep2,
			this.tsbWarnings,
			this.tsbSep3,
			this.tsbMessages,
			this.tsbSearch,
			this.cboSearch});
			this.cbToolBar1.Location = new System.Drawing.Point(0, 0);
			this.cbToolBar1.Name = "cbToolBar1";
			this.cbToolBar1.Size = new System.Drawing.Size(557, 25);
			this.cbToolBar1.TabIndex = 0;
			this.cbToolBar1.Text = "cbToolBar1";
			// 
			// tsbFilter
			// 
			this.tsbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.onlyShowOpenDocumentsToolStripMenuItem,
			this.showEfToolStripMenuItem,
			this.currentDocumentToolStripMenuItem});
			this.tsbFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbFilter.Image")));
			this.tsbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbFilter.Name = "tsbFilter";
			this.tsbFilter.Size = new System.Drawing.Size(32, 22);
			this.tsbFilter.Text = "toolStripSplitButton1";
			// 
			// onlyShowOpenDocumentsToolStripMenuItem
			// 
			this.onlyShowOpenDocumentsToolStripMenuItem.Name = "onlyShowOpenDocumentsToolStripMenuItem";
			this.onlyShowOpenDocumentsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.onlyShowOpenDocumentsToolStripMenuItem.Text = "&Open Documents";
			// 
			// showEfToolStripMenuItem
			// 
			this.showEfToolStripMenuItem.Name = "showEfToolStripMenuItem";
			this.showEfToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.showEfToolStripMenuItem.Text = "Current &Project";
			// 
			// currentDocumentToolStripMenuItem
			// 
			this.currentDocumentToolStripMenuItem.Name = "currentDocumentToolStripMenuItem";
			this.currentDocumentToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.currentDocumentToolStripMenuItem.Text = "Current &Document";
			// 
			// tsbSep1
			// 
			this.tsbSep1.Name = "tsbSep1";
			this.tsbSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbErrors
			// 
			this.tsbErrors.Checked = true;
			this.tsbErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbErrors.Image = ((System.Drawing.Image)(resources.GetObject("tsbErrors.Image")));
			this.tsbErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbErrors.Name = "tsbErrors";
			this.tsbErrors.Size = new System.Drawing.Size(66, 22);
			this.tsbErrors.Text = "0 Errors";
			// 
			// tsbSep2
			// 
			this.tsbSep2.Name = "tsbSep2";
			this.tsbSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbWarnings
			// 
			this.tsbWarnings.Checked = true;
			this.tsbWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbWarnings.Image = ((System.Drawing.Image)(resources.GetObject("tsbWarnings.Image")));
			this.tsbWarnings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbWarnings.Name = "tsbWarnings";
			this.tsbWarnings.Size = new System.Drawing.Size(86, 22);
			this.tsbWarnings.Text = "0 Warnings";
			// 
			// tsbSep3
			// 
			this.tsbSep3.Name = "tsbSep3";
			this.tsbSep3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbMessages
			// 
			this.tsbMessages.Checked = true;
			this.tsbMessages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbMessages.Image = ((System.Drawing.Image)(resources.GetObject("tsbMessages.Image")));
			this.tsbMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbMessages.Name = "tsbMessages";
			this.tsbMessages.Size = new System.Drawing.Size(87, 22);
			this.tsbMessages.Text = "0 Messages";
			// 
			// tsbSearch
			// 
			this.tsbSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbSearch.Image")));
			this.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSearch.Name = "tsbSearch";
			this.tsbSearch.Size = new System.Drawing.Size(23, 22);
			this.tsbSearch.Text = "toolStripButton4";
			// 
			// cboSearch
			// 
			this.cboSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cboSearch.Name = "cboSearch";
			this.cboSearch.Size = new System.Drawing.Size(160, 25);
			// 
			// lv
			// 
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.chIndex,
			this.chDescription,
			this.chFileName,
			this.chLineNumber,
			this.chColumnNumber,
			this.chProjectName});
			this.lv.ContextMenuStrip = this.mnuContext;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(0, 25);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(557, 230);
			this.lv.TabIndex = 1;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// chIndex
			// 
			this.chIndex.Text = "";
			this.chIndex.Width = 45;
			// 
			// chDescription
			// 
			this.chDescription.Text = "Description";
			this.chDescription.Width = 223;
			// 
			// chFileName
			// 
			this.chFileName.Text = "File";
			this.chFileName.Width = 95;
			// 
			// chLineNumber
			// 
			this.chLineNumber.Text = "Line";
			// 
			// chColumnNumber
			// 
			this.chColumnNumber.Text = "Column";
			// 
			// chProjectName
			// 
			this.chProjectName.Text = "Project";
			this.chProjectName.Width = 68;
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuContextSortBy,
			this.mnuContextShowColumns,
			this.showErrorHelpToolStripMenuItem,
			this.toolStripMenuItem1,
			this.copyToolStripMenuItem,
			this.pasteSpecialToolStripMenuItem,
			this.toolStripMenuItem9,
			this.nextErrorToolStripMenuItem,
			this.previousErrorToolStripMenuItem});
			this.mnuContext.Name = "mnuContext";
			this.mnuContext.Size = new System.Drawing.Size(160, 170);
			// 
			// mnuContextSortBy
			// 
			this.mnuContextSortBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItem2,
			this.toolStripMenuItem3,
			this.toolStripMenuItem4,
			this.toolStripMenuItem5,
			this.toolStripMenuItem6,
			this.toolStripMenuItem7,
			this.toolStripMenuItem8});
			this.mnuContextSortBy.Name = "mnuContextSortBy";
			this.mnuContextSortBy.Size = new System.Drawing.Size(159, 22);
			this.mnuContextSortBy.Text = "&Sort By";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem2.Text = "&Category";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem3.Text = "Default &Order";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem4.Text = "&Description";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem5.Text = "&File";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem6.Text = "&Line";
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem7.Text = "Colu&mn";
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(145, 22);
			this.toolStripMenuItem8.Text = "&Project";
			// 
			// mnuContextShowColumns
			// 
			this.mnuContextShowColumns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.categoryToolStripMenuItem,
			this.dToolStripMenuItem,
			this.descriptionToolStripMenuItem,
			this.fileToolStripMenuItem,
			this.lineToolStripMenuItem,
			this.columnToolStripMenuItem,
			this.projectToolStripMenuItem});
			this.mnuContextShowColumns.Name = "mnuContextShowColumns";
			this.mnuContextShowColumns.Size = new System.Drawing.Size(159, 22);
			this.mnuContextShowColumns.Text = "Show &Columns";
			// 
			// categoryToolStripMenuItem
			// 
			this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
			this.categoryToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.categoryToolStripMenuItem.Text = "&Category";
			// 
			// dToolStripMenuItem
			// 
			this.dToolStripMenuItem.Name = "dToolStripMenuItem";
			this.dToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.dToolStripMenuItem.Text = "Default &Order";
			// 
			// descriptionToolStripMenuItem
			// 
			this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
			this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.descriptionToolStripMenuItem.Text = "&Description";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// lineToolStripMenuItem
			// 
			this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
			this.lineToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.lineToolStripMenuItem.Text = "&Line";
			// 
			// columnToolStripMenuItem
			// 
			this.columnToolStripMenuItem.Name = "columnToolStripMenuItem";
			this.columnToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.columnToolStripMenuItem.Text = "Colu&mn";
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.projectToolStripMenuItem.Text = "&Project";
			// 
			// showErrorHelpToolStripMenuItem
			// 
			this.showErrorHelpToolStripMenuItem.Name = "showErrorHelpToolStripMenuItem";
			this.showErrorHelpToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.showErrorHelpToolStripMenuItem.Text = "Show Error &Help";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.copyToolStripMenuItem.Text = "Cop&y";
			// 
			// pasteSpecialToolStripMenuItem
			// 
			this.pasteSpecialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.pasteJSONAsClassesToolStripMenuItem});
			this.pasteSpecialToolStripMenuItem.Name = "pasteSpecialToolStripMenuItem";
			this.pasteSpecialToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.pasteSpecialToolStripMenuItem.Text = "Paste &Special";
			// 
			// pasteJSONAsClassesToolStripMenuItem
			// 
			this.pasteJSONAsClassesToolStripMenuItem.Name = "pasteJSONAsClassesToolStripMenuItem";
			this.pasteJSONAsClassesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.pasteJSONAsClassesToolStripMenuItem.Text = "Paste &JSON As Classes";
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(156, 6);
			// 
			// nextErrorToolStripMenuItem
			// 
			this.nextErrorToolStripMenuItem.Name = "nextErrorToolStripMenuItem";
			this.nextErrorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.nextErrorToolStripMenuItem.Text = "Ne&xt Error";
			// 
			// previousErrorToolStripMenuItem
			// 
			this.previousErrorToolStripMenuItem.Name = "previousErrorToolStripMenuItem";
			this.previousErrorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.previousErrorToolStripMenuItem.Text = "P&revious Error";
			// 
			// ErrorList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lv);
			this.Controls.Add(this.cbToolBar1);
			this.Name = "ErrorList";
			this.Size = new System.Drawing.Size(557, 255);
			this.cbToolBar1.ResumeLayout(false);
			this.cbToolBar1.PerformLayout();
			this.mnuContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AwesomeControls.CommandBars.CBToolBar cbToolBar1;
		private System.Windows.Forms.ToolStripSplitButton tsbFilter;
		private System.Windows.Forms.ToolStripMenuItem onlyShowOpenDocumentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showEfToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem currentDocumentToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator tsbSep1;
		private System.Windows.Forms.ToolStripButton tsbErrors;
		private System.Windows.Forms.ToolStripSeparator tsbSep2;
		private System.Windows.Forms.ToolStripButton tsbWarnings;
		private System.Windows.Forms.ToolStripSeparator tsbSep3;
		private System.Windows.Forms.ToolStripButton tsbMessages;
		private System.Windows.Forms.ToolStripButton tsbSearch;
		private System.Windows.Forms.ToolStripComboBox cboSearch;
		private System.Windows.Forms.ColumnHeader chIndex;
		private System.Windows.Forms.ColumnHeader chDescription;
		private System.Windows.Forms.ColumnHeader chFileName;
		private System.Windows.Forms.ColumnHeader chLineNumber;
		private System.Windows.Forms.ColumnHeader chColumnNumber;
		private System.Windows.Forms.ColumnHeader chProjectName;
		internal System.Windows.Forms.ListView lv;
		private AwesomeControls.CommandBars.CBContextMenu mnuContext;
		private System.Windows.Forms.ToolStripMenuItem mnuContextShowColumns;
		private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem columnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuContextSortBy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem showErrorHelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteSpecialToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteJSONAsClassesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem nextErrorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem previousErrorToolStripMenuItem;
	}
}
