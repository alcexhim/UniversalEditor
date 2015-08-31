namespace UniversalEditor.UserInterface.WindowsForms.Dialogs.PropertyList
{
	partial class PropertyDetailsDialog
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
			this.lblPropertyName = new System.Windows.Forms.Label();
			this.lblPropertyValue = new System.Windows.Forms.Label();
			this.txtPropertyName = new System.Windows.Forms.TextBox();
			this.txtPropertyValue = new System.Windows.Forms.TextBox();
			this.lblPropertyType = new System.Windows.Forms.Label();
			this.cboPropertyType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(274, 140);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(193, 140);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// lblPropertyName
			// 
			this.lblPropertyName.AutoSize = true;
			this.lblPropertyName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPropertyName.Location = new System.Drawing.Point(12, 15);
			this.lblPropertyName.Name = "lblPropertyName";
			this.lblPropertyName.Size = new System.Drawing.Size(78, 13);
			this.lblPropertyName.TabIndex = 0;
			this.lblPropertyName.Text = "Property &name:";
			// 
			// lblPropertyValue
			// 
			this.lblPropertyValue.AutoSize = true;
			this.lblPropertyValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPropertyValue.Location = new System.Drawing.Point(12, 68);
			this.lblPropertyValue.Name = "lblPropertyValue";
			this.lblPropertyValue.Size = new System.Drawing.Size(78, 13);
			this.lblPropertyValue.TabIndex = 4;
			this.lblPropertyValue.Text = "Property &value:";
			// 
			// txtPropertyName
			// 
			this.txtPropertyName.Location = new System.Drawing.Point(96, 12);
			this.txtPropertyName.Name = "txtPropertyName";
			this.txtPropertyName.Size = new System.Drawing.Size(253, 20);
			this.txtPropertyName.TabIndex = 1;
			// 
			// txtPropertyValue
			// 
			this.txtPropertyValue.Location = new System.Drawing.Point(96, 65);
			this.txtPropertyValue.Name = "txtPropertyValue";
			this.txtPropertyValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPropertyValue.Size = new System.Drawing.Size(253, 20);
			this.txtPropertyValue.TabIndex = 5;
			// 
			// lblPropertyType
			// 
			this.lblPropertyType.AutoSize = true;
			this.lblPropertyType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPropertyType.Location = new System.Drawing.Point(12, 41);
			this.lblPropertyType.Name = "lblPropertyType";
			this.lblPropertyType.Size = new System.Drawing.Size(72, 13);
			this.lblPropertyType.TabIndex = 2;
			this.lblPropertyType.Text = "Property &type:";
			// 
			// cboPropertyType
			// 
			this.cboPropertyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboPropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPropertyType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboPropertyType.FormattingEnabled = true;
			this.cboPropertyType.Items.AddRange(new object[] {
			"(auto-detect)",
			"String",
			"Binary",
			"DWORD",
			"Expanded String",
			"Link",
			"String List",
			"None",
			"QWORD",
			"Unknown"});
			this.cboPropertyType.Location = new System.Drawing.Point(96, 38);
			this.cboPropertyType.Name = "cboPropertyType";
			this.cboPropertyType.Size = new System.Drawing.Size(253, 21);
			this.cboPropertyType.TabIndex = 3;
			this.cboPropertyType.SelectedIndexChanged += new System.EventHandler(this.cboPropertyType_SelectedIndexChanged);
			// 
			// PropertyDetailsDialog
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(361, 175);
			this.Controls.Add(this.cboPropertyType);
			this.Controls.Add(this.txtPropertyValue);
			this.Controls.Add(this.txtPropertyName);
			this.Controls.Add(this.lblPropertyType);
			this.Controls.Add(this.lblPropertyValue);
			this.Controls.Add(this.lblPropertyName);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertyDetailsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Property Details";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label lblPropertyName;
		private System.Windows.Forms.Label lblPropertyValue;
		internal System.Windows.Forms.TextBox txtPropertyName;
		internal System.Windows.Forms.TextBox txtPropertyValue;
		private System.Windows.Forms.Label lblPropertyType;
		internal System.Windows.Forms.ComboBox cboPropertyType;
	}
}