using System;
using UnityEngine;

namespace Core.View
{
    public abstract class PhysicalTriggerBroadcaster<T> : MonoBehaviour, IPhysicalTriggerBroadcaster<T> where T : MonoBehaviour
    {
        public event Action<T> onTriggerEnter;
        public event Action<T> onTriggerExit;
        public event Action<T> onTriggerStay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T physicsEventBroadcaster))
                onTriggerEnter?.Invoke(physicsEventBroadcaster);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T physicsEventBroadcaster))
                onTriggerExit?.Invoke(physicsEventBroadcaster);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out T physicsEventBroadcaster))
                onTriggerStay?.Invoke(physicsEventBroadcaster);
        }
    }
}
