namespace UniversalEditor.Dialogs.Setup.Microsoft.ACME.BootstrapScript
{
	partial class BootstrapFilePropertiesDialogImpl
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
			this.cmdBrowseSourceFileName = new System.Windows.Forms.Button();
			this.txtSourceFileName = new System.Windows.Forms.TextBox();
			this.cmdBrowseDestinationFileName = new System.Windows.Forms.Button();
			this.txtDestinationFileName = new System.Windows.Forms.TextBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdBrowseSourceFileName
			// 
			this.cmdBrowseSourceFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowseSourceFileName.Location = new System.Drawing.Point(12, 12);
			this.cmdBrowseSourceFileName.Name = "cmdBrowseSourceFileName";
			this.cmdBrowseSourceFileName.Size = new System.Drawing.Size(125, 23);
			this.cmdBrowseSourceFileName.TabIndex = 0;
			this.cmdBrowseSourceFileName.Text = "&Source file name:";
			this.cmdBrowseSourceFileName.UseVisualStyleBackColor = true;
			this.cmdBrowseSourceFileName.Click += new System.EventHandler(this.cmdBrowseSourceFileName_Click);
			// 
			// txtSourceFileName
			// 
			this.txtSourceFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSourceFileName.HideSelection = false;
			this.txtSourceFileName.Location = new System.Drawing.Point(143, 14);
			this.txtSourceFileName.Name = "txtSourceFileName";
			this.txtSourceFileName.Size = new System.Drawing.Size(241, 20);
			this.txtSourceFileName.TabIndex = 1;
			// 
			// cmdBrowseDestinationFileName
			// 
			this.cmdBrowseDestinationFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowseDestinationFileName.Location = new System.Drawing.Point(12, 41);
			this.cmdBrowseDestinationFileName.Name = "cmdBrowseDestinationFileName";
			this.cmdBrowseDestinationFileName.Size = new System.Drawing.Size(125, 23);
			this.cmdBrowseDestinationFileName.TabIndex = 2;
			this.cmdBrowseDestinationFileName.Text = "&Destination file name:";
			this.cmdBrowseDestinationFileName.UseVisualStyleBackColor = true;
			this.cmdBrowseDestinationFileName.Click += new System.EventHandler(this.cmdBrowseDestinationFileName_Click);
			// 
			// txtDestinationFileName
			// 
			this.txtDestinationFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDestinationFileName.HideSelection = false;
			this.txtDestinationFileName.Location = new System.Drawing.Point(143, 43);
			this.txtDestinationFileName.Name = "txtDestinationFileName";
			this.txtDestinationFileName.Size = new System.Drawing.Size(241, 20);
			this.txtDestinationFileName.TabIndex = 3;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(309, 69);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(228, 69);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// BootstrapFilePropertiesDialogImpl
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(396, 104);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.txtDestinationFileName);
			this.Controls.Add(this.txtSourceFileName);
			this.Controls.Add(this.cmdBrowseDestinationFileName);
			this.Controls.Add(this.cmdBrowseSourceFileName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(412, 142);
			this.Name = "BootstrapFilePropertiesDialogImpl";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Bootstrap File Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdBrowseSourceFileName;
		private System.Windows.Forms.Button cmdBrowseDestinationFileName;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.TextBox txtSourceFileName;
		internal System.Windows.Forms.TextBox txtDestinationFileName;
	}
}