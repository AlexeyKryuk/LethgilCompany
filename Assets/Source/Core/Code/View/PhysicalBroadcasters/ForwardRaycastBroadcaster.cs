using UnityEngine;

namespace Core.View
{
    public abstract class ForwardRaycastBroadcaster<T> : MonoBehaviour, IRaycastBroadcaster<T> where T : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _distance;
        [SerializeField] private float _raduis;
        [SerializeField] private LayerMask _layerMask;

        public T CurrentHit { get; private set; }
        public T LastHit { get; private set; }
        public bool IsHit => CurrentHit != null;

        public void Initialize(Transform origin)
        {
            _origin = origin;
        }

        protected abstract bool TryGetComponent(RaycastHit hit, out T component);

        private void Update()
        {
            if (Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hit, _distance, _layerMask))
            {
                if (TryGetComponent(hit, out T component))
                {
                    CurrentHit = component;
                    LastHit = component;
                }
            }
            else
                CurrentHit = default;
        }
    }
}
