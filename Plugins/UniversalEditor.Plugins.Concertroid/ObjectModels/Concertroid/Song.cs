//
//  Song.cs - represents a song performed as part of a Concertroid Performance
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

namespace UniversalEditor.ObjectModels.Concertroid
{
	/// <summary>
	/// Represents a song performed as part of a Concertroid <see cref="Concert.Performance"/>.
	/// </summary>
	public class Song
	{
		public class SongCollection
			: System.Collections.ObjectModel.Collection<Song>
		{
		}

		/// <summary>
		/// Gets or sets the title of the <see cref="Song" />.
		/// </summary>
		/// <value>The title of the <see cref="Song" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the full path to the audio file to play during this <see cref="Song" />.
		/// </summary>
		/// <value>The full path to the audio file to play during this <see cref="Song" />.</value>
		public string AudioFileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="SongProducer" /> instances representing the producers involved in the creation of this <see cref="Song" />
		/// and the statuses of the associated licensing permissions.
		/// </summary>
		/// <value>The producers involved in the creation of this <see cref="Song" /> and the statuses of the associated licensing permissions.</value>
		public SongProducer.SongProducerCollection Producers { get; } = new SongProducer.SongProducerCollection();
		/// <summary>
		/// Gets or sets the millisecond delay between when this <see cref="Song" /> is played and when the animation frame is rendered.
		/// </summary>
		/// <value>The millisecond delay between when this <see cref="Song" /> is played and when the animation frame is rendered.</value>
		public int Delay { get; set; } = 0;
		/// <summary>
		/// Gets or sets the tempo, in beats-per-minute, used to synchronize the audio and animation data for this <see cref="Song" />.
		/// </summary>
		/// <value>The tempo, in beats-per-minute, used to synchronize the audio and animation data for this <see cref="Song" />.</value>
		public decimal Tempo { get; set; } = 120.0M;
	}
}
