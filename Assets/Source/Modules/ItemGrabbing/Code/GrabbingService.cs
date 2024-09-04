using Core;
using Core.View;
using VContainer.Unity;
using UnityEngine;

namespace ItemGrabbing
{
    public class GrabbingService : IGrabbingService, IInitializable
    {
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;
        private readonly PlayerService _playerService;

        private IGrabberView _grabber;
        private GrabbingUI _grabbingUI;

        public GrabbingService(IInputService inputService, IUIService uiService, PlayerService playerService)
        {
            _inputService = inputService;
            _uiService = uiService;
            _playerService = playerService;
        }

        public void Initialize()
        {
            _grabber = _playerService.Presenter.GetView<IGrabberView>();
            _grabbingUI = _uiService.CreateUIElement<GrabbingUI>(UIElementID.GrabberView);
        }

        public void Start()
        {

        }

        public void Tick()
        {
            CheckInputs();
            RenderUI();
        }

        public void LateTick()
        {

        }

        public void Dispose()
        {

        }

        private void RenderUI()
        {
            _grabbingUI.Render(_grabber.IsGrabReady && _grabber.IsGrabActive == false);
        }

        private void CheckInputs()
        {
            if (_inputService.CharacterInputs.ActionButton)
            {
                if (_grabber.IsGrabActive)
                {
                    var droped = _grabber.Drop();
                    droped.Drop();
                }
                else if (_grabber.IsGrabReady)
                {
                    var item = _grabber.Grab();
                    item.Attach(_grabber);
                }

                _inputService.CharacterInputs.ActionButton = false;
            }
        }
    }
}
