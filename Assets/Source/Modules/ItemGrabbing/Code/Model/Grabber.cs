namespace ItemGrabbing
{
    public class Grabber : IGrabber
    {
        public bool IsGrabActive { get; private set; }

        public void Grab()
        {
            IsGrabActive = true;
        }

        public void Drop()
        {
            IsGrabActive = false;
        }
    }
}
