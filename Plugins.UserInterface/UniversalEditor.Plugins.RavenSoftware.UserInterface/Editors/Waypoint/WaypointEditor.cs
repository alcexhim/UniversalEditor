//
//  WaypointEditor.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Waypoint;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Waypoint
{
	[ContainerLayout("~/Editors/RavenSoftware/Waypoint/WaypointEditor.glade")]
	public class WaypointEditor : Editor
	{
		private ListViewControl tvWaypoints;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Waypoint Editor";
				_er.SupportedObjectModels.Add(typeof(WaypointObjectModel));
			}
			return _er;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated)
				return;

			tvWaypoints.Model.Rows.Clear();

			WaypointObjectModel waypoint = (ObjectModel as WaypointObjectModel);
			if (waypoint == null)
				return;

			foreach (WaypointEntry entry in waypoint.Entries)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvWaypoints.Model.Columns[0], entry.Index),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[1], entry.Type.ToString()),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[2], entry.A),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[3], entry.X),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[4], entry.Y),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[5], entry.Z),
					// new TreeModelRowColumn(tvWaypoints.Model.Columns[6], entry.Liststr),
					new TreeModelRowColumn(tvWaypoints.Model.Columns[7], entry.Q)
				});

				row.SetExtraData<WaypointEntry>("entry", entry);
				tvWaypoints.Model.Rows.Add(row);
			}
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}
	}
}
