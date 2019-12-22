using System.Collections.Generic;
using UnityEngine;

namespace CoreUtils.Collections
{
    public class EzPool: IPool
    {
        private readonly Transform _parent;
        private readonly GameObject _prefabOrigin;
        private readonly Queue<PoolItem> _pool = new Queue<PoolItem>();
        private readonly bool _reportEvents;

        public EzPool(Transform parent, GameObject prefabOrigin, int startCapacity, bool reportEvents)
        {
            _parent = parent;
            _prefabOrigin = prefabOrigin;
            _reportEvents = reportEvents;
            for (var i = 0; i < startCapacity; i++)
            {
                _pool.Enqueue(Instantiate());
            }
        }

        private PoolItem Instantiate()
        {
            var instance = Object.Instantiate(_prefabOrigin, _parent);
            instance.SetActive(false);
            var item = new PoolItem(instance, this, _reportEvents);
            return item;
        }
        
        public PoolItem Get()
        {
            var result = _pool.Count == 0
                ?Instantiate()
                :_pool.Dequeue();
            result.ReportGetInstance();
            return result;
        }

        public void Return(PoolItem poolItem)
        {
            if(poolItem.Owner != this)
            {
                Debug.LogError($"pool got bastard {poolItem.Instance.name}", poolItem.Instance);
                return;
            }

            poolItem.Instance.SetActive(false);
            poolItem.ReportReturnInstance();
            _pool.Enqueue(poolItem);
        }
    }
}