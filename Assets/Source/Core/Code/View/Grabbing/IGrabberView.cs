using UnityEngine;

namespace Core.View
{
    public interface IGrabberView
    {
        void Initialize(Transform dropForward);
        IAttachableView Grab();
        IAttachableView Drop();

        IAttachableView ItemInRange { get; }

        Transform Anchor { get; }
        Transform DropForward { get; }

        bool IsGrabReady { get; }
        bool IsGrabActive { get; }
    }
}
