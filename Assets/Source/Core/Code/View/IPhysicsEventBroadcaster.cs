using System;

namespace Core.View
{
    public interface IPhysicsEventBroadcaster<T>
    {
        event Action<T> onTriggerEnter;
        event Action<T> onTriggerExit;
        event Action<T> onTriggerStay;
    }
}
