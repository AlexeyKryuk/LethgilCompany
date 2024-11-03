using Core;
using UnityEngine;

namespace Customization
{
    public class CustomizationService : ICustomizationService
    {
        private readonly ISaveService<CustomizationInfo> _saveService;
        private readonly IUIService _uiService;
        private readonly Transform _spawnPoint;

        private CustomizationEditorPresenter _presenter;

        public CustomizationService(ISaveService<CustomizationInfo> saveService, IUIService uiService,
            PlayerSpawnPoint spawnPoint)
        {
            _saveService = saveService;
            _uiService = uiService;
            _spawnPoint = spawnPoint.transform;
        }

        public string Key => "CustomizationInfo";

        public void Initialize()
        {
            var uiElement = _uiService.CreateUIElement<CustomizationUI>(UIElementID.CustomizationUI);
            var view = uiElement.GetComponentInChildren<CustomizationView>();

            uiElement.transform.SetParent(_spawnPoint);

            _presenter = new CustomizationEditorPresenter(view, _saveService, uiElement);
            _presenter.Initialize();
        }

        public void Dispose() => _presenter.Dispose();
    }
}
