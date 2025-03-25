using Core;
using Core.View;
using Customization;
using VContainer.Unity;

namespace Network
{
    public class CustomizationPhotonPresenter : ILifetimeCycleService, IStartable, ITickable, ISaveLoaded
    {
        private readonly IPlayerPresenter _player;
        private readonly ISaveService<CustomizationInfo> _saveService;

        private ICharacterCameraView _characterCameraView;
        private IRaycastBroadcaster<CustomizationPhotonView> _raycastBroadcaster;

        public CustomizationPhotonPresenter(IPlayerPresenter player, ISaveService<CustomizationInfo> saveService)
        {
            _player = player;
            _saveService = saveService;
        }

        public string Key => "CustomizationInfo";

        public void Start()
        {
            _raycastBroadcaster = _player.GetView<IRaycastBroadcaster<CustomizationPhotonView>>();
            _characterCameraView = _player.GetView<ICharacterCameraView>();

            var model = _saveService.Load(this, new());
            var customizationView = _player.GetView<ICustomizationView>();

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
