//
//  StoryEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls;
using UniversalEditor.ObjectModels.StoryWriter.Story;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.StoryWriter.UserInterface.Editors.Story
{
	[ContainerLayout("~/Extensions/StoryWriter/Editors/Story/StoryEditor.glade")]
	public class StoryEditor : Editor
	{
		private static EditorReference _er = null;

		private TextBox txt;

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(StoryObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated)
				return;

			txt.Text = "The two hackers sat down at the picnic table. Crystal smirked at Zero.\n\n\"It's gonna be good,\" she promised.";

			DocumentExplorer.Nodes.Add("Story");

			EditorDocumentExplorerNode nodeCharacters = new EditorDocumentExplorerNode("Characters");
			EditorDocumentExplorerNode nodeProtagonists = new EditorDocumentExplorerNode("Protagonists / Heroes");
			nodeProtagonists.Nodes.Add("Laurynne Westerfield");
			nodeProtagonists.Nodes.Add("Jarelyn Costas");
			nodeProtagonists.Nodes.Add("Ian Hunt");

			nodeCharacters.Nodes.Add(nodeProtagonists);

			EditorDocumentExplorerNode nodeAntagonists = new EditorDocumentExplorerNode("Antagonists / Villains");
			nodeAntagonists.Nodes.Add("Crystal Blackheart");
			nodeAntagonists.Nodes.Add("Russell 'Zero Hour' Wade");
			nodeCharacters.Nodes.Add(nodeAntagonists);

			nodeCharacters.Nodes.Add("Businesses / Companies / Organizations");
			DocumentExplorer.Nodes.Add(nodeCharacters);

			EditorDocumentExplorerNode nodeItems = new EditorDocumentExplorerNode("Items");
			nodeItems.Nodes.Add("Vehicles");
			nodeItems.Nodes.Add("Weapons");
			DocumentExplorer.Nodes.Add(nodeItems);

			TextBoxStyleDefinition chara = new TextBoxStyleDefinition("chara");
			chara.ForegroundColor = Color.Parse("#009695");
			chara.Editable = false;
			txt.StyleDefinitions.Add(chara);

			TextBoxStyleDefinition dialogue = new TextBoxStyleDefinition("dialogue");
			dialogue.ForegroundColor = Color.Parse("#db7100");
			txt.StyleDefinitions.Add(dialogue);

			txt.StyleAreas.Add(new TextBoxStyleArea(txt.StyleDefinitions[0], 46, 7));
			txt.StyleAreas.Add(new TextBoxStyleArea(txt.StyleDefinitions[0], 65, 4));
			txt.StyleAreas.Add(new TextBoxStyleArea(txt.StyleDefinitions[1], 72, 35));
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(EventArgs.Empty);
		}
	}
}
