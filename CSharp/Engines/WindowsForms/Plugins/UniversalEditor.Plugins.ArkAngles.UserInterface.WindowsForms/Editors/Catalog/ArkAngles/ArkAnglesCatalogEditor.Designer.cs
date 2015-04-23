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
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("General");
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Categories");
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Platforms");
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Listings");
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Products");
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.pnlGeneral = new System.Windows.Forms.Panel();
			this.pnlCategories = new System.Windows.Forms.Panel();
			this.pnlPlatforms = new System.Windows.Forms.Panel();
			this.pnlListings = new System.Windows.Forms.Panel();
			this.pnlProducts = new System.Windows.Forms.Panel();
			this.lvProducts = new System.Windows.Forms.ListView();
			this.cmdProductAdd = new System.Windows.Forms.Button();
			this.cmdProductModify = new System.Windows.Forms.Button();
			this.cmdProductRemove = new System.Windows.Forms.Button();
			this.cmdProductClear = new System.Windows.Forms.Button();
			this.chProductTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductPlatform = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chProductListing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.listView3 = new System.Windows.Forms.ListView();
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.pnlCategories.SuspendLayout();
			this.pnlPlatforms.SuspendLayout();
			this.pnlListings.SuspendLayout();
			this.pnlProducts.SuspendLayout();
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
			this.scMain.Panel2.Controls.Add(this.pnlProducts);
			this.scMain.Panel2.Controls.Add(this.pnlListings);
			this.scMain.Panel2.Controls.Add(this.pnlPlatforms);
			this.scMain.Panel2.Controls.Add(this.pnlCategories);
			this.scMain.Panel2.Controls.Add(this.pnlGeneral);
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
			treeNode16.Name = "nodeGeneral";
			treeNode16.Text = "General";
			treeNode17.Name = "nodeCategories";
			treeNode17.Text = "Categories";
			treeNode18.Name = "nodePlatforms";
			treeNode18.Text = "Platforms";
			treeNode19.Name = "nodeListings";
			treeNode19.Text = "Listings";
			treeNode20.Name = "nodeProducts";
			treeNode20.Text = "Products";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
			this.tv.Size = new System.Drawing.Size(164, 376);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// pnlGeneral
			// 
			this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGeneral.Location = new System.Drawing.Point(0, 0);
			this.pnlGeneral.Name = "pnlGeneral";
			this.pnlGeneral.Size = new System.Drawing.Size(324, 376);
			this.pnlGeneral.TabIndex = 0;
			// 
			// pnlCategories
			// 
			this.pnlCategories.Controls.Add(this.button9);
			this.pnlCategories.Controls.Add(this.button10);
			this.pnlCategories.Controls.Add(this.button11);
			this.pnlCategories.Controls.Add(this.button12);
			this.pnlCategories.Controls.Add(this.listView3);
			this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCategories.Location = new System.Drawing.Point(0, 0);
			this.pnlCategories.Name = "pnlCategories";
			this.pnlCategories.Size = new System.Drawing.Size(324, 376);
			this.pnlCategories.TabIndex = 1;
			// 
			// pnlPlatforms
			// 
			this.pnlPlatforms.Controls.Add(this.button5);
			this.pnlPlatforms.Controls.Add(this.button6);
			this.pnlPlatforms.Controls.Add(this.button7);
			this.pnlPlatforms.Controls.Add(this.button8);
			this.pnlPlatforms.Controls.Add(this.listView2);
			this.pnlPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPlatforms.Location = new System.Drawing.Point(0, 0);
			this.pnlPlatforms.Name = "pnlPlatforms";
			this.pnlPlatforms.Size = new System.Drawing.Size(324, 376);
			this.pnlPlatforms.TabIndex = 2;
			// 
			// pnlListings
			// 
			this.pnlListings.Controls.Add(this.button1);
			this.pnlListings.Controls.Add(this.button2);
			this.pnlListings.Controls.Add(this.button3);
			this.pnlListings.Controls.Add(this.button4);
			this.pnlListings.Controls.Add(this.listView1);
			this.pnlListings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlListings.Location = new System.Drawing.Point(0, 0);
			this.pnlListings.Name = "pnlListings";
			this.pnlListings.Size = new System.Drawing.Size(324, 376);
			this.pnlListings.TabIndex = 3;
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
			// 
			// cmdProductAdd
			// 
			this.cmdProductAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdProductAdd.Name = "cmdProductAdd";
			this.cmdProductAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdProductAdd.TabIndex = 1;
			this.cmdProductAdd.Text = "&Add";
			this.cmdProductAdd.UseVisualStyleBackColor = true;
			this.cmdProductAdd.Click += new System.EventHandler(this.cmdProductAdd_Click);
			// 
			// cmdProductModify
			// 
			this.cmdProductModify.Location = new System.Drawing.Point(84, 3);
			this.cmdProductModify.Name = "cmdProductModify";
			this.cmdProductModify.Size = new System.Drawing.Size(75, 23);
			this.cmdProductModify.TabIndex = 1;
			this.cmdProductModify.Text = "&Modify";
			this.cmdProductModify.UseVisualStyleBackColor = true;
			// 
			// cmdProductRemove
			// 
			this.cmdProductRemove.Location = new System.Drawing.Point(165, 3);
			this.cmdProductRemove.Name = "cmdProductRemove";
			this.cmdProductRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdProductRemove.TabIndex = 1;
			this.cmdProductRemove.Text = "&Remove";
			this.cmdProductRemove.UseVisualStyleBackColor = true;
			// 
			// cmdProductClear
			// 
			this.cmdProductClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdProductClear.Location = new System.Drawing.Point(246, 3);
			this.cmdProductClear.Name = "cmdProductClear";
			this.cmdProductClear.Size = new System.Drawing.Size(75, 23);
			this.cmdProductClear.TabIndex = 1;
			this.cmdProductClear.Text = "&Clear";
			this.cmdProductClear.UseVisualStyleBackColor = true;
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
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(246, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "&Clear";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(165, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "&Remove";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(84, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "&Modify";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(3, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 6;
			this.button4.Text = "&Add";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(3, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(318, 341);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Title";
			this.columnHeader1.Width = 113;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Category";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Platform";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Listing";
			this.columnHeader4.Width = 81;
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Location = new System.Drawing.Point(246, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 3;
			this.button5.Text = "&Clear";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(165, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 23);
			this.button6.TabIndex = 4;
			this.button6.Text = "&Remove";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(84, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(75, 23);
			this.button7.TabIndex = 5;
			this.button7.Text = "&Modify";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(3, 3);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(75, 23);
			this.button8.TabIndex = 6;
			this.button8.Text = "&Add";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// listView2
			// 
			this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.HideSelection = false;
			this.listView2.Location = new System.Drawing.Point(3, 32);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(318, 341);
			this.listView2.TabIndex = 2;
			this.listView2.UseCompatibleStateImageBehavior = false;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Title";
			this.columnHeader5.Width = 113;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Category";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Platform";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Listing";
			this.columnHeader8.Width = 81;
			// 
			// button9
			// 
			this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button9.Location = new System.Drawing.Point(246, 3);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(75, 23);
			this.button9.TabIndex = 3;
			this.button9.Text = "&Clear";
			this.button9.UseVisualStyleBackColor = true;
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(165, 3);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(75, 23);
			this.button10.TabIndex = 4;
			this.button10.Text = "&Remove";
			this.button10.UseVisualStyleBackColor = true;
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(84, 3);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(75, 23);
			this.button11.TabIndex = 5;
			this.button11.Text = "&Modify";
			this.button11.UseVisualStyleBackColor = true;
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(3, 3);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(75, 23);
			this.button12.TabIndex = 6;
			this.button12.Text = "&Add";
			this.button12.UseVisualStyleBackColor = true;
			// 
			// listView3
			// 
			this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
			this.listView3.FullRowSelect = true;
			this.listView3.GridLines = true;
			this.listView3.HideSelection = false;
			this.listView3.Location = new System.Drawing.Point(3, 32);
			this.listView3.Name = "listView3";
			this.listView3.Size = new System.Drawing.Size(318, 341);
			this.listView3.TabIndex = 2;
			this.listView3.UseCompatibleStateImageBehavior = false;
			this.listView3.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Title";
			this.columnHeader9.Width = 113;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Category";
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Platform";
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Listing";
			this.columnHeader12.Width = 81;
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
			this.pnlCategories.ResumeLayout(false);
			this.pnlPlatforms.ResumeLayout(false);
			this.pnlListings.ResumeLayout(false);
			this.pnlProducts.ResumeLayout(false);
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
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.ListView listView3;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
	}
}
