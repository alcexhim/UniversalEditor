//
//  PianoRollEditor.Designer.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.UserInterface;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

using UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Controls;
using UniversalEditor.ObjectModels.Markup;

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
				_er.SupportedObjectModels.Add(typeof(SynthesizedAudioObjectModel));
			}
			return _er;
		}

		private PianoRollView PianoRoll = null;

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);


		}

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			PianoRoll = new PianoRollView();
			this.Controls.Add(PianoRoll, new BoxLayout.Constraints(true, true));
		}
	}
}
