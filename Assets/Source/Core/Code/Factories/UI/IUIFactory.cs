namespace Core
{
    public interface IUIFactory
    {
        T CreateUIElement<T>() where T : IUIElement;
    }
}
