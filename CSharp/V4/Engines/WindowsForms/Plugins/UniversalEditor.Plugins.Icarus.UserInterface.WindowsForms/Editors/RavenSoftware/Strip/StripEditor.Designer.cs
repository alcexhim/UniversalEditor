namespace UniversalEditor.Editors.RavenSoftware.Strip
{
    partial class StripEditor
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
			this.lv = new System.Windows.Forms.ListView();
			this.chGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chLanguage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraList = new System.Windows.Forms.GroupBox();
			this.fraEditor = new System.Windows.Forms.GroupBox();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdUpdate = new System.Windows.Forms.Button();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.cboLanguage = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboGroup = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fraList.SuspendLayout();
			this.fraEditor.SuspendLayout();
			this.SuspendLayout();
			// 
			// lv
			// 
			this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGroup,
            this.chLanguage,
            this.chValue});
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(6, 19);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(468, 160);
			this.lv.TabIndex = 0;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
			// 
			// chGroup
			// 
			this.chGroup.Text = "Group";
			this.chGroup.Width = 122;
			// 
			// chLanguage
			// 
			this.chLanguage.Text = "Language";
			this.chLanguage.Width = 129;
			// 
			// chValue
			// 
			this.chValue.Text = "Value";
			this.chValue.Width = 208;
			// 
			// fraList
			// 
			this.fraList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraList.Controls.Add(this.lv);
			this.fraList.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraList.Location = new System.Drawing.Point(3, 3);
			this.fraList.Name = "fraList";
			this.fraList.Size = new System.Drawing.Size(480, 185);
			this.fraList.TabIndex = 1;
			this.fraList.TabStop = false;
			this.fraList.Text = "Entry list";
			// 
			// fraEditor
			// 
			this.fraEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraEditor.Controls.Add(this.cmdRemove);
			this.fraEditor.Controls.Add(this.cmdUpdate);
			this.fraEditor.Controls.Add(this.txtValue);
			this.fraEditor.Controls.Add(this.cboLanguage);
			this.fraEditor.Controls.Add(this.label3);
			this.fraEditor.Controls.Add(this.label2);
			this.fraEditor.Controls.Add(this.cboGroup);
			this.fraEditor.Controls.Add(this.label1);
			this.fraEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraEditor.Location = new System.Drawing.Point(3, 194);
			this.fraEditor.Name = "fraEditor";
			this.fraEditor.Size = new System.Drawing.Size(480, 146);
			this.fraEditor.TabIndex = 2;
			this.fraEditor.TabStop = false;
			this.fraEditor.Text = "Entry editor";
			// 
			// cmdRemove
			// 
			this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdRemove.Location = new System.Drawing.Point(399, 77);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdRemove.TabIndex = 3;
			this.cmdRemove.Text = "&Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdUpdate.Location = new System.Drawing.Point(399, 48);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
			this.cmdUpdate.TabIndex = 3;
			this.cmdUpdate.Text = "Upd&ate";
			this.cmdUpdate.UseVisualStyleBackColor = true;
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.Location = new System.Drawing.Point(86, 48);
			this.txtValue.Multiline = true;
			this.txtValue.Name = "txtValue";
			this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtValue.Size = new System.Drawing.Size(307, 92);
			this.txtValue.TabIndex = 2;
			// 
			// cboLanguage
			// 
			this.cboLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboLanguage.FormattingEnabled = true;
			this.cboLanguage.Location = new System.Drawing.Point(273, 19);
			this.cboLanguage.Name = "cboLanguage";
			this.cboLanguage.Size = new System.Drawing.Size(201, 21);
			this.cboLanguage.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(22, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "&Value:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(209, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "&Language:";
			// 
			// cboGroup
			// 
			this.cboGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboGroup.FormattingEnabled = true;
			this.cboGroup.Location = new System.Drawing.Point(86, 19);
			this.cboGroup.Name = "cboGroup";
			this.cboGroup.Size = new System.Drawing.Size(117, 21);
			this.cboGroup.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(22, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Group:";
			// 
			// StripEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraEditor);
			this.Controls.Add(this.fraList);
			this.Name = "StripEditor";
			this.Size = new System.Drawing.Size(486, 343);
			this.fraList.ResumeLayout(false);
			this.fraEditor.ResumeLayout(false);
			this.fraEditor.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ColumnHeader chGroup;
		private System.Windows.Forms.ColumnHeader chLanguage;
		private System.Windows.Forms.ColumnHeader chValue;
		private System.Windows.Forms.GroupBox fraList;
		private System.Windows.Forms.GroupBox fraEditor;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdUpdate;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.ComboBox cboLanguage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboGroup;
		private System.Windows.Forms.Label label1;
    }
}
