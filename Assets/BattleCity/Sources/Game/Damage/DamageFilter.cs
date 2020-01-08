using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageFilter: MonoBehaviour
    {
        [SerializeField]
        private DamageReceiverFilter[] filters;

        private readonly List<DamageReceiverFilter> _runtimeFilters= new List<DamageReceiverFilter>();
        private readonly List<DamageReceiver> _receivers = new List<DamageReceiver>();

        private void Awake()
        {
            _runtimeFilters.AddRange(filters);
        }

        public void AddFilter(DamageReceiverFilter container)
        {
            _runtimeFilters.Add(container);
        }

        public void RemoveFilter(DamageReceiverFilter container)
        {
            _runtimeFilters.Remove(container);
        }

        public void FilterReceivers(Bounds bounds, List<DamageReceiver> receivers)
        {
            receivers.Clear();
            foreach (var filter in _runtimeFilters)
            {
                filter.Filter(bounds, _receivers);
                receivers.AddRange(_receivers);
            }
        }
    }
}