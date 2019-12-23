using System.Collections.Generic;
using BattleCity.Config;
using CoreUtils.Collections;
using Photon.Pun;
using UnityEngine;

namespace BattleCity.Game
{
    public class EzPunPrefabPool : MonoBehaviour, IPunPrefabPool
    {
        [SerializeField]
        private PrefabPoolConfig config;
        
        public event EzPunPrefabPoolEvent Instantiated;

        private readonly Dictionary<string, IPool> _pools = new Dictionary<string, IPool>();
        private readonly Dictionary<GameObject, PoolItem> _items = new Dictionary<GameObject, PoolItem>();
        private Transform _transform;
        private readonly EzPunPrefabPoolEventErgs _ezPunPrefabPoolEventErgs = new EzPunPrefabPoolEventErgs();

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            PhotonNetwork.PrefabPool = this;
            Init();
        }

        private void OnDestroy()
        {
            PhotonNetwork.PrefabPool = new DefaultPool();
        }

        private void Init()
        {
            foreach (var pool in config.pools)
            {
                if (_pools.ContainsKey(pool.prefabId))
                {
                    Debug.LogError($"Duplicate prefab id {pool.prefabId}", this);
                    continue;
                }
                _pools.Add(pool.prefabId, pool.CreatePool(_transform));
            }
        }

        public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(prefabId)) return null;
            var pool = _pools[prefabId];
            var result = pool.Get();
            OnInstantiated(prefabId, result);
            result.ReportGetInstance();
            result.ApplyTransform(position, rotation);
            _items[result.Instance] = result;
            return result.Instance;
        }

        public void Destroy(GameObject instance)
        {
            if (!_items.ContainsKey(instance))
            {
                // todo: debug this bullshit
                //Debug.LogError($"Destroy bastard instance {instance.name}", instance);
                return;
            }

            var poolItem = _items[instance];
            _items.Remove(poolItem.Instance);
            poolItem.ReportReturnInstance();
            poolItem.Return();
        }

        protected virtual void OnInstantiated(string prefabId, PoolItem item)
        {
            _ezPunPrefabPoolEventErgs.prefabId = prefabId;
            _ezPunPrefabPoolEventErgs.item = item;
            Instantiated?.Invoke(this, _ezPunPrefabPoolEventErgs);
        }
    }
}