//
//  WaveformAudioEditor.cs
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
using MBS.Audio;
using MBS.Audio.PortAudio;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
using UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform
{
	public class WaveformAudioEditor : Editor
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

		public MBS.Audio.ITransport Transport { get; set; } = null;

		public WaveformAudioEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			this.Controls.Clear();

			WaveformAudioObjectModel wave = (ObjectModel as WaveformAudioObjectModel);
			if (wave == null) return;

			BoxLayout.Constraints cc = new BoxLayout.Constraints(false, false);
			cc.VerticalExpand = false;
			cc.HorizontalExpand = true;
			this.Controls.Add(new WaveformAudioEditorTrack(wave), cc);
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Transport = new MBS.Audio.CustomTransport(Transport_Play, null, null);
		}

		private void Transport_Play(object sender, EventArgs e)
		{
			WaveformAudioObjectModel wave = (ObjectModel as WaveformAudioObjectModel);
			if (wave == null) return /*false*/;

			// get the setting "Editors -> Audio -> Waveform -> Audio engine

			// get the setting "Editors -> Audio -> Waveform -> Synchronize with JACK transport
			AudioPlayer player = new AudioPlayer();
			player.Play(wave);
			return /*true*/;
		}

		public override void UpdateSelections()
		{
			throw new System.NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new System.NotImplementedException();
		}
	}
}
