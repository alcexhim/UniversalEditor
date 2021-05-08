//
//  EventFragment.cs
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.WorldMap2D.NewWorldComputing.Heroes2
{
	public class EventFragment : DataFormatFragment<MapEvent>
	{
		protected override MapEvent ReadInternal(Reader reader)
		{
			MapEvent evt = new MapEvent();
			evt.AmountWood = reader.ReadInt32();
			evt.AmountMercury = reader.ReadInt32();
			evt.AmountOre = reader.ReadInt32();
			evt.AmountSulfur = reader.ReadInt32();
			evt.AmountCrystal = reader.ReadInt32();
			evt.AmountGems = reader.ReadInt32();
			evt.AmountGold = reader.ReadInt32();

			short nArtifact = reader.ReadInt16();
			// evt.Artifact = (MapArtifact)nArtifact;

			evt.AllowComputer = reader.ReadBoolean();
			evt.SingleVisit = reader.ReadBoolean();

			byte[] unknown = reader.ReadBytes(10);

			bool colorBlue = reader.ReadBoolean();
			if (colorBlue) evt.AllowedKingdoms |= MapKingdomColor.Blue;
			bool colorGreen = reader.ReadBoolean();
			if (colorGreen) evt.AllowedKingdoms |= MapKingdomColor.Green;
			bool colorRed = reader.ReadBoolean();
			if (colorRed) evt.AllowedKingdoms |= MapKingdomColor.Red;
			bool colorYellow = reader.ReadBoolean();
			if (colorYellow) evt.AllowedKingdoms |= MapKingdomColor.Yellow;
			bool colorOrange = reader.ReadBoolean();
			if (colorOrange) evt.AllowedKingdoms |= MapKingdomColor.Orange;
			bool colorPurple = reader.ReadBoolean();
			if (colorPurple) evt.AllowedKingdoms |= MapKingdomColor.Purple;

			evt.Message = reader.ReadNullTerminatedString();
			return evt;
		}
		protected override void WriteInternal(Writer writer, MapEvent value)
		{
			writer.WriteInt32(value.AmountWood);
			writer.WriteInt32(value.AmountMercury);
			writer.WriteInt32(value.AmountOre);
			writer.WriteInt32(value.AmountSulfur);
			writer.WriteInt32(value.AmountCrystal);
			writer.WriteInt32(value.AmountGems);
			writer.WriteInt32(value.AmountGold);

			short nArtifact = 0;
			writer.WriteInt16(nArtifact);
			// evt.Artifact = (MapArtifact)nArtifact;

			writer.WriteBoolean(value.AllowComputer);
			writer.WriteBoolean(value.SingleVisit);

			writer.WriteBytes(new byte[10]);

			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Blue) == MapKingdomColor.Blue);
			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Green) == MapKingdomColor.Green);
			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Red) == MapKingdomColor.Red);
			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Yellow) == MapKingdomColor.Yellow);
			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Orange) == MapKingdomColor.Orange);
			writer.WriteBoolean((value.AllowedKingdoms & MapKingdomColor.Purple) == MapKingdomColor.Purple);

			writer.WriteNullTerminatedString(value.Message);
		}
	}
}
