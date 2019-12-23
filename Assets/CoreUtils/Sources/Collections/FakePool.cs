using UnityEngine;

namespace CoreUtils.Collections
{
    public class FakePool : IPool
    {
        private readonly Transform _parent;
        private readonly GameObject _origin;
        private readonly bool _reportEvents;

        public FakePool(Transform parent, GameObject origin, bool reportEvents)
        {
            _parent = parent;
            _origin = origin;
            _reportEvents = reportEvents;
        }

        public PoolItem Get()
        {
            var instance = Object.Instantiate(_origin, _parent);
            instance.SetActive(false);
            var result = new PoolItem(instance, this, _reportEvents);
            return result;
        }

        public void Return(PoolItem poolItem)
        {
            Object.Destroy(poolItem.Instance);
        }
    }
}