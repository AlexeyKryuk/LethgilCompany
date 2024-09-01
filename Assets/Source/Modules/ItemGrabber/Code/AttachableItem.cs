using Core;
using UnityEngine;

namespace ItemGrabber
{
    public class AttachableItem : MonoBehaviour, IAttachable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private IGrabberView _grabber;

        public Transform Transform => transform;
        public bool IsAttached => _grabber != null;

        public void Attach(IGrabberView grabber)
        {
            _grabber = grabber;

            transform.parent = _grabber.Anchor;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
        }

        public void Drop()
        {
            _rigidbody.isKinematic = false;
            _collider.isTrigger = false;

            transform.parent = null;
            _rigidbody.AddForce(_grabber.Anchor.forward * 50f);

            _grabber = null;
        }
    }
}
