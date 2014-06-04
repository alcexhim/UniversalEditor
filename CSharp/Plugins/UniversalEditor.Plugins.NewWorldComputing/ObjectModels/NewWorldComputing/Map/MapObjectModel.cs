using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public class MapObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
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

        private MapDifficulty mvarDifficulty = MapDifficulty.Easy;
        public MapDifficulty Difficulty { get { return mvarDifficulty; } set { mvarDifficulty = value; } }

        private byte mvarWidth = 0;
        public byte Width { get { return mvarWidth; } set { mvarWidth = value; } }

        private byte mvarHeight = 0;
        public byte Height { get { return mvarHeight; } set { mvarHeight = value; } }

        private MapKingdomColor mvarAllowedKingdomColors = MapKingdomColor.None;
        public MapKingdomColor AllowedKingdomColors { get { return mvarAllowedKingdomColors; } set { mvarAllowedKingdomColors = value; } }

        private MapKingdomColor mvarAllowedHumanPlayerColors = MapKingdomColor.None;
        public MapKingdomColor AllowedHumanPlayerColors { get { return mvarAllowedHumanPlayerColors; } set { mvarAllowedHumanPlayerColors = value; } }

        private MapKingdomColor mvarAllowedComputerPlayerColors = MapKingdomColor.None;
        public MapKingdomColor AllowedComputerPlayerColors { get { return mvarAllowedComputerPlayerColors; } set { mvarAllowedComputerPlayerColors = value; } }

        private MapWinCondition mvarWinConditions = MapWinCondition.None;
        public MapWinCondition WinConditions { get { return mvarWinConditions; } set { mvarWinConditions = value; } }

        private MapLoseCondition mvarLoseConditions = MapLoseCondition.None;
        public MapLoseCondition LoseConditions { get { return mvarLoseConditions; } set { mvarLoseConditions = value; } }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private MapTile.MapTileCollection mvarTiles = new MapTile.MapTileCollection();
        public MapTile.MapTileCollection Tiles { get { return mvarTiles; } }
    }
}
