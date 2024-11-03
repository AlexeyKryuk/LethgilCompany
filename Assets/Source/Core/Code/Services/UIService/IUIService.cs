using VContainer.Unity;

namespace Core
{
    public interface IUIService : ILifetimeCycleService, IInitializable
    {
        T CreateUIElement<T>(UIElementID id) where T : IUIElement;
    }
}
