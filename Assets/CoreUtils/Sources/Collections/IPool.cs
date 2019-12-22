using UnityEngine;

namespace CoreUtils.Collections
{
    public interface IPool
    {
        PoolItem Get();
        void Return(PoolItem poolItem);
    }
}