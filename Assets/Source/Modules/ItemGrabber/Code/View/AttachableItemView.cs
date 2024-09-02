using Core;
using UnityEngine;

namespace ItemGrabber
{
    public class AttachableItemView : MonoBehaviour, IAttachableView
    {
        private const float DropPower = 50f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private Transform _anchor;

        public void Attach(IGrabberView grabber)
        {
            _anchor = grabber.Anchor;

            RenderAttach();
            SetPhysical(false);
        }

        public void Drop()
        {
            SetPhysical(true);
            RenderDrop();
        }

        private void SetPhysical(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.isTrigger = !value;
        }

        private void RenderAttach()
        {
            transform.parent = _anchor;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        private void RenderDrop()
        {
            transform.parent = null;
            _rigidbody.AddForce(_anchor.forward * DropPower);
        }
    }
}
