//
//  SyntaxEditor.cs - provides a UWT-based Editor for text-based editing of a CodeObjectModel
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
using UniversalEditor.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.SourceCode;
using MBS.Framework.UserInterface;

namespace UniversalEditor.Plugins.SoftwareDevelopment.UserInterface.Editors.Syntax
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for text-based editing of a <see cref="CodeObjectModel" />.
	/// </summary>
	public class SyntaxEditor : Editor
	{
		SyntaxTextBox txt = null;

		public SyntaxEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(CodeObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
		}

		private void InitializeComponent()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			txt = new SyntaxTextBox();
			txt.Changed += txt_Changed;
			Controls.Add(txt, new BoxLayout.Constraints(true, true));
		}

		void txt_Changed(object sender, EventArgs e)
		{
			BeginEdit();

			// FIXME: implement something like this...
			// CodeObjectModel code = (ObjectModel as CodeObjectModel);
			// code.Append(txt.Text, CurrentLanguage);

			EndEdit();
		}


		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}
