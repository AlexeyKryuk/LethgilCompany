using Photon.Pun;
using UnityEngine;

namespace ItemGrabbing
{
    public class AttachableItemView : MonoBehaviour, IAttachableView
    {
        private Collider _collider;
        private Rigidbody _rigidbody;
        private PhotonView _photonView;

        public bool IsAvailable { get; private set; } = true;
        public int NetworkId => _photonView.ViewID;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            _photonView = GetComponent<PhotonView>();
        }

        public void UpdateTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Attach()
        {
            SwitchActivityState(false);
            IsAvailable = false;

            _photonView.RequestOwnership();
        }

        public void Unattach()
        {
            SwitchActivityState(true);
            IsAvailable = true;
        }

        public void Throw(Vector3 direction, float power)
        {
            _rigidbody.AddForce(direction * power);
        }

        private void SwitchActivityState(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.enabled = value;
        }
    }
}
