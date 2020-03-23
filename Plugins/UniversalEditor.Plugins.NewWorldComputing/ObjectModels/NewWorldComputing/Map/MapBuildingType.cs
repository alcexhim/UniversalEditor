using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    [Flags()]
    public enum MapBuildingType : ushort
    {
        None = 0,
        ThievesGuild = 2,
        Tavern = 4,
        Shipyard = 8,
        Well = 16,
        Statue = 128,
        LeftTurret = 256,
        RightTurret = 512,
        Marketplace = 1024,
        /// <summary>
        /// Creature enhancer. Farm (Knight), Garbage Heap (Barbarian), Crystal Garden (Sorceress), Waterfall (Warlock),
        /// Orchard (Wizard), Skull Pile (Necroman).
        /// </summary>
        CreatureEnhancer = 2048,
        Moat = 4096,
        /// <summary>
        /// Castle enhancer. Fortification (Knight), Coliseum (Barbarian), Rainbow (Sorceress), Dungeon (Warlock),
        /// Library (Wizard), Storm (Necroman).
        /// </summary>
        CastleEnhancer = 8192
    }
}
