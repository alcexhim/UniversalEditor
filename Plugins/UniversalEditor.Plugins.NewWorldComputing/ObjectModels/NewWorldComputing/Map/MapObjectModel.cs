//
//  MapObjectModel.cs - provides an ObjectModel for manipulating New World Computing (Heroes of Might and Magic II) levels (maps)
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating New World Computing (Heroes of Might and Magic II) levels (maps).
	/// </summary>
	public class MapObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Heroes of Might and Magic map file";
			}
			return _omr;
		}

		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		public MapDifficulty Difficulty { get; set; } = MapDifficulty.Easy;

		public byte Width { get; set; } = 0;
		public byte Height { get; set; } = 0;

		public MapKingdomColor AllowedKingdomColors { get; set; } = MapKingdomColor.None;
		public MapKingdomColor AllowedHumanPlayerColors { get; set; } = MapKingdomColor.None;
		public MapKingdomColor AllowedComputerPlayerColors { get; set; } = MapKingdomColor.None;

		/// <summary>
		/// Indicates the condition(s) required to win a game using this map.
		/// </summary>
		/// <value>The win conditions.</value>
		public MapWinCondition WinConditions { get; set; } = MapWinCondition.None;

		public MapWinCondition ComputerAlsoWins { get; set; } = MapWinCondition.None;
		public bool AllowNormalVictory { get; set; } = false;

		/// <summary>
		/// Indicates the condition(s) required to lose a game using this map.
		/// </summary>
		/// <value>The lose conditions.</value>
		public MapLoseCondition LoseConditions { get; set; } = MapLoseCondition.None;

		public string Name { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;

		public MapTile.MapTileCollection Tiles { get; } = new MapTile.MapTileCollection();
		public MapItem.MapItemCollection Items { get; } = new MapItem.MapItemCollection();

		public byte ObeliskCount { get; set; } = 0;
	}
}
