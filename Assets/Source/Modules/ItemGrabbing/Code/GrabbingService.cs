using Core;
using Core.Model;
using Core.View;
using Photon.Pun;
using UnityEngine;
using VContainer.Unity;

namespace ItemGrabbing
{
    public class GrabbingService : IGrabbingService, IInitializable
    {
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;

        private readonly IPlayerService _playerService;
        private readonly GrabbingConfig _config;

        private ICharacterInputs _inputs;
        private IGrabberView _grabber;
        private PhotonView _grabberPhotonView;

        private GrabbingUI _grabbingUI;
        private bool _isGrabbing;

        public GrabbingService(IInputService inputService, IUIService uiService, IPlayerService playerService, GrabbingConfig config)
        {
            _inputService = inputService;
            _uiService = uiService;
            _playerService = playerService;
            _config = config;
        }

        public void Initialize()
        {
            _inputs = _inputService.CharacterInputs;
            _grabber = _playerService.Presenter.GetView<IGrabberView>();
            _grabberPhotonView = _playerService.Presenter.GetView<PhotonView>();
            _grabber.Initialize(_playerService.Presenter.GetView<ICharacterCameraView>().Transform);

            _grabbingUI = _uiService.CreateUIElement<GrabbingUI>(UIElementID.GrabbingCanvas);
        }

        public void Start()
        {
            _inputs.ActionButton.PointerDown += OnPointerDown;
            _inputs.ActionButton.PointerUp += OnPointerUp;
        }

        public void Dispose()
        {
            _inputs.ActionButton.PointerDown -= OnPointerDown;
            _inputs.ActionButton.PointerUp -= OnPointerUp;
        }

        public void Tick()
        {
            float holdValue = ClampHoldTime(_inputs.ActionButton.HoldValue);
            float targetValue = _config.DropDelayClamp.y;

            if (_isGrabbing)
                _grabbingUI.Render(targetValue, holdValue);
            else
                _grabbingUI.Render(targetValue, 0);
        }

        public void LateTick() { }

        [PunRPC]
        public void TakeItem(int grabberId, int itemId)
        {
            var grabber = PhotonNetwork.GetPhotonView(grabberId).GetComponentInChildren<IGrabberView>();
            var item = PhotonNetwork.GetPhotonView(itemId).GetComponent<IAttachableView>();

            if (_grabberPhotonView.sceneViewId == grabberId)
                grabber.Grab();

            item.Attach(grabber);
        }

        [PunRPC]
        public void DropItem(int grabberId, int itemId, float holdTime)
        {
            var grabber = PhotonNetwork.GetPhotonView(grabberId).GetComponentInChildren<IGrabberView>();
            var item = PhotonNetwork.GetPhotonView(itemId).GetComponent<IAttachableView>();

            if (_grabberPhotonView.sceneViewId == grabberId)
            {
                float power = GetDropPower(ClampHoldTime(holdTime));
                item.Drop(power);
            }

            item.Attach(grabber);




            float power = GetDropPower(ClampHoldTime(holdTime));
            IAttachableView attachable = _grabber.Drop();

            attachable.Drop(power);
        }

        private void OnPointerDown()
        {
            if (_grabber.IsGrabReady)
            {
                _grabberPhotonView.RPC(nameof(TakeItem), RpcTarget.AllBuffered, 
                    _grabberPhotonView.sceneViewId, _grabber.ItemInRange.PhotonViewId);
            }
        }

        private void OnPointerUp(float holdTime)
        {
            if (_grabber.IsGrabActive)
            {
                _grabberPhotonView.RPC(nameof(DropItem), RpcTarget.AllBuffered,
                    _grabberPhotonView.sceneViewId, _grabber.ItemInRange.PhotonViewId, holdTime);
            }
            else if (_grabber.IsGrabReady)
            {
                _grabberPhotonView.RPC(nameof(TakeItem), RpcTarget.AllBuffered,
                    _grabberPhotonView.sceneViewId, _grabber.ItemInRange.PhotonViewId);
            }
        }

        private float ClampHoldTime(float time)
        {
            float min = _config.DropDelayClamp.x;
            float max = _config.DropDelayClamp.y;

            return Mathf.Clamp(time, min, max);
        }

        private float GetDropPower(float holdTime)
            => _config.Graph.Evaluate(holdTime);
    }
}
