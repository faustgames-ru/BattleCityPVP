using System;

namespace BattleCity.Game.Damage
{
    [Flags]
    public enum DamageLayer
    {
        None = 0x0,
        MasterPlayer = 0x1,
        SlavePlayer = 0x2,
        MasterCommandCenter = 0x4,
        SlaveCommandCenter = 0x8,
        All = -1,
    }
}