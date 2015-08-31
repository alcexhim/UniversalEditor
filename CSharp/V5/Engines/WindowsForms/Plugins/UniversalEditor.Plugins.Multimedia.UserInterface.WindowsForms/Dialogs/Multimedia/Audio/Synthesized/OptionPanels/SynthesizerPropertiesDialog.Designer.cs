using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace UniversalEditor.Dialogs.Multimedia.Audio.Synthesized.OptionPanels
{
	partial class SynthesizerPropertiesDialog : Form
	{
		private IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(526, 295);
			base.Name = "SynthesizerPropertiesDialog";
			this.Text = "SynthesizerPropertiesDialog";
			base.ResumeLayout(false);
		}
	}
}
