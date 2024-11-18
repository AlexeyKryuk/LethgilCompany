using Photon.Pun;
using Photon.Realtime;
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
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 50f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 50f);
        }

        public void TransferOwnership(Player newOwner)
        {
            _photonView.TransferOwnership(newOwner);
        }

        public void Attach()
        {
            IsAvailable = false;

            _collider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void Unattach()
        {
            IsAvailable = true;
            _collider.enabled = true;
        }

        public void Throw(Vector3 direction, float power)
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
            _rigidbody.AddForce(direction * power);
        }
    }
}
