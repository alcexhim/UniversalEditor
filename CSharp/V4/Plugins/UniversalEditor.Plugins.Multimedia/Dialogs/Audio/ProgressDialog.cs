using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace UniversalEditor.Plugins.Multimedia.Dialogs.Audio
{
	public class ProgressDialog : Form
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
			this.pb2 = new ProgressBar();
			this.cmdCancel = new Button();
			this.lblTrackProgress = new Label();
			this.pb1 = new ProgressBar();
			this.lblNoteProgress = new Label();
			base.SuspendLayout();
			this.pb2.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.pb2.Location = new Point(12, 76);
			this.pb2.Name = "pb2";
			this.pb2.Size = new Size(264, 23);
			this.pb2.TabIndex = 3;
			this.cmdCancel.Anchor = AnchorStyles.Bottom;
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.System;
			this.cmdCancel.Location = new Point(107, 105);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.lblTrackProgress.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.lblTrackProgress.FlatStyle = FlatStyle.System;
			this.lblTrackProgress.Location = new Point(12, 9);
			this.lblTrackProgress.Name = "lblTrackProgress";
			this.lblTrackProgress.Size = new Size(264, 13);
			this.lblTrackProgress.TabIndex = 0;
			this.lblTrackProgress.Text = "Synthesizing track 1 of 1...";
			this.pb1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.pb1.Location = new Point(12, 25);
			this.pb1.Name = "pb1";
			this.pb1.Size = new Size(264, 23);
			this.pb1.TabIndex = 1;
			this.lblNoteProgress.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.lblNoteProgress.FlatStyle = FlatStyle.System;
			this.lblNoteProgress.Location = new Point(12, 60);
			this.lblNoteProgress.Name = "lblNoteProgress";
			this.lblNoteProgress.Size = new Size(264, 13);
			this.lblNoteProgress.TabIndex = 2;
			this.lblNoteProgress.Text = "Synthesizing note 1 of 1...";
			base.AcceptButton = this.cmdCancel;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new Size(288, 140);
			base.Controls.Add(this.lblNoteProgress);
			base.Controls.Add(this.lblTrackProgress);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.pb1);
			base.Controls.Add(this.pb2);
			this.Font = new Font("Tahoma", 8f);
			this.MinimumSize = new Size(304, 178);
			base.Name = "ProgressDialog";
			base.ShowIcon = false;
			this.Text = "Progress";
			base.ResumeLayout(false);
		}
		public ProgressDialog()
		{
			this.InitializeComponent();
		}
	}
}
