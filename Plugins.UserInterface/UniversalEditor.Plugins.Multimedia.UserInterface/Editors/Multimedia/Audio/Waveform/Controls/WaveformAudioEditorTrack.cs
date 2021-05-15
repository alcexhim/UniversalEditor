//
//  WaveformAudioEditorTrack.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls
{
	[ContainerLayout(typeof(WaveformAudioEditorTrack), "UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls.WaveformAudioEditorTrack.glade")]
	public class WaveformAudioEditorTrack : Container
	{
		private WaveformAudioEditorTrackControlPanel ctTrackControlPanel;
		private WaveformAudioEditorTrackWaveform ctTrackWaveform;

		public WaveformAudioObjectModel ObjectModel { get; set; }

		public WaveformAudioEditorTrack()
		{
		}
		public WaveformAudioEditorTrack(WaveformAudioObjectModel wave)
		{
			ObjectModel = wave;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
		}
	}
}
