namespace Core
{
    public interface IAttachableView
    {
        void Attach(IGrabberView grabber);
        void Drop();
    }
}
