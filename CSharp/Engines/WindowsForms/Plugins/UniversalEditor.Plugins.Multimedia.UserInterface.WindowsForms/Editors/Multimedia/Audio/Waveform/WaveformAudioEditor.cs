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
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Waveform
{
	public partial class WaveformAudioEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(WaveformAudioObjectModel));
			}
			return _er;
		}

		public WaveformAudioEditor()
		{
			InitializeComponent();
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
