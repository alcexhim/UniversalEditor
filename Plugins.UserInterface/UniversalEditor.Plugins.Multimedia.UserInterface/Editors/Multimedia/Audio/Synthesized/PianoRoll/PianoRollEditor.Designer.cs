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

using UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Views;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll
{
	partial class PianoRollEditor : Editor
	{
		public PianoRollEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Views.Add("Piano Roll");
				_er.Views.Add("Score");
				_er.Views.Add("MIDI Events");
				_er.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
			}
			return _er;
		}

		private PianoRollView PianoRoll = null;

		/// <summary>
		/// UWT designer initialization for <see cref="PianoRollEditor" />.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			PianoRoll = new PianoRollView(this);
			this.Controls.Add(PianoRoll, new BoxLayout.Constraints(true, true));
		}
	}
}
