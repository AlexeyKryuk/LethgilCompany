using UnityEngine;

namespace Core.View
{
    public interface IRaycastBroadcaster<T>
    {
        T CurrentHit { get; }
        bool IsHit { get; }

        void Initialize(Transform origin);
    }
}
