namespace UniversalEditor.Editors.Multimedia.Audio.Waveform
{
    partial class WaveformAudioEditor
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
			this.trackList = new UniversalEditor.Controls.Multimedia.Audio.Waveform.WaveformTrackList.WaveformTrackListControl();
			this.cmdPlay = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// trackList
			// 
			this.trackList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackList.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.trackList.Location = new System.Drawing.Point(0, 32);
			this.trackList.Name = "trackList";
			this.trackList.Size = new System.Drawing.Size(471, 255);
			this.trackList.TabIndex = 0;
			// 
			// cmdPlay
			// 
			this.cmdPlay.Location = new System.Drawing.Point(3, 3);
			this.cmdPlay.Name = "cmdPlay";
			this.cmdPlay.Size = new System.Drawing.Size(75, 23);
			this.cmdPlay.TabIndex = 1;
			this.cmdPlay.Text = "&Play";
			this.cmdPlay.UseVisualStyleBackColor = true;
			this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
			// 
			// WaveformAudioEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdPlay);
			this.Controls.Add(this.trackList);
			this.Name = "WaveformAudioEditor";
			this.Size = new System.Drawing.Size(471, 287);
			this.ResumeLayout(false);

        }

        #endregion

        private Controls.Multimedia.Audio.Waveform.WaveformTrackList.WaveformTrackListControl trackList;
        private System.Windows.Forms.Button cmdPlay;
    }
}
