//
//  MIDIEventsView.cs
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

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.Views.MIDIEvents
{
	[ContainerLayout("~/Editors/Multimedia/Audio/Synthesized/Views/MIDIEvents/MIDIEventsView.glade")]
	public class MIDIEventsView : View
	{
		private DefaultTreeModel tmEvents;

		public void UpdateView()
		{
			if (!IsCreated) return;

			tmEvents.Rows.Clear();

			SynthesizedAudioObjectModel syn = ((Parent as Editor).ObjectModel as SynthesizedAudioObjectModel);
			if (syn == null) return;
			if ((Parent as SynthesizedAudioEditor).SelectedTrack == null) return;

			for (int i = 0; i < (Parent as SynthesizedAudioEditor).SelectedTrack.Commands.Count; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmEvents.Columns[0], (Parent as SynthesizedAudioEditor).SelectedTrack.Commands[i].GetType().Name),
					new TreeModelRowColumn(tmEvents.Columns[1], ((Parent as SynthesizedAudioEditor).SelectedTrack.Commands[i] as SynthesizedAudioCommandNote)?.Position),
					new TreeModelRowColumn(tmEvents.Columns[2], ((Parent as SynthesizedAudioEditor).SelectedTrack.Commands[i] as SynthesizedAudioCommandNote)?.Frequency)
				});
				tmEvents.Rows.Add(row);
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			UpdateView();
		}
	}
}
