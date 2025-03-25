using System.Collections.Generic;

namespace Core
{
    public class UIService : IUIService
    {
        private readonly IUIFactory _factory;

        private List<IUIElement> _uiElements = new List<IUIElement>();

        public UIService(IUIFactory factory)
        {
            _factory = factory;
        }

        public T CreateUIElement<T>() where T : IUIElement
        {
            T element = _factory.CreateUIElement<T>();
            _uiElements.Add(element);

            return element;
        }

        public void Initialize()
        {
            _factory.CreateUIElement<MainCanvas>();
        }

        public void DisableAll()
        {
            foreach (var element in _uiElements)
                element.Disable();
        }
    }
}
