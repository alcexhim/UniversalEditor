using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public class MapArmyMonster
    {
        private ushort mvarAmount = 0;
        public ushort Amount { get { return mvarAmount; } set { mvarAmount = value; } }

        private MapMonsterType mvarMonsterType = MapMonsterType.Unknown;
        public MapMonsterType MonsterType { get { return mvarMonsterType; } set { mvarMonsterType = value; } }

    }
}
