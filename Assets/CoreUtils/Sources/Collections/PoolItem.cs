using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CoreUtils.Collections
{
    public class PoolItem
    {
        public GameObject Instance { get; }
        public IPool Owner { get; }
        private readonly IPoolListener[] _listeners;
        private readonly Transform _transform;
        private readonly ApplyTransformArgs _applyTransformArgs = new ApplyTransformArgs();

        public PoolItem(GameObject instance, IPool owner, bool reportEvents)
        {
            Instance = instance;
            Owner = owner;
            _transform = instance.GetComponent<Transform>();
            if (!reportEvents) return;
            _listeners = instance.GetComponentsInChildren<IPoolListener>();
        }

        public void ReportGetInstance()
        {
            foreach (var listener in _listeners)
            {
                listener.OnGetInstance(this);
            }
        }

        public void ReportReturnInstance()
        {
            foreach (var listener in _listeners)
            {
                listener.OnReturnInstance(this);
            }
        }

        public void Return()
        {
            Owner.Return(this);
        }

        public void ApplyTransform(Vector3 position, Quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
            _applyTransformArgs.position = position;
            _applyTransformArgs.rotation = rotation;
            foreach (var listener in _listeners)
            {
                listener.OnApplyTransform(this, _applyTransformArgs);
            }
        }
    }
}