//
//  PianoRollEditor.cs - provides a UWT-based piano roll-style Editor for a SynthesizedAudioObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Provides a UWT-based piano roll-style <see cref="Editor" /> for a <see cref="SynthesizedAudioObjectModel" />.
	/// </summary>
	public partial class SynthesizedAudioEditor
	{
		// This is the first real implementation of an Editor with multiple Views.

		// My original intent was rather than have multiple Editors copying ObjectModels back and forth when they're switched,
		// the single Editor would reference a single ObjectModel which all Views would draw from to display their content.
		// 
		// Unfortunately maintaining all of this state ourselves rather defeats the purpose of using a "Universal" Editor platform.
		// The platform itself should handle most of the heavy lifting and we should not have to worry about synchronizing things
		// like ObjectModel content and selections and whatever between multiple Views.

		private SynthesizedAudioTrack _SelectedTrack = null;
		public SynthesizedAudioTrack SelectedTrack { get { return _SelectedTrack; } set { _SelectedTrack = value; PianoRoll.Refresh(); MIDIEvents.UpdateView(); } }

		public override void UpdateSelections()
		{
			Selections.Clear();
			if (PianoRoll.SelectedCommands.Count > 0)
			{
				SynthesizedAudioEditorSelection sel = new SynthesizedAudioEditorSelection(PianoRoll);
				for (int i = 0; i < PianoRoll.SelectedCommands.Count; i++)
				{
					sel.Commands.Add(PianoRoll.SelectedCommands[i]);
				}
				Selections.Add(sel);
			}
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		protected override void OnDocumentEdited(EventArgs e)
		{
			base.OnDocumentEdited(e);
			PianoRoll.Refresh();
		}
		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DocumentExplorer.Nodes.Clear();

			SynthesizedAudioObjectModel om = (ObjectModel as SynthesizedAudioObjectModel);
			if (om == null) return;

			if (om.Tracks.Count == 0)
			{
				// HACK: since we don't have a good way to specify defaults for blank templates (yet).
				// this fixes the fixme in SynthesizedAudioObjectModel.cs so we don't have an extra empty track when we open an existing file
				om.Tracks.Add(new SynthesizedAudioTrack());
			}
			SelectedTrack = om.Tracks[0];

			EditorDocumentExplorerNode nodeTracks = DocumentExplorer.Nodes.Add("Tracks");
			for (int i = 0; i < om.Tracks.Count; i++)
			{
				EditorDocumentExplorerNode nodeTrack = new EditorDocumentExplorerNode(om.Tracks[i].Name);
				nodeTrack.SetExtraData<SynthesizedAudioTrack>("track", om.Tracks[i]);
				nodeTracks.Nodes.Add(nodeTrack);
			}

			PianoRoll.Refresh();
		}

		protected override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			if (e.Node == null) return;

			SynthesizedAudioTrack track = e.Node.GetExtraData<SynthesizedAudioTrack>("track");
			if (track != null)
			{
				SelectedTrack = track;
			}
		}

		protected override void OnToolboxItemSelected(ToolboxItemEventArgs e)
		{
			base.OnToolboxItemSelected(e);

			switch (e.Item.Name)
			{
				case "ToolboxItem_Select":
				{
					PianoRoll.SelectionMode = Views.PianoRoll.PianoRollViewSelectionMode.Select;
					break;
				}
				case "ToolboxItem_Insert":
				{
					PianoRoll.SelectionMode = Views.PianoRoll.PianoRollViewSelectionMode.Insert;
					break;
				}
			}
		}
	}
}
