namespace UniversalEditor.Dialogs.Contact
{
	partial class LabelPropertiesDialogImpl
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
			this.lblLabel = new System.Windows.Forms.Label();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this.lblElementID = new System.Windows.Forms.Label();
			this.txtModificationDate = new System.Windows.Forms.DateTimePicker();
			this.lblModificationDate = new System.Windows.Forms.Label();
			this.chkIsEmpty = new System.Windows.Forms.CheckBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.txtElementID = new AwesomeControls.GuidTextBox.GuidTextBoxControl();
			this.SuspendLayout();
			// 
			// lblLabel
			// 
			this.lblLabel.AutoSize = true;
			this.lblLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLabel.Location = new System.Drawing.Point(12, 50);
			this.lblLabel.Name = "lblLabel";
			this.lblLabel.Size = new System.Drawing.Size(36, 13);
			this.lblLabel.TabIndex = 2;
			this.lblLabel.Text = "&Label:";
			// 
			// txtLabel
			// 
			this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabel.Location = new System.Drawing.Point(109, 47);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(362, 20);
			this.txtLabel.TabIndex = 3;
			// 
			// lblElementID
			// 
			this.lblElementID.AutoSize = true;
			this.lblElementID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblElementID.Location = new System.Drawing.Point(12, 17);
			this.lblElementID.Name = "lblElementID";
			this.lblElementID.Size = new System.Drawing.Size(62, 13);
			this.lblElementID.TabIndex = 0;
			this.lblElementID.Text = "Element ID:";
			// 
			// txtModificationDate
			// 
			this.txtModificationDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtModificationDate.Location = new System.Drawing.Point(109, 73);
			this.txtModificationDate.Name = "txtModificationDate";
			this.txtModificationDate.ShowCheckBox = true;
			this.txtModificationDate.Size = new System.Drawing.Size(362, 20);
			this.txtModificationDate.TabIndex = 5;
			// 
			// lblModificationDate
			// 
			this.lblModificationDate.AutoSize = true;
			this.lblModificationDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblModificationDate.Location = new System.Drawing.Point(12, 75);
			this.lblModificationDate.Name = "lblModificationDate";
			this.lblModificationDate.Size = new System.Drawing.Size(91, 13);
			this.lblModificationDate.TabIndex = 4;
			this.lblModificationDate.Text = "&Modification date:";
			// 
			// chkIsEmpty
			// 
			this.chkIsEmpty.AutoSize = true;
			this.chkIsEmpty.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsEmpty.Location = new System.Drawing.Point(109, 99);
			this.chkIsEmpty.Name = "chkIsEmpty";
			this.chkIsEmpty.Size = new System.Drawing.Size(61, 18);
			this.chkIsEmpty.TabIndex = 6;
			this.chkIsEmpty.Text = "&Empty";
			this.chkIsEmpty.UseVisualStyleBackColor = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(396, 133);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 8;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(315, 133);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// txtElementID
			// 
			this.txtElementID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtElementID.Location = new System.Drawing.Point(109, 12);
			this.txtElementID.MinimumSize = new System.Drawing.Size(362, 29);
			this.txtElementID.Name = "txtElementID";
			this.txtElementID.Size = new System.Drawing.Size(362, 29);
			this.txtElementID.TabIndex = 1;
			this.txtElementID.Value = new System.Guid("00000000-0000-0000-0000-000000000000");
			// 
			// LabelPropertiesDialogImpl
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(483, 168);
			this.Controls.Add(this.txtElementID);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.chkIsEmpty);
			this.Controls.Add(this.txtModificationDate);
			this.Controls.Add(this.lblElementID);
			this.Controls.Add(this.txtLabel);
			this.Controls.Add(this.lblModificationDate);
			this.Controls.Add(this.lblLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(499, 206);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(499, 206);
			this.Name = "LabelPropertiesDialogImpl";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Label Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLabel;
		private System.Windows.Forms.Label lblElementID;
		private System.Windows.Forms.Label lblModificationDate;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.TextBox txtLabel;
		internal System.Windows.Forms.DateTimePicker txtModificationDate;
		internal System.Windows.Forms.CheckBox chkIsEmpty;
		internal AwesomeControls.GuidTextBox.GuidTextBoxControl txtElementID;
	}
}