using Core;
using Core.View;
using Photon.Pun;

namespace ItemGrabbing
{
    public class GrabbingService : IGrabbingService
    {
        private readonly IUIService _uiService;
        private readonly IInputService _inputService;
        private readonly IPlayerService _playerService;
        private readonly GrabbingConfig _config;

        private GrabbingPresenter _presenter;

        public GrabbingService(IUIService uiService, IInputService inputService, 
            IPlayerService playerService, GrabbingConfig config)
        {
            _uiService = uiService;
            _inputService = inputService;
            _playerService = playerService;
            _config = config;
        }

        public void Initialize()
        {
            _presenter = CreatePresenter();
            _presenter.Initialize();
        }

        public void Start() => _presenter.Start();
        public void Tick() => _presenter.Tick();
        public void Dispose() => _presenter.Dispose();

        private GrabbingPresenter CreatePresenter()
        {
            var view = _playerService.GetView<IGrabberView>();
            var cameraView = _playerService.GetView<ICharacterCameraView>();
            var raycastBroadcaster = _playerService.GetView<IRaycastBroadcaster<AttachableItemView>>();
            var photonView = _playerService.GetView<PhotonView>();
            var dropUI = _uiService.CreateUIElement<GrabbingDropUI>(UIElementID.GrabbingDrop);
            var tooltipUI = _uiService.CreateUIElement<GrabbingTooltipUI>(UIElementID.GrabbingTooltip);

            return new GrabbingPresenter(view, cameraView, _inputService, raycastBroadcaster, 
                photonView, dropUI, tooltipUI, _config);
        }
    }
}
