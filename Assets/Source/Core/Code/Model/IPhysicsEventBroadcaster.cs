using System;

namespace Core
{
    public interface IPhysicsEventBroadcaster
    {
        event Action<IPhysicsEventBroadcaster> onTriggerEnter;
        event Action<IPhysicsEventBroadcaster> onTriggerExit;
        event Action<IPhysicsEventBroadcaster> onTriggerStay;

        public bool TryGetComponent<T>(out T component);
    }
}
