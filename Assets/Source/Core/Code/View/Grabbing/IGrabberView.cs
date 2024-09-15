using UnityEngine;

namespace Core.View
{
    public interface IGrabberView
    {
        void Initialize(Transform dropForward);
        IAttachableView Grab();
        IAttachableView Drop();

        Transform Anchor { get; }
        Transform DropForward { get; }
        bool IsGrabReady { get; }
        bool IsGrabActive { get; }
    }
}
