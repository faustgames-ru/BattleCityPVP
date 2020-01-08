using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageSystem: MonoBehaviour
    {
        [SerializeField]
        private DamageFilter damageFilter;

        private readonly List<DamageSpawner> _damageSpawners = new List<DamageSpawner>();
        private readonly List<DamageSpawner> _deadList= new List<DamageSpawner>();
        private readonly List<DamageSpawner> _mutedList = new List<DamageSpawner>();
        private readonly List<DamageReceiver> _receivers = new List<DamageReceiver>();

        public void AddSpawner(DamageSpawner spawner)
        {
            _damageSpawners.Add(spawner);
        }

        public void RemoveSpawner(DamageSpawner spawner)
        {
            _damageSpawners.Remove(spawner);
        }

        private void FixedUpdate()
        {
            _deadList.Clear();
            _mutedList.Clear();
            foreach (var damageSpawner in _damageSpawners)
            {
                if (!damageSpawner.IsAlive)
                {
                    _deadList.Add(damageSpawner);
                    continue;
                }

                if (damageSpawner.IsMuted)
                {
                    _mutedList.Add(damageSpawner);
                    continue;
                }

                damageFilter.FilterReceivers(damageSpawner.Bounds, _receivers);
                var hasCollision = false;
                var hasBlock = false;
                foreach (var receiver in _receivers)
                {
                    if (!receiver.damageLayer.Intersects(damageSpawner.damageLayer)) continue;
                    if (!receiver.IsAlive) continue;
                    if (!receiver.ApplyDamage(damageSpawner))
                    {
                        hasBlock = true;
                    }

                    hasCollision = true;
                }

                if (damageSpawner.collisionWithSpawners)
                {
                    foreach (var otherSpawner in _damageSpawners)
                    {
                        if (otherSpawner == damageSpawner) continue;
                        if (!otherSpawner.IsAlive) continue;
                        if (otherSpawner.IsMuted) continue;
                        if (damageSpawner.Bounds.Intersects(otherSpawner.Bounds))
                        {
                            hasCollision = true;
                        }
                    }
                }


                if (hasBlock)
                {
                    damageSpawner.ReportBlock();
                }
                else if (hasCollision)
                {
                    damageSpawner.ReportCollision();
                }

            }

            foreach (var damageSpawner in _mutedList)
            {
                damageSpawner.OnMuted();
            }


            foreach (var damageSpawner in _deadList)
            {
                RemoveSpawner(damageSpawner);
                damageSpawner.OnDead();
            }


        }

    }
}