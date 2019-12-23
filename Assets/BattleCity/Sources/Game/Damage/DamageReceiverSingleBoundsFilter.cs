using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageReceiverSingleBoundsFilter : DamageReceiverFilter
    {
        public Bounds receiverBounds;
        public DamageReceiver damageReceiver;

        public override void Filter(Bounds bounds, List<DamageReceiver> receivers)
        {
            receivers.Clear();
            if (!receiverBounds.Intersects(bounds)) return;
            receivers.Add(damageReceiver);
        }
    }
}