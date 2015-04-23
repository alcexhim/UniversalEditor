namespace UniversalEditor.Editors.Catalog.ArkAngles
{
	partial class ArkAnglesCatalogEditor
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
			System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("General");
			System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Categories");
			System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Platforms");
			System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Listings");
			System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Products");
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.pnlPlatforms = new System.Windows.Forms.Panel();
			this.cmdPlatformClear = new System.Windows.Forms.Button();
			this.cmdPlatformRemove = new System.Windows.Forms.Button();
			this.cmdPlatformModify = new System.Windows.Forms.Button();
			this.cmdPlatformAdd = new System.Windows.Forms.Button();
			this.lvPlatforms = new System.Windows.Forms.ListView();
			this.chPlatformTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chPlatformProducts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlCategories = new System.Windows.Forms.Panel();
			this.cmdCategoryClear = new System.Windows.Forms.Button();
			this.cmdCategoryRemove = new System.Windows.Forms.Button();
			this.cmdCategoryModify = new System.Windows.Forms.Button();
			this.cmdCategoryAdd = new System.Windows.Forms.Button();
			this.lvCategories = new System.Windows.Forms.ListView();
			this.chCategoryTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chCategoryProducts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlGeneral = new System.Windows.Forms.Panel();
			this.pnlProducts = new System.Windows.Forms.Panel();
			this.cmdProductClear = new System.Windows.Forms.Button();
			this.cmdProductRemove = new System.Windows.Forms.Button();
			this.cmdProductModify = new System.Windows.Forms.Button();
			this.cmdProductAdd = new System.Windows.Forms.Button();
			this.lvProducts = new System.Windows.Forms.ListView();
			this.chProductTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductPlatform = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductListing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pnlListings = new System.Windows.Forms.Panel();
			this.cmdListingClear = new System.Windows.Forms.Button();
			this.cmdListingRemove = new System.Windows.Forms.Button();
			this.cmdListingModify = new System.Windows.Forms.Button();
			this.cmdListingsAdd = new System.Windows.Forms.Button();
			this.lvListings = new System.Windows.Forms.ListView();
			this.chListingTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chListingProductCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.pnlPlatforms.SuspendLayout();
			this.pnlCategories.SuspendLayout();
			this.pnlProducts.SuspendLayout();
			this.pnlListings.SuspendLayout();
			this.SuspendLayout();
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(0, 0);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.tv);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.pnlCategories);
			this.scMain.Panel2.Controls.Add(this.pnlGeneral);
			this.scMain.Panel2.Controls.Add(this.pnlProducts);
			this.scMain.Panel2.Controls.Add(this.pnlListings);
			this.scMain.Panel2.Controls.Add(this.pnlPlatforms);
			this.scMain.Size = new System.Drawing.Size(492, 376);
			this.scMain.SplitterDistance = 164;
			this.scMain.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode36.Name = "nodeGeneral";
			treeNode36.Text = "General";
			treeNode37.Name = "nodeCategories";
			treeNode37.Text = "Categories";
			treeNode38.Name = "nodePlatforms";
			treeNode38.Text = "Platforms";
			treeNode39.Name = "nodeListings";
			treeNode39.Text = "Listings";
			treeNode40.Name = "nodeProducts";
			treeNode40.Text = "Products";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode36,
            treeNode37,
            treeNode38,
            treeNode39,
            treeNode40});
			this.tv.Size = new System.Drawing.Size(164, 376);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// pnlPlatforms
			// 
			this.pnlPlatforms.Controls.Add(this.cmdPlatformClear);
			this.pnlPlatforms.Controls.Add(this.cmdPlatformRemove);
			this.pnlPlatforms.Controls.Add(this.cmdPlatformModify);
			this.pnlPlatforms.Controls.Add(this.cmdPlatformAdd);
			this.pnlPlatforms.Controls.Add(this.lvPlatforms);
			this.pnlPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPlatforms.Location = new System.Drawing.Point(0, 0);
			this.pnlPlatforms.Name = "pnlPlatforms";
			this.pnlPlatforms.Size = new System.Drawing.Size(324, 376);
			this.pnlPlatforms.TabIndex = 2;
			// 
			// cmdPlatformClear
			// 
			this.cmdPlatformClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPlatformClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPlatformClear.Location = new System.Drawing.Point(246, 3);
			this.cmdPlatformClear.Name = "cmdPlatformClear";
			this.cmdPlatformClear.Size = new System.Drawing.Size(75, 23);
			this.cmdPlatformClear.TabIndex = 3;
			this.cmdPlatformClear.Text = "&Clear";
			this.cmdPlatformClear.UseVisualStyleBackColor = true;
			this.cmdPlatformClear.Click += new System.EventHandler(this.cmdPlatformClear_Click);
			// 
			// cmdPlatformRemove
			// 
			this.cmdPlatformRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPlatformRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdPlatformRemove.Name = "cmdPlatformRemove";
			this.cmdPlatformRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdPlatformRemove.TabIndex = 4;
			this.cmdPlatformRemove.Text = "&Remove";
			this.cmdPlatformRemove.UseVisualStyleBackColor = true;
			this.cmdPlatformRemove.Click += new System.EventHandler(this.cmdPlatformRemove_Click);
			// 
			// cmdPlatformModify
			// 
			this.cmdPlatformModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPlatformModify.Location = new System.Drawing.Point(84, 3);
			this.cmdPlatformModify.Name = "cmdPlatformModify";
			this.cmdPlatformModify.Size = new System.Drawing.Size(75, 23);
			this.cmdPlatformModify.TabIndex = 5;
			this.cmdPlatformModify.Text = "&Modify";
			this.cmdPlatformModify.UseVisualStyleBackColor = true;
			this.cmdPlatformModify.Click += new System.EventHandler(this.cmdPlatformModify_Click);
			// 
			// cmdPlatformAdd
			// 
			this.cmdPlatformAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPlatformAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdPlatformAdd.Name = "cmdPlatformAdd";
			this.cmdPlatformAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdPlatformAdd.TabIndex = 6;
			this.cmdPlatformAdd.Text = "&Add";
			this.cmdPlatformAdd.UseVisualStyleBackColor = true;
			this.cmdPlatformAdd.Click += new System.EventHandler(this.cmdPlatformAdd_Click);
			// 
			// lvPlatforms
			// 
			this.lvPlatforms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvPlatforms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPlatformTitle,
            this.chPlatformProducts});
			this.lvPlatforms.FullRowSelect = true;
			this.lvPlatforms.GridLines = true;
			this.lvPlatforms.HideSelection = false;
			this.lvPlatforms.Location = new System.Drawing.Point(3, 32);
			this.lvPlatforms.Name = "lvPlatforms";
			this.lvPlatforms.Size = new System.Drawing.Size(318, 341);
			this.lvPlatforms.TabIndex = 2;
			this.lvPlatforms.UseCompatibleStateImageBehavior = false;
			this.lvPlatforms.View = System.Windows.Forms.View.Details;
			this.lvPlatforms.ItemActivate += new System.EventHandler(this.lvPlatforms_ItemActivate);
			this.lvPlatforms.SelectedIndexChanged += new System.EventHandler(this.lvPlatforms_SelectedIndexChanged);
			// 
			// chPlatformTitle
			// 
			this.chPlatformTitle.Text = "Title";
			this.chPlatformTitle.Width = 249;
			// 
			// chPlatformProducts
			// 
			this.chPlatformProducts.Text = "Products";
			// 
			// pnlCategories
			// 
			this.pnlCategories.Controls.Add(this.cmdCategoryClear);
			this.pnlCategories.Controls.Add(this.cmdCategoryRemove);
			this.pnlCategories.Controls.Add(this.cmdCategoryModify);
			this.pnlCategories.Controls.Add(this.cmdCategoryAdd);
			this.pnlCategories.Controls.Add(this.lvCategories);
			this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCategories.Location = new System.Drawing.Point(0, 0);
			this.pnlCategories.Name = "pnlCategories";
			this.pnlCategories.Size = new System.Drawing.Size(324, 376);
			this.pnlCategories.TabIndex = 1;
			// 
			// cmdCategoryClear
			// 
			this.cmdCategoryClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCategoryClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCategoryClear.Location = new System.Drawing.Point(246, 3);
			this.cmdCategoryClear.Name = "cmdCategoryClear";
			this.cmdCategoryClear.Size = new System.Drawing.Size(75, 23);
			this.cmdCategoryClear.TabIndex = 3;
			this.cmdCategoryClear.Text = "&Clear";
			this.cmdCategoryClear.UseVisualStyleBackColor = true;
			this.cmdCategoryClear.Click += new System.EventHandler(this.cmdCategoryClear_Click);
			// 
			// cmdCategoryRemove
			// 
			this.cmdCategoryRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCategoryRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdCategoryRemove.Name = "cmdCategoryRemove";
			this.cmdCategoryRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdCategoryRemove.TabIndex = 4;
			this.cmdCategoryRemove.Text = "&Remove";
			this.cmdCategoryRemove.UseVisualStyleBackColor = true;
			this.cmdCategoryRemove.Click += new System.EventHandler(this.cmdCategoryRemove_Click);
			// 
			// cmdCategoryModify
			// 
			this.cmdCategoryModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCategoryModify.Location = new System.Drawing.Point(84, 3);
			this.cmdCategoryModify.Name = "cmdCategoryModify";
			this.cmdCategoryModify.Size = new System.Drawing.Size(75, 23);
			this.cmdCategoryModify.TabIndex = 5;
			this.cmdCategoryModify.Text = "&Modify";
			this.cmdCategoryModify.UseVisualStyleBackColor = true;
			this.cmdCategoryModify.Click += new System.EventHandler(this.cmdCategoryModify_Click);
			// 
			// cmdCategoryAdd
			// 
			this.cmdCategoryAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCategoryAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdCategoryAdd.Name = "cmdCategoryAdd";
			this.cmdCategoryAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdCategoryAdd.TabIndex = 6;
			this.cmdCategoryAdd.Text = "&Add";
			this.cmdCategoryAdd.UseVisualStyleBackColor = true;
			this.cmdCategoryAdd.Click += new System.EventHandler(this.cmdCategoryAdd_Click);
			// 
			// lvCategories
			// 
			this.lvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCategoryTitle,
            this.chCategoryProducts});
			this.lvCategories.FullRowSelect = true;
			this.lvCategories.GridLines = true;
			this.lvCategories.HideSelection = false;
			this.lvCategories.Location = new System.Drawing.Point(3, 32);
			this.lvCategories.Name = "lvCategories";
			this.lvCategories.Size = new System.Drawing.Size(318, 341);
			this.lvCategories.TabIndex = 2;
			this.lvCategories.UseCompatibleStateImageBehavior = false;
			this.lvCategories.View = System.Windows.Forms.View.Details;
			this.lvCategories.SelectedIndexChanged += new System.EventHandler(this.lvCategories_SelectedIndexChanged);
			// 
			// chCategoryTitle
			// 
			this.chCategoryTitle.Text = "Title";
			this.chCategoryTitle.Width = 113;
			// 
			// chCategoryProducts
			// 
			this.chCategoryProducts.Text = "Products";
			this.chCategoryProducts.Width = 81;
			// 
			// pnlGeneral
			// 
			this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGeneral.Location = new System.Drawing.Point(0, 0);
			this.pnlGeneral.Name = "pnlGeneral";
			this.pnlGeneral.Size = new System.Drawing.Size(324, 376);
			this.pnlGeneral.TabIndex = 0;
			// 
			// pnlProducts
			// 
			this.pnlProducts.Controls.Add(this.cmdProductClear);
			this.pnlProducts.Controls.Add(this.cmdProductRemove);
			this.pnlProducts.Controls.Add(this.cmdProductModify);
			this.pnlProducts.Controls.Add(this.cmdProductAdd);
			this.pnlProducts.Controls.Add(this.lvProducts);
			this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlProducts.Location = new System.Drawing.Point(0, 0);
			this.pnlProducts.Name = "pnlProducts";
			this.pnlProducts.Size = new System.Drawing.Size(324, 376);
			this.pnlProducts.TabIndex = 4;
			// 
			// cmdProductClear
			// 
			this.cmdProductClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdProductClear.Enabled = false;
			this.cmdProductClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdProductClear.Location = new System.Drawing.Point(246, 3);
			this.cmdProductClear.Name = "cmdProductClear";
			this.cmdProductClear.Size = new System.Drawing.Size(75, 23);
			this.cmdProductClear.TabIndex = 1;
			this.cmdProductClear.Text = "&Clear";
			this.cmdProductClear.UseVisualStyleBackColor = true;
			this.cmdProductClear.Click += new System.EventHandler(this.cmdProductClear_Click);
			// 
			// cmdProductRemove
			// 
			this.cmdProductRemove.Enabled = false;
			this.cmdProductRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdProductRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdProductRemove.Name = "cmdProductRemove";
			this.cmdProductRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdProductRemove.TabIndex = 1;
			this.cmdProductRemove.Text = "&Remove";
			this.cmdProductRemove.UseVisualStyleBackColor = true;
			this.cmdProductRemove.Click += new System.EventHandler(this.cmdProductRemove_Click);
			// 
			// cmdProductModify
			// 
			this.cmdProductModify.Enabled = false;
			this.cmdProductModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdProductModify.Location = new System.Drawing.Point(84, 3);
			this.cmdProductModify.Name = "cmdProductModify";
			this.cmdProductModify.Size = new System.Drawing.Size(75, 23);
			this.cmdProductModify.TabIndex = 1;
			this.cmdProductModify.Text = "&Modify";
			this.cmdProductModify.UseVisualStyleBackColor = true;
			this.cmdProductModify.Click += new System.EventHandler(this.cmdProductModify_Click);
			// 
			// cmdProductAdd
			// 
			this.cmdProductAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdProductAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdProductAdd.Name = "cmdProductAdd";
			this.cmdProductAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdProductAdd.TabIndex = 1;
			this.cmdProductAdd.Text = "&Add";
			this.cmdProductAdd.UseVisualStyleBackColor = true;
			this.cmdProductAdd.Click += new System.EventHandler(this.cmdProductAdd_Click);
			// 
			// lvProducts
			// 
			this.lvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProductTitle,
            this.chProductCategory,
            this.chProductPlatform,
            this.chProductListing});
			this.lvProducts.FullRowSelect = true;
			this.lvProducts.GridLines = true;
			this.lvProducts.HideSelection = false;
			this.lvProducts.Location = new System.Drawing.Point(3, 32);
			this.lvProducts.Name = "lvProducts";
			this.lvProducts.Size = new System.Drawing.Size(318, 341);
			this.lvProducts.TabIndex = 0;
			this.lvProducts.UseCompatibleStateImageBehavior = false;
			this.lvProducts.View = System.Windows.Forms.View.Details;
			this.lvProducts.ItemActivate += new System.EventHandler(this.lvProducts_ItemActivate);
			this.lvProducts.SelectedIndexChanged += new System.EventHandler(this.lvProducts_SelectedIndexChanged);
			// 
			// chProductTitle
			// 
			this.chProductTitle.Text = "Title";
			this.chProductTitle.Width = 113;
			// 
			// chProductCategory
			// 
			this.chProductCategory.Text = "Category";
			// 
			// chProductPlatform
			// 
			this.chProductPlatform.Text = "Platform";
			// 
			// chProductListing
			// 
			this.chProductListing.Text = "Listing";
			this.chProductListing.Width = 81;
			// 
			// pnlListings
			// 
			this.pnlListings.Controls.Add(this.cmdListingClear);
			this.pnlListings.Controls.Add(this.cmdListingRemove);
			this.pnlListings.Controls.Add(this.cmdListingModify);
			this.pnlListings.Controls.Add(this.cmdListingsAdd);
			this.pnlListings.Controls.Add(this.lvListings);
			this.pnlListings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlListings.Location = new System.Drawing.Point(0, 0);
			this.pnlListings.Name = "pnlListings";
			this.pnlListings.Size = new System.Drawing.Size(324, 376);
			this.pnlListings.TabIndex = 3;
			// 
			// cmdListingClear
			// 
			this.cmdListingClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdListingClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdListingClear.Location = new System.Drawing.Point(246, 3);
			this.cmdListingClear.Name = "cmdListingClear";
			this.cmdListingClear.Size = new System.Drawing.Size(75, 23);
			this.cmdListingClear.TabIndex = 3;
			this.cmdListingClear.Text = "&Clear";
			this.cmdListingClear.UseVisualStyleBackColor = true;
			this.cmdListingClear.Click += new System.EventHandler(this.cmdListingClear_Click);
			// 
			// cmdListingRemove
			// 
			this.cmdListingRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdListingRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdListingRemove.Name = "cmdListingRemove";
			this.cmdListingRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdListingRemove.TabIndex = 4;
			this.cmdListingRemove.Text = "&Remove";
			this.cmdListingRemove.UseVisualStyleBackColor = true;
			this.cmdListingRemove.Click += new System.EventHandler(this.cmdListingRemove_Click);
			// 
			// cmdListingModify
			// 
			this.cmdListingModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdListingModify.Location = new System.Drawing.Point(84, 3);
			this.cmdListingModify.Name = "cmdListingModify";
			this.cmdListingModify.Size = new System.Drawing.Size(75, 23);
			this.cmdListingModify.TabIndex = 5;
			this.cmdListingModify.Text = "&Modify";
			this.cmdListingModify.UseVisualStyleBackColor = true;
			this.cmdListingModify.Click += new System.EventHandler(this.cmdListingModify_Click);
			// 
			// cmdListingsAdd
			// 
			this.cmdListingsAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdListingsAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdListingsAdd.Name = "cmdListingsAdd";
			this.cmdListingsAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdListingsAdd.TabIndex = 6;
			this.cmdListingsAdd.Text = "&Add";
			this.cmdListingsAdd.UseVisualStyleBackColor = true;
			this.cmdListingsAdd.Click += new System.EventHandler(this.cmdListingAdd_Click);
			// 
			// lvListings
			// 
			this.lvListings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvListings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chListingTitle,
            this.chListingProductCount});
			this.lvListings.FullRowSelect = true;
			this.lvListings.GridLines = true;
			this.lvListings.HideSelection = false;
			this.lvListings.Location = new System.Drawing.Point(3, 32);
			this.lvListings.Name = "lvListings";
			this.lvListings.Size = new System.Drawing.Size(318, 341);
			this.lvListings.TabIndex = 2;
			this.lvListings.UseCompatibleStateImageBehavior = false;
			this.lvListings.View = System.Windows.Forms.View.Details;
			// 
			// chListingTitle
			// 
			this.chListingTitle.Text = "Title";
			this.chListingTitle.Width = 252;
			// 
			// chListingProductCount
			// 
			this.chListingProductCount.Text = "Products";
			// 
			// ArkAnglesCatalogEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scMain);
			this.MinimumSize = new System.Drawing.Size(492, 376);
			this.Name = "ArkAnglesCatalogEditor";
			this.Size = new System.Drawing.Size(492, 376);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.ResumeLayout(false);
			this.pnlPlatforms.ResumeLayout(false);
			this.pnlCategories.ResumeLayout(false);
			this.pnlProducts.ResumeLayout(false);
			this.pnlListings.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Panel pnlGeneral;
		private System.Windows.Forms.Panel pnlListings;
		private System.Windows.Forms.Panel pnlPlatforms;
		private System.Windows.Forms.Panel pnlCategories;
		private System.Windows.Forms.Panel pnlProducts;
		private System.Windows.Forms.Button cmdProductClear;
		private System.Windows.Forms.Button cmdProductRemove;
		private System.Windows.Forms.Button cmdProductModify;
		private System.Windows.Forms.Button cmdProductAdd;
		private System.Windows.Forms.ListView lvProducts;
		private System.Windows.Forms.ColumnHeader chProductTitle;
		private System.Windows.Forms.ColumnHeader chProductCategory;
		private System.Windows.Forms.ColumnHeader chProductPlatform;
		private System.Windows.Forms.ColumnHeader chProductListing;
		private System.Windows.Forms.Button cmdListingClear;
		private System.Windows.Forms.Button cmdListingRemove;
		private System.Windows.Forms.Button cmdListingModify;
		private System.Windows.Forms.Button cmdListingsAdd;
		private System.Windows.Forms.ListView lvListings;
		private System.Windows.Forms.ColumnHeader chListingTitle;
		private System.Windows.Forms.Button cmdPlatformClear;
		private System.Windows.Forms.Button cmdPlatformRemove;
		private System.Windows.Forms.Button cmdPlatformModify;
		private System.Windows.Forms.Button cmdPlatformAdd;
		private System.Windows.Forms.ListView lvPlatforms;
		private System.Windows.Forms.ColumnHeader chPlatformTitle;
		private System.Windows.Forms.Button cmdCategoryClear;
		private System.Windows.Forms.Button cmdCategoryRemove;
		private System.Windows.Forms.Button cmdCategoryModify;
		private System.Windows.Forms.Button cmdCategoryAdd;
		private System.Windows.Forms.ListView lvCategories;
		private System.Windows.Forms.ColumnHeader chCategoryTitle;
		private System.Windows.Forms.ColumnHeader chCategoryProducts;
		private System.Windows.Forms.ColumnHeader chListingProductCount;
		private System.Windows.Forms.ColumnHeader chPlatformProducts;
	}
}
