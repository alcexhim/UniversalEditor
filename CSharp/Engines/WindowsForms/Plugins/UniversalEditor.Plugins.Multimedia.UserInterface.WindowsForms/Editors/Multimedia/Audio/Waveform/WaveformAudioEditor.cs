using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.Editors.Multimedia.Audio.Waveform
{
	public partial class WaveformAudioEditor : Editor
	{
		public WaveformAudioEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(WaveformAudioObjectModel));
		}
		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			WaveformAudioObjectModel wave = (ObjectModel as WaveformAudioObjectModel);
			trackList.Tracks.Add("Track 1", wave);
		}

		private void cmdPlay_Click(object sender, EventArgs e)
		{
			Surodoine.AudioPlayer player = new Surodoine.AudioPlayer();
			cmdPlay.Enabled = false;


			WaveformAudioObjectModel wave = (ObjectModel as WaveformAudioObjectModel);
			player.Play(wave);
			cmdPlay.Enabled = true;
		}
	}
}
