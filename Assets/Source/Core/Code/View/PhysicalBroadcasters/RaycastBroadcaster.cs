using UnityEngine;

namespace Core.View
{
    public abstract class RaycastBroadcaster<T> : MonoBehaviour, IRaycastBroadcaster<T> where T : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;

        public T CurrentHit { get; private set; }
        public bool IsHit => CurrentHit != null;

        public void Initialize(Transform origin)
        {
            _origin = origin;
        }

        private void Update()
        {
            if (Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hit, _distance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out T component))
                    CurrentHit = component;
            }
            else
                CurrentHit = default;
        }
    }
}
