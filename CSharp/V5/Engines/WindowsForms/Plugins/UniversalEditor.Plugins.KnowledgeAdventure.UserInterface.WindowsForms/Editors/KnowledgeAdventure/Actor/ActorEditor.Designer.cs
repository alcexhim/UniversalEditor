namespace UniversalEditor.Plugins.KnowledgeAdventure.UserInterface.WindowsForms.Editors.KnowledgeAdventure.Actor
{
	partial class ActorEditor
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
			this.fraGeneralInformation = new System.Windows.Forms.GroupBox();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblImageFileName = new System.Windows.Forms.Label();
			this.txtImageFileName = new System.Windows.Forms.TextBox();
			this.cmdBrowseImageFileName = new System.Windows.Forms.Button();
			this.fraGeneralInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraGeneralInformation
			// 
			this.fraGeneralInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fraGeneralInformation.Controls.Add(this.cmdBrowseImageFileName);
			this.fraGeneralInformation.Controls.Add(this.txtImageFileName);
			this.fraGeneralInformation.Controls.Add(this.lblImageFileName);
			this.fraGeneralInformation.Controls.Add(this.txtName);
			this.fraGeneralInformation.Controls.Add(this.lblName);
			this.fraGeneralInformation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraGeneralInformation.Location = new System.Drawing.Point(4, 4);
			this.fraGeneralInformation.Name = "fraGeneralInformation";
			this.fraGeneralInformation.Size = new System.Drawing.Size(326, 100);
			this.fraGeneralInformation.TabIndex = 0;
			this.fraGeneralInformation.TabStop = false;
			this.fraGeneralInformation.Text = "General information";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblName.Location = new System.Drawing.Point(15, 22);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(64, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Actor &name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(105, 19);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(215, 20);
			this.txtName.TabIndex = 1;
			// 
			// lblImageFileName
			// 
			this.lblImageFileName.AutoSize = true;
			this.lblImageFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblImageFileName.Location = new System.Drawing.Point(15, 48);
			this.lblImageFileName.Name = "lblImageFileName";
			this.lblImageFileName.Size = new System.Drawing.Size(84, 13);
			this.lblImageFileName.TabIndex = 0;
			this.lblImageFileName.Text = "Image &file name:";
			// 
			// txtImageFileName
			// 
			this.txtImageFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtImageFileName.Location = new System.Drawing.Point(105, 45);
			this.txtImageFileName.Name = "txtImageFileName";
			this.txtImageFileName.Size = new System.Drawing.Size(215, 20);
			this.txtImageFileName.TabIndex = 1;
			// 
			// cmdBrowseImageFileName
			// 
			this.cmdBrowseImageFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseImageFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdBrowseImageFileName.Location = new System.Drawing.Point(245, 71);
			this.cmdBrowseImageFileName.Name = "cmdBrowseImageFileName";
			this.cmdBrowseImageFileName.Size = new System.Drawing.Size(75, 23);
			this.cmdBrowseImageFileName.TabIndex = 2;
			this.cmdBrowseImageFileName.Text = "&Browse...";
			this.cmdBrowseImageFileName.UseVisualStyleBackColor = true;
			this.cmdBrowseImageFileName.Click += new System.EventHandler(this.cmdBrowseImageFileName_Click);
			// 
			// ActorEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fraGeneralInformation);
			this.Name = "ActorEditor";
			this.Size = new System.Drawing.Size(333, 227);
			this.fraGeneralInformation.ResumeLayout(false);
			this.fraGeneralInformation.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraGeneralInformation;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button cmdBrowseImageFileName;
		private System.Windows.Forms.TextBox txtImageFileName;
		private System.Windows.Forms.Label lblImageFileName;
		private System.Windows.Forms.TextBox txtName;
	}
}
