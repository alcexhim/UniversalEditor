//
//  SongProducer.cs - indicates the producer and permission status for a Song in a Concertroid Performance
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

namespace UniversalEditor.ObjectModels.Concertroid
{
	/// <summary>
	/// Indicates the producer and permission status for a <see cref="Song" /> in a Concertroid <see cref="Concert.Performance" />.
	/// </summary>
	public class SongProducer
	{
		public class SongProducerCollection
			: System.Collections.ObjectModel.Collection<SongProducer>
		{
		}
		/// <summary>
		/// Gets or sets the permission status that the associated <see cref="Producer" /> has granted for the song.
		/// </summary>
		/// <value>The permission status that the associated <see cref="Producer" /> has granted for the song.</value>
		public PermissionStatus PermissionStatus { get; set; } = PermissionStatus.Unknown;
		/// <summary>
		/// Gets or sets the producer for the song.
		/// </summary>
		/// <value>The producer for the song.</value>
		public Producer Producer { get; set; } = null;
	}
}
