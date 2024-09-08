using Core;
using Core.View;
using VContainer.Unity;

namespace ItemGrabbing
{
    public class GrabbingService : IGrabbingService, IInitializable
    {
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;
        private readonly PlayerService _playerService;

        private ICharacterInputs _inputs;
        private IGrabberView _grabber;

        private GrabbingUI _grabbingUI;
        private GrabbingCanvas _grabbingCanvas;

        private bool _isGrabbing;
        private float _dropPower = 100f;

        public GrabbingService(IInputService inputService, IUIService uiService, PlayerService playerService)
        {
            _inputService = inputService;
            _uiService = uiService;
            _playerService = playerService;
        }

        public void Initialize()
        {
            _inputs = _inputService.CharacterInputs;
            _grabber = _playerService.Presenter.GetView<IGrabberView>();

            _grabbingUI = _uiService.CreateUIElement<GrabbingUI>(UIElementID.GrabberView);
            _grabbingCanvas = _uiService.CreateUIElement<GrabbingCanvas>(UIElementID.GrabbingCanvas);
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
            _grabbingUI.Render(_grabber.IsGrabReady && _grabber.IsGrabActive == false);
            _grabbingCanvas.Render(_inputs.ActionButton.HoldValue * _dropPower);
        }

        private void OnPointerDown()
        {
            if (_grabber.IsGrabReady)
                _grabber.Grab().Attach(_grabber);
        }

        private void OnPointerUp(float holdTime)
        {
            if (_isGrabbing == false)
                _isGrabbing = true;
            else
            {
                if (_grabber.IsGrabActive)
                {
                    _grabber.Drop().Drop(holdTime * _dropPower);
                    _isGrabbing = false;
                }
            }
        }

        public void LateTick() { }
    }
}
