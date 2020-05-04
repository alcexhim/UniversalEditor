﻿//
//  FamilyTreeEditor.cs - provides an Editor for a FamilyTreeObjectModel
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
using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.FamilyTree
{
	/// <summary>
	/// Provides an <see cref="Editor" /> for a FamilyTreeObjectModel.
	/// </summary>
	public class FamilyTreeEditor : Editor
	{
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}
		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference ()
		{
			if (_er == null) {
				_er = base.MakeReference ();
				_er.SupportedObjectModels.Add (typeof (FamilyTreeObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged (EventArgs e)
		{
			base.OnObjectModelChanged (e);

			FamilyTreeObjectModel ftom = (ObjectModel as FamilyTreeObjectModel);
		}
	}
}