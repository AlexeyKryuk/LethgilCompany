using Core;
using UnityEngine;

namespace Customization
{
    public class CustomizationService : ICustomizationService
    {
        private readonly ISaveService<CustomizationInfo> _saveService;
        private readonly IUIService _uiService;
        private readonly NicknameConfirmButton _nicknameConfirmButton;

        private CustomizationEditorPresenter _presenter;

        public CustomizationService(ISaveService<CustomizationInfo> saveService, IUIService uiService,
            NicknameConfirmButton nicknameConfirmButton)
        {
            _saveService = saveService;
            _uiService = uiService;
            _nicknameConfirmButton = nicknameConfirmButton;
        }

        public string Key => "CustomizationInfo";

        public void Initialize()
        {
            var uiElement = _uiService.CreateUIElement<CustomizationUI>();
            var view = uiElement.GetComponentInChildren<CustomizationView>();

            uiElement.transform.SetParent(GameObject.FindGameObjectWithTag(GameObjectTags.PlayerSpawnPoint.ToString()).transform);

            _presenter = new CustomizationEditorPresenter(view, _saveService, uiElement, _nicknameConfirmButton);
            _presenter.Initialize();
        }

        public void Dispose() => _presenter.Dispose();
    }
}
