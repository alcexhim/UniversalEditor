namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
	partial class SelectPerspectiveDialogBase
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
			this.fraEnvironments = new System.Windows.Forms.GroupBox();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdModify = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.lvEnvironments = new System.Windows.Forms.ListView();
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.fraEnvironments.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraEnvironments
			// 
			this.fraEnvironments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraEnvironments.Controls.Add(this.cmdRemove);
			this.fraEnvironments.Controls.Add(this.cmdModify);
			this.fraEnvironments.Controls.Add(this.cmdAdd);
			this.fraEnvironments.Controls.Add(this.txtSearch);
			this.fraEnvironments.Controls.Add(this.lvEnvironments);
			this.fraEnvironments.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraEnvironments.Location = new System.Drawing.Point(12, 12);
			this.fraEnvironments.Name = "fraEnvironments";
			this.fraEnvironments.Size = new System.Drawing.Size(404, 180);
			this.fraEnvironments.TabIndex = 0;
			this.fraEnvironments.TabStop = false;
			this.fraEnvironments.Text = "Available environments";
			// 
			// cmdRemove
			// 
			this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdRemove.Location = new System.Drawing.Point(323, 77);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdRemove.TabIndex = 4;
			this.cmdRemove.Text = "&Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// cmdModify
			// 
			this.cmdModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdModify.Location = new System.Drawing.Point(323, 48);
			this.cmdModify.Name = "cmdModify";
			this.cmdModify.Size = new System.Drawing.Size(75, 23);
			this.cmdModify.TabIndex = 3;
			this.cmdModify.Text = "&Modify...";
			this.cmdModify.UseVisualStyleBackColor = true;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(323, 19);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdAdd.TabIndex = 2;
			this.cmdAdd.Text = "&Add...";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point(6, 19);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(311, 20);
			this.txtSearch.TabIndex = 0;
			// 
			// lvEnvironments
			// 
			this.lvEnvironments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvEnvironments.CheckBoxes = true;
			this.lvEnvironments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chDescription});
			this.lvEnvironments.FullRowSelect = true;
			this.lvEnvironments.GridLines = true;
			this.lvEnvironments.HideSelection = false;
			this.lvEnvironments.Location = new System.Drawing.Point(6, 41);
			this.lvEnvironments.Name = "lvEnvironments";
			this.lvEnvironments.Size = new System.Drawing.Size(311, 133);
			this.lvEnvironments.TabIndex = 1;
			this.lvEnvironments.UseCompatibleStateImageBehavior = false;
			this.lvEnvironments.View = System.Windows.Forms.View.Details;
			this.lvEnvironments.SelectedIndexChanged += new System.EventHandler(this.lvEnvironments_SelectedIndexChanged);
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 109;
			// 
			// chDescription
			// 
			this.chDescription.Text = "Description";
			this.chDescription.Width = 184;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(341, 198);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Enabled = false;
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(260, 198);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// SelectEnvironmentDialogBase
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(428, 233);
			this.Controls.Add(this.fraEnvironments);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.MinimumSize = new System.Drawing.Size(444, 271);
			this.Name = "SelectEnvironmentDialogBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Environments";
			this.fraEnvironments.ResumeLayout(false);
			this.fraEnvironments.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraEnvironments;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdModify;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.ListView lvEnvironments;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chDescription;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;

	}
}