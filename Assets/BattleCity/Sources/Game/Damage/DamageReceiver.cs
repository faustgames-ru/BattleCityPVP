using BattleCity.Config;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageReceiver
    {
        public int armor;
        public int health;
        public bool immortal;

        private readonly DamageReceiverModel _model;

        public DamageReceiver()
        {
        }

        public DamageReceiver(DamageReceiverModel model)
        {
            _model = model;
            armor = _model.armor;
            health = _model.health;
            immortal = _model.immortal;
        }

        public bool IsAlive => immortal || health > 0;

        public bool ApplyDamage(DamageSpawner spawner)
        {
            if (immortal) return false;
            var damage = Mathf.Max(spawner.Damage - armor, 0);
            if (damage == 0) return false;
            health = Mathf.Max(health - damage, 0);
            return true;
        }

        public string FormatHealth()
        {
            return $"{health * 100 / _model.health}%";
        }
    }
}