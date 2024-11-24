using CharacterController;
using Core;
using Core.View;
using System;
using UnityEngine;
using VContainer.Unity;

namespace ItemGrabbing
{
    public class GrabbingPresenter : ILifetimeCycleService, IInitializable, IStartable, ITickable, IDisposable
    {
        private readonly IInputService<PlayerCharacterInputs> _inputService;
        private readonly IUIService _uiService;
        private readonly IPlayerPresenter _player;
        private readonly GrabbingConfig _config;

        private IRaycastBroadcaster<AttachableItemView> _raycastBroadcaster;
        private ICharacterCameraView _cameraView;
        private IGrabberView _view;

        private TooltipUI _tooltipUI;
        private GrabbingDropUI _dropUI;
        private bool _isGrabActive;

        private GrabbingData _model;

        public GrabbingPresenter(IInputService<PlayerCharacterInputs> inputService, GrabbingConfig config,
            IPlayerPresenter player, IUIService uiService)
        {
            _inputService = inputService;
            _config = config;
            _player = player;
            _uiService = uiService;
        }

        public void Initialize()
        {
            _view = _player.GetView<IGrabberView>();
            _cameraView = _player.GetView<ICharacterCameraView>();
            _raycastBroadcaster = _player.GetView<IRaycastBroadcaster<AttachableItemView>>();

            _dropUI = _uiService.CreateUIElement<GrabbingDropUI>(UIElementID.GrabbingDrop);
            _tooltipUI = _uiService.CreateUIElement<TooltipUI>(UIElementID.GrabbingTooltip);

            _model = new GrabbingData(_config.Graph);

            _view.Initialize(_model, _cameraView.Transform);
            _raycastBroadcaster.Initialize(_cameraView.Transform);
            _tooltipUI.Initialize(_cameraView.Transform);
        }

        public void Start()
        {
            _inputService.Inputs.ActionButton.PointerUp += OnPointerUp;
        }

        public void Dispose()
        {
            _inputService.Inputs.ActionButton.PointerUp -= OnPointerUp;
        }

        public void Tick()
        {
            float holdValue = ClampHoldTime(_inputService.Inputs.ActionButton.HoldValue) * (_isGrabActive ? 1 : 0);
            float targetValue = _config.DropDelayClamp.y;

            _dropUI.Render(targetValue, holdValue);
            _tooltipUI.Render(_raycastBroadcaster.CurrentHit == null ? null : _raycastBroadcaster.CurrentHit.transform);
        }
            
        private void OnPointerUp(float holdTime)
        {
            if (_isGrabActive)
            {
                _isGrabActive = false;
                _view.Drop(ClampHoldTime(holdTime));
            }
            else if (_raycastBroadcaster.IsHit)
            {
                _isGrabActive = true;
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
