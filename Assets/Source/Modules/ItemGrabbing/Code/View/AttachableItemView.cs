using Core.Model;
using Core.View;
using Photon.Pun;
using UnityEngine;

namespace ItemGrabbing
{
    public class AttachableItemView : MonoBehaviour, IAttachableView
    {
        [SerializeField] private LootType _itemType;
        [SerializeField] private GameObject _tooltip;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PhotonView _photonView;

        private Transform _anchor;
        private Transform _dropForward;

        private bool _isAvailable = true;

        public LootType Type => _itemType;
        public bool IsAvailable => _isAvailable;
        public int PhotonViewId => _photonView.sceneViewId;

        private void Awake()
        {
            _tooltip.transform.SetParent(null);
            _tooltip.SetActive(false);
        }

        private void Update()
        {
            RenderTooltip();
            Render();
        }

        private void RenderTooltip()
        {
            _tooltip.transform.position = transform.position + Vector3.up;
            _tooltip.transform.LookAt(Camera.main.transform);
        }

        private void Render()
        {
            if (_isAvailable)
                return;

            transform.position = _anchor.position;
            transform.rotation = _anchor.rotation;
        }

        public void Attach(IGrabberView grabber)
        {
            Activate(false);

            _anchor = grabber.Anchor;
            _dropForward = grabber.DropForward;
            _isAvailable = false;
        }

        public void Drop(float power)
        {
            _isAvailable = true;

            Activate(true);
            _rigidbody.AddForce(_dropForward.forward * power);
        }

        public void Render(bool isReady)
        {
            _tooltip.SetActive(isReady);
        }

        private void Activate(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.enabled = value;
        }
    }
}
