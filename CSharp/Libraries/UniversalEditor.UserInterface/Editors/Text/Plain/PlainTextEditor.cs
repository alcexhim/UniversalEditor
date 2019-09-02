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

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.Editors.Text.Plain
{
	public class PlainTextEditor : Editor
	{
		public override void Copy()
		{
			throw new NotImplementedException();
		}

		public override void Delete()
		{
			throw new NotImplementedException();
		}

		public override void Paste()
		{
			throw new NotImplementedException();
		}

		private TextBox txt = null;

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
			om.Text = txt.Text;
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
