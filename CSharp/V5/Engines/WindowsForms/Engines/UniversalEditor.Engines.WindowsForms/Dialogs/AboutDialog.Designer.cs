using System.Drawing;
namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
    partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblApplicationTitle = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.tbsTabs = new System.Windows.Forms.TabControl();
			this.tabAuthors = new System.Windows.Forms.TabPage();
			this.txtContributors = new System.Windows.Forms.TextBox();
			this.tabInstalledComponents = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tvComponents = new System.Windows.Forms.TreeView();
			this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.pnlAssemblyInfo = new System.Windows.Forms.Panel();
			this.cmdOpenContainingFolder = new System.Windows.Forms.Button();
			this.txtAssemblyLocation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAssemblyFullName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.fraDescription = new System.Windows.Forms.GroupBox();
			this.txtAssemblyDescription = new System.Windows.Forms.TextBox();
			this.pnlDataFormatInfo = new System.Windows.Forms.Panel();
			this.fraDataFormatContentTypes = new System.Windows.Forms.GroupBox();
			this.lvDataFormatContentTypes = new System.Windows.Forms.ListView();
			this.chDataFormatContentType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraDataFormatFilters = new System.Windows.Forms.GroupBox();
			this.lvDataFormatFilters = new System.Windows.Forms.ListView();
			this.chDataFormatFilterTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDataFormatFilterFilters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.txtDataFormatTypeName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtDataFormatID = new System.Windows.Forms.TextBox();
			this.lblDataFormatID = new System.Windows.Forms.Label();
			this.pnlObjectModelInfo = new System.Windows.Forms.Panel();
			this.txtObjectModelTitle = new System.Windows.Forms.TextBox();
			this.lblObjectModelTitle = new System.Windows.Forms.Label();
			this.txtObjectModelTypeName = new System.Windows.Forms.TextBox();
			this.lblObjectModelTypeName = new System.Windows.Forms.Label();
			this.txtObjectModelID = new System.Windows.Forms.TextBox();
			this.lblObjectModelID = new System.Windows.Forms.Label();
			this.tabLicense = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.lblPlatform = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tbsTabs.SuspendLayout();
			this.tabAuthors.SuspendLayout();
			this.tabInstalledComponents.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlAssemblyInfo.SuspendLayout();
			this.fraDescription.SuspendLayout();
			this.pnlDataFormatInfo.SuspendLayout();
			this.fraDataFormatContentTypes.SuspendLayout();
			this.fraDataFormatFilters.SuspendLayout();
			this.pnlObjectModelInfo.SuspendLayout();
			this.tabLicense.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblApplicationTitle
			// 
			this.lblApplicationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblApplicationTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblApplicationTitle.Location = new System.Drawing.Point(50, 12);
			this.lblApplicationTitle.Name = "lblApplicationTitle";
			this.lblApplicationTitle.Size = new System.Drawing.Size(470, 19);
			this.lblApplicationTitle.TabIndex = 1;
			this.lblApplicationTitle.Text = "Universal Editor";
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblVersion.Location = new System.Drawing.Point(50, 31);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(470, 13);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "Version 1.0";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(13, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(507, 2);
			this.label1.TabIndex = 2;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(445, 378);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// tbsTabs
			// 
			this.tbsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbsTabs.Controls.Add(this.tabAuthors);
			this.tbsTabs.Controls.Add(this.tabInstalledComponents);
			this.tbsTabs.Controls.Add(this.tabLicense);
			this.tbsTabs.Location = new System.Drawing.Point(12, 65);
			this.tbsTabs.Name = "tbsTabs";
			this.tbsTabs.SelectedIndex = 0;
			this.tbsTabs.Size = new System.Drawing.Size(508, 307);
			this.tbsTabs.TabIndex = 4;
			// 
			// tabAuthors
			// 
			this.tabAuthors.Controls.Add(this.txtContributors);
			this.tabAuthors.Location = new System.Drawing.Point(4, 22);
			this.tabAuthors.Name = "tabAuthors";
			this.tabAuthors.Padding = new System.Windows.Forms.Padding(3);
			this.tabAuthors.Size = new System.Drawing.Size(500, 281);
			this.tabAuthors.TabIndex = 0;
			this.tabAuthors.Text = "Contributors";
			this.tabAuthors.UseVisualStyleBackColor = true;
			// 
			// txtContributors
			// 
			this.txtContributors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtContributors.Location = new System.Drawing.Point(3, 3);
			this.txtContributors.Multiline = true;
			this.txtContributors.Name = "txtContributors";
			this.txtContributors.ReadOnly = true;
			this.txtContributors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtContributors.Size = new System.Drawing.Size(494, 275);
			this.txtContributors.TabIndex = 0;
			this.txtContributors.Text = "Michael Becker, Lead Developer";
			// 
			// tabInstalledComponents
			// 
			this.tabInstalledComponents.Controls.Add(this.splitContainer1);
			this.tabInstalledComponents.Location = new System.Drawing.Point(4, 22);
			this.tabInstalledComponents.Name = "tabInstalledComponents";
			this.tabInstalledComponents.Padding = new System.Windows.Forms.Padding(3);
			this.tabInstalledComponents.Size = new System.Drawing.Size(500, 281);
			this.tabInstalledComponents.TabIndex = 1;
			this.tabInstalledComponents.Text = "Installed Components";
			this.tabInstalledComponents.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tvComponents);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pnlAssemblyInfo);
			this.splitContainer1.Panel2.Controls.Add(this.pnlDataFormatInfo);
			this.splitContainer1.Panel2.Controls.Add(this.pnlObjectModelInfo);
			this.splitContainer1.Size = new System.Drawing.Size(494, 275);
			this.splitContainer1.SplitterDistance = 261;
			this.splitContainer1.TabIndex = 1;
			// 
			// tvComponents
			// 
			this.tvComponents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvComponents.ImageIndex = 0;
			this.tvComponents.ImageList = this.imlSmallIcons;
			this.tvComponents.Location = new System.Drawing.Point(0, 0);
			this.tvComponents.Name = "tvComponents";
			this.tvComponents.SelectedImageIndex = 0;
			this.tvComponents.Size = new System.Drawing.Size(261, 275);
			this.tvComponents.TabIndex = 0;
			this.tvComponents.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvComponents_AfterCollapse);
			this.tvComponents.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvComponents_AfterExpand);
			this.tvComponents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvComponents_AfterSelect);
			// 
			// imlSmallIcons
			// 
			this.imlSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcons.ImageStream")));
			this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlSmallIcons.Images.SetKeyName(0, "LibraryClosed");
			this.imlSmallIcons.Images.SetKeyName(1, "LibraryOpen");
			this.imlSmallIcons.Images.SetKeyName(2, "DataFormat");
			this.imlSmallIcons.Images.SetKeyName(3, "ObjectModel");
			// 
			// pnlAssemblyInfo
			// 
			this.pnlAssemblyInfo.Controls.Add(this.cmdOpenContainingFolder);
			this.pnlAssemblyInfo.Controls.Add(this.txtAssemblyLocation);
			this.pnlAssemblyInfo.Controls.Add(this.label4);
			this.pnlAssemblyInfo.Controls.Add(this.txtAssemblyFullName);
			this.pnlAssemblyInfo.Controls.Add(this.label3);
			this.pnlAssemblyInfo.Controls.Add(this.fraDescription);
			this.pnlAssemblyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlAssemblyInfo.Enabled = false;
			this.pnlAssemblyInfo.Location = new System.Drawing.Point(0, 0);
			this.pnlAssemblyInfo.Name = "pnlAssemblyInfo";
			this.pnlAssemblyInfo.Size = new System.Drawing.Size(229, 275);
			this.pnlAssemblyInfo.TabIndex = 2;
			this.pnlAssemblyInfo.Visible = false;
			// 
			// cmdOpenContainingFolder
			// 
			this.cmdOpenContainingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOpenContainingFolder.AutoSize = true;
			this.cmdOpenContainingFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOpenContainingFolder.Location = new System.Drawing.Point(95, 55);
			this.cmdOpenContainingFolder.Name = "cmdOpenContainingFolder";
			this.cmdOpenContainingFolder.Size = new System.Drawing.Size(132, 23);
			this.cmdOpenContainingFolder.TabIndex = 4;
			this.cmdOpenContainingFolder.Text = "Open Containing Folder";
			this.cmdOpenContainingFolder.UseVisualStyleBackColor = true;
			this.cmdOpenContainingFolder.Click += new System.EventHandler(this.cmdOpenContainingFolder_Click);
			// 
			// txtAssemblyLocation
			// 
			this.txtAssemblyLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAssemblyLocation.Location = new System.Drawing.Point(64, 29);
			this.txtAssemblyLocation.Name = "txtAssemblyLocation";
			this.txtAssemblyLocation.ReadOnly = true;
			this.txtAssemblyLocation.Size = new System.Drawing.Size(162, 20);
			this.txtAssemblyLocation.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Location:";
			// 
			// txtAssemblyFullName
			// 
			this.txtAssemblyFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAssemblyFullName.Location = new System.Drawing.Point(64, 3);
			this.txtAssemblyFullName.Name = "txtAssemblyFullName";
			this.txtAssemblyFullName.ReadOnly = true;
			this.txtAssemblyFullName.Size = new System.Drawing.Size(162, 20);
			this.txtAssemblyFullName.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Full name:";
			// 
			// fraDescription
			// 
			this.fraDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraDescription.Controls.Add(this.txtAssemblyDescription);
			this.fraDescription.Location = new System.Drawing.Point(3, 84);
			this.fraDescription.Name = "fraDescription";
			this.fraDescription.Size = new System.Drawing.Size(223, 188);
			this.fraDescription.TabIndex = 1;
			this.fraDescription.TabStop = false;
			this.fraDescription.Text = "Description";
			// 
			// txtAssemblyDescription
			// 
			this.txtAssemblyDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAssemblyDescription.HideSelection = false;
			this.txtAssemblyDescription.Location = new System.Drawing.Point(6, 19);
			this.txtAssemblyDescription.Multiline = true;
			this.txtAssemblyDescription.Name = "txtAssemblyDescription";
			this.txtAssemblyDescription.ReadOnly = true;
			this.txtAssemblyDescription.Size = new System.Drawing.Size(211, 163);
			this.txtAssemblyDescription.TabIndex = 0;
			// 
			// pnlDataFormatInfo
			// 
			this.pnlDataFormatInfo.Controls.Add(this.fraDataFormatContentTypes);
			this.pnlDataFormatInfo.Controls.Add(this.fraDataFormatFilters);
			this.pnlDataFormatInfo.Controls.Add(this.txtDataFormatTypeName);
			this.pnlDataFormatInfo.Controls.Add(this.label6);
			this.pnlDataFormatInfo.Controls.Add(this.txtDataFormatID);
			this.pnlDataFormatInfo.Controls.Add(this.lblDataFormatID);
			this.pnlDataFormatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDataFormatInfo.Enabled = false;
			this.pnlDataFormatInfo.Location = new System.Drawing.Point(0, 0);
			this.pnlDataFormatInfo.Name = "pnlDataFormatInfo";
			this.pnlDataFormatInfo.Size = new System.Drawing.Size(229, 275);
			this.pnlDataFormatInfo.TabIndex = 4;
			this.pnlDataFormatInfo.Visible = false;
			// 
			// fraDataFormatContentTypes
			// 
			this.fraDataFormatContentTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraDataFormatContentTypes.Controls.Add(this.lvDataFormatContentTypes);
			this.fraDataFormatContentTypes.Location = new System.Drawing.Point(3, 55);
			this.fraDataFormatContentTypes.Name = "fraDataFormatContentTypes";
			this.fraDataFormatContentTypes.Size = new System.Drawing.Size(223, 100);
			this.fraDataFormatContentTypes.TabIndex = 4;
			this.fraDataFormatContentTypes.TabStop = false;
			this.fraDataFormatContentTypes.Text = "Content types";
			// 
			// lvDataFormatContentTypes
			// 
			this.lvDataFormatContentTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvDataFormatContentTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDataFormatContentType});
			this.lvDataFormatContentTypes.FullRowSelect = true;
			this.lvDataFormatContentTypes.GridLines = true;
			this.lvDataFormatContentTypes.HideSelection = false;
			this.lvDataFormatContentTypes.Location = new System.Drawing.Point(6, 19);
			this.lvDataFormatContentTypes.Name = "lvDataFormatContentTypes";
			this.lvDataFormatContentTypes.Size = new System.Drawing.Size(211, 75);
			this.lvDataFormatContentTypes.TabIndex = 0;
			this.lvDataFormatContentTypes.UseCompatibleStateImageBehavior = false;
			this.lvDataFormatContentTypes.View = System.Windows.Forms.View.Details;
			// 
			// chDataFormatContentType
			// 
			this.chDataFormatContentType.Text = "Content type";
			this.chDataFormatContentType.Width = 204;
			// 
			// fraDataFormatFilters
			// 
			this.fraDataFormatFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraDataFormatFilters.Controls.Add(this.lvDataFormatFilters);
			this.fraDataFormatFilters.Location = new System.Drawing.Point(3, 161);
			this.fraDataFormatFilters.Name = "fraDataFormatFilters";
			this.fraDataFormatFilters.Size = new System.Drawing.Size(223, 111);
			this.fraDataFormatFilters.TabIndex = 4;
			this.fraDataFormatFilters.TabStop = false;
			this.fraDataFormatFilters.Text = "Filters";
			// 
			// lvDataFormatFilters
			// 
			this.lvDataFormatFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvDataFormatFilters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDataFormatFilterTitle,
            this.chDataFormatFilterFilters});
			this.lvDataFormatFilters.FullRowSelect = true;
			this.lvDataFormatFilters.GridLines = true;
			this.lvDataFormatFilters.HideSelection = false;
			this.lvDataFormatFilters.Location = new System.Drawing.Point(6, 19);
			this.lvDataFormatFilters.Name = "lvDataFormatFilters";
			this.lvDataFormatFilters.Size = new System.Drawing.Size(211, 86);
			this.lvDataFormatFilters.TabIndex = 0;
			this.lvDataFormatFilters.UseCompatibleStateImageBehavior = false;
			this.lvDataFormatFilters.View = System.Windows.Forms.View.Details;
			// 
			// chDataFormatFilterTitle
			// 
			this.chDataFormatFilterTitle.Text = "Title";
			this.chDataFormatFilterTitle.Width = 105;
			// 
			// chDataFormatFilterFilters
			// 
			this.chDataFormatFilterFilters.Text = "Filters";
			this.chDataFormatFilterFilters.Width = 99;
			// 
			// txtDataFormatTypeName
			// 
			this.txtDataFormatTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDataFormatTypeName.Location = new System.Drawing.Point(72, 29);
			this.txtDataFormatTypeName.Name = "txtDataFormatTypeName";
			this.txtDataFormatTypeName.ReadOnly = true;
			this.txtDataFormatTypeName.Size = new System.Drawing.Size(154, 20);
			this.txtDataFormatTypeName.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 32);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Type name:";
			// 
			// txtDataFormatID
			// 
			this.txtDataFormatID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDataFormatID.Location = new System.Drawing.Point(72, 3);
			this.txtDataFormatID.Name = "txtDataFormatID";
			this.txtDataFormatID.ReadOnly = true;
			this.txtDataFormatID.Size = new System.Drawing.Size(154, 20);
			this.txtDataFormatID.TabIndex = 3;
			// 
			// lblDataFormatID
			// 
			this.lblDataFormatID.AutoSize = true;
			this.lblDataFormatID.Location = new System.Drawing.Point(3, 6);
			this.lblDataFormatID.Name = "lblDataFormatID";
			this.lblDataFormatID.Size = new System.Drawing.Size(21, 13);
			this.lblDataFormatID.TabIndex = 2;
			this.lblDataFormatID.Text = "ID:";
			// 
			// pnlObjectModelInfo
			// 
			this.pnlObjectModelInfo.Controls.Add(this.txtObjectModelTitle);
			this.pnlObjectModelInfo.Controls.Add(this.lblObjectModelTitle);
			this.pnlObjectModelInfo.Controls.Add(this.txtObjectModelTypeName);
			this.pnlObjectModelInfo.Controls.Add(this.lblObjectModelTypeName);
			this.pnlObjectModelInfo.Controls.Add(this.txtObjectModelID);
			this.pnlObjectModelInfo.Controls.Add(this.lblObjectModelID);
			this.pnlObjectModelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlObjectModelInfo.Enabled = false;
			this.pnlObjectModelInfo.Location = new System.Drawing.Point(0, 0);
			this.pnlObjectModelInfo.Name = "pnlObjectModelInfo";
			this.pnlObjectModelInfo.Size = new System.Drawing.Size(229, 275);
			this.pnlObjectModelInfo.TabIndex = 3;
			this.pnlObjectModelInfo.Visible = false;
			// 
			// txtObjectModelTitle
			// 
			this.txtObjectModelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtObjectModelTitle.Location = new System.Drawing.Point(72, 55);
			this.txtObjectModelTitle.Name = "txtObjectModelTitle";
			this.txtObjectModelTitle.ReadOnly = true;
			this.txtObjectModelTitle.Size = new System.Drawing.Size(154, 20);
			this.txtObjectModelTitle.TabIndex = 3;
			// 
			// lblObjectModelTitle
			// 
			this.lblObjectModelTitle.AutoSize = true;
			this.lblObjectModelTitle.Location = new System.Drawing.Point(3, 58);
			this.lblObjectModelTitle.Name = "lblObjectModelTitle";
			this.lblObjectModelTitle.Size = new System.Drawing.Size(30, 13);
			this.lblObjectModelTitle.TabIndex = 2;
			this.lblObjectModelTitle.Text = "Title:";
			// 
			// txtObjectModelTypeName
			// 
			this.txtObjectModelTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtObjectModelTypeName.Location = new System.Drawing.Point(72, 29);
			this.txtObjectModelTypeName.Name = "txtObjectModelTypeName";
			this.txtObjectModelTypeName.ReadOnly = true;
			this.txtObjectModelTypeName.Size = new System.Drawing.Size(154, 20);
			this.txtObjectModelTypeName.TabIndex = 3;
			// 
			// lblObjectModelTypeName
			// 
			this.lblObjectModelTypeName.AutoSize = true;
			this.lblObjectModelTypeName.Location = new System.Drawing.Point(3, 32);
			this.lblObjectModelTypeName.Name = "lblObjectModelTypeName";
			this.lblObjectModelTypeName.Size = new System.Drawing.Size(63, 13);
			this.lblObjectModelTypeName.TabIndex = 2;
			this.lblObjectModelTypeName.Text = "Type name:";
			// 
			// txtObjectModelID
			// 
			this.txtObjectModelID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtObjectModelID.Location = new System.Drawing.Point(72, 3);
			this.txtObjectModelID.Name = "txtObjectModelID";
			this.txtObjectModelID.ReadOnly = true;
			this.txtObjectModelID.Size = new System.Drawing.Size(154, 20);
			this.txtObjectModelID.TabIndex = 3;
			// 
			// lblObjectModelID
			// 
			this.lblObjectModelID.AutoSize = true;
			this.lblObjectModelID.Location = new System.Drawing.Point(3, 6);
			this.lblObjectModelID.Name = "lblObjectModelID";
			this.lblObjectModelID.Size = new System.Drawing.Size(21, 13);
			this.lblObjectModelID.TabIndex = 2;
			this.lblObjectModelID.Text = "ID:";
			// 
			// tabLicense
			// 
			this.tabLicense.Controls.Add(this.label2);
			this.tabLicense.Controls.Add(this.txtLicense);
			this.tabLicense.Location = new System.Drawing.Point(4, 22);
			this.tabLicense.Name = "tabLicense";
			this.tabLicense.Padding = new System.Windows.Forms.Padding(3);
			this.tabLicense.Size = new System.Drawing.Size(500, 281);
			this.tabLicense.TabIndex = 2;
			this.tabLicense.Text = "Application License";
			this.tabLicense.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(488, 49);
			this.label2.TabIndex = 2;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// txtLicense
			// 
			this.txtLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLicense.Location = new System.Drawing.Point(3, 61);
			this.txtLicense.Multiline = true;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.ReadOnly = true;
			this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLicense.Size = new System.Drawing.Size(491, 210);
			this.txtLicense.TabIndex = 1;
			this.txtLicense.Text = resources.GetString("txtLicense.Text");
			// 
			// lblPlatform
			// 
			this.lblPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPlatform.Enabled = false;
			this.lblPlatform.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPlatform.Location = new System.Drawing.Point(10, 383);
			this.lblPlatform.Name = "lblPlatform";
			this.lblPlatform.Size = new System.Drawing.Size(429, 18);
			this.lblPlatform.TabIndex = 5;
			this.lblPlatform.Text = "Powered by the Universal Editor Platform";
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdOK;
			this.ClientSize = new System.Drawing.Size(532, 413);
			this.Controls.Add(this.lblPlatform);
			this.Controls.Add(this.tbsTabs);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblApplicationTitle);
			this.Controls.Add(this.pictureBox1);
			this.MinimumSize = new System.Drawing.Size(467, 438);
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Universal Editor";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tbsTabs.ResumeLayout(false);
			this.tabAuthors.ResumeLayout(false);
			this.tabAuthors.PerformLayout();
			this.tabInstalledComponents.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.pnlAssemblyInfo.ResumeLayout(false);
			this.pnlAssemblyInfo.PerformLayout();
			this.fraDescription.ResumeLayout(false);
			this.fraDescription.PerformLayout();
			this.pnlDataFormatInfo.ResumeLayout(false);
			this.pnlDataFormatInfo.PerformLayout();
			this.fraDataFormatContentTypes.ResumeLayout(false);
			this.fraDataFormatFilters.ResumeLayout(false);
			this.pnlObjectModelInfo.ResumeLayout(false);
			this.pnlObjectModelInfo.PerformLayout();
			this.tabLicense.ResumeLayout(false);
			this.tabLicense.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblApplicationTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TabControl tbsTabs;
        private System.Windows.Forms.TabPage tabAuthors;
        private System.Windows.Forms.TextBox txtContributors;
		private System.Windows.Forms.TabPage tabInstalledComponents;
        private System.Windows.Forms.TabPage tabLicense;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvComponents;
        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.Panel pnlAssemblyInfo;
        private System.Windows.Forms.GroupBox fraDescription;
        private System.Windows.Forms.TextBox txtAssemblyDescription;
        private System.Windows.Forms.Button cmdOpenContainingFolder;
        private System.Windows.Forms.TextBox txtAssemblyLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAssemblyFullName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlObjectModelInfo;
        private System.Windows.Forms.TextBox txtObjectModelTypeName;
        private System.Windows.Forms.Label lblObjectModelTypeName;
        private System.Windows.Forms.TextBox txtObjectModelID;
        private System.Windows.Forms.Label lblObjectModelID;
        private System.Windows.Forms.TextBox txtObjectModelTitle;
        private System.Windows.Forms.Label lblObjectModelTitle;
        private System.Windows.Forms.Panel pnlDataFormatInfo;
        private System.Windows.Forms.TextBox txtDataFormatTypeName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDataFormatID;
        private System.Windows.Forms.Label lblDataFormatID;
        private System.Windows.Forms.GroupBox fraDataFormatFilters;
        private System.Windows.Forms.ListView lvDataFormatFilters;
        private System.Windows.Forms.ColumnHeader chDataFormatFilterTitle;
        private System.Windows.Forms.ColumnHeader chDataFormatFilterFilters;
        private System.Windows.Forms.GroupBox fraDataFormatContentTypes;
        private System.Windows.Forms.ListView lvDataFormatContentTypes;
        private System.Windows.Forms.ColumnHeader chDataFormatContentType;
		private System.Windows.Forms.Label lblPlatform;
    }
}