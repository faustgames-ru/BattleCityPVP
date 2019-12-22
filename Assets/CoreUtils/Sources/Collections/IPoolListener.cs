using UnityEngine;

namespace CoreUtils.Collections
{
    public interface IPoolListener
    {
        void OnGetInstance(PoolItem sender);
        void OnApplyTransform(PoolItem sender, ApplyTransformArgs e);
        void OnReturnInstance(PoolItem sender);
    }
}