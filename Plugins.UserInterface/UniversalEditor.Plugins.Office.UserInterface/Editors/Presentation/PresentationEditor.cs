//
//  PresentationEditor.cs
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
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Plugins.Office.ObjectModels.Presentation;
using UniversalEditor.Plugins.Office.UserInterface.Editors.Presentation.Controls;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Office.UserInterface.Editors.Presentation
{
	public class PresentationEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PresentationObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DocumentExplorer.Nodes.Clear();

			PresentationObjectModel pres = (ObjectModel as PresentationObjectModel);
			if (pres == null) return;

			if (pres.Slides.Count == 0)
				pres.Slides.Add(new Slide());

			EditorDocumentExplorerNode nodeSlides = DocumentExplorer.Nodes.Add("Slides");
			for (int i = 0; i < pres.Slides.Count; i++)
			{
				EditorDocumentExplorerNode nodeSlide = new EditorDocumentExplorerNode(String.Format("Slide {0}", (i + 1).ToString()));
				nodeSlides.Nodes.Add(nodeSlide);
			}
		}

		public PresentationEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			Controls.Add(new PresentationSlideControl(), new BoxLayout.Constraints(true, true));
		}
	}
}
