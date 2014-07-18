namespace UniversalEditor.Dialogs.UnrealEngine
{
	partial class ExportTableEntryPropertiesDialog
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
			this.lblObjectName = new System.Windows.Forms.Label();
			this.txtObjectName = new System.Windows.Forms.TextBox();
			this.lblParentObjectName = new System.Windows.Forms.Label();
			this.cboParentObjectName = new System.Windows.Forms.ComboBox();
			this.lblClassName = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.fraFlags = new System.Windows.Forms.GroupBox();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.lblSourceData = new System.Windows.Forms.Label();
			this.txtSourceData = new System.Windows.Forms.TextBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.chkTransactional = new System.Windows.Forms.CheckBox();
			this.chkUnreachable = new System.Windows.Forms.CheckBox();
			this.chkPublic = new System.Windows.Forms.CheckBox();
			this.chkImporting = new System.Windows.Forms.CheckBox();
			this.chkExporting = new System.Windows.Forms.CheckBox();
			this.chkSourceModified = new System.Windows.Forms.CheckBox();
			this.chkGarbageCollect = new System.Windows.Forms.CheckBox();
			this.chkRequireLoad = new System.Windows.Forms.CheckBox();
			this.chkHighlightName = new System.Windows.Forms.CheckBox();
			this.chkRemappedName = new System.Windows.Forms.CheckBox();
			this.chkSuppressed = new System.Windows.Forms.CheckBox();
			this.fraFlags.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblObjectName
			// 
			this.lblObjectName.AutoSize = true;
			this.lblObjectName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblObjectName.Location = new System.Drawing.Point(12, 15);
			this.lblObjectName.Name = "lblObjectName";
			this.lblObjectName.Size = new System.Drawing.Size(70, 13);
			this.lblObjectName.TabIndex = 0;
			this.lblObjectName.Text = "Object &name:";
			// 
			// txtObjectName
			// 
			this.txtObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtObjectName.Location = new System.Drawing.Point(120, 12);
			this.txtObjectName.Name = "txtObjectName";
			this.txtObjectName.Size = new System.Drawing.Size(260, 20);
			this.txtObjectName.TabIndex = 1;
			// 
			// lblParentObjectName
			// 
			this.lblParentObjectName.AutoSize = true;
			this.lblParentObjectName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblParentObjectName.Location = new System.Drawing.Point(12, 41);
			this.lblParentObjectName.Name = "lblParentObjectName";
			this.lblParentObjectName.Size = new System.Drawing.Size(102, 13);
			this.lblParentObjectName.TabIndex = 2;
			this.lblParentObjectName.Text = "&Parent object name:";
			// 
			// cboParentObjectName
			// 
			this.cboParentObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboParentObjectName.FormattingEnabled = true;
			this.cboParentObjectName.Location = new System.Drawing.Point(120, 38);
			this.cboParentObjectName.Name = "cboParentObjectName";
			this.cboParentObjectName.Size = new System.Drawing.Size(260, 21);
			this.cboParentObjectName.TabIndex = 3;
			// 
			// lblClassName
			// 
			this.lblClassName.AutoSize = true;
			this.lblClassName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblClassName.Location = new System.Drawing.Point(12, 68);
			this.lblClassName.Name = "lblClassName";
			this.lblClassName.Size = new System.Drawing.Size(64, 13);
			this.lblClassName.TabIndex = 4;
			this.lblClassName.Text = "&Class name:";
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(120, 65);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(260, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// fraFlags
			// 
			this.fraFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraFlags.Controls.Add(this.chkSuppressed);
			this.fraFlags.Controls.Add(this.chkRemappedName);
			this.fraFlags.Controls.Add(this.chkHighlightName);
			this.fraFlags.Controls.Add(this.chkRequireLoad);
			this.fraFlags.Controls.Add(this.chkExporting);
			this.fraFlags.Controls.Add(this.chkImporting);
			this.fraFlags.Controls.Add(this.chkGarbageCollect);
			this.fraFlags.Controls.Add(this.chkSourceModified);
			this.fraFlags.Controls.Add(this.chkPublic);
			this.fraFlags.Controls.Add(this.chkUnreachable);
			this.fraFlags.Controls.Add(this.chkTransactional);
			this.fraFlags.Location = new System.Drawing.Point(12, 121);
			this.fraFlags.Name = "fraFlags";
			this.fraFlags.Size = new System.Drawing.Size(368, 188);
			this.fraFlags.TabIndex = 9;
			this.fraFlags.TabStop = false;
			this.fraFlags.Text = "Flags";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowse.Location = new System.Drawing.Point(305, 92);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
			this.cmdBrowse.TabIndex = 8;
			this.cmdBrowse.Text = "&Browse...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			// 
			// lblSourceData
			// 
			this.lblSourceData.AutoSize = true;
			this.lblSourceData.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblSourceData.Location = new System.Drawing.Point(12, 97);
			this.lblSourceData.Name = "lblSourceData";
			this.lblSourceData.Size = new System.Drawing.Size(68, 13);
			this.lblSourceData.TabIndex = 6;
			this.lblSourceData.Text = "Source data:";
			// 
			// txtSourceData
			// 
			this.txtSourceData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSourceData.Location = new System.Drawing.Point(120, 94);
			this.txtSourceData.Name = "txtSourceData";
			this.txtSourceData.ReadOnly = true;
			this.txtSourceData.Size = new System.Drawing.Size(179, 20);
			this.txtSourceData.TabIndex = 7;
			this.txtSourceData.Text = "(0 bytes)";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(305, 315);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 11;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(224, 315);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 10;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// chkTransactional
			// 
			this.chkTransactional.AutoSize = true;
			this.chkTransactional.Location = new System.Drawing.Point(22, 19);
			this.chkTransactional.Name = "chkTransactional";
			this.chkTransactional.Size = new System.Drawing.Size(90, 17);
			this.chkTransactional.TabIndex = 0;
			this.chkTransactional.Text = "&Transactional";
			this.chkTransactional.UseVisualStyleBackColor = true;
			// 
			// chkUnreachable
			// 
			this.chkUnreachable.AutoSize = true;
			this.chkUnreachable.Location = new System.Drawing.Point(22, 42);
			this.chkUnreachable.Name = "chkUnreachable";
			this.chkUnreachable.Size = new System.Drawing.Size(87, 17);
			this.chkUnreachable.TabIndex = 0;
			this.chkUnreachable.Text = "Un&reachable";
			this.chkUnreachable.UseVisualStyleBackColor = true;
			// 
			// chkPublic
			// 
			this.chkPublic.AutoSize = true;
			this.chkPublic.Location = new System.Drawing.Point(22, 65);
			this.chkPublic.Name = "chkPublic";
			this.chkPublic.Size = new System.Drawing.Size(55, 17);
			this.chkPublic.TabIndex = 0;
			this.chkPublic.Text = "P&ublic";
			this.chkPublic.UseVisualStyleBackColor = true;
			// 
			// chkImporting
			// 
			this.chkImporting.AutoSize = true;
			this.chkImporting.Location = new System.Drawing.Point(142, 19);
			this.chkImporting.Name = "chkImporting";
			this.chkImporting.Size = new System.Drawing.Size(69, 17);
			this.chkImporting.TabIndex = 0;
			this.chkImporting.Text = "&Importing";
			this.chkImporting.UseVisualStyleBackColor = true;
			// 
			// chkExporting
			// 
			this.chkExporting.AutoSize = true;
			this.chkExporting.Location = new System.Drawing.Point(142, 42);
			this.chkExporting.Name = "chkExporting";
			this.chkExporting.Size = new System.Drawing.Size(70, 17);
			this.chkExporting.TabIndex = 0;
			this.chkExporting.Text = "&Exporting";
			this.chkExporting.UseVisualStyleBackColor = true;
			// 
			// chkSourceModified
			// 
			this.chkSourceModified.AutoSize = true;
			this.chkSourceModified.Location = new System.Drawing.Point(22, 88);
			this.chkSourceModified.Name = "chkSourceModified";
			this.chkSourceModified.Size = new System.Drawing.Size(102, 17);
			this.chkSourceModified.TabIndex = 0;
			this.chkSourceModified.Text = "Source &modified";
			this.chkSourceModified.UseVisualStyleBackColor = true;
			// 
			// chkGarbageCollect
			// 
			this.chkGarbageCollect.AutoSize = true;
			this.chkGarbageCollect.Location = new System.Drawing.Point(22, 111);
			this.chkGarbageCollect.Name = "chkGarbageCollect";
			this.chkGarbageCollect.Size = new System.Drawing.Size(101, 17);
			this.chkGarbageCollect.TabIndex = 0;
			this.chkGarbageCollect.Text = "Gar&bage collect";
			this.chkGarbageCollect.UseVisualStyleBackColor = true;
			// 
			// chkRequireLoad
			// 
			this.chkRequireLoad.AutoSize = true;
			this.chkRequireLoad.Location = new System.Drawing.Point(142, 65);
			this.chkRequireLoad.Name = "chkRequireLoad";
			this.chkRequireLoad.Size = new System.Drawing.Size(86, 17);
			this.chkRequireLoad.TabIndex = 0;
			this.chkRequireLoad.Text = "Re&quire load";
			this.chkRequireLoad.UseVisualStyleBackColor = true;
			// 
			// chkHighlightName
			// 
			this.chkHighlightName.AutoSize = true;
			this.chkHighlightName.Location = new System.Drawing.Point(142, 88);
			this.chkHighlightName.Name = "chkHighlightName";
			this.chkHighlightName.Size = new System.Drawing.Size(174, 17);
			this.chkHighlightName.TabIndex = 0;
			this.chkHighlightName.Text = "&Highlight name/eliminate object";
			this.chkHighlightName.UseVisualStyleBackColor = true;
			// 
			// chkRemappedName
			// 
			this.chkRemappedName.AutoSize = true;
			this.chkRemappedName.Location = new System.Drawing.Point(142, 111);
			this.chkRemappedName.Name = "chkRemappedName";
			this.chkRemappedName.Size = new System.Drawing.Size(189, 17);
			this.chkRemappedName.TabIndex = 0;
			this.chkRemappedName.Text = "Remappe&d name/singular function";
			this.chkRemappedName.UseVisualStyleBackColor = true;
			// 
			// chkSuppressed
			// 
			this.chkSuppressed.AutoSize = true;
			this.chkSuppressed.Location = new System.Drawing.Point(142, 134);
			this.chkSuppressed.Name = "chkSuppressed";
			this.chkSuppressed.Size = new System.Drawing.Size(155, 17);
			this.chkSuppressed.TabIndex = 0;
			this.chkSuppressed.Text = "&Suppressed/state changed";
			this.chkSuppressed.UseVisualStyleBackColor = true;
			// 
			// ExportTableEntryPropertiesDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 350);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.lblSourceData);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.fraFlags);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.cboParentObjectName);
			this.Controls.Add(this.lblClassName);
			this.Controls.Add(this.txtSourceData);
			this.Controls.Add(this.txtObjectName);
			this.Controls.Add(this.lblParentObjectName);
			this.Controls.Add(this.lblObjectName);
			this.MinimumSize = new System.Drawing.Size(400, 272);
			this.Name = "ExportTableEntryPropertiesDialog";
			this.Text = "Export Table Entry Properties";
			this.fraFlags.ResumeLayout(false);
			this.fraFlags.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblObjectName;
		private System.Windows.Forms.Label lblParentObjectName;
		private System.Windows.Forms.Label lblClassName;
		private System.Windows.Forms.GroupBox fraFlags;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Label lblSourceData;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.TextBox txtObjectName;
		internal System.Windows.Forms.ComboBox cboParentObjectName;
		internal System.Windows.Forms.ComboBox comboBox1;
		internal System.Windows.Forms.TextBox txtSourceData;
		private System.Windows.Forms.CheckBox chkTransactional;
		private System.Windows.Forms.CheckBox chkUnreachable;
		private System.Windows.Forms.CheckBox chkPublic;
		private System.Windows.Forms.CheckBox chkExporting;
		private System.Windows.Forms.CheckBox chkImporting;
		private System.Windows.Forms.CheckBox chkSourceModified;
		private System.Windows.Forms.CheckBox chkGarbageCollect;
		private System.Windows.Forms.CheckBox chkRequireLoad;
		private System.Windows.Forms.CheckBox chkHighlightName;
		private System.Windows.Forms.CheckBox chkRemappedName;
		private System.Windows.Forms.CheckBox chkSuppressed;
	}
}