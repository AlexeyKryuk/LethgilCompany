namespace ItemGrabbing
{
    public interface IGrabber
    {
        bool IsGrabActive { get; }

        void Grab();
        void Drop();
    }
}
