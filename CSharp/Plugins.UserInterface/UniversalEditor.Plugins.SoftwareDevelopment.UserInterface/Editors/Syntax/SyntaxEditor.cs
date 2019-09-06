//
//  SyntaxEditor.cs
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
using UniversalEditor.UserInterface;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Layouts;
using UniversalEditor.ObjectModels.SourceCode;
using UniversalWidgetToolkit;

namespace UniversalEditor.Plugins.SoftwareDevelopment.UserInterface.Editors.Syntax
{
	public class SyntaxEditor : Editor
	{
		SyntaxTextBox txt = null;

		public SyntaxEditor ()
		{
			InitializeComponent ();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference ()
		{
			if (_er == null) {
				_er = base.MakeReference ();
				_er.SupportedObjectModels.Add (typeof(CodeObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged (EventArgs e)
		{
			base.OnObjectModelChanged (e);
		}

		private void InitializeComponent()
		{
			Layout = new BoxLayout (Orientation.Vertical);

			txt = new SyntaxTextBox ();
			Controls.Add (txt, new BoxLayout.Constraints (true, true));
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}

