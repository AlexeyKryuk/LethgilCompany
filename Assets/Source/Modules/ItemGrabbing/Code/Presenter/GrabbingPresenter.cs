using Core;
using Core.View;
using Photon.Pun;
using System;
using UnityEngine;
using VContainer.Unity;

namespace ItemGrabbing
{
    public class GrabbingPresenter : IInitializable, IStartable, ITickable, IDisposable
    {
        private readonly IGrabberView _view;
        private readonly ICharacterCameraView _cameraView;
        private readonly IInputService _inputService;
        private readonly IRaycastBroadcaster<AttachableItemView> _raycastBroadcaster;
        private readonly PhotonView _photonView;
        private readonly GrabbingDropUI _dropUI;
        private readonly GrabbingTooltipUI _tooltipUI;
        private readonly GrabbingConfig _config;

        private IGrabber _model;

        public GrabbingPresenter(IGrabberView view, ICharacterCameraView cameraView,
            IInputService inputService, IRaycastBroadcaster<AttachableItemView> raycastBroadcaster,
            PhotonView photonView, GrabbingDropUI dropUI, GrabbingTooltipUI tooltipUI, GrabbingConfig config)
        {
            _view = view;
            _cameraView = cameraView;
            _inputService = inputService;
            _raycastBroadcaster = raycastBroadcaster;
            _photonView = photonView;
            _dropUI = dropUI;
            _tooltipUI = tooltipUI;
            _config = config;
        }

        public void Initialize()
        {
            _model = new Grabber();

            _view.Initialize(_config, _cameraView.Transform);
            _raycastBroadcaster.Initialize(_cameraView.Transform);
            _tooltipUI.Initialize(_cameraView.Transform);
        }

        public void Start()
        {
            _inputService.CharacterInputs.ActionButton.PointerUp += OnPointerUp;
        }

        public void Dispose()
        {
            _inputService.CharacterInputs.ActionButton.PointerUp -= OnPointerUp;
        }

        public void Tick()
        {
            float holdValue = ClampHoldTime(_inputService.CharacterInputs.ActionButton.HoldValue) * (_model.IsGrabActive ? 1 : 0);
            float targetValue = _config.DropDelayClamp.y;

            _dropUI.Render(targetValue, holdValue);
            _tooltipUI.Render(_raycastBroadcaster.CurrentHit);
        }
            
        private void OnPointerUp(float holdTime)
        {
            if (_model.IsGrabActive)
            {
                _model.Drop();
                _view.Drop(ClampHoldTime(holdTime));
            }
            else if (_raycastBroadcaster.IsHit)
            {
                _model.Grab();
                _view.Grab(_raycastBroadcaster.CurrentHit);
            }
        }

        private float ClampHoldTime(float time)
        {
            float min = _config.DropDelayClamp.x;
            float max = _config.DropDelayClamp.y;

            return Mathf.Clamp(time, min, max);
        }
    }
}
