using System;
using CoreUtils.Collections;
using UnityEngine;

namespace BattleCity.Config
{
    [Serializable]
    public class PrefabPoolMapping
    {
        public string prefabId;
        public GameObject origin;
        public int capacity;
        public bool reportEvents = true;
        public PrefabsPoolMode mode;

        public PrefabPoolMapping() { }

        public PrefabPoolMapping(string prefabId, int capacity, PrefabsPoolMode mode)
        {
            this.prefabId = prefabId;
            this.capacity = capacity;
            this.mode = mode;
        }

        public IPool CreatePool(Transform parent)
        {
            switch (mode)
            {
                case PrefabsPoolMode.None:
                    return new FakePool(parent, origin, reportEvents);
                case PrefabsPoolMode.EzPool:
                    return new EzPool(parent, origin, capacity, reportEvents);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}