namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Panels.Multimedia3D.Motion
{
	partial class TimelinePanel
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
			this.timelineControl1 = new AwesomeControls.Timeline.TimelineControl();
			this.SuspendLayout();
			// 
			// timelineControl1
			// 
			this.timelineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.timelineControl1.EntryContextMenuStrip = null;
			this.timelineControl1.EntryQuantization = 8;
			this.timelineControl1.GroupContextMenuStrip = null;
			this.timelineControl1.Location = new System.Drawing.Point(0, 0);
			this.timelineControl1.Name = "timelineControl1";
			this.timelineControl1.Size = new System.Drawing.Size(385, 203);
			this.timelineControl1.TabIndex = 1;
			// 
			// TimelinePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.timelineControl1);
			this.Name = "TimelinePanel";
			this.Size = new System.Drawing.Size(385, 203);
			this.ResumeLayout(false);

		}

		#endregion

		private AwesomeControls.Timeline.TimelineControl timelineControl1;
	}
}
