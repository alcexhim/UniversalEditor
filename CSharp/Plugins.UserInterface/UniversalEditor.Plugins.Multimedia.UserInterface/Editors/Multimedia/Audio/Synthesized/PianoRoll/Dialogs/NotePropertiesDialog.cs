//
//  NotePropertiesDialog.cs
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

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Dialogs
{
	public class NotePropertiesDialog : Dialog
	{
		private Label lblNoteValue = null;
		private TextBox txtNoteValue = null;

		public NotePropertiesDialog()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			Container p = new Container();
			p.Layout = new GridLayout();

			lblNoteValue = new Label();
			lblNoteValue.Text = "Note _value:";
			p.Controls.Add(lblNoteValue, new GridLayout.Constraints(0, 0));
			txtNoteValue = new TextBox();
			p.Controls.Add(txtNoteValue, new GridLayout.Constraints(0, 1));

			this.Controls.Add(p, new BoxLayout.Constraints(true, true));

			this.Buttons.Add(new Button(StockType.OK, DialogResult.OK));
			this.Buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));
		}
	}
}
