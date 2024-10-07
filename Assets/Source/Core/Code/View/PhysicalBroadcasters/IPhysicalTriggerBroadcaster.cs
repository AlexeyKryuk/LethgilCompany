using System;

namespace Core.View
{
    public interface IPhysicalTriggerBroadcaster<T>
    {
        event Action<T> onTriggerEnter;
        event Action<T> onTriggerExit;
        event Action<T> onTriggerStay;
    }
}
