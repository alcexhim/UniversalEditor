//
//  PianoRollView.cs - provides a UWT-based View for manipulating SynthesizedAudioCommands in a piano roll style
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
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.Views.PianoRoll
{
	public class NoteEventArgs : EventArgs
	{
		public SynthesizedAudioCommand Note { get; private set; }
		public Rectangle Bounds { get; private set; }

		public NoteEventArgs(SynthesizedAudioCommand note, Rectangle bounds)
		{
			this.Note = note;
			this.Bounds = bounds;
		}
	}
}
