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
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.Views.MIDIEvents
{
	[ContainerLayout("~/Editors/Multimedia/Audio/Synthesized/Views/MIDIEvents/MIDIEventsView.glade")]
	public class MIDIEventsView : View
	{
		private ListViewControl tvEvents;
		private DefaultTreeModel tmEvents;

		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection SelectedCommands { get { return (Parent as SynthesizedAudioEditor).SelectedCommands; } }

		private bool inhibitSelectionChanged = false;

		[EventHandler(nameof(tvEvents), nameof(ListViewControl.SelectionChanged))]
		private void tvEvents_SelectionChanged(object sender, EventArgs e)
		{
			if (inhibitSelectionChanged)
				return;

			SelectedCommands.Clear();
			foreach (TreeModelRow row in tvEvents.SelectedRows)
			{
				SynthesizedAudioCommand cmd = row.GetExtraData<SynthesizedAudioCommand>("cmd");
				SelectedCommands.Add(cmd);
			}
		}

		public void UpdateView()
		{
			if (!IsCreated) return;

			SynthesizedAudioObjectModel syn = ((Parent as Editor).ObjectModel as SynthesizedAudioObjectModel);
			if (syn == null) return;
			if ((Parent as SynthesizedAudioEditor).SelectedTrack == null) return;

			inhibitSelectionChanged = true;

			tmEvents.Rows.Clear();

			bool focused = false;
			for (int i = 0; i < (Parent as SynthesizedAudioEditor).SelectedTrack.Commands.Count; i++)
			{
				SynthesizedAudioCommand cmd = (Parent as SynthesizedAudioEditor).SelectedTrack.Commands[i];
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmEvents.Columns[0], cmd.GetType().Name),
					new TreeModelRowColumn(tmEvents.Columns[1], (cmd as SynthesizedAudioCommandNote)?.Position),
					new TreeModelRowColumn(tmEvents.Columns[2], (cmd as SynthesizedAudioCommandNote)?.Frequency)
				});
				row.SetExtraData<SynthesizedAudioCommand>("cmd", cmd);
				tmEvents.Rows.Add(row);

				if (SelectedCommands.Contains(cmd))
				{
					if (!focused)
					{
						// for some reason, Focus only allows one row to be selected (on GTK3)
						// so ... only call it once, but keep adding selected rows
						tvEvents.Focus(row);
						focused = true;
					}
					tvEvents.SelectedRows.Add(row);
				}
			}

			inhibitSelectionChanged = false;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			tvEvents.ContextMenuCommandID = "MIDIEventsEditor_ContextMenu";

			(Parent as Editor).Context.AttachCommandEventHandler("MIDIEventsEditor_ContextMenu_GoToPianoRoll", ContextMenu_GoToPianoRoll_Click);

			UpdateView();
		}

		private void ContextMenu_GoToPianoRoll_Click(object sender, EventArgs e)
		{
			// FIXME: this looks yucky
			(Parent as Editor).CurrentView = (Parent as Editor).MakeReference().Views[0];
		}
	}
}
