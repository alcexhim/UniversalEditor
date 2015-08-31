namespace UniversalEditor.Controls
{
	partial class ComplexObjectPropertiesControl
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
			this.txtElementID = new AwesomeControls.GuidTextBox.GuidTextBoxControl();
			this.chkIsEmpty = new System.Windows.Forms.CheckBox();
			this.txtModificationDate = new System.Windows.Forms.DateTimePicker();
			this.lblElementID = new System.Windows.Forms.Label();
			this.lblModificationDate = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtElementID
			// 
			this.txtElementID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtElementID.Location = new System.Drawing.Point(100, 3);
			this.txtElementID.MinimumSize = new System.Drawing.Size(362, 29);
			this.txtElementID.Name = "txtElementID";
			this.txtElementID.Size = new System.Drawing.Size(362, 29);
			this.txtElementID.TabIndex = 13;
			this.txtElementID.Value = new System.Guid("00000000-0000-0000-0000-000000000000");
			// 
			// chkIsEmpty
			// 
			this.chkIsEmpty.AutoSize = true;
			this.chkIsEmpty.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsEmpty.Location = new System.Drawing.Point(100, 64);
			this.chkIsEmpty.Name = "chkIsEmpty";
			this.chkIsEmpty.Size = new System.Drawing.Size(61, 18);
			this.chkIsEmpty.TabIndex = 16;
			this.chkIsEmpty.Text = "&Empty";
			this.chkIsEmpty.UseVisualStyleBackColor = true;
			// 
			// txtModificationDate
			// 
			this.txtModificationDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtModificationDate.Location = new System.Drawing.Point(100, 38);
			this.txtModificationDate.Name = "txtModificationDate";
			this.txtModificationDate.ShowCheckBox = true;
			this.txtModificationDate.Size = new System.Drawing.Size(362, 20);
			this.txtModificationDate.TabIndex = 15;
			// 
			// lblElementID
			// 
			this.lblElementID.AutoSize = true;
			this.lblElementID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblElementID.Location = new System.Drawing.Point(3, 8);
			this.lblElementID.Name = "lblElementID";
			this.lblElementID.Size = new System.Drawing.Size(62, 13);
			this.lblElementID.TabIndex = 12;
			this.lblElementID.Text = "Element ID:";
			// 
			// lblModificationDate
			// 
			this.lblModificationDate.AutoSize = true;
			this.lblModificationDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblModificationDate.Location = new System.Drawing.Point(3, 40);
			this.lblModificationDate.Name = "lblModificationDate";
			this.lblModificationDate.Size = new System.Drawing.Size(91, 13);
			this.lblModificationDate.TabIndex = 14;
			this.lblModificationDate.Text = "&Modification date:";
			// 
			// ComplexObjectPropertiesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtElementID);
			this.Controls.Add(this.chkIsEmpty);
			this.Controls.Add(this.txtModificationDate);
			this.Controls.Add(this.lblElementID);
			this.Controls.Add(this.lblModificationDate);
			this.MinimumSize = new System.Drawing.Size(465, 102);
			this.Name = "ComplexObjectPropertiesControl";
			this.Size = new System.Drawing.Size(465, 102);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblElementID;
		private System.Windows.Forms.Label lblModificationDate;
		private AwesomeControls.GuidTextBox.GuidTextBoxControl txtElementID;
		private System.Windows.Forms.CheckBox chkIsEmpty;
		private System.Windows.Forms.DateTimePicker txtModificationDate;
	}
}
