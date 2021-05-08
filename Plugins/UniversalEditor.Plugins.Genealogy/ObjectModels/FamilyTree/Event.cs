//
//  Event.cs
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
namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree
{
	public class Event : CitableDatabaseObject
	{
		public string Type { get; set; } = null;
		public DateReference Date { get; set; } = null;
		public Place Place { get; set; } = null;
		public string Description { get; set; } = null;

		public class EventCollection
			: System.Collections.ObjectModel.Collection<Event>
		{

		}

		public Event()
		{
		}

		protected override DatabaseObject CloneInternal()
		{
			Event clone = new Event();
			CloneTo(clone);
			clone.Type = Type?.Clone() as string;
			clone.Date = Date;
			clone.Place = Place;
			return clone;
		}
	}
}
