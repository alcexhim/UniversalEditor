using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
    public enum MapMonsterType : byte
    {
        Unknown,
        #region Knight
        Peasant,
        Archer,
        Ranger,
        Pikeman,
        VeteranPikeman,
        Swordsman,
        MasterSwordsman,
        Cavalry,
        Champion,
        Paladin,
        Crusader,
        #endregion
        #region Barbarian
        Goblin,
        Orc,
        OrcChief,
        Wolf,
        Ogre,
        OgreLord,
        Troll,
        WarTroll,
        Cyclops,
        #endregion
        #region Sorceress
        Sprite,
        Dwarf,
        BattleDwarf,
        Elf,
        GrandElf,
        Druid,
        GreaterDruid,
        Unicorn,
        Phoenix,
        #endregion
        #region Warlock
        Centaur,
        Gargoyle,
        Griffin,
        Minotaur,
        MinotaurKing,
        Hydra,
        GreenDragon,
        RedDragon,
        BlackDragon,
        #endregion
        #region Wizard
        Halfling,
        Boar,
        IronGolem,
        SteelGolem,
        Roc,
        Mage,
        ArchMage,
        Giant,
        Titan,
        #endregion
        #region Necromancer
        Skeleton,
        Zombie,
        MutantZombie,
        Mummy,
        RoyalMummy,
        Vampire,
        VampireLord,
        Lich,
        PowerLich,
        BoneDragon,
        #endregion
        #region Neutral
        Rogue,
        Nomad,
        Ghost,
        Genie,
        Medusa,
        EarthElemental,
        AirElemental,
        FireElemental,
        WaterElemental,
        #endregion
        #region Random
        Random1,
        Random2,
        Random3,
        Random4,
        Random
        #endregion
    }
}
