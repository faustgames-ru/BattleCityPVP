using UnityEngine;

namespace CoreUtils.Collections
{
    public class PoolListener : MonoBehaviour, IPoolListener
    {
        public event PoolEvent GetInstance;
        public event PoolEvent ReturnInstance;
        public event ApplyTransformEvent ApplyTransform;
        public void OnGetInstance(PoolItem sender)
        {
            GetInstance?.Invoke(sender);
        }

        public void OnApplyTransform(PoolItem sender, ApplyTransformArgs e)
        {
            ApplyTransform?.Invoke(sender, e);
        }

        public void OnReturnInstance(PoolItem sender)
        {
            ReturnInstance?.Invoke(sender);
        }
    }
}