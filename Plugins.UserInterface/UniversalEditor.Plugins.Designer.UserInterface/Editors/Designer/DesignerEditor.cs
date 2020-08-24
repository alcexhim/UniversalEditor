//
//  DesignerEditor.cs - provides a UWT-based Editor for manipulating component designer layouts
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
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.Designer;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Designer.UserInterface.Editors.Designer
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for manipulating component designer layouts.
	/// </summary>
	[ContainerLayout("~/Editors/Designer/DesignerEditor.glade")]
	public class DesignerEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(DesignerObjectModel));
			}
			return _er;
		}

		private Controls.DesignerControl designer;

		public override void UpdateSelections()
		{
			throw new System.NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new System.NotImplementedException();
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

			DesignerObjectModel dsn = (ObjectModel as DesignerObjectModel);
			designer.ObjectModel = dsn;
		}
	}
}
