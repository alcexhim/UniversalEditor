namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll
{
    partial class PianoRollControl
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
			this.txtLyric = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtLyric
			// 
			this.txtLyric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLyric.Enabled = false;
			this.txtLyric.Location = new System.Drawing.Point(16, 45);
			this.txtLyric.Name = "txtLyric";
			this.txtLyric.Size = new System.Drawing.Size(71, 20);
			this.txtLyric.TabIndex = 0;
			this.txtLyric.Visible = false;
			this.txtLyric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLyric_KeyDown);
			// 
			// PianoRollControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtLyric);
			this.Name = "PianoRollControl";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox txtLyric;
    }
}
