using System;

namespace ItemGrabbing
{
    public class Attachable : IAttachable
    {
        public bool IsAttached { get; private set; }

        public void Attach()
        {
            if (IsAttached)
                throw new InvalidOperationException("This is object is already attached!");

            IsAttached = true;
        }

        public void UnAttach()
        {
            if (!IsAttached)
                throw new InvalidOperationException("This is object is already unattached!");

            IsAttached = false;
        }
    }
}
