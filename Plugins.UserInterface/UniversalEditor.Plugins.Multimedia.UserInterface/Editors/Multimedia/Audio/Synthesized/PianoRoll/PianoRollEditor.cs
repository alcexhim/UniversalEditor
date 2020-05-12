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

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll
{
	/// <summary>
	/// Provides a UWT-based piano roll-style <see cref="Editor" /> for a <see cref="SynthesizedAudioObjectModel" />.
	/// </summary>
	public partial class PianoRollEditor
	{
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			SynthesizedAudioObjectModel om = (ObjectModel as SynthesizedAudioObjectModel);
			if (om == null) return;

			if (om.Tracks.Count == 0)
			{
				// HACK: since we don't have a good way to specify defaults for blank templates.
				// this fixes the fixme in SynthesizedAudioObjectModel.cs so we don't have an extra empty track when we open a new file
				om.Tracks.Add(new SynthesizedAudioTrack());
			}
			PianoRoll.SelectedTrack = om.Tracks[0];

			PianoRoll.Refresh();
		}

		protected override void OnToolboxItemSelected(ToolboxItemEventArgs e)
		{
			base.OnToolboxItemSelected(e);

			switch (e.Item.Name)
			{
				case "ToolboxItem_Select":
				{
					PianoRoll.SelectionMode = PianoRollSelectionMode.Select;
					break;
				}
				case "ToolboxItem_Insert":
				{
					PianoRoll.SelectionMode = PianoRollSelectionMode.Insert;
					break;
				}
			}
		}
	}
}
