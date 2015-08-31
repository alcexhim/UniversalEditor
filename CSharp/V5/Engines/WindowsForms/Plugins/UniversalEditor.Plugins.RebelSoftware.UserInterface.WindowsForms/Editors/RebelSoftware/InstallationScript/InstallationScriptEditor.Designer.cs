namespace UniversalEditor.Editors.RebelSoftware.InstallationScript
{
	partial class InstallationScriptEditor
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Product");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Dialogs");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Shortcuts");
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Welcome");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("License");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Destination");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Summary");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Progress");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Finish");
			this.tvExplorer = new System.Windows.Forms.TreeView();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.pnlProduct = new System.Windows.Forms.Panel();
			this.lblProductName = new System.Windows.Forms.Label();
			this.txtProductName = new System.Windows.Forms.TextBox();
			this.lblProductVersion = new System.Windows.Forms.Label();
			this.txtProductVersion = new System.Windows.Forms.TextBox();
			this.lblProductDiskSpace = new System.Windows.Forms.Label();
			this.txtProductDiskSpace = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lblProductBackgroundColor = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.lblProductBackgroundImage = new System.Windows.Forms.Label();
			this.txtProductBackgroundImage = new System.Windows.Forms.TextBox();
			this.cmdBrowseProductBackgroundImage = new System.Windows.Forms.Button();
			this.pnlDialogs = new System.Windows.Forms.Panel();
			this.lvDialogs = new System.Windows.Forms.ListView();
			this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdDialogMoveUp = new System.Windows.Forms.Button();
			this.cmdDialogMoveDown = new System.Windows.Forms.Button();
			this.fraProperties = new System.Windows.Forms.GroupBox();
			this.fraPreview = new System.Windows.Forms.GroupBox();
			this.picDialogPreview = new System.Windows.Forms.PictureBox();
			this.pnlDialogPropertiesNone = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.pnlProduct.SuspendLayout();
			this.pnlDialogs.SuspendLayout();
			this.fraProperties.SuspendLayout();
			this.fraPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picDialogPreview)).BeginInit();
			this.pnlDialogPropertiesNone.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvExplorer
			// 
			this.tvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvExplorer.HideSelection = false;
			this.tvExplorer.Location = new System.Drawing.Point(0, 0);
			this.tvExplorer.Name = "tvExplorer";
			treeNode1.Name = "nodeProduct";
			treeNode1.Text = "Product";
			treeNode2.Name = "nodeDialogs";
			treeNode2.Text = "Dialogs";
			treeNode3.Name = "nodeShortcuts";
			treeNode3.Text = "Shortcuts";
			this.tvExplorer.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			this.tvExplorer.Size = new System.Drawing.Size(181, 406);
			this.tvExplorer.TabIndex = 0;
			this.tvExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExplorer_AfterSelect);
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.scMain.Location = new System.Drawing.Point(0, 0);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.tvExplorer);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.pnlDialogs);
			this.scMain.Panel2.Controls.Add(this.pnlProduct);
			this.scMain.Size = new System.Drawing.Size(534, 406);
			this.scMain.SplitterDistance = 181;
			this.scMain.TabIndex = 1;
			// 
			// pnlProduct
			// 
			this.pnlProduct.Controls.Add(this.cmdBrowseProductBackgroundImage);
			this.pnlProduct.Controls.Add(this.button2);
			this.pnlProduct.Controls.Add(this.button1);
			this.pnlProduct.Controls.Add(this.label3);
			this.pnlProduct.Controls.Add(this.txtProductBackgroundImage);
			this.pnlProduct.Controls.Add(this.txtProductDiskSpace);
			this.pnlProduct.Controls.Add(this.label2);
			this.pnlProduct.Controls.Add(this.lblProductBackgroundImage);
			this.pnlProduct.Controls.Add(this.lblProductBackgroundColor);
			this.pnlProduct.Controls.Add(this.lblProductDiskSpace);
			this.pnlProduct.Controls.Add(this.txtProductVersion);
			this.pnlProduct.Controls.Add(this.lblProductVersion);
			this.pnlProduct.Controls.Add(this.txtProductName);
			this.pnlProduct.Controls.Add(this.lblProductName);
			this.pnlProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlProduct.Location = new System.Drawing.Point(0, 0);
			this.pnlProduct.Name = "pnlProduct";
			this.pnlProduct.Size = new System.Drawing.Size(349, 406);
			this.pnlProduct.TabIndex = 0;
			// 
			// lblProductName
			// 
			this.lblProductName.AutoSize = true;
			this.lblProductName.Location = new System.Drawing.Point(3, 6);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(76, 13);
			this.lblProductName.TabIndex = 0;
			this.lblProductName.Text = "Product &name:";
			// 
			// txtProductName
			// 
			this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProductName.Location = new System.Drawing.Point(108, 3);
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.Size = new System.Drawing.Size(238, 20);
			this.txtProductName.TabIndex = 1;
			this.txtProductName.Text = "My Application";
			// 
			// lblProductVersion
			// 
			this.lblProductVersion.AutoSize = true;
			this.lblProductVersion.Location = new System.Drawing.Point(3, 32);
			this.lblProductVersion.Name = "lblProductVersion";
			this.lblProductVersion.Size = new System.Drawing.Size(84, 13);
			this.lblProductVersion.TabIndex = 0;
			this.lblProductVersion.Text = "Product &version:";
			// 
			// txtProductVersion
			// 
			this.txtProductVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProductVersion.Location = new System.Drawing.Point(108, 29);
			this.txtProductVersion.Name = "txtProductVersion";
			this.txtProductVersion.Size = new System.Drawing.Size(238, 20);
			this.txtProductVersion.TabIndex = 1;
			this.txtProductVersion.Text = "1.0.0.0";
			// 
			// lblProductDiskSpace
			// 
			this.lblProductDiskSpace.AutoSize = true;
			this.lblProductDiskSpace.Location = new System.Drawing.Point(3, 58);
			this.lblProductDiskSpace.Name = "lblProductDiskSpace";
			this.lblProductDiskSpace.Size = new System.Drawing.Size(63, 13);
			this.lblProductDiskSpace.TabIndex = 0;
			this.lblProductDiskSpace.Text = "&Disk space:";
			// 
			// txtProductDiskSpace
			// 
			this.txtProductDiskSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProductDiskSpace.Location = new System.Drawing.Point(108, 55);
			this.txtProductDiskSpace.Name = "txtProductDiskSpace";
			this.txtProductDiskSpace.Size = new System.Drawing.Size(238, 20);
			this.txtProductDiskSpace.TabIndex = 1;
			this.txtProductDiskSpace.Text = "60";
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.button1.BackColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(147, 81);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.UseVisualStyleBackColor = false;
			// 
			// lblProductBackgroundColor
			// 
			this.lblProductBackgroundColor.AutoSize = true;
			this.lblProductBackgroundColor.Location = new System.Drawing.Point(3, 86);
			this.lblProductBackgroundColor.Name = "lblProductBackgroundColor";
			this.lblProductBackgroundColor.Size = new System.Drawing.Size(94, 13);
			this.lblProductBackgroundColor.TabIndex = 0;
			this.lblProductBackgroundColor.Text = "Background color:";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(144, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Top";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(225, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "Bottom";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.button2.BackColor = System.Drawing.Color.Black;
			this.button2.Location = new System.Drawing.Point(228, 81);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.UseVisualStyleBackColor = false;
			// 
			// lblProductBackgroundImage
			// 
			this.lblProductBackgroundImage.AutoSize = true;
			this.lblProductBackgroundImage.Location = new System.Drawing.Point(3, 131);
			this.lblProductBackgroundImage.Name = "lblProductBackgroundImage";
			this.lblProductBackgroundImage.Size = new System.Drawing.Size(99, 13);
			this.lblProductBackgroundImage.TabIndex = 0;
			this.lblProductBackgroundImage.Text = "Background &image:";
			// 
			// txtProductBackgroundImage
			// 
			this.txtProductBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProductBackgroundImage.Location = new System.Drawing.Point(108, 128);
			this.txtProductBackgroundImage.Name = "txtProductBackgroundImage";
			this.txtProductBackgroundImage.Size = new System.Drawing.Size(238, 20);
			this.txtProductBackgroundImage.TabIndex = 1;
			// 
			// cmdBrowseProductBackgroundImage
			// 
			this.cmdBrowseProductBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseProductBackgroundImage.Location = new System.Drawing.Point(271, 154);
			this.cmdBrowseProductBackgroundImage.Name = "cmdBrowseProductBackgroundImage";
			this.cmdBrowseProductBackgroundImage.Size = new System.Drawing.Size(75, 23);
			this.cmdBrowseProductBackgroundImage.TabIndex = 3;
			this.cmdBrowseProductBackgroundImage.Text = "&Browse...";
			this.cmdBrowseProductBackgroundImage.UseVisualStyleBackColor = true;
			// 
			// pnlDialogs
			// 
			this.pnlDialogs.Controls.Add(this.fraPreview);
			this.pnlDialogs.Controls.Add(this.fraProperties);
			this.pnlDialogs.Controls.Add(this.cmdDialogMoveDown);
			this.pnlDialogs.Controls.Add(this.cmdDialogMoveUp);
			this.pnlDialogs.Controls.Add(this.lvDialogs);
			this.pnlDialogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDialogs.Location = new System.Drawing.Point(0, 0);
			this.pnlDialogs.Name = "pnlDialogs";
			this.pnlDialogs.Size = new System.Drawing.Size(349, 406);
			this.pnlDialogs.TabIndex = 1;
			// 
			// lvDialogs
			// 
			this.lvDialogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvDialogs.CheckBoxes = true;
			this.lvDialogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTitle});
			this.lvDialogs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.StateImageIndex = 0;
			listViewItem4.StateImageIndex = 0;
			listViewItem5.StateImageIndex = 0;
			listViewItem6.StateImageIndex = 0;
			this.lvDialogs.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.lvDialogs.Location = new System.Drawing.Point(3, 3);
			this.lvDialogs.Name = "lvDialogs";
			this.lvDialogs.Size = new System.Drawing.Size(262, 119);
			this.lvDialogs.TabIndex = 0;
			this.lvDialogs.UseCompatibleStateImageBehavior = false;
			this.lvDialogs.View = System.Windows.Forms.View.Details;
			this.lvDialogs.SelectedIndexChanged += new System.EventHandler(this.lvDialogs_SelectedIndexChanged);
			// 
			// chTitle
			// 
			this.chTitle.Text = "Title";
			this.chTitle.Width = 169;
			// 
			// cmdDialogMoveUp
			// 
			this.cmdDialogMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDialogMoveUp.Location = new System.Drawing.Point(271, 3);
			this.cmdDialogMoveUp.Name = "cmdDialogMoveUp";
			this.cmdDialogMoveUp.Size = new System.Drawing.Size(75, 23);
			this.cmdDialogMoveUp.TabIndex = 1;
			this.cmdDialogMoveUp.Text = "Move &Up";
			this.cmdDialogMoveUp.UseVisualStyleBackColor = true;
			// 
			// cmdDialogMoveDown
			// 
			this.cmdDialogMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDialogMoveDown.Location = new System.Drawing.Point(271, 32);
			this.cmdDialogMoveDown.Name = "cmdDialogMoveDown";
			this.cmdDialogMoveDown.Size = new System.Drawing.Size(75, 23);
			this.cmdDialogMoveDown.TabIndex = 1;
			this.cmdDialogMoveDown.Text = "Move &Down";
			this.cmdDialogMoveDown.UseVisualStyleBackColor = true;
			// 
			// fraProperties
			// 
			this.fraProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraProperties.Controls.Add(this.pnlDialogPropertiesNone);
			this.fraProperties.Location = new System.Drawing.Point(3, 128);
			this.fraProperties.Name = "fraProperties";
			this.fraProperties.Size = new System.Drawing.Size(343, 111);
			this.fraProperties.TabIndex = 2;
			this.fraProperties.TabStop = false;
			this.fraProperties.Text = "Properties";
			// 
			// fraPreview
			// 
			this.fraPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraPreview.Controls.Add(this.picDialogPreview);
			this.fraPreview.Location = new System.Drawing.Point(3, 245);
			this.fraPreview.Name = "fraPreview";
			this.fraPreview.Size = new System.Drawing.Size(343, 158);
			this.fraPreview.TabIndex = 2;
			this.fraPreview.TabStop = false;
			this.fraPreview.Text = "Preview";
			// 
			// pictureBox1
			// 
			this.picDialogPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picDialogPreview.Image = global::UniversalEditor.Properties.Resources.Screenshot_Install_01_Welcome;
			this.picDialogPreview.Location = new System.Drawing.Point(6, 19);
			this.picDialogPreview.Name = "pictureBox1";
			this.picDialogPreview.Size = new System.Drawing.Size(331, 133);
			this.picDialogPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picDialogPreview.TabIndex = 0;
			this.picDialogPreview.TabStop = false;
			// 
			// pnlDialogPropertiesNone
			// 
			this.pnlDialogPropertiesNone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDialogPropertiesNone.Controls.Add(this.label1);
			this.pnlDialogPropertiesNone.Location = new System.Drawing.Point(6, 19);
			this.pnlDialogPropertiesNone.Name = "pnlDialogPropertiesNone";
			this.pnlDialogPropertiesNone.Size = new System.Drawing.Size(331, 86);
			this.pnlDialogPropertiesNone.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(52, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(227, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "There are no properties available for this dialog";
			// 
			// InstallationScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scMain);
			this.Name = "InstallationScriptEditor";
			this.Size = new System.Drawing.Size(534, 406);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.ResumeLayout(false);
			this.pnlProduct.ResumeLayout(false);
			this.pnlProduct.PerformLayout();
			this.pnlDialogs.ResumeLayout(false);
			this.fraProperties.ResumeLayout(false);
			this.fraPreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picDialogPreview)).EndInit();
			this.pnlDialogPropertiesNone.ResumeLayout(false);
			this.pnlDialogPropertiesNone.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvExplorer;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.Panel pnlProduct;
		private System.Windows.Forms.TextBox txtProductName;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.TextBox txtProductDiskSpace;
		private System.Windows.Forms.Label lblProductDiskSpace;
		private System.Windows.Forms.TextBox txtProductVersion;
		private System.Windows.Forms.Label lblProductVersion;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblProductBackgroundColor;
		private System.Windows.Forms.Button cmdBrowseProductBackgroundImage;
		private System.Windows.Forms.TextBox txtProductBackgroundImage;
		private System.Windows.Forms.Label lblProductBackgroundImage;
		private System.Windows.Forms.Panel pnlDialogs;
		private System.Windows.Forms.Button cmdDialogMoveDown;
		private System.Windows.Forms.Button cmdDialogMoveUp;
		private System.Windows.Forms.ListView lvDialogs;
		private System.Windows.Forms.ColumnHeader chTitle;
		private System.Windows.Forms.GroupBox fraPreview;
		private System.Windows.Forms.GroupBox fraProperties;
		private System.Windows.Forms.PictureBox picDialogPreview;
		private System.Windows.Forms.Panel pnlDialogPropertiesNone;
		private System.Windows.Forms.Label label1;
	}
}
