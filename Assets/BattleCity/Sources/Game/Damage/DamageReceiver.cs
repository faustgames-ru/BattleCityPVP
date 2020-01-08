using System;
using BattleCity.Config;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageReceiver
    {
        public int armor;
        public int health;
        public bool immortal;
        public event Action Dead;
        public event Action Damage;
        public DamageLayer damageLayer = DamageLayer.All;

        public bool IsAlive => immortal || health > 0;

        private readonly DamageReceiverModel _model;

        public DamageReceiver()
        {
        }

        public DamageReceiver(DamageReceiverModel model)
        {
            _model = model;
            SetModel();
        }

        public void Reset()
        {
            SetModel();
        }

        public void SetHealth(int value)
        {
            health = value;
            if (!IsAlive)
            {
                OnDead();
            }
        }

        public bool ApplyDamage(DamageSpawner spawner)
        {
            if (immortal) return false;
            var damage = Mathf.Max(spawner.Damage - armor, 0);
            if (damage == 0) return false;
            health = Mathf.Max(health - damage, 0);
            OnDamage();
            if (!IsAlive)
            {
                OnDead();
            }
            return true;
        }

        public void Kill()
        {
            health = 0;
        }

        public string FormatHealth()
        {
            return $"{health * 100 / _model.health}%";
        }
        
        private void SetModel()
        {
            armor = _model.armor;
            health = _model.health;
            immortal = _model.immortal;
        }

        private void OnDead()
        {
            Dead?.Invoke();
        }

        private void OnDamage()
        {
            Damage?.Invoke();
        }
    }
}