using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public class MapCastle
    {
        private MapCastleColor mvarColor = MapCastleColor.Unknown;
        public MapCastleColor Color { get { return mvarColor; } set { mvarColor = value; } }

        private bool mvarHasCustomBuilding = false;
        public bool HasCustomBuilding { get { return mvarHasCustomBuilding; } set { mvarHasCustomBuilding = value; } }

        private MapBuildingType mvarBuildings = MapBuildingType.None;
        public MapBuildingType Buildings { get { return mvarBuildings; } set { mvarBuildings = value; } }

        private MapDwellingType mvarDwellings = MapDwellingType.None;
        public MapDwellingType Dwellings { get { return mvarDwellings; } set { mvarDwellings = value; } }

        private MapMageGuildLevel mvarMageGuildLevel = MapMageGuildLevel.None;
        public MapMageGuildLevel MageGuildLevel { get { return mvarMageGuildLevel; } set { mvarMageGuildLevel = value; } }

        private bool mvarHasCustomTroops = false;
        public bool HasCustomTroops { get { return mvarHasCustomTroops; } set { mvarHasCustomTroops = value; } }

        private MapArmyMonster[] mvarMonsters = new MapArmyMonster[5];
        public MapArmyMonster[] Monsters { get { return mvarMonsters; } }

        private bool mvarHasCaptain = false;
        public bool HasCaptain { get { return mvarHasCaptain; } set { mvarHasCaptain = value; } }

        private bool mvarHasCustomName = false;
        public bool HasCustomName { get { return mvarHasCustomName; } set { mvarHasCustomName = value; } }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private MapCastleType mvarType = MapCastleType.Unknown;
        public MapCastleType Type { get { return mvarType; } set { mvarType = value; } }

        private bool mvarIsCastle = false;
        public bool IsCastle { get { return mvarIsCastle; } set { mvarIsCastle = value; } }

        private bool mvarIsUpgradable = false;
        public bool IsUpgradable { get { return mvarIsUpgradable; } set { mvarIsUpgradable = value; } }
    }
}
