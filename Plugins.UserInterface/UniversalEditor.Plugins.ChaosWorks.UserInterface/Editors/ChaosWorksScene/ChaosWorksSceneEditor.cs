//
//  ChaosWorksSceneEditor.cs -
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.ChaosWorks;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.Plugins.ChaosWorks.DataFormats.Multimedia.PictureCollection;
using UniversalEditor.Plugins.ChaosWorks.ObjectModels.ChaosWorksScene;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.ChaosWorks.UserInterface.Editors.ChaosWorksScene
{
	public class ChaosWorksSceneEditor : Editor
	{
		private LevelRenderer levelRenderer = null;
		public ChaosWorksSceneEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			levelRenderer = new LevelRenderer(this);
			Controls.Add(levelRenderer, new BoxLayout.Constraints(true, true));
		}

		private static ChaosWorksVOLDataFormat voldf = new ChaosWorksVOLDataFormat();
		private System.Collections.Generic.Dictionary<string, PictureCollectionObjectModel> _sprites = new System.Collections.Generic.Dictionary<string, PictureCollectionObjectModel>();
		private void InitializeSprites()
		{
			_sprites.Clear();

			FileSystemObjectModel fsom = new FileSystemObjectModel();
			string path = MBS.Framework.Application.Instance.GetSetting<string>(new Guid("{25e4e8be-bee2-47ed-bc4c-5d3bb3d5aeae}"));
			string[] volfiles = System.IO.Directory.GetFiles(path, "*.vol");
			foreach (string volfile in volfiles)
			{
				FileSystemObjectModel fsom1 = new FileSystemObjectModel();
				Document.Load(fsom1, voldf, new FileAccessor(volfile));

				fsom1.CopyTo(fsom);
				// ff84f01a-3269-4038-88d8-f9680c8b1b98
			}

			ChaosWorksSceneObjectModel scene = (ObjectModel as ChaosWorksSceneObjectModel);
			string groupName = "BROWN";
			for (int i = 0; i < scene.Sprites.Count; i++)
			{
				File f = fsom.GetFileByLabel(String.Format("{0}/{1}/HIRES", groupName.ToUpper(), scene.Sprites[i].SpriteName.ToUpper()));
				if (f != null)
				{
					PictureCollectionObjectModel coll = new PictureCollectionObjectModel();
					Document.Load(coll, spldf, new EmbeddedFileAccessor(f));
					_sprites[scene.Sprites[i].SpriteName] = coll;
				}
			}
		}

		private static CWESpriteDataFormat spldf = new CWESpriteDataFormat();
		public PictureCollectionObjectModel LoadFFSprite(string name)
		{
			if (_sprites.ContainsKey(name))
			{
				return _sprites[name];
			}
			return null;
		}
		private string GetDefaultProjectPath()
		{
			return MBS.Framework.Application.Instance.GetSetting<string>(new Guid("{25e4e8be-bee2-47ed-bc4c-5d3bb3d5aeae}"));
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = new EditorReference(GetType());
				_er.SupportedObjectModels.Add(typeof(ChaosWorksSceneObjectModel));
			}
			return _er;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			string searchPath = GetDefaultProjectPath();
			if (searchPath == null)
			{
				MBS.Framework.UserInterface.Dialogs.MessageDialog.ShowDialog("Please specify default project path in settings", "Error", MBS.Framework.UserInterface.Dialogs.MessageDialogButtons.OK, MBS.Framework.UserInterface.Dialogs.MessageDialogIcon.Error);
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			ChaosWorksSceneObjectModel scene = (ObjectModel as ChaosWorksSceneObjectModel);
			if (scene == null)
				return;

			InitializeSprites();

			EditorDocumentExplorerNode nodeTypes = new EditorDocumentExplorerNode("Types");
			for (int i = 0; i < scene.Types.Count; i++)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(String.Format("{0} : {1} {2} {3}", scene.Types[i].Name, scene.Types[i].Flags1, scene.Types[i].Flags2, scene.Types[i].Flags3));
				node.SetExtraData<ChaosWorksSceneType>("type", scene.Types[i]);
				nodeTypes.Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodeTypes);

			EditorDocumentExplorerNode nodeSprites = new EditorDocumentExplorerNode("Sprites");
			for (int i = 0; i < scene.Sprites.Count; i++)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(String.Format("{0} : {1}", scene.Sprites[i].SpriteName, scene.Sprites[i].TypeName));
				node.SetExtraData<ChaosWorksSceneSprite>("sprite", scene.Sprites[i]);
				nodeSprites.Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodeSprites);

			EditorDocumentExplorerNode nodeLevels = new EditorDocumentExplorerNode("Levels");
			for (int i = 0; i < scene.Levels.Count; i++)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(String.Format("Level {0}", i + 1));
				node.SetExtraData<ChaosWorksSceneLevel>("level", scene.Levels[i]);
				nodeLevels.Nodes.Add(node);
			}
			DocumentExplorer.Nodes.Add(nodeLevels);
		}

		protected override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			if (e.Node != null)
			{
				ChaosWorksSceneLevel level = e.Node.GetExtraData<ChaosWorksSceneLevel>("level");
				if (level != null)
				{
					levelRenderer.Level = level;
				}
			}
		}
	}
}
