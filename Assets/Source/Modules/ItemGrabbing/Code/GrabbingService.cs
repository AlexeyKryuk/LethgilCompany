using Core;
using Core.View;
using UnityEngine;
using VContainer.Unity;

namespace ItemGrabbing
{
    public class GrabbingService : IGrabbingService, IInitializable
    {
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;

        private readonly PlayerService _playerService;
        private readonly GrabbingConfig _config;

        private ICharacterInputs _inputs;
        private IGrabberView _grabber;

        private GrabbingUI _grabbingUI;
        private bool _isGrabbing;

        public GrabbingService(IInputService inputService, IUIService uiService, PlayerService playerService, GrabbingConfig config)
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

        private void OnPointerDown()
        {
            if (_grabber.IsGrabReady)
                _grabber.Grab().Attach(_grabber);
        }

        private void OnPointerUp(float holdTime)
        {
            if (_isGrabbing == false)
                _isGrabbing = _grabber.IsGrabReady;
            else
            {
                if (_grabber.IsGrabActive)
                {
                    float power = GetDropPower(ClampHoldTime(holdTime));
                    IAttachableView attachable = _grabber.Drop();

                    attachable.Drop(power);

                    _isGrabbing = false;
                }
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
