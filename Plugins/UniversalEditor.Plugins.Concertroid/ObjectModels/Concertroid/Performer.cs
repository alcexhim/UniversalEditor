//
//  Performer.cs - represents a tuple containing Character, Costume, and Animation information for a particular Concertroid Performance
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
	/// Represents a tuple containing <see cref="Character" />, <see cref="Costume" />, and <see cref="Animation" /> information for a particular
	/// Concertroid <see cref="Concert.Performance" />.
	/// </summary>
	public class Performer
	{
		public class PerformerCollection
			: System.Collections.ObjectModel.Collection<Performer>
		{
		}
		/// <summary>
		/// Gets or sets the globally-unique identifier (GUID) for this <see cref="Performer" />.
		/// </summary>
		/// <value>The globally-unique identifier (GUID) for this <see cref="Performer" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the <see cref="Character" /> for this <see cref="Performer" />.
		/// </summary>
		/// <value>The <see cref="Character" /> for this <see cref="Performer" />.</value>
		public Character Character { get; set; } = null;
		/// <summary>
		/// Gets or sets the <see cref="Costume" /> worn by this <see cref="Performer" />.
		/// </summary>
		/// <value>The <see cref="Costume" /> worn by this <see cref="Performer" />.</value>
		public Costume Costume { get; set; } = null;
		/// <summary>
		/// Gets or sets the <see cref="Animation" /> played by this <see cref="Performer" /> during the performance.
		/// </summary>
		/// <value>The <see cref="Animation" /> played by this <see cref="Performer" /> during the performance.</value>
		public Animation Animation { get; set; } = null;
		/// <summary>
		/// Gets or sets the offset of the 3D model associated with this <see cref="Performer" /> onstage.
		/// </summary>
		/// <value>The offset of the 3D model associated with this <see cref="Performer" /> onstage.</value>
		public PositionVector3 Offset { get; set; } = new PositionVector3();
	}
}
