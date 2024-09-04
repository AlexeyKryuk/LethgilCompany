using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class UIService : IUIService
    {
        private readonly UIFactory _factory;

        private List<IUIElement> _uiElements = new List<IUIElement>();

        public UIService(UIFactory factory)
        {
            _factory = factory;
        }

        public T CreateUIElement<T>(UIElementID id) where T : IUIElement
        {
            T element = _factory.CreateUIElement<T>();
            _uiElements.Add(element);

            return element;
        }

        public void Initialize()
        {
            _factory.CreateUIElement<MainCanvas>();
        }

        public void Start()
        {
        }

        public void Tick()
        {
        }

        public void LateTick()
        {
        }

        public void Dispose()
        {
        }

        public void DisableAll()
        {
            foreach (var element in _uiElements)
                element.Disable();
        }
    }
}
