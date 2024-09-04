namespace Core.Model
{
    public interface IGrabber
    {
        void Grab(IAttachable attachable);
        IAttachable Drop();

        bool IsGrabActive { get; }
    }
}
