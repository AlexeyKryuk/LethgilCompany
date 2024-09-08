using Core.View;
using UnityEngine;

namespace ItemGrabbing
{
    public class AttachableItemView : MonoBehaviour, IAttachableView
    {
        private const float DropPower = 200f;
        private const float Offset = 1f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _tooltip;

        private Transform _anchor;

        private void Awake()
        {
            _tooltip.transform.SetParent(null);
            _tooltip.SetActive(false);
        }

        private void Update()
        {
            _tooltip.transform.position = transform.position + Vector3.up * Offset;
        }

        public void Attach(IGrabberView grabber)
        {
            _anchor = grabber.Anchor;

            RenderAttach();
            SetPhysical(false);
        }

        public void Drop(float holdTime)
        {
            SetPhysical(true);
            RenderDrop(holdTime);
        }

        private void SetPhysical(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.isTrigger = !value;
        }

        private void RenderDrop(float power)
        {
            transform.parent = null;
            _rigidbody.AddForce(_anchor.forward * power);
        }

        private void RenderAttach()
        {
            transform.parent = _anchor;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void Render(bool isReady)
        {
            _tooltip.SetActive(isReady);
        }
    }
}
