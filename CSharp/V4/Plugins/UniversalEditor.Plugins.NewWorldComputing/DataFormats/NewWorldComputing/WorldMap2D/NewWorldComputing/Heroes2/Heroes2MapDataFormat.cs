using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.Gaming.WorldMap2D.NewWorldComputing.Heroes2
{
    public class Heroes2MapDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(MapObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }
        
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            MapObjectModel map = (objectModel as MapObjectModel);
            if (map == null) return;


            IO.Reader br = base.Accessor.Reader;
            uint magic = br.ReadUInt32();
            if (magic != 0x0000005C) throw new InvalidDataFormatException();

            map.Difficulty = (MapDifficulty)br.ReadUInt16();
            map.Width = br.ReadByte();
            map.Height = br.ReadByte();

            #region Allow kingdom colors
            {
                bool colorBlue = br.ReadBoolean();
                if (colorBlue) map.AllowedKingdomColors |= MapKingdomColor.Blue;

                bool colorGreen = br.ReadBoolean();
                if (colorGreen) map.AllowedKingdomColors |= MapKingdomColor.Green;

                bool colorRed = br.ReadBoolean();
                if (colorRed) map.AllowedKingdomColors |= MapKingdomColor.Red;

                bool colorYellow = br.ReadBoolean();
                if (colorYellow) map.AllowedKingdomColors |= MapKingdomColor.Yellow;

                bool colorOrange = br.ReadBoolean();
                if (colorOrange) map.AllowedKingdomColors |= MapKingdomColor.Orange;

                bool colorPurple = br.ReadBoolean();
                if (colorPurple) map.AllowedKingdomColors |= MapKingdomColor.Purple;
            }
            #endregion
            #region Allow player colors
            {
                bool colorBlue = br.ReadBoolean();
                if (colorBlue) map.AllowedHumanPlayerColors |= MapKingdomColor.Blue;

                bool colorGreen = br.ReadBoolean();
                if (colorGreen) map.AllowedHumanPlayerColors |= MapKingdomColor.Green;

                bool colorRed = br.ReadBoolean();
                if (colorRed) map.AllowedHumanPlayerColors |= MapKingdomColor.Red;

                bool colorYellow = br.ReadBoolean();
                if (colorYellow) map.AllowedHumanPlayerColors |= MapKingdomColor.Yellow;

                bool colorOrange = br.ReadBoolean();
                if (colorOrange) map.AllowedHumanPlayerColors |= MapKingdomColor.Orange;

                bool colorPurple = br.ReadBoolean();
                if (colorPurple) map.AllowedHumanPlayerColors |= MapKingdomColor.Purple;
            }
            #endregion
            #region Allow AI colors
            {
                bool colorBlue = br.ReadBoolean();
                if (colorBlue) map.AllowedComputerPlayerColors |= MapKingdomColor.Blue;

                bool colorGreen = br.ReadBoolean();
                if (colorGreen) map.AllowedComputerPlayerColors |= MapKingdomColor.Green;

                bool colorRed = br.ReadBoolean();
                if (colorRed) map.AllowedComputerPlayerColors |= MapKingdomColor.Red;

                bool colorYellow = br.ReadBoolean();
                if (colorYellow) map.AllowedComputerPlayerColors |= MapKingdomColor.Yellow;

                bool colorOrange = br.ReadBoolean();
                if (colorOrange) map.AllowedComputerPlayerColors |= MapKingdomColor.Orange;

                bool colorPurple = br.ReadBoolean();
                if (colorPurple) map.AllowedComputerPlayerColors |= MapKingdomColor.Purple;
            }
            #endregion

            // kingdom count
            br.Accessor.Seek(0x1A, IO.SeekOrigin.Begin);
            byte kingdomCount = br.ReadByte();

            br.Accessor.Seek(0x1D, IO.SeekOrigin.Begin);
            map.WinConditions = (MapWinCondition)br.ReadByte();
            
            byte wins1 = br.ReadByte();
            byte wins2 = br.ReadByte();
            ushort wins3 = br.ReadUInt16();
            br.Accessor.Seek(0x2C, IO.SeekOrigin.Begin);
            ushort wins4 = br.ReadUInt16();

            br.Accessor.Seek(0x22, IO.SeekOrigin.Begin);
            map.LoseConditions = (MapLoseCondition)br.ReadByte();

            ushort loss1 = br.ReadUInt16();
            br.Accessor.Seek(0x2E, IO.SeekOrigin.Begin);
            ushort loss2 = br.ReadUInt16();

            // starting hero
            br.Accessor.Seek(0x25, IO.SeekOrigin.Begin);
            byte startingHero = br.ReadByte();
            bool withHeroes = (startingHero == 0);

            byte[] races = br.ReadBytes(5);

            // name 
            br.Accessor.Seek(0x3A, IO.SeekOrigin.Begin);
            map.Name = br.ReadFixedLengthString(16);
            map.Name = map.Name.TrimNull();
            // name 
            br.Accessor.Seek(0x76, IO.SeekOrigin.Begin);
            map.Description = br.ReadFixedLengthString(143);
            map.Description = map.Description.TrimNull();

            byte[] unknown = br.ReadBytes(157);

            ushort unknown2 = br.ReadUInt16();

            // 33044 bytes between here and there - tiles (36 * 36 * 25 bytes per tile)

            long pos = br.Accessor.Position;
            br.Accessor.Position = pos;

            for (ushort i = 0; i < unknown2; i++)
            {
                MapTile tile = ReadTile(br);
            }

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    MapTile tile = ReadTile(br);
                    map.Tiles.Add(tile);
                }
            }

            #region Castle
            for (byte i = 0; i < kingdomCount; i++)
            {
                MapCastle castle = ReadCastle(br);
            }
            #endregion
        }

        private MapTile ReadTile(IO.Reader br)
        {
            MapTile tile = new MapTile();
            tile.GroundType = (MapGroundType)br.ReadUInt16();		// tile (ocean, grass, snow, swamp, lava, desert, dirt, wasteland, beach)
            byte objectName1 = br.ReadByte();	    // level 1.0
            byte indexName1 = br.ReadByte();	    // index level 1.0 or 0xFF
            byte quantity1 = br.ReadByte();		    // count
            byte quantity2 = br.ReadByte();		    // count
            byte objectName2 = br.ReadByte();	    // level 2.0
            byte indexName2 = br.ReadByte();	    // index level 2.0 or 0xFF
            byte shape = br.ReadByte();		        // shape reflect % 4, 0 none, 1 vertical, 2 horizontal, 3 any
            byte generalObject = br.ReadByte();     // zero or object
            ushort indexAddon = br.ReadUInt16();    // zero or index addons_t
            uint uniqNumber1 = br.ReadUInt32();     // level 1.0
            uint uniqNumber2 = br.ReadUInt32();     // level 2.0

            byte[] unknown = br.ReadBytes(5);
            // 259229
            return tile;

            ushort indexAddonN = br.ReadUInt16();	// zero or next addons_t
            byte objectNameN1 = br.ReadByte();	// level 1.N
            byte indexNameN1 = br.ReadByte();	// level 1.N or 0xFF
            byte quantityN = br.ReadByte();	//
            byte objectNameN2 = br.ReadByte();	// level 2.N
            byte indexNameN2 = br.ReadByte();	// level 1.N or 0xFF
            uint uniqNumberN1 = br.ReadUInt32();	// level 1.N
            uint uniqNumberN2 = br.ReadUInt32();	// level 2.N

            return tile;
        }

        private MapCastle ReadCastle(IO.Reader br)
        {
            MapCastle castle = new MapCastle();
            
            ushort signal = br.ReadUInt16();

            castle.Color = (MapCastleColor)br.ReadByte();
            castle.HasCustomBuilding = br.ReadBoolean();
            castle.Buildings = (MapBuildingType)br.ReadUInt16();
            castle.Dwellings = (MapDwellingType)br.ReadUInt16();
            castle.MageGuildLevel = (MapMageGuildLevel)br.ReadByte();
            castle.HasCustomTroops = br.ReadBoolean();

            for (int i = 0; i < 5; i++)
            {
                castle.Monsters[i] = new MapArmyMonster();
                castle.Monsters[i].MonsterType = (MapMonsterType)br.ReadByte();
            }
            for (int i = 0; i < 5; i++)
            {
                castle.Monsters[i].Amount = br.ReadUInt16();
            }

            castle.HasCaptain = br.ReadBoolean();
            castle.HasCustomName = br.ReadBoolean();
            castle.Name = br.ReadFixedLengthString(13);
            castle.Name = castle.Name.TrimNull();
            castle.Type = (MapCastleType)br.ReadByte();
            castle.IsCastle = br.ReadBoolean();
            castle.IsUpgradable = br.ReadBoolean(); // 00 TRUE, 01 FALSE

            byte[] unknown = br.ReadBytes(29);

            return castle;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
