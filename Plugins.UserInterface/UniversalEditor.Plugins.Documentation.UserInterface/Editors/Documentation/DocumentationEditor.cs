//
//  DocumentationEditor.cs - provides a UWT-based Editor for manipulating documentation files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;

using UniversalEditor.ObjectModels.Database;
using UniversalEditor.ObjectModels.Help.Compiled;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Documentation
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for manipulating database files.
	/// </summary>
	[ContainerLayout("~/Editors/Documentation/DocumentationEditor.glade")]
	public class DocumentationEditor : Editor
	{
		private TextBox txt;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(CompiledHelpObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(EventArgs.Empty);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			CompiledHelpObjectModel help = (ObjectModel as CompiledHelpObjectModel);

		}
	}
}
