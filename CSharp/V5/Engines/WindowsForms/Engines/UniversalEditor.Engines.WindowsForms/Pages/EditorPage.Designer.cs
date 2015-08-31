using UniversalEditor.UserInterface.WindowsForms.Controls;
namespace UniversalEditor.Engines.WindowsForms.Pages
{
	partial class EditorPage
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
			this.errorMessage1 = new UniversalEditor.UserInterface.WindowsForms.Controls.ErrorMessage();
			this.pnlLoading = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.pnlLoading.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorMessage1
			// 
			this.errorMessage1.Details = "";
			this.errorMessage1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorMessage1.Enabled = false;
			this.errorMessage1.Location = new System.Drawing.Point(0, 0);
			this.errorMessage1.Name = "errorMessage1";
			this.errorMessage1.Size = new System.Drawing.Size(555, 329);
			this.errorMessage1.TabIndex = 0;
			this.errorMessage1.Title = "There are no editors available for this object model";
			this.errorMessage1.Visible = false;
			// 
			// pnlLoading
			// 
			this.pnlLoading.Controls.Add(this.label1);
			this.pnlLoading.Controls.Add(this.progressBar1);
			this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlLoading.Enabled = false;
			this.pnlLoading.Location = new System.Drawing.Point(0, 0);
			this.pnlLoading.Name = "pnlLoading";
			this.pnlLoading.Size = new System.Drawing.Size(555, 329);
			this.pnlLoading.TabIndex = 1;
			this.pnlLoading.Visible = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(80, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(396, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Loading, please wait...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(80, 115);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(396, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 0;
			// 
			// EditorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.errorMessage1);
			this.Controls.Add(this.pnlLoading);
			this.Name = "EditorPage";
			this.Size = new System.Drawing.Size(555, 329);
			this.pnlLoading.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ErrorMessage errorMessage1;
		private System.Windows.Forms.Panel pnlLoading;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;

	}
}
