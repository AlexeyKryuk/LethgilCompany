namespace Core
{
    public interface IGrabber
    {
        IAttachable Grab();
        IAttachable Drop();

        bool IsGrabActive { get; }
    }
}
