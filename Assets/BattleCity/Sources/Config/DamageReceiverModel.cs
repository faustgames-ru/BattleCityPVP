using System;

namespace BattleCity.Config
{
    [Serializable]
    public class DamageReceiverModel
    {
        public int health = 10;
        public int armor = 0;
        public bool immortal = false;
    }
}