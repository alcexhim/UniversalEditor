namespace UniversalEditor.Editors.Contact
{
	partial class ContactEditor
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Summary");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Names and E-mail Addresses");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Physical Addresses");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Employment");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Family and Relationships");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Notes");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Digital IDs and Certificates");
			this.sc = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.pnlGeneral = new System.Windows.Forms.Panel();
			this.tblGeneral = new System.Windows.Forms.TableLayoutPanel();
			this.fraEmailAddresses = new System.Windows.Forms.GroupBox();
			this.lvEmailAddresses = new AwesomeControls.CollectionListView.CollectionListViewControl();
			this.fraNames = new System.Windows.Forms.GroupBox();
			this.lvNames = new AwesomeControls.CollectionListView.CollectionListViewControl();
			this.sc.Panel1.SuspendLayout();
			this.sc.Panel2.SuspendLayout();
			this.sc.SuspendLayout();
			this.pnlGeneral.SuspendLayout();
			this.tblGeneral.SuspendLayout();
			this.fraEmailAddresses.SuspendLayout();
			this.fraNames.SuspendLayout();
			this.SuspendLayout();
			// 
			// sc
			// 
			this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.sc.IsSplitterFixed = true;
			this.sc.Location = new System.Drawing.Point(0, 0);
			this.sc.Name = "sc";
			// 
			// sc.Panel1
			// 
			this.sc.Panel1.Controls.Add(this.tv);
			// 
			// sc.Panel2
			// 
			this.sc.Panel2.Controls.Add(this.pnlGeneral);
			this.sc.Size = new System.Drawing.Size(588, 289);
			this.sc.SplitterDistance = 163;
			this.sc.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.FullRowSelect = true;
			this.tv.ItemHeight = 26;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode1.Name = "nodeSummary";
			treeNode1.Text = "Summary";
			treeNode2.Name = "nodeNameAndEmail";
			treeNode2.Text = "Names and E-mail Addresses";
			treeNode3.Name = "nodePhysicalAddresses";
			treeNode3.Text = "Physical Addresses";
			treeNode4.Name = "nodeEmployment";
			treeNode4.Text = "Employment";
			treeNode5.Name = "nodeFamilyAndRelationships";
			treeNode5.Text = "Family and Relationships";
			treeNode6.Name = "nodeNotes";
			treeNode6.Text = "Notes";
			treeNode7.Name = "nodeDigitalIDs";
			treeNode7.Text = "Digital IDs and Certificates";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
			this.tv.ShowLines = false;
			this.tv.ShowRootLines = false;
			this.tv.Size = new System.Drawing.Size(163, 289);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// pnlGeneral
			// 
			this.pnlGeneral.Controls.Add(this.tblGeneral);
			this.pnlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGeneral.Location = new System.Drawing.Point(0, 0);
			this.pnlGeneral.Name = "pnlGeneral";
			this.pnlGeneral.Size = new System.Drawing.Size(421, 289);
			this.pnlGeneral.TabIndex = 0;
			// 
			// tblGeneral
			// 
			this.tblGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tblGeneral.ColumnCount = 1;
			this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblGeneral.Controls.Add(this.fraEmailAddresses, 0, 1);
			this.tblGeneral.Controls.Add(this.fraNames, 0, 0);
			this.tblGeneral.Location = new System.Drawing.Point(3, 3);
			this.tblGeneral.Name = "tblGeneral";
			this.tblGeneral.RowCount = 2;
			this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblGeneral.Size = new System.Drawing.Size(415, 283);
			this.tblGeneral.TabIndex = 1;
			// 
			// fraEmailAddresses
			// 
			this.fraEmailAddresses.Controls.Add(this.lvEmailAddresses);
			this.fraEmailAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraEmailAddresses.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraEmailAddresses.Location = new System.Drawing.Point(3, 144);
			this.fraEmailAddresses.Name = "fraEmailAddresses";
			this.fraEmailAddresses.Size = new System.Drawing.Size(409, 136);
			this.fraEmailAddresses.TabIndex = 1;
			this.fraEmailAddresses.TabStop = false;
			this.fraEmailAddresses.Text = "E-mail addresses";
			// 
			// lvEmailAddresses
			// 
			this.lvEmailAddresses.AllowItemInsert = true;
			this.lvEmailAddresses.AllowItemModify = true;
			this.lvEmailAddresses.AllowItemRemove = true;
			this.lvEmailAddresses.AllowItemReorder = false;
			this.lvEmailAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvEmailAddresses.FullRowSelect = false;
			this.lvEmailAddresses.HideSelection = true;
			this.lvEmailAddresses.ItemNamePlural = "items";
			this.lvEmailAddresses.ItemNameSingular = "item";
			this.lvEmailAddresses.Location = new System.Drawing.Point(6, 19);
			this.lvEmailAddresses.MultiSelect = false;
			this.lvEmailAddresses.Name = "lvEmailAddresses";
			this.lvEmailAddresses.ShowGridLines = true;
			this.lvEmailAddresses.Size = new System.Drawing.Size(397, 111);
			this.lvEmailAddresses.TabIndex = 0;
			this.lvEmailAddresses.RequestItemProperties += new AwesomeControls.CollectionListView.ItemPropertiesEventHandler(this.lvEmailAddresses_RequestItemProperties);
			// 
			// fraNames
			// 
			this.fraNames.Controls.Add(this.lvNames);
			this.fraNames.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraNames.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraNames.Location = new System.Drawing.Point(3, 3);
			this.fraNames.Name = "fraNames";
			this.fraNames.Size = new System.Drawing.Size(409, 135);
			this.fraNames.TabIndex = 0;
			this.fraNames.TabStop = false;
			this.fraNames.Text = "Names";
			// 
			// lvNames
			// 
			this.lvNames.AllowItemInsert = true;
			this.lvNames.AllowItemModify = true;
			this.lvNames.AllowItemRemove = true;
			this.lvNames.AllowItemReorder = false;
			this.lvNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvNames.FullRowSelect = false;
			this.lvNames.HideSelection = true;
			this.lvNames.ItemNamePlural = "items";
			this.lvNames.ItemNameSingular = "item";
			this.lvNames.Location = new System.Drawing.Point(6, 22);
			this.lvNames.MultiSelect = false;
			this.lvNames.Name = "lvNames";
			this.lvNames.ShowGridLines = true;
			this.lvNames.Size = new System.Drawing.Size(397, 107);
			this.lvNames.TabIndex = 0;
			this.lvNames.RequestItemProperties += new AwesomeControls.CollectionListView.ItemPropertiesEventHandler(this.lvNames_RequestItemProperties);
			// 
			// ContactEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.sc);
			this.Name = "ContactEditor";
			this.Size = new System.Drawing.Size(588, 289);
			this.sc.Panel1.ResumeLayout(false);
			this.sc.Panel2.ResumeLayout(false);
			this.sc.ResumeLayout(false);
			this.pnlGeneral.ResumeLayout(false);
			this.tblGeneral.ResumeLayout(false);
			this.fraEmailAddresses.ResumeLayout(false);
			this.fraNames.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer sc;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Panel pnlGeneral;
		private System.Windows.Forms.GroupBox fraNames;
		private System.Windows.Forms.TableLayoutPanel tblGeneral;
		private System.Windows.Forms.GroupBox fraEmailAddresses;
		private AwesomeControls.CollectionListView.CollectionListViewControl lvEmailAddresses;
		private AwesomeControls.CollectionListView.CollectionListViewControl lvNames;
	}
}
