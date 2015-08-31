namespace UniversalEditor.Editors
{
    partial class MarkupEditor
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
            this.tv = new System.Windows.Forms.TreeView();
            this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.sc = new System.Windows.Forms.SplitContainer();
            this.pnlTag = new System.Windows.Forms.Panel();
            this.fraValue = new System.Windows.Forms.GroupBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.optValueSpecific = new System.Windows.Forms.RadioButton();
            this.optValueEmpty = new System.Windows.Forms.RadioButton();
            this.fraAttributes = new System.Windows.Forms.GroupBox();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.chAttributeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttributeValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblNamespace = new System.Windows.Forms.Label();
            this.lblTagName = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.sc.Panel1.SuspendLayout();
            this.sc.Panel2.SuspendLayout();
            this.sc.SuspendLayout();
            this.pnlTag.SuspendLayout();
            this.fraValue.SuspendLayout();
            this.fraAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imlSmallIcons;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(203, 354);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // imlSmallIcons
            // 
            this.imlSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // sc
            // 
            this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc.Location = new System.Drawing.Point(0, 0);
            this.sc.Name = "sc";
            // 
            // sc.Panel1
            // 
            this.sc.Panel1.Controls.Add(this.tv);
            // 
            // sc.Panel2
            // 
            this.sc.Panel2.Controls.Add(this.pnlTag);
            this.sc.Size = new System.Drawing.Size(508, 354);
            this.sc.SplitterDistance = 203;
            this.sc.TabIndex = 2;
            // 
            // pnlTag
            // 
            this.pnlTag.Controls.Add(this.fraValue);
            this.pnlTag.Controls.Add(this.fraAttributes);
            this.pnlTag.Controls.Add(this.lblNamespace);
            this.pnlTag.Controls.Add(this.lblTagName);
            this.pnlTag.Controls.Add(this.txtNamespace);
            this.pnlTag.Controls.Add(this.txtTagName);
            this.pnlTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTag.Enabled = false;
            this.pnlTag.Location = new System.Drawing.Point(0, 0);
            this.pnlTag.Name = "pnlTag";
            this.pnlTag.Size = new System.Drawing.Size(301, 354);
            this.pnlTag.TabIndex = 0;
            this.pnlTag.Visible = false;
            // 
            // fraValue
            // 
            this.fraValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraValue.Controls.Add(this.txtValue);
            this.fraValue.Controls.Add(this.optValueSpecific);
            this.fraValue.Controls.Add(this.optValueEmpty);
            this.fraValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraValue.Location = new System.Drawing.Point(3, 55);
            this.fraValue.Name = "fraValue";
            this.fraValue.Size = new System.Drawing.Size(295, 124);
            this.fraValue.TabIndex = 3;
            this.fraValue.TabStop = false;
            this.fraValue.Text = "Value";
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(20, 71);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValue.Size = new System.Drawing.Size(269, 47);
            this.txtValue.TabIndex = 1;
            this.txtValue.Validated += new System.EventHandler(this.txtValue_Validated);
            // 
            // optValueSpecific
            // 
            this.optValueSpecific.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optValueSpecific.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optValueSpecific.Location = new System.Drawing.Point(20, 45);
            this.optValueSpecific.Name = "optValueSpecific";
            this.optValueSpecific.Size = new System.Drawing.Size(269, 20);
            this.optValueSpecific.TabIndex = 0;
            this.optValueSpecific.Text = "Specific &value (no children allowed)";
            this.optValueSpecific.UseVisualStyleBackColor = true;
            this.optValueSpecific.CheckedChanged += new System.EventHandler(this.optValue_CheckedChanged);
            // 
            // optValueEmpty
            // 
            this.optValueEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optValueEmpty.Checked = true;
            this.optValueEmpty.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optValueEmpty.Location = new System.Drawing.Point(20, 19);
            this.optValueEmpty.Name = "optValueEmpty";
            this.optValueEmpty.Size = new System.Drawing.Size(269, 20);
            this.optValueEmpty.TabIndex = 0;
            this.optValueEmpty.TabStop = true;
            this.optValueEmpty.Text = "&Empty/child elements";
            this.optValueEmpty.UseVisualStyleBackColor = true;
            this.optValueEmpty.CheckedChanged += new System.EventHandler(this.optValue_CheckedChanged);
            // 
            // fraAttributes
            // 
            this.fraAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraAttributes.Controls.Add(this.lvAttributes);
            this.fraAttributes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraAttributes.Location = new System.Drawing.Point(3, 185);
            this.fraAttributes.Name = "fraAttributes";
            this.fraAttributes.Size = new System.Drawing.Size(295, 166);
            this.fraAttributes.TabIndex = 2;
            this.fraAttributes.TabStop = false;
            this.fraAttributes.Text = "Attributes";
            // 
            // lvAttributes
            // 
            this.lvAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAttributeName,
            this.chAttributeValue});
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.GridLines = true;
            this.lvAttributes.HideSelection = false;
            this.lvAttributes.Location = new System.Drawing.Point(6, 19);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(283, 141);
            this.lvAttributes.SmallImageList = this.imlSmallIcons;
            this.lvAttributes.TabIndex = 0;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            // 
            // chAttributeName
            // 
            this.chAttributeName.Text = "Name";
            this.chAttributeName.Width = 113;
            // 
            // chAttributeValue
            // 
            this.chAttributeValue.Text = "Value";
            this.chAttributeValue.Width = 157;
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblNamespace.Location = new System.Drawing.Point(3, 32);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 13);
            this.lblNamespace.TabIndex = 1;
            this.lblNamespace.Text = "Name&space:";
            // 
            // lblTagName
            // 
            this.lblTagName.AutoSize = true;
            this.lblTagName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTagName.Location = new System.Drawing.Point(3, 6);
            this.lblTagName.Name = "lblTagName";
            this.lblTagName.Size = new System.Drawing.Size(58, 13);
            this.lblTagName.TabIndex = 1;
            this.lblTagName.Text = "Tag &name:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespace.Location = new System.Drawing.Point(76, 29);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(222, 20);
            this.txtNamespace.TabIndex = 0;
            // 
            // txtTagName
            // 
            this.txtTagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTagName.Location = new System.Drawing.Point(76, 3);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(222, 20);
            this.txtTagName.TabIndex = 0;
            // 
            // MarkupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sc);
            this.Name = "MarkupEditor";
            this.Size = new System.Drawing.Size(508, 354);
            this.sc.Panel1.ResumeLayout(false);
            this.sc.Panel2.ResumeLayout(false);
            this.sc.ResumeLayout(false);
            this.pnlTag.ResumeLayout(false);
            this.pnlTag.PerformLayout();
            this.fraValue.ResumeLayout(false);
            this.fraValue.PerformLayout();
            this.fraAttributes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.SplitContainer sc;
        private System.Windows.Forms.Panel pnlTag;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.GroupBox fraAttributes;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.ColumnHeader chAttributeName;
        private System.Windows.Forms.ColumnHeader chAttributeValue;
        private System.Windows.Forms.GroupBox fraValue;
        private System.Windows.Forms.RadioButton optValueEmpty;
        private System.Windows.Forms.RadioButton optValueSpecific;
        private System.Windows.Forms.TextBox txtValue;

    }
}
