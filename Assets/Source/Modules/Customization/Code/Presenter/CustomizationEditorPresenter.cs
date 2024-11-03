using Core;
using System;
using VContainer.Unity;

namespace Customization
{
    public class CustomizationEditorPresenter : IInitializable, ISaveLoaded, IDisposable
    {
        private readonly ISaveService<CustomizationInfo> _saveService;
        private readonly CustomizationUI _uIElement;
        private readonly CustomizationView _customizationView;
        private readonly NicknameConfirmButton _nicknameConfirmButton;

        private CustomizationInfo _model;

        public CustomizationEditorPresenter(CustomizationView customizationView, ISaveService<CustomizationInfo> saveService, 
            CustomizationUI uIElement, NicknameConfirmButton nicknameConfirmButton)
        {
            _nicknameConfirmButton = nicknameConfirmButton;
            _customizationView = customizationView;
            _saveService = saveService;
            _uIElement = uIElement;
        }

        public string Key => "CustomizationInfo";

        public void Initialize()
        {
            _model = _saveService.Load(this, new());

            _uIElement.Selected += OnSelected;
            _nicknameConfirmButton.NicknameConfirmed += OnNicknameConfirmed;
        }

        public void Dispose()
        {
            _uIElement.Selected -= OnSelected;
            _nicknameConfirmButton.NicknameConfirmed -= OnNicknameConfirmed;

            _saveService.Save(this, _model);
        }

        private void OnSelected(SkinType skin)
        {
            _model.Skin = skin;
            _customizationView.Set(skin);
        }

        private void OnNicknameConfirmed(string nickname)
        {
            _model.NickName = nickname;
        }
    }
}
