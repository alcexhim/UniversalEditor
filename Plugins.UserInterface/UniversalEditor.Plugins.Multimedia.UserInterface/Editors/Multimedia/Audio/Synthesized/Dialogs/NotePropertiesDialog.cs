//
//  NotePropertiesDialog.cs - provides a UWT Dialog that modifies the properties of a SynthesizedAudioCommandNote
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.Dialogs
{
	/// <summary>
	/// Provides a UWT <see cref="ContainerLayoutAttribute" />-based <see cref="CustomDialog" /> that modifies the properties of a
	/// <see cref="ObjectModels.Multimedia.Audio.Synthesized.SynthesizedAudioCommandNote" />.
	/// </summary>
	[ContainerLayout("~/Editors/Multimedia/Synthesized/Dialogs/NotePropertiesDialog.glade")]
	public class NotePropertiesDialog : CustomDialog
	{
		private TextBox txtNoteValue;
		private TextBox txtLyric;
		private TextBox txtPhoneme;
		private NumericTextBox txtNoteOn;
		private NumericTextBox txtNoteOff;

		private Button cmdOK;

		public string Lyric { get; set; } = null;
		public string Phoneme { get; set; } = null;
		public double NoteOn { get; set; } = 0.0;
		public double NoteOff { get; set; } = 0.0;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			DefaultButton = cmdOK;

			txtLyric.Text = Lyric;
			txtPhoneme.Text = Phoneme;
			txtNoteOn.Value = NoteOn;
			txtNoteOff.Value = NoteOff;
		}

		[EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			Lyric = txtLyric.Text;
			Phoneme = txtPhoneme.Text;
			NoteOn = txtNoteOn.Value;
			NoteOff = txtNoteOff.Value;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
