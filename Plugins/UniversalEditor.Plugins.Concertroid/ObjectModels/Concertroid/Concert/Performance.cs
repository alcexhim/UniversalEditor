//
//  Performance.cs - represents a single performance of a particular Song by specific Performers, optionally featuring guest Musicians
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

namespace UniversalEditor.ObjectModels.Concertroid.Concert
{
	/// <summary>
	/// Represents a single performance of a particular <see cref="Song" /> by specific <see cref="Performer"/>s, optionally featuring guest
	/// <see cref="Musician" />s.
	/// </summary>
	public class Performance
	{
		public class PerformanceCollection
			: System.Collections.ObjectModel.Collection<Performance>
		{
		}

		/// <summary>
		/// Gets or sets the title of the <see cref="Performance" />. This is usually the same as the title of the associated <see cref="Song" />.
		/// </summary>
		/// <value>The title of the <see cref="Performance" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the <see cref="Song" /> performed in this <see cref="Performance" />.
		/// </summary>
		/// <value>The <see cref="Song" /> performed in this <see cref="Performance" />.</value>
		public Song Song { get; set; } = null;
		/// <summary>
		/// Gets a collection of <see cref="Performer" /> instances representing the performers (characters in specific costumes) onstage during the
		/// <see cref="Performance" />.
		/// </summary>
		/// <value>The <see cref="Performer" />s onstage during the <see cref="Performance" />.</value>
		public Performer.PerformerCollection Performers { get; } = new Performer.PerformerCollection();
		/// <summary>
		/// Gets a collection of <see cref="Musician" /> instances representing the additional (non-band member) musicians onstage during the
		/// <see cref="Performance" />.
		/// </summary>
		/// <value>The additional (non-band member) musicians onstage during the <see cref="Performance" />.</value>
		public Musician.MusicianCollection GuestMusicians { get; } = new Musician.MusicianCollection();
	}
}
