//
//  MyClass.cs
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
using MBS.Framework;
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.NewWorldComputing.UserInterface.Editors.NewWorldComputing.Map
{
	[ContainerLayout("~/Editors/NewWorldComputing/Map/MapEditor.glade")]
	public class MapEditor : Editor
	{
		private Controls.MapViewControl mapView;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(MapObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
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

			MapObjectModel map = (ObjectModel as MapObjectModel);
			if (map == null) return;

			mapView.Refresh();
		}

		private MapDocumentPropertiesSettingsProvider _mpds = null;
		public MapEditor()
		{
			_mpds = new MapDocumentPropertiesSettingsProvider(this);
		}

		protected override SettingsProvider[] GetDocumentPropertiesSettingsProvidersInternal()
		{
			return new SettingsProvider[] { _mpds };
		}

	}
}
