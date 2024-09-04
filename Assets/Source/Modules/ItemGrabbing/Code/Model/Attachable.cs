using Core.Model;

namespace ItemGrabbing
{
    public class Attachable : IAttachable
    {
        private IGrabber _grabber;

        public Attachable(IGrabber grabber)
        {
            _grabber = grabber;
        }

        public bool IsAttached => _grabber != null;

        public void Attach(IGrabber grabber)
        {
            _grabber = grabber;
        }

        public void Drop()
        {
            _grabber = null;
        }
    }
}
