namespace UniversalEditor.Dialogs.Contact
{
	partial class NamePropertiesDialogImpl
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
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.lblGivenName = new System.Windows.Forms.Label();
			this.txtGivenName = new System.Windows.Forms.TextBox();
			this.lblMiddleName = new System.Windows.Forms.Label();
			this.txtMiddleName = new System.Windows.Forms.TextBox();
			this.lblFamilyName = new System.Windows.Forms.Label();
			this.txtFamilyName = new System.Windows.Forms.TextBox();
			this.cboDisplayName = new System.Windows.Forms.ComboBox();
			this.lblDisplayName = new System.Windows.Forms.Label();
			this.lblPersonalTitle = new System.Windows.Forms.Label();
			this.txtPersonalTitle = new System.Windows.Forms.TextBox();
			this.lblNickname = new System.Windows.Forms.Label();
			this.txtNickname = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(308, 169);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 13;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(227, 169);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 12;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// lblGivenName
			// 
			this.lblGivenName.AutoSize = true;
			this.lblGivenName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblGivenName.Location = new System.Drawing.Point(12, 15);
			this.lblGivenName.Name = "lblGivenName";
			this.lblGivenName.Size = new System.Drawing.Size(67, 13);
			this.lblGivenName.TabIndex = 0;
			this.lblGivenName.Text = "&Given name:";
			// 
			// txtGivenName
			// 
			this.txtGivenName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGivenName.Location = new System.Drawing.Point(91, 12);
			this.txtGivenName.Name = "txtGivenName";
			this.txtGivenName.Size = new System.Drawing.Size(292, 20);
			this.txtGivenName.TabIndex = 1;
			// 
			// lblMiddleName
			// 
			this.lblMiddleName.AutoSize = true;
			this.lblMiddleName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblMiddleName.Location = new System.Drawing.Point(12, 41);
			this.lblMiddleName.Name = "lblMiddleName";
			this.lblMiddleName.Size = new System.Drawing.Size(70, 13);
			this.lblMiddleName.TabIndex = 2;
			this.lblMiddleName.Text = "&Middle name:";
			// 
			// txtMiddleName
			// 
			this.txtMiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMiddleName.Location = new System.Drawing.Point(91, 38);
			this.txtMiddleName.Name = "txtMiddleName";
			this.txtMiddleName.Size = new System.Drawing.Size(292, 20);
			this.txtMiddleName.TabIndex = 3;
			// 
			// lblFamilyName
			// 
			this.lblFamilyName.AutoSize = true;
			this.lblFamilyName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFamilyName.Location = new System.Drawing.Point(12, 67);
			this.lblFamilyName.Name = "lblFamilyName";
			this.lblFamilyName.Size = new System.Drawing.Size(68, 13);
			this.lblFamilyName.TabIndex = 4;
			this.lblFamilyName.Text = "&Family name:";
			// 
			// txtFamilyName
			// 
			this.txtFamilyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFamilyName.Location = new System.Drawing.Point(91, 64);
			this.txtFamilyName.Name = "txtFamilyName";
			this.txtFamilyName.Size = new System.Drawing.Size(292, 20);
			this.txtFamilyName.TabIndex = 5;
			// 
			// cboDisplayName
			// 
			this.cboDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDisplayName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboDisplayName.FormattingEnabled = true;
			this.cboDisplayName.Location = new System.Drawing.Point(91, 90);
			this.cboDisplayName.Name = "cboDisplayName";
			this.cboDisplayName.Size = new System.Drawing.Size(292, 21);
			this.cboDisplayName.TabIndex = 7;
			// 
			// lblDisplayName
			// 
			this.lblDisplayName.AutoSize = true;
			this.lblDisplayName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDisplayName.Location = new System.Drawing.Point(12, 93);
			this.lblDisplayName.Name = "lblDisplayName";
			this.lblDisplayName.Size = new System.Drawing.Size(73, 13);
			this.lblDisplayName.TabIndex = 6;
			this.lblDisplayName.Text = "&Display name:";
			// 
			// lblPersonalTitle
			// 
			this.lblPersonalTitle.AutoSize = true;
			this.lblPersonalTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPersonalTitle.Location = new System.Drawing.Point(12, 120);
			this.lblPersonalTitle.Name = "lblPersonalTitle";
			this.lblPersonalTitle.Size = new System.Drawing.Size(70, 13);
			this.lblPersonalTitle.TabIndex = 8;
			this.lblPersonalTitle.Text = "&Personal title:";
			// 
			// txtPersonalTitle
			// 
			this.txtPersonalTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPersonalTitle.Location = new System.Drawing.Point(91, 117);
			this.txtPersonalTitle.Name = "txtPersonalTitle";
			this.txtPersonalTitle.Size = new System.Drawing.Size(292, 20);
			this.txtPersonalTitle.TabIndex = 9;
			// 
			// lblNickname
			// 
			this.lblNickname.AutoSize = true;
			this.lblNickname.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNickname.Location = new System.Drawing.Point(12, 146);
			this.lblNickname.Name = "lblNickname";
			this.lblNickname.Size = new System.Drawing.Size(58, 13);
			this.lblNickname.TabIndex = 10;
			this.lblNickname.Text = "&Nickname:";
			// 
			// txtNickname
			// 
			this.txtNickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNickname.Location = new System.Drawing.Point(91, 143);
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.Size = new System.Drawing.Size(292, 20);
			this.txtNickname.TabIndex = 11;
			// 
			// NamePropertiesDialogImpl
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(395, 204);
			this.Controls.Add(this.cboDisplayName);
			this.Controls.Add(this.txtNickname);
			this.Controls.Add(this.txtPersonalTitle);
			this.Controls.Add(this.lblNickname);
			this.Controls.Add(this.txtFamilyName);
			this.Controls.Add(this.lblPersonalTitle);
			this.Controls.Add(this.lblDisplayName);
			this.Controls.Add(this.lblFamilyName);
			this.Controls.Add(this.txtMiddleName);
			this.Controls.Add(this.lblMiddleName);
			this.Controls.Add(this.txtGivenName);
			this.Controls.Add(this.lblGivenName);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(411, 242);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(411, 242);
			this.Name = "NamePropertiesDialogImpl";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Name Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label lblGivenName;
		private System.Windows.Forms.Label lblMiddleName;
		private System.Windows.Forms.Label lblFamilyName;
		private System.Windows.Forms.Label lblDisplayName;
		private System.Windows.Forms.Label lblPersonalTitle;
		private System.Windows.Forms.Label lblNickname;
		internal System.Windows.Forms.TextBox txtGivenName;
		internal System.Windows.Forms.TextBox txtMiddleName;
		internal System.Windows.Forms.TextBox txtFamilyName;
		internal System.Windows.Forms.ComboBox cboDisplayName;
		internal System.Windows.Forms.TextBox txtPersonalTitle;
		internal System.Windows.Forms.TextBox txtNickname;
	}
}