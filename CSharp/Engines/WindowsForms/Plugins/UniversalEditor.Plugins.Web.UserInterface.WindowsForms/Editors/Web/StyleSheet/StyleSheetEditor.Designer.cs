namespace UniversalEditor.Editors.Web.StyleSheet
{
	partial class StyleSheetEditor
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Font");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Block");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Background");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Border");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Box");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Position");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Layout");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("List");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Table");
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.fraCode = new System.Windows.Forms.GroupBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.fraPreview = new System.Windows.Forms.GroupBox();
			this.pnlPreview = new System.Windows.Forms.Panel();
			this.fraStyleBuilder = new System.Windows.Forms.GroupBox();
			this.pnlFont = new System.Windows.Forms.Panel();
			this.chkFontTextDecorationBlink = new System.Windows.Forms.CheckBox();
			this.chkFontTextDecorationLineThrough = new System.Windows.Forms.CheckBox();
			this.chkFontTextDecorationOverline = new System.Windows.Forms.CheckBox();
			this.chkFontTextDecorationUnderline = new System.Windows.Forms.CheckBox();
			this.cmdFontColor = new System.Windows.Forms.Button();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cboFontFamily = new System.Windows.Forms.ComboBox();
			this.lblFontFamily = new System.Windows.Forms.Label();
			this.tvStyleBuilder = new System.Windows.Forms.TreeView();
			this.txtFontSize = new UniversalEditor.Controls.Web.StyleSheet.MeasurementUpDown.MeasurementUpDownControl();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.fraCode.SuspendLayout();
			this.fraPreview.SuspendLayout();
			this.fraStyleBuilder.SuspendLayout();
			this.pnlFont.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tv);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.fraCode);
			this.splitContainer1.Panel2.Controls.Add(this.fraPreview);
			this.splitContainer1.Panel2.Controls.Add(this.fraStyleBuilder);
			this.splitContainer1.Size = new System.Drawing.Size(560, 425);
			this.splitContainer1.SplitterDistance = 186;
			this.splitContainer1.TabIndex = 0;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(186, 425);
			this.tv.TabIndex = 0;
			// 
			// fraCode
			// 
			this.fraCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraCode.Controls.Add(this.txtCode);
			this.fraCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraCode.Location = new System.Drawing.Point(3, 341);
			this.fraCode.Name = "fraCode";
			this.fraCode.Size = new System.Drawing.Size(364, 81);
			this.fraCode.TabIndex = 1;
			this.fraCode.TabStop = false;
			this.fraCode.Text = "Code";
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.Location = new System.Drawing.Point(6, 19);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ReadOnly = true;
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCode.Size = new System.Drawing.Size(352, 56);
			this.txtCode.TabIndex = 0;
			// 
			// fraPreview
			// 
			this.fraPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraPreview.Controls.Add(this.pnlPreview);
			this.fraPreview.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraPreview.Location = new System.Drawing.Point(3, 254);
			this.fraPreview.Name = "fraPreview";
			this.fraPreview.Size = new System.Drawing.Size(364, 81);
			this.fraPreview.TabIndex = 1;
			this.fraPreview.TabStop = false;
			this.fraPreview.Text = "Preview";
			// 
			// pnlPreview
			// 
			this.pnlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPreview.BackColor = System.Drawing.Color.White;
			this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPreview.Location = new System.Drawing.Point(6, 19);
			this.pnlPreview.Name = "pnlPreview";
			this.pnlPreview.Size = new System.Drawing.Size(352, 56);
			this.pnlPreview.TabIndex = 0;
			// 
			// fraStyleBuilder
			// 
			this.fraStyleBuilder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraStyleBuilder.Controls.Add(this.pnlFont);
			this.fraStyleBuilder.Controls.Add(this.tvStyleBuilder);
			this.fraStyleBuilder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraStyleBuilder.Location = new System.Drawing.Point(3, 3);
			this.fraStyleBuilder.Name = "fraStyleBuilder";
			this.fraStyleBuilder.Size = new System.Drawing.Size(364, 245);
			this.fraStyleBuilder.TabIndex = 0;
			this.fraStyleBuilder.TabStop = false;
			this.fraStyleBuilder.Text = "Style Builder";
			// 
			// pnlFont
			// 
			this.pnlFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFont.Controls.Add(this.txtFontSize);
			this.pnlFont.Controls.Add(this.chkFontTextDecorationBlink);
			this.pnlFont.Controls.Add(this.chkFontTextDecorationLineThrough);
			this.pnlFont.Controls.Add(this.chkFontTextDecorationOverline);
			this.pnlFont.Controls.Add(this.chkFontTextDecorationUnderline);
			this.pnlFont.Controls.Add(this.cmdFontColor);
			this.pnlFont.Controls.Add(this.comboBox4);
			this.pnlFont.Controls.Add(this.label6);
			this.pnlFont.Controls.Add(this.label5);
			this.pnlFont.Controls.Add(this.comboBox3);
			this.pnlFont.Controls.Add(this.label4);
			this.pnlFont.Controls.Add(this.comboBox2);
			this.pnlFont.Controls.Add(this.label3);
			this.pnlFont.Controls.Add(this.comboBox1);
			this.pnlFont.Controls.Add(this.label2);
			this.pnlFont.Controls.Add(this.label1);
			this.pnlFont.Controls.Add(this.cboFontFamily);
			this.pnlFont.Controls.Add(this.lblFontFamily);
			this.pnlFont.Location = new System.Drawing.Point(101, 19);
			this.pnlFont.Name = "pnlFont";
			this.pnlFont.Size = new System.Drawing.Size(257, 220);
			this.pnlFont.TabIndex = 1;
			// 
			// chkFontTextDecorationBlink
			// 
			this.chkFontTextDecorationBlink.AutoSize = true;
			this.chkFontTextDecorationBlink.Location = new System.Drawing.Point(139, 128);
			this.chkFontTextDecorationBlink.Name = "chkFontTextDecorationBlink";
			this.chkFontTextDecorationBlink.Size = new System.Drawing.Size(49, 17);
			this.chkFontTextDecorationBlink.TabIndex = 5;
			this.chkFontTextDecorationBlink.Text = "&Blink";
			this.chkFontTextDecorationBlink.UseVisualStyleBackColor = true;
			// 
			// chkFontTextDecorationLineThrough
			// 
			this.chkFontTextDecorationLineThrough.AutoSize = true;
			this.chkFontTextDecorationLineThrough.Location = new System.Drawing.Point(139, 105);
			this.chkFontTextDecorationLineThrough.Name = "chkFontTextDecorationLineThrough";
			this.chkFontTextDecorationLineThrough.Size = new System.Drawing.Size(89, 17);
			this.chkFontTextDecorationLineThrough.TabIndex = 5;
			this.chkFontTextDecorationLineThrough.Text = "Strikethrough";
			this.chkFontTextDecorationLineThrough.UseVisualStyleBackColor = true;
			// 
			// chkFontTextDecorationOverline
			// 
			this.chkFontTextDecorationOverline.AutoSize = true;
			this.chkFontTextDecorationOverline.Location = new System.Drawing.Point(139, 82);
			this.chkFontTextDecorationOverline.Name = "chkFontTextDecorationOverline";
			this.chkFontTextDecorationOverline.Size = new System.Drawing.Size(65, 17);
			this.chkFontTextDecorationOverline.TabIndex = 5;
			this.chkFontTextDecorationOverline.Text = "&Overline";
			this.chkFontTextDecorationOverline.UseVisualStyleBackColor = true;
			// 
			// chkFontTextDecorationUnderline
			// 
			this.chkFontTextDecorationUnderline.AutoSize = true;
			this.chkFontTextDecorationUnderline.Location = new System.Drawing.Point(139, 59);
			this.chkFontTextDecorationUnderline.Name = "chkFontTextDecorationUnderline";
			this.chkFontTextDecorationUnderline.Size = new System.Drawing.Size(71, 17);
			this.chkFontTextDecorationUnderline.TabIndex = 5;
			this.chkFontTextDecorationUnderline.Text = "&Underline";
			this.chkFontTextDecorationUnderline.UseVisualStyleBackColor = true;
			// 
			// cmdFontColor
			// 
			this.cmdFontColor.BackColor = System.Drawing.Color.Black;
			this.cmdFontColor.Location = new System.Drawing.Point(66, 165);
			this.cmdFontColor.Name = "cmdFontColor";
			this.cmdFontColor.Size = new System.Drawing.Size(67, 23);
			this.cmdFontColor.TabIndex = 4;
			this.cmdFontColor.UseVisualStyleBackColor = false;
			this.cmdFontColor.Click += new System.EventHandler(this.cmdFontColor_Click);
			// 
			// comboBox4
			// 
			this.comboBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Items.AddRange(new object[] {
            "normal",
            "small-caps",
            "inherit"});
			this.comboBox4.Location = new System.Drawing.Point(66, 138);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(67, 21);
			this.comboBox4.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(3, 170);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Color:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(3, 141);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "T&ransform:";
			// 
			// comboBox3
			// 
			this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Items.AddRange(new object[] {
            "normal",
            "small-caps",
            "inherit"});
			this.comboBox3.Location = new System.Drawing.Point(66, 111);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(67, 21);
			this.comboBox3.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(3, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "&Variant:";
			// 
			// comboBox2
			// 
			this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[] {
            "italic",
            "normal",
            "oblique",
            "inherit"});
			this.comboBox2.Location = new System.Drawing.Point(66, 84);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(67, 21);
			this.comboBox2.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(3, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "St&yle:";
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "normal",
            "bold",
            "lighter",
            "bolder",
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "inherit"});
			this.comboBox1.Location = new System.Drawing.Point(66, 57);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(67, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(3, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "&Weight:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(3, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Size:";
			// 
			// cboFontFamily
			// 
			this.cboFontFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboFontFamily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboFontFamily.FormattingEnabled = true;
			this.cboFontFamily.Location = new System.Drawing.Point(66, 3);
			this.cboFontFamily.Name = "cboFontFamily";
			this.cboFontFamily.Size = new System.Drawing.Size(188, 21);
			this.cboFontFamily.TabIndex = 1;
			// 
			// lblFontFamily
			// 
			this.lblFontFamily.AutoSize = true;
			this.lblFontFamily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFontFamily.Location = new System.Drawing.Point(3, 6);
			this.lblFontFamily.Name = "lblFontFamily";
			this.lblFontFamily.Size = new System.Drawing.Size(39, 13);
			this.lblFontFamily.TabIndex = 0;
			this.lblFontFamily.Text = "&Family:";
			// 
			// tvStyleBuilder
			// 
			this.tvStyleBuilder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tvStyleBuilder.FullRowSelect = true;
			this.tvStyleBuilder.HideSelection = false;
			this.tvStyleBuilder.ItemHeight = 24;
			this.tvStyleBuilder.Location = new System.Drawing.Point(6, 19);
			this.tvStyleBuilder.Name = "tvStyleBuilder";
			treeNode1.Name = "nodeFont";
			treeNode1.Text = "Font";
			treeNode2.Name = "nodeBlock";
			treeNode2.Text = "Block";
			treeNode3.Name = "nodeBackground";
			treeNode3.Text = "Background";
			treeNode4.Name = "nodeBorder";
			treeNode4.Text = "Border";
			treeNode5.Name = "nodeBox";
			treeNode5.Text = "Box";
			treeNode6.Name = "nodePosition";
			treeNode6.Text = "Position";
			treeNode7.Name = "nodeLayout";
			treeNode7.Text = "Layout";
			treeNode8.Name = "nodeList";
			treeNode8.Text = "List";
			treeNode9.Name = "nodeTable";
			treeNode9.Text = "Table";
			this.tvStyleBuilder.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
			this.tvStyleBuilder.ShowLines = false;
			this.tvStyleBuilder.ShowRootLines = false;
			this.tvStyleBuilder.Size = new System.Drawing.Size(89, 220);
			this.tvStyleBuilder.TabIndex = 0;
			// 
			// txtFontSize
			// 
			this.txtFontSize.Location = new System.Drawing.Point(66, 30);
			this.txtFontSize.MaximumValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.txtFontSize.MinimumSize = new System.Drawing.Size(120, 21);
			this.txtFontSize.MinimumValue = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.txtFontSize.Name = "txtFontSize";
			this.txtFontSize.Size = new System.Drawing.Size(138, 21);
			this.txtFontSize.TabIndex = 6;
			// 
			// StyleSheetEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(560, 425);
			this.Name = "StyleSheetEditor";
			this.Size = new System.Drawing.Size(560, 425);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.fraCode.ResumeLayout(false);
			this.fraCode.PerformLayout();
			this.fraPreview.ResumeLayout(false);
			this.fraStyleBuilder.ResumeLayout(false);
			this.pnlFont.ResumeLayout(false);
			this.pnlFont.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.GroupBox fraPreview;
		private System.Windows.Forms.GroupBox fraStyleBuilder;
		private System.Windows.Forms.TreeView tvStyleBuilder;
		private System.Windows.Forms.Panel pnlFont;
		private System.Windows.Forms.ComboBox cboFontFamily;
		private System.Windows.Forms.Label lblFontFamily;
		private System.Windows.Forms.GroupBox fraCode;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Panel pnlPreview;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdFontColor;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkFontTextDecorationBlink;
		private System.Windows.Forms.CheckBox chkFontTextDecorationLineThrough;
		private System.Windows.Forms.CheckBox chkFontTextDecorationOverline;
		private System.Windows.Forms.CheckBox chkFontTextDecorationUnderline;
		private Controls.Web.StyleSheet.MeasurementUpDown.MeasurementUpDownControl txtFontSize;
	}
}
