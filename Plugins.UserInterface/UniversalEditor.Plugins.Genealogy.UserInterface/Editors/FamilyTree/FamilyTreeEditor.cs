//
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
		protected override Selection CreateSelectionInternal(object content)
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

			EditorDocumentExplorerNode nodeDashboard = new EditorDocumentExplorerNode("Dashboard");
			DocumentExplorer.Nodes.Add(nodeDashboard);

			EditorDocumentExplorerNode nodePeople = new EditorDocumentExplorerNode("People");

			foreach (Person item in ftom.Persons)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(item.ToString());
				nodePeople.Nodes.Add(node);
			}

			DocumentExplorer.Nodes.Add(nodePeople);

			EditorDocumentExplorerNode nodeRelationships = new EditorDocumentExplorerNode("Relationships");
			DocumentExplorer.Nodes.Add(nodeRelationships);

			EditorDocumentExplorerNode nodeFamilies = new EditorDocumentExplorerNode("Families");
			DocumentExplorer.Nodes.Add(nodeFamilies);

			EditorDocumentExplorerNode nodeCharts = new EditorDocumentExplorerNode("Charts");
			DocumentExplorer.Nodes.Add(nodeCharts);

			EditorDocumentExplorerNode nodeEvents = new EditorDocumentExplorerNode("Events");
			foreach (Event item in ftom.Events)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(item.ToString());
				nodeEvents.Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodeEvents);

			EditorDocumentExplorerNode nodePlaces = new EditorDocumentExplorerNode("Places");
			foreach (Place item in ftom.Places)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(item.ToString());
				nodePlaces.Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodePlaces);

			EditorDocumentExplorerNode nodeGeography = new EditorDocumentExplorerNode("Geography");
			DocumentExplorer.Nodes.Add(nodeGeography);

			EditorDocumentExplorerNode nodeSources = new EditorDocumentExplorerNode("Sources");
			DocumentExplorer.Nodes.Add(nodeSources);

			EditorDocumentExplorerNode nodeCitations = new EditorDocumentExplorerNode("Citations");
			DocumentExplorer.Nodes.Add(nodeCitations);

			EditorDocumentExplorerNode nodeRepositories = new EditorDocumentExplorerNode("Repositories");
			DocumentExplorer.Nodes.Add(nodeRepositories);

			EditorDocumentExplorerNode nodeMedia = new EditorDocumentExplorerNode("Media");
			DocumentExplorer.Nodes.Add(nodeMedia);

			EditorDocumentExplorerNode nodeNotes = new EditorDocumentExplorerNode("Notes");
			DocumentExplorer.Nodes.Add(nodeNotes);
		}
	}
}
