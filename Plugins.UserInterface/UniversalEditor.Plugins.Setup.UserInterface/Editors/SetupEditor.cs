//
//  SetupEditor.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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

namespace UniversalEditor.Plugins.Setup.UserInterface.Editors
{
	public class SetupEditor : Editor
	{
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private class SetupObjectModel : ObjectModel
		{
			public override void Clear()
			{
				//throw new NotImplementedException();
			}
			public override void CopyTo(ObjectModel where)
			{
				//throw new NotImplementedException();
			}
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = new EditorReference(typeof(SetupEditor));
				_er.SupportedObjectModels.Add(typeof(SetupObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			DocumentExplorer.Nodes.Clear();

			base.OnObjectModelChanged(e);

			EditorDocumentExplorerNode dnProjectDefinition = new EditorDocumentExplorerNode("Project Definition", MBS.Framework.StockType.Folder);
			DocumentExplorer.Nodes.Add(dnProjectDefinition);

			EditorDocumentExplorerNode dnInstallationDetails = new EditorDocumentExplorerNode("Installation Details", MBS.Framework.StockType.Folder);
			DocumentExplorer.Nodes.Add(dnInstallationDetails);

			EditorDocumentExplorerNode dnSetupAppearance = new EditorDocumentExplorerNode("Setup Appearance", MBS.Framework.StockType.Folder);
			DocumentExplorer.Nodes.Add(dnSetupAppearance);
		}
	}
}
