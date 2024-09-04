namespace Core.View
{
    public interface IAttachableView
    {
        void Attach(IGrabberView grabber);
        void Render(bool isReady);
        void Drop();
    }
}
