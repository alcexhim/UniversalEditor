//
//  TextEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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

using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.UserInterface;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Editors.Text.Plain
{
	public class PlainTextEditor : TextEditor
	{
		protected override EditorSelection CreateSelectionInternal(object content)
		{
			if (content is string)
			{
				txt.SelectedText = (string)content;
				return new TextEditorSelection(this, (string)content);
			}
			return null;
		}
		public override void UpdateSelections()
		{
			Selections.Clear();
			Selections.Add(new TextEditorSelection(this, txt.SelectedText, txt.SelectionStart, txt.SelectionLength));
		}
		private TextBox txt = null;

		internal override void ClearSelectedText()
		{
			txt.SelectedText = null;
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference ()
		{
			if (_er == null) {
				_er = base.MakeReference ();
				_er.SupportedObjectModels.Add (typeof (PlainTextObjectModel));
			}
			return _er;
		}

		private void txt_Changed(object sender, EventArgs e)
		{
			PlainTextObjectModel om = (this.ObjectModel as PlainTextObjectModel);

			BeginEdit();
			om.Text = txt.Text;
			EndEdit();
		}

		public PlainTextEditor ()
		{
			txt = new TextBox();	
			txt.Changed += txt_Changed;
			txt.Multiline = true;

			this.Layout = new BoxLayout(Orientation.Vertical);
			this.Controls.Add(txt, new BoxLayout.Constraints(true, true));
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			txt.Text = String.Empty;

			PlainTextObjectModel om = (this.ObjectModel as PlainTextObjectModel);
			if (om == null) return;

			txt.Text = om.Text;
		}
	}
}
