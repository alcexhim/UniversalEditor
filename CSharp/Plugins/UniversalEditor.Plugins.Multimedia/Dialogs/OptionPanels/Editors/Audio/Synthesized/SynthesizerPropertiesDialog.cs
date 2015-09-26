using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace UniversalEditor.Plugins.Multimedia.Dialogs.OptionPanels.Editors.Audio.SynthesizedAudio
{
	public class SynthesizerPropertiesDialog : Form
	{
		private IContainer components = null;
		public SynthesizerPropertiesDialog()
		{
			this.InitializeComponent();
		}
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
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(526, 295);
			base.Name = "SynthesizerPropertiesDialog";
			this.Text = "SynthesizerPropertiesDialog";
			base.ResumeLayout(false);
		}
	}
}
