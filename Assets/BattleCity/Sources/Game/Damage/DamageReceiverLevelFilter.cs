using System.Collections.Generic;
using BattleCity.Level;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageReceiverLevelFilter : DamageReceiverFilter
    {
        [SerializeField]
        private LevelController levelController;

        private readonly List<LevelTile> _damageTiles = new List<LevelTile>();

        public override void Filter(Bounds bounds, List<DamageReceiver> receivers)
        {
            receivers.Clear();
            levelController.GetTiles(bounds, _damageTiles);
            foreach (var damageTile in _damageTiles)
            {
                receivers.Add(damageTile.damageReceiver);
            }
            levelController.SetDirty(_damageTiles);
        }
    }
}