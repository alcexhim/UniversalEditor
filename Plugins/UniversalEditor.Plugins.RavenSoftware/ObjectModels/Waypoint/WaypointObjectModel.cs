//
//  WaypointObjectModel.cs
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
namespace UniversalEditor.ObjectModels.Waypoint
{
	// FIXME:	this should be moved to a parent Quake plugin or something,
	// 			as it also relates to wpeditor waypoints in xonotic, etc.
	public class WaypointObjectModel : ObjectModel
	{
		/// <summary>
		/// Gets a collection of <see cref="WaypointEntry" /> instances that
		/// represent the waypoints defined in this botroutes file.
		/// </summary>
		/// <value>The collection of <see cref="WaypointEntry" /> instances.</value>
		public WaypointEntry.WaypointEntryCollection Entries { get; } = new WaypointEntry.WaypointEntryCollection();

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Game development", "Bot waypoint route" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Entries.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			WaypointObjectModel clone = (where as WaypointObjectModel);
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			foreach (WaypointEntry entry in Entries)
			{
				clone.Entries.Add(entry.Clone() as WaypointEntry);
			}
		}
	}
}
