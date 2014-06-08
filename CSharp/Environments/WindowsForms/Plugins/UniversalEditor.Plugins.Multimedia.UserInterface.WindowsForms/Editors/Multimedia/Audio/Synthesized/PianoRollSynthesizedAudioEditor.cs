using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
	public partial class PianoRollSynthesizedAudioEditor : Editor
	{
		public override string Title { get { return "Piano Roll"; } }

		public PianoRollSynthesizedAudioEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
		}
	}
}
