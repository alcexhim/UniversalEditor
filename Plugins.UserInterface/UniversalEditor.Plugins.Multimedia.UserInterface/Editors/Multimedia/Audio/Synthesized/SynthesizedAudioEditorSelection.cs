//
//  PianoRollEditorSelection.cs
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

using MBS.Framework.Drawing;
using UniversalEditor.Editors.Multimedia.Audio.Synthesized.Views.PianoRoll;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioEditorSelection : Selection
	{
		private Views.PianoRoll.PianoRollView _parent = null;
		internal SynthesizedAudioEditorSelection(Views.PianoRoll.PianoRollView parent)
		{
			_parent = parent;
		}

		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection Commands { get; private set; } = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		public override object Content { get => Commands; set => Commands = (SynthesizedAudioCommand.SynthesizedAudioCommandCollection)value; }

		protected override void DeleteInternal()
		{
			(_parent.Parent as Editor).BeginEdit();
			for (int i = 0; i < Commands.Count; i++)
			{
				SynthesizedAudioCommand cmd = Commands[i];
				Rectangle bounds = (_parent.Parent as SynthesizedAudioEditor).PianoRoll.GetCommandBounds(cmd);

				(_parent.Parent as SynthesizedAudioEditor).SelectedTrack.Commands.Remove(cmd);
				(_parent.Parent as SynthesizedAudioEditor).PianoRoll.OnNoteDeleted(new NoteEventArgs(cmd, bounds));
			}
			(_parent.Parent as Editor).EndEdit();
		}
	}
}
