using Core.Model;

namespace ItemGrabbing
{
    public class Grabber : IGrabber
    {
        private IAttachable _attachable;

        public Grabber(IAttachable attachable)
        {
            _attachable = attachable;
        }

        public bool IsGrabActive => _attachable != null;

        public IAttachable Drop()
        {
            var dropped = _attachable;
            _attachable = null;

            return dropped;
        }

        public void Grab(IAttachable attachable)
        {
            _attachable = attachable;
        }
    }
}
