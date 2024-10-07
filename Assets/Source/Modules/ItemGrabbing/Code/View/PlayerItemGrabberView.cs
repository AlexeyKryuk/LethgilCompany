using Core.View;
using Photon.Pun;
using UnityEngine;

namespace ItemGrabbing
{
    public class PlayerItemGrabberView : MonoBehaviourPun, IGrabberView
    {
        [SerializeField] private Transform _anchor;

        private GrabbingConfig _config;
        private Transform _directionOfView;
        private IAnimatorController _animatorController;
        private IAttachableView _current;

        public Transform Anchor => _anchor;
        public Transform DirectionOfView => _directionOfView;

        private void Awake()
        {
            _animatorController = GetComponentInChildren<IAnimatorController>();
        }

        private void Update()
        {
            if (_current != null)
                _current.UpdateTransform(Anchor.position, Anchor.rotation);
        }

        public void Initialize(GrabbingConfig config, Transform directionOfView)
        {
            _config = config;
            _directionOfView = directionOfView;
        }

        public void Grab(IAttachableView item)
        {
            _current = item;
            _current.Attach();

            photonView.RPC(nameof(GrabRPC), RpcTarget.AllBuffered, _current.NetworkId, photonView.ViewID);
            _animatorController.SetBool(AnimatorParameter.Grab, true);
        }

        public void Drop(float holdTime)
        {
            photonView.RPC(nameof(DropRPC), RpcTarget.AllBuffered, _current.NetworkId);
            _animatorController.SetBool(AnimatorParameter.Grab, false);

            _current.Unattach();
            _current.Throw(DirectionOfView.forward, GetDropPower(holdTime));
            _current = null;
        }

        [PunRPC]
        public void GrabRPC(int itemID, int ownerID)
        {
            var itemNetView = PhotonNetwork.GetPhotonView(itemID);
            var item = itemNetView.GetComponent<IAttachableView>();

            item.Attach();
        }

        [PunRPC]
        public void DropRPC(int itemID)
        {
            var item = PhotonNetwork.GetPhotonView(itemID).GetComponent<IAttachableView>();

            item.Unattach();
        }

        private float GetDropPower(float holdTime)
            => _config.Graph.Evaluate(holdTime);
    }
}
