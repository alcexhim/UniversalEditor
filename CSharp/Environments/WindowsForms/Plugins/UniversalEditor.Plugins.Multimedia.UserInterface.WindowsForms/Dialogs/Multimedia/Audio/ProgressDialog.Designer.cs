using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Multimedia.Audio
{
	partial class ProgressDialog : Form
	{
		private IContainer components = null;
		internal ProgressBar pb2;
		private Button cmdCancel;
		private Label lblTrackProgress;
		internal ProgressBar pb1;
		private Label lblNoteProgress;
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.pb2 = new System.Windows.Forms.ProgressBar();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.lblTrackProgress = new System.Windows.Forms.Label();
			this.pb1 = new System.Windows.Forms.ProgressBar();
			this.lblNoteProgress = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pb2
			// 
			this.pb2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pb2.Location = new System.Drawing.Point(12, 76);
			this.pb2.Name = "pb2";
			this.pb2.Size = new System.Drawing.Size(264, 23);
			this.pb2.TabIndex = 3;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(111, 105);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// lblTrackProgress
			// 
			this.lblTrackProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblTrackProgress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTrackProgress.Location = new System.Drawing.Point(12, 9);
			this.lblTrackProgress.Name = "lblTrackProgress";
			this.lblTrackProgress.Size = new System.Drawing.Size(264, 13);
			this.lblTrackProgress.TabIndex = 0;
			this.lblTrackProgress.Text = "Synthesizing track 1 of 1...";
			// 
			// pb1
			// 
			this.pb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pb1.Location = new System.Drawing.Point(12, 25);
			this.pb1.Name = "pb1";
			this.pb1.Size = new System.Drawing.Size(264, 23);
			this.pb1.TabIndex = 1;
			// 
			// lblNoteProgress
			// 
			this.lblNoteProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblNoteProgress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNoteProgress.Location = new System.Drawing.Point(12, 60);
			this.lblNoteProgress.Name = "lblNoteProgress";
			this.lblNoteProgress.Size = new System.Drawing.Size(264, 13);
			this.lblNoteProgress.TabIndex = 2;
			this.lblNoteProgress.Text = "Synthesizing note 1 of 1...";
			// 
			// ProgressDialog
			// 
			this.AcceptButton = this.cmdCancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(296, 144);
			this.Controls.Add(this.lblNoteProgress);
			this.Controls.Add(this.lblTrackProgress);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.pb1);
			this.Controls.Add(this.pb2);
			this.Font = new System.Drawing.Font("Tahoma", 8F);
			this.MinimumSize = new System.Drawing.Size(304, 178);
			this.Name = "ProgressDialog";
			this.ShowIcon = false;
			this.Text = "Progress";
			this.ResumeLayout(false);

		}
	}
}
