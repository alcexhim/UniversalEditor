//
//  Event.cs - represents a timed event in a subtitle file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
	/// <summary>
	/// Represents a timed event in a subtitle file.
	/// </summary>
	public class Event : ICloneable
	{
		public class EventCollection
			: System.Collections.ObjectModel.Collection<Event>
		{
		}

		public Actor Actor { get; set; } = null;
		public Style Style { get; set; } = null;
		public DateTime StartTimestamp { get; set; } = new DateTime();
		public DateTime EndTimestamp { get; set; } = new DateTime();
		public PositionVector2 Position { get; set; } = PositionVector2.Empty;
		public string Text { get; set; } = String.Empty;

		public object Clone()
		{
			Event clone = new Event();
			clone.Actor = Actor;
			clone.EndTimestamp = EndTimestamp;
			clone.Position = (PositionVector2)(Position.Clone());
			clone.StartTimestamp = StartTimestamp;
			clone.Style = Style;
			clone.Text = (Text.Clone() as string);
			return clone;
		}
	}
}
