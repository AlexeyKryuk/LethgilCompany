namespace Core
{
    public interface IUIElement
    {
        UIElementID UIElementType { get; }

        void Enable();
        void Disable();
    }
}
