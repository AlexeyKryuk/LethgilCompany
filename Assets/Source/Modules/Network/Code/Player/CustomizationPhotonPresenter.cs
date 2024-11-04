using Core;
using Core.View;
using Customization;
using VContainer.Unity;

namespace Network
{
    public class CustomizationPhotonPresenter : ILifetimeCycleService, IStartable, ITickable, ISaveLoaded
    {
        private readonly IPlayerService _playerService;
        private readonly ISaveService<CustomizationInfo> _saveService;

        private ICharacterCameraView _characterCameraView;
        private IRaycastBroadcaster<CustomizationPhotonView> _raycastBroadcaster;

        public CustomizationPhotonPresenter(IPlayerService playerService, ISaveService<CustomizationInfo> saveService)
        {
            _playerService = playerService;
            _saveService = saveService;
        }

        public string Key => "CustomizationInfo";

        public void Start()
        {
            _raycastBroadcaster = _playerService.GetView<IRaycastBroadcaster<CustomizationPhotonView>>();
            _characterCameraView = _playerService.GetView<ICharacterCameraView>();

            var model = _saveService.Load(this, new());
            var customizationView = _playerService.GetView<ICustomizationView>();

            customizationView.Set(model);
        }

        public void Tick()
        {
            if (_raycastBroadcaster.IsHit)
                _raycastBroadcaster.CurrentHit.NickNameView.Show(_characterCameraView.Transform);
            else if (_raycastBroadcaster.LastHit != null)
                _raycastBroadcaster.LastHit.NickNameView.Hide();
        }
    }
}
