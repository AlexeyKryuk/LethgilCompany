using Core;
using System;
using VContainer.Unity;

namespace Customization
{
    public class CustomizationEditorPresenter : IInitializable, ISaveLoaded, IDisposable
    {
        private readonly CustomizationView _customizationView;
        private readonly ISaveService<CustomizationInfo> _saveService;
        private readonly CustomizationUI _uIElement;

        private CustomizationInfo _model;

        public CustomizationEditorPresenter(CustomizationView customizationView, ISaveService<CustomizationInfo> saveService, CustomizationUI uIElement)
        {
            _customizationView = customizationView;
            _saveService = saveService;
            _uIElement = uIElement;
        }

        public string Key => "CustomizationInfo";

        public void Initialize()
        {
            _model = _saveService.Load(this, new CustomizationInfo());

            _uIElement.Selected += OnSelected;
            _uIElement.Initialize(_model.Skin);

            _customizationView.Set(_model.Skin);
        }

        public void Dispose()
        {
            _uIElement.Selected -= OnSelected;
            _saveService.Save(this, _model);
        }

        private void OnSelected(SkinType skin)
        {
            _model.SetSkin(skin);
            _customizationView.Set(skin);
        }
    }
}
