using Core.View;
using Photon.Pun;
using UnityEngine;

namespace ItemGrabbing
{
    public class PlayerItemGrabberView : MonoBehaviourPun, IGrabberView
    {
        [SerializeField] private Transform _anchor;

        private IAnimatorController<AnimatorParameter> _animatorController;
        private IAttachableView _current;
        private Transform _directionOfView;

        private GrabbingData _model;

        public Transform Anchor => _anchor;
        public Transform DirectionOfView => _directionOfView;

        private void Awake()
        {
            _animatorController = GetComponentInParent<IAnimatorController<AnimatorParameter>>();
        }

        private void Update()
        {
            if (_current != null)
                _current.UpdateTransform(Anchor.position, Anchor.rotation);
        }

        public void Initialize(GrabbingData model, Transform directionOfView)
        {
            _model = model;
            _directionOfView = directionOfView;
        }

        public void Grab(IAttachableView item)
        {
            _current = item;
            _current.TransferOwnership(PhotonNetwork.LocalPlayer);

            photonView.RPC(nameof(GrabRPC), RpcTarget.AllBuffered, _current.NetworkId, photonView.ViewID);
            _animatorController.SetBool(AnimatorParameter.Grab, true);
        }

        public void Drop(float holdTime)
        {
            photonView.RPC(nameof(DropRPC), RpcTarget.AllBuffered, _current.NetworkId);
            _animatorController.SetBool(AnimatorParameter.Grab, false);

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
            => _model.GetDropPower(holdTime);
    }
}
