namespace Core
{
    public interface IAttachable
    {
        bool IsAttached { get; }
        void Attach(IGrabber grabber);
        void Drop();
    }
}
