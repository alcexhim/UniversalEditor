namespace UniversalEditor.OptionPanels.Editors.CodeEditor
{
	partial class AppearanceOptionPanel
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("XML Attribute");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("XML Attribute Value");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("XML CDATA Section");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("XML Comment");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("XML Delimiter");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("XML Documentation Comment");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("XML Documentation Tag");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("XML Keyword");
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("XML Name");
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("XML Processing Instruction");
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("XML Text");
			this.fraComponents = new System.Windows.Forms.GroupBox();
			this.cboStyleGroup = new System.Windows.Forms.ComboBox();
			this.lblStyleGroup = new System.Windows.Forms.Label();
			this.lvComponents = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraFont = new System.Windows.Forms.GroupBox();
			this.chkFontStrikethrough = new System.Windows.Forms.CheckBox();
			this.chkUnderline = new System.Windows.Forms.CheckBox();
			this.chkFontItalic = new System.Windows.Forms.CheckBox();
			this.chkFontBold = new System.Windows.Forms.CheckBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.cboFontFamily = new System.Windows.Forms.ComboBox();
			this.lblFontSize = new System.Windows.Forms.Label();
			this.lblFontFamily = new System.Windows.Forms.Label();
			this.fraColor = new System.Windows.Forms.GroupBox();
			this.cmdColorBackground = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdColorForeground = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.fraPreview = new System.Windows.Forms.GroupBox();
			this.pnlPreview = new System.Windows.Forms.Panel();
			this.fraComponents.SuspendLayout();
			this.fraFont.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.fraColor.SuspendLayout();
			this.fraPreview.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraComponents
			// 
			this.fraComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraComponents.Controls.Add(this.cboStyleGroup);
			this.fraComponents.Controls.Add(this.lblStyleGroup);
			this.fraComponents.Controls.Add(this.lvComponents);
			this.fraComponents.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraComponents.Location = new System.Drawing.Point(3, 3);
			this.fraComponents.Name = "fraComponents";
			this.fraComponents.Size = new System.Drawing.Size(518, 166);
			this.fraComponents.TabIndex = 0;
			this.fraComponents.TabStop = false;
			this.fraComponents.Text = "Components";
			// 
			// cboStyleGroup
			// 
			this.cboStyleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboStyleGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStyleGroup.FormattingEnabled = true;
			this.cboStyleGroup.Location = new System.Drawing.Point(75, 19);
			this.cboStyleGroup.Name = "cboStyleGroup";
			this.cboStyleGroup.Size = new System.Drawing.Size(437, 21);
			this.cboStyleGroup.TabIndex = 2;
			// 
			// lblStyleGroup
			// 
			this.lblStyleGroup.AutoSize = true;
			this.lblStyleGroup.Location = new System.Drawing.Point(6, 22);
			this.lblStyleGroup.Name = "lblStyleGroup";
			this.lblStyleGroup.Size = new System.Drawing.Size(63, 13);
			this.lblStyleGroup.TabIndex = 1;
			this.lblStyleGroup.Text = "Style &group:";
			// 
			// lvComponents
			// 
			this.lvComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
			this.lvComponents.FullRowSelect = true;
			this.lvComponents.GridLines = true;
			this.lvComponents.HideSelection = false;
			this.lvComponents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11});
			this.lvComponents.Location = new System.Drawing.Point(6, 46);
			this.lvComponents.Name = "lvComponents";
			this.lvComponents.Size = new System.Drawing.Size(506, 114);
			this.lvComponents.TabIndex = 0;
			this.lvComponents.UseCompatibleStateImageBehavior = false;
			this.lvComponents.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 154;
			// 
			// fraFont
			// 
			this.fraFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraFont.Controls.Add(this.chkFontStrikethrough);
			this.fraFont.Controls.Add(this.chkUnderline);
			this.fraFont.Controls.Add(this.chkFontItalic);
			this.fraFont.Controls.Add(this.chkFontBold);
			this.fraFont.Controls.Add(this.numericUpDown1);
			this.fraFont.Controls.Add(this.cboFontFamily);
			this.fraFont.Controls.Add(this.lblFontSize);
			this.fraFont.Controls.Add(this.lblFontFamily);
			this.fraFont.Location = new System.Drawing.Point(3, 175);
			this.fraFont.Name = "fraFont";
			this.fraFont.Size = new System.Drawing.Size(288, 100);
			this.fraFont.TabIndex = 1;
			this.fraFont.TabStop = false;
			this.fraFont.Text = "Font";
			// 
			// chkFontStrikethrough
			// 
			this.chkFontStrikethrough.AutoSize = true;
			this.chkFontStrikethrough.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFontStrikethrough.Location = new System.Drawing.Point(122, 48);
			this.chkFontStrikethrough.Name = "chkFontStrikethrough";
			this.chkFontStrikethrough.Size = new System.Drawing.Size(95, 18);
			this.chkFontStrikethrough.TabIndex = 3;
			this.chkFontStrikethrough.Text = "S&trikethrough";
			this.chkFontStrikethrough.UseVisualStyleBackColor = true;
			// 
			// chkUnderline
			// 
			this.chkUnderline.AutoSize = true;
			this.chkUnderline.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkUnderline.Location = new System.Drawing.Point(182, 72);
			this.chkUnderline.Name = "chkUnderline";
			this.chkUnderline.Size = new System.Drawing.Size(77, 18);
			this.chkUnderline.TabIndex = 3;
			this.chkUnderline.Text = "&Underline";
			this.chkUnderline.UseVisualStyleBackColor = true;
			// 
			// chkFontItalic
			// 
			this.chkFontItalic.AutoSize = true;
			this.chkFontItalic.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFontItalic.Location = new System.Drawing.Point(122, 72);
			this.chkFontItalic.Name = "chkFontItalic";
			this.chkFontItalic.Size = new System.Drawing.Size(54, 18);
			this.chkFontItalic.TabIndex = 3;
			this.chkFontItalic.Text = "&Italic";
			this.chkFontItalic.UseVisualStyleBackColor = true;
			// 
			// chkFontBold
			// 
			this.chkFontBold.AutoSize = true;
			this.chkFontBold.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFontBold.Location = new System.Drawing.Point(63, 72);
			this.chkFontBold.Name = "chkFontBold";
			this.chkFontBold.Size = new System.Drawing.Size(53, 18);
			this.chkFontBold.TabIndex = 3;
			this.chkFontBold.Text = "&Bold";
			this.chkFontBold.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(63, 46);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
			this.numericUpDown1.TabIndex = 2;
			// 
			// cboFontFamily
			// 
			this.cboFontFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboFontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFontFamily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboFontFamily.FormattingEnabled = true;
			this.cboFontFamily.Location = new System.Drawing.Point(63, 19);
			this.cboFontFamily.Name = "cboFontFamily";
			this.cboFontFamily.Size = new System.Drawing.Size(219, 21);
			this.cboFontFamily.TabIndex = 1;
			// 
			// lblFontSize
			// 
			this.lblFontSize.AutoSize = true;
			this.lblFontSize.Location = new System.Drawing.Point(18, 48);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(30, 13);
			this.lblFontSize.TabIndex = 0;
			this.lblFontSize.Text = "&Size:";
			// 
			// lblFontFamily
			// 
			this.lblFontFamily.AutoSize = true;
			this.lblFontFamily.Location = new System.Drawing.Point(18, 22);
			this.lblFontFamily.Name = "lblFontFamily";
			this.lblFontFamily.Size = new System.Drawing.Size(39, 13);
			this.lblFontFamily.TabIndex = 0;
			this.lblFontFamily.Text = "&Family:";
			// 
			// fraColor
			// 
			this.fraColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.fraColor.Controls.Add(this.cmdColorBackground);
			this.fraColor.Controls.Add(this.label2);
			this.fraColor.Controls.Add(this.cmdColorForeground);
			this.fraColor.Controls.Add(this.label1);
			this.fraColor.Location = new System.Drawing.Point(297, 175);
			this.fraColor.Name = "fraColor";
			this.fraColor.Size = new System.Drawing.Size(218, 100);
			this.fraColor.TabIndex = 1;
			this.fraColor.TabStop = false;
			this.fraColor.Text = "Color";
			// 
			// cmdColorBackground
			// 
			this.cmdColorBackground.BackColor = System.Drawing.SystemColors.Window;
			this.cmdColorBackground.Location = new System.Drawing.Point(91, 46);
			this.cmdColorBackground.Name = "cmdColorBackground";
			this.cmdColorBackground.Size = new System.Drawing.Size(75, 23);
			this.cmdColorBackground.TabIndex = 1;
			this.cmdColorBackground.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(17, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Background:";
			// 
			// cmdColorForeground
			// 
			this.cmdColorForeground.BackColor = System.Drawing.SystemColors.WindowText;
			this.cmdColorForeground.Location = new System.Drawing.Point(91, 17);
			this.cmdColorForeground.Name = "cmdColorForeground";
			this.cmdColorForeground.Size = new System.Drawing.Size(75, 23);
			this.cmdColorForeground.TabIndex = 1;
			this.cmdColorForeground.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(17, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Foreground:";
			// 
			// fraPreview
			// 
			this.fraPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraPreview.Controls.Add(this.pnlPreview);
			this.fraPreview.Location = new System.Drawing.Point(3, 281);
			this.fraPreview.Name = "fraPreview";
			this.fraPreview.Size = new System.Drawing.Size(512, 98);
			this.fraPreview.TabIndex = 1;
			this.fraPreview.TabStop = false;
			this.fraPreview.Text = "Preview";
			// 
			// pnlPreview
			// 
			this.pnlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPreview.BackColor = System.Drawing.SystemColors.Window;
			this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPreview.Location = new System.Drawing.Point(6, 19);
			this.pnlPreview.Name = "pnlPreview";
			this.pnlPreview.Size = new System.Drawing.Size(500, 73);
			this.pnlPreview.TabIndex = 0;
			// 
			// AppearanceOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraPreview);
			this.Controls.Add(this.fraColor);
			this.Controls.Add(this.fraFont);
			this.Controls.Add(this.fraComponents);
			this.Name = "AppearanceOptionPanel";
			this.Size = new System.Drawing.Size(524, 382);
			this.fraComponents.ResumeLayout(false);
			this.fraComponents.PerformLayout();
			this.fraFont.ResumeLayout(false);
			this.fraFont.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.fraColor.ResumeLayout(false);
			this.fraColor.PerformLayout();
			this.fraPreview.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraComponents;
		private System.Windows.Forms.ListView lvComponents;
		private System.Windows.Forms.GroupBox fraFont;
		private System.Windows.Forms.GroupBox fraColor;
		private System.Windows.Forms.GroupBox fraPreview;
		private System.Windows.Forms.ComboBox cboFontFamily;
		private System.Windows.Forms.Label lblFontFamily;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ComboBox cboStyleGroup;
		private System.Windows.Forms.Label lblStyleGroup;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label lblFontSize;
		private System.Windows.Forms.CheckBox chkFontStrikethrough;
		private System.Windows.Forms.CheckBox chkUnderline;
		private System.Windows.Forms.CheckBox chkFontItalic;
		private System.Windows.Forms.CheckBox chkFontBold;
		private System.Windows.Forms.Panel pnlPreview;
		private System.Windows.Forms.Button cmdColorForeground;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdColorBackground;
		private System.Windows.Forms.Label label2;
	}
}
