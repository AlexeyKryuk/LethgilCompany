using System;

namespace Core.Model
{
    public class Loot : IAttachable
    {
        public bool IsAttached { get; private set; }

        public Loot(bool isAttached)
        {
            IsAttached = isAttached;
        }

        public void Attach()
        {
            if (IsAttached)
                throw new Exception("The item is already attached");

            IsAttached = true;
        }

        public void UnAttach()
        {
            if (!IsAttached) 
                throw new Exception("The item is already unattached");

            IsAttached = false;
        }
    }
}
