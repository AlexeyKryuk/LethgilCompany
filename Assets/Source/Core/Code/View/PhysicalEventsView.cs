using System;
using UnityEngine;

namespace Core.View
{
    public class PhysicalEventsView : MonoBehaviour, IPhysicsEventBroadcaster
    {
        public event Action<IPhysicsEventBroadcaster> onTriggerEnter;
        public event Action<IPhysicsEventBroadcaster> onTriggerExit;
        public event Action<IPhysicsEventBroadcaster> onTriggerStay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPhysicsEventBroadcaster physicsEventBroadcaster))
                onTriggerEnter?.Invoke(physicsEventBroadcaster);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IPhysicsEventBroadcaster physicsEventBroadcaster))
                onTriggerExit?.Invoke(physicsEventBroadcaster);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IPhysicsEventBroadcaster physicsEventBroadcaster))
                onTriggerStay?.Invoke(physicsEventBroadcaster);
        }

        bool IPhysicsEventBroadcaster.TryGetComponent<T>(out T component)
        {
            return TryGetComponent(out component);
        }
    }
}
