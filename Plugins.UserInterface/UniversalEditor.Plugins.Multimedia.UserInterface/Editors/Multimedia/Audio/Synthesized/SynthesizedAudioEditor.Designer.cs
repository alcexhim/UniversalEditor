//
//  PianoRollEditor.Designer.cs - UWT designer initialization for PianoRollEditor
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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.UserInterface;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using System;
using MBS.Framework;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized
{
	partial class SynthesizedAudioEditor : Editor
	{
		public SynthesizedAudioEditor()
		{
			InitializeComponent();
		}

		public MBS.Audio.ITransport Transport { get; set; } = null;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Views.Add("Piano Roll");
				_er.Views.Add("Score");
				_er.Views.Add("MIDI Events");

				_er.DefaultView = _er.Views[0];
				_er.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
			}
			return _er;
		}

		protected override void OnViewChanged(EditorViewChangedEventArgs e)
		{
			base.OnViewChanged(e);

			switch (e.NewView.Title)
			{
				case "Piano Roll":
				{
					PianoRoll.Visible = true;
					PianoRoll.Refresh();
					// PianoRoll.UpdateView();

					MIDIEvents.Visible = false;
					break;
				}
				case "MIDI Events":
				{
					PianoRoll.Visible = false;
					MIDIEvents.UpdateView();

					MIDIEvents.Visible = true;
					break;
				}
			}
		}

		public Views.PianoRoll.PianoRollView PianoRoll = null;
		public Views.MIDIEvents.MIDIEventsView MIDIEvents = null;

		/// <summary>
		/// UWT designer initialization for <see cref="SynthesizedAudioEditor" />.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			PianoRoll = new Views.PianoRoll.PianoRollView();
			this.Controls.Add(PianoRoll, new BoxLayout.Constraints(true, true));

			MIDIEvents = new Views.MIDIEvents.MIDIEventsView();
			this.Controls.Add(MIDIEvents, new BoxLayout.Constraints(true, true));

			MIDIEvents.Visible = false;
		}
	}
}
