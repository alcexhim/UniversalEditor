namespace UniversalEditor.Dialogs.Contact
{
	partial class EmailPropertiesDialogImpl
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
			this.lblEmailAddress = new System.Windows.Forms.Label();
			this.txtEmailAddress = new System.Windows.Forms.TextBox();
			this.cmdCompose = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.fraLabels = new System.Windows.Forms.GroupBox();
			this.lvLabels = new AwesomeControls.CollectionListView.CollectionListViewControl();
			this.fraProperties = new System.Windows.Forms.GroupBox();
			this.copc = new UniversalEditor.Controls.ComplexObjectPropertiesControl();
			this.fraLabels.SuspendLayout();
			this.fraProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblEmailAddress
			// 
			this.lblEmailAddress.AutoSize = true;
			this.lblEmailAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblEmailAddress.Location = new System.Drawing.Point(12, 15);
			this.lblEmailAddress.Name = "lblEmailAddress";
			this.lblEmailAddress.Size = new System.Drawing.Size(78, 13);
			this.lblEmailAddress.TabIndex = 0;
			this.lblEmailAddress.Text = "&E-mail address:";
			// 
			// txtEmailAddress
			// 
			this.txtEmailAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEmailAddress.Location = new System.Drawing.Point(109, 12);
			this.txtEmailAddress.Name = "txtEmailAddress";
			this.txtEmailAddress.Size = new System.Drawing.Size(380, 20);
			this.txtEmailAddress.TabIndex = 1;
			// 
			// cmdCompose
			// 
			this.cmdCompose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCompose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCompose.Location = new System.Drawing.Point(414, 38);
			this.cmdCompose.Name = "cmdCompose";
			this.cmdCompose.Size = new System.Drawing.Size(75, 23);
			this.cmdCompose.TabIndex = 2;
			this.cmdCompose.Text = "Co&mpose...";
			this.cmdCompose.UseVisualStyleBackColor = true;
			this.cmdCompose.Click += new System.EventHandler(this.cmdCompose_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(414, 340);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(333, 340);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// fraLabels
			// 
			this.fraLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraLabels.Controls.Add(this.lvLabels);
			this.fraLabels.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraLabels.Location = new System.Drawing.Point(12, 199);
			this.fraLabels.Name = "fraLabels";
			this.fraLabels.Size = new System.Drawing.Size(477, 135);
			this.fraLabels.TabIndex = 4;
			this.fraLabels.TabStop = false;
			this.fraLabels.Text = "Labels";
			// 
			// lvLabels
			// 
			this.lvLabels.AllowItemInsert = true;
			this.lvLabels.AllowItemModify = true;
			this.lvLabels.AllowItemRemove = true;
			this.lvLabels.AllowItemReorder = false;
			this.lvLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvLabels.FullRowSelect = true;
			this.lvLabels.HideSelection = true;
			this.lvLabels.ItemNamePlural = "items";
			this.lvLabels.ItemNameSingular = "item";
			this.lvLabels.Location = new System.Drawing.Point(6, 19);
			this.lvLabels.MultiSelect = false;
			this.lvLabels.Name = "lvLabels";
			this.lvLabels.ShowGridLines = true;
			this.lvLabels.Size = new System.Drawing.Size(465, 110);
			this.lvLabels.TabIndex = 0;
			this.lvLabels.RequestItemProperties += new AwesomeControls.CollectionListView.ItemPropertiesEventHandler(this.lvLabels_RequestItemProperties);
			// 
			// fraProperties
			// 
			this.fraProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraProperties.Controls.Add(this.copc);
			this.fraProperties.Location = new System.Drawing.Point(12, 67);
			this.fraProperties.Name = "fraProperties";
			this.fraProperties.Size = new System.Drawing.Size(477, 126);
			this.fraProperties.TabIndex = 3;
			this.fraProperties.TabStop = false;
			this.fraProperties.Text = "Properties";
			// 
			// copc
			// 
			this.copc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.copc.ElementID = new System.Guid("00000000-0000-0000-0000-000000000000");
			this.copc.IsEmpty = false;
			this.copc.Location = new System.Drawing.Point(6, 19);
			this.copc.MinimumSize = new System.Drawing.Size(465, 102);
			this.copc.ModificationDate = new System.DateTime(2015, 5, 18, 15, 29, 37, 74);
			this.copc.Name = "copc";
			this.copc.Size = new System.Drawing.Size(465, 102);
			this.copc.TabIndex = 0;
			// 
			// EmailPropertiesDialogImpl
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(501, 375);
			this.Controls.Add(this.fraProperties);
			this.Controls.Add(this.fraLabels);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdCompose);
			this.Controls.Add(this.txtEmailAddress);
			this.Controls.Add(this.lblEmailAddress);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(517, 413);
			this.Name = "EmailPropertiesDialogImpl";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "E-mail Address Properties";
			this.fraLabels.ResumeLayout(false);
			this.fraProperties.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblEmailAddress;
		private System.Windows.Forms.Button cmdCompose;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.TextBox txtEmailAddress;
		private System.Windows.Forms.GroupBox fraLabels;
		private System.Windows.Forms.GroupBox fraProperties;
		internal Controls.ComplexObjectPropertiesControl copc;
		internal AwesomeControls.CollectionListView.CollectionListViewControl lvLabels;
	}
}