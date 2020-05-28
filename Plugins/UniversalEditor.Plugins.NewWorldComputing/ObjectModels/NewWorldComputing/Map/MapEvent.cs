//
//  MapEvent.cs - represents an event which occurs on the map
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
	public class MapEvent
	{
		public class MapEventCollection
			: System.Collections.ObjectModel.Collection<MapEvent>
		{

		}

		// resource counts
		/// <summary>
		/// Gets or sets the amount of Wood resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Wood resource that should be given to or taken from the player.</value>
		public int AmountWood { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Mercury resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Mercury resource that should be given to or taken from the player.</value>
		public int AmountMercury { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Ore resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Ore resource that should be given to or taken from the player.</value>
		public int AmountOre { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Sulfur resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Sulfur resource that should be given to or taken from the player.</value>
		public int AmountSulfur { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Crystal resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Crystal resource that should be given to or taken from the player.</value>
		public int AmountCrystal { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Gems resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Gems resource that should be given to or taken from the player.</value>
		public int AmountGems { get; set; } = 0;
		// resource counts
		/// <summary>
		/// Gets or sets the amount of Gold resource that should be given to or taken from the player.
		/// </summary>
		/// <value>The amount of Gold resource that should be given to or taken from the player.</value>
		public int AmountGold { get; set; } = 0;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MapEvent"/> is available to be triggered by a non-player (i.e., computer) character.
		/// </summary>
		/// <value><c>true</c> if computer players are allowed to trigger the event; otherwise, <c>false</c>.</value>
		public bool AllowComputer { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MapEvent" /> triggers only once.
		/// </summary>
		/// <value><c>true</c> if this <see cref="MapEvent" /> triggers only once; otherwise, <c>false</c>.</value>
		public bool SingleVisit { get; set; } = false;

		/// <summary>
		/// Gets or sets a <see cref="MapKingdomColor" /> mask indicating the kingdom alliances which are allowed to trigger this event.
		/// </summary>
		/// <value>The kingdom alliances which are allowed to trigger this event.</value>
		public MapKingdomColor AllowedKingdoms { get; set; } = MapKingdomColor.All;

		/// <summary>
		/// Gets or sets the message displayed to the player upon triggering this event.
		/// </summary>
		/// <value>The message displayed to the player upon triggering this event.</value>
		public string Message { get; set; } = null;
	}
}
