/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/4/2013
 * Time: 3:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace UniversalEditor.Controls
{
	partial class ProgressPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblTask = new System.Windows.Forms.Label();
			this.pb = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTask
			// 
			this.lblTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTask.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTask.Location = new System.Drawing.Point(3, 17);
			this.lblTask.Name = "lblTask";
			this.lblTask.Size = new System.Drawing.Size(382, 19);
			this.lblTask.TabIndex = 0;
			this.lblTask.Text = "Task name";
			this.lblTask.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pb
			// 
			this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pb.Location = new System.Drawing.Point(3, 39);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(382, 23);
			this.pb.TabIndex = 1;
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblProgress.Location = new System.Drawing.Point(3, 68);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(382, 19);
			this.lblProgress.TabIndex = 0;
			this.lblProgress.Text = "100%";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ProgressPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pb);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.lblTask);
			this.Name = "ProgressPanel";
			this.Size = new System.Drawing.Size(388, 104);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.Label lblTask;
	}
}
