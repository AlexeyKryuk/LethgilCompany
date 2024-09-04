using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class UIFactory
    {
        private readonly UIConfig _uiConfig;
        private readonly IObjectResolver _objectResolver;

        public UIFactory(IObjectResolver objectResolver, UIConfig config)
        {
            _objectResolver = objectResolver;
            _uiConfig = config;
        }

        public T CreateUIElement<T>() where T : IUIElement
        {
            GameObject gameObject = _objectResolver.Instantiate(_uiConfig.GetPrefab<T>());

            return gameObject.GetComponent<T>();
        }
    }
}
