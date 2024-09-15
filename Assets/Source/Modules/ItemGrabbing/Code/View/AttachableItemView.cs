using Core.View;
using UnityEngine;

namespace ItemGrabbing
{
    public class AttachableItemView : MonoBehaviour, IAttachableView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _tooltip;

        private Transform _anchor;
        private Transform _dropForward;

        private void Awake()
        {
            _tooltip.transform.SetParent(null);
            _tooltip.SetActive(false);
        }

        private void Update()
        {
            _tooltip.transform.position = transform.position + Vector3.up;
            _tooltip.transform.LookAt(Camera.main.transform);
        }

        public void Attach(IGrabberView grabber)
        {
            _anchor = grabber.Anchor;
            _dropForward = grabber.DropForward;

            RenderAttach();
            SetPhysical(false);
        }

        public void Drop(float power)
        {
            SetPhysical(true);
            RenderDrop(power);
        }

        private void SetPhysical(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.isTrigger = !value;
        }

        private void RenderDrop(float power)
        {
            transform.parent = null;
            _rigidbody.AddForce(_dropForward.forward * power);
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
