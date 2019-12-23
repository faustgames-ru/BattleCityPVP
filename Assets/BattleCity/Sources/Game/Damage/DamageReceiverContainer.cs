using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public abstract class DamageReceiverFilter : MonoBehaviour
    {
        public abstract void Filter(Bounds bounds, List<DamageReceiver> receivers);
    }
}