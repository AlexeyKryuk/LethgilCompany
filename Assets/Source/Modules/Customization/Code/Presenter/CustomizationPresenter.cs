using Core;
using VContainer.Unity;

namespace Customization
{
    public class CustomizationPresenter : ILifetimeCycleService, IStartable, ISaveLoaded
    {
        private readonly IPlayerService _playerService;
        private readonly ISaveService<CustomizationInfo> _saveService;

        public CustomizationPresenter(IPlayerService playerService, ISaveService<CustomizationInfo> saveService)
        {
            _playerService = playerService;
            _saveService = saveService;
        }

        public string Key => "CustomizationInfo";

        public void Start()
        {
            var model = _saveService.Load(this, new());
            var view = _playerService.GetView<ICustomizationView>();
            var nicknameView = _playerService.GetView<NickNameView>();

            view.Set(model.Skin);
        }
    }
}
