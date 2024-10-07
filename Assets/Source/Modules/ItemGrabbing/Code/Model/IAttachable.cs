namespace ItemGrabbing
{
    public interface IAttachable
    {
        bool IsAttached { get; }

        void Attach();
        void UnAttach();
    }
}
