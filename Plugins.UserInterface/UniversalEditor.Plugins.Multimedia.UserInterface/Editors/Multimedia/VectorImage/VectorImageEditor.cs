//
//  VectorImageEditor.cs
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
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.VectorImage
{
	[ContainerLayout("~/Editors/Multimedia/VectorImage/VectorImageEditor.glade")]
	public class VectorImageEditor : Editor
	{
		private Controls.VectorImageControl veced;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(VectorImageObjectModel));
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

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			VectorImageObjectModel vec = (ObjectModel as VectorImageObjectModel);
			if (vec == null) return;

			veced.Refresh();
		}
	}
}
