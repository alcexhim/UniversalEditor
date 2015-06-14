namespace UniversalEditor.UserInterface.WindowsForms.Dialogs.FileSystem.Internal
{
	partial class FilePropertiesDialogImpl
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
			this.tv = new System.Windows.Forms.TreeView();
			this.pnlGeneral = new System.Windows.Forms.Panel();
			this.fraGeneralInformation = new System.Windows.Forms.GroupBox();
			this.txtGeneralInformationDateAccessed = new System.Windows.Forms.TextBox();
			this.lblGeneralInformationDateAccessed = new System.Windows.Forms.Label();
			this.lblGeneralInformationDateModified = new System.Windows.Forms.Label();
			this.txtGeneralInformationDateModified = new System.Windows.Forms.TextBox();
			this.lblGeneralInformationDateCreated = new System.Windows.Forms.Label();
			this.txtGeneralInformationSize = new System.Windows.Forms.TextBox();
			this.txtGeneralInformationDateCreated = new System.Windows.Forms.TextBox();
			this.lblGeneralInformationObjectModel = new System.Windows.Forms.Label();
			this.lblGeneralInformationDataFormat = new System.Windows.Forms.Label();
			this.lblGeneralInformationSize = new System.Windows.Forms.Label();
			this.txtGeneralInformationObjectModel = new System.Windows.Forms.TextBox();
			this.txtGeneralInformationDataFormat = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdGeneralInformationLocationBrowse = new System.Windows.Forms.Button();
			this.cmdGeneralInformationLocationChange = new System.Windows.Forms.Button();
			this.cmdGeneralInformationDataFormatChange = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGeneralInformationLocation = new System.Windows.Forms.TextBox();
			this.fraAttributes = new System.Windows.Forms.GroupBox();
			this.chkGeneralAttributesArchive = new System.Windows.Forms.CheckBox();
			this.chkGeneralAttributesHidden = new System.Windows.Forms.CheckBox();
			this.chkGeneralAttributesReadOnly = new System.Windows.Forms.CheckBox();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.sc = new System.Windows.Forms.SplitContainer();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.pnlNoObjectsSelected = new System.Windows.Forms.Panel();
			this.lblNoObjectsSelected = new System.Windows.Forms.Label();
			this.chkGeneralAttributesDeleted = new System.Windows.Forms.CheckBox();
			this.pnlGeneral.SuspendLayout();
			this.fraGeneralInformation.SuspendLayout();
			this.fraAttributes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.sc.Panel1.SuspendLayout();
			this.sc.Panel2.SuspendLayout();
			this.sc.SuspendLayout();
			this.pnlNoObjectsSelected.SuspendLayout();
			this.SuspendLayout();
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode1.Name = "nodeGeneral";
			treeNode1.Text = "General";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.tv.Size = new System.Drawing.Size(127, 373);
			this.tv.TabIndex = 0;
			// 
			// pnlGeneral
			// 
			this.pnlGeneral.Controls.Add(this.fraGeneralInformation);
			this.pnlGeneral.Controls.Add(this.fraAttributes);
			this.pnlGeneral.Controls.Add(this.txtFileName);
			this.pnlGeneral.Controls.Add(this.picIcon);
			this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGeneral.Location = new System.Drawing.Point(0, 0);
			this.pnlGeneral.Name = "pnlGeneral";
			this.pnlGeneral.Size = new System.Drawing.Size(323, 373);
			this.pnlGeneral.TabIndex = 0;
			// 
			// fraGeneralInformation
			// 
			this.fraGeneralInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationDateAccessed);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationDateAccessed);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationDateModified);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationDateModified);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationDateCreated);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationSize);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationDateCreated);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationObjectModel);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationDataFormat);
			this.fraGeneralInformation.Controls.Add(this.lblGeneralInformationSize);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationObjectModel);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationDataFormat);
			this.fraGeneralInformation.Controls.Add(this.label2);
			this.fraGeneralInformation.Controls.Add(this.cmdGeneralInformationLocationBrowse);
			this.fraGeneralInformation.Controls.Add(this.cmdGeneralInformationLocationChange);
			this.fraGeneralInformation.Controls.Add(this.cmdGeneralInformationDataFormatChange);
			this.fraGeneralInformation.Controls.Add(this.label3);
			this.fraGeneralInformation.Controls.Add(this.txtGeneralInformationLocation);
			this.fraGeneralInformation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraGeneralInformation.Location = new System.Drawing.Point(7, 45);
			this.fraGeneralInformation.Name = "fraGeneralInformation";
			this.fraGeneralInformation.Size = new System.Drawing.Size(313, 270);
			this.fraGeneralInformation.TabIndex = 1;
			this.fraGeneralInformation.TabStop = false;
			this.fraGeneralInformation.Text = "Information";
			// 
			// txtGeneralInformationDateAccessed
			// 
			this.txtGeneralInformationDateAccessed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationDateAccessed.Location = new System.Drawing.Point(101, 241);
			this.txtGeneralInformationDateAccessed.Name = "txtGeneralInformationDateAccessed";
			this.txtGeneralInformationDateAccessed.ReadOnly = true;
			this.txtGeneralInformationDateAccessed.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationDateAccessed.TabIndex = 13;
			// 
			// lblGeneralInformationDateAccessed
			// 
			this.lblGeneralInformationDateAccessed.AutoSize = true;
			this.lblGeneralInformationDateAccessed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationDateAccessed.Location = new System.Drawing.Point(13, 244);
			this.lblGeneralInformationDateAccessed.Name = "lblGeneralInformationDateAccessed";
			this.lblGeneralInformationDateAccessed.Size = new System.Drawing.Size(82, 13);
			this.lblGeneralInformationDateAccessed.TabIndex = 12;
			this.lblGeneralInformationDateAccessed.Text = "Date accessed:";
			// 
			// lblGeneralInformationDateModified
			// 
			this.lblGeneralInformationDateModified.AutoSize = true;
			this.lblGeneralInformationDateModified.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationDateModified.Location = new System.Drawing.Point(13, 218);
			this.lblGeneralInformationDateModified.Name = "lblGeneralInformationDateModified";
			this.lblGeneralInformationDateModified.Size = new System.Drawing.Size(75, 13);
			this.lblGeneralInformationDateModified.TabIndex = 10;
			this.lblGeneralInformationDateModified.Text = "Date modified:";
			// 
			// txtGeneralInformationDateModified
			// 
			this.txtGeneralInformationDateModified.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationDateModified.Location = new System.Drawing.Point(101, 215);
			this.txtGeneralInformationDateModified.Name = "txtGeneralInformationDateModified";
			this.txtGeneralInformationDateModified.ReadOnly = true;
			this.txtGeneralInformationDateModified.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationDateModified.TabIndex = 11;
			// 
			// lblGeneralInformationDateCreated
			// 
			this.lblGeneralInformationDateCreated.AutoSize = true;
			this.lblGeneralInformationDateCreated.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationDateCreated.Location = new System.Drawing.Point(13, 192);
			this.lblGeneralInformationDateCreated.Name = "lblGeneralInformationDateCreated";
			this.lblGeneralInformationDateCreated.Size = new System.Drawing.Size(72, 13);
			this.lblGeneralInformationDateCreated.TabIndex = 8;
			this.lblGeneralInformationDateCreated.Text = "Date created:";
			// 
			// txtGeneralInformationSize
			// 
			this.txtGeneralInformationSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationSize.Location = new System.Drawing.Point(101, 155);
			this.txtGeneralInformationSize.Name = "txtGeneralInformationSize";
			this.txtGeneralInformationSize.ReadOnly = true;
			this.txtGeneralInformationSize.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationSize.TabIndex = 6;
			// 
			// txtGeneralInformationDateCreated
			// 
			this.txtGeneralInformationDateCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationDateCreated.Location = new System.Drawing.Point(101, 189);
			this.txtGeneralInformationDateCreated.Name = "txtGeneralInformationDateCreated";
			this.txtGeneralInformationDateCreated.ReadOnly = true;
			this.txtGeneralInformationDateCreated.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationDateCreated.TabIndex = 9;
			// 
			// lblGeneralInformationObjectModel
			// 
			this.lblGeneralInformationObjectModel.AutoSize = true;
			this.lblGeneralInformationObjectModel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationObjectModel.Location = new System.Drawing.Point(13, 22);
			this.lblGeneralInformationObjectModel.Name = "lblGeneralInformationObjectModel";
			this.lblGeneralInformationObjectModel.Size = new System.Drawing.Size(72, 13);
			this.lblGeneralInformationObjectModel.TabIndex = 0;
			this.lblGeneralInformationObjectModel.Text = "Object model:";
			// 
			// lblGeneralInformationDataFormat
			// 
			this.lblGeneralInformationDataFormat.AutoSize = true;
			this.lblGeneralInformationDataFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationDataFormat.Location = new System.Drawing.Point(13, 48);
			this.lblGeneralInformationDataFormat.Name = "lblGeneralInformationDataFormat";
			this.lblGeneralInformationDataFormat.Size = new System.Drawing.Size(65, 13);
			this.lblGeneralInformationDataFormat.TabIndex = 0;
			this.lblGeneralInformationDataFormat.Text = "Data format:";
			// 
			// lblGeneralInformationSize
			// 
			this.lblGeneralInformationSize.AutoSize = true;
			this.lblGeneralInformationSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGeneralInformationSize.Location = new System.Drawing.Point(13, 158);
			this.lblGeneralInformationSize.Name = "lblGeneralInformationSize";
			this.lblGeneralInformationSize.Size = new System.Drawing.Size(30, 13);
			this.lblGeneralInformationSize.TabIndex = 5;
			this.lblGeneralInformationSize.Text = "Size:";
			// 
			// txtGeneralInformationObjectModel
			// 
			this.txtGeneralInformationObjectModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationObjectModel.Location = new System.Drawing.Point(101, 19);
			this.txtGeneralInformationObjectModel.Name = "txtGeneralInformationObjectModel";
			this.txtGeneralInformationObjectModel.ReadOnly = true;
			this.txtGeneralInformationObjectModel.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationObjectModel.TabIndex = 1;
			// 
			// txtGeneralInformationDataFormat
			// 
			this.txtGeneralInformationDataFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationDataFormat.Location = new System.Drawing.Point(101, 45);
			this.txtGeneralInformationDataFormat.Name = "txtGeneralInformationDataFormat";
			this.txtGeneralInformationDataFormat.ReadOnly = true;
			this.txtGeneralInformationDataFormat.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationDataFormat.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(13, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Location:";
			// 
			// cmdGeneralInformationLocationBrowse
			// 
			this.cmdGeneralInformationLocationBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGeneralInformationLocationBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGeneralInformationLocationBrowse.Location = new System.Drawing.Point(151, 126);
			this.cmdGeneralInformationLocationBrowse.Name = "cmdGeneralInformationLocationBrowse";
			this.cmdGeneralInformationLocationBrowse.Size = new System.Drawing.Size(75, 23);
			this.cmdGeneralInformationLocationBrowse.TabIndex = 2;
			this.cmdGeneralInformationLocationBrowse.Text = "&Browse";
			this.cmdGeneralInformationLocationBrowse.UseVisualStyleBackColor = true;
			this.cmdGeneralInformationLocationBrowse.Click += new System.EventHandler(this.cmdGeneralInformationLocationBrowse_Click);
			// 
			// cmdGeneralInformationLocationChange
			// 
			this.cmdGeneralInformationLocationChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGeneralInformationLocationChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGeneralInformationLocationChange.Location = new System.Drawing.Point(232, 126);
			this.cmdGeneralInformationLocationChange.Name = "cmdGeneralInformationLocationChange";
			this.cmdGeneralInformationLocationChange.Size = new System.Drawing.Size(75, 23);
			this.cmdGeneralInformationLocationChange.TabIndex = 2;
			this.cmdGeneralInformationLocationChange.Text = "&Change...";
			this.cmdGeneralInformationLocationChange.UseVisualStyleBackColor = true;
			this.cmdGeneralInformationLocationChange.Click += new System.EventHandler(this.cmdGeneralInformationLocationChange_Click);
			// 
			// cmdGeneralInformationDataFormatChange
			// 
			this.cmdGeneralInformationDataFormatChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGeneralInformationDataFormatChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGeneralInformationDataFormatChange.Location = new System.Drawing.Point(232, 71);
			this.cmdGeneralInformationDataFormatChange.Name = "cmdGeneralInformationDataFormatChange";
			this.cmdGeneralInformationDataFormatChange.Size = new System.Drawing.Size(75, 23);
			this.cmdGeneralInformationDataFormatChange.TabIndex = 2;
			this.cmdGeneralInformationDataFormatChange.Text = "&Change...";
			this.cmdGeneralInformationDataFormatChange.UseVisualStyleBackColor = true;
			this.cmdGeneralInformationDataFormatChange.Click += new System.EventHandler(this.cmdGeneralInformationDataFormatChange_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(6, 181);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(301, 2);
			this.label3.TabIndex = 7;
			// 
			// txtGeneralInformationLocation
			// 
			this.txtGeneralInformationLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneralInformationLocation.Location = new System.Drawing.Point(101, 100);
			this.txtGeneralInformationLocation.Name = "txtGeneralInformationLocation";
			this.txtGeneralInformationLocation.ReadOnly = true;
			this.txtGeneralInformationLocation.Size = new System.Drawing.Size(206, 20);
			this.txtGeneralInformationLocation.TabIndex = 4;
			// 
			// fraAttributes
			// 
			this.fraAttributes.Controls.Add(this.chkGeneralAttributesDeleted);
			this.fraAttributes.Controls.Add(this.chkGeneralAttributesArchive);
			this.fraAttributes.Controls.Add(this.chkGeneralAttributesHidden);
			this.fraAttributes.Controls.Add(this.chkGeneralAttributesReadOnly);
			this.fraAttributes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraAttributes.Location = new System.Drawing.Point(7, 321);
			this.fraAttributes.Name = "fraAttributes";
			this.fraAttributes.Size = new System.Drawing.Size(313, 47);
			this.fraAttributes.TabIndex = 2;
			this.fraAttributes.TabStop = false;
			this.fraAttributes.Text = "Attributes";
			// 
			// chkGeneralAttributesArchive
			// 
			this.chkGeneralAttributesArchive.AutoSize = true;
			this.chkGeneralAttributesArchive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkGeneralAttributesArchive.Location = new System.Drawing.Point(162, 19);
			this.chkGeneralAttributesArchive.Name = "chkGeneralAttributesArchive";
			this.chkGeneralAttributesArchive.Size = new System.Drawing.Size(68, 18);
			this.chkGeneralAttributesArchive.TabIndex = 2;
			this.chkGeneralAttributesArchive.Text = "&Archive";
			this.chkGeneralAttributesArchive.UseVisualStyleBackColor = true;
			// 
			// chkGeneralAttributesHidden
			// 
			this.chkGeneralAttributesHidden.AutoSize = true;
			this.chkGeneralAttributesHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkGeneralAttributesHidden.Location = new System.Drawing.Point(96, 19);
			this.chkGeneralAttributesHidden.Name = "chkGeneralAttributesHidden";
			this.chkGeneralAttributesHidden.Size = new System.Drawing.Size(66, 18);
			this.chkGeneralAttributesHidden.TabIndex = 1;
			this.chkGeneralAttributesHidden.Text = "&Hidden";
			this.chkGeneralAttributesHidden.UseVisualStyleBackColor = true;
			// 
			// chkGeneralAttributesReadOnly
			// 
			this.chkGeneralAttributesReadOnly.AutoSize = true;
			this.chkGeneralAttributesReadOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkGeneralAttributesReadOnly.Location = new System.Drawing.Point(16, 19);
			this.chkGeneralAttributesReadOnly.Name = "chkGeneralAttributesReadOnly";
			this.chkGeneralAttributesReadOnly.Size = new System.Drawing.Size(80, 18);
			this.chkGeneralAttributesReadOnly.TabIndex = 0;
			this.chkGeneralAttributesReadOnly.Text = "&Read-only";
			this.chkGeneralAttributesReadOnly.UseVisualStyleBackColor = true;
			// 
			// txtFileName
			// 
			this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFileName.Location = new System.Drawing.Point(45, 7);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(275, 20);
			this.txtFileName.TabIndex = 0;
			// 
			// picIcon
			// 
			this.picIcon.Location = new System.Drawing.Point(7, 7);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 32);
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// sc
			// 
			this.sc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.sc.Location = new System.Drawing.Point(12, 12);
			this.sc.Name = "sc";
			// 
			// sc.Panel1
			// 
			this.sc.Panel1.Controls.Add(this.tv);
			// 
			// sc.Panel2
			// 
			this.sc.Panel2.Controls.Add(this.pnlGeneral);
			this.sc.Size = new System.Drawing.Size(454, 373);
			this.sc.SplitterDistance = 127;
			this.sc.TabIndex = 0;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(391, 391);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(310, 391);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// pnlNoObjectsSelected
			// 
			this.pnlNoObjectsSelected.Controls.Add(this.lblNoObjectsSelected);
			this.pnlNoObjectsSelected.Location = new System.Drawing.Point(12, 12);
			this.pnlNoObjectsSelected.Name = "pnlNoObjectsSelected";
			this.pnlNoObjectsSelected.Size = new System.Drawing.Size(454, 318);
			this.pnlNoObjectsSelected.TabIndex = 3;
			this.pnlNoObjectsSelected.Visible = false;
			// 
			// lblNoObjectsSelected
			// 
			this.lblNoObjectsSelected.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblNoObjectsSelected.AutoSize = true;
			this.lblNoObjectsSelected.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNoObjectsSelected.Location = new System.Drawing.Point(184, 153);
			this.lblNoObjectsSelected.Name = "lblNoObjectsSelected";
			this.lblNoObjectsSelected.Size = new System.Drawing.Size(87, 13);
			this.lblNoObjectsSelected.TabIndex = 0;
			this.lblNoObjectsSelected.Text = "Nothing selected";
			// 
			// chkGeneralAttributesDeleted
			// 
			this.chkGeneralAttributesDeleted.AutoSize = true;
			this.chkGeneralAttributesDeleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkGeneralAttributesDeleted.Location = new System.Drawing.Point(230, 19);
			this.chkGeneralAttributesDeleted.Name = "chkGeneralAttributesDeleted";
			this.chkGeneralAttributesDeleted.Size = new System.Drawing.Size(69, 18);
			this.chkGeneralAttributesDeleted.TabIndex = 2;
			this.chkGeneralAttributesDeleted.Text = "&Deleted";
			this.chkGeneralAttributesDeleted.UseVisualStyleBackColor = true;
			// 
			// FilePropertiesDialogImpl
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(486, 437);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.sc);
			this.Controls.Add(this.pnlNoObjectsSelected);
			this.MinimumSize = new System.Drawing.Size(494, 464);
			this.Name = "FilePropertiesDialogImpl";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Properties";
			this.pnlGeneral.ResumeLayout(false);
			this.pnlGeneral.PerformLayout();
			this.fraGeneralInformation.ResumeLayout(false);
			this.fraGeneralInformation.PerformLayout();
			this.fraAttributes.ResumeLayout(false);
			this.fraAttributes.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.sc.Panel1.ResumeLayout(false);
			this.sc.Panel2.ResumeLayout(false);
			this.sc.ResumeLayout(false);
			this.pnlNoObjectsSelected.ResumeLayout(false);
			this.pnlNoObjectsSelected.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Panel pnlGeneral;
		private System.Windows.Forms.SplitContainer sc;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.GroupBox fraAttributes;
		private System.Windows.Forms.GroupBox fraGeneralInformation;
		private System.Windows.Forms.Label lblGeneralInformationDataFormat;
		private System.Windows.Forms.Button cmdGeneralInformationDataFormatChange;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblGeneralInformationSize;
		private System.Windows.Forms.Label lblGeneralInformationDateCreated;
		private System.Windows.Forms.Label lblGeneralInformationDateModified;
		private System.Windows.Forms.Label lblGeneralInformationDateAccessed;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.CheckBox chkGeneralAttributesArchive;
		private System.Windows.Forms.CheckBox chkGeneralAttributesHidden;
		private System.Windows.Forms.CheckBox chkGeneralAttributesReadOnly;
		private System.Windows.Forms.TextBox txtGeneralInformationDataFormat;
		internal System.Windows.Forms.TextBox txtGeneralInformationLocation;
		private System.Windows.Forms.TextBox txtGeneralInformationSize;
		private System.Windows.Forms.TextBox txtGeneralInformationDateCreated;
		private System.Windows.Forms.TextBox txtGeneralInformationDateModified;
		private System.Windows.Forms.TextBox txtGeneralInformationDateAccessed;
		private System.Windows.Forms.Panel pnlNoObjectsSelected;
		private System.Windows.Forms.Label lblNoObjectsSelected;
		private System.Windows.Forms.TextBox txtGeneralInformationObjectModel;
		private System.Windows.Forms.Label lblGeneralInformationObjectModel;
		private System.Windows.Forms.Button cmdGeneralInformationLocationBrowse;
		private System.Windows.Forms.Button cmdGeneralInformationLocationChange;
		private System.Windows.Forms.CheckBox chkGeneralAttributesDeleted;

	}
}