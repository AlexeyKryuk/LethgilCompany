namespace Core
{
    public interface IUIService : ILifetimeCycleService
    {
        T CreateUIElement<T>(UIElementID id) where T : IUIElement;
    }
}
